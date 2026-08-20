namespace Bazneshastegi.Contracts.FormsContract.GetRequestTypeGuideContract;
public readonly record struct GetRequestTypeGuideApiResponse(
    string RequestTypeID,
    string GuideText,
    string RequestTypeName);