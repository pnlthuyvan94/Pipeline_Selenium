using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM.OneTimeItem;

namespace Pipeline.Testing.Pages.Jobs.Job.JobBOM
{
    public partial class JobBOMPage : DashboardContentPage<JobBOMPage>
    {

        string grid_Xpath = "//*[@id='ctl00_CPH_Content_rgOptionsView_ctl00']";
        string loadingGrid_xpath = "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlRpt']/div[1]";
        protected IGrid JobBomPage_Grid => new Grid(FindType.XPath, grid_Xpath, loadingGrid_xpath);
        protected Button GenerateBOM_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCalculate']");
        protected Label JobBomHeader_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblGeneratedBOMs']");
        protected IGrid JobBomPhasesView_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00_ctl05_Detail10']", "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlRpt']/div[1]");
        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='export-modal']//a[@class='close']");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypUtils']");
        protected Button BomAdjustments_btn => new Button(FindType.XPath, "//*[@id='bomAdjustments']");
        protected Button OneTimeItem_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbOneTimeProduct']");
        public OneTimeItemModal OneTimeItemModal { get; private set; }
        protected IGrid OneTimeItem_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOneTimeProduct_ctl00']", "//*[@id='ctl00_CPH_Content_rgOneTimeProduct_ctl00']/div[1]");
        protected IGrid PhaseViewJobBomPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptionsPhasesUsesView_ctl00']", "//[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlRpt']/div[1]");
        protected Button BOMAdjustments_btn => new Button(FindType.XPath, "//*[@id='bomAdjustments']");
        protected DropdownList Option_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00_ctl03_ctl01_ddlAddOption']");
        protected DropdownList Buildingphase_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00_ctl03_ctl01_ddlAddPhase']");
        protected DropdownList Product_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00_ctl03_ctl01_ddlAddProduct']");
        protected DropdownList Style_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00_ctl03_ctl01_ddlAddStyle']");
        protected DropdownList Use_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00_ctl03_ctl01_ddlAddUse']");
        protected Textbox NewQuantity_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00_ctl03_ctl01_txtAddNewQuantity']");
        protected Button Insert_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00']//input[@title='Insert']");
        protected IGrid AdjustQuantities_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00']", "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgQuantityAdjustments']/div[1]");
        protected Button JobBOMArchives_btn => new Button(FindType.XPath, "//*[@id='activity-sort']");
        protected Button ArchiveJobBom_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hlArchiveJobBom']");
        protected Button JobConfigBom_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hlArchiveJobConfigBom']");
    }
}
