namespace Bazneshastegi.Application.Services.Provider;
public readonly record struct ProviderInsertComplementary_WorkDisability_BurialRequest(
    string MethodName,
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);
public readonly record struct ProviderInsertComplementary_WorkDisability_BurialResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID);