using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Costing.CostingEstimate;
using Pipeline.Testing.Pages.Purchasing.PurchaseOrders.ManageAllPurchaseOrders.ChangeStatus;
using Pipeline.Testing.Pages.Purchasing.Trades.AddTrade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.PurchaseOrders.ManageAllPurchaseOrders
{
    public partial class ManageAllPurchaseOrdersPage
    {
        public void FilterItemInGrid(string columnName, string value)
        {
            ManageAllPurchaseOrdersPage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 2000);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return ManageAllPurchaseOrdersPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public ManageAllPurchaseOrdersPage ViewAndPrintPO()
        {
            ExtentReportsHelper.LogPass($"<font color ='green'>PO Report will be replaced by jaspersoft.</font color>");
            return this;
        }

        public void ClickToOpenChangeStatusModal()
        {
            PageLoad();
            Button PO_chk = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrders_ctl00']//tbody//tr[1]//td[2]//input");
            PO_chk.Click();
            GetItemOnHeader(DashboardContentItems.ChangeStatus).Click();
            ChangeStatusModal = new ChangeStatusModal();
            System.Threading.Thread.Sleep(500);
        }


        public void CancelPO()
        {
            if (ChangeStatusModal.IsModalDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Could not open Chnage status modal or the title is incorrect</font>.");

            ChangeStatusModal.ChangeStatus("Cancelled");

        }

    }
}
