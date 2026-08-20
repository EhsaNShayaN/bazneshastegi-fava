using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditBasketReceiveTypeRetiredFeature;

public sealed record class InsertRequestForEditBasketReceiveTypeRetiredCommand(
    string RequestID,
    string RequestTypeID,
    string LoginedPersonID,
    string BasketReceiveTypeID) : IPrimitiveResultCommand<InsertRequestForEditBasketReceiveTypeRetiredCommandResponse>,
    IValidatableRequest<InsertRequestForEditBasketReceiveTypeRetiredCommand>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditBasketReceiveTypeRetiredCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}