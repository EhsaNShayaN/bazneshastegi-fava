namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_PhysicalDisabilityContract;
public readonly record struct InsertComplementary_PhysicalDisabilityApiResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    bool? HasWelfareCertificate,
    string IllnessHistory);