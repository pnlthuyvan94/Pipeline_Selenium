using NUnit.Framework;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionVendors.AddVendor;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionVendors
{
    public partial class DivisionVendorPage
    {
        public void FilterItemInDivisionVendorGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            AllowedVendors_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgAllowVendors']/div[1]");
        }

        public bool IsItemInDivisionVendorGrid(string columnName, string value)
        {
            return AllowedVendors_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInDivisionVendorGrid(string columnName, string value)
        {
            AllowedVendors_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitDivisionVendorGridLoad()
        {
            AllowedVendors_Grid.WaitGridLoad();
        }

        public void SelectItemInDivisionVendorGrid(string columnName, string value)
        {
            AllowedVendors_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public void FilterItemInVendorAssignmentsGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            VendorAssignments_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]", 1000);
        }

        public bool IsItemInVendorAssignmentsGrid(string columnName, string value)
        {
            return VendorAssignments_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void EditVendorAssignments(string buildingPhase, string vendorName)
        {
            VendorAssignments_Grid.ClickEditItemInGridWithTextContains("Building Phase", buildingPhase);
            //System.Threading.Thread.Sleep(2000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']");
            DropdownList primaryVendorDropdown = new DropdownList(FindType.XPath, "//*[contains(@id,'ddlPrimaryVendors')]");
            primaryVendorDropdown.SelectItemByValueOrIndex(vendorName,1);

            Button accept = new Button(FindType.XPath, "//*[contains(@id,'_UpdateButton')]");
            accept.Click();
            VendorAssignments_Grid.WaitGridLoad();
        }

        public void OpenDivisionVendorModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddVendor']").Click();
            DivisionVendorModal = new DivisionVendorModal();
            CommonHelper.WaitUntilElementVisible(5, "//h1[text()='Add Allowed Vendors']");
        }

        /// <summary>
        /// Assign Vendor to Division by Name
        /// </summary>
        /// <param name="vendorList"></param>
        public void AssignVendorToDivision(string[] vendorList)
        {
            foreach (string vendor in vendorList)
            {
                // Filter Vendor name
                FilterItemInDivisionVendorGrid("Name", GridFilterOperator.EqualTo, vendor);

                if (!IsItemInDivisionVendorGrid("Name", vendor))
                {
                    // Assign if vendor doesn't exist on the grid view
                    OpenDivisionVendorModal();
                    DivisionVendorModal.SelectDivisionVendorByName(vendor);
                    DivisionVendorModal.Save();
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_tbFilter']");
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Vendor '{vendor}' was assigned to Division before.");
                }
            }
        }
    }
}
