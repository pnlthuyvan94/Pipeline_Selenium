using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;


namespace Pipeline.Testing.Pages.Costing.OptionBidCost.HistoricCosting
{
    public partial class HistoricCostingPage
    {
        public void WaitForHistoricCostPanelLoadingGif()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCosts']/div[1]", 10, 0);
        }

        public bool IsItemInHistoricCostGrid(string columnName, string value)
        {
            return HistoricCost_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void SelectItemHistoricCostInGrid(string columnName, string valueToFind)
        {
            HistoricCost_Grid.ClickItemInGrid(columnName, valueToFind);
            PageLoad();
        }

        public void EditFutureBidCostRecord(string buildingPhase, double bidCost)
        {
            HistoricCost_Grid.ClickEditItemInGridWithTextContains("Building Phase", buildingPhase);
            Textbox bidCostTextbox = new Textbox(FindType.XPath, "//*[contains(@id,'txtFuture_Cost')]");
            bidCostTextbox.Clear();
            bidCostTextbox.AppendKeys(bidCost.ToString("N2"));

            Textbox futureCostTextbox = new Textbox(FindType.Name, "ctl00$CPH_Content$rgCosts$ctl00$ctl04$Future_Cost_EffectiveDateDatePickerTier1$dateInput");
            futureCostTextbox.Clear();
            futureCostTextbox.AppendKeys(DateTime.Now.ToString("MM/dd/yyyy"));

            Button accept = new Button(FindType.XPath, "//*[contains(@id,'_UpdateButton')]");
            accept.Click();
            HistoricCost_Grid.WaitGridLoad();
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            HistoricCost_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public HistoricCostingPage ClickBackToPreviousBtn()
        {
            BackToPrevious_Btn.Click();
            return this;
        }

    }
}
