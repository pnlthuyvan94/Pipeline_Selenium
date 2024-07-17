
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor.AddVendorToTrade;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor
{
    public partial class TradeVendorPage
    {
        public void DeleteItemInGrid(string columnName, string value)
        {
            Vendor_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            PageLoad();
        }

        public void WaitBuildingPhaseGird()
        {
            Vendor_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Vendor_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Vendor_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }


        public void DeleteSelectedVendors(List<string> vendors)
        {
            foreach(var vendor in vendors)
            {
                Vendor_Grid.SelectItemOnGridViaCheckbox("Vendor Name", vendor);
            }
           
            BulkAction_Btn.Click();
            DeleteSelectedVendor_Btn.Click();
            ConfirmDialog(ConfirmType.OK);
            PageLoad();
        }

        public void Save()
        {
            ShowModal_btn.Click();
            System.Threading.Thread.Sleep(1000);
        }
        public void WaitGridLoad()
        {
            Vendor_Grid.WaitGridLoad();
        }

        public void ShowAddVendorToTradeModal()
        {
            ShowModal_btn.Click();
            AddVendorToTradeModal = new AddVendorToTradeModal();
            System.Threading.Thread.Sleep(1000);
        }

        public void DeleteAllVendors()
        {
            SelectAll_chk.Check();
            BulkActions_btn.Click();
            RemoveSelectedVendors_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendors']/div[1]");
        }
    }
}
