namespace Bazneshastegi.Contracts.FormsContract.InsertRequestAttachmentContract;
public readonly record struct InsertRequestAttachmentApiResponse(
    string RequestAttachmentID,
    string RequestID,
    string AttachementTypeID,
    string AttachementTypeName,
    string AttachementDesc,
    string Attachment,
    string ContentType,
    string InsertUserID,
    DateTime? InsertTime,
    string UpdateUserID,
    DateTime? UpdateTime);