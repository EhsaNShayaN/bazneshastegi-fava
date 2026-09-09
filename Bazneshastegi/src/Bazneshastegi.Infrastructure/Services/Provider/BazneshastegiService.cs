using Bazneshastegi.Application.Services.Provider;
using Bazneshastegi.Domain.Utilities;
using Bazneshastegi.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;

namespace Bazneshastegi.Infrastructure.Services.Provider;

public sealed class BazneshastegiService : IBazneshastegiService
{
    public const string HttpClientName = "Bazneshastegi";
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<BazneshastegiService> _logger;

    public BazneshastegiService(IHttpClientFactory httpClientFactory, ILogger<BazneshastegiService> logger)
    {
        this._httpClientFactory = httpClientFactory;
        this._logger = logger;
    }
    public ValueTask<PrimitiveResult<ProviderLoginResponse>> Login(string username, string password, CancellationToken cancellationToken)
    {
        var endpoint = "api/User/Login";

        return HttpProxyHelper.PostBazneshastegiRequest<ProviderLoginRequest, ProviderLoginResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderLoginRequest(username, password),
            cancellationToken)
            .MapIf(result => result.Status && result.ItemList?.Length > 0,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderLoginResponse>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderPersonInfoResponse>> GetPersonInfo(string personID, CancellationToken cancellationToken)
    {
        var endpoint = $"api/Facility/GetPersonInfo?personID={personID}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderPersonInfoRequest, ProviderPersonInfoResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderPersonInfoRequest(personID),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderPersonInfoResponse>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderLoginForPortalProxyResponse>> LoginForPortal(string nationalCode, string cellPhone, CancellationToken cancellationToken)
    {
        var endpoint = $"api/Facility/LoginForPortal?nationalCode={nationalCode}&cellPhone=0{cellPhone.ToMobile()}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderLoginForPortalRequest, ProviderLoginForPortalProxyResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderLoginForPortalRequest(nationalCode, cellPhone),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderLoginForPortalProxyResponse>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderRequestTypeResponse[]>> GetRequestTypes(CancellationToken cancellationToken)
    {
        var requestForm = 2;
        var endpoint = $"api/Request/GetRequestType?requestFrom={requestForm}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderRequestTypeRequest, ProviderRequestTypeResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderRequestTypeRequest(requestForm),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.ToArray())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderRequestTypeResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderLookupDataResponse[]>> GetLookupData(string lookupType, string lookUpParentId, CancellationToken cancellationToken)
    {
        var endpoint = $"api/Shared/GetLookupData?lookUpType={lookupType}";
        if (!string.IsNullOrWhiteSpace(lookUpParentId)) endpoint += $"&lookUpParentID={lookUpParentId}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderLookupDataRequest, ProviderLookupDataResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderLookupDataRequest(lookupType, lookUpParentId),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.ToArray())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderLookupDataResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderRelationshipResponse[]>> GetRelationship(string RelationshipID, CancellationToken cancellationToken)
    {
        var endpoint = $"api/Shared/GetRelationship";
        if (!string.IsNullOrWhiteSpace(RelationshipID)) endpoint += $"&RelationshipID={RelationshipID}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderRelationshipRequest, ProviderRelationshipResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderRelationshipRequest(RelationshipID),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.ToArray())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderRelationshipResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderInsertResponse>> InsertRequest(ProviderInsert request, CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequest";

        return await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsert, ProviderInsertResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertResponse>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderInsertComplementaryResponse>> InsertComplementaryRequest(ProviderInsertComplementaryRequest request, CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestComplementary";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertComplementaryRequest, ProviderInsertComplementaryResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertComplementaryResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderSendRequestToNextStateResponse>> SendRequestToNextStateRequest(ProviderSendRequestToNextStateRequest request, CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/SendRequestToNextState";

        return await HttpProxyHelper.PostBazneshastegiRequest<ProviderSendRequestToNextStateRequest, ProviderSendRequestToNextStateResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderSendRequestToNextStateResponse>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderRelatedPersonsResponse[]>> GetRelatedListByParentPersonId(string personID, CancellationToken cancellationToken)
    {
        var endpoint = $"api/Related/GetRelatedListByParentPersonID?parentPersonID={personID}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderRelatedPersonsRequest, ProviderRelatedPersonsResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderRelatedPersonsRequest(personID),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderRelatedPersonsResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderRelatedListForPortalResponse[]>> GetRelatedListForPortal(
        string requestTypeID,
        string parentPersonID,
        CancellationToken cancellationToken)
    {
        var endpoint = $"api/Facility/GetRelatedListForPortal?requestTypeID={requestTypeID}&parentPersonID={parentPersonID}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderRelatedListForPortalRequest, ProviderRelatedListForPortalResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderRelatedListForPortalRequest(requestTypeID, parentPersonID),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderRelatedListForPortalResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderNewPersonByParentIdResponse[]>> GetNewPersonByParentId(string loginedPersonId, CancellationToken cancellationToken)
    {
        var endpoint = $"api/TempPerson/GetNewPersonByParentId?loginedPersonID={loginedPersonId}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderNewPersonByParentIdRequest, ProviderNewPersonByParentIdResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderNewPersonByParentIdRequest(loginedPersonId),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderNewPersonByParentIdResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderRequestTypeAttachmentResponse[]>> GetRequestTypeAttachment(string requestTypeID, CancellationToken cancellationToken)
    {
        var endpoint = $"api/Request/GetRequestTypeAttachment?requestTypeID={requestTypeID}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderRequestTypeAttachmentRequest, ProviderRequestTypeAttachmentResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderRequestTypeAttachmentRequest(requestTypeID),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderRequestTypeAttachmentResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderInsertRequestAttachmentResponse>> InsertRequestAttachment(ProviderInsertRequestAttachmentRequest request, CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestAttachment";

        return await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertRequestAttachmentRequest, ProviderInsertRequestAttachmentResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertRequestAttachmentResponse>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderGetRequestTypeConfigResponse[]>> GetRequestTypeConfig(
        string requestTypeID,
        string lookupID,
        string facilityReceiverRelationshipID,
        string pensionaryStatusCategory,
        string genderLookupID,
        string facilityReceiverPersonID,
        CancellationToken cancellationToken)
    {
        var endpoint = $"api/Request/GetRequestTypeConfig?requestTypeID={requestTypeID}";
        if (!string.IsNullOrWhiteSpace(lookupID))
        {
            endpoint += $"&lookupID={lookupID}";
        }
        if (!string.IsNullOrWhiteSpace(facilityReceiverRelationshipID))
        {
            endpoint += $"&facilityReceiverRelationshipID={facilityReceiverRelationshipID}";
        }
        if (!string.IsNullOrWhiteSpace(pensionaryStatusCategory))
        {
            endpoint += $"&pensionaryStatusCategory={pensionaryStatusCategory}";
        }
        if (!string.IsNullOrWhiteSpace(genderLookupID))
        {
            endpoint += $"&genderLookupID={genderLookupID}";
        }
        if (!string.IsNullOrWhiteSpace(facilityReceiverPersonID))
        {
            endpoint += $"&facilityReceiverPersonID={facilityReceiverPersonID}";
        }

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderGetRequestTypeConfigRequest, ProviderGetRequestTypeConfigResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderGetRequestTypeConfigRequest(requestTypeID, lookupID),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderGetRequestTypeConfigResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderSetInstalementAmountResponse[]>> SetInstalementAmount(
        string requestTypeID,
        decimal defaultAmount,
        decimal defaultInstalementCount,
        CancellationToken cancellationToken)
    {
        var endpoint = $"api/Request/SetInstalementAmount?requestTypeID={requestTypeID}&defaultAmount={defaultAmount}&defaultInstalementCount={defaultInstalementCount}";
        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderSetInstalementAmountRequest, ProviderSetInstalementAmountResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderSetInstalementAmountRequest(requestTypeID, defaultAmount, defaultInstalementCount),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderSetInstalementAmountResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderActiveFacilitiesOfPersonResponse[]>> GetActiveFacilitiesOfPerson(
        string personID,
        string requestTypeID,
        CancellationToken cancellationToken)
    {
        var endpoint = $"api/Facility/GetActiveFacilitiesOfPerson?personID={personID}&requestTypeID={requestTypeID}&showNotConfirmedRequestsToo=false&showExpiredFacilitiesToo=false";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderActiveFacilitiesOfPersonRequest, ProviderActiveFacilitiesOfPersonResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderActiveFacilitiesOfPersonRequest(personID, requestTypeID),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderActiveFacilitiesOfPersonResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderGetRequestTypeGuideResponse[]>> GetRequestTypeGuide(
        string requestTypeID,
        CancellationToken cancellationToken)
    {
        var endpoint = $"api/Request/GetRequestTypeGuide?requestTypeID={requestTypeID}";

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderGetRequestTypeGuideRequest, ProviderGetRequestTypeGuideResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderGetRequestTypeGuideRequest(requestTypeID),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderGetRequestTypeGuideResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderGetLookupResponse[]>> GetLookup(
        string? lookupType,
        string? lookupName,
        CancellationToken cancellationToken)
    {
        var endpoint = $"api/Lookup/GetLookup?lookupType={lookupType}";
        if (!string.IsNullOrWhiteSpace(lookupName))
        {
            endpoint += $"&lookupName={lookupName}";
        }
        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderGetLookupRequest, ProviderGetLookupResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderGetLookupRequest(lookupType, lookupName),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderGetLookupResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderInsertComplementary_WorkDisability_BurialResponse>> InsertRequestComplementary_WorkDisability_Burial(
        ProviderInsertComplementary_WorkDisability_BurialRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestComplementary_" + request.MethodName;

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertComplementary_WorkDisability_BurialRequest, ProviderInsertComplementary_WorkDisability_BurialResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertComplementary_WorkDisability_BurialResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertComplementary_PhysicalDisability_IllnessResponse>> InsertRequestComplementary_PhysicalDisability_Illness(
        ProviderInsertComplementary_PhysicalDisability_IllnessRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestComplementary_" + request.MethodName;

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertComplementary_PhysicalDisability_IllnessRequest, ProviderInsertComplementary_PhysicalDisability_IllnessResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertComplementary_PhysicalDisability_IllnessResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertComplementary_EducationalPlace_SportsVenueResponse>> InsertRequestComplementary_EducationalPlace_SportsVenue(
        ProviderInsertComplementary_EducationalPlace_SportsVenueRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestComplementary_IntroduceTo" + request.MethodName;

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertComplementary_EducationalPlace_SportsVenueRequest, ProviderInsertComplementary_EducationalPlace_SportsVenueResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertComplementary_EducationalPlace_SportsVenueResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertComplementary_NursingExpensesResponse>> InsertRequestComplementary_NursingExpenses(
        ProviderInsertComplementary_NursingExpensesRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestComplementary_NursingExpenses";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertComplementary_NursingExpensesRequest, ProviderInsertComplementary_NursingExpensesResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertComplementary_NursingExpensesResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertComplementary_MarriageLoanResponse>> InsertRequestComplementary_MarriageLoan(
        ProviderInsertComplementary_MarriageLoanRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestComplementary_MarriageLoan";

        var res = await HttpProxyHelper
            .PostBazneshastegiRequest<ProviderInsertComplementary_MarriageLoanRequest, ProviderInsertComplementary_MarriageLoanResponse>(
                () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
                endpoint,
                request,
                cancellationToken)
            .MapIf(result => result.Status,
            result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
            result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertComplementary_MarriageLoanResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertComplementary_MarriageAidResponse>> InsertRequestComplementary_MarriageAid(
        ProviderInsertComplementary_MarriageAidRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestComplementary_MarriageAid";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertComplementary_MarriageAidRequest, ProviderInsertComplementary_MarriageAidResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertComplementary_MarriageAidResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertComplementary_ImprestResponse>> InsertRequestComplementary_Imprest(
        ProviderInsertComplementary_ImprestRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/Request/InsertRequestComplementary_Imprest";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertComplementary_ImprestRequest, ProviderInsertComplementary_ImprestResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertComplementary_ImprestResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertRequestForEditBasketReceiveTypeRetiredResponse>> InsertRequestForEditBasketReceiveTypeRetired(
        ProviderInsertRequestForEditBasketReceiveTypeRetiredRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/tempPerson/InsertRequestForEditBasketReceiveTypeRetired";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertRequestForEditBasketReceiveTypeRetiredRequest, ProviderInsertRequestForEditBasketReceiveTypeRetiredResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertRequestForEditBasketReceiveTypeRetiredResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "RetiredPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertRequestForEditBasketReceiveTypeHeirResponse>> InsertRequestForEditBasketReceiveTypeHeir(
        ProviderInsertRequestForEditBasketReceiveTypeHeirRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/tempPerson/InsertRequestForEditBasketReceiveTypeHeir";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertRequestForEditBasketReceiveTypeHeirRequest, ProviderInsertRequestForEditBasketReceiveTypeHeirResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertRequestForEditBasketReceiveTypeHeirResponse>("", result.Error))
            );
        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "HeirPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertNewPersonResponse>> InsertNewPerson(
        ProviderInsertNewPersonRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/tempPerson/InsertNewPerson";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertNewPersonRequest, ProviderInsertNewPersonResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertNewPersonResponse>("", result.Error))
            );

        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "InsertNewPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderUpdateNewPersonResponse>> UpdateNewPerson(
        ProviderUpdateNewPersonRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/tempPerson/UpdateNewPerson";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderUpdateNewPersonRequest, ProviderUpdateNewPersonResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderUpdateNewPersonResponse>("", result.Error))
            );

        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "UpdateNewPerson",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderInsertRequestForEditPersonInfoResponse>> InsertRequestForEditPersonInfo(
        ProviderInsertRequestForEditPersonInfoRequest request,
        CancellationToken cancellationToken)
    {
        var x = JsonHelpers.Serialize(request);
        var endpoint = "api/tempPerson/InsertRequestForEditPersonInfo";

        var res = await HttpProxyHelper.PostBazneshastegiRequest<ProviderInsertRequestForEditPersonInfoRequest, ProviderInsertRequestForEditPersonInfoResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            request,
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList.FirstOrDefault())),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderInsertRequestForEditPersonInfoResponse>("", result.Error))
            );

        var z = await this.SendRequestToNextStateRequest(
               new ProviderSendRequestToNextStateRequest(
                   request.RequestID,
                   1,
                   string.Empty,
                   "InsertRequestForEditPersonInfo",
                   //string.Empty,
                   request.RequestTypeID),
               cancellationToken);
        return res;
    }
    public async ValueTask<PrimitiveResult<ProviderGetTempPersonResponse[]>> GetTempPerson(
        string? personID,
        string? requestID,
        CancellationToken cancellationToken)
    {
        var isNewPerson = true;
        var endpoint = $"api/tempPerson/getTempPerson?loginedPersonID={personID}&isNewPerson={isNewPerson}";
        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderGetTempPersonRequest, ProviderGetTempPersonResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderGetTempPersonRequest(personID, isNewPerson),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderGetTempPersonResponse[]>("", result.Error))
            );
    }
    public async ValueTask<PrimitiveResult<ProviderCalculateMedicalTreatmentCostResponse[]>> CalculateMedicalTreatmentCost(
        string mainPersonID,
        string? serviceTypeLookupID,
        string? relatedPersonID,
        string? deliveryType,
        CancellationToken cancellationToken)
    {
        var endpoint = $"api/Request/CalculateMedicalTreatmentCost?mainPersonID={mainPersonID}";
        if (!string.IsNullOrWhiteSpace(serviceTypeLookupID))
        {
            endpoint += $"&serviceTypeLookupID={serviceTypeLookupID}";
        }
        if (!string.IsNullOrWhiteSpace(relatedPersonID))
        {
            endpoint += $"&relatedPersonID={relatedPersonID}";
        }
        if (!string.IsNullOrWhiteSpace(deliveryType))
        {
            endpoint += $"&deliveryType={deliveryType}";
        }

        return await HttpProxyHelper.GetBazneshastegiRequest<ProviderCalculateMedicalTreatmentCostRequest, ProviderCalculateMedicalTreatmentCostResponse>(
            () => ValueTask.FromResult(PrimitiveResult.Success(this._httpClientFactory.CreateClient(HttpClientName))),
            endpoint,
            new ProviderCalculateMedicalTreatmentCostRequest(
                mainPersonID,
                serviceTypeLookupID,
                relatedPersonID,
                deliveryType),
            cancellationToken)
            .MapIf(result => result.Status,
                result => ValueTask.FromResult(PrimitiveResult.Success(result.ItemList)),
                result => ValueTask.FromResult(PrimitiveResult.Failure<ProviderCalculateMedicalTreatmentCostResponse[]>("", result.Error))
            );
    }
}