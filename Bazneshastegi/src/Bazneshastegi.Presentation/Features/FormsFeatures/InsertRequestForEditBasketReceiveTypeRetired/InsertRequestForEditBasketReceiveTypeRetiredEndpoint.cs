using Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditBasketReceiveTypeRetiredFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertRequestForEditBasketReceiveTypeRetiredContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertRequestForEditBasketReceiveTypeRetired;
sealed class InsertRequestForEditBasketReceiveTypeRetiredEndpoint : EndpointHandlerBase<
    InsertRequestForEditBasketReceiveTypeRetiredApiRequest,
    InsertRequestForEditBasketReceiveTypeRetiredCommand,
    InsertRequestForEditBasketReceiveTypeRetiredCommandResponse,
    InsertRequestForEditBasketReceiveTypeRetiredApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertRequestForEditBasketReceiveTypeRetiredEndpoint(
        IPresentationMapper<InsertRequestForEditBasketReceiveTypeRetiredApiRequest, InsertRequestForEditBasketReceiveTypeRetiredCommand> apiRequestMapper,
        IPresentationMapper<InsertRequestForEditBasketReceiveTypeRetiredCommandResponse, InsertRequestForEditBasketReceiveTypeRetiredApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertRequestForEditBasketReceiveTypeRetired,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertRequestForEditBasketReceiveTypeRetiredApiRequestMapper : IPresentationMapper<InsertRequestForEditBasketReceiveTypeRetiredApiRequest, InsertRequestForEditBasketReceiveTypeRetiredCommand>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditBasketReceiveTypeRetiredCommand>> Map(InsertRequestForEditBasketReceiveTypeRetiredApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertRequestForEditBasketReceiveTypeRetiredCommand(
                    src.RequestID,
                    src.RequestTypeID,
                    src.LoginedPersonID,
                    src.BasketReceiveTypeID)));
}
sealed class InsertRequestForEditBasketReceiveTypeRetiredApiResponseMapper : IPresentationMapper<
    InsertRequestForEditBasketReceiveTypeRetiredCommandResponse,
    InsertRequestForEditBasketReceiveTypeRetiredApiResponse>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditBasketReceiveTypeRetiredApiResponse>> Map(
        InsertRequestForEditBasketReceiveTypeRetiredCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertRequestForEditBasketReceiveTypeRetiredApiResponse(
                        src.RequestID,
                        src.RequestTypeID,
                        src.LoginedPersonID,
                        src.BasketReceiveTypeID)));
}