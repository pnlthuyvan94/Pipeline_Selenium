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
    public partial class RT_01071 : BaseTestScript
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
        [Category("Section_II"), Order(8)]
        [Ignore("SalesPricing menu was removed from Pipeline, so this test sript will be ignored.")]
        public void H_SalesPricingMenu()
        {
            // Hover mouse to Purchasing
            DashboardPage.Instance.SelectMenu(MenuItems.SALESPRICING);
            // Verify Purchasing list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01071, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListSALESPRICING;
            // Assert.That(CommonHelper.IsEqual2List(expected, actual), "The list items of Purchasing menu is not as expected. Actual results: " + CommonHelper.CastListToString(actual));

            if (CommonHelper.IsEqual2List(expected, actual) is false)
                ExtentReportsHelper.LogFail("<font color = 'red'>The list items of Sale Pricing menu is not as expected.</font>" +
                    "<br>Actual results: " + CommonHelper.CastListToString(actual) +
                    "<br>Expected results: " + CommonHelper.CastListToString(expected));

            //********************* Verify URL ************************//////

            // Click on All Purchase Orders from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALESPRICING).SelectItem(SalesPricingMenu.OptionGroupRules);
            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.OPTION_GROUP_RULES_URL; 
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(null, $"Option Group Rules Page URL: {_current_URL}");
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Option Group Rules Page:");
            }
            // Click on Work Completed from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALESPRICING).SelectItem(SalesPricingMenu.OptionRules);
            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.OPTION_RULES_URL; 
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(null, $"Option Rules Page URL: {_current_URL}");
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Option Rules Page:");
            }
            // Click on Work Completed from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALESPRICING).SelectItem(SalesPricingMenu.OptionsPricing);
            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.OPTION_PRICING_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(null, $"Options Pricing Page URL: {_current_URL}");
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Options Pricing Page:");
            }
            // Click on pENDING from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALESPRICING).SelectItem(SalesPricingMenu.PendingFuturePrices);
            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.PENDING_FUTURE_PRICES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(null, $"Pending Future Prices Page URL: {_current_URL}");
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Pending Future Prices Page:");
            }
            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


