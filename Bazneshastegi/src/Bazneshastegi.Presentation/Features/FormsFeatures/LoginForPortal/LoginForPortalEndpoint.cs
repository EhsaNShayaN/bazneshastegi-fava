using Bazneshastegi.Application.Features.FormsFeature.LoginForPortalFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.LoginForPortalContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.LoginForPortal;

sealed class LoginForPortalEndpoint : EndpointHandlerBase<
    LoginForPortalApiRequest,
    LoginForPortalQuery,
    ProviderLoginForPortalResponse,
    LoginForPortalApiResponse>
{
    protected override bool NeedTaxPayerFile => true;
    protected override bool NeedCaptcha => true;

    public LoginForPortalEndpoint(
        IPresentationMapper<LoginForPortalApiRequest, LoginForPortalQuery> requestMapper,
        IPresentationMapper<ProviderLoginForPortalResponse, LoginForPortalApiResponse> responseMapper)
        : base(
            Endpoints.Forms.LoginForPortal,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] LoginForPortalApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new LoginForPortalQuery(request.NationalCode, request.CellPhone))),
            cancellationToken);

}
sealed class GetLoginForPortalApiRequestMapper : IPresentationMapper<
    LoginForPortalApiRequest,
    LoginForPortalQuery>
{
    public ValueTask<PrimitiveResult<LoginForPortalQuery>> Map(
        LoginForPortalApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new LoginForPortalQuery(src.NationalCode, src.CellPhone)));
    }
}
sealed class GetLoginForPortalApiResponseMapper : IPresentationMapper<
    ProviderLoginForPortalResponse,
    LoginForPortalApiResponse>
{
    public ValueTask<PrimitiveResult<LoginForPortalApiResponse>> Map(
        ProviderLoginForPortalResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new LoginForPortalApiResponse(
                        src.Token)));
    }
}