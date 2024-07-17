using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Jobs.Job.JobDetail
{
    public partial class JobDetailPage : DetailsContentPage<JobDetailPage>
    {
        protected Textbox Name_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtJobNo']");

        protected DropdownList Community_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCommunities']");

        protected DropdownList House_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlHouses']");

        protected DropdownList Lot_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlLots']");

        private string _loadingHouseXpath
         => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlHouses']/div[1]";
        private string _loadingLotAddressXpath
        => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlLotAddressInfo']/div[1]";

        protected DropdownList Orientation_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOrientations']");

        protected DropdownList Drafting_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlLocations']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        protected Button CloseJob_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCloseUncloseJob' and text() = 'Open Job']");
        protected Button OpenJob_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCloseUncloseJob' and text() = 'Close Job']");
        protected Button AssignCustomer_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAssignCustomer']");
        protected Button AssignCustomerSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_CustomersDialogModal_lbGeneric']");

        protected Button SyncSaberis_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaberis']");

        protected DropdownList ViewByOnJobBOM_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlViewTypes']");

        protected Button ScheduleTemplate_div => new Button(FindType.Id, "ctl00_CPH_Content_pnlSchedulingTemplate");
        protected Button SyncToBuildPro_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbStartSync");
        protected Button StartSyncToBuildPro => new Button(FindType.Id, "ctl00_CPH_Content_BuildProSyncModal_lbBuildProIntegrationSync");
        protected Button ViewBuildProEPO_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbViewEPOs");
        protected Button DeleteQuantities_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbDeleteModalOpener");
        protected Button DeleteOnModal_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbDeleteAllConfigQuantities");
        protected CheckBox CheckAll_Chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbSourcesForDelete']/div/div[2]/label/input");
        protected Button CloseModal_Btn => new Button(FindType.XPath, "//*[@id='configs-delete-modal']/section/header/a");
        protected CheckBox OptionSpecified_Chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_rbImportType_0");
        protected CheckBox NoOptionSpecified_Chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_rbImportType_1");
        protected Textbox ImportFile_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_RadAsyncUpload1file0");
        protected Button DeleteSelectedImportFile_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbDelete");
        protected Button ProcessedImportFile_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbProcessFileAndConfiguration");
        protected Button FinishImportFile_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbFinishJobImportProcess");
        protected Button GenerateBomAndEstimate_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbCalculate");
        protected Button ExpandAllCreatePO_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']/thead/tr[1]/th[1]/input");
        protected Button CreatePO_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSelected");
        protected Button ExpandAllPO_Btn => new Button(FindType.Id, "ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl02_ctl00_GECAllBtnExpandColumn");
        protected IGrid VendorChangeRequest_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgVendorChangesPending_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendorChangesPending']/div[1]");
        protected IGrid AssignCustomer_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_CustomersDialogModal_myCustomerGrid_rgCustomers_ctl00", "//*[@id='ctl00_CPH_Content_CustomersDialogModal_LoadingPanel1ctl00_CPH_Content_CustomersDialogModal_rapMain']/div[1]");
        protected Button ProcessPOVenderChange_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbProcessVendorChanges");
        protected Button AddLot_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddLot");
        protected Textbox NewLot_Text => new Textbox(FindType.Id, "ctl00_CPH_Content_txtNewLot");
        protected Button InsertLot_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertLots");

    }

}
