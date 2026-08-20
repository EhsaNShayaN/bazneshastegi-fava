using Bazneshastegi.Application.Features.FormsFeature.RelatedListForPortalFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.RelatedListForPortalContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.RelatedListForPortal;

sealed class RelatedListForPortalEndpoint : EndpointHandlerBase<
    RelatedListForPortalApiRequest,
    RelatedListForPortalQuery,
    ProviderRelatedListForPortalResponse[],
    RelatedListForPortalApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public RelatedListForPortalEndpoint(
        IPresentationMapper<RelatedListForPortalApiRequest, RelatedListForPortalQuery> requestMapper,
        IPresentationMapper<ProviderRelatedListForPortalResponse[], RelatedListForPortalApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.RelatedListForPortal,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
sealed class GetRelatedListForPortalApiRequestMapper : IPresentationMapper<
    RelatedListForPortalApiRequest,
    RelatedListForPortalQuery>
{
    public ValueTask<PrimitiveResult<RelatedListForPortalQuery>> Map(
        RelatedListForPortalApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new RelatedListForPortalQuery(src.RequestTypeID)));
    }
}
sealed class GetRelatedListForPortalApiResponseMapper : IPresentationMapper<
    ProviderRelatedListForPortalResponse[],
    RelatedListForPortalApiResponse[]>
{
    public ValueTask<PrimitiveResult<RelatedListForPortalApiResponse[]>> Map(
        ProviderRelatedListForPortalResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new RelatedListForPortalApiResponse(
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