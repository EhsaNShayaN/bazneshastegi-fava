using Bazneshastegi.Application.Services.Provider;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.RequestTypesFeature;

public sealed record RequestTypesQuery() : IPrimitiveResultQuery<ProviderRequestTypeResponse[]>;

sealed class RequestTypesQueryHandler : IPrimitiveResultQueryHandler<RequestTypesQuery, ProviderRequestTypeResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public RequestTypesQueryHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderRequestTypeResponse[]>> Handle(RequestTypesQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetRequestTypes(cancellationToken)
        .ConfigureAwait(false);
}