using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.RequestTypeAttachmentFeature;

public sealed record RequestTypeAttachmentQuery(string RequestTypeID) : IPrimitiveResultQuery<ProviderRequestTypeAttachmentResponse[]>
{
    //public readonly static RequestTypeAttachmentQuery Default = new(string.Empty);
}

sealed class RequestTypeAttachmentQueryHandler : IPrimitiveResultQueryHandler<RequestTypeAttachmentQuery, ProviderRequestTypeAttachmentResponse[]>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IBazneshastegiService _bazneshastegiService;

    public RequestTypeAttachmentQueryHandler(
            IUserContextAccessor userContextAccessor,
        IBazneshastegiService bazneshastegiService)
    {
        this._userContextAccessor = userContextAccessor;
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderRequestTypeAttachmentResponse[]>> Handle(RequestTypeAttachmentQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetRequestTypeAttachment(request.RequestTypeID, cancellationToken)
        .ConfigureAwait(false);
}