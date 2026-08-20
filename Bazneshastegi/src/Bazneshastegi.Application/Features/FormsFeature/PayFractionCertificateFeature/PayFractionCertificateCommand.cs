using Bazneshastegi.Domain.Shared;
using SRH.MediatRMessaging;

namespace Bazneshastegi.Application.Features.FormsFeature.PayFractionCertificateFeature;

public sealed record class PayFractionCertificateCommand(
    ////////////////Insert
    string RequestID,
    string PersonID,
    string NationalCode,
    string PersonFirstName,
    string PersonLastName,
    DateTime? RequestDate,
    string RequestTypeID,
    string RequestText,
    string InsertUserID,
    string UpdateUserID,
    int? RequestFrom,
    int? State,
    string StateName,
    string RequestTypeName,
    string RequestTypeNameFa,
    string RequestNO,
    int? ConditionValue,
    bool IsLocked,
    string LockedUserID,
    string Page,
    ConditionModel[] Conditions,
    ////////////////InsertComplementary
    string RequestComplementaryID,
    bool InsertPayAmountInCertificate,
    bool InsertDurationInCertificate,
    string ApplicantNationalCode,
    DateTime? ApplicantBirthDate,
    string ApplicantFirstName,
    string ApplicantLastName,
    string ApplicantRelationship,
    string RelatedPersonID,
    string PrizeReceiverLookupID,
    string FacilityGiverLookupID,
    string FacilityGiverDesc,
    decimal? FacilityAmount,
    decimal? FacilityDiscountPercent,
    int? FacilityInstalementCount,
    bool NeedGuarantor,
    bool ReferralToCommittee,
    string IssueTypeLookupID,
    string PreviousCardNumber,
    string FacilityReceiverFullName,
    string FacilityReceiveTypeLookupID,
    string CeremonyTypeLookupID,
    DateTime? CeremonyDate,
    int? CeremonyGuestCount,
    string IntroducedToLookupID,
    string RequestDescription,
    ////////////////InsertRequestAttachment
    string RequestAttachmentID,
    string AttachementTypeID,
    string AttachementTypeName,
    string AttachementDesc,
    DateTime? InsertTime,
    DateTime? UpdateTime,
    List<Tuple<string, string>> Files) : IPrimitiveResultCommand<PayFractionCertificateCommandResponse>,
    IValidatableRequest<PayFractionCertificateCommand>
{
    public ValueTask<PrimitiveResult<PayFractionCertificateCommand>> Validate() => PrimitiveResult.Success(this)
            .Ensure([
                value => PrimitiveResult.Success(this.RequestID)
                .Match(
                    _ => PrimitiveResult.Success() ,
                    _ => PrimitiveResult.Failure("Validation.Error", "موبایل ارسالی نامعتبر است"))
                ]);
}