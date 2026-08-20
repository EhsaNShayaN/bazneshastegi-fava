using Bazneshastegi.Application;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserAuthenticationServices;
using Bazneshastegi.Infrastructure.Services.Provider;
using Bazneshastegi.Infrastructure.Services.UserAuthenticationToken;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Polly;
using SRH.ServiceInstaller;
using System.Reflection;

namespace Bazneshastegi.Infrastructure.DI;
internal sealed class ProjectInstaller : IServiceInstaller
{
    public Assembly[]? DependantAssemblies => [ApplicationAssemblyReference.Assembly];

    public IServiceCollection InstallService(IServiceCollection services, IConfiguration config) =>
        services
            .InstallDomainServices(this.DependantAssemblies)
            .InstallIHttpContextAccessor()
            .InstallBazneshastegiService(config)
            .InstallUserAuthenticationTokenService(config);
}
static class ServiceCollectionExtension
{
    internal static IServiceCollection InstallUserAuthenticationTokenService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<UserAuthenticationTokenServiceOptions>(configuration.GetSection("Jwt"));
        services.TryAddSingleton<IUserAuthenticationTokenService, UserAuthenticationTokenService>();

        return services;
    }


    internal static IServiceCollection InstallDomainServices(this IServiceCollection services, Assembly[]? dependantAssemblies)
    {
        return services.Scan(scan =>
            scan
                .FromAssemblies([InfrastructureAssemblyReference.Assembly, .. dependantAssemblies ?? []])
                .AddClasses(classes => classes.AssignableTo<Domain.Abstractions.IDomainValidator>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );
    }

    internal static IServiceCollection InstallIHttpContextAccessor(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();

        return services;
    }
    internal static IServiceCollection InstallBazneshastegiService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BazneshastegiServiceOptions>(configuration.GetSection("Bazneshastegi"));

        services.TryAddScoped<IBazneshastegiCredentialProvider, BazneshastegiCredentialProvider>();

        services.TryAddTransient<BazneshastegiServiceLoggerDelegatingHandler>();
        services.TryAddTransient<BazneshastegiTokenDelegatingHandler>();
        services.TryAddSingleton<BazneshastegiTokenProvider>();

        services.AddHttpClient(BazneshastegiService.HttpClientName,
            (sp, client) =>
            {
                var bazneshastegiServiceOpts = sp.GetRequiredService<IOptions<BazneshastegiServiceOptions>>();
                client.BaseAddress = new Uri(bazneshastegiServiceOpts.Value.BaseUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            })
            .AddHttpMessageHandler<BazneshastegiServiceLoggerDelegatingHandler>()
            .AddHttpMessageHandler<BazneshastegiTokenDelegatingHandler>()
            .AddResilienceHandler("BazneshastegiServiceResilienceStrategy", resilienceBuilder =>
            {
                // Retry Strategy configuration
                /*resilienceBuilder.AddRetry(new HttpRetryStrategyOptions // Configures retry behavior
                {
                    MaxRetryAttempts = 4, // Maximum retries before throwing an exception (default: 3)

                    Delay = TimeSpan.FromSeconds(2), // Delay between retries (default: varies by strategy)

                    BackoffType = DelayBackoffType.Constant, // Exponential backoff for increasing delays (default)

                    UseJitter = true, // Adds random jitter to delay for better distribution (default: false)

                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>() // Defines exceptions to trigger retries
                    .Handle<HttpRequestException>() // Includes any HttpRequestException
                    .HandleResult(response => !response.IsSuccessStatusCode) // Includes non-successful responses
                });*/

                resilienceBuilder.AddTimeout(TimeSpan.FromSeconds(50));

                // Circuit Breaker Strategy configuration
                /*resilienceBuilder.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions // Configures circuit breaker behavior
                {
                    // Tracks failures within this time frame
                    SamplingDuration = TimeSpan.FromSeconds(10),

                    // Trips the circuit if failure ratio exceeds this within sampling duration (20% failures allowed)
                    FailureRatio = 0.2,

                    // Requires at least this many successful requests within sampling duration to reset
                    MinimumThroughput = 3,

                    // How long the circuit stays open after tripping
                    BreakDuration = TimeSpan.FromSeconds(1),

                    // Defines exceptions to trip the circuit breaker
                    ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<HttpRequestException>() // Includes any HttpRequestException
                    .HandleResult(response => !response.IsSuccessStatusCode) // Includes non-successful responses
                });*/
            });

        services.TryAddScoped<IBazneshastegiService, BazneshastegiService>();
        services.TryAddScoped<IBazneshastegiLoginService, BazneshastegiService>();
        //services.Decorate<IBazneshastegiService, MoqBazneshastegiService>();

        return services;
    }
}