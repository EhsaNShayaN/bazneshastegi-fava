namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderInsertRequestForEditBasketReceiveTypeRetiredRequest(
    string RequestID,
    string RequestTypeID,
    string LoginedPersonID,
    string BasketReceiveTypeID);
public readonly record struct ProviderInsertRequestForEditBasketReceiveTypeRetiredResponse(
    string RequestID,
    string RequestTypeID,
    string LoginedPersonID,
    string BasketReceiveTypeID);
