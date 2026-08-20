namespace Bazneshastegi.Contracts.FormsContract.GetRequestTypeConfigContract;

public readonly record struct GetRequestTypeConfigApiRequest(
    string RequestTypeID,
    string? LookupID,
    string? FacilityReceiverRelationshipID,
    string? PensionaryStatusCategory,
    string? GenderLookupID,
    string? FacilityReceiverPersonID);