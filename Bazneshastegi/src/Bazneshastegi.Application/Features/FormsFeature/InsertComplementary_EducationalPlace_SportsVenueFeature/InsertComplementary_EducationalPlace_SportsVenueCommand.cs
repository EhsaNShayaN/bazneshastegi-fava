using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_EducationalPlace_SportsVenueFeature;

public sealed record class InsertComplementary_EducationalPlace_SportsVenueCommand(
    string MethodName,
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    int ProfitOrDiscountPercent) : IPrimitiveResultCommand<InsertComplementary_EducationalPlace_SportsVenueCommandResponse>,
    IValidatableRequest<InsertComplementary_EducationalPlace_SportsVenueCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_EducationalPlace_SportsVenueCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}