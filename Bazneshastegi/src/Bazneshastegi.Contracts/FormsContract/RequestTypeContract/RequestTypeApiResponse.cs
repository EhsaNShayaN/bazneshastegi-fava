namespace Bazneshastegi.Contracts.FormsContract.RequestTypeContract;
public readonly record struct RequestTypeApiResponse(
    Guid? RequestTypeID,
    string Name,
    int? StartState,
    string WorkFlowName,
    bool? UserView,
    string Role,
    bool? NatoinalCodeIsMandentory,
    string Page,
    int? RequestFrom);