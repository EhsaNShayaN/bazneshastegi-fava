using Bazneshastegi.Application.Services.Provider;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.GetLookupFeature;

public sealed record GetLookupQuery(
    string? LookupType,
    string? LookupName) : IPrimitiveResultQuery<ProviderGetLookupResponse[]>;

sealed class GetLookupQueryHandler : IPrimitiveResultQueryHandler<GetLookupQuery, ProviderGetLookupResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public GetLookupQueryHandler(
        IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderGetLookupResponse[]>> Handle(GetLookupQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetLookup(
            request.LookupType,
            request.LookupName,
            cancellationToken)
        .ConfigureAwait(false);
}