using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_EducationalPlace_SportsVenueFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_SportsVenueContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_SportsVenue;
sealed class InsertComplementary_SportsVenueEndpoint : EndpointHandlerBase<
    InsertComplementary_SportsVenueApiRequest,
    InsertComplementary_EducationalPlace_SportsVenueCommand,
    InsertComplementary_EducationalPlace_SportsVenueCommandResponse,
    InsertComplementary_SportsVenueApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_SportsVenueEndpoint(
        IPresentationMapper<InsertComplementary_SportsVenueApiRequest, InsertComplementary_EducationalPlace_SportsVenueCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_EducationalPlace_SportsVenueCommandResponse, InsertComplementary_SportsVenueApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_IntroduceToSportsVenue,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_SportsVenueApiRequestMapper : IPresentationMapper<InsertComplementary_SportsVenueApiRequest, InsertComplementary_EducationalPlace_SportsVenueCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_EducationalPlace_SportsVenueCommand>> Map(InsertComplementary_SportsVenueApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_EducationalPlace_SportsVenueCommand(
                    "SportsVenue",
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID,
                    src.FacilityGiverLookupID,
                    src.FacilityGiverDesc,
                    src.ProfitOrDiscountPercent)));
}
sealed class InsertComplementary_SportsVenueApiResponseMapper : IPresentationMapper<
    InsertComplementary_EducationalPlace_SportsVenueCommandResponse,
    InsertComplementary_SportsVenueApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_SportsVenueApiResponse>> Map(
        InsertComplementary_EducationalPlace_SportsVenueCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_SportsVenueApiResponse(
                        src.RequestComplementaryID,
                        src.RelatedPersonID,
                        src.RequestDescription,
                        src.RequestID,
                        src.FacilityGiverLookupID,
                        src.FacilityGiverDesc,
                        src.ProfitOrDiscountPercent)));
}