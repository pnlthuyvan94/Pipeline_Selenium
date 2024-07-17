using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.HouseBOM
{
    public partial class HouseBOMDetailPage : DetailsContentPage<HouseBOMDetailPage>
    {
        protected DropdownList Community_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCommunities']");
        protected DropdownList AdvanceCommunity_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAdvanceCommunities']");
        protected Button BomGeneration_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCleanAndGenerateAll']");
        protected Button AdvanceBomGeneration_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCalculateAdvance']");
        protected static string loadingIcon_Xpath => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]";
        protected IGrid QuantitiesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSuperOptions_ctl00']", loadingIcon_Xpath);
        protected IGrid AdvanceQuantities_Grid => new Grid(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgAdvanceHouseBOMView_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAdvance']/div[1]");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypUtils']");
        protected CheckBox CheckAll_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_lsbBuildingPhase']//input[@class='rlbCheckAllItemsCheckBox']");
        protected CheckBox AdvanceCheckAll_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_lsbBuildingPhaseAdvance']//input[@class='rlbCheckAllItemsCheckBox']");
        protected Textbox BuildingPhaseExport_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBuildingPhase']");
        protected Textbox AdvanceBuildingPhaseExport_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBuildingPhaseAdvance']");
        protected Button Export_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_lbExportHouseBOMPanel']//a[@id='ctl00_CPH_Content_lbExportHouseBOM']");
        protected Button ExportView_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbExportHouseBOMAdvance']");
        protected Button Utils_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypUtils']");
        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='export-modal']//a[@class='close']");
        protected Button AdvanceCancel_btn => new Button(FindType.XPath, "//*[@id='export-modal-advance']//a[@class='close']");
        protected IGrid HouseBOMDetailPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSuperOptions_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
        protected Button Expand_SWG => new Button(FindType.XPath, "//td[contains(text(),'No ( SWG ) assignment')]/../td[@class='rgExpandCol']");
        protected Button AdvancedHouseBOMView_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbAdvancedHouseBOMView_1']");
        protected Button AdvanceOptions_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcOptions_Arrow']");
        protected Button CheckAllOption_btn => new Button(FindType.XPath, "//input[@class='rcbCheckAllItemsCheckBox']");
        protected Button LoadSelectedProduct_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_btnLoadSelectedProducts']");
        protected Button BasicHouseBOMView_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbAdvancedHouseBOMView']//label[contains(text(),'Basic')]");
    }

}
