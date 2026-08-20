using Bazneshastegi.Application.Features.FormsFeature.GetRequestTypeGuideFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.GetRequestTypeGuideContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.GetRequestTypeGuide;

sealed class GetRequestTypeGuideEndpoint : EndpointHandlerBase<
    GetRequestTypeGuideApiRequest,
    GetRequestTypeGuideQuery,
    ProviderGetRequestTypeGuideResponse[],
    GetRequestTypeGuideApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public GetRequestTypeGuideEndpoint(
        IPresentationMapper<GetRequestTypeGuideApiRequest, GetRequestTypeGuideQuery> requestMapper,
        IPresentationMapper<ProviderGetRequestTypeGuideResponse[], GetRequestTypeGuideApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.GetRequestTypeGuide,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] GetRequestTypeGuideApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new GetRequestTypeGuideQuery(request.RequestTypeID))),
            cancellationToken);

}
sealed class GetGetRequestTypeGuideApiRequestMapper : IPresentationMapper<
    GetRequestTypeGuideApiRequest,
    GetRequestTypeGuideQuery>
{
    public ValueTask<PrimitiveResult<GetRequestTypeGuideQuery>> Map(
        GetRequestTypeGuideApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new GetRequestTypeGuideQuery(src.RequestTypeID)));
    }
}
sealed class GetGetRequestTypeGuideApiResponseMapper : IPresentationMapper<
    ProviderGetRequestTypeGuideResponse[],
    GetRequestTypeGuideApiResponse[]>
{
    public ValueTask<PrimitiveResult<GetRequestTypeGuideApiResponse[]>> Map(
        ProviderGetRequestTypeGuideResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new GetRequestTypeGuideApiResponse(
                    data.RequestTypeID,
                    data.GuideText,
                    data.RequestTypeName)).ToArray()));
    }
}