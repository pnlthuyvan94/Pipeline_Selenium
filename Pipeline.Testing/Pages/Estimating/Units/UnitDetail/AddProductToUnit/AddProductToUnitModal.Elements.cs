using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Units.UnitDetail.AddProductToUnit
{
    public partial class AddProductToUnitModal : DetailsContentPage<AddProductToUnitModal>
    {
        // Add Building Phase to Building Group
        protected Label AddProductToUnitTitle_lbl => new Label(FindType.XPath, "//h1[text()='Add Product to Unit']");

        protected Button AddProductToUnit_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertProduct']");

        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='products-modal']/section/header/a");

        string ListOfProductXpath => "//*[@id='ctl00_CPH_Content_rlbProducts']/div/ul/li";
        protected ListItem ListOfProduct_lst => new ListItem(FindElementHelper.FindElements(FindType.XPath, ListOfProductXpath).ToList());

        protected string loadingIcon => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgProducts']/div[1]";
    }
}
