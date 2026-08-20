namespace Bazneshastegi.Application.Services.Provider;
public readonly record struct ProviderInsertComplementary_PhysicalDisability_IllnessRequest(
    string MethodName,
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    bool? HasWelfareCertificate,
    string IllnessHistory);
public readonly record struct ProviderInsertComplementary_PhysicalDisability_IllnessResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    bool? HasWelfareCertificate,
    string IllnessHistory);