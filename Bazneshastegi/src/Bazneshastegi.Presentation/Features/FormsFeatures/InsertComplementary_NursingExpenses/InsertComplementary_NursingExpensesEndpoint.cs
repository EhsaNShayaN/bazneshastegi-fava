using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_NursingExpensesFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_NursingExpensesContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_NursingExpenses;
sealed class InsertComplementary_NursingExpensesEndpoint : EndpointHandlerBase<
    InsertComplementary_NursingExpensesApiRequest,
    InsertComplementary_NursingExpensesCommand,
    InsertComplementary_NursingExpensesCommandResponse,
    InsertComplementary_NursingExpensesApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_NursingExpensesEndpoint(
        IPresentationMapper<InsertComplementary_NursingExpensesApiRequest, InsertComplementary_NursingExpensesCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_NursingExpensesCommandResponse, InsertComplementary_NursingExpensesApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_NursingExpenses,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_NursingExpensesApiRequestMapper : IPresentationMapper<InsertComplementary_NursingExpensesApiRequest, InsertComplementary_NursingExpensesCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_NursingExpensesCommand>> Map(InsertComplementary_NursingExpensesApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_NursingExpensesCommand(
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID,
                    src.IssueTypeLookupID)));
}
sealed class InsertComplementary_NursingExpensesApiResponseMapper : IPresentationMapper<
    InsertComplementary_NursingExpensesCommandResponse,
    InsertComplementary_NursingExpensesApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_NursingExpensesApiResponse>> Map(
        InsertComplementary_NursingExpensesCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_NursingExpensesApiResponse(
                        src.RequestComplementaryID,
                        src.RelatedPersonID,
                        src.RequestDescription,
                        src.RequestID,
                        src.IssueTypeLookupID)));
}