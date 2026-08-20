using Bazneshastegi.Application.Features.FormsFeature.GetRequestTypeConfigFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.GetRequestTypeConfigContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.GetRequestTypeConfig;

sealed class GetRequestTypeConfigEndpoint : EndpointHandlerBase<
    GetRequestTypeConfigApiRequest,
    GetRequestTypeConfigQuery,
    ProviderGetRequestTypeConfigResponse[],
    GetRequestTypeConfigApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public GetRequestTypeConfigEndpoint(
        IPresentationMapper<GetRequestTypeConfigApiRequest, GetRequestTypeConfigQuery> requestMapper,
        IPresentationMapper<ProviderGetRequestTypeConfigResponse[], GetRequestTypeConfigApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.GetRequestTypeConfig,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] GetRequestTypeConfigApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new GetRequestTypeConfigQuery(
                request.RequestTypeID,
                request.LookupID,
                request.FacilityReceiverRelationshipID,
                request.PensionaryStatusCategory,
                request.GenderLookupID,
                request.FacilityReceiverPersonID))),
            cancellationToken);

}
sealed class GetGetRequestTypeConfigApiRequestMapper : IPresentationMapper<
    GetRequestTypeConfigApiRequest,
    GetRequestTypeConfigQuery>
{
    public ValueTask<PrimitiveResult<GetRequestTypeConfigQuery>> Map(
        GetRequestTypeConfigApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new GetRequestTypeConfigQuery(
            src.RequestTypeID,
            src.LookupID,
            src.FacilityReceiverRelationshipID,
            src.PensionaryStatusCategory,
            src.GenderLookupID,
            src.FacilityReceiverPersonID)));
    }
}
sealed class GetGetRequestTypeConfigApiResponseMapper : IPresentationMapper<
    ProviderGetRequestTypeConfigResponse[],
    GetRequestTypeConfigApiResponse[]>
{
    public ValueTask<PrimitiveResult<GetRequestTypeConfigApiResponse[]>> Map(
        ProviderGetRequestTypeConfigResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new GetRequestTypeConfigApiResponse(
                    data.RequestTypeConfigID,
                    data.RequestTypeID,
                    data.LookupID,
                    data.DefaultAmount,
                    data.DeliveryCost,
                    data.DefaultInstalementCount,
                    data.DefaultDiscountPercent,
                    data.ProfitOrDiscountPercent,
                    data.GuarantorCost,
                    data.ValidationStartDate,
                    data.FacilityReceiverRelationshipID,
                    data.PensionaryStatusCategory,
                    data.GenderLookupID,
                    data.IsActive,
                    data.RequestTypeName,
                    data.LookupName,
                    data.FacilityReceiverRelationshipName,
                    data.PensionaryStatusCategoryName,
                    data.GenderName,
                    data.DefaultInstalementAmount))
                .ToArray()));
    }
}