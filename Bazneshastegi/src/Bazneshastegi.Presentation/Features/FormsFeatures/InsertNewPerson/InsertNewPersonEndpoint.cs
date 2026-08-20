using Bazneshastegi.Application.Features.FormsFeature.InsertNewPersonFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertNewPersonContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertNewPerson;
sealed class InsertNewPersonEndpoint : EndpointHandlerBase<
    InsertNewPersonApiRequest,
    InsertNewPersonCommand,
    InsertNewPersonCommandResponse,
    InsertNewPersonApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertNewPersonEndpoint(
        IPresentationMapper<InsertNewPersonApiRequest, InsertNewPersonCommand> apiRequestMapper,
        IPresentationMapper<InsertNewPersonCommandResponse, InsertNewPersonApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertNewPerson,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertNewPersonApiRequestMapper : IPresentationMapper<InsertNewPersonApiRequest, InsertNewPersonCommand>
{
    public ValueTask<PrimitiveResult<InsertNewPersonCommand>> Map(InsertNewPersonApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertNewPersonCommand
                {
                    RequestID = src.RequestID,
                    TempPersonID = string.Empty,
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
sealed class InsertNewPersonApiResponseMapper : IPresentationMapper<
    InsertNewPersonCommandResponse,
    InsertNewPersonApiResponse>
{
    public ValueTask<PrimitiveResult<InsertNewPersonApiResponse>> Map(
        InsertNewPersonCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertNewPersonApiResponse(
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