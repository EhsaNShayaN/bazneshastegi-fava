namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderRequestTypeRequest(int? RequestFrom);

public readonly record struct ProviderRequestTypeResponse(
    Guid? RequestTypeID,
    string Name,
    int? StartState,
    string WorkFlowName,
    bool? UserView,
    string Role,
    bool? NatoinalCodeIsMandentory,
    string Page,
    int? RequestFrom);
