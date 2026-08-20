namespace Bazneshastegi.Contracts.FormsContract.RequestTypeAttachmentContract;
public readonly record struct RequestTypeAttachmentApiResponse(
    string RequestTypeAttachmentID,
    string RequestTypeID,
    string LookupID,
    string LookupName,
    bool? Mandantory,
    string InsertUserID,
    DateTime? InsertTime,
    string? UpdateUserID,
    DateTime? UpdateTime);
