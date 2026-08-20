namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderCalculateMedicalTreatmentCostRequest(
    string MainPersonID,
    string ServiceTypeLookupID,
    string RelatedPersonID,
    string DeliveryType);
public readonly record struct ProviderCalculateMedicalTreatmentCostResponse(
    decimal? ServiceCost,
    decimal? DeliveryCost,
    decimal? ServiceDiscount,
    decimal? ServiceCountOfInstalement,
    string? MessageForUser);
