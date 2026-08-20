namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderLookupDataRequest(string LookUpType, string LookUpParentID);

public readonly record struct ProviderLookupDataResponse(
    string LookUpID,
    string LookUpType,
    string LookUpTypeName,
    string LookUpName,
    string? LookUpParentID,
    string? LookUpDescription,
    string? LookUpParentIDName,
    bool? IsDeleted);
