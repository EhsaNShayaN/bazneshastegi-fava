using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_MarriageLoanFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_MarriageLoanContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_MarriageLoan;
sealed class InsertComplementary_MarriageLoanEndpoint : EndpointHandlerBase<
    InsertComplementary_MarriageLoanApiRequest,
    InsertComplementary_MarriageLoanCommand,
    InsertComplementary_MarriageLoanCommandResponse,
    InsertComplementary_MarriageLoanApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_MarriageLoanEndpoint(
        IPresentationMapper<InsertComplementary_MarriageLoanApiRequest, InsertComplementary_MarriageLoanCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_MarriageLoanCommandResponse, InsertComplementary_MarriageLoanApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_MarriageLoan,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_MarriageLoanApiRequestMapper : IPresentationMapper<InsertComplementary_MarriageLoanApiRequest, InsertComplementary_MarriageLoanCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_MarriageLoanCommand>> Map(InsertComplementary_MarriageLoanApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_MarriageLoanCommand(
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.FacilityAmount,
                    src.ProfitOrDiscountPercent,
                    src.FacilityInstalementAmount,
                    src.FacilityInstalementCount,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID)));
}
sealed class InsertComplementary_MarriageLoanApiResponseMapper : IPresentationMapper<
    InsertComplementary_MarriageLoanCommandResponse,
    InsertComplementary_MarriageLoanApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_MarriageLoanApiResponse>> Map(
        InsertComplementary_MarriageLoanCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_MarriageLoanApiResponse(
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.FacilityAmount,
                    src.ProfitOrDiscountPercent,
                    src.FacilityInstalementAmount,
                    src.FacilityInstalementCount,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID)));
}