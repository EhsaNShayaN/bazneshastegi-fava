using Bazneshastegi.Application.Features.FormsFeature.UpdateNewPersonFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.UpdateNewPersonContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.UpdateNewPerson;
sealed class UpdateNewPersonEndpoint : EndpointHandlerBase<
    UpdateNewPersonApiRequest,
    UpdateNewPersonCommand,
    UpdateNewPersonCommandResponse,
    UpdateNewPersonApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public UpdateNewPersonEndpoint(
        IPresentationMapper<UpdateNewPersonApiRequest, UpdateNewPersonCommand> apiRequestMapper,
        IPresentationMapper<UpdateNewPersonCommandResponse, UpdateNewPersonApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.UpdateNewPerson,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class UpdateNewPersonApiRequestMapper : IPresentationMapper<UpdateNewPersonApiRequest, UpdateNewPersonCommand>
{
    public ValueTask<PrimitiveResult<UpdateNewPersonCommand>> Map(UpdateNewPersonApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new UpdateNewPersonCommand
                {
                    RequestTypeID = src.RequestTypeID,
                    TempPersonID = src.TempPersonID,
                    RequestID = src.RequestID,
                    RelationshipID = src.RelationshipID,
                    PersonFirstName = src.PersonFirstName,
                    PersonLastName = src.PersonLastName,
                    PersonNationalCode = src.PersonNationalCode,
                    PersonFatherName = src.PersonFatherName,
                    PersonCertificateNo = src.PersonCertificateNo,
                    PersonBirthDate = src.PersonBirthDate,
                    PersonBirthPlaceStateID = src.PersonBirthPlaceStateID,
                    PersonBirthPlaceCityID = src.PersonBirthPlaceCityID,
                    GenderID = src.GenderID,
                    MaritalStatusID = src.MaritalStatusID,
                    EducationTypeID = src.EducationTypeID,
                    EducationBranchID = src.EducationBranchID,
                    UniversityID = src.UniversityID,
                    PersonPhone = src.PersonPhone,
                    PersonCellPhone = src.PersonCellPhone,
                    PersonStateID = src.PersonStateID,
                    PersonCityID = src.PersonCityID,
                    PersonRegion = src.PersonRegion,
                    PersonArea = src.PersonArea,
                    PersonPostalCode = src.PersonPostalCode,
                    PersonAddress = src.PersonAddress,
                    PersonDescription = src.PersonDescription,
                }));
}
sealed class UpdateNewPersonApiResponseMapper : IPresentationMapper<
    UpdateNewPersonCommandResponse,
    UpdateNewPersonApiResponse>
{
    public ValueTask<PrimitiveResult<UpdateNewPersonApiResponse>> Map(
        UpdateNewPersonCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new UpdateNewPersonApiResponse(
                        src.TempPersonID,
                        src.RequestID,
                        src.LoginedPersonID,
                        src.RelationshipID,
                        src.PensionaryStatusID,
                        src.PersonNationalCode,
                        src.PersonFirstName,
                        src.PersonLastName,
                        src.PersonFatherName,
                        src.PersonCertificateNo,
                        src.PersonBirthDate,
                        src.PersonBirthPlaceStateID,
                        src.PersonBirthPlaceCityID,
                        src.PersonCountryID,
                        src.PersonStateID,
                        src.PersonCityID,
                        src.PersonAddress,
                        src.PersonPostalCode,
                        src.PersonRegion,
                        src.PersonArea,
                        src.PersonPhone,
                        src.PersonCellPhone,
                        src.GenderID,
                        src.MaritalStatusID,
                        src.EducationTypeID,
                        src.UniversityID,
                        src.EducationTypeCaption,
                        src.UniversityCaption,
                        src.PersonDescription)));
}