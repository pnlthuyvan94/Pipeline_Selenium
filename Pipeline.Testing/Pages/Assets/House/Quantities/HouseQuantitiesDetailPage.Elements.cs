using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.Quantities
{
    public partial class HouseQuantitiesDetailPage : DetailsContentPage<HouseQuantitiesDetailPage>
    {
        protected DropdownList FilterCommunity_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCommunities']");
        protected static string loadingIcon_Xpath => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductsToHouses']/div[1]";
        protected IGrid QuantitiesGrid => new HouseQuantitiesGrid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProductsToHouses_ctl00']");
        protected Button BulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions']");
        protected Button CopyQtyHouseOption => new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbCopyQty']");
        protected Label NoHouseQuantitiesData_lbl => new Label(FindType.XPath, "//*[@class='rgNoRecords']//div[contains(text(),'No records to display.')]");
        
    }

}
