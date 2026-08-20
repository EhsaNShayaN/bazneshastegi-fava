namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderInsertComplementary_EducationalPlace_SportsVenueRequest(
    string MethodName,
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    int? ProfitOrDiscountPercent);
public readonly record struct ProviderInsertComplementary_EducationalPlace_SportsVenueResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    int? ProfitOrDiscountPercent);
