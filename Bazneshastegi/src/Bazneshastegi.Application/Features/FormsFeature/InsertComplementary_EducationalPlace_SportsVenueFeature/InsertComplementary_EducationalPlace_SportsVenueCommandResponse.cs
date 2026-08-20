namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_EducationalPlace_SportsVenueFeature;
public sealed record InsertComplementary_EducationalPlace_SportsVenueCommandResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    int? ProfitOrDiscountPercent);