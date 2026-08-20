using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditBasketReceiveTypeRetiredFeature;
public sealed class InsertRequestForEditBasketReceiveTypeRetiredCommandHandler : IPrimitiveResultCommandHandler<
    InsertRequestForEditBasketReceiveTypeRetiredCommand,
    InsertRequestForEditBasketReceiveTypeRetiredCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertRequestForEditBasketReceiveTypeRetiredCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertRequestForEditBasketReceiveTypeRetiredCommandResponse>> Handle(
        InsertRequestForEditBasketReceiveTypeRetiredCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestForEditBasketReceiveTypeRetired(
            new ProviderInsertRequestForEditBasketReceiveTypeRetiredRequest(
                request.RequestID,
                request.RequestTypeID,
                request.LoginedPersonID,
                request.BasketReceiveTypeID),
            cancellationToken)
            .Map(s => new InsertRequestForEditBasketReceiveTypeRetiredCommandResponse(
                s.RequestID,
                s.RequestTypeID,
                s.LoginedPersonID,
                s.BasketReceiveTypeID))
            .ConfigureAwait(false);
    }
}
