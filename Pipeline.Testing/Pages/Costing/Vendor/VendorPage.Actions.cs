using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Threading;

namespace Pipeline.Testing.Pages.Costing.Vendor
{
    public partial class VendorPage
    {
        public void ClickAddToVendorIcon()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            PageLoad();
        }
         
        public void FilterItemInGrid(string columnName, GridFilterOperator GridFilterOperator, string value)
        {
            VendorPage_Grid.FilterByColumn( columnName, GridFilterOperator,  value);
            //WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendors']/div[1]");
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgVendors']/div[1]"); //john: debug mode
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return VendorPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public VendorPage DeleteItemInGrid(string columnName, string value)
        {
            VendorPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendors']/div[1]");
            return this;
        }

        public void WaitGridLoad()
        {
            VendorPage_Grid.WaitGridLoad();
        }

        public VendorPage EnterVendorNameToFilter(string columnName, string name)
        {
            VendorPage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, name);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendors']");
            return this;
        }

        public void SelectVendor(string columnName, string name)
        {
            VendorPage_Grid.ClickItemInGrid(columnName, name);
            WaitGridLoad();
            PageLoad();
           
        }
        public void SyncVendorToBuildPro()
        {
            SyncToBuildPro.Click();
            CommonHelper.WaitUntilElementVisible(5, "//*[@id='ctl00_CPH_Content_BuildProSyncModal_lblHeader']");
            StartSyncToBuildPro_Btn.Click();
            System.Threading.Thread.Sleep(2000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_BuildProSyncModal_autoGrid_rgResults']/div[1]", 600, 0);
        }

        /// <summary>
        /// Get total number on the grid view
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return VendorPage_Grid.GetTotalItems;
        }

        public VendorPage NavigateToPage(int pageNumber)
        {
            VendorPage_Grid.NavigateToPage(pageNumber);
            return this;
        }
        public int GetJobGridPageCount()
        {
            return VendorPage_Grid.GetTotalPages;
        }
        public VendorPage ChangePageSize(int size)
        {
            VendorPage_Grid.ChangePageSize(size);
            return this;
        }
    }
}
