using Asp.Versioning;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using Bazneshastegi.Presentation.AppCore;
using Bazneshastegi.Presentation.Features.Captcha;
using Bazneshastegi.Presentation.GlobalExceptionHandlers;
using Bazneshastegi.Presentation.Options;
using Bazneshastegi.Presentation.Services.ApplicationServices.UserContextAccessorServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SRH.ServiceInstaller;
using System.Reflection;

namespace Bazneshastegi.Presentation.DI;
internal sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => null;

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config)
    {
        services.TryAddSingleton<IResultHandler, ResultHandler>();

        return services
            .ConfigureOptions<JsonOptionsConfigurator>()
            .ScanAndAddPresentationMappers([PresentationAssemblyReference.Assembly])
            .InstallGlobalExceptionHandler()
            .InstallApiVersioning()
            .Configure<CaptchaOption>(config.GetSection("Captcha"))
            .AddDistributedMemoryCache()
            .AddSession(options =>
            {
                options.Cookie.Name = ".MyApp.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;

                options.IdleTimeout = TimeSpan.FromMinutes(30);
            })
            .InstallCaptchaProvider(config);
    }
}

static class ServiceCollectionExtension
{
    static readonly Type MaperGenericInterfaceType = typeof(IPresentationMapper<,>);

    public static IServiceCollection InstallGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.TryAddSingleton<IUserContextAccessor, UserContextAccessor>();

        return services;
    }
    internal static IServiceCollection ScanAndAddPresentationMappers(this IServiceCollection services, params Assembly[] assemblies)
    {
        var implementingTypes = assemblies.SelectMany(a => a.GetTypes())
            .Where(type => type is { IsAbstract: false } && type.GetInterfaces().Any(
                i => i.IsGenericType && i.GetGenericTypeDefinition() == MaperGenericInterfaceType))
            .ToArray();

        foreach (var type in implementingTypes)
        {
            var serviceType = MaperGenericInterfaceType.MakeGenericType(type.GetInterfaces()
                .First(i => i.GetGenericTypeDefinition() == MaperGenericInterfaceType)
                .GetGenericArguments());

            services.Add(
                ServiceDescriptor.Describe(serviceType, type, ServiceLifetime.Transient));
        }
        return services;

    }
    internal static IServiceCollection InstallApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("X-Version"));

        }).AddApiExplorer(options =>
        {
            // Format: v1, v2, ...
            options.GroupNameFormat = "'v'VVV";

            // Substitute version into URL
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddMinimalEndpoints(PresentationAssemblyReference.Assembly);

        return services;
    }
}
