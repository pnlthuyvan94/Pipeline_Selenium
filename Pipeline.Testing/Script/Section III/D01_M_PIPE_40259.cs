using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;

namespace Pipeline.Testing.Script.Section_III
{
    public class D01_M_PIPE_40259 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private double acceptableLoadTime = 10;
        private string nameFilter = "AA";
        private string codeFilter = "AA";
        private string tradeFilter = "A";
        private string mainContactFilter = "A";
        private string addressFilter = "St";
        private string phoneFilter = "910";
        private string emailFilter = ".com";

        [Test]
        public void D01_M_Vendors_Landing_Page()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Go to Vendors Landing Page.</b></font>");
            var pageLoadTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors)); 
            if(pageLoadTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendors Landing Page load time is " + pageLoadTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Vendors Landing Page load time is " + pageLoadTime + " seconds.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Filter Vendors Grid using Name column.</b></font>");
            var filterNameTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, nameFilter));

            CommonHelper.CaptureScreen();
            if (filterNameTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Name column load time is " + filterNameTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Name column load time is " + filterNameTime + " seconds.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, "");
            CommonHelper.CaptureScreen();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Filter Vendors Grid using Code column.</b></font>");
            var filterCodeTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, codeFilter));
            CommonHelper.CaptureScreen();
            if(filterCodeTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Code column load time is " + filterCodeTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Code column load time is " + filterCodeTime + " seconds.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, "");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Filter Vendors Grid using Trade column.</b></font>");
            var filterTradeTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, tradeFilter));
            CommonHelper.CaptureScreen();
            if(filterTradeTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Trade column load time is " + filterTradeTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Trade column load time is " + filterTradeTime + " seconds.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.NoFilter, "");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Filter Vendors Grid using Main Contact column.</b></font>");
            var filterMainContactTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.FilterItemInGrid("Main Contact", GridFilterOperator.Contains, mainContactFilter)); 
            CommonHelper.CaptureScreen();
            if(filterMainContactTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Main Contact column load time is " + filterMainContactTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Main Contact column load time is " + filterMainContactTime + " seconds.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Main Contact", GridFilterOperator.NoFilter, "");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Filter Vendors Grid using Address column.</b></font>");
            var filterAddressTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.FilterItemInGrid("Address", GridFilterOperator.Contains, addressFilter));
            CommonHelper.CaptureScreen();
            if(filterAddressTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Address column load time is " + filterAddressTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Address column load time is " + filterAddressTime + " seconds.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Address", GridFilterOperator.NoFilter, "");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Filter Vendors Grid using Phone column.</b></font>");
            var filterPhoneTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.FilterItemInGrid("Phone", GridFilterOperator.Contains, phoneFilter));
            CommonHelper.CaptureScreen();
            if (filterPhoneTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Phone column load time is " + filterPhoneTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Phone column load time is " + filterPhoneTime + " seconds.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Phone", GridFilterOperator.NoFilter, "");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Filter Vendors Grid using filterEmailTime column.</b></font>");
            var filterEmailTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.FilterItemInGrid("Email", GridFilterOperator.Contains, emailFilter));
            CommonHelper.CaptureScreen();
            if (filterEmailTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Email column load time is " + filterEmailTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Email column load time is " + filterEmailTime + " seconds.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Email", GridFilterOperator.NoFilter, "");

            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Go to Vendor Grid page 2.</b></font>");
            var goToPage2Time = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.NavigateToPage(2));
            if(goToPage2Time <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Go to page 2 load time is " + goToPage2Time + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Go to page 2 load time is " + goToPage2Time + " seconds.</b></font>");
                      
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Go to Vendor Grid last page.</b></font>");
            var goToLastPageTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.NavigateToPage(VendorPage.Instance.GetJobGridPageCount()));
            if (goToLastPageTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Go to last page load time is " + goToLastPageTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Go to last page load time is " + goToLastPageTime + " seconds.</b></font>");


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Go to Vendor Grid first page.</b></font>");
            var goToFirstPageTime = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.NavigateToPage(1));
            if (goToLastPageTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Go to first page load time is " + goToLastPageTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Go to first page load time is " + goToLastPageTime + " seconds.</b></font>");

            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.11: Change page size to 500.</b></font>");
            var changePageSize500Time = CommonHelper.MeasureLoadTime(() => VendorPage.Instance.ChangePageSize(500));
            if (changePageSize500Time <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Change page size to 500 load time is " + changePageSize500Time + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Change page size to 500 load time is " + changePageSize500Time + " seconds.</b></font>");

        }
    }
}
