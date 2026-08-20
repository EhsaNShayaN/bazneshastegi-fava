namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderRelatedListForPortalRequest(string RequestTypeID, string ParentPersonID);

public readonly record struct ProviderRelatedListForPortalResponse(
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