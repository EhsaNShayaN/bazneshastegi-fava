using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.RelatedPersonsFeature;

public sealed record RelatedPersonsQuery() : IPrimitiveResultQuery<ProviderRelatedPersonsResponse[]>
{
    public readonly static RelatedPersonsQuery Default = new();
}

sealed class RelatedPersonsQueryHandler : IPrimitiveResultQueryHandler<RelatedPersonsQuery, ProviderRelatedPersonsResponse[]>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IBazneshastegiService _bazneshastegiService;

    public RelatedPersonsQueryHandler(
            IUserContextAccessor userContextAccessor,
        IBazneshastegiService bazneshastegiService)
    {
        this._userContextAccessor = userContextAccessor;
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderRelatedPersonsResponse[]>> Handle(RelatedPersonsQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetRelatedListByParentPersonId(_userContextAccessor.CurrentPersonId, cancellationToken)
        .ConfigureAwait(false);
}