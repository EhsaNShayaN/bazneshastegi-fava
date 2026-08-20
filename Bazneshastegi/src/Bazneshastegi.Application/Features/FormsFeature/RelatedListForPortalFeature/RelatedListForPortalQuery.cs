using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.RelatedListForPortalFeature;

public sealed record RelatedListForPortalQuery(string RequestTypeID) : IPrimitiveResultQuery<ProviderRelatedListForPortalResponse[]>;

sealed class RelatedListForPortalQueryHandler : IPrimitiveResultQueryHandler<RelatedListForPortalQuery, ProviderRelatedListForPortalResponse[]>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IBazneshastegiService _bazneshastegiService;

    public RelatedListForPortalQueryHandler(
            IUserContextAccessor userContextAccessor,
        IBazneshastegiService bazneshastegiService)
    {
        this._userContextAccessor = userContextAccessor;
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderRelatedListForPortalResponse[]>> Handle(RelatedListForPortalQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetRelatedListForPortal(request.RequestTypeID, _userContextAccessor.CurrentPersonId, cancellationToken)
        .ConfigureAwait(false);
}