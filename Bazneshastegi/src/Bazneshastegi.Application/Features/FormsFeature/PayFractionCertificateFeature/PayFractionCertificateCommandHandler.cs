using Bazneshastegi.Application.Services.Provider;

namespace Bazneshastegi.Application.Features.FormsFeature.PayFractionCertificateFeature;
public sealed class PayFractionCertificateCommandHandler : IPrimitiveResultCommandHandler<PayFractionCertificateCommand, PayFractionCertificateCommandResponse>
{
    private readonly IBazneshastegiService _bazneshastegiService;

    public PayFractionCertificateCommandHandler(IBazneshastegiService bazneshastegiService)
    {
        this._bazneshastegiService = bazneshastegiService;
    }

    public async Task<PrimitiveResult<PayFractionCertificateCommandResponse>> Handle(PayFractionCertificateCommand request, CancellationToken cancellationToken)
    {
        await this._bazneshastegiService.InsertRequest(
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
            .Map(s => this._bazneshastegiService.InsertComplementaryRequest(
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
                    s.RequestID,
                    null,
                    null),
                cancellationToken))
            .Map(s => PrimitiveResult.BindAll(request.Files, (tuple, itemIndex) =>
                this._bazneshastegiService.InsertRequestAttachment(
                new ProviderInsertRequestAttachmentRequest(
                request.RequestAttachmentID,
                request.RequestID,
                request.AttachementTypeID,
                request.AttachementTypeName,
                request.AttachementDesc,
                tuple.Item1,
                tuple.Item2,
                request.InsertUserID,
                request.InsertTime,
                request.UpdateUserID,
                request.UpdateTime),
                cancellationToken),
                BindAllIterationStrategy.GoToLast))
            .Map(s => this._bazneshastegiService.SendRequestToNextStateRequest(new ProviderSendRequestToNextStateRequest(
                request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID), cancellationToken))
            .ConfigureAwait(false);

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
            .Map(s => new PayFractionCertificateCommandResponse(
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
