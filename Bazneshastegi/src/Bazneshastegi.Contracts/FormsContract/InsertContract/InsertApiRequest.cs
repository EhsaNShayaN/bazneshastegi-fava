namespace Bazneshastegi.Contracts.FormsContract.InsertContract;

public readonly record struct InsertApiRequest(
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
    ConditionApiRequest[] Conditions);

public readonly record struct ConditionApiRequest(
    int? ConditionValue,
    int? NextSate,
    string ButtonName);
