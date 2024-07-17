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
    public partial class RT_01012 : BaseTestScript
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
        [Category("Section_II"), Order(2)]
        public void B_EstimatingMenu()
        {
            // Hover mouse to PRODUCTS
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING);
            // Verify PRODUCTS list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01012, "Value"));
            expected.Remove("BUILDING TRADES");
            IList<string> actual = DashboardPage.Instance.GetListPRODUCTS;
            
            if (CommonHelper.IsEqual2List(expected, actual) is false)
                ExtentReportsHelper.LogFail("<font color = 'red'>The list items of Estimating menu is not as expected.</font>" +
                    "<br>Actual results: " + CommonHelper.CastListToString(actual) +
                    "<br>Expected results: " + CommonHelper.CastListToString(expected));


            //********************* Verify URL ************************//////

            // Click on Products from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);

            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL; 
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Products Page URL: {_current_URL}");
            }

            /* TODO: Remove building trade from code
             // Click on Building Trades from PRODUCTS menu
             DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingTrades);

             _current_URL = DashboardPage.Instance.CurrentURL;
             _expected_URL = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01012, 2)["URL"].ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain"));
             if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
             {
                 ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                 Flag = false;
             }
             // Capture current screen information
             if (_current_URL == _expected_URL)
             {
                 ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Building Trades Page URL: {_current_URL}");
             }
             */

            // Click on Building Groups from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL; 
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Building Groups Page URL: {_current_URL}");
            }
            // Click on Building Phases from PRODUCTS menu

            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Building Phases Page URL: {_current_URL}");
            }
            // Click on Quantity Building Phase Rules from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.QuanttityPhaseRules);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_QUANTITY_BUILDING_PHASE_RULES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Quantity Building Phases Rules Page URL: {_current_URL}");
            }
            // Click on BOM Building Phase Rules from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BomPhaseRules);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_BOM_BUILDING_PHASE_RULES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"BOM Building Phases Rules Page URL: {_current_URL}");
            }
            // Click on Building Phase Types from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhaseTypes);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASE_TYPES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Building Phases Types Page URL: {_current_URL}");
            }
            // Click on Types from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Styles Page URL: {_current_URL}");
            }
            // Click on Styles Import Rules from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.StyleImportRules);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_IMPORT_RULE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Style Import Phase Rules Page URL: {_current_URL}");
            }
            // Click on Manufactures from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"ManufacturesPage URL: {_current_URL}");
            }
            // Click on USES from ESTIMATING menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_USES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Uses Page URL: {_current_URL}");
            }
            // Click on Units from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Units);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_UNITS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Units Page URL: {_current_URL}");
            }
            // Click on Categories from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Categories);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_CATEGORIES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Categories Page URL: {_current_URL}");
            }
            // Click on Calculations from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Calculations);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_CALCULATION_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Calculations Page URL: {_current_URL}");
            }
            // Click on Spec Sets from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Spec Sets Page URL: {_current_URL}");
            }
            // Click on Worksheet from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_WORKSHEETS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Worksheet Page URL: {_current_URL}");
            }

            // Click on Worksheet from PRODUCTS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BOMLogicRules);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_BOM_LOGIC_RULES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            if (_current_URL == _expected_URL)
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"BOM Logic Rules Page URL: {_current_URL}");
            }
            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


