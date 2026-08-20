using Bazneshastegi.Application.Features.FormsFeature.InsertRequestAttachmentFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertRequestAttachmentContract;
using Bazneshastegi.Presentation.Helpers;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertRequestAttachment;

sealed class InsertRequestAttachmentEndpoint : EndpointHandlerBase<
    InsertRequestAttachmentApiRequest,
    InsertRequestAttachmentCommand,
    InsertRequestAttachmentCommandResponse,
    InsertRequestAttachmentApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertRequestAttachmentEndpoint(
        IPresentationMapper<InsertRequestAttachmentApiRequest, InsertRequestAttachmentCommand> apiRequestMapper
        ) : base(
            Endpoints.Forms.InsertRequestAttachment,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertRequestAttachmentApiRequestMapper : IPresentationMapper<InsertRequestAttachmentApiRequest, InsertRequestAttachmentCommand>
{
    public async ValueTask<PrimitiveResult<InsertRequestAttachmentCommand>> Map(InsertRequestAttachmentApiRequest src, CancellationToken cancellationToken)
    {
        var (Base64, ContentType, HasInvalidFormat) = await FileService.ConvertToBase64Async(src.File);
        if (HasInvalidFormat) return await ValueTask.FromResult(PrimitiveResult.Failure<InsertRequestAttachmentCommand>("", "فرمت فایل نامعتبر است."));
        return await ValueTask.FromResult(
                PrimitiveResult.Success(
                    new InsertRequestAttachmentCommand(
                        src.RequestAttachmentID,
                        src.RequestID,
                        src.AttachementTypeID,
                        src.AttachementTypeName,
                        src.AttachementDesc,
                        Base64,
                        ContentType,
                        src.InsertUserID,
                        src.InsertTime,
                        src.UpdateUserID,
                        src.UpdateTime)));
    }
}
sealed class InsertRequestAttachmentApiResponseMapper : IPresentationMapper<
    InsertRequestAttachmentCommandResponse,
    InsertRequestAttachmentApiResponse>
{
    public ValueTask<PrimitiveResult<InsertRequestAttachmentApiResponse>> Map(
        InsertRequestAttachmentCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertRequestAttachmentApiResponse(
                        src.RequestAttachmentID,
                        src.RequestID,
                        src.AttachementTypeID,
                        src.AttachementTypeName,
                        src.AttachementDesc,
                        src.Attachment,
                        src.ContentType,
                        src.InsertUserID,
                        src.InsertTime,
                        src.UpdateUserID,
                        src.UpdateTime)));
}