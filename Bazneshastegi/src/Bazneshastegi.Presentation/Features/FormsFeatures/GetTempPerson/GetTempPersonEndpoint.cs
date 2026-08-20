using Bazneshastegi.Application.Features.FormsFeature.GetTempPersonFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.GetTempPersonContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.GetTempPerson;

sealed class GetTempPersonEndpoint : EndpointHandlerBase<
    GetTempPersonApiRequest,
    GetTempPersonQuery,
    ProviderGetTempPersonResponse[],
    GetTempPersonApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public GetTempPersonEndpoint(
        IPresentationMapper<GetTempPersonApiRequest, GetTempPersonQuery> requestMapper,
        IPresentationMapper<ProviderGetTempPersonResponse[], GetTempPersonApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.GetTempPerson,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] GetTempPersonApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new GetTempPersonQuery(
                request.RequestID))),
            cancellationToken);

}
sealed class GetGetTempPersonApiRequestMapper : IPresentationMapper<
    GetTempPersonApiRequest,
    GetTempPersonQuery>
{
    public ValueTask<PrimitiveResult<GetTempPersonQuery>> Map(
        GetTempPersonApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new GetTempPersonQuery(
            src.RequestID)));
    }
}
sealed class GetGetTempPersonApiResponseMapper : IPresentationMapper<
    ProviderGetTempPersonResponse[],
    GetTempPersonApiResponse[]>
{
    public ValueTask<PrimitiveResult<GetTempPersonApiResponse[]>> Map(
        ProviderGetTempPersonResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new GetTempPersonApiResponse(
                    data.TempPersonID,
                    data.IsNewPerson,
                    data.ThisPersonID,
                    data.RequestID,
                    data.IsChecked,
                    data.LoginedPersonID,
                    data.RelationshipID,
                    data.PensionaryStatusID,
                    data.PersonNationalCode,
                    data.PersonFirstName,
                    data.PersonLastName,
                    data.PersonFatherName,
                    data.PersonCertificateNo,
                    data.PersonBirthDate,
                    data.PersonBirthPlaceStateID,
                    data.PersonBirthPlaceCityID,
                    data.PersonCountryID,
                    data.PersonStateID,
                    data.PersonCityID,
                    data.PersonAddress,
                    data.PersonPostalCode,
                    data.PersonRegion,
                    data.PersonArea,
                    data.PersonPhone,
                    data.PersonCellPhone,
                    data.GenderID,
                    data.MaritalStatusID,
                    data.EducationTypeID,
                    data.UniversityID,
                    data.EducationTypeCaption,
                    data.UniversityCaption,
                    data.PersonDescription,
                    data.IsUnderGauarantee,
                    data.ExistingPerson,
                    data.EducationTypeName,
                    data.RelationshipName))
                .ToArray()));
    }
}