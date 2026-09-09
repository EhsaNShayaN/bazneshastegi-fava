using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditPersonInfoFeature;
public sealed class InsertRequestForEditPersonInfoCommandHandler : IPrimitiveResultCommandHandler<
    InsertRequestForEditPersonInfoCommand,
    InsertRequestForEditPersonInfoCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;
    private readonly IUserContextAccessor _userContextAccessor;

    public InsertRequestForEditPersonInfoCommandHandler(
        IBazneshastegiService bazneshastegiService,
        IUserContextAccessor userContextAccessor)
    {
        this._bazneshastegiService = bazneshastegiService;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<InsertRequestForEditPersonInfoCommandResponse>> Handle(
        InsertRequestForEditPersonInfoCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequestForEditPersonInfo(
            new ProviderInsertRequestForEditPersonInfoRequest(
                request.RequestTypeID,
                request.TempPersonID,
                request.RequestID,
                _userContextAccessor.CurrentPersonId,
                _userContextAccessor.CurrentPersonId,
                request.PersonNationalCode,
                request.PersonFirstName,
                request.PersonLastName,
                request.PersonFatherName,
                request.PersonCertificateNo,
                request.PersonBirthDate,
                request.PersonBirthPlaceStateID,
                request.PersonBirthPlaceCityID,
                request.PersonCountryID,
                request.PersonStateID,
                request.PersonCityID,
                request.PersonAddress,
                request.PersonPostalCode,
                request.PersonRegion,
                request.PersonArea,
                request.PersonPhone,
                request.PersonCellPhone,
                request.Longitude,
                request.Latitude,
                request.BackupFirstName,
                request.BackupLastName,
                request.BackupNationalCode,
                request.BackupRelation,
                request.BackupPhone,
                request.BackupCellphone,
                request.BackupAddress,
                request.GenderID,
                request.MaritalStatusID,
                request.EducationTypeID,
                request.PersonDescription,
                request.Picture),
            cancellationToken)
            .Map(s => new InsertRequestForEditPersonInfoCommandResponse(
                s.TempPersonID,
                s.RequestID,
                s.LoginedPersonID,
                s.ThisPersonID,
                s.PersonNationalCode,
                s.PersonFirstName,
                s.PersonLastName,
                s.PersonFatherName,
                s.PersonCertificateNo,
                s.PersonBirthDate,
                s.PersonBirthPlaceStateID,
                s.PersonBirthPlaceCityID,
                s.PersonCountryID,
                s.PersonStateID,
                s.PersonCityID,
                s.PersonAddress,
                s.PersonPostalCode,
                s.PersonRegion,
                s.PersonArea,
                s.PersonPhone,
                s.PersonCellPhone,
                s.Longitude,
                s.Latitude,
                s.BackupFirstName,
                s.BackupLastName,
                s.BackupNationalCode,
                s.BackupRelation,
                s.BackupPhone,
                s.BackupCellphone,
                s.BackupAddress,
                s.GenderID,
                s.MaritalStatusID,
                s.EducationTypeID,
                s.PersonDescription,
                s.Picture))
            .ConfigureAwait(false);
    }
}
