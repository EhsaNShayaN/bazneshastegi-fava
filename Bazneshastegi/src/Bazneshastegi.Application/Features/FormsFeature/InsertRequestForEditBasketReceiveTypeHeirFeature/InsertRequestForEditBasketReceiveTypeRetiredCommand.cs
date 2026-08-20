using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditBasketReceiveTypeHeirFeature;

public sealed record class InsertRequestForEditBasketReceiveTypeHeirCommand(
    string RequestID,
    string RequestTypeID,
    string LoginedPersonID,
    string BasketReceiveTypeID,

    string ThisPersonID,
    string PersonAddress,
    string PersonPostalCode,
    int? PersonRegion,
    int? PersonArea,
    string PersonPhone,
    string PersonCellPhone) : IPrimitiveResultCommand<InsertRequestForEditBasketReceiveTypeHeirCommandResponse>,
    IValidatableRequest<InsertRequestForEditBasketReceiveTypeHeirCommand>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditBasketReceiveTypeHeirCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}