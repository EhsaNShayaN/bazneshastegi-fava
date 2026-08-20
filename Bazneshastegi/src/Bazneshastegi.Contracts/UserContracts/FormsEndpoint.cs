using SRH.PresentationApi.ApiEndpoint;

namespace Bazneshastegi.Contracts.UserContracts;

public sealed class FormsEndpoint : ApiEndpointBase
{
    const string _tag = "Forms";

    protected override ApiEndpointItem MyEndpoint => EndpointMetadata.Forms;

    public EndpointInfo PersonInfo { get; private set; }
    public EndpointInfo LoginForPortal { get; private set; }
    public EndpointInfo RequestTypes { get; private set; }
    public EndpointInfo LookupData { get; private set; }
    public EndpointInfo Relationship { get; private set; }
    public EndpointInfo Insert { get; private set; }
    public EndpointInfo InsertComplementary { get; private set; }
    public EndpointInfo RelatedPersons { get; private set; }
    public EndpointInfo RelatedListForPortal { get; set; }
    public EndpointInfo NewPersonByParentId { get; private set; }
    public EndpointInfo RequestTypeAttachment { get; private set; }
    public EndpointInfo InsertRequestAttachment { get; private set; }
    public EndpointInfo ActiveFacilitiesOfPerson { get; private set; }
    public EndpointInfo GetRequestTypeConfig { get; private set; }
    public EndpointInfo SetInstalementAmount { get; private set; }
    public EndpointInfo GetRequestTypeGuide { get; private set; }
    public EndpointInfo PayFractionCertificate { get; private set; }
    public EndpointInfo GetLookup { get; private set; }
    public EndpointInfo InsertComplementary_WorkDisability { get; private set; }
    public EndpointInfo InsertComplementary_Burial { get; private set; }
    public EndpointInfo InsertComplementary_PhysicalDisability { get; private set; }
    public EndpointInfo InsertComplementary_Illness { get; private set; }
    public EndpointInfo InsertComplementary_IntroduceToEducationalPlace { get; private set; }
    public EndpointInfo InsertComplementary_IntroduceToSportsVenue { get; private set; }
    public EndpointInfo InsertComplementary_NursingExpenses { get; private set; }
    public EndpointInfo InsertComplementary_MarriageLoan { get; private set; }
    public EndpointInfo InsertComplementary_MarriageAid { get; private set; }
    public EndpointInfo InsertComplementary_Imprest { get; private set; }
    public EndpointInfo InsertRequestForEditBasketReceiveTypeRetired { get; private set; }
    public EndpointInfo InsertRequestForEditBasketReceiveTypeHeir { get; private set; }
    public EndpointInfo InsertNewPerson { get; private set; }
    public EndpointInfo UpdateNewPerson { get; private set; }
    public EndpointInfo InsertRequestForEditPersonInfo { get; private set; }
    public EndpointInfo GetTempPerson { get; private set; }
    public EndpointInfo CalculateMedicalTreatmentCost { get; private set; }

