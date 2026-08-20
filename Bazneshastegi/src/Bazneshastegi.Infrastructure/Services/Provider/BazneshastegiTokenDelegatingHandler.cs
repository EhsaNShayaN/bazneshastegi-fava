namespace Bazneshastegi.Infrastructure.Services.Provider;
internal sealed class BazneshastegiTokenDelegatingHandler : DelegatingHandler
{
    public static string Token = string.Empty;
    const string AuthorizationHeaderName = "JWTToken";
    private readonly BazneshastegiTokenProvider _bazneshastegiTokenProvider;

    public BazneshastegiTokenDelegatingHandler(
        BazneshastegiTokenProvider bazneshastegiTokenProvider)
    {
        this._bazneshastegiTokenProvider = bazneshastegiTokenProvider;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri?.ToString()?.Contains("api/User/Login", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            return await base.SendAsync(request, cancellationToken);
        }
        request.Headers.Remove(AuthorizationHeaderName);
        var token = await this._bazneshastegiTokenProvider.TryGetValidToken(cancellationToken)
            .ConfigureAwait(false);
        if (token.IsSuccess)
        {
            request.Headers.TryAddWithoutValidation(
               AuthorizationHeaderName,
               $"Bearer {token.Value}");
        }
        return await base.SendAsync(request, cancellationToken);
    }
}
