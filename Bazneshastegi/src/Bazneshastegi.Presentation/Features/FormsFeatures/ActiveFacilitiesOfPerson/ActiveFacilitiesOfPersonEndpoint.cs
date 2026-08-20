using Bazneshastegi.Application.Features.FormsFeature.ActiveFacilitiesOfPersonFeature;
using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Contracts;
using Bazneshastegi.Contracts.FormsContract.ActiveFacilitiesOfPersonContract;
using Bazneshastegi.Domain.Utilities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Bazneshastegi.Presentation.Features.FormsFeatures.ActiveFacilitiesOfPerson;

sealed class ActiveFacilitiesOfPersonEndpoint : EndpointHandlerBase<
    ActiveFacilitiesOfPersonApiRequest,
    ActiveFacilitiesOfPersonQuery,
    ProviderActiveFacilitiesOfPersonResponse[],
    ActiveFacilitiesOfPersonApiResponse[]>
{
    protected override bool NeedTaxPayerFile => true;

    public ActiveFacilitiesOfPersonEndpoint(
        IPresentationMapper<ActiveFacilitiesOfPersonApiRequest, ActiveFacilitiesOfPersonQuery> requestMapper,
        IPresentationMapper<ProviderActiveFacilitiesOfPersonResponse[], ActiveFacilitiesOfPersonApiResponse[]> responseMapper)
        : base(
            Endpoints.Forms.ActiveFacilitiesOfPerson,
            HttpMethod.Get,
            requestMapper,
            responseMapper,
            DefaultResponseFactory.Instance.CreateOk)
    { }

    protected override Delegate EndpointDelegate =>
    (
            [AsParameters] ActiveFacilitiesOfPersonApiRequest request,
            ISender sender,
            CancellationToken cancellationToken) => this.CallMediatRHandler(
            sender,
            () => ValueTask.FromResult(PrimitiveResult.Success(new ActiveFacilitiesOfPersonQuery(request.RequestTypeID))),
            cancellationToken);

}
sealed class GetActiveFacilitiesOfPersonApiRequestMapper : IPresentationMapper<
    ActiveFacilitiesOfPersonApiRequest,
    ActiveFacilitiesOfPersonQuery>
{
    public ValueTask<PrimitiveResult<ActiveFacilitiesOfPersonQuery>> Map(
        ActiveFacilitiesOfPersonApiRequest src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(PrimitiveResult.Success(new ActiveFacilitiesOfPersonQuery(src.RequestTypeID)));
    }
}
sealed class GetActiveFacilitiesOfPersonApiResponseMapper : IPresentationMapper<
    ProviderActiveFacilitiesOfPersonResponse[],
    ActiveFacilitiesOfPersonApiResponse[]>
{
    public ValueTask<PrimitiveResult<ActiveFacilitiesOfPersonApiResponse[]>> Map(
        ProviderActiveFacilitiesOfPersonResponse[] src,
        CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(
            PrimitiveResult.Success(
                src.Select(data => new ActiveFacilitiesOfPersonApiResponse(
                    data.FacilityID,
                    data.PersonID,
                    data.MainpersonFirstName,
                    data.MainpersonLastName,
                    data.RequestTypeID,
                    data.RelatedPersonID,
                    data.RelatedpersonFirstName,
                    data.RelatedpersonLastName,
                    data.PayItemTypeID,
                    data.ConfirmDate?.ToPersianDate(),
                    data.FacilityAmount,
                    data.InstalementAmount,
                    data.InstalementCount,
                    data.RemainedAmount,
                    data.ExecuteYear,
                    data.ExecuteMonth,
                    data.ExpireDate,
                    data.ExpireUserID,
                    data.RequestID,
                    data.RequestComplementaryID,
                    data.InsertPayAmountInCertificate,
                    data.InsertDurationInCertificate,
                    data.ApplicantNationalCode,
                    data.ApplicantBirthDate,
                    data.ApplicantFirstName,
                    data.ApplicantLastName,
                    data.ApplicantRelationship,
                    data.PrizeReceiverLookupID,
                    data.FacilityGiverLookupID,
                    data.FacilityGiverDesc,
                    data.NeedGuarantor,
                    data.ReferralToCommittee,
                    data.IssueTypeLookupID,
                    data.PreviousCardNumber,
                    data.FacilityReceiverFullName,
                    data.FacilityReceiveTypeLookupID,
                    data.CeremonyTypeLookupID,
                    data.CeremonyDate,
                    data.CeremonyGuestCount,
                    data.IntroducedToLookupID,
                    data.RequestDescription,
                    data.RequestTypeName,
                    data.PrizeReceiverLookupName,
                    data.FacilityGiverLookupName,
                    data.IssueTypeLookupName,
                    data.FacilityReceiveTypeLookupName,
                    data.CeremonyTypeLookupName,
                    data.IntroducedToLookupName,
                    data.LastInstalementDate,
                    data.BasketRecieveTypeID,
                    data.BasketRecieveTypeName)
                ).ToArray()));
    }
}