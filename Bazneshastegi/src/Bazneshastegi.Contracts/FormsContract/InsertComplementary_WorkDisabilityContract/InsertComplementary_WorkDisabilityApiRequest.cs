namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_WorkDisabilityContract;
public readonly record struct InsertComplementary_WorkDisabilityApiRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);