namespace Bazneshastegi.Contracts.FormsContract.CalculateMedicalTreatmentCostContract;
public readonly record struct CalculateMedicalTreatmentCostApiResponse(
    decimal? ServiceCost,
    decimal? DeliveryCost,
    decimal? ServiceDiscount,
    decimal? ServiceCountOfInstalement,
    string? MessageForUser);