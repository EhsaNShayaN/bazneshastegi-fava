namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_ImprestContract;
public readonly record struct InsertComplementary_ImprestApiRequest(
    string RequestComplementaryID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);