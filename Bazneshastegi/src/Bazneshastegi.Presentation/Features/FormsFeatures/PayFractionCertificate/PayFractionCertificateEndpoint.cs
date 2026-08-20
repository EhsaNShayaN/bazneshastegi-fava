using Bazneshastegi.Application.Features.FormsFeature.PayFractionCertificateFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract;
using Bazneshastegi.Contracts.FormsContract.InsertContract;
using Bazneshastegi.Domain.Shared;
using Bazneshastegi.Presentation.Helpers;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.PayFractionCertificate;

sealed class PayFractionCertificateEndpoint : EndpointHandlerBase<
    PayFractionCertificateApiRequest,
    PayFractionCertificateCommand,
    PayFractionCertificateCommandResponse,
    PayFractionCertificateApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public PayFractionCertificateEndpoint(
        IPresentationMapper<PayFractionCertificateApiRequest, PayFractionCertificateCommand> requestMapper,
        IPresentationMapper<PayFractionCertificateCommandResponse, PayFractionCertificateApiResponse> responseMapper
        ) : base(
            Endpoints.Forms.PayFractionCertificate,
            HttpMethod.Post,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class PayFractionCertificateApiRequestMapper : IPresentationMapper<PayFractionCertificateApiRequest, PayFractionCertificateCommand>
{
    public async ValueTask<PrimitiveResult<PayFractionCertificateCommand>> Map(PayFractionCertificateApiRequest src, CancellationToken cancellationToken)
    {
        List<Tuple<string, string>> tuples = [];
        foreach (var file in src.Files)
        {
            var (Attachment, ContentType, HasInvalidFormat) = await FileService.ConvertToBase64Async(file);
            if (HasInvalidFormat) return await ValueTask.FromResult(PrimitiveResult.Failure<PayFractionCertificateCommand>("", "فرمت فایل نامعتبر است."));
            tuples.Add(new Tuple<string, string>(Attachment, ContentType));
        }

        return await ValueTask.FromResult(
               PrimitiveResult.Success(
                   new PayFractionCertificateCommand(
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
                             condition.ButtonName
                         ))?.ToArray(),
                        ///////////////////
                        src.RequestComplementaryID,
                        src.InsertPayAmountInCertificate,
                        src.InsertDurationInCertificate,
                        src.ApplicantNationalCode,
                        src.ApplicantBirthDate,
                        src.ApplicantFirstName,
                        src.ApplicantLastName,
                        src.ApplicantRelationship,
                        src.RelatedPersonID,
                        src.PrizeReceiverLookupID,
                        src.FacilityGiverLookupID,
                        src.FacilityGiverDesc,
                        src.FacilityAmount,
                        src.FacilityDiscountPercent,
                        src.FacilityInstalementCount,
                        src.NeedGuarantor,
                        src.ReferralToCommittee,
                        src.IssueTypeLookupID,
                        src.PreviousCardNumber,
                        src.FacilityReceiverFullName,
                        src.FacilityReceiveTypeLookupID,
                        src.CeremonyTypeLookupID,
                        src.CeremonyDate,
                        src.CeremonyGuestCount,
                        src.IntroducedToLookupID,
                        src.RequestDescription,
                        ///////////////////
                        src.RequestAttachmentID,
                        src.AttachementTypeID,
                        src.AttachementTypeName,
                        src.AttachementDesc,
                        src.InsertTime,
                        src.UpdateTime,
                        tuples)));
    }
}
sealed class PayFractionCertificateApiResponseMapper : IPresentationMapper<
    PayFractionCertificateCommandResponse,
    PayFractionCertificateApiResponse>
{
    public ValueTask<PrimitiveResult<PayFractionCertificateApiResponse>> Map(
        PayFractionCertificateCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new PayFractionCertificateApiResponse(
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