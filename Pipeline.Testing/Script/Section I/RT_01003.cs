using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Dashboard;
using System.Windows.Forms;

namespace Pipeline.Testing.Script.Section_I
{
    [TestFixture]
    public class RT_01003 : BaseTestScript
    {
        bool Flag = true;
        private const string ACTIVITY_FEED = "Activity Feed";

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_I);
        }

        [SetUp]
        public void SetupTest()
        {
            // Check current page
            if (!string.Equals(DashboardPage.Instance.CurrentURL, BaseDashboardUrl, System.StringComparison.OrdinalIgnoreCase))
                DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            ExtentReportsHelper.LogInformation($"<font color='lavender'><b>The current Resolution: ({screenWidth}, {screenHeight}).</b></font>");
        }

        [Test]
        [Category("Section_I"), Order(3)]
        public void C_ActivityFeedPanel()
        {
            string _message;
            // 0) Click on the Sort by button and verify the Sort by menu
            DashboardPage.Instance.SortActivity_Btn.Click();
            BasePage.PageLoad();
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01003, 1)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListMenu_SortActivity);
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = string.Format("The list items Sort menu of Activity Feed is not as expected.<br>Expected results: '{0}'.<br>Actual results: '{1}'.", _expected, _actual);
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Menu), _message);
                Flag = false;
            }
            else
            {
                _message = "The list items Sort menu of Activity Feed is expected.";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Menu), _message);
            }

            // 1) Sort by Date(Newest)
            // Newest Activity should display at the top of the list, then descend from top > bottom, Newest > oldest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Date(Newest).</b></font>");
            DashboardPage.Instance.Activity_Menu.SelectItem("Sort by Date (Newest)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(ACTIVITY_FEED);
            DashboardPage.Instance.WaitingActivityLoad();
            /*DashboardPage.Instance.WaitingActivityLoad();
            if (!DashboardPage.Instance.IsNewest_ActivityFeed)
            {

                _message = "Newest Activity does not display at the top of the list. Please refer the Activity Feed for more details.";
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Grid), _message);
                Flag = false;
            }
            else
            {
                _message = "Newest Activity is displayed at the top of the list.";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Grid), _message);
            }*/

            // 2) Sort by Date(Oldest)
            // Oldest Activity should display at the top of the list, then ascend from top > bottom, Oldest > Newest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Date(Oldest).</b></font>");
            DashboardPage.Instance.Activity_Menu.SelectItem("Sort by Date (Oldest)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(ACTIVITY_FEED);
            DashboardPage.Instance.WaitingActivityLoad();
            /*DashboardPage.Instance.WaitingActivityLoad();
            if (!DashboardPage.Instance.IsOldest_ActivityFeed)
            {
                _message = "Oldest Activity does not display at the top of the list. Please refer the Activity Feed for more details";
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Grid), _message);
                Flag = false;
            }
            else
            {
                _message = "Oldest Activity is displayed at the top of the list.";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Grid), _message);
            }*/

            // 3) Sort by Username(A - Z)
            // Username should display alphabetically in A - Z order from top to bottom:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Username(A - Z).</b></font>");
            DashboardPage.Instance.Activity_Menu.SelectItem("Sort by Username (A-Z)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(ACTIVITY_FEED);
            DashboardPage.Instance.WaitingActivityLoad();
            /*DashboardPage.Instance.WaitingActivityLoad();
            if (!DashboardPage.Instance.IsNameAscending_ActivityFeed)
            {
                _message = "Username does not display alphabetically in A - Z order from top to bottom. Please refer the Activity Feed for more details";
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Grid), _message);
                Flag = false;
            }
            else
            {
                _message = "Username is displayed alphabetically in A - Z order from top to bottom";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Grid), _message);
            }*/

            // 4) Sort by Username(Z - A)
            // Username should display alphabetically in Z - A order from top to bottom:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Username(Z - A).</b></font>");
            DashboardPage.Instance.Activity_Menu.SelectItem("Sort by Username (Z-A)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(ACTIVITY_FEED);
            DashboardPage.Instance.WaitingActivityLoad();
            /*DashboardPage.Instance.WaitingActivityLoad();
            if (!DashboardPage.Instance.IsNameDescending_ActivityFeed)
            {
                _message = "Username does not display alphabetically in Z - A order from top to bottom. Please refer the Activity Feed for more details";
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Grid), _message);
                Flag = false;
            }
            else
            {
                _message = "Username is displayed alphabetically in Z - A order from top to bottom.";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.Activity_Grid), _message);
            }*/

            // 5) View All Activity
            // The 'View All Activity' link(highlighted below) should be present
            // DashboardPage.Instance.ViewAllActivity_Btn.Click();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>View All Activity.</b></font>");
            DashboardPage.Instance.ViewAllActivity_Btn.WaitForElementIsVisible(5);
            string _actuaURL = DashboardPage.Instance.GetViewAllActivityHyperLink();
            string _expectedURL = BaseDashboardUrl + BaseMenuUrls.DASHBOARD_USER_ACTIVITY_LOG_URL;
            if (!AssertHelper.AreEqual(_expectedURL, _actuaURL))
            {
                _message = "After clicking on the 'View All Activity' link, the All Activity page does not display as expected.<br>Actual Result: " + _actuaURL
                    +"<br>Expected Result: " + _expectedURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "After clicking on the 'View All Activity' link, the All Activity page is displayed as expected.";
                ExtentReportsHelper.LogPass(_message);
            }
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }

    }
}
