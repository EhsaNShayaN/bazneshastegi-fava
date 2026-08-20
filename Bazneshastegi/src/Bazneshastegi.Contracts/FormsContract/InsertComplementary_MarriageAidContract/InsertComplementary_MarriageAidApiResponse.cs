namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_MarriageAidContract;
public readonly record struct InsertComplementary_MarriageAidApiResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);