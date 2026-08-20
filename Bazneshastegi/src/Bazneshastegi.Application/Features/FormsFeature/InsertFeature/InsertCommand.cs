using Bazneshastegi.Domain.Shared;
using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertFeature;

public sealed record class InsertCommand(
    string RequestID,
    string PersonID,
    string NationalCode,
    string PersonFirstName,
    string PersonLastName,
    DateTime? RequestDate,
    string RequestTypeID,
    string RequestText,
    string InsertUserID,
    string UpdateUserID,
    int? RequestFrom,
    int? State,
    string StateName,
    string RequestTypeName,
    string RequestTypeNameFa,
    string RequestNO,
    int? ConditionValue,
    bool IsLocked,
    string LockedUserID,
    string Page,
    ConditionModel[]? Conditions) : IPrimitiveResultCommand<InsertCommandResponse>,
    IValidatableRequest<InsertCommand>
{
    public ValueTask<PrimitiveResult<InsertCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}