using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_WorkDisability_BurialFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_BurialContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_Burial;
sealed class InsertComplementary_BurialEndpoint : EndpointHandlerBase<
    InsertComplementary_BurialApiRequest,
    InsertComplementary_WorkDisability_BurialCommand,
    InsertComplementary_WorkDisability_BurialCommandResponse,
    InsertComplementary_BurialApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_BurialEndpoint(
        IPresentationMapper<InsertComplementary_BurialApiRequest, InsertComplementary_WorkDisability_BurialCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_WorkDisability_BurialCommandResponse, InsertComplementary_BurialApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_Burial,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_BurialApiRequestMapper : IPresentationMapper<InsertComplementary_BurialApiRequest, InsertComplementary_WorkDisability_BurialCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_WorkDisability_BurialCommand>> Map(InsertComplementary_BurialApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_WorkDisability_BurialCommand(
                    "Burial",
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID)));
}
sealed class InsertComplementary_BurialApiResponseMapper : IPresentationMapper<
    InsertComplementary_WorkDisability_BurialCommandResponse,
    InsertComplementary_BurialApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_BurialApiResponse>> Map(
        InsertComplementary_WorkDisability_BurialCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_BurialApiResponse(
                        src.RequestComplementaryID,
                        src.RelatedPersonID,
                        src.RequestDescription,
                        src.RequestID)));
}