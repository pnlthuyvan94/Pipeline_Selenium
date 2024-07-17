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
    public partial class RT_01017 : BaseTestScript
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
        [Category("Section_II"), Order(7)]
        [Ignore("Sales menu was removed from Pipeline, so this test sript will be ignored.")]
        public void G_SalesMenu()
        {
            // Hover mouse to Sale
            DashboardPage.Instance.SelectMenu(MenuItems.SALES);
            // Verify Sale list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01017, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListSALES;
            Assert.That(CommonHelper.IsEqual2List(expected, actual), "The list items of Sales menu is not as expected. Actual results: " + CommonHelper.CastListToString(actual));

            //********************* Verify URL ************************//////

            // Click on Prospects from Sale menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.Prospects);

            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_PROSPECTS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Prospects Page URL: {_current_URL}");


            // Click on Customers from Sale menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.Customers);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_SALE_CUSTOMER_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Customers Page URL: {_current_URL}");

            // Click on Jobs from Sale menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.Jobs);

            _current_URL = DashboardPage.Instance.CurrentURL;
             _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_SALE_JOBS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Jobs Page: {_current_URL}");

            // Click on SSSalesPrices from Sale menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.SSSalesPrices);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_SS_SALES_PRICES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"SS Sales Prices Page URL: {_current_URL}");

            // Click on Contract Documents Elements from Sale menu
            DashboardPage.Instance.SelectMenu(MenuItems.SALES).SelectItem(SalesMenu.ContractDocuments);

            _current_URL = DashboardPage.Instance.CurrentURL;
             _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_CONTRACT_DOCUMENTS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Contract Documents Page URL: {_current_URL}");

            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


