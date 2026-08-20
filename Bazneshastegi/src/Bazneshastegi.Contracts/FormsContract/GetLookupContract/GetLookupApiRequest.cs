namespace Bazneshastegi.Contracts.FormsContract.GetLookupContract;

public readonly record struct GetLookupApiRequest(
    string? LookupType,
    string? LookupName);