namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestAttachmentFeature;
public sealed record InsertRequestAttachmentCommandResponse(
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