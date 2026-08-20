namespace Bazneshastegi.Application.Services.Provider;
public readonly record struct ProviderInsertRequestAttachmentRequest(
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

public readonly record struct ProviderInsertRequestAttachmentResponse(string RequestAttachmentID,
    string RequestID,
    string AttachementTypeID,
    string AttachementTypeName,
    string AttachementDesc,
    string Attachment,
    string ContentType,
    string InsertUserID,
    DateTime? InsertTime,
    string UpdateUserID,
    DateTime? UpdateTime
);