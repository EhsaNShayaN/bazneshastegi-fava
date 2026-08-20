using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_EducationalPlace_SportsVenueFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_EducationalPlaceContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_EducationalPlace;
sealed class InsertComplementary_EducationalPlaceEndpoint : EndpointHandlerBase<
    InsertComplementary_EducationalPlaceApiRequest,
    InsertComplementary_EducationalPlace_SportsVenueCommand,
    InsertComplementary_EducationalPlace_SportsVenueCommandResponse,
    InsertComplementary_EducationalPlaceApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_EducationalPlaceEndpoint(
        IPresentationMapper<InsertComplementary_EducationalPlaceApiRequest, InsertComplementary_EducationalPlace_SportsVenueCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_EducationalPlace_SportsVenueCommandResponse, InsertComplementary_EducationalPlaceApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_IntroduceToEducationalPlace,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_EducationalPlaceApiRequestMapper : IPresentationMapper<InsertComplementary_EducationalPlaceApiRequest, InsertComplementary_EducationalPlace_SportsVenueCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_EducationalPlace_SportsVenueCommand>> Map(InsertComplementary_EducationalPlaceApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_EducationalPlace_SportsVenueCommand(
                    "EducationalPlace",
                    src.RequestComplementaryID,
                    src.RelatedPersonID,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID,
                    src.FacilityGiverLookupID,
                    src.FacilityGiverDesc,
                    src.ProfitOrDiscountPercent)));
}
sealed class InsertComplementary_EducationalPlaceApiResponseMapper : IPresentationMapper<
    InsertComplementary_EducationalPlace_SportsVenueCommandResponse,
    InsertComplementary_EducationalPlaceApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_EducationalPlaceApiResponse>> Map(
        InsertComplementary_EducationalPlace_SportsVenueCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_EducationalPlaceApiResponse(
                        src.RequestComplementaryID,
                        src.RelatedPersonID,
                        src.RequestDescription,
                        src.RequestID,
                        src.FacilityGiverLookupID,
                        src.FacilityGiverDesc,
                        src.ProfitOrDiscountPercent)));
}