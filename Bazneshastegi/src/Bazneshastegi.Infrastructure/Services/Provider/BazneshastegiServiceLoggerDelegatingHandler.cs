using Microsoft.Extensions.Logging;

namespace Bazneshastegi.Infrastructure.Services.Provider;

public sealed class BazneshastegiServiceLoggerDelegatingHandler : DelegatingHandler
{
    private readonly ILogger<BazneshastegiServiceLoggerDelegatingHandler> _logger;

    public BazneshastegiServiceLoggerDelegatingHandler(ILogger<BazneshastegiServiceLoggerDelegatingHandler> logger)
    {
        this._logger = logger;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            this._logger.LogInformation($"infocomplete national code and birthdate....");


            var result = await base.SendAsync(request, cancellationToken);

            this._logger.LogInformation($"infocomplete national code and birthdatedone....");

            return result;
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, $"infocomplete national code and birthdate error!");
            throw;
        }
    }
}
