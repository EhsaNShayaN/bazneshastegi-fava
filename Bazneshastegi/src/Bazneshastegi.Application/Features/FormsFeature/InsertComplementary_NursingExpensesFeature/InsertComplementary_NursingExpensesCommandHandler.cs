using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_NursingExpensesFeature;
public sealed class InsertComplementary_NursingExpensesCommandHandler : IPrimitiveResultCommandHandler<
    InsertComplementary_NursingExpensesCommand,
    InsertComplementary_NursingExpensesCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertComplementary_NursingExpensesCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertComplementary_NursingExpensesCommandResponse>> Handle(
        InsertComplementary_NursingExpensesCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestComplementary_NursingExpenses(
            new ProviderInsertComplementary_NursingExpensesRequest(
                request.RequestComplementaryID,
                request.RelatedPersonID,
                request.RequestDescription,
                request.RequestID,
                request.RequestTypeID,
                request.IssueTypeLookupID),
            cancellationToken)
            .Map(s => new InsertComplementary_NursingExpensesCommandResponse(
                s.RequestComplementaryID,
                s.RelatedPersonID,
                s.RequestDescription,
                s.RequestID,
                s.IssueTypeLookupID))
            .ConfigureAwait(false);
    }
}
