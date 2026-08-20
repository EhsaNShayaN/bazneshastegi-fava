namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_PhysicalDisability_IllnessFeature;
public sealed record InsertComplementary_PhysicalDisability_IllnessCommandResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    bool? HasWelfareCertificate,
    string IllnessHistory);