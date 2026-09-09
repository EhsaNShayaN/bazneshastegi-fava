using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditPersonInfoFeature;

public sealed record class InsertRequestForEditPersonInfoCommand(
    string RequestTypeID,
    string TempPersonID,
    string RequestID,
    string LoginedPersonID,
    string ThisPersonID,
    string PersonNationalCode,
    string PersonFirstName,
    string PersonLastName,
    string PersonFatherName,
    string PersonCertificateNo,
    DateTime? PersonBirthDate,
    string PersonBirthPlaceStateID,
    string PersonBirthPlaceCityID,
    string PersonCountryID,
    string PersonStateID,
    string PersonCityID,
    string PersonAddress,
    string PersonPostalCode,
    int? PersonRegion,
    int? PersonArea,
    string PersonPhone,
    string PersonCellPhone,
    double Longitude,
    double Latitude,
    string BackupFirstName,
    string BackupLastName,
    string BackupNationalCode,
    string BackupRelation,
    string BackupPhone,
    string BackupCellphone,
    string BackupAddress,
    string GenderID,
    string MaritalStatusID,
    string EducationTypeID,
    string PersonDescription,
    string Picture) : IPrimitiveResultCommand<InsertRequestForEditPersonInfoCommandResponse>,
    IValidatableRequest<InsertRequestForEditPersonInfoCommand>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditPersonInfoCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}