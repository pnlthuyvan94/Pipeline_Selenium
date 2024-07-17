using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Category.CategoryDetail.AddProductToCategory
{
    public partial class AddProductToCategoryModal : DetailsContentPage<AddProductToCategoryModal>
    {
        // Add Product to Category
        protected Label AddProductToCategoryTitle_lbl => new Label(FindType.XPath, "//h1[text()='Add Product to Category']");

        protected Button AddProduct_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertProduct']");

        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='products-modal']/section/header/a");

        string ListOfPhaseXpath => "//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']/div/ul/li/label";
        protected ListItem ListOfPhase_lst => new ListItem(FindElementHelper.FindElements(FindType.XPath, ListOfPhaseXpath).ToList());
        protected DropdownList BuildingPhase_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingPhases']");
        protected Textbox ProductName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_tbFilter']");
    }
}
