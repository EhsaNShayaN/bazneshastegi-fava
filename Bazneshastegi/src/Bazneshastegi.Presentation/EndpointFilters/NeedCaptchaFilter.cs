using Bazneshastegi.Presentation.AppCore;
using Bazneshastegi.Presentation.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using static Bazneshastegi.Presentation.Middlewares.HttpContextExtensions;

namespace ER.Sanjesh.Presentation.EndpointFilters;

public sealed class NeedCaptchaFilter : IEndpointFilter
{
    private readonly IResultHandler _resultHandler;


    public readonly static PrimitiveResult<bool> InvalidCaptchaError = PrimitiveResult.Failure<bool>(
          DomainErrorCodes.InvalidCaptcha_ErrorCode,
          $"کد امنیتی وارد شده صحیح نیست."
      );

    public NeedCaptchaFilter(IResultHandler resultHandler)
    {
        this._resultHandler = resultHandler;
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        context.HttpContext.GetCaptchaStatus();

        if (context.HttpContext.GetCaptchaStatus() != CaptchaStatuses.Validated)
        {
            return DefaultResponseFactory.Instance.CreateOk(InvalidCaptchaError);
        }

        return await next(context);
    }
}
internal sealed class NeedCaptchaFactory
{
    public NeedCaptchaFactory()
    {
    }

    public EndpointFilterDelegate Create(IServiceProvider services, EndpointFilterDelegate next)
    {
        var resultHandler = services.GetRequiredService<IResultHandler>();

        var filter = new NeedCaptchaFilter(resultHandler);
        return context => filter.InvokeAsync(context, next);
    }
}
public static class EndpointConventionBuilderExtensions
{
    public static TBuilder RequireCaptchaValidation<TBuilder>(this TBuilder group)
        where TBuilder : IEndpointConventionBuilder
    {
        var factory = new NeedCaptchaFactory();

        group.AddEndpointFilterFactory((context, next) =>
            factory.Create(context.ApplicationServices, next));

        return group;
    }
}
