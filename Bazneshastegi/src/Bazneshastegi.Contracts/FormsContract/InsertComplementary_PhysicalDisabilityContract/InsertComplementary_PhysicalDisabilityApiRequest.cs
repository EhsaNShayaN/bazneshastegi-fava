namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_PhysicalDisabilityContract;
public readonly record struct InsertComplementary_PhysicalDisabilityApiRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    bool HasWelfareCertificate,
    string IllnessHistory);