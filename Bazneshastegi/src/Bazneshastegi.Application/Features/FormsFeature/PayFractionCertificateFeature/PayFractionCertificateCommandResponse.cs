using Bazneshastegi.Domain.Shared;

namespace Bazneshastegi.Application.Features.FormsFeature.PayFractionCertificateFeature;
public sealed record PayFractionCertificateCommandResponse(
    string RequestID,
    string PersonID,
    string NationalCode,
    string PersonFirstName,
    string PersonLastName,
    DateTime? RequestDate,
    string RequestTypeID,
    string RequestText,
    string InsertUserID,
    string? UpdateUserID,
    int? RequestFrom,
    int? State,
    string? StateName,
    string? RequestTypeName,
    string? RequestTypeNameFa,
    string RequestNO,
    int? ConditionValue,
    bool? IsLocked,
    string? LockedUserID,
    string? Page,
    ConditionModel[]? Conditions);