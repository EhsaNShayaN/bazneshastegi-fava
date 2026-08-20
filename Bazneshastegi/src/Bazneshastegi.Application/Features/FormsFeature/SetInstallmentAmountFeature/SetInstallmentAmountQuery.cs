using Bazneshastegi.Application.Services.Provider;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.SetInstalementAmountFeature;

public sealed record SetInstalementAmountQuery(
        string RequestTypeID,
        decimal DefaultAmount,
        decimal DefaultInstalementCount) : IPrimitiveResultQuery<ProviderSetInstalementAmountResponse[]>;

sealed class SetInstalementAmountQueryHandler : IPrimitiveResultQueryHandler<SetInstalementAmountQuery, ProviderSetInstalementAmountResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public SetInstalementAmountQueryHandler(
        IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }
    public async Task<PrimitiveResult<ProviderSetInstalementAmountResponse[]>> Handle(SetInstalementAmountQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.SetInstalementAmount(
            request.RequestTypeID,
            request.DefaultAmount,
            request.DefaultInstalementCount,
            cancellationToken)
        .ConfigureAwait(false);
}