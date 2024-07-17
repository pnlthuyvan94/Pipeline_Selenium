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
    public partial class RT_01020 : BaseTestScript
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
        [Category("Section_II"), Order(11)]
        public void K_ProfileMenu()
        {
            // Hover mouse to Profile
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true);
            System.Threading.Thread.Sleep(1000);
            // Verify Profile list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01020, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListPROFILE;

            if (actual[0].Contains(expected[0])) expected[0] = actual[0]; //Then we know it's the profile menu item

            if (CommonHelper.IsEqual2List(expected, actual) is false)
                ExtentReportsHelper.LogFail("<font color = 'red'>The list items of Profile menu is not as expected.</font>" +
                    "<br>Actual results: " + CommonHelper.CastListToString(actual) +
                    "<br>Expected results: " + CommonHelper.CastListToString(expected));

            //********************* Verify URL ************************//////

            // Click on User Profile from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(actual[0], true);

            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.PROFILE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"User Profile Page URL: {_current_URL}");

            // Click on Subscriptions from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Subscriptions);

            _current_URL = DashboardPage.Instance.CurrentURL;
             _expected_URL = BaseDashboardUrl + BaseMenuUrls.SUBCRIPTIONS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Subscriptions Page URL: {_current_URL}");

            // Click on Admin Dashboard from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.AdminDashboard);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.ADMIN_DASHBOARD_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Admin Dashboard Page: {_current_URL}");

            // Click on Users from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Users);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.ADMIN_USERS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Users Page URL: {_current_URL}");

            // Click on Roles from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Roles);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.ADMIN_ROLES_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Roles Page URL: {_current_URL}");

            // Click on Settings from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Settings);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.ADMIN_SETTINGS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Settings Page: {_current_URL}");

            // Click on Queue from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Queue);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.ADMIN_QUEUE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Queue Page URL: {_current_URL}");

            // Click on Generated Reports from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.GeneratedReports);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.ADMIN_GENERATED_REPORTS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Generated Reports Page URL: {_current_URL}");

            // Click on Documents from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Documents);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.ADMIN_DOCUMENTS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Documents Page URL: {_current_URL}");

            // Click on Scheduled Task from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.ScheduledTask);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.ADMIN_SCHEDULED_TASKS_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Scheduled Task Page URL: {_current_URL}");

            // Click on Playbook Template from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.PlaybookTemplate);
            // Switch to last window and check
            CommonHelper.SwitchLastestTab();
            BasePage.PageLoad();
            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.PLAYBOOK_TEMPLATE_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Playbook Template Page URL: {_current_URL}");
                CommonHelper.CloseCurrentTab();
            }

            // Click on Logout from Profile menu
            CommonHelper.SwitchTab(0);
            // Click on Help from Profile menu
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Help);
            // Switch to last window and check
            CommonHelper.SwitchLastestTab();
            BasePage.PageLoad();
            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseMenuUrls.VIEW_HELP;
            if (!_current_URL.EndsWith(_expected_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Help Page URL: {_current_URL}");
                CommonHelper.CloseCurrentTab();
            }
            // Click on Logout from Profile menu
            CommonHelper.SwitchTab(0);
            DashboardPage.Instance.SelectMenu(MenuItems.PROFILE, true).SelectItem(ProfileMenu.Logout);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = "logout";
            if (!_current_URL.Contains(_expected_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format("This page does not redirect to the Log Out page containing the string '{0}' in the URL as expected. Actual: {1}", _expected_URL, _current_URL));
                Flag = false;
            }
            else
            {
                var _message = string.Format("This page is redirected to the Log Out page with URL '{0}' as expected. ", _current_URL);
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(), _message);
            }

            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


