using Bazneshastegi.Application.Features.FormsFeature.SetInstalementAmountFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.SetInstalementAmountContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.SetInstalementAmount;

sealed class SetInstalementAmountEndpoint : EndpointHandlerBase<
    SetInstalementAmountApiRequest,
    SetInstalementAmountQuery,
    ProviderSetInstalementAmountResponse[],
    SetInstalementAmountApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public SetInstalementAmountEndpoint(
        IPresentationMapper<SetInstalementAmountApiRequest, SetInstalementAmountQuery> requestMapper,
        IPresentationMapper<ProviderSetInstalementAmountResponse[], SetInstalementAmountApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.SetInstalementAmount,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] SetInstalementAmountApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new SetInstalementAmountQuery(
                request.RequestTypeID,
                request.DefaultAmount,
                request.DefaultInstalementCount))),
            cancellationToken);

}
sealed class GetSetInstalementAmountApiRequestMapper : IPresentationMapper<
    SetInstalementAmountApiRequest,
    SetInstalementAmountQuery>
{
    public ValueTask<PrimitiveResult<SetInstalementAmountQuery>> Map(
        SetInstalementAmountApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new SetInstalementAmountQuery(
            src.RequestTypeID,
            src.DefaultAmount,
            src.DefaultInstalementCount)));
    }
}
sealed class GetSetInstalementAmountApiResponseMapper : IPresentationMapper<
    ProviderSetInstalementAmountResponse[],
    SetInstalementAmountApiResponse[]>
{
    public ValueTask<PrimitiveResult<SetInstalementAmountApiResponse[]>> Map(
        ProviderSetInstalementAmountResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new SetInstalementAmountApiResponse(
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