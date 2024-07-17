using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Dashboard;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_II
{
    // Inherit the BaseTestScript
    public partial class RT_01014 : BaseTestScript
    {
        readonly string _compareMessage = @"The Actual and Expected value does not match.<br>Actual Result  :'{1}'<br>Expected Result:'{0}'.";

        bool Flag = true;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_II);
        }

        #region"Test case"
        // The testcase run only 1 time
        [Test]
        [Category("Section_II"), Order(4)]
        public void D_CostingMenu()
        {
            // Hover mouse to Costings
            DashboardPage.Instance.SelectMenu(MenuItems.COSTING);
            // Verify Costings list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01014, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListCOSTING;
            //TODO: Need update
            if (!CommonHelper.IsEqual2List(expected, actual))
            {
                ExtentReportsHelper.LogWarning("The list items of Costings menu is not as expected. Actual results: " + CommonHelper.CastListToString(actual));
            }
            //********************* Verify URL ************************//////

            // Click on Vendors from COSTING menu
            DashboardPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);

            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_VENDORS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Vendors Page URL: {_current_URL}");

            // Click on CommunitySalesTax from COSTING menu
            DashboardPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.CommunitySalesTax);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_SALES_TAX_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Community SalesTax Page URL: {_current_URL}");

            // Click on TaxGroups from COSTING menu
            DashboardPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.TaxGroups);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_TAX_GROUP_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"TaxGroups Page Page URL: {_current_URL}");

            // Click on Cost Comparison from COSTING menu
            DashboardPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.CostComparison);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_COST_COMPARISON_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Cost Comparison Page URL: {_current_URL}");

            // Click on Option Bid Cost from COSTING menu
            DashboardPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.OptionBidCost);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_BID_COST_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Bid Cost Page URL: {_current_URL}");

            // Click on House Estimate from COSTING menu
            DashboardPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.CostingEstimate);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_COST_ESTIMATE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"House Estimate page URL: {_current_URL}");

            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


