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
    public partial class RT_01013 : BaseTestScript
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
        [Category("Section_II"), Order(3)]
        public void C_JobsMenu()
        {
            // Hover mouse to Jobs
            DashboardPage.Instance.SelectMenu(MenuItems.JOBS);
            // Verify Jobs list
            IList<string> expected = new List<string>(ExcelFactory.GetListByColumn(DashboardPage.Instance.TestData_RT01013, "Value"));
            IList<string> actual = DashboardPage.Instance.GetListJOBS;
            Assert.That(CommonHelper.IsEqual2List(expected, actual), "The list items of Jobs menu is not as expected. Actual results: " + CommonHelper.CastListToString(actual));
            //********************* Verify URL ************************//////

            // Click on All Jobs from Jobs menu
            DashboardPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            string _current_URL = DashboardPage.Instance.CurrentURL;
            string _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_JOB_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"All Jobs Page URL : {_current_URL}");

            // Click on Active Jobs from Jobs menu
            DashboardPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.ActiveJobs);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_ACTIVE_JOB_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Active Jobs Page URL: {_current_URL}");

            // Click on Completed Jobs from Jobs menu

            DashboardPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.CompleteJobs);

            _current_URL = DashboardPage.Instance.CurrentURL;
            _expected_URL = BaseDashboardUrl + BaseMenuUrls.VIEW_COMPLETED_JOB_URL;
            if (!AssertHelper.AreEqual(_expected_URL, _current_URL))
            {
                ExtentReportsHelper.LogWarning(string.Format(_compareMessage, _expected_URL, _current_URL));
                Flag = false;
            }
            // Capture current screen information
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Completed Jobs Page: {_current_URL}");

            // Assert the testscript
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        #endregion

    }
}


