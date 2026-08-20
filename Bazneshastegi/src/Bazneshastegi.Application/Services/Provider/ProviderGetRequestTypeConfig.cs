namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderGetRequestTypeConfigRequest(string RequestTypeID, string LookupID);

public readonly record struct ProviderGetRequestTypeConfigResponse(
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
