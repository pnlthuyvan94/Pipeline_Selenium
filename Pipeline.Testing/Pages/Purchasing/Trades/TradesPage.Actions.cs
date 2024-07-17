using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Costing.CommunitySalesTax;
using Pipeline.Testing.Pages.Purchasing.Trades.AddTrade;
using Pipeline.Testing.Pages.Purchasing.Trades.EditTrade;

namespace Pipeline.Testing.Pages.Purchasing.Trades
{
    public partial class TradesPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Trades_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Trades_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Trades_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }
                
        public void SelectItemInGrid(string columnName, string value)
        {
            Trades_Grid.ClickItemInGrid(columnName, value);
        }

        public void SelectTrade(string columnName, string name)
        {
            Trades_Grid.ClickItemInGrid(columnName, name);
            WaitGridLoad();
            PageLoad();
        }

        public void WaitGridLoad()
        {
            //WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingTrades']/div[1]");
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgBuildingTrades']/div[1]");
        }

        public void ClickAddToOpenCreateTradeModal()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddTradeModal = new AddTradeModal();
            System.Threading.Thread.Sleep(500);
        }

        public void OpenTradesExportModal()
        {
            PageLoad();
            SelectItemInUtiliestButton("Export", true);
            System.Threading.Thread.Sleep(3000);
            TradesExportModal = new TradesExportModal();
        }

        public void ClickEditToOpenEditTradeModal(string tradeName)
        {
            PageLoad();
            Trades_Grid.ClickEditItemInGridButton("Trade", tradeName);
            System.Threading.Thread.Sleep(5000);
            WaitGridLoad();
            EditTradeModal = new EditTradeModal();
        }

        public void ClickCancelEditTradeModal()
        {
            EditTradeModal.Close();
        }

        /// <summary>
        /// Create a new Trade
        /// </summary>
        /// <param name="data"></param>
        public void CreateTrade(TradesData data, bool forDuplicateTest = false, bool isOldModal = false)
        {
            if (AddTradeModal.IsModalDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Could not open Create Trade modal or the title is incorrect</font>.");

            // Create Trade - Click 'Save' Button
            if (isOldModal)
                AddTradeModal.AddTrade(data, true);
            else
                AddTradeModal.AddTrade(data);

            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = $"Trade {data.Code} {data.TradeName} saved successfully!";
            if(forDuplicateTest)
            {
                _expectedMessage = "Duplicated Trade Name.";
                if (_actualMessage == _expectedMessage)
                {
                    ExtentReportsHelper.LogPass($"Duplicate trade name was not created.");
                }
            }
            else
            {
                if (_expectedMessage == _actualMessage)
                {
                    ExtentReportsHelper.LogPass("The message is displayed as expected. Actual results: " + _actualMessage);
                    CloseToastMessage();
                }
                else
                {
                    FilterItemInGrid("Trade", GridFilterOperator.Contains, data.TradeName);
                    if (IsItemInGrid("Trade", data.TradeName) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Create Trade with name {data.TradeName} successfully.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, $"<font color='red'></font>Can't find Trade with name {data.TradeName} on the grid view." +
                            $"<br>Failed to create Trade.</br></font>");
                }
            }
        }

        /// <summary>
        /// Update Trade
        /// </summary>
        /// <param name="data"></param>
        public void UpdateTrade(TradesData data)
        {
            ClickEditToOpenEditTradeModal(data.TradeName);

            if (EditTradeModal.IsModalDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Could not open Edit Trade modal or the title is incorrect</font>.");

            // Update Trade - Click 'Save' Button
            EditTradeModal.SaveUpdatedTrade(data);

            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = $"Trade {data.Code} {data.TradeName} saved successfully!";
            if (_actualMessage == _expectedMessage)
            {
                ExtentReportsHelper.LogPass("The message is displayed as expected. Actual results: " + _actualMessage);
            }
            else
            {
                ExtentReportsHelper.LogFail("The message is displayed as not expected. Actual results: " + _expectedMessage);
            }
        }


        /// <summary>
        /// Delete Trades by Name
        /// </summary>
        /// <param name="costTradeName"></param>
        public void DeleteTrade(string tradeName)
        {
            DeleteItemInGrid("Trade", tradeName);
            WaitGridLoad();

            string successfulMess = $"Trade {tradeName} deleted successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Trade deleted successfully!</b></font>");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Trade", tradeName))
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Trade '{tradeName}' could not be deleted!</font>");
                else
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Trade '{tradeName}' deleted successfully!</b></font>");
            }
        }

        public void CloseModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='trades-modal']/section/header/a").Click();
            System.Threading.Thread.Sleep(500);
        }

        public void ClickVendorAssignments()
        {
            VendorAssignments_btn.Click();
        }

        public string EditTrade(string columnName, string value, string newTradeName, string buildingPhase, string schedulingTasks)
        {
            Trades_Grid.ClickEditItemInGrid(columnName, value);
            System.Threading.Thread.Sleep(4000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Enter trade name.</b></font>");
            Textbox trade_txt = new Textbox(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgBuildingTrades_ctl00']/tbody/tr/td/input[contains(@id,'txtBuildingTrades_Name')]");
            trade_txt.SetText(newTradeName);

            //if (!string.IsNullOrEmpty(vendor))
            //{
            //    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select vendor.</b></font>");
            //    DropdownList vendor_dll = new DropdownList(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgBuildingTrades_ctl00']/tbody/tr/td/div/div/select[contains(@id,'ddlVendorsNameEdit')]");
            //    vendor_dll.SelectItem(vendor);
            //}

            if (!string.IsNullOrEmpty(buildingPhase))
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select building phase.</b></font>");
                DropdownList buildingPhase_dll = new DropdownList(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgBuildingTrades_ctl00']/tbody/tr/td/div/div/select[contains(@id,'lstBuildingPhaseEdit')]");
                buildingPhase_dll.SelectItem(buildingPhase);
            }

            if (!string.IsNullOrEmpty(schedulingTasks))
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select scheduling task.</b></font>");
                DropdownList schedulingTask_dll = new DropdownList(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgBuildingTrades_ctl00']/tbody/tr/td/div/div/select[contains(@id,'lsSchedulingTaskEdit')]");
                schedulingTask_dll.SelectItem(schedulingTasks);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click update button.</b></font>");
            Button updateBtn = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgBuildingTrades_ctl00'" +
              "]/tbody/tr/td/input[contains(@id,'UpdateButton')]");
            updateBtn.Click();

            return GetLastestToastMessage();
        }

        public void EditCancelTrade(string columnName, string value)
        {
            Trades_Grid.ClickEditItemInGrid(columnName, value);
            System.Threading.Thread.Sleep(4000);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click cancel button.</b></font>");
            Button updateBtn = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgBuildingTrades_ctl00'" +
              "]/tbody/tr/td/input[contains(@id,'CancelButton')]");
            updateBtn.Click();
        }

    }

}
