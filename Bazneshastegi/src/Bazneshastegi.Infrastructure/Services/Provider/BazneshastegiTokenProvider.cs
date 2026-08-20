using Bazneshastegi.Application.Services.Provider;
using Microsoft.Extensions.Logging;

namespace Bazneshastegi.Infrastructure.Services.Provider;

internal sealed class BazneshastegiTokenProvider
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<BazneshastegiTokenProvider> _logger;
    private ProviderLoginResponse? _token;

    public BazneshastegiTokenProvider(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<BazneshastegiTokenProvider> logger)
    {
        this._serviceScopeFactory = serviceScopeFactory;
        this._logger = logger;
    }
    public async ValueTask<PrimitiveResult<string>> TryGetValidToken(CancellationToken cancellationToken)
    {
        if (
            this._token is null
            || string.IsNullOrWhiteSpace(this._token.Value.Token)
            || this._token.Value.Expiredate <= DateTime.Now.AddMinutes(-1))
        {
            using var scope = this._serviceScopeFactory.CreateScope();

            var bazneshastegiCredentialProvider = scope.ServiceProvider.GetRequiredService<IBazneshastegiCredentialProvider>();
            var bazneshastegiLoginService = scope.ServiceProvider.GetRequiredService<IBazneshastegiLoginService>();

            var userCredntial = await bazneshastegiCredentialProvider.GetUserCredentialInfo().ConfigureAwait(false);

            this._logger.LogInformation("Trying to get token from server ....");

            return await bazneshastegiLoginService.Login(userCredntial.Username, userCredntial.Pasword, cancellationToken)
               .OnFailure(_ => this._logger.LogError("Error", "Can not get token. error is {@error}", _.Errors))
               .OnSuccess(loginResult => this._token = loginResult.Value)
               .Map(x => this._token.Value.Token)
               .ConfigureAwait(false);
        }
        return this._token.Value.Token;
    }

}
