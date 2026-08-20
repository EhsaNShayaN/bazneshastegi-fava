using Bazneshastegi.Application.Features.FormsFeature.PersonInfoFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.PersonInfoContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.PersonInfo;

sealed class PersonInfoEndpoint : EndpointHandlerBase<
    PersonInfoApiRequest,
    PersonInfoQuery,
    ProviderPersonInfoResponse,
    PersonInfoApiResponse>
{
    protected override bool NeedTaxPayerFile => true;

    public PersonInfoEndpoint(
        IPresentationMapper<PersonInfoApiRequest, PersonInfoQuery> requestMapper,
        IPresentationMapper<ProviderPersonInfoResponse, PersonInfoApiResponse> responseMapper)
        : base(
            Endpoints.Forms.PersonInfo,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] PersonInfoApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(PersonInfoQuery.Default)),
            cancellationToken);

}
sealed class GetPersonInfoApiRequestMapper : IPresentationMapper<
    PersonInfoApiRequest,
    PersonInfoQuery>
{
    public ValueTask<PrimitiveResult<PersonInfoQuery>> Map(
        PersonInfoApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(PersonInfoQuery.Default));
    }
}
sealed class GetPersonInfoApiResponseMapper : IPresentationMapper<
    ProviderPersonInfoResponse,
    PersonInfoApiResponse>
{
    public ValueTask<PrimitiveResult<PersonInfoApiResponse>> Map(
        ProviderPersonInfoResponse src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                    new PersonInfoApiResponse(
                        src.PersonID,
                        src.PensionaryID,
                        src.PersonNationalCode,
                        src.PersonFirstName,
                        src.PersonLastName,
                        src.PersonFatherName,
                        src.PersonCertificateNo,
                        src.PersonBirthDate,
                        src.PersonAddress,
                        src.PersonPostalCode,
                        src.PersonRegion,
                        src.PersonArea,
                        src.PersonPhone,
                        src.PersonCellPhone,
                        src.PersonCellPhone2,
                        src.RetiredID,
                        src.RetiredRealDuration,
                        src.RetiredRealDurationYEAR,
                        src.RetiredRealDurationMONTH,
                        src.RetiredRealDurationDAY,
                        src.ParentPersonID,
                        src.PensionaryStatusID,
                        src.PensionaryStatusName,
                        src.GenderID,
                        src.RelationshipWithParentID,
                        src.RelationshipWithParentName,
                        src.RetirementDate,
                        src.PayAmount,
                        src.EducationTypeID,
                        src.EducationTypeName,
                        src.EducationTypeCaption,
                        src.BasketReceiveTypeID,
                        src.BasketReceiveTypeName,
                        src.RemainedAmountForCertificate,
                        src.EmploymentTypeID,
                        src.EmploymentTypeName,
                        ///////////////////////////////////
                        src.MaritalStatusID,
                        src.RelatedCount,
                        src.PersonBirthPlaceStateID,
                        src.PersonBirthPlaceCityID,
                        src.PersonCountryID,
                        src.PersonStateID,
                        src.PersonCityID,
                        src.Longitude,
                        src.Latitude,
                        src.BackupFirstName,
                        src.BackupLastName,
                        src.BackupNationalCode,
                        src.BackupRelation,
                        src.BackupPhone,
                        src.BackupCellphone,
                        src.BackupAddress,
                        src.PersonDescription,
                        src.Picture)));
    }
}