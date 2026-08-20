using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestAttachmentFeature;

public sealed record class InsertRequestAttachmentCommand(
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
    DateTime? UpdateTime) : IPrimitiveResultCommand<InsertRequestAttachmentCommandResponse>,
    IValidatableRequest<InsertRequestAttachmentCommand>
{
    public ValueTask<PrimitiveResult<InsertRequestAttachmentCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}