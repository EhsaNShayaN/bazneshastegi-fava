namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderInsertRequestForEditBasketReceiveTypeHeirRequest(
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
public readonly record struct ProviderInsertRequestForEditBasketReceiveTypeHeirResponse(
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
