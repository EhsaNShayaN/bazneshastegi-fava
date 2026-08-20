using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_PhysicalDisability_IllnessFeature;
public sealed class InsertComplementary_PhysicalDisability_IllnessCommandHandler : IPrimitiveResultCommandHandler<InsertComplementary_PhysicalDisability_IllnessCommand, InsertComplementary_PhysicalDisability_IllnessCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertComplementary_PhysicalDisability_IllnessCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertComplementary_PhysicalDisability_IllnessCommandResponse>> Handle(InsertComplementary_PhysicalDisability_IllnessCommand request, CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestComplementary_PhysicalDisability_Illness(
            new ProviderInsertComplementary_PhysicalDisability_IllnessRequest(
                request.MethodName,
                request.RequestComplementaryID,
                request.RelatedPersonID,
                request.RequestDescription,
                request.RequestID,
                request.RequestTypeID,
                request.HasWelfareCertificate,
                request.IllnessHistory),
            cancellationToken)
            .Map(s => new InsertComplementary_PhysicalDisability_IllnessCommandResponse(
                s.RequestComplementaryID,
                s.RelatedPersonID,
                s.RequestDescription,
                s.RequestID,
                s.HasWelfareCertificate,
                s.IllnessHistory))
            .ConfigureAwait(false);
    }
}
