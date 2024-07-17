using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments
{
    public partial class VendorAssignmentsPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            VendorAssignmentsHeader_grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return VendorAssignments_grid.IsItemOnCurrentPage(columnName, value);
        }

        public string EditCompanyVendor(string newVendor)
        {
            Button editButton = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
                "]/tbody/tr[1]/td/input[contains(@id,'EditButton')]");
            editButton.Click();

            VendorAssignments_grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();

            DropdownList companyVendor_dll = new DropdownList(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
                "]/tbody/tr[1]/td/select[contains(@id,'ddlCompanyVendors_Name')]");

            companyVendor_dll.SelectItem(newVendor);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();


            Button updateBtn = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
                "]/tbody/tr/td/input[contains(@src,'accept')]");
            updateBtn.Click();

            string toastMessage = GetLastestToastMessage();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            return toastMessage;
        }

        public string EditDivisionVendor(string divisionName, string newVendor)
        {
            Button editButton = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
                "]/tbody/tr[1]/td/input[contains(@id,'EditButton')]");
            editButton.Click();
           
            VendorAssignments_grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(10000);
            CommonHelper.CaptureScreen();

            DropdownList divisionVendor_dll = new DropdownList(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
                "]/tbody/tr[1]/td/select[contains(@id,'" + divisionName + "')]");
          
            divisionVendor_dll.SelectItem(newVendor);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(10000);
            CommonHelper.CaptureScreen();


            Button updateBtn = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
                "]/tbody/tr/td/input[contains(@src,'accept')]");
            updateBtn.Click();

            string toastMessage = GetLastestToastMessage();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            return toastMessage;
        }

        public string EditCommunityVendor(string communityName, string newVendor)
        {
            Button editButton = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
                "]/tbody/tr[1]/td/input[contains(@id,'EditButton')]");
            editButton.Click();

            VendorAssignments_grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(10000);
            CommonHelper.CaptureScreen();

            DropdownList communityVendor_dll = new DropdownList(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
                "]/tbody/tr[1]/td/select[contains(@id,'" + communityName + "')]");

            communityVendor_dll.SelectItem(newVendor);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(10000);
            CommonHelper.CaptureScreen();

            Button updateBtn = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgTradesToVendors_ctl00'" +
              "]/tbody/tr/td/input[contains(@src,'accept')]");
            updateBtn.Click();

            string toastMessage = GetLastestToastMessage();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]", 1000);
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            return toastMessage;
        }

        public string SelectDivision(string division, int index)
        {
            return Divisions_ddl.SelectItemByValueOrIndex(division, index);
        }

        public void SelectCommunities(IList<string> communities)
        {
            //Expand Communities
            CommunitiesArrow_btn.Click();
            foreach (string item in communities)
            {
                CheckBox communities_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcCommunities_DropDown']//li//label[contains(text(),'{item}')]/input");
                communities_chk.SetCheck(true);
            }
            System.Threading.Thread.Sleep(10000);
        }

        public void WaitGridLoad()
        {
            VendorAssignments_grid.WaitGridLoad();
        }
        public void ClickLoadVendors()
        {
            LoadVendors_btn.Click();
            System.Threading.Thread.Sleep(10000);
            WaitGridLoad();
        }

        public void BackToTradesPage()
        {
            BackToTrades_btn.Click();
        }
    }
}
