namespace Bazneshastegi.Contracts.FormsContract.InsertRequestForEditBasketReceiveTypeHeirContract;
public readonly record struct InsertRequestForEditBasketReceiveTypeHeirApiResponse(
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