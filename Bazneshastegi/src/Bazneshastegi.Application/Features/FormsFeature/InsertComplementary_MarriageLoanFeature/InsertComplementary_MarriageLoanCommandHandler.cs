using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_MarriageLoanFeature;
public sealed class InsertComplementary_MarriageLoanCommandHandler : IPrimitiveResultCommandHandler<
    InsertComplementary_MarriageLoanCommand,
    InsertComplementary_MarriageLoanCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertComplementary_MarriageLoanCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertComplementary_MarriageLoanCommandResponse>> Handle(
        InsertComplementary_MarriageLoanCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestComplementary_MarriageLoan(
            new ProviderInsertComplementary_MarriageLoanRequest(
                request.RequestComplementaryID,
                request.RelatedPersonID,
                request.FacilityAmount,
                request.ProfitOrDiscountPercent,
                request.FacilityInstalementAmount,
                request.FacilityInstalementCount,
                request.RequestDescription,
                request.RequestID,
                request.RequestTypeID),
            cancellationToken)
            .Map(s => new InsertComplementary_MarriageLoanCommandResponse(
                s.RequestComplementaryID,
                s.RelatedPersonID,
                s.FacilityAmount,
                s.ProfitOrDiscountPercent,
                s.FacilityInstalementAmount,
                s.FacilityInstalementCount,
                s.RequestDescription,
                s.RequestID,
                s.RequestTypeID))
            .ConfigureAwait(false);
    }
}
