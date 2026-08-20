using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.PersonInfoFeature;

public sealed record PersonInfoQuery() : IPrimitiveResultQuery<ProviderPersonInfoResponse>
{
    public readonly static PersonInfoQuery Default = new();
}

sealed class PersonInfoQueryHandler : IPrimitiveResultQueryHandler<PersonInfoQuery, ProviderPersonInfoResponse>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IBazneshastegiService _bazneshastegiService;

    public PersonInfoQueryHandler(
        IUserContextAccessor userContextAccessor,
        IBazneshastegiService bazneshastegiService)
    {
        this._userContextAccessor = userContextAccessor;
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderPersonInfoResponse>> Handle(PersonInfoQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetPersonInfo(_userContextAccessor.CurrentPersonId, cancellationToken)
        .ConfigureAwait(false);
}