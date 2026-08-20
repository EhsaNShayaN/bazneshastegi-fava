namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_ImprestFeature;
public sealed record InsertComplementary_ImprestCommandResponse(
    string RequestComplementaryID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);