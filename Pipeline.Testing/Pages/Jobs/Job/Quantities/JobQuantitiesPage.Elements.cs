using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Jobs.Job.Quantities
{
    public partial class JobQuantitiesPage : DashboardContentPage<JobQuantitiesPage>
    {
        string grid_Xpath = "//*[@id='ctl00_CPH_Content_rgOptionConfigurationQuantities_ctl00']";
        string loadingGrid_xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionConfigurationQuantities']/div[1]";
        protected IGrid QuantitiesPage_Grid => new Grid(FindType.XPath, grid_Xpath, loadingGrid_xpath);
        protected Button ApplyQuantities_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbApplyHouseQuantities']");
        protected Button DeleteQuantities_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbDeleteModalOpener']");
        protected Button AddNewQuantity_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewQuantity']");
        protected Label AddProductQuantities_lbl => new Label(FindType.XPath, "//*[@class='rwTitle']");
        protected DropdownList Option_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlOption']");
        protected DropdownList Source_ddl => new DropdownList(FindType.XPath, "//*[@id='ddlSources']");
        protected Button OpenBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='rcbPhases_Arrow']");
        protected Button LoadProducts_btn => new Button(FindType.XPath, "//*[@id='Button1Panel']/a");
        protected IGrid ProductQuantitiesPage_Grid => new Grid(FindType.XPath, "//*[@id='rgProductsToAdd_ctl00']", "//*[@id='lp1rgProductsToAdd']/div[1]");
        protected Button SaveProductToAdd_btn => new Button(FindType.XPath, "//*[@id='lbSaveProductsToAdd']");
        protected Button CloseModalBtn => new Button(FindType.XPath, "//*[@class='rwCommandButton rwCloseButton']");
        protected Button ExportQuantitiesBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbExportOther']");
        protected Button ExportXMLQuantitiesBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbExportXML']");
        protected CheckBox CheckAllChkBox => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbcsvexport']//input[@class='rlbCheckAllItemsCheckBox']");
        protected Button ExportBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbcsvexport']");
        protected Button ExportXMLBtn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbxmlexport']");
        protected Button CancelBtn=> new Button(FindType.XPath, "//*[@class='card-header']/h1[contains(text(),'Export Job Quantities By Source in CSV or Excel')]/following-sibling::a[@class='close']");
        protected Button CancelExportXMLBtn => new Button(FindType.XPath, "//*[@class='card-header']/h1[contains(text(),'Export Job Quantities By Source in XML')]/following-sibling::a[@class='close']");
    }
}
