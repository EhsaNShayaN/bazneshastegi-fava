using System;

namespace Bazneshastegi.Application.Services.Provider;

public readonly record struct ProviderRelatedPersonsRequest(string PersonID);

public readonly record struct ProviderRelatedPersonsResponse(
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