using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM.OneTimeItem;
using System;
using System.Linq;

namespace Pipeline.Testing.Pages.Jobs.Job.Vendors
{
    public partial class JobVendorsPage
    {

        public bool IsItemInGrid(string columnName, string value)
        {
            return JobVendorPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInJobVendorGrid(string columnName, GridFilterOperator filterOperator, string value)
        {
            JobVendorPage_Grid.FilterByColumn(columnName, filterOperator, value);
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        public string EditItemInGrid(string columnName,string value, string newVendor)
        {
            JobVendorPage_Grid.ClickEditItemInGrid(columnName, value);
            System.Threading.Thread.Sleep(4000);

            if (!string.IsNullOrEmpty(newVendor))
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select new Vendor.</b></font>");
                DropdownList vendor_ddl = new DropdownList(FindType.XPath, "//table[@id='ctl00_CPH_Content_RgJobVendors_ctl00']/tbody/tr/td/select[contains(@id,'ddlVendors')]");
                vendor_ddl.SelectItem(newVendor);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click update button.</b></font>");
                Button updateBtn = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_RgJobVendors_ctl00'" +
                  "]/tbody/tr/td/input[contains(@id,'UpdateButton')]");
                updateBtn.Click();

                return GetLastestToastMessage();
            }
            return string.Empty;

        }

        public bool IsColumnFoundInGrid(string columnName)
        {
            try
            {
                return JobVendorPage_Grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }
    }
}
