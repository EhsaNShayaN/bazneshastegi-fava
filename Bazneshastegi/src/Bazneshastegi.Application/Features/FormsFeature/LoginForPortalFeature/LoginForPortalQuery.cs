using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserAuthenticationServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.LoginForPortalFeature;

public sealed record LoginForPortalQuery(string NationalCode, string CellPhone) : IPrimitiveResultQuery<ProviderLoginForPortalResponse>;

sealed class LoginForPortalQueryHandler : IPrimitiveResultQueryHandler<LoginForPortalQuery, ProviderLoginForPortalResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;
    private readonly IUserAuthenticationTokenService _userAuthenticationTokenService;

    public LoginForPortalQueryHandler(
        IBazneshastegiService bazneshastegiService,
        IUserAuthenticationTokenService userAuthenticationTokenService)
    {
        this._bazneshastegiService = bazneshastegiService;
        this._userAuthenticationTokenService = userAuthenticationTokenService;
    }
    public async Task<PrimitiveResult<ProviderLoginForPortalResponse>> Handle(LoginForPortalQuery request, CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.LoginForPortal(
            request.NationalCode,
            request.CellPhone,
            cancellationToken)
            .Map(proxyData => this._userAuthenticationTokenService.GenerateToken(proxyData.PersonID))
            .Map(token => new ProviderLoginForPortalResponse(token))
         .ConfigureAwait(false);
    }
}