using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_MarriageAidFeature;

public sealed record class InsertComplementary_MarriageAidCommand(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID) : IPrimitiveResultCommand<InsertComplementary_MarriageAidCommandResponse>,
    IValidatableRequest<InsertComplementary_MarriageAidCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_MarriageAidCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}