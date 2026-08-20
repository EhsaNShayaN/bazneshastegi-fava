using Bazneshastegi.Contracts.FormsContract.InsertContract;
using Bazneshastegi.Contracts.Helpers;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Bazneshastegi.Contracts.FormsContract;

public readonly record struct PayFractionCertificateApiRequest(
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
    ConditionApiRequest[] Conditions,
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
    decimal FacilityAmount,
    decimal FacilityDiscountPercent,
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
    IFormFile[] Files)
{
    public static async ValueTask<PayFractionCertificateApiRequest?> BindAsync(HttpContext context, ParameterInfo _)
    {
        var form = await context.Request.ReadFormAsync(context.RequestAborted).ConfigureAwait(false);
        return FormBinderHelper.Bind<PayFractionCertificateApiRequest>(form);
    }
}