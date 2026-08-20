using Bazneshastegi.CaptchaProvider;
using Bazneshastegi.Presentation.AppCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Bazneshastegi.Presentation.Features.Captcha;

public static partial class CaptchaEndpoints
{
    const string TagName = "Bazneshastegi";

    static List<Func<IEndpointRouteBuilder, RouteHandlerBuilder>> _routeHandlerBuilders = new();
    static CaptchaEndpoints()
    {
        AddCreateNewCaptchaIdEndpoint();
        AddGetCaptchaImageEndpoint();
        AddGetAndRenewCaptchaImageEndpoint();
    }
    public static void AddAllEndpoitnts(WebApplication app, RouteGroupBuilder? group = null)
    {
        IEndpointRouteBuilder routeBuilder = group ?? (IEndpointRouteBuilder)app;

        foreach (var route in _routeHandlerBuilders)
        {
            route.Invoke(routeBuilder);
        }
    }

    static void AddCreateNewCaptchaIdEndpoint()
    {
        _routeHandlerBuilders.Add((app) =>
        {
            var builder = app.MapGet("api/v1/captcha/new",
                async (
                    ICaptchaService captchaService,
                    IOptionsMonitor<CaptchaOption> captchaOption,
                    IResultHandler resultHandler,
                    CancellationToken cancellationToken) =>
                {
                    var expiry = captchaOption.CurrentValue.Expiry;
                    var result = await captchaService.GenerateNewCaptchaDigit(expiry, cancellationToken)
                        .Map(captchaId => new { Id = captchaId, Expiry = expiry })
                        .ConfigureAwait(false);

                    return resultHandler.Handle(result);
                })
            .WithTags(TagName);
            return builder;
        });
    }
    static void AddGetCaptchaImageEndpoint()
    {
        _routeHandlerBuilders.Add((app) =>
        {
            var builder = app.MapGet("api/v1/captcha",
                async (
                    [AsParameters] CaptchaInfo info,
                    ICaptchaService captchaService,
                    IOptionsMonitor<CaptchaOption> captchaOption,
                    IResultHandler resultHandler,
                    CancellationToken cancellationToken) =>
                {
                    if (string.IsNullOrWhiteSpace(info.Id)) Results.NotFound();

                    var result = await captchaService.GetCaptchaById(
                        info.Id ?? string.Empty,
                        info.Width ?? 200,
                        info.Height ?? 100,
                        cancellationToken)
                        .ConfigureAwait(false);

                    if (result.IsFailure) return Results.NotFound();

                    var image = result.Value;
                    image.Position = 0;

                    return Results.File(image.ToArray(), "image/png");
                })
            .WithTags(TagName);
            return builder;
        });
    }
    static void AddGetAndRenewCaptchaImageEndpoint()
    {
        _routeHandlerBuilders.Add((app) =>
        {
            var builder = app.MapGet("api/v1/captcha/img",
                async (
                    [AsParameters] CaptchaInfo info,
                    ICaptchaService captchaService,
                    IOptionsMonitor<CaptchaOption> captchaOption,
                    HttpContext httpContext,
                    IResultHandler resultHandler,
                    CancellationToken cancellationToken) =>
                {
                    var expiry = captchaOption.CurrentValue.Expiry;

                    var oldCaptchaId = string.IsNullOrWhiteSpace(info.Id)
                        ? httpContext.Request.Headers[captchaOption.CurrentValue.OldCaptchaIdHeaderName].FirstOrDefault() ?? string.Empty
                        : info.Id ?? string.Empty;

                    var result = await captchaService.RenewCaptchaDigit(
                        oldCaptchaId,
                        expiry,
                        info.Width ?? 200,
                        info.Height ?? 100,
                        cancellationToken)
                    .OnSuccess(x => x.Value.MemoryStream.Position = 0)
                    .Map(x => new CaptchaResponse(
                        x.Id,
                        expiry,
                        $"data:image/png;base64,{Convert.ToBase64String(x.MemoryStream.ToArray())}"))
                    .ConfigureAwait(false);

                    if (result.IsFailure) return Results.NotFound();

                    return resultHandler.Handle(result);

                }).WithTags(TagName);

            return builder;
        });
    }

    sealed record CaptchaResponse(string Id, TimeSpan Expiry, string Image);
}
internal sealed class CaptchaOption
{
    public TimeSpan Expiry { get; set; } = TimeSpan.FromMinutes(2);
    public string OldCaptchaIdHeaderName { get; set; } = "x-OldCaptchaId";
}
public sealed record CaptchaInfo(string? Id, int? Width, int? Height);