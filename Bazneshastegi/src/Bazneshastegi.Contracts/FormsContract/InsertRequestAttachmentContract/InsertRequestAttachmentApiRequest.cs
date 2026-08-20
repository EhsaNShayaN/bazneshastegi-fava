using Bazneshastegi.Contracts.Helpers;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Bazneshastegi.Contracts.FormsContract.InsertRequestAttachmentContract;
public sealed class InsertRequestAttachmentApiRequest
{
    public string RequestAttachmentID { get; set; } = string.Empty;
    public string RequestID { get; set; } = string.Empty;
    public string AttachementTypeID { get; set; } = string.Empty;
    public string AttachementTypeName { get; set; } = string.Empty;
    public string AttachementDesc { get; set; } = string.Empty;
    public string InsertUserID { get; set; } = string.Empty;
    public DateTime? InsertTime { get; set; }
    public string UpdateUserID { get; set; } = string.Empty;
    public DateTime? UpdateTime { get; set; }
    public IFormFile File { get; private set; }

    public static async ValueTask<InsertRequestAttachmentApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<InsertRequestAttachmentApiRequest>(form);
    }
}