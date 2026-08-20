namespace Bazneshastegi.Contracts.FormsContract.InsertRequestForEditBasketReceiveTypeHeirContract;
public readonly record struct InsertRequestForEditBasketReceiveTypeHeirApiRequest(
    string RequestTypeID,
    string RequestID,
    string LoginedPersonID,
    string BasketReceiveTypeID,

    string ThisPersonID,
    string PersonAddress,
    string PersonPostalCode,
    int? PersonRegion,
    int? PersonArea,
    string PersonPhone,
    string PersonCellPhone);