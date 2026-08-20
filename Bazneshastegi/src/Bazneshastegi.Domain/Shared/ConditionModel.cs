namespace Bazneshastegi.Domain.Shared;
public readonly record struct ConditionModel(
    int? ConditionValue,
    int? NextSate,
    string ButtonName);