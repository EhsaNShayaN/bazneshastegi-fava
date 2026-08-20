namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_NursingExpensesContract;
public readonly record struct InsertComplementary_NursingExpensesApiResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string IssueTypeLookupID);