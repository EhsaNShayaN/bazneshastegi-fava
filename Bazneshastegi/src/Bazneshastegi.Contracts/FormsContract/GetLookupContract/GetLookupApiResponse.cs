namespace Bazneshastegi.Contracts.FormsContract.GetLookupContract;
public readonly record struct GetLookupApiResponse(
    string LookUpID,
    string LookUpType,
    string LookUpTypeName,
    string LookUpName,
    string LookUpParentID,
    string LookUpDescription,
    string LookUpParentIDName,
    bool? IsDeleted);