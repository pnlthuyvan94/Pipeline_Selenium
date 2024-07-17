using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Assets.House.Import
{
    public partial class HouseImportDetailPage : DetailsContentPage<HouseImportDetailPage>
    {
        protected Button UploadBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbOpenUploadModal']");
        protected Textbox SelectFileTxt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_RadAsyncUpload1file0']");
        protected DropdownList ExportTypeDdl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlImportTypes']");
        protected CheckBox FileCheckAllChk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");
        protected IGrid UploadHouseGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPendingFiles']/div[1]");
        protected IGrid UploadHouseMaterialFilesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgFiles_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgFiles']/div[1]");
        protected Button UploadHouseQuantitiesBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbUpload']");
        protected Button UploadHouseMaterialFilesCloseBtn => new Button(FindType.XPath, "//*[@class='close']");
        protected Button GenerateReportViewBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbGenerateReportView']");
        protected Button StartComparisonSetupBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbStartComparisonSetup']");
        protected Button StartComparisonBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbStartComparison']");
        protected Button ViewComparisonBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbViewComparison']");
        protected CheckBox CheckAllChk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgFiles_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");
        protected Button ImportBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbImport']");
        protected Button FinishImportBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbFinalize']");
        protected Textbox ImportCompletedTxt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlFinalize']//strong[contains(text(),'Import Completed')]");
        protected IGrid ImportQuantitiesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReportView_ctl00_Header']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel']/div[1]");

        protected Label ValidateProductLbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlAddProducts']//h1[contains(text(),'Validate Products')]");
        protected CheckBox CheckAllProductChk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgAddProducts_ctl00']//th[contains(text(),'Create')]//input[contains(@id,'ClientSelectColumnSelectCheckBox')]");
        protected IGrid ValidateProductsGrid=> new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgAddProducts_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddProducts']/div[1]");

        protected Button ContinueProductsImportBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbContinueProducts']");
        protected Label BuildingPhaseNotFoundLbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlAddProductToBuildingPhase']//h1[contains(text(),'Building Phase Assignment Not Found')]");
        protected CheckBox CheckAllBuildingPhaseChk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgAddProductToBuildingPhase_ctl00']//input[contains(@id,'CreateCheckAll')]");
        protected IGrid BuildingPhaseNotFoundGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgAddProductToBuildingPhase_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddProductToBuildingPhase']/div[1]");

        protected Button ContinueProductToBuildingPhaseBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbContinueProductToBuildingPhase']");

        protected CheckBox Un_ModifiedOptionsChk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgUnmodifiedOptions_ctl00']//th[@class='rgHeader rgCheck']/input");
        protected Button AddBaseOptionBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCreateBaseOption']");

        protected IGrid ComparisonGroupsGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rtlComparisonGroups']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");

        protected IGrid Un_ModifiedOptionsGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgUnmodifiedOptions_ctl00']", string.Empty);

        protected Button GenerateComparisonBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbGenerateComparisons']");

        protected IGrid ReportViewGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReportView_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel']/div[1]");

        protected Label StatusLbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlFinalize']//p");

        protected Label NoImportComparionLbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//tr[@class='rtlR wrapword']//div[contains(text(),'No Import Comparison Groups to display.')]");
        protected Button AddComparisonGroupsBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddConfigurationGroup']");

        protected DropdownList OptionDdl => new DropdownList(FindType.XPath, "//*[contains(@id,'ddlMainOption')]");
        protected Button ConditionBtn => new Button(FindType.XPath, "//*[@class='linkBtn']");
        protected DropdownList InlineOptionDdl => new DropdownList(FindType.XPath, "//*[contains(@id,'InlineOption')]");

        protected DropdownList InlineConditonDdl => new DropdownList(FindType.XPath, "//*[contains(@id,'InlineCondition')]");

        protected Textbox InlineConditonTxt => new Textbox(FindType.XPath, "//*[contains(@id,'txtInlineCondition')]");

        protected Button InsertBtn => new Button(FindType.XPath, "//*[@alt='Insert' and contains(@id,'InsertCommandColumn')]");

        protected Button UpdateBtn => new Button(FindType.XPath, "//*[@alt='Update' and contains(@id,'EditCommandColumn')]");

        protected Label ValidateProductStylesLbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlAddStyles']//h1[contains(text(),'Validate Product Styles')]");

        protected CheckBox CheckAllCreateChk => new CheckBox(FindType.XPath, "//input[contains(@id,'CreateCheckAll')]");

        protected IGrid ValidateProductStylesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgAddStyles_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddStyles']/div[1]");

        protected Button ContinueStylesImportBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbContinueStyles']");
    }
}
