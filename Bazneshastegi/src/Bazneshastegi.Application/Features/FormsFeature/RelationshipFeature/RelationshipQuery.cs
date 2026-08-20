using Bazneshastegi.Application.Services.Provider;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.RelationshipFeature;

public sealed record RelationshipQuery(string RelationshipId) : IPrimitiveResultQuery<ProviderRelationshipResponse[]>;

sealed class RelationshipQueryHandler : IPrimitiveResultQueryHandler<RelationshipQuery, ProviderRelationshipResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public RelationshipQueryHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderRelationshipResponse[]>> Handle(RelationshipQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetRelationship(request.RelationshipId, cancellationToken)
        .ConfigureAwait(false);
}