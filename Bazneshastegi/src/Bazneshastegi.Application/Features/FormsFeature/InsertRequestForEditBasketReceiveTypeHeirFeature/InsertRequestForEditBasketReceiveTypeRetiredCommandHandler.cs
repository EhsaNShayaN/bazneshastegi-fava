using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditBasketReceiveTypeHeirFeature;
public sealed class InsertRequestForEditBasketReceiveTypeHeirCommandHandler : IPrimitiveResultCommandHandler<
    InsertRequestForEditBasketReceiveTypeHeirCommand,
    InsertRequestForEditBasketReceiveTypeHeirCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertRequestForEditBasketReceiveTypeHeirCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertRequestForEditBasketReceiveTypeHeirCommandResponse>> Handle(
        InsertRequestForEditBasketReceiveTypeHeirCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestForEditBasketReceiveTypeHeir(
            new ProviderInsertRequestForEditBasketReceiveTypeHeirRequest(
                request.RequestID,
                request.RequestTypeID,
                request.LoginedPersonID,
                request.BasketReceiveTypeID,

                request.ThisPersonID,
                request.PersonAddress,
                request.PersonPostalCode,
                request.PersonRegion,
                request.PersonArea,
                request.PersonPhone,
                request.PersonCellPhone),
            cancellationToken)
            .Map(s => new InsertRequestForEditBasketReceiveTypeHeirCommandResponse(
                s.RequestID,
                s.RequestTypeID,
                s.LoginedPersonID,
                s.BasketReceiveTypeID,

                s.ThisPersonID,
                s.PersonAddress,
                s.PersonPostalCode,
                s.PersonRegion,
                s.PersonArea,
                s.PersonPhone,
                s.PersonCellPhone))
            .ConfigureAwait(false);
    }
}
