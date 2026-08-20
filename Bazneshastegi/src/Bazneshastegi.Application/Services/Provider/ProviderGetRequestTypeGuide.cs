namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderGetRequestTypeGuideRequest(string RequestTypeID);

public readonly record struct ProviderGetRequestTypeGuideResponse(
    string RequestTypeID,
    string GuideText,
    string RequestTypeName);
