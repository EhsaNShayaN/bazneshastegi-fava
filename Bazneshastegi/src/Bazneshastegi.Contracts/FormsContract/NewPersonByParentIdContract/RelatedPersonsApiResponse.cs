namespace Bazneshastegi.Contracts.FormsContract.NewPersonByParentIdContract;
public readonly record struct NewPersonByParentIdApiResponse(
    string PersonID,
    string? PensionaryID,
    string PersonNationalCode,
    string PersonFirstName,
    string PersonLastName,
    string PersonFatherName);
