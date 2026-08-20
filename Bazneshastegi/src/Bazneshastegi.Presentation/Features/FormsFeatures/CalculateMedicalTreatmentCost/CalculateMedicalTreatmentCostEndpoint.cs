using Bazneshastegi.Application.Features.FormsFeature.CalculateMedicalTreatmentCostFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.CalculateMedicalTreatmentCostContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.CalculateMedicalTreatmentCost;

sealed class CalculateMedicalTreatmentCostEndpoint : EndpointHandlerBase<
    CalculateMedicalTreatmentCostApiRequest,
    CalculateMedicalTreatmentCostQuery,
    ProviderCalculateMedicalTreatmentCostResponse[],
    CalculateMedicalTreatmentCostApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public CalculateMedicalTreatmentCostEndpoint(
        IPresentationMapper<CalculateMedicalTreatmentCostApiRequest, CalculateMedicalTreatmentCostQuery> requestMapper,
        IPresentationMapper<ProviderCalculateMedicalTreatmentCostResponse[], CalculateMedicalTreatmentCostApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.CalculateMedicalTreatmentCost,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] CalculateMedicalTreatmentCostApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new CalculateMedicalTreatmentCostQuery(
                request.ServiceTypeLookupID,
                request.RelatedPersonID,
                request.DeliveryType))),
            cancellationToken);

}
sealed class GetCalculateMedicalTreatmentCostApiRequestMapper : IPresentationMapper<
    CalculateMedicalTreatmentCostApiRequest,
    CalculateMedicalTreatmentCostQuery>
{
    public ValueTask<PrimitiveResult<CalculateMedicalTreatmentCostQuery>> Map(
        CalculateMedicalTreatmentCostApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new CalculateMedicalTreatmentCostQuery(
            src.ServiceTypeLookupID,
            src.RelatedPersonID,
            src.DeliveryType)));
    }
}
sealed class GetCalculateMedicalTreatmentCostApiResponseMapper : IPresentationMapper<
    ProviderCalculateMedicalTreatmentCostResponse[],
    CalculateMedicalTreatmentCostApiResponse[]>
{
    public ValueTask<PrimitiveResult<CalculateMedicalTreatmentCostApiResponse[]>> Map(
        ProviderCalculateMedicalTreatmentCostResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new CalculateMedicalTreatmentCostApiResponse(
                    data.ServiceCost,
                    data.DeliveryCost,
                    data.ServiceDiscount,
                    data.ServiceCountOfInstalement,
                    data.MessageForUser))
                .ToArray()));
    }
}