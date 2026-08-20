using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertNewPersonFeature;
public sealed class InsertNewPersonCommandHandler : IPrimitiveResultCommandHandler<
    InsertNewPersonCommand,
    InsertNewPersonCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;
    private readonly IUserContextAccessor _userContextAccessor;

    public InsertNewPersonCommandHandler(
        IBazneshastegiService bazneshastegiService,
        IUserContextAccessor userContextAccessor)
    {
        this._bazneshastegiService = bazneshastegiService;
        this._userContextAccessor = userContextAccessor;
    }

    public async Task<PrimitiveResult<InsertNewPersonCommandResponse>> Handle(
        InsertNewPersonCommand request,
        CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertNewPerson(
            new ProviderInsertNewPersonRequest(
                request.TempPersonID,
                request.RequestID,
                _userContextAccessor.CurrentPersonId,
                request.RelationshipID,
                request.PensionaryStatusID,
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
                request.GenderID,
                request.MaritalStatusID,
                request.EducationTypeID,
                request.UniversityID,
                request.EducationTypeCaption,
                request.UniversityCaption,
                request.PersonDescription,
                true),
            cancellationToken)
            .Map(s => new InsertNewPersonCommandResponse(
                s.TempPersonID,
                s.RequestID,
                s.LoginedPersonID,
                s.RelationshipID,
                s.PensionaryStatusID,
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
                s.GenderID,
                s.MaritalStatusID,
                s.EducationTypeID,
                s.UniversityID,
                s.EducationTypeCaption,
                s.UniversityCaption,
                s.PersonDescription))
            .ConfigureAwait(false);
    }
}
