using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.GetRequestTypeConfigFeature;

public sealed record GetRequestTypeConfigQuery(
    string RequestTypeID,
    string LookupID,
    string FacilityReceiverRelationshipID,
    string PensionaryStatusCategory,
    string GenderLookupID,
    string FacilityReceiverPersonID) : IPrimitiveResultQuery<ProviderGetRequestTypeConfigResponse[]>;

sealed class GetRequestTypeConfigQueryHandler : IPrimitiveResultQueryHandler<GetRequestTypeConfigQuery, ProviderGetRequestTypeConfigResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;
    private readonly IUserContextAccessor _userContextAccessor;

    public GetRequestTypeConfigQueryHandler(
        IBazneshastegiService bazneshastegiService,
        IUserContextAccessor userContextAccessor)
    {
        this._bazneshastegiService = bazneshastegiService;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<ProviderGetRequestTypeConfigResponse[]>> Handle(GetRequestTypeConfigQuery request, CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.GetRequestTypeConfig(
            request.RequestTypeID,
            request.LookupID,
            request.FacilityReceiverRelationshipID,
            request.PensionaryStatusCategory,
            request.GenderLookupID,
            request.FacilityReceiverPersonID == "-1" ? _userContextAccessor.CurrentPersonId : request.FacilityReceiverPersonID,
            cancellationToken)
        .ConfigureAwait(false);
    }
}