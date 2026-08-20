using Bazneshastegi.Application.Features.FormsFeature.InsertComplementary_ImprestFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementary_ImprestContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary_Imprest;
sealed class InsertComplementary_ImprestEndpoint : EndpointHandlerBase<
    InsertComplementary_ImprestApiRequest,
    InsertComplementary_ImprestCommand,
    InsertComplementary_ImprestCommandResponse,
    InsertComplementary_ImprestApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementary_ImprestEndpoint(
        IPresentationMapper<InsertComplementary_ImprestApiRequest, InsertComplementary_ImprestCommand> apiRequestMapper,
        IPresentationMapper<InsertComplementary_ImprestCommandResponse, InsertComplementary_ImprestApiResponse> apiResponseMapper
        ) : base(
            Endpoints.Forms.InsertComplementary_Imprest,
            HttpMethod.Post,
            apiRequestMapper,
            apiResponseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementary_ImprestApiRequestMapper : IPresentationMapper<InsertComplementary_ImprestApiRequest, InsertComplementary_ImprestCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementary_ImprestCommand>> Map(InsertComplementary_ImprestApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementary_ImprestCommand(
                    src.RequestComplementaryID,
                    src.FacilityAmount,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID)));
}
sealed class InsertComplementary_ImprestApiResponseMapper : IPresentationMapper<
    InsertComplementary_ImprestCommandResponse,
    InsertComplementary_ImprestApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementary_ImprestApiResponse>> Map(
        InsertComplementary_ImprestCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementary_ImprestApiResponse(
                    src.RequestComplementaryID,
                    src.FacilityAmount,
                    src.RequestDescription,
                    src.RequestID,
                    src.RequestTypeID)));
}