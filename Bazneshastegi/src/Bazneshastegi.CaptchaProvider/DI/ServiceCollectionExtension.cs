using Bazneshastegi.CaptchaProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtension
{
    public static IServiceCollection InstallCaptchaProvider(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<ICaptchaService, CaptchaService>();
        services.TryAddSingleton<ICaptchaValidator, CaptchaService>();
        return services;
    }
}
