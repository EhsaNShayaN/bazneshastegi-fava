namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_SportsVenueContract;
public readonly record struct InsertComplementary_SportsVenueApiRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    int ProfitOrDiscountPercent);