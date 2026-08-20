using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertFeature;
public sealed class InsertCommandHandler : IPrimitiveResultCommandHandler<InsertCommand, InsertCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertCommandResponse>> Handle(InsertCommand request, CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertRequest(
            new ProviderInsert(
                request.RequestID,
                request.PersonID,
                request.NationalCode,
                request.PersonFirstName,
                request.PersonLastName,
                request.RequestDate,
                request.RequestTypeID,
                request.RequestText,
                request.InsertUserID,
                request.UpdateUserID,
                request.RequestFrom,
                request.State,
                request.StateName,
                request.RequestTypeName,
                request.RequestTypeNameFa,
                request.RequestNO,
                request.ConditionValue,
                request.IsLocked,
                request.LockedUserID,
                request.Page,
                request.Conditions), cancellationToken)
            .Map(s => new InsertCommandResponse(
                s.RequestID,
                s.PersonID,
                s.NationalCode,
                s.PersonFirstName,
                s.PersonLastName,
                s.RequestDate,
                s.RequestTypeID,
                s.RequestText,
                s.InsertUserID,
                s.UpdateUserID,
                s.RequestFrom,
                s.State,
                s.StateName,
                s.RequestTypeName,
                s.RequestTypeNameFa,
                s.RequestNO,
                s.ConditionValue,
                s.IsLocked,
                s.LockedUserID,
                s.Page,
                s.Conditions))
            .ConfigureAwait(false);
    }
}
