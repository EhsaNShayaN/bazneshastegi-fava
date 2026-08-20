using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_MarriageAidFeature;
public sealed class InsertComplementary_MarriageAidCommandHandler : IPrimitiveResultCommandHandler<
    InsertComplementary_MarriageAidCommand,
    InsertComplementary_MarriageAidCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertComplementary_MarriageAidCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertComplementary_MarriageAidCommandResponse>> Handle(
        InsertComplementary_MarriageAidCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestComplementary_MarriageAid(
            new ProviderInsertComplementary_MarriageAidRequest(
                request.RequestComplementaryID,
                request.RelatedPersonID,
                request.FacilityAmount,
                request.RequestDescription,
                request.RequestID,
                request.RequestTypeID),
            cancellationToken)
            .Map(s => new InsertComplementary_MarriageAidCommandResponse(
                s.RequestComplementaryID,
                s.RelatedPersonID,
                s.FacilityAmount,
                s.RequestDescription,
                s.RequestID,
                s.RequestTypeID))
            .ConfigureAwait(false);
    }
}
