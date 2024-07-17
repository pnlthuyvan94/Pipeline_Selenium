using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.CommunityVendor.AddVendor;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityVendor
{
    public partial class CommunityVendorPage
    {
        public void FilterItemInCommunityVendorGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            AllowedVendors_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgAllowVendors']/div[1]", 2000);
        }

        public bool IsItemInCommunityVendorGrid(string columnName, string value)
        {
            return AllowedVendors_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInCommunityVendorGrid(string columnName, string value)
        {
            AllowedVendors_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitCommunityVendorGridLoad()
        {
            AllowedVendors_Grid.WaitGridLoad();
        }

        public void SelectItemInCommunityVendorGrid(string columnName, string value)
        {
            AllowedVendors_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public void FilterItemInVendorAssignmentsGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            VendorAssignments_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]", 3000);
        }

        public bool IsItemInVendorAssignmentsGrid(string columnName, string value)
        {
            return VendorAssignments_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void EditVendorAssignments(string buildingPhase, string vendorName)
        {
            VendorAssignments_Grid.ClickEditItemInGridWithTextContains("Building Phase", buildingPhase);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]", 3000);
            DropdownList primaryVendorDropdown = new DropdownList(FindType.XPath, "//*[contains(@id,'ddlPrimaryVendors')]");
            primaryVendorDropdown.SelectItemByValueOrIndex(vendorName,1);

            Button accept = new Button(FindType.XPath, "//*[contains(@id,'_UpdateButton')]");
            accept.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]", 3000);
        }

        public void OpenCommunityVendorModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddVendor']").Click();
            CommunityVendorModal = new CommunityVendorModal();
            CommonHelper.WaitUntilElementVisible(5, "//h1[text()='Add Allowed Vendors']");
        }

        /// <summary>
        /// Assign Community to Vendor
        /// </summary>
        /// <param name="vendorLis"></param>
        public void AssignVendorToCommunity(string[] vendorLis)
        {
            foreach (string vendor in vendorLis)
            {
                // Filter Vendor name
                FilterItemInCommunityVendorGrid("Name", GridFilterOperator.EqualTo, vendor);

                // If Vendor was assigned to community before
                if (!IsItemInCommunityVendorGrid("Name", vendor))
                {
                    OpenCommunityVendorModal();
                    CommunityVendorModal.SelectCommunityVendorByName(vendor);
                    CommunityVendorModal.Save();
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_tbFilter']");
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Vendor '{vendor}' was assigned to Community before.");
                }
            }
        }
    }
}
