using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.UpdateNewPersonFeature;

public sealed class UpdateNewPersonCommand
    : IPrimitiveResultCommand<UpdateNewPersonCommandResponse>,
      IValidatableRequest<UpdateNewPersonCommand>
{
    public string TempPersonID { get; set; } = string.Empty;
    public string RequestID { get; set; } = string.Empty;
    public string RelationshipID { get; set; } = string.Empty;
    public string PensionaryStatusID { get; set; } = string.Empty;
    public string PersonNationalCode { get; set; } = string.Empty;
    public string PersonFirstName { get; set; } = string.Empty;
    public string PersonLastName { get; set; } = string.Empty;
    public string PersonFatherName { get; set; } = string.Empty;
    public string PersonCertificateNo { get; set; } = string.Empty;
    public DateTime? PersonBirthDate { get; set; }
    public string PersonBirthPlaceStateID { get; set; } = string.Empty;
    public string PersonBirthPlaceCityID { get; set; } = string.Empty;
    public string PersonCountryID { get; set; } = string.Empty;
    public string PersonStateID { get; set; } = string.Empty;
    public string PersonCityID { get; set; } = string.Empty;
    public string PersonAddress { get; set; } = string.Empty;
    public string PersonPostalCode { get; set; } = string.Empty;
    public int? PersonRegion { get; set; }
    public int? PersonArea { get; set; }
    public string PersonPhone { get; set; } = string.Empty;
    public string PersonCellPhone { get; set; } = string.Empty;
    public string GenderID { get; set; } = string.Empty;
    public string MaritalStatusID { get; set; } = string.Empty;
    public string EducationTypeID { get; set; } = string.Empty;
    public string EducationBranchID { get; set; } = string.Empty;
    public string UniversityID { get; set; } = string.Empty;
    public string EducationTypeCaption { get; set; } = string.Empty;
    public string UniversityCaption { get; set; } = string.Empty;
    public string PersonDescription { get; set; } = string.Empty;

    public ValueTask<PrimitiveResult<UpdateNewPersonCommand>> Validate()
        => PrimitiveResult.Success(this)
            .Ensure([
                _ => PrimitiveResult.Success(this.RequestID)
                    .Match(
                        _ => PrimitiveResult.Success(),
                        _ => PrimitiveResult.Failure(
                            "Validation.Error",
                            "موبایل ارسالی نامعتبر است"))
            ]);
}
