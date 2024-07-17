using Pipeline.Common.Enums;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Assets.Options.Bid_Costs;

namespace Pipeline.Testing.Pages.Assets.House.Bid_Costs
{
    public partial class BidCostsToHousePage
    {
        public void FilterOptionBuildingPhaseInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionBuildingPhaseGrid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(optionBuildingPhaseLoadingIcon, 2000);
        }

        public BidCostsToHousePage EnterVendorNameToFilter(string columnName, string name)
        {
            OptionBuildingPhaseGrid.FilterByColumn(columnName, GridFilterOperator.Contains, name);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']");
            return this;
        }

        public void SelectVendor(string columnName, string name)
        {
            OptionBuildingPhaseGrid.ClickItemInGrid(columnName, name);
            WaitingLoadingGifByXpath(optionBuildingPhaseLoadingIcon, 2000);
            PageLoad();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionBuildingPhaseGrid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]");
        }

        public void FilterOptionBuildingPhaseByDropDownInGrid(string columnName, string value)
        {
            string valueToFind_ListItem;
            if ("Building Phase" == columnName)
                valueToFind_ListItem = "//*[contains(@id, 'ddlBuildingPhases_Input')]/div/ul";
            else
                // If there are any column with drop down list, display value here
                valueToFind_ListItem = string.Empty;

            OptionBuildingPhaseGrid.FilterByColumnDropDowwn(columnName, valueToFind_ListItem, value);
            WaitingLoadingGifByXpath(optionBuildingPhaseLoadingIcon, 2000);
        }

        public bool IsOptionBuildingPhaseInGrid(string columnName, string value)
        {
            return OptionBuildingPhaseGrid.IsItemOnCurrentPage(columnName, value);
        }

        public void ClickEditItemInGrid(string columnName, string value)
        {
            OptionBuildingPhaseGrid.ClickEditItemInGrid(columnName, value);
            PageLoad();
            System.Threading.Thread.Sleep(3000);
            CommonHelper.CaptureScreen();
        }

        public void Update_HouseOptionBidCost(string newBidCost)
        {
            HouseOptionBidCostHouseOverried_txt.SetText(newBidCost);
            HouseOptionBidCostUpdate_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]");
            System.Threading.Thread.Sleep(3000);
            CommonHelper.CaptureScreen();
        }

        public void ShowAssignedCostOnly()
        {
            Label title_lbl = new Label(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_ctl00_CPH_Content_ckbAssignedOnlyPanel\"]/span/label");
            title_lbl.Click();
            System.Threading.Thread.Sleep(3000);
            CommonHelper.CaptureScreen();
        }
    }
}
