using Bazneshastegi.Domain.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.FeatureManagement;
using Polly.Contrib.DuplicateRequestCollapser;
using SRH.MediatRMessaging;
using SRH.ServiceInstaller;
using System.Reflection;

namespace Bazneshastegi.Application.DI;
internal sealed class ProjectServiceInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => null;

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config)
    {
        return services
            .InstallDomainEntityValidators(config, this.DependantAssemblies)
            .InstallMediatRServices()
            .InstallRequestCollapser()
            .InstallFeatureManagment();
    }
}

static class ServiceCollectionExtension
{
    public static IServiceCollection InstallDomainEntityValidators(this IServiceCollection services,
       IConfiguration config,
       Assembly[]? dependantAssemblies)
    {
        services.Scan(scan =>
            scan
                .FromAssemblies([ApplicationAssemblyReference.Assembly, .. dependantAssemblies ?? []])
                .AddClasses(classes => classes.AssignableTo<IDomainEntityValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        return services;
    }

    public static IServiceCollection InstallMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), MediatRMessaginAssemblyReference.Assembly);
        });

        return services;
    }
    public static IServiceCollection InstallRequestCollapser(this IServiceCollection services)
    {
        services.TryAddSingleton(_ => AsyncRequestCollapserPolicy.Create());
        return services;
    }
    public static IServiceCollection InstallFeatureManagment(this IServiceCollection services)
    {
        services.AddFeatureManagement();

        return services;
    }
}
