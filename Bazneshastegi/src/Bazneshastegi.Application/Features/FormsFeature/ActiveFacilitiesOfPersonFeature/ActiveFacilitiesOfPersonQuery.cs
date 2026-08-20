using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.ActiveFacilitiesOfPersonFeature;

public sealed record ActiveFacilitiesOfPersonQuery(string RequestTypeID) : IPrimitiveResultQuery<ProviderActiveFacilitiesOfPersonResponse[]>;

sealed class ActiveFacilitiesOfPersonQueryHandler : IPrimitiveResultQueryHandler<ActiveFacilitiesOfPersonQuery, ProviderActiveFacilitiesOfPersonResponse[]>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IBazneshastegiService _bazneshastegiService;

    public ActiveFacilitiesOfPersonQueryHandler(
            IUserContextAccessor userContextAccessor,
        IBazneshastegiService bazneshastegiService)
    {
        this._userContextAccessor = userContextAccessor;
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderActiveFacilitiesOfPersonResponse[]>> Handle(ActiveFacilitiesOfPersonQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetActiveFacilitiesOfPerson(_userContextAccessor.CurrentPersonId, request.RequestTypeID, cancellationToken)
        .ConfigureAwait(false);
}