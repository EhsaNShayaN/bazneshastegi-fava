using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Application.Services.UserContextAccessorServices;
using SRH.MediatRMessaging.Queries;

namespace Bazneshastegi.Application.Features.FormsFeature.CalculateMedicalTreatmentCostFeature;

public sealed record CalculateMedicalTreatmentCostQuery(
    string? ServiceTypeLookupID,
    string? RelatedPersonID,
    string? DeliveryType) : IPrimitiveResultQuery<ProviderCalculateMedicalTreatmentCostResponse[]>;

sealed class CalculateMedicalTreatmentCostQueryHandler : IPrimitiveResultQueryHandler<CalculateMedicalTreatmentCostQuery, ProviderCalculateMedicalTreatmentCostResponse[]>
{
    private readonly IBazneshastegiService _bazneshastegiService;
    private readonly IUserContextAccessor _userContextAccessor;

    public CalculateMedicalTreatmentCostQueryHandler(
        IBazneshastegiService bazneshastegiService,
        IUserContextAccessor userContextAccessor)
    {
        this._bazneshastegiService = bazneshastegiService;
        this._userContextAccessor = userContextAccessor;
    }
    public async Task<PrimitiveResult<ProviderCalculateMedicalTreatmentCostResponse[]>> Handle(CalculateMedicalTreatmentCostQuery request, CancellationToken cancellationToken) =>
        await this._bazneshastegiService.CalculateMedicalTreatmentCost(
            _userContextAccessor.CurrentPersonId,
            request.ServiceTypeLookupID,
            request.RelatedPersonID,
            request.DeliveryType,
            cancellationToken)
        .ConfigureAwait(false);
}