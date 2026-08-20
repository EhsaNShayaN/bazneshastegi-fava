using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_MarriageLoanFeature;

public sealed record class InsertComplementary_MarriageLoanCommand(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    decimal? ProfitOrDiscountPercent,
    decimal? FacilityInstalementAmount,
    decimal? FacilityInstalementCount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID) : IPrimitiveResultCommand<InsertComplementary_MarriageLoanCommandResponse>,
    IValidatableRequest<InsertComplementary_MarriageLoanCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_MarriageLoanCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}