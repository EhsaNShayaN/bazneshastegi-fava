using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_WorkDisability_BurialFeature;
public sealed class InsertComplementary_WorkDisability_BurialCommandHandler : IPrimitiveResultCommandHandler<InsertComplementary_WorkDisability_BurialCommand, InsertComplementary_WorkDisability_BurialCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertComplementary_WorkDisability_BurialCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertComplementary_WorkDisability_BurialCommandResponse>> Handle(InsertComplementary_WorkDisability_BurialCommand request, CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestComplementary_WorkDisability_Burial(
            new ProviderInsertComplementary_WorkDisability_BurialRequest(
                request.MethodName,
                request.RequestComplementaryID,
                request.RelatedPersonID,
                request.RequestDescription,
                request.RequestID,
                request.RequestTypeID),
            cancellationToken)
            .Map(s => new InsertComplementary_WorkDisability_BurialCommandResponse(
                s.RequestComplementaryID,
                s.RelatedPersonID,
                s.RequestDescription,
                s.RequestID))
            .ConfigureAwait(false);
    }
}
