using Bazneshastegi.Application.Features.FormsFeature.RequestTypeAttachmentFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.RequestTypeAttachmentContract;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.RequestTypeAttachment;

sealed class RequestTypeAttachmentEndpoint : EndpointHandlerBase<
    RequestTypeAttachmentApiRequest,
    RequestTypeAttachmentQuery,
    ProviderRequestTypeAttachmentResponse[],
    RequestTypeAttachmentApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public RequestTypeAttachmentEndpoint(
        IPresentationMapper<RequestTypeAttachmentApiRequest, RequestTypeAttachmentQuery> requestMapper,
        IPresentationMapper<ProviderRequestTypeAttachmentResponse[], RequestTypeAttachmentApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.RequestTypeAttachment,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] RequestTypeAttachmentApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new RequestTypeAttachmentQuery(request.RequestTypeID))),
            cancellationToken);

}
sealed class GetRequestTypeAttachmentApiRequestMapper : IPresentationMapper<
    RequestTypeAttachmentApiRequest,
    RequestTypeAttachmentQuery>
{
    public ValueTask<PrimitiveResult<RequestTypeAttachmentQuery>> Map(
        RequestTypeAttachmentApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new RequestTypeAttachmentQuery(src.RequestTypeID)));
    }
}
sealed class GetRequestTypeAttachmentApiResponseMapper : IPresentationMapper<
    ProviderRequestTypeAttachmentResponse[],
    RequestTypeAttachmentApiResponse[]>
{
    public ValueTask<PrimitiveResult<RequestTypeAttachmentApiResponse[]>> Map(
        ProviderRequestTypeAttachmentResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new RequestTypeAttachmentApiResponse(
                    data.RequestTypeAttachmentID,
                    data.RequestTypeID,
                    data.LookupID,
                    data.LookupName,
                    data.Mandantory,
                    data.InsertUserID,
                    data.InsertTime,
                    data.UpdateUserID,
                    data.UpdateTime))
                .ToArray()));
    }
}