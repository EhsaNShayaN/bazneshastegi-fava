namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_EducationalPlaceContract;
public readonly record struct InsertComplementary_EducationalPlaceApiResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    int? ProfitOrDiscountPercent);