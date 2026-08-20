namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderGetLookupRequest(
    string? LookupType,
    string? LookupName);

public readonly record struct ProviderGetLookupResponse(
    string LookUpID,
    string LookUpType,
    string LookUpTypeName,
    string LookUpName,
    string LookUpParentID,
    string LookUpDescription,
    string LookUpParentIDName,
    bool? IsDeleted);