namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderRequestTypeAttachmentRequest(string RequestTypeID);

public readonly record struct ProviderRequestTypeAttachmentResponse(
    string RequestTypeAttachmentID,
    string RequestTypeID,
    string LookupID,
    string LookupName,
    bool? Mandantory,
    string InsertUserID,
    DateTime? InsertTime,
    string? UpdateUserID,
    DateTime? UpdateTime);
