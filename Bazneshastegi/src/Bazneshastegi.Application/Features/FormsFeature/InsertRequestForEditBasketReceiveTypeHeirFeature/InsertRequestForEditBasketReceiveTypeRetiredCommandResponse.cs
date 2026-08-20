namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditBasketReceiveTypeHeirFeature;
public sealed record InsertRequestForEditBasketReceiveTypeHeirCommandResponse(
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
    string PersonCellPhone);