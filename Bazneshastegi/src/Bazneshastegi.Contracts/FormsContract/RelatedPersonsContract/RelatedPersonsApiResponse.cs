namespace Bazneshastegi.Contracts.FormsContract.RelatedPersonsContract;
public readonly record struct RelatedPersonsApiResponse(
    string PersonID,
    string PensionaryID,
    string PersonNationalCode,
    string PersonFirstName,
    string PersonLastName,
    DateTime? PersonBirthDate,
    string RelationshipWithParentID,
    string RelationshipWithParentName,
    bool? PensionaryIsUnderGaurantee,
    string PensionaryIsUnderGauranteeText,
    string RelatedMedicalText,
    string EducationTypeID,
    string EducationTypeName,
    bool? IsChosen,
    bool? IsPermitted);
