using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_PhysicalDisability_IllnessFeature;

public sealed record class InsertComplementary_PhysicalDisability_IllnessCommand(
    string MethodName,
    string RequestComplementaryID,
    string RelatedPersonID,
    string RequestDescription,
    string RequestID,
    string RequestTypeID,
    bool HasWelfareCertificate,
    string IllnessHistory) : IPrimitiveResultCommand<InsertComplementary_PhysicalDisability_IllnessCommandResponse>,
    IValidatableRequest<InsertComplementary_PhysicalDisability_IllnessCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_PhysicalDisability_IllnessCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}