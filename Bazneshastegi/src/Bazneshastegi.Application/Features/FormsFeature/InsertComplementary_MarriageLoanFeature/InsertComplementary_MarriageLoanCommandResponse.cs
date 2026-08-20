namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_MarriageLoanFeature;
public sealed record InsertComplementary_MarriageLoanCommandResponse(
    string RequestComplementaryID,
    string RelatedPersonID,
    decimal? FacilityAmount,
    decimal? ProfitOrDiscountPercent,
    decimal? FacilityInstalementAmount,
    decimal? FacilityInstalementCount,
    string RequestDescription,
    string RequestID,
    string RequestTypeID);