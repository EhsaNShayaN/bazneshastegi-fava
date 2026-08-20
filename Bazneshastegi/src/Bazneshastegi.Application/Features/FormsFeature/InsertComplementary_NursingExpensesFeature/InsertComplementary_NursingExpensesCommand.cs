using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_NursingExpensesFeature;

public sealed record class InsertComplementary_NursingExpensesCommand(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    string IssueTypeLookupID) : IPrimitiveResultCommand<InsertComplementary_NursingExpensesCommandResponse>,
    IValidatableRequest<InsertComplementary_NursingExpensesCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_NursingExpensesCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}