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
    public partial class RT_01018 : BaseTestScript
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
        [Category("Section_II"), Order(9)]
        public void I_ReportsMenu()
        {
            // Hover mouse to Report
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS);
            // Verify Report list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01018, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListREPORTS;
            //Assert.That(CommonHelper.IsEqual2List(expected, actual), "The list items of Report menu is not as expected. Actual results: " + CommonHelper.CastListToString(actual));

            if (CommonHelper.IsEqual2List(expected, actual) is false)
                ExtentReportsHelper.LogFail("<font color = 'red'>The list items of Report menu is not as expected.</font>" +
                    "<br>Actual results: " + CommonHelper.CastListToString(actual) +
                    "<br>Expected results: " + CommonHelper.CastListToString(expected));

            //********************* Verify URL ************************//////

            // Click on User Activity Log from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.UserActivityLog);

            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.DASHBOARD_USER_ACTIVITY_LOG_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"User Activity Log Page URL: {_current_URL}");
            }
            // Click on User Activity Log V3 from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.UserActivityLogV3);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.DASHBOARD_USER_ACTIVITY_LOG_V3_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"User Activity Log V3 Page URL: {_current_URL}");
            }
            // Click on Communities By House from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.CommunitiesByHouse);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BUILDER_COMMUNITIES_BY_HOUSE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Communities By House Page URL: {_current_URL}");
            }
            // Click on House by Communities from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.HousesByCommunity);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BUILDER_HOUSES_BY_COMMUNITY_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Houses By Community Page URL: {_current_URL}");
            }
            // Click on Items By Building Phase from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.ItemsByBuildingPhase);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.PRODUCTS_ITEMS_BY_BUILDING_PHASE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Items By Building Phase URL: {_current_URL}");
            }
            // Click on Products By House And Option from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.ProductsByHouseAndOption);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BOM_PRODUCTS_BY_HOUSE_AND_OPTION_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Products By House And Option Page URL: {_current_URL}");
            }
            // Click on Products Qtys By Houses And Opt from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.ProductQtysByHousesAndOpt);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BOM_PRODUCT_QTYS_BY_HOUSES_AND_OPT_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Products Qtys By Houses And Opt Page URL: {_current_URL}");
            }
            // Click on Option Price Margin Analysis from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.OptionPriceMarginAnalysis);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BOM_OPTION_PRICE_MARGIN_ANALYSIS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Price Margin Analysis Page URL: {_current_URL}");
            }
            // Click on House Bom Status Report from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.HouseBomStatusReport);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BOM_HOUSE_BOM_STATUS_REPORT_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"House BOM Status Report Page URL: {_current_URL}");
            }
            // Click on Pipeline Wms Bom Assembly Comparison from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.PipelineWmsBomAssemblyComparison);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BOM_PL_WMS_BOM_ASSEMBLY_COMPARISON_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Pipeline WMS BOM Assembly Comparison Page URL: {_current_URL}");
            }
            // Click on Pipeline Wms Bom Assembly Comparison SYNC from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.PipelineWmsBomAssemblyComparisonAndSync);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BOM_PL_WMS_BOM_ASSEMBLY_COMPARISON_AND_SYNC_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Pipeline WMS BOM Assembly Comparison Sync Page URL: {_current_URL}");
            }
            // Click on Trace Qty Down from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.TraceQtyDown);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.BOM_TRACE_QTY_DOWN_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Pipeline WMS BOM Assembly Comparison Sync Page URL: {_current_URL}");
            }
            // Click on Contract from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.Contract);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.COSTING_CONTRACT_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Contract Page URL: {_current_URL}");
            }
            // Click on Cost By Elevation from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.CostByElevation);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.COSTING_COST_BY_ELEVATION_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Cost By Elevation Page URL: {_current_URL}");
            }
            // Click on Cost Comparison By Elevation from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.CostComparisonByElevation);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.COSTING_COST_COMPARISON_BY_ELEVATION_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Cost Comparison By Elevation Page URL: {_current_URL}");
            }
            // Click on House Cost And Price Elevation from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.HouseCostAndPriceByElevation);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.COSTING_HOUSE_COST_AND_PRICE_BY_ELEVATION_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"House Cost And Price Elevation Page URL: {_current_URL}");
            }
            // Click on Option Cost And Price By House from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.OptionCostAndPriceByHouse);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.COSTING_OPTION_COST_AND_PRICE_BY_HOUSE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Cost And Price By House Page URL: {_current_URL}");
            }
            // Click on Option Cost from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.OptionCost);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.WMS_COSTING_OPTION_COST_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Cost Page URL: {_current_URL}");
            }
            // Click on Option Cost By Phase from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.OptionCostByPhase);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.WMS_COSTING_OPTION_COST_BY_PHASE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Cost By Phase Page URL: {_current_URL}");
            }
            // Click on Option Cost Comparison from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.OptionCostComparison);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.WMS_COSTING_OPTION_COST_COMPARISON_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Cost By Comparison Page URL: {_current_URL}");
            }
            // Click on Budgets from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.Budgets);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.view;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Budgets Page URL: {_current_URL}");
            }

            // Click on house options from Report menu
            DashboardPage.Instance.SelectMenu(MenuItems.REPORTS).SelectItem(ReportsMenu.HouseOptions);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.HOMEFRONT_HOUSE_OPTIONS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"House Option Page URL: {_current_URL}");
            }
            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}