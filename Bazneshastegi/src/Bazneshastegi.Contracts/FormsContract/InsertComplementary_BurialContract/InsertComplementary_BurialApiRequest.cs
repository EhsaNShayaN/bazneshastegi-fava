namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_BurialContract;
public readonly record struct InsertComplementary_BurialApiRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);