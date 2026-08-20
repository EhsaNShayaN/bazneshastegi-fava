using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_EducationalPlace_SportsVenueFeature;
public sealed class InsertComplementary_EducationalPlace_SportsVenueCommandHandler : IPrimitiveResultCommandHandler<
    InsertComplementary_EducationalPlace_SportsVenueCommand,
    InsertComplementary_EducationalPlace_SportsVenueCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertComplementary_EducationalPlace_SportsVenueCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertComplementary_EducationalPlace_SportsVenueCommandResponse>> Handle(
        InsertComplementary_EducationalPlace_SportsVenueCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestComplementary_EducationalPlace_SportsVenue(
            new ProviderInsertComplementary_EducationalPlace_SportsVenueRequest(
                request.MethodName,
                request.RequestComplementaryID,
                request.RelatedPersonID,
                request.RequestDescription,
                request.RequestID,
                request.RequestTypeID,
                request.FacilityGiverLookupID,
                request.FacilityGiverDesc,
                request.ProfitOrDiscountPercent),
            cancellationToken)
            .Map(s => new InsertComplementary_EducationalPlace_SportsVenueCommandResponse(
                s.RequestComplementaryID,
                s.RelatedPersonID,
                s.RequestDescription,
                s.RequestID,
                s.FacilityGiverLookupID,
                s.FacilityGiverDesc,
                s.ProfitOrDiscountPercent))
            .ConfigureAwait(false);
    }
}
