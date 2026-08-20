namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_ImprestContract;
public readonly record struct InsertComplementary_ImprestApiResponse(
    string RequestComplementaryID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);