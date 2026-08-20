using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_ImprestFeature;
public sealed class InsertComplementary_ImprestCommandHandler : IPrimitiveResultCommandHandler<
    InsertComplementary_ImprestCommand,
    InsertComplementary_ImprestCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertComplementary_ImprestCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertComplementary_ImprestCommandResponse>> Handle(
        InsertComplementary_ImprestCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestComplementary_Imprest(
            new ProviderInsertComplementary_ImprestRequest(
                request.RequestComplementaryID,
                request.FacilityAmount,
                request.RequestDescription,
                request.RequestID,
                request.RequestTypeID),
            cancellationToken)
            .Map(s => new InsertComplementary_ImprestCommandResponse(
                s.RequestComplementaryID,
                s.FacilityAmount,
                s.RequestDescription,
                s.RequestID,
                s.RequestTypeID))
            .ConfigureAwait(false);
    }
}
