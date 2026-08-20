using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Bazneshastegi.Presentation.Middlewares;

public sealed class CaptchaValidatorMiddleware
{
    public const string CaptchaId_HeaderName = "x-CaptchaId";
    public const string CaptchaValue_HeaderName = "x-CaptchaValue";

    private readonly RequestDelegate _next;

    public CaptchaValidatorMiddleware(RequestDelegate requestDelegate)
    {
        this._next = requestDelegate;
    }

    public async Task Invoke(
        HttpContext httpContext,
        IServiceScopeFactory serviceScopeFactory,
        ILogger<CaptchaValidatorMiddleware> logger)
    {
        /*
        #if DEBUG
                httpContext.SetCaptchaValidated();
                await this._next(httpContext);
                return;
        #endif
        */
        try
        {
            var captchaId = httpContext.Request.Headers[CaptchaId_HeaderName].FirstOrDefault();
            var captchaValue = httpContext.Request.Headers[CaptchaValue_HeaderName].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(captchaId) || string.IsNullOrWhiteSpace(captchaValue))
            {
                httpContext.SetCaptchaNotSent();
                await this._next(httpContext);
                return;
            }

            using var scope = serviceScopeFactory.CreateScope();
            var captchaService = scope.ServiceProvider.GetRequiredService<CaptchaProvider.ICaptchaValidator>();

            var result = await captchaService.Validate(captchaId, captchaValue, CancellationToken.None);

            if (result.IsSuccess)
            {
                httpContext.SetCaptchaValidated();
            }
            else
            {
                httpContext.SetCaptchaIsNotValid();
            }
        }
        catch
        {
            throw;
        }
        finally
        {
        }
        await this._next(httpContext);
        return;
    }
}
internal static partial class HttpContextExtensions
{
    public const string CaptchaStatusKey = "CaptchaStatus";

    public static void SetCaptchaValidated(this HttpContext httpContext)
    {
        httpContext.Items.Add(CaptchaStatusKey, CaptchaStatuses.Validated.GetHashCode());
    }
    public static void SetCaptchaIsNotValid(this HttpContext httpContext)
    {
        httpContext.Items.Add(CaptchaStatusKey, CaptchaStatuses.IsNotValid.GetHashCode());
    }
    public static void SetCaptchaNotSent(this HttpContext httpContext)
    {
        httpContext.Items.Add(CaptchaStatusKey, CaptchaStatuses.NotSent.GetHashCode());
    }

    public static CaptchaStatuses GetCaptchaStatus(this HttpContext httpContext)
    {
        if (httpContext.Items.TryGetValue(CaptchaStatusKey, out var statusObj)
            && statusObj is int statusInt
            && Enum.IsDefined(typeof(CaptchaStatuses), statusInt))
            return (CaptchaStatuses)statusInt;

        return CaptchaStatuses.NotSent;
    }
}
internal static partial class HttpContextExtensions
{
    public enum CaptchaStatuses
    {
        NotSent = -1,
        Validated = 0,
        IsNotValid = 1
    }
}
