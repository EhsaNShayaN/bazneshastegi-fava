using Bazneshastegi.Application.Features.FormsFeature.GetLookupFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.GetLookupContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.GetLookup;

sealed class GetLookupEndpoint : EndpointHandlerBase<
    GetLookupApiRequest,
    GetLookupQuery,
    ProviderGetLookupResponse[],
    GetLookupApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public GetLookupEndpoint(
        IPresentationMapper<GetLookupApiRequest, GetLookupQuery> requestMapper,
        IPresentationMapper<ProviderGetLookupResponse[], GetLookupApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.GetLookup,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] GetLookupApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new GetLookupQuery(
                request.LookupType,
                request.LookupName))),
            cancellationToken);

}
sealed class GetGetLookupApiRequestMapper : IPresentationMapper<
    GetLookupApiRequest,
    GetLookupQuery>
{
    public ValueTask<PrimitiveResult<GetLookupQuery>> Map(
        GetLookupApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new GetLookupQuery(
            src.LookupType,
            src.LookupType)));
    }
}
sealed class GetGetLookupApiResponseMapper : IPresentationMapper<
    ProviderGetLookupResponse[],
    GetLookupApiResponse[]>
{
    public ValueTask<PrimitiveResult<GetLookupApiResponse[]>> Map(
        ProviderGetLookupResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new GetLookupApiResponse(
                    data.LookUpID,
                    data.LookUpType,
                    data.LookUpTypeName,
                    data.LookUpName,
                    data.LookUpParentID,
                    data.LookUpDescription,
                    data.LookUpParentIDName,
                    data.IsDeleted)).ToArray()));
    }
}