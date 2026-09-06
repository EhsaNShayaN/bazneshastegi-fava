using Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditPersonInfoFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertRequestForEditPersonInfoContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertRequestForEditPersonInfo;
sealed class InsertRequestForEditPersonInfoEndpoint : EndpointHandlerBase<
    InsertRequestForEditPersonInfoApiRequest,
    InsertRequestForEditPersonInfoCommand,
    InsertRequestForEditPersonInfoCommandResponse,
    InsertRequestForEditPersonInfoApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertRequestForEditPersonInfoEndpoint(
        IPresentationMapper<InsertRequestForEditPersonInfoApiRequest, InsertRequestForEditPersonInfoCommand> apiRequestMapper,
        IPresentationMapper<InsertRequestForEditPersonInfoCommandResponse, InsertRequestForEditPersonInfoApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertRequestForEditPersonInfo,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertRequestForEditPersonInfoApiRequestMapper :
    IPresentationMapper<InsertRequestForEditPersonInfoApiRequest, InsertRequestForEditPersonInfoCommand>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditPersonInfoCommand>> Map(
        InsertRequestForEditPersonInfoApiRequest src,
        CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertRequestForEditPersonInfoCommand(
                    src.TempPersonID,
                    src.RequestID,
                    src.LoginedPersonID,
                    src.ThisPersonID,
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
                    src.Longitude ?? 0,
                    src.Latitude ?? 0,
                    src.BackupFirstName,
                    src.BackupLastName,
                    src.BackupNationalCode,
                    src.BackupRelation,
                    src.BackupPhone,
                    src.BackupCellphone,
                    src.BackupAddress,
                    src.GenderID,
                    src.MaritalStatusID,
                    src.EducationTypeID,
                    src.PersonDescription,
                    src.Picture)));
}
sealed class InsertRequestForEditPersonInfoApiResponseMapper : IPresentationMapper<
    InsertRequestForEditPersonInfoCommandResponse,
    InsertRequestForEditPersonInfoApiResponse>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditPersonInfoApiResponse>> Map(
        InsertRequestForEditPersonInfoCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertRequestForEditPersonInfoApiResponse(
                    src.TempPersonID,
                    src.RequestID,
                    src.LoginedPersonID,
                    src.ThisPersonID,
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
                    src.Longitude,
                    src.Latitude,
                    src.BackupFirstName,
                    src.BackupLastName,
                    src.BackupNationalCode,
                    src.BackupRelation,
                    src.BackupPhone,
                    src.BackupCellphone,
                    src.BackupAddress,
                    src.GenderID,
                    src.MaritalStatusID,
                    src.EducationTypeID,
                    src.PersonDescription,
                    src.Picture)));
}