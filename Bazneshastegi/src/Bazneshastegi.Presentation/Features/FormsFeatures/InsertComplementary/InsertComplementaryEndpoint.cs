using Bazneshastegi.Application.Features.FormsFeature.InsertComplementaryFeature;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.InsertComplementaryContract;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.InsertComplementary;
sealed class InsertComplementaryEndpoint : EndpointHandlerBase<
    InsertComplementaryApiRequest,
    InsertComplementaryCommand,
    InsertComplementaryCommandResponse,
    InsertComplementaryApiResponse>
{
    protected override bool NeedAuthentication => false;
    protected override bool NeedTaxPayerFile => false;

    public InsertComplementaryEndpoint(
        IPresentationMapper<InsertComplementaryApiRequest, InsertComplementaryCommand> apiRequestMapper
        ) : base(
            Endpoints.Forms.InsertComplementary,
            HttpMethod.Post,
            apiRequestMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }
}
internal sealed class InsertComplementaryApiRequestMapper : IPresentationMapper<InsertComplementaryApiRequest, InsertComplementaryCommand>
{
    public ValueTask<PrimitiveResult<InsertComplementaryCommand>> Map(InsertComplementaryApiRequest src, CancellationToken cancellationToken) =>
        ValueTask.FromResult(
            PrimitiveResult.Success(
                new InsertComplementaryCommand(
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
                    src.PersonID,
                    src.RequestTypeID,
                    src.RequestID,
                    src.ServiceCost,
                    src.DeliveryCost)));
}
sealed class InsertComplementaryApiResponseMapper : IPresentationMapper<
    InsertComplementaryCommandResponse,
    InsertComplementaryApiResponse>
{
    public ValueTask<PrimitiveResult<InsertComplementaryApiResponse>> Map(
        InsertComplementaryCommandResponse src,
        CancellationToken cancellationToken) => ValueTask.FromResult(
            PrimitiveResult.Success(
                    new InsertComplementaryApiResponse(
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
                        src.PersonID,
                        src.RequestTypeID,
                        src.RequestID)));
}