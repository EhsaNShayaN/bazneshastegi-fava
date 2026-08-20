namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderRelationshipRequest(string RelationshipID);

public readonly record struct ProviderRelationshipResponse(
    string RelationshipID,
    string RelationshipName,
    string RelationshipTypeID,
    bool? RelationshipGetsChildRight);
