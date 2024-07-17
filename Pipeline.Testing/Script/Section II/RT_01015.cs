using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Dashboard;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_II
{
    // Inherit the BaseTestScript
    public partial class RT_01015 : BaseTestScript
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
        [Category("Section_II"), Order(5)]
        public void E_PurchasingMenu()
        {
            // Hover mouse to Purchasing=
            Menu this_menu = DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING);
            // Verify Purchasing list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01015, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListPURCHASING;

            if (CommonHelper.IsEqual2List(expected, actual) is false)
                ExtentReportsHelper.LogFail("<font color = 'red'>The list items of Purchasing menu is not as expected.</font>" +
                    "<br>Actual results: " + CommonHelper.CastListToString(actual) +
                    "<br>Expected results: " + CommonHelper.CastListToString(expected));

            //********************* Verify URL ************************//////

            this_menu.SelectItem(PurchasingMenu.PuschaseOrders, false);
            this_menu.SelectItem(PurchasingMenu.ManageAllPurchaseOrders, true);
            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_MANAGE_ALL_PO_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"All Purchase Orders Page URL: {_current_URL}");
            }
            // Click on Work Completed from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.WorkCompleted);
            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_WORK_COMPLETED_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Work Completed Page URL: {_current_URL}");
            }
            // Click on approve for payment from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ApprovedForPayment);
            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_APPROVE_FOR_PAYMENT_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Approved For Payment Page URL: {_current_URL}");
            }

            // Click on Trades from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_TRADES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Trades Page URL: {_current_URL}");
            }

            // Click on Building Phase from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_PURCHASING_BUILDING_PHASES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Building Phase Page URL: {_current_URL}");
            }

            // Click on Release Groups from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ReleaseGroups);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_RELEASE_GROUPS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Release Groups Page URL: {_current_URL}");
            }
            // Click on Cost Codes from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_COST_CODES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Cost Codes Page URL: {_current_URL}");
            }
            // Click on Cost Types from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostTypes);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_COST_TYPES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Cost Types Page URL: {_current_URL}");
            }
            // Click on Cost Categories from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_COST_CATEGORIES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Cost Categories Page URL: {_current_URL}");
            }

            // Cut Off Phase was removed from Purchasing menu
            /*
            // Click on Cut Off Phases from Purchasing menu
            DashboardPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CutoffPhases);

            _current_URL = DashboardPage.Instance.CurrentURL;
            //_expected_URL = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01015, 7)["URL"].ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain"));
            _expected_URL = CommonHelper.ConvertWithNewDomain(ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01015, 7)["URL"].ToString());
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Cut Off Phases Page URL: {_current_URL}");
            }
            */

            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}