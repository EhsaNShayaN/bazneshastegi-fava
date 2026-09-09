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
            this._logger.LogInformation($"BazneshastegiServiceLoggerDelegatingHandler {request.RequestUri.ToString()} started");

            var result = await base.SendAsync(request, cancellationToken);

            this._logger.LogInformation($"BazneshastegiServiceLoggerDelegatingHandler {request.RequestUri.ToString()} finished");

            return result;
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, $"Error: {request.RequestUri.ToString()}");
            throw;
        }
    }
}


public sealed class BazneshastegiServiceExceptionHandler : DelegatingHandler
{
    private readonly ILogger<BazneshastegiServiceExceptionHandler> _logger;

    public BazneshastegiServiceExceptionHandler(ILogger<BazneshastegiServiceExceptionHandler> logger)
    {
        this._logger = logger;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) => base.SendAsync(request, cancellationToken);
}