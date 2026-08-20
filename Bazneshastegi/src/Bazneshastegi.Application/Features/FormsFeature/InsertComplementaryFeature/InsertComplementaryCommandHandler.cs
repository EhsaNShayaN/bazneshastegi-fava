using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.InsertComplementaryFeature;
public sealed class InsertComplementaryCommandHandler : IPrimitiveResultCommandHandler<InsertComplementaryCommand, InsertComplementaryCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public InsertComplementaryCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<InsertComplementaryCommandResponse>> Handle(InsertComplementaryCommand request, CancellationToken cancellationToken)
    {
        return await this._bazneshastegiService.InsertComplementaryRequest(
            new ProviderInsertComplementaryRequest(
                request.RequestComplementaryID,
                request.InsertPayAmountInCertificate,
                request.InsertDurationInCertificate,
                request.ApplicantNationalCode,
                request.ApplicantBirthDate,
                request.ApplicantFirstName,
                request.ApplicantLastName,
                request.ApplicantRelationship,
                request.RelatedPersonID,
                request.PrizeReceiverLookupID,
                request.FacilityGiverLookupID,
                request.FacilityGiverDesc,
                request.FacilityAmount,
                request.FacilityDiscountPercent,
                request.FacilityInstalementCount,
                request.NeedGuarantor,
                request.ReferralToCommittee,
                request.IssueTypeLookupID,
                request.PreviousCardNumber,
                request.FacilityReceiverFullName,
                request.FacilityReceiveTypeLookupID,
                request.CeremonyTypeLookupID,
                request.CeremonyDate,
                request.CeremonyGuestCount,
                request.IntroducedToLookupID,
                request.RequestDescription,
                request.PersonID,
                request.RequestTypeID,
                request.RequestID,
                request.ServiceCost,
                request.DeliveryCost),
            cancellationToken)
            .Map(s => new InsertComplementaryCommandResponse(
                s.RequestComplementaryID,
                s.InsertPayAmountInCertificate,
                s.InsertDurationInCertificate,
                s.ApplicantNationalCode,
                s.ApplicantBirthDate,
                s.ApplicantFirstName,
                s.ApplicantLastName,
                s.ApplicantRelationship,
                s.RelatedPersonID,
                s.PrizeReceiverLookupID,
                s.FacilityGiverLookupID,
                s.FacilityGiverDesc,
                s.FacilityAmount,
                s.FacilityDiscountPercent,
                s.FacilityInstalementCount,
                s.NeedGuarantor,
                s.ReferralToCommittee,
                s.IssueTypeLookupID,
                s.PreviousCardNumber,
                s.FacilityReceiverFullName,
                s.FacilityReceiveTypeLookupID,
                s.CeremonyTypeLookupID,
                s.CeremonyDate,
                s.CeremonyGuestCount,
                s.IntroducedToLookupID,
                s.RequestDescription,
                s.PersonID,
                s.RequestTypeID,
                s.RequestID))
            .ConfigureAwait(false);
    }
}
