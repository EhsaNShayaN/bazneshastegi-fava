using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.NewPersonByParentIdFeature;

public sealed record NewPersonByParentIdQuery() : IPrimitiveResultQuery<ProviderNewPersonByParentIdResponse[]>
{
    public readonly static NewPersonByParentIdQuery Default = new();
}

sealed class NewPersonByParentIdQueryHandler : IPrimitiveResultQueryHandler<NewPersonByParentIdQuery, ProviderNewPersonByParentIdResponse[]>
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IBazneshastegiService _bazneshastegiService;

    public NewPersonByParentIdQueryHandler(
            IUserContextAccessor userContextAccessor,
        IBazneshastegiService bazneshastegiService)
    {
        this._userContextAccessor = userContextAccessor;
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderNewPersonByParentIdResponse[]>> Handle(NewPersonByParentIdQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.GetNewPersonByParentId(_userContextAccessor.CurrentPersonId, cancellationToken)
        .ConfigureAwait(false);
}