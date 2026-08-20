namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_SportsVenueContract;
public readonly record struct InsertComplementary_SportsVenueApiResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    int? ProfitOrDiscountPercent);