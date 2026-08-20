using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_ImprestFeature;

public sealed record class InsertComplementary_ImprestCommand(
    string RequestComplementaryID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID) : IPrimitiveResultCommand<InsertComplementary_ImprestCommandResponse>,
    IValidatableRequest<InsertComplementary_ImprestCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_ImprestCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}