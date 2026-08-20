namespace Bazneshastegi.Contracts.FormsContract.InsertComplementary_NursingExpensesContract;
public readonly record struct InsertComplementary_NursingExpensesApiRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    string IssueTypeLookupID);