    public FormsEndpoint()
    {
        PersonInfo = new EndpointInfo(
           this.GetUrl("PersonInfo"),
           this.GetUrl("PersonInfo"),
           "PersonInfo",
           "PersonInfo",
           _tag);

        LoginForPortal = new EndpointInfo(
           this.GetUrl("LoginForPortal"),
           this.GetUrl("LoginForPortal"),
           "LoginForPortal",
           "LoginForPortal",
           _tag);

        RequestTypes = new EndpointInfo(
           this.GetUrl("RequestTypes"),
           this.GetUrl("RequestTypes"),
           "RequestTypes",
           "RequestTypes",
           _tag);

        LookupData = new EndpointInfo(
           this.GetUrl("LookupData"),
           this.GetUrl("LookupData"),
           "LookupData",
           "LookupData",
           _tag);

        Relationship = new EndpointInfo(
           this.GetUrl("Relationship"),
           this.GetUrl("Relationship"),
           "Relationship",
           "Relationship",
           _tag);

        Insert = new EndpointInfo(
           this.GetUrl("Insert"),
           this.GetUrl("Insert"),
           "Insert",
           "Insert",
           _tag);

        InsertComplementary = new EndpointInfo(
           this.GetUrl("InsertComplementary"),
           this.GetUrl("InsertComplementary"),
           "InsertComplementary",
           "InsertComplementary",
           _tag);

        RelatedPersons = new EndpointInfo(
           this.GetUrl("RelatedPersons"),
           this.GetUrl("RelatedPersons"),
           "RelatedPersons",
           "RelatedPersons",
           _tag);

        RelatedListForPortal = new EndpointInfo(
           this.GetUrl("RelatedListForPortal"),
           this.GetUrl("RelatedListForPortal"),
           "RelatedListForPortal",
           "RelatedListForPortal",
           _tag);

        NewPersonByParentId = new EndpointInfo(
           this.GetUrl("NewPersonByParentId"),
           this.GetUrl("NewPersonByParentId"),
           "NewPersonByParentId",
           "NewPersonByParentId",
           _tag);

        RequestTypeAttachment = new EndpointInfo(
           this.GetUrl("RequestTypeAttachment"),
           this.GetUrl("RequestTypeAttachment"),
           "RequestTypeAttachment",
           "RequestTypeAttachment",
           _tag);

        InsertRequestAttachment = new EndpointInfo(
           this.GetUrl("InsertRequestAttachment"),
           this.GetUrl("InsertRequestAttachment"),
           "InsertRequestAttachment",
           "InsertRequestAttachment",
           _tag);

        ActiveFacilitiesOfPerson = new EndpointInfo(
           this.GetUrl("ActiveFacilitiesOfPerson"),
           this.GetUrl("ActiveFacilitiesOfPerson"),
           "ActiveFacilitiesOfPerson",
           "ActiveFacilitiesOfPerson",
           _tag);

        GetRequestTypeConfig = new EndpointInfo(
           this.GetUrl("GetRequestTypeConfig"),
           this.GetUrl("GetRequestTypeConfig"),
           "GetRequestTypeConfig",
           "GetRequestTypeConfig",
           _tag);

        SetInstalementAmount = new EndpointInfo(
           this.GetUrl("SetInstalementAmount"),
           this.GetUrl("SetInstalementAmount"),
           "SetInstalementAmount",
           "SetInstalementAmount",
           _tag);

        GetRequestTypeGuide = new EndpointInfo(
           this.GetUrl("GetRequestTypeGuide"),
           this.GetUrl("GetRequestTypeGuide"),
           "GetRequestTypeGuide",
           "GetRequestTypeGuide",
           _tag);

        PayFractionCertificate = new EndpointInfo(
           this.GetUrl("PayFractionCertificate"),
           this.GetUrl("PayFractionCertificate"),
           "PayFractionCertificate",
           "PayFractionCertificate",
           _tag);

        GetLookup = new EndpointInfo(
           this.GetUrl("GetLookup"),
           this.GetUrl("GetLookup"),
           "GetLookup",
           "GetLookup",
           _tag);

        InsertComplementary_WorkDisability = new EndpointInfo(
           this.GetUrl("InsertComplementary_WorkDisability"),
           this.GetUrl("InsertComplementary_WorkDisability"),
           "InsertComplementary_WorkDisability",
           "InsertComplementary_WorkDisability",
           _tag);

        InsertComplementary_Burial = new EndpointInfo(
           this.GetUrl("InsertComplementary_Burial"),
           this.GetUrl("InsertComplementary_Burial"),
           "InsertComplementary_Burial",
           "InsertComplementary_Burial",
           _tag);

        InsertComplementary_PhysicalDisability = new EndpointInfo(
           this.GetUrl("InsertComplementary_PhysicalDisability"),
           this.GetUrl("InsertComplementary_PhysicalDisability"),
           "InsertComplementary_PhysicalDisability",
           "InsertComplementary_PhysicalDisability",
           _tag);

        InsertComplementary_Illness = new EndpointInfo(
           this.GetUrl("InsertComplementary_Illness"),
           this.GetUrl("InsertComplementary_Illness"),
           "InsertComplementary_Illness",
           "InsertComplementary_Illness",
           _tag);

        InsertComplementary_IntroduceToEducationalPlace = new EndpointInfo(
           this.GetUrl("InsertComplementary_IntroduceToEducationalPlace"),
           this.GetUrl("InsertComplementary_IntroduceToEducationalPlace"),
           "InsertComplementary_IntroduceToEducationalPlace",
           "InsertComplementary_IntroduceToEducationalPlace",
           _tag);

        InsertComplementary_IntroduceToSportsVenue = new EndpointInfo(
           this.GetUrl("InsertComplementary_IntroduceToSportsVenue"),
           this.GetUrl("InsertComplementary_IntroduceToSportsVenue"),
           "InsertComplementary_IntroduceToSportsVenue",
           "InsertComplementary_IntroduceToSportsVenue",
           _tag);

        InsertComplementary_NursingExpenses = new EndpointInfo(
           this.GetUrl("InsertComplementary_NursingExpenses"),
           this.GetUrl("InsertComplementary_NursingExpenses"),
           "InsertComplementary_NursingExpenses",
           "InsertComplementary_NursingExpenses",
           _tag);

        InsertComplementary_MarriageLoan = new EndpointInfo(
           this.GetUrl("InsertComplementary_MarriageLoan"),
           this.GetUrl("InsertComplementary_MarriageLoan"),
           "InsertComplementary_MarriageLoan",
           "InsertComplementary_MarriageLoan",
           _tag);

        InsertComplementary_MarriageAid = new EndpointInfo(
           this.GetUrl("InsertComplementary_MarriageAid"),
           this.GetUrl("InsertComplementary_MarriageAid"),
           "InsertComplementary_MarriageAid",
           "InsertComplementary_MarriageAid",
           _tag);

        InsertComplementary_Imprest = new EndpointInfo(
           this.GetUrl("InsertComplementary_Imprest"),
           this.GetUrl("InsertComplementary_Imprest"),
           "InsertComplementary_Imprest",
           "InsertComplementary_Imprest",
           _tag);

        InsertRequestForEditBasketReceiveTypeRetired = new EndpointInfo(
           this.GetUrl("InsertRequestForEditBasketReceiveTypeRetired"),
           this.GetUrl("InsertRequestForEditBasketReceiveTypeRetired"),
           "InsertRequestForEditBasketReceiveTypeRetired",
           "InsertRequestForEditBasketReceiveTypeRetired",
           _tag);

        InsertRequestForEditBasketReceiveTypeHeir = new EndpointInfo(
           this.GetUrl("InsertRequestForEditBasketReceiveTypeHeir"),
           this.GetUrl("InsertRequestForEditBasketReceiveTypeHeir"),
           "InsertRequestForEditBasketReceiveTypeHeir",
           "InsertRequestForEditBasketReceiveTypeHeir",
           _tag);

        InsertNewPerson = new EndpointInfo(
           this.GetUrl("InsertNewPerson"),
           this.GetUrl("InsertNewPerson"),
           "InsertNewPerson",
           "InsertNewPerson",
           _tag);

        UpdateNewPerson = new EndpointInfo(
           this.GetUrl("UpdateNewPerson"),
           this.GetUrl("UpdateNewPerson"),
           "UpdateNewPerson",
           "UpdateNewPerson",
           _tag);

        InsertRequestForEditPersonInfo = new EndpointInfo(
           this.GetUrl("InsertRequestForEditPersonInfo"),
           this.GetUrl("InsertRequestForEditPersonInfo"),
           "InsertRequestForEditPersonInfo",
           "InsertRequestForEditPersonInfo",
           _tag);

        GetTempPerson = new EndpointInfo(
           this.GetUrl("GetTempPerson"),
           this.GetUrl("GetTempPerson"),
           "GetTempPerson",
           "GetTempPerson",
           _tag);

        CalculateMedicalTreatmentCost = new EndpointInfo(
           this.GetUrl("CalculateMedicalTreatmentCost"),
           this.GetUrl("CalculateMedicalTreatmentCost"),
           "CalculateMedicalTreatmentCost",
           "CalculateMedicalTreatmentCost",
           _tag);
    }
}

