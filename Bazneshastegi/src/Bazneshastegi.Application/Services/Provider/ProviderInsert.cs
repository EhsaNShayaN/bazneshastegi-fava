using Bazneshastegi.Domain.Shared;

namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderInsert(
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
    bool IsLocked,
    string? LockedUserID,
    string? Page,
    ConditionModel[]? Conditions);

public readonly record struct ProviderInsertResponse(
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