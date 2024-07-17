using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.Category.CategoryDetail.AddProductToCategory;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Category.CategoryDetail
{
    public partial class CategoryDetailPage : DetailsContentPage<CategoryDetailPage>
    {
        public AddProductToCategoryModal AddProductToCategoryModal { get; private set; }

        // Building Group Detail
        protected Label CategoryTitle_lbl => new Label(FindType.XPath, "//h1[text()='Category Details']");

        protected Textbox CategoryName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected DropdownList Parent_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlParent']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        string loadingIcon => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbLoadingAnimation']/div[1]";

        // Product
        protected Label ProductTitle_lbl => new Label(FindType.XPath, "//h1[text()='Products']");

        protected Button AddProduct_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddProduct']");

        protected CheckBox AllCheckBox_cb => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProducts_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");

        protected Button BulkAction_btn => new Button(FindType.XPath, "//a[@id='bulk-actions']");

        protected Button DeleteAll_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbDeleteSelectedProducts']");

        protected IGrid AddProduct_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProducts_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgProducts']/div[1]");

    }
}
