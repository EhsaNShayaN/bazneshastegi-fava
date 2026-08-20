using Bazneshastegi.Application.Features.FormsFeature.NewPersonByParentIdFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.NewPersonByParentIdContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.NewPersonByParentId;

sealed class NewPersonByParentIdEndpoint : EndpointHandlerBase<
    NewPersonByParentIdApiRequest,
    NewPersonByParentIdQuery,
    ProviderNewPersonByParentIdResponse[],
    NewPersonByParentIdApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public NewPersonByParentIdEndpoint(
        IPresentationMapper<NewPersonByParentIdApiRequest, NewPersonByParentIdQuery> requestMapper,
        IPresentationMapper<ProviderNewPersonByParentIdResponse[], NewPersonByParentIdApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.NewPersonByParentId,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] NewPersonByParentIdApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(NewPersonByParentIdQuery.Default)),
            cancellationToken);

}
sealed class GetNewPersonByParentIdApiRequestMapper : IPresentationMapper<
    NewPersonByParentIdApiRequest,
    NewPersonByParentIdQuery>
{
    public ValueTask<PrimitiveResult<NewPersonByParentIdQuery>> Map(
        NewPersonByParentIdApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(NewPersonByParentIdQuery.Default));
    }
}
sealed class GetNewPersonByParentIdApiResponseMapper : IPresentationMapper<
    ProviderNewPersonByParentIdResponse[],
    NewPersonByParentIdApiResponse[]>
{
    public ValueTask<PrimitiveResult<NewPersonByParentIdApiResponse[]>> Map(
        ProviderNewPersonByParentIdResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new NewPersonByParentIdApiResponse(
                        data.PersonID,
                        data.PensionaryID,
                        data.PersonNationalCode,
                        data.PersonFirstName,
                        data.PersonLastName,
                        data.PersonFatherName)
                    ).ToArray()));
    }
}