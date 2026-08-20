using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_WorkDisability_BurialFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_WorkDisabilityContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_WorkDisability;
sealed class InsertComplementary_WorkDisabilityEndpoint : EndpointHandlerBase<
    InsertComplementary_WorkDisabilityApiRequest,
    InsertComplementary_WorkDisability_BurialCommand,
    InsertComplementary_WorkDisability_BurialCommandResponse,
    InsertComplementary_WorkDisabilityApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_WorkDisabilityEndpoint(
        IPresentationMapper<InsertComplementary_WorkDisabilityApiRequest, InsertComplementary_WorkDisability_BurialCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_WorkDisability_BurialCommandResponse, InsertComplementary_WorkDisabilityApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_WorkDisability,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_WorkDisabilityApiRequestMapper : IPresentationMapper<InsertComplementary_WorkDisabilityApiRequest, InsertComplementary_WorkDisability_BurialCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_WorkDisability_BurialCommand>> Map(InsertComplementary_WorkDisabilityApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_WorkDisability_BurialCommand(
                    "WorkDisability",
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID)));
}
sealed class InsertComplementary_WorkDisabilityApiResponseMapper : IPresentationMapper<
    InsertComplementary_WorkDisability_BurialCommandResponse,
    InsertComplementary_WorkDisabilityApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_WorkDisabilityApiResponse>> Map(
        InsertComplementary_WorkDisability_BurialCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_WorkDisabilityApiResponse(
                        src.RequestComplementaryID,
                        src.RelatedPersonID,
                        src.RequestDescription,
                        src.RequestID)));
}