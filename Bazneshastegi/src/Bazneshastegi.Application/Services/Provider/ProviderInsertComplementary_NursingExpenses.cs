namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderInsertComplementary_NursingExpensesRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    string IssueTypeLookupID);
public readonly record struct ProviderInsertComplementary_NursingExpensesResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string IssueTypeLookupID);
