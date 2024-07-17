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
    public partial class RT_01016 : BaseTestScript
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
        [Category("Section_II"), Order(6)]
        [Ignore("Pathway menu was removed from Pipeline, so this test sript will be ignored.")]
        public void F_PathWayMenu()
        {
            // Hover mouse to Pathway
            DashboardPage.Instance.SelectMenu(MenuItems.PATHWAY);
            // Verify Pathway list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01016, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListPATHWAY;
            Assert.That(CommonHelper.IsEqual2List(expected, actual), "The list items of Pathway menu is not as expected. Actual results: " + CommonHelper.CastListToString(actual));

            //********************* Verify URL ************************//////

            // Click on Houses from Pathway menu
            DashboardPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.Houses);

            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_PATHWAY_HOUSES_RULES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Houses Page URL: {_current_URL}");


            // Click on Assets from Pathway menu
            DashboardPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.Assets);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_PATHWAY_ASSETS_RULES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Assets Page URL: {_current_URL}");

            // Click on Asset Groups from Pathway menu
            DashboardPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.AssetGroups);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_PATHWAY_ASSET_GROUPS_RULES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Asset Groups Page URL: {_current_URL}");

            // Click on Designer Views from Pathway menu
            DashboardPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.DesignerViews);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_PATHWAY_DESIGNER_VIEWS_RULES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Designer Views Page URL: {_current_URL}");

            // Click on Designer Elements from Pathway menu
            DashboardPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.DesignerElements);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_PATHWAY_DESIGNER_ELEMENTS_RULES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Designer Elements Page: {_current_URL}");

            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


