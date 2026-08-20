namespace Bazneshastegi.Contracts.FormsContract.CalculateMedicalTreatmentCostContract;

public readonly record struct CalculateMedicalTreatmentCostApiRequest(
    string? ServiceTypeLookupID,
    string? RelatedPersonID,
    string? DeliveryType);