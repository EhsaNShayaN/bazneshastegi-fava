using Bazneshastegi.Application.Services.Provider;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.GetRequestTypeGuideFeature;

public sealed record GetRequestTypeGuideQuery(string RequestTypeID) : IPrimitiveResultQuery<ProviderGetRequestTypeGuideResponse[]>;

sealed class GetRequestTypeGuideQueryHandler : IPrimitiveResultQueryHandler<GetRequestTypeGuideQuery, ProviderGetRequestTypeGuideResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public GetRequestTypeGuideQueryHandler(
        IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderGetRequestTypeGuideResponse[]>> Handle(GetRequestTypeGuideQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetRequestTypeGuide(
            request.RequestTypeID,
            cancellationToken)
        .ConfigureAwait(false);
}