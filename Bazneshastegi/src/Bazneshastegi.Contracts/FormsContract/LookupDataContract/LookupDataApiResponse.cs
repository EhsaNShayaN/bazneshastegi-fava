namespace Bazneshastegi.Contracts.FormsContract.LookupDataContract;
public readonly record struct LookupDataApiResponse(
    string LookUpID,
    string LookUpType,
    string LookUpTypeName,
    string LookUpName,
    string? LookUpParentID,
    string? LookUpDescription,
    string? LookUpParentIDName,
    bool? IsDeleted);