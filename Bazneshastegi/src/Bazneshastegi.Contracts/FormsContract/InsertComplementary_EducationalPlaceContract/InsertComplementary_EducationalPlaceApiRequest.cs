namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_EducationalPlaceContract;
public readonly record struct InsertComplementary_EducationalPlaceApiRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    int ProfitOrDiscountPercent);