using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.GetTempPersonFeature;

public sealed record GetTempPersonQuery(
    string? RequestID) : IPrimitiveResultQuery<ProviderGetTempPersonResponse[]>;

sealed class GetTempPersonQueryHandler : IPrimitiveResultQueryHandler<GetTempPersonQuery, ProviderGetTempPersonResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;
    private readonly IUserContextAccessor _userContextAccessor;

    public GetTempPersonQueryHandler(
        IBazneshastegiService bazneshastegiService,
        IUserContextAccessor userContextAccessor)
    {
        this._bazneshastegiService = bazneshastegiService;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<ProviderGetTempPersonResponse[]>> Handle(GetTempPersonQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetTempPerson(
            _userContextAccessor.CurrentPersonId,
            request.RequestID,
            cancellationToken)
        .ConfigureAwait(false);
}