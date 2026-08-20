using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_WorkDisability_BurialFeature;

public sealed record class InsertComplementary_WorkDisability_BurialCommand(
    string MethodName,
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID) : IPrimitiveResultCommand<InsertComplementary_WorkDisability_BurialCommandResponse>,
    IValidatableRequest<InsertComplementary_WorkDisability_BurialCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_WorkDisability_BurialCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}