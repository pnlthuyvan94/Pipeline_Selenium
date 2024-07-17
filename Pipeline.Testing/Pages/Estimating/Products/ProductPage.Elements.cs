using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Estimating.Products
{
    public partial class ProductPage : DashboardContentPage<ProductPage>
    {
        private string _grid => "//*[id='ctl00_CPH_Content_rgProducts_ctl00']";
        protected Button DeleteByBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbByPhase']");
        protected Button DeleteByStyle_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbByStyle']");
        protected Button Specific_Product => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbSpecific']");
        protected CheckBox StyleDelete_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ckStyle']");

        protected Button EntireProduct_btn => new Button(FindType.XPath, "//button[.='Entire Product']");
        protected Button Delete_btn => new Button(FindType.XPath, "//input[@id='ctl00_CPH_Content_lbDelete']");
        protected Button DeleteByAll_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAll");

        private string _loadingWhileDeleting => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[2]";

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]";
        protected IGrid ProductPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProducts_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]");
        protected CheckBox CheckAll_btn => new CheckBox(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_rgProducts_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");
        protected Button BulkActions_btn => new Button(FindType.XPath, "//a[@id = 'bulk-actions']");


    }
}
