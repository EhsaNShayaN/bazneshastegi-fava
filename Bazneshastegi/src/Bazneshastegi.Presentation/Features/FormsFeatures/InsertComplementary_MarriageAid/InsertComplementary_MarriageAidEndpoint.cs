using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_MarriageAidFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_MarriageAidContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_MarriageAid;
sealed class InsertComplementary_MarriageAidEndpoint : EndpointHandlerBase<
    InsertComplementary_MarriageAidApiRequest,
    InsertComplementary_MarriageAidCommand,
    InsertComplementary_MarriageAidCommandResponse,
    InsertComplementary_MarriageAidApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_MarriageAidEndpoint(
        IPresentationMapper<InsertComplementary_MarriageAidApiRequest, InsertComplementary_MarriageAidCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_MarriageAidCommandResponse, InsertComplementary_MarriageAidApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_MarriageAid,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_MarriageAidApiRequestMapper : IPresentationMapper<InsertComplementary_MarriageAidApiRequest, InsertComplementary_MarriageAidCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_MarriageAidCommand>> Map(InsertComplementary_MarriageAidApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_MarriageAidCommand(
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.FacilityAmount,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID)));
}
sealed class InsertComplementary_MarriageAidApiResponseMapper : IPresentationMapper<
    InsertComplementary_MarriageAidCommandResponse,
    InsertComplementary_MarriageAidApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_MarriageAidApiResponse>> Map(
        InsertComplementary_MarriageAidCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_MarriageAidApiResponse(
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.FacilityAmount,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID)));
}