using Bazneshastegi.Application.Features.FormsFeature.InsertFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertContract;
using Bazneshastegi.Domain.Shared;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.Insert;
sealed class InsertEndpoint : EndpointHandlerBase<
    InsertApiRequest,
    InsertCommand,
    InsertCommandResponse,
    InsertApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertEndpoint(
        IPresentationMapper<InsertApiRequest, InsertCommand> requestMapper,
        IPresentationMapper<InsertCommandResponse, InsertApiResponse> responseMapper
        ) : base(
            Endpoints.Forms.Insert,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertApiRequestMapper : IPresentationMapper<InsertApiRequest, InsertCommand>
{
    public ValueTask<PrimitiveResult<InsertCommand>> Map(InsertApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
               PrimitiveResult.Success(
                   new InsertCommand(
                       src.RequestID,
                       src.PersonID,
                       src.NationalCode,
                       src.PersonFirstName,
                       src.PersonLastName,
                       src.RequestDate,
                       src.RequestTypeID,
                       src.RequestText,
                       src.InsertUserID,
                       src.UpdateUserID,
                       src.RequestFrom,
                       src.State,
                       src.StateName,
                       src.RequestTypeName,
                       src.RequestTypeNameFa,
                       src.RequestNO,
                       src.ConditionValue,
                       src.IsLocked,
                       src.LockedUserID,
                       src.Page,
                       src.Conditions?.Select(condition =>
                        new ConditionModel(
                            condition.ConditionValue,
                            condition.NextSate,
                            condition.ButtonName))?.ToArray())));
}
sealed class InsertApiResponseMapper : IPresentationMapper<
    InsertCommandResponse,
    InsertApiResponse>
{
    public ValueTask<PrimitiveResult<InsertApiResponse>> Map(
        InsertCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertApiResponse(
                        src.RequestID,
                        src.PersonID,
                        src.NationalCode,
                        src.PersonFirstName,
                        src.PersonLastName,
                        src.RequestDate,
                        src.RequestTypeID,
                        src.RequestText,
                        src.InsertUserID,
                        src.UpdateUserID,
                        src.RequestFrom,
                        src.State,
                        src.StateName,
                        src.RequestTypeName,
                        src.RequestTypeNameFa,
                        src.RequestNO,
                        src.ConditionValue,
                        src.IsLocked,
                        src.LockedUserID,
                        src.Page,
                        src.Conditions?.Select(s => new ConditionApiRequest(s.ConditionValue, s.NextSate, s.ButtonName)).ToArray())
                    ));
}