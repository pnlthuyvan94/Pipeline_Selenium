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
    public partial class RT_01011 : BaseTestScript
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
        [Category("Section_II"), Order(1)]
        public void A_AssetsMenu()
        {
            // Hover mouse to ASSETS
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS);
            // Verify ASSETS list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01011, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListASSETS;
            Assert.That(CommonHelper.IsEqual2List(expected, actual), "The list items of ASSETS menu is not as expected. Actual results: " + CommonHelper.CastListToString(actual) + " \n Expected: " + CommonHelper.CastListToString(expected));

            //********************* Verify URL ************************//////

            // Click on Divisions from ASSETS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);

            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Divisions Page URL: {_current_URL}");
            }

            // Click on Communities from ASSETS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Communities Page URL: {_current_URL}");
            }

            // Click on Series from ASSETS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Series Page URL: {_current_URL}");
            }

            // Click on House from ASSETS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Houses Page URL: {_current_URL}");
            }

            // Click on Options from ASSETS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Options Page URL: {_current_URL}");
            }

            // Click on Options Groups from ASSETS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionGroups);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_GROUP_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Groups Page URL: {_current_URL}");
            }

            //// Click on Options Rooms from ASSETS menu
            //DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionRooms);

            //_current_URL = DashboardPage.Instance.CurrentURL;
            //_expected_URL = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01011, 7)["URL"].ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain"));
            //if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            //{
            //    ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
            //    Flag = false;
            //}
            //else
            //{
            //    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Rooms Page URL: {_current_URL}");
            //}

            //// Click on Option Selections from ASSETS menu
            //DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelections);

            //_current_URL = DashboardPage.Instance.CurrentURL;
            //_expected_URL = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01011, 8)["URL"].ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain"));
            //if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            //{
            //    ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
            //    Flag = false;
            //}
            //else
            //{
            //    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Selections Page URL: {_current_URL}");
            //}

            //// Click on Option Selection Group from ASSETS menu
            //DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelectionGroups);

            //_current_URL = DashboardPage.Instance.CurrentURL;
            //_expected_URL = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01011, 9)["URL"].ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain"));
            //if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            //{
            //    ExtentReportsHelper.LogWarning( string.Format(_compareMessage, _expected_URL, _current_URL));
            //    Flag = false;
            //}
            //else
            //{
            //    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Selection Groups Page URL: {_current_URL}");
            //}

            // Click on Option Types from ASSETS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionTypes);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_TYPE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Option Types Page URL: {_current_URL}");
            }

            // Click on Custom Options from ASSETS menu
            DashboardPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.CustomOptions);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Custom Options Page URL: {_current_URL}");
            }

            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


