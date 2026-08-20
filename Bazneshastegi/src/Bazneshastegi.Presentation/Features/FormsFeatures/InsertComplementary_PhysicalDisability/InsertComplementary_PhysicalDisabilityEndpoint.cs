using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_PhysicalDisability_IllnessFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_PhysicalDisabilityContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_PhysicalDisability;
sealed class InsertComplementary_PhysicalDisabilityEndpoint : EndpointHandlerBase<
    InsertComplementary_PhysicalDisabilityApiRequest,
    InsertComplementary_PhysicalDisability_IllnessCommand,
    InsertComplementary_PhysicalDisability_IllnessCommandResponse,
    InsertComplementary_PhysicalDisabilityApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_PhysicalDisabilityEndpoint(
        IPresentationMapper<InsertComplementary_PhysicalDisabilityApiRequest, InsertComplementary_PhysicalDisability_IllnessCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_PhysicalDisability_IllnessCommandResponse, InsertComplementary_PhysicalDisabilityApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_PhysicalDisability,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_PhysicalDisabilityApiRequestMapper : IPresentationMapper<InsertComplementary_PhysicalDisabilityApiRequest, InsertComplementary_PhysicalDisability_IllnessCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_PhysicalDisability_IllnessCommand>> Map(InsertComplementary_PhysicalDisabilityApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_PhysicalDisability_IllnessCommand(
                    "PhysicalDisability",
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID,
                    src.HasWelfareCertificate,
                    src.IllnessHistory)));
}
sealed class InsertComplementary_PhysicalDisabilityApiResponseMapper : IPresentationMapper<
    InsertComplementary_PhysicalDisability_IllnessCommandResponse,
    InsertComplementary_PhysicalDisabilityApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_PhysicalDisabilityApiResponse>> Map(
        InsertComplementary_PhysicalDisability_IllnessCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_PhysicalDisabilityApiResponse(
                        src.RequestComplementaryID,
                        src.RelatedPersonID,
                        src.RequestDescription,
                        src.RequestID,
                        src.HasWelfareCertificate,
                        src.IllnessHistory)));
}