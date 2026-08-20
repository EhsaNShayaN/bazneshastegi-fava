namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_WorkDisability_BurialFeature;
public sealed record InsertComplementary_WorkDisability_BurialCommandResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID);