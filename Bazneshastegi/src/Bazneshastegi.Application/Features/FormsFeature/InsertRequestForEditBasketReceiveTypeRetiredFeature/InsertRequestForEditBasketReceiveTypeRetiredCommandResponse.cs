namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditBasketReceiveTypeRetiredFeature;
public sealed record InsertRequestForEditBasketReceiveTypeRetiredCommandResponse(
    string RequestID,
    string RequestTypeID,
    string LoginedPersonID,
    string BasketReceiveTypeID);