using Bazneshastegi.Application.Features.FormsFeature.RelationshipFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.RelationshipContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.Relationship;

sealed class RelationshipEndpoint : EndpointHandlerBase<
    RelationshipApiRequest,
    RelationshipQuery,
    ProviderRelationshipResponse[],
    RelationshipApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public RelationshipEndpoint(
        IPresentationMapper<RelationshipApiRequest, RelationshipQuery> requestMapper,
        IPresentationMapper<ProviderRelationshipResponse[], RelationshipApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.Relationship,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] RelationshipApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new RelationshipQuery(request.RelationshipID))),
            cancellationToken);

}
sealed class GetRelationshipApiRequestMapper : IPresentationMapper<
    RelationshipApiRequest,
    RelationshipQuery>
{
    public ValueTask<PrimitiveResult<RelationshipQuery>> Map(
        RelationshipApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new RelationshipQuery(src.RelationshipID)));
    }
}
sealed class GetRelationshipApiResponseMapper : IPresentationMapper<
    ProviderRelationshipResponse[],
    RelationshipApiResponse[]>
{
    public ValueTask<PrimitiveResult<RelationshipApiResponse[]>> Map(
        ProviderRelationshipResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data =>
                    new RelationshipApiResponse(
                        data.RelationshipID,
                        data.RelationshipName,
                        data.RelationshipTypeID,
                        data.RelationshipGetsChildRight))
                .ToArray()));
    }
}