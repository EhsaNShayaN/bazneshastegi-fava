using Bazneshastegi.Application.Features.FormsFeature.InsertRequestForEditBasketReceiveTypeHeirFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertRequestForEditBasketReceiveTypeHeirContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertRequestForEditBasketReceiveTypeHeir;
sealed class InsertRequestForEditBasketReceiveTypeHeirEndpoint : EndpointHandlerBase<
    InsertRequestForEditBasketReceiveTypeHeirApiRequest,
    InsertRequestForEditBasketReceiveTypeHeirCommand,
    InsertRequestForEditBasketReceiveTypeHeirCommandResponse,
    InsertRequestForEditBasketReceiveTypeHeirApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertRequestForEditBasketReceiveTypeHeirEndpoint(
        IPresentationMapper<InsertRequestForEditBasketReceiveTypeHeirApiRequest, InsertRequestForEditBasketReceiveTypeHeirCommand> apiRequestMapper,
        IPresentationMapper<InsertRequestForEditBasketReceiveTypeHeirCommandResponse, InsertRequestForEditBasketReceiveTypeHeirApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertRequestForEditBasketReceiveTypeHeir,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertRequestForEditBasketReceiveTypeHeirApiRequestMapper : IPresentationMapper<InsertRequestForEditBasketReceiveTypeHeirApiRequest, InsertRequestForEditBasketReceiveTypeHeirCommand>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditBasketReceiveTypeHeirCommand>> Map(InsertRequestForEditBasketReceiveTypeHeirApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertRequestForEditBasketReceiveTypeHeirCommand(
                    src.RequestID,
                    src.RequestTypeID,
                    src.LoginedPersonID,
                    src.BasketReceiveTypeID,

                    src.ThisPersonID,
                    src.PersonAddress,
                    src.PersonPostalCode,
                    src.PersonRegion,
                    src.PersonArea,
                    src.PersonPhone,
                    src.PersonCellPhone)));
}
sealed class InsertRequestForEditBasketReceiveTypeHeirApiResponseMapper : IPresentationMapper<
    InsertRequestForEditBasketReceiveTypeHeirCommandResponse,
    InsertRequestForEditBasketReceiveTypeHeirApiResponse>
{
    public ValueTask<PrimitiveResult<InsertRequestForEditBasketReceiveTypeHeirApiResponse>> Map(
        InsertRequestForEditBasketReceiveTypeHeirCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertRequestForEditBasketReceiveTypeHeirApiResponse(
                        src.RequestID,
                        src.RequestTypeID,
                        src.LoginedPersonID,
                        src.BasketReceiveTypeID,

                        src.ThisPersonID,
                        src.PersonAddress,
                        src.PersonPostalCode,
                        src.PersonRegion,
                        src.PersonArea,
                        src.PersonPhone,
                        src.PersonCellPhone)));
}