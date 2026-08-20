namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_MarriageLoanContract;
public readonly record struct InsertComplementary_MarriageLoanApiResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    decimal? ProfitOrDiscountPercent,
    decimal? FacilityInstalementAmount,
    decimal? FacilityInstalementCount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);