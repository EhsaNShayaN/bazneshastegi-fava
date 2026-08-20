using Bazneshastegi.Application.Features.FormsFeature.RequestTypesFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.RequestTypeContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.RequestType;

sealed class RequestTypesEndpoint : EndpointHandlerBase<
    RequestTypeApiRequest,
    RequestTypesQuery,
    ProviderRequestTypeResponse[],
    RequestTypeApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public RequestTypesEndpoint(
        IPresentationMapper<RequestTypeApiRequest, RequestTypesQuery> requestMapper,
        IPresentationMapper<ProviderRequestTypeResponse[], RequestTypeApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.RequestTypes,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] RequestTypeApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new RequestTypesQuery())),
            cancellationToken);

}
sealed class GetRequestTypeApiRequestMapper : IPresentationMapper<
    RequestTypeApiRequest,
    RequestTypesQuery>
{
    public ValueTask<PrimitiveResult<RequestTypesQuery>> Map(
        RequestTypeApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new RequestTypesQuery()));
    }
}
sealed class GetRequestTypeApiResponseMapper : IPresentationMapper<
    ProviderRequestTypeResponse[],
    RequestTypeApiResponse[]>
{
    public ValueTask<PrimitiveResult<RequestTypeApiResponse[]>> Map(
        ProviderRequestTypeResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data =>
                    new RequestTypeApiResponse(
                        data.RequestTypeID,
                        data.Name,
                        data.StartState,
                        data.WorkFlowName,
                        data.UserView,
                        data.Role,
                        data.NatoinalCodeIsMandentory,
                        data.Page,
                        data.RequestFrom
                        )
                    ).ToArray()));
    }
}