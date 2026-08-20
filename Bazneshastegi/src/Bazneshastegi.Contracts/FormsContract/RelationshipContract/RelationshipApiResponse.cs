namespace Bazneshastegi.Contracts.FormsContract.RelationshipContract;
public readonly record struct RelationshipApiResponse(
    string RelationshipID,
    string RelationshipName,
    string RelationshipTypeID,
    bool? RelationshipGetsChildRight);