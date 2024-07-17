using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.House
{
    public partial class HousePage : DashboardContentPage<HousePage>
    {
        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]";

        protected IGrid HousePage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgHouses_ctl00']", _gridLoading);
        protected Textbox enterNewHouseNameInCopyModal => new Textbox(FindType.XPath, "//input[@placeholder = 'Enter new house name']");
        protected Button copyButtonInCopyModal => new Button(FindType.XPath, "//a[text() = 'Copy']");
        protected CheckBox includeAllQuantitiesInCopyModal => new CheckBox(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_chkCopyQuantities']");
        protected CheckBox includeAllHouseOptionsInCopyModal => new CheckBox(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_chkCopyAllOptions']");
        protected CheckBox includeAllSalesOptionsLogicInCopyModal => new CheckBox(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_chkCopyAllSalesOptionLogic']");
        protected CheckBox includeAllCommunitiesInCopyModal => new CheckBox(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_chkCopyAllCommunities']");
        protected CheckBox includeAllSalesOptionsInCopyModal => new CheckBox(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_chkCopyAllSalesOptions']");
    }
}
