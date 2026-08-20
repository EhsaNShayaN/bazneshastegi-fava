namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderInsertComplementary_MarriageLoanRequest(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    decimal? ProfitOrDiscountPercent,
    decimal? FacilityInstalementAmount,
    decimal? FacilityInstalementCount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);
public readonly record struct ProviderInsertComplementary_MarriageLoanResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    decimal? ProfitOrDiscountPercent,
    decimal? FacilityInstalementAmount,
    decimal? FacilityInstalementCount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);
