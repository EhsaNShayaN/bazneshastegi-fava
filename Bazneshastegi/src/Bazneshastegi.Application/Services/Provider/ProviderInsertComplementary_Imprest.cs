namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderInsertComplementary_ImprestRequest(
    string RequestComplementaryID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);
public readonly record struct ProviderInsertComplementary_ImprestResponse(
    string RequestComplementaryID,
    decimal? FacilityAmount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);