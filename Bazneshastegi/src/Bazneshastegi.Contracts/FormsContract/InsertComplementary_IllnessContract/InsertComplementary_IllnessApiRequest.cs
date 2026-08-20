namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_IllnessContract;
public readonly record struct InsertComplementary_IllnessApiRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    bool HasWelfareCertificate,
    string IllnessHistory);