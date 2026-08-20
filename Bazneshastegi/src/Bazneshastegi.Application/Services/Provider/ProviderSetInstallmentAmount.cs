namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderSetInstalementAmountRequest(string RequestTypeID, decimal DefaultAmount, decimal DefaultInstalementCount);

public readonly record struct ProviderSetInstalementAmountResponse(
    string RequestTypeConfigID,
    Guid? RequestTypeID,
    string LookupID,
    decimal? DefaultAmount,
    decimal? DeliveryCost,
    int? DefaultInstalementCount,
    decimal? DefaultDiscountPercent,
    decimal? ProfitOrDiscountPercent,
    decimal? GuarantorCost,
    DateTime? ValidationStartDate,
    string FacilityReceiverRelationshipID,
    string PensionaryStatusCategory,
    string GenderLookupID,
    bool? IsActive,
    string RequestTypeName,
    string LookupName,
    string? FacilityReceiverRelationshipName,
    string? PensionaryStatusCategoryName,
    string? GenderName,
    decimal? DefaultInstalementAmount);
