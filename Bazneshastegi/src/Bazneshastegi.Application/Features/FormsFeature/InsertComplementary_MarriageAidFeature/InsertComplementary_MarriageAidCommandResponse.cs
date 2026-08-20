namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_MarriageAidFeature;
public sealed record InsertComplementary_MarriageAidCommandResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);