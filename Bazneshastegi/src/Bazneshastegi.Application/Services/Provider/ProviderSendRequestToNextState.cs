namespace Bazneshastegi.Application.Services.Provider;
public readonly record struct ProviderSendRequestToNextStateRequest(
    string RequestID,
    int? ConditionValue,
    string Description,
    string Role,
    string RequestTypeID);

public readonly record struct ProviderSendRequestToNextStateResponse();