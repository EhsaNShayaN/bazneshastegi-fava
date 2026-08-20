namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderInsertComplementary_MarriageAidRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);
public readonly record struct ProviderInsertComplementary_MarriageAidResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);