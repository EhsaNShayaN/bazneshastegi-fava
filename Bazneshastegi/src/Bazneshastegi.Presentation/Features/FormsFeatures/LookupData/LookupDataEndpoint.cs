using Bazneshastegi.Application.Features.FormsFeature.LookupDataFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.LookupDataContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.LookupData;

sealed class LookupDataEndpoint : EndpointHandlerBase<
    LookupDataApiRequest,
    LookupDataQuery,
    ProviderLookupDataResponse[],
    LookupDataApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public LookupDataEndpoint(
        IPresentationMapper<LookupDataApiRequest, LookupDataQuery> requestMapper,
        IPresentationMapper<ProviderLookupDataResponse[], LookupDataApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.LookupData,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
        [AsParameters] LookupDataApiRequest request,
        ISender sender,
        CancellationToken cancellationToken) => this.CallMediatRHandler(
        sender,
        () => ValueTask.FromResult(PrimitiveResult.Success(new LookupDataQuery(request.LookupType, request.LookUpParentID))),
        cancellationToken);
}
sealed class GetLookupDataApiRequestMapper : IPresentationMapper<
    LookupDataApiRequest,
    LookupDataQuery>
{
    public ValueTask<PrimitiveResult<LookupDataQuery>> Map(
        LookupDataApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new LookupDataQuery(src.LookupType, src.LookUpParentID)));
    }
}
sealed class GetLookupDataApiResponseMapper : IPresentationMapper<
    ProviderLookupDataResponse[],
    LookupDataApiResponse[]>
{
    public ValueTask<PrimitiveResult<LookupDataApiResponse[]>> Map(
        ProviderLookupDataResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data =>
                    new LookupDataApiResponse(
                        data.LookUpID,
                        data.LookUpType,
                        data.LookUpTypeName,
                        data.LookUpName,
                        data.LookUpParentID,
                        data.LookUpDescription,
                        data.LookUpParentIDName,
                        data.IsDeleted)
                    ).ToArray()));
    }
}