using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestAttachmentFeature;
public sealed class InsertRequestAttachmentCommandHandler : IPrimitiveResultCommandHandler<InsertRequestAttachmentCommand, InsertRequestAttachmentCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertRequestAttachmentCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertRequestAttachmentCommandResponse>> Handle(InsertRequestAttachmentCommand request, CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestAttachment(
            new ProviderInsertRequestAttachmentRequest(
                request.RequestAttachmentID,
                request.RequestID,
                request.AttachementTypeID,
                request.AttachementTypeName,
                request.AttachementDesc,
                request.Attachment,
                request.ContentType,
                request.InsertUserID,
                request.InsertTime,
                request.UpdateUserID,
                request.UpdateTime),
            cancellationToken)
            .Map(s => new InsertRequestAttachmentCommandResponse(
                s.RequestAttachmentID,
                s.RequestID,
                s.AttachementTypeID,
                s.AttachementTypeName,
                s.AttachementDesc,
                s.Attachment,
                s.ContentType,
                s.InsertUserID,
                s.InsertTime,
                s.UpdateUserID,
                s.UpdateTime))
            .ConfigureAwait(false);
    }
}
