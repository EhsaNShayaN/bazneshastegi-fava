namespace Bazneshastegi.Application.Services.Provider;
public interface IBazneshastegiLoginService
{
    ValueTask<PrimitiveResult<ProviderLoginResponse>> Login(string username, string password, CancellationToken cancellationToken);
}
public interface IBazneshastegiService : IBazneshastegiLoginService
{
    ValueTask<PrimitiveResult<ProviderPersonInfoResponse>> GetPersonInfo(string personID, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderLoginForPortalProxyResponse>> LoginForPortal(string nationalCode, string cellPhone, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderRequestTypeResponse[]>> GetRequestTypes(CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderLookupDataResponse[]>> GetLookupData(string lookupType, string lookUpParentID, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderRelationshipResponse[]>> GetRelationship(string RelationshipID, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertResponse>> InsertRequest(ProviderInsert request, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertComplementaryResponse>> InsertComplementaryRequest(ProviderInsertComplementaryRequest request, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderSendRequestToNextStateResponse>> SendRequestToNextStateRequest(ProviderSendRequestToNextStateRequest request, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderRelatedPersonsResponse[]>> GetRelatedListByParentPersonId(string personID, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderRelatedListForPortalResponse[]>> GetRelatedListForPortal(string requestTypeID, string personID, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderNewPersonByParentIdResponse[]>> GetNewPersonByParentId(string loginedPersonId, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderRequestTypeAttachmentResponse[]>> GetRequestTypeAttachment(string requestTypeID, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertRequestAttachmentResponse>> InsertRequestAttachment(ProviderInsertRequestAttachmentRequest request, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderActiveFacilitiesOfPersonResponse[]>> GetActiveFacilitiesOfPerson(string personID, string requestTypeID, CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderGetRequestTypeConfigResponse[]>> GetRequestTypeConfig(
        string requestTypeID,
        string lookupID,
        string facilityReceiverRelationshipID,
        string pensionaryStatusCategory,
        string genderLookupID,
        string facilityReceiverPersonID,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderSetInstalementAmountResponse[]>> SetInstalementAmount(
        string requestTypeID,
        decimal defaultAmount,
        decimal defaultInstalementCount,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderGetRequestTypeGuideResponse[]>> GetRequestTypeGuide(
        string requestTypeID,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderGetLookupResponse[]>> GetLookup(
        string? lookupType,
        string? lookupName,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertComplementary_WorkDisability_BurialResponse>> InsertRequestComplementary_WorkDisability_Burial(
            ProviderInsertComplementary_WorkDisability_BurialRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertComplementary_PhysicalDisability_IllnessResponse>> InsertRequestComplementary_PhysicalDisability_Illness(
            ProviderInsertComplementary_PhysicalDisability_IllnessRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertComplementary_EducationalPlace_SportsVenueResponse>> InsertRequestComplementary_EducationalPlace_SportsVenue(
            ProviderInsertComplementary_EducationalPlace_SportsVenueRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertComplementary_NursingExpensesResponse>> InsertRequestComplementary_NursingExpenses(
            ProviderInsertComplementary_NursingExpensesRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertComplementary_MarriageLoanResponse>> InsertRequestComplementary_MarriageLoan(
            ProviderInsertComplementary_MarriageLoanRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertComplementary_MarriageAidResponse>> InsertRequestComplementary_MarriageAid(
            ProviderInsertComplementary_MarriageAidRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertComplementary_ImprestResponse>> InsertRequestComplementary_Imprest(
            ProviderInsertComplementary_ImprestRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertRequestForEditBasketReceiveTypeRetiredResponse>> InsertRequestForEditBasketReceiveTypeRetired(
            ProviderInsertRequestForEditBasketReceiveTypeRetiredRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertRequestForEditBasketReceiveTypeHeirResponse>> InsertRequestForEditBasketReceiveTypeHeir(
            ProviderInsertRequestForEditBasketReceiveTypeHeirRequest request,
            CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertNewPersonResponse>> InsertNewPerson(
        ProviderInsertNewPersonRequest request,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderUpdateNewPersonResponse>> UpdateNewPerson(
        ProviderUpdateNewPersonRequest request,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderInsertRequestForEditPersonInfoResponse>> InsertRequestForEditPersonInfo(
        ProviderInsertRequestForEditPersonInfoRequest request,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderGetTempPersonResponse[]>> GetTempPerson(
        string? personID,
        string? requestID,
        CancellationToken cancellationToken);
    ValueTask<PrimitiveResult<ProviderCalculateMedicalTreatmentCostResponse[]>> CalculateMedicalTreatmentCost(
        string mainPersonID,
        string? serviceTypeLookupID,
        string? relatedPersonID,
        string? deliveryType,
        CancellationToken cancellationToken);
}