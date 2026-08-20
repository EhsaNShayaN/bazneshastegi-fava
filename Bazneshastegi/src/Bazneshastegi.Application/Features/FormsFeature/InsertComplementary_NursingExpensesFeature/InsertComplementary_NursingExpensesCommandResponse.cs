namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_NursingExpensesFeature;
public sealed record InsertComplementary_NursingExpensesCommandResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string IssueTypeLookupID);