using Bazneshastegi.Application.Features.FormsFeature.RelatedPersonsFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.RelatedPersonsContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.RelatedPersons;

sealed class RelatedPersonsEndpoint : EndpointHandlerBase<
    RelatedPersonsApiRequest,
    RelatedPersonsQuery,
    ProviderRelatedPersonsResponse[],
    RelatedPersonsApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public RelatedPersonsEndpoint(
        IPresentationMapper<RelatedPersonsApiRequest, RelatedPersonsQuery> requestMapper,
        IPresentationMapper<ProviderRelatedPersonsResponse[], RelatedPersonsApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.RelatedPersons,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] RelatedPersonsApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(RelatedPersonsQuery.Default)),
            cancellationToken);

}
sealed class GetRelatedPersonsApiRequestMapper : IPresentationMapper<
    RelatedPersonsApiRequest,
    RelatedPersonsQuery>
{
    public ValueTask<PrimitiveResult<RelatedPersonsQuery>> Map(
        RelatedPersonsApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(RelatedPersonsQuery.Default));
    }
}
sealed class GetRelatedPersonsApiResponseMapper : IPresentationMapper<
    ProviderRelatedPersonsResponse[],
    RelatedPersonsApiResponse[]>
{
    public ValueTask<PrimitiveResult<RelatedPersonsApiResponse[]>> Map(
        ProviderRelatedPersonsResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new RelatedPersonsApiResponse(
                    data.PersonID,
                    data.PensionaryID,
                    data.PersonNationalCode,
                    data.PersonFirstName,
                    data.PersonLastName,
                    data.PersonBirthDate,
                    data.RelationshipWithParentID,
                    data.RelationshipWithParentName,
                    data.PensionaryIsUnderGaurantee,
                    data.PensionaryIsUnderGauranteeText,
                    data.RelatedMedicalText,
                    data.EducationTypeID,
                    data.EducationTypeName,
                    data.IsChosen,
                    data.IsPermitted)
                    ).ToArray()));
    }
}