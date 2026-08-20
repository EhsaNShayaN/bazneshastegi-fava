using Bazneshastegi.Application.Services.Provider;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.LookupDataFeature;

public sealed record LookupDataQuery(string LookupType, string LookUpParentID) : IPrimitiveResultQuery<ProviderLookupDataResponse[]>;

sealed class LookupDataQueryHandler : IPrimitiveResultQueryHandler<LookupDataQuery, ProviderLookupDataResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public LookupDataQueryHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderLookupDataResponse[]>> Handle(LookupDataQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetLookupData(request.LookupType, request.LookUpParentID, cancellationToken)
        .ConfigureAwait(false);
}