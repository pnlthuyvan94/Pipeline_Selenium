using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Dashboard;

namespace Pipeline.Testing.Script.Section_I
{
    [TestFixture]
    public partial class RT_01005 : BaseTestScript
    {
        bool Flag = true;

        [SetUp]
        public void SetupTest()
        {
            // Check current page
            if (!string.Equals(DashboardPage.Instance.CurrentURL, BaseDashboardUrl, System.StringComparison.OrdinalIgnoreCase))
                DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);
        }

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_I);
        }

        [Test]
        [Category("Section_I"), Order(5)]
        public void E_RecentJobsPanel()
        {
            string _message;
            // 0) Click on the Sort by button and verify the Sort by menu
            DashboardPage.Instance.SortJobs_Btn.Click();
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01005, 1)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListMenu_SortRecentJobs);
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                Flag = false;
                _message = "The list items Sort menu of Activity Feed is not as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogWarning(_message);
            }
            else
            {
                _message = "The list items Sort menu of Activity Feed is displayed as expected.";
                ExtentReportsHelper.LogPass(_message);
            }

            //TODO: Waiting dev fix the issue, currently we will comment this code
            // 1) Sort by Recently Added
            // Newest Added Job should display at the top of the list, then descend from top > bottom, Newest > oldest:
            //DashboardPage.Instance.SortJobs_Menu.SelectItem(0);
            //DashboardPage.Instance.WaitingRecentJobsLoad();
            //AssertHelper.IsTrue(DashboardPage.Instance.IsListDisplayRecentlyJobsAdded, "Newest Added Job does not display at the top of the list. The newest added Jobs are: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListNewestAddedJobs), ref Log);

            // 2) View(Job name) Details
            // Selecting the View(Job name) Details link(s) should direct the user to the Job Details page for which Job was selected from the list:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>View(Job name) Details.</b></font>");
            var targetJob_URL = DashboardPage.Instance.GetJobURL(0);
            var targetJob_Name = DashboardPage.Instance.GetListJobsName_RecentActiveJobs[0];
            CommonHelper.OpenLinkInNewTab(targetJob_URL);
            CommonHelper.SwitchTab(1);
            BasePage.PageLoad();
            bool result = DashboardPage.Instance.IsJobDetailsPageOpened(targetJob_Name);
            if (!result)
            {
                _message = "Details link(s) Job does not direct the user to the Job Details page. Please review it again.";
                Flag = false;
                ExtentReportsHelper.LogWarning(_message);
            }
            else
            {
                _message = string.Format("Selecting the View({0}) Details link(s) direct the user to the Job Details page", targetJob_Name);
                ExtentReportsHelper.LogPass(_message);
            }


            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            // 3) Expand carats on listed Jobs
            // Clicking a carat next to a Job name should expand details for that Job while closing the detailed view of other Jobs in the view:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Expand carats on listed Jobs.</b></font>");
            DashboardPage.Instance.ClickingCaratToExpand_Job(1);
            result = DashboardPage.Instance.IsJobExpanded(1);
            if (!result)
            {
                Flag = false;
                _message = "Clicking a carat next to a Job name does not expand details for that Job while opening the detailed view of other Jobs in the view.";
                ExtentReportsHelper.LogWarning(_message);
            }
            else
            {
                _message = "Clicking a carat next to a Job name is expanded details as expected.";
                ExtentReportsHelper.LogPass(_message);
            }

            result = DashboardPage.Instance.IsJobExpanded(0);
            if (result)
            {
                Flag = false;
                _message = "Clicking a carat next to a Job name, the previous Job does not collapse details.";
                ExtentReportsHelper.LogWarning(_message);
            }
            else
            {
                _message = "Clicking a carat next to a Job name, the previous Job collapse details as expected.";
                ExtentReportsHelper.LogPass(_message);
            }

            // 4) See all Jobs
            // The 'See all Jobs' link should be available as seen in the below example:
            // Clicking the link should direct the user to http://dev.bimpipeline.com/Dashboard/Builder/Jobs/:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>See all Jobs.</b></font>");
            DashboardPage.Instance.ViewAllJobs_Btn.Click();
            BasePage.PageLoad();
            string _expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_JOB_URL;
            string _actualURL = DashboardPage.Instance.CurrentURL;

            if (!AssertHelper.AreEqual(_expectedURL, _actualURL))
            {
                Flag = false;
                _message = "Clicking the link does not direct the user to http://dev.bimpipeline.com/Dashboard/Builder/Jobs/. Actual results: " + _actualURL;
                ExtentReportsHelper.LogWarning(_message);
            }
            else
            {
                _message = "Clicking the link direct the user to http://dev.bimpipeline.com/Dashboard/Builder/Jobs/ as expected.";
                ExtentReportsHelper.LogPass(_message);
            }

            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }

    }
}
