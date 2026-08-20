namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_MarriageAidContract;
public readonly record struct InsertComplementary_MarriageAidApiRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);