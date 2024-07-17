using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Costing.OptionBidCost.AddJobOptionBidCost;
using Pipeline.Testing.Pages.Costing.OptionBidCost.AddOptionBidCost;
using Pipeline.Testing.Pages.Costing.OptionBidCost.HistoricCosting;

namespace Pipeline.Testing.Pages.Costing.OptionBidCost
{
    public partial class OptionBidCostPage
    {
        public void WaitForBidcostPanelLoadingGif()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobBidCosts']/div[1]", 10, 0);
        }

        public void WaitForJobBidcostPanelLoadingGif()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]", 10, 0);
        }

        public OptionBidCostPage SelectVendor(string vendorName)
        {
            Vendor_Dropdown.SelectItem(vendorName);
            WaitForBidcostPanelLoadingGif();
            WaitForJobBidcostPanelLoadingGif();
            return this;
        }

        public void ClickAddToOpenBidCostModal()
        {
            AddBidCost_Btn.Click();
            WaitingLoadingGifByXpath("//*[id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]", 5000);
            AddOptionBidCostModal = new AddOptionBidCostModal();
            AddOptionBidCostModal.IsModalDisplayed();
        }

        public void ClickAddToOpenJobBidCostModal()
        {
            AddJobBidCost_Btn.Click();
            AddJobOptionBidCostModal = new AddJobOptionBidCostModal();
            AddJobOptionBidCostModal.IsModalDisplayed();
        }

        public void ChangeBidCostPageSize(int size)
        {
            BidCost_Grid.ChangePageSize(size);
            PageLoad();
            System.Threading.Thread.Sleep(2000);
        }

        public void ChangeJobBidCostPageSize(int size)
        {
            JobBidCost_Grid.ChangePageSize(size);
            PageLoad();
            System.Threading.Thread.Sleep(2000);
        }

        public bool IsItemInBidCostGrid(string columnName, string value)
        {
            return BidCost_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsItemInJobBidCostGrid(string columnName, string value)
        {
            return JobBidCost_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public void SelectTier(string tier)
        {
            Button TypeTier_select = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgBidCosts_ctl00_ctl02_ctl02_ddlCostingTier_DropDown']//li[contains(text(),'{tier}')]");
            TypeTier_btn.Click();
            TypeTier_select.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]", 1000);
        }
        public string EditItemBidCostGrid(string columnName, string value, string newcost,string Tier_number, string tier)
        {
            Textbox Cost_txt = new Textbox(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgBidCosts_ctl00']/tbody/tr//input[contains(@id,'txt" + Tier_number + "')]");
            Button Accept = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgBidCosts_ctl00'" +
                "]/tbody/tr/td/input[contains(@src,'accept')]");
            SelectTier(tier);
            BidCost_Grid.ClickEditItemInGrid(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]", 1000);
            Cost_txt.Clear();
            Cost_txt.SetText(newcost);
            Accept.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]", 1000);
            return GetLastestToastMessage();
        }

        public string EditItemJobBidCostGrid(string columnName, string value, string text, string tier)
        {
            Textbox Tier_txt = new Textbox(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgJobBidCosts_ctl00']/tbody/tr//input[contains(@id,'txt" + tier + "')]");
            Button Accept = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgJobBidCosts_ctl00'" +
                "]/tbody/tr/td/input[contains(@src,'accept')]");
            JobBidCost_Grid.ClickEditItemInGrid(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobBidCosts']/div[1]", 1000);
            Tier_txt.Clear();
            Tier_txt.SetText(text);
            Accept.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobBidCosts']/div[1]", 1000);
            return GetLastestToastMessage();
        }

        public string RemoveItemBidCostGrid(string columnName, string value)
        {
            BidCost_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]", 1000);
            return GetLastestToastMessage(3);

        }
        public string RemoveItemJobBidCostGrid(string columnName, string value)
        {
            JobBidCost_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobBidCosts']/div[1]",1000);
            JQueryLoad();
            return GetLastestToastMessage(2);
        }

        public bool IsHistoryCostingButtonDisplayed()
        {
            Button historicCostingBtn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbHistoricCostings']");
            if (historicCostingBtn.IsDisplayed() is true)
                return true;
            else
                return false;
        }

        public OptionBidCostPage ClickHistoricCostingBtn()
        {
            HistoricCostingPage = new HistoricCostingPage();
            HistoricCosting_Btn.Click();           
            return this;
        }
    }
}
