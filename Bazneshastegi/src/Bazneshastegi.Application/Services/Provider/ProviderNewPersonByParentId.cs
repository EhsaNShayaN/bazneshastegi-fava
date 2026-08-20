namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderNewPersonByParentIdRequest(string PersonID);

public readonly record struct ProviderNewPersonByParentIdResponse(
    string PersonID,
    string? PensionaryID,
    string PersonNationalCode,
    string PersonFirstName,
    string PersonLastName,
    string PersonFatherName);
