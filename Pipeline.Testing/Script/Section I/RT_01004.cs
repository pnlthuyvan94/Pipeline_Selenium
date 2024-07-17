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
    public partial class RT_01004 : BaseTestScript
    {
        bool Flag = true;
        private const string RECENT_HOUSES = "Recent Houses";

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
        }

        [Test]
        [Category("Section_I"), Order(4)]
        public void D_RecentHousesPanel()
        {
            string _message;
            // 0) Click on the Sort by button and verify the Sort by menu
            DashboardPage.Instance.SortHouses_Btn.Click();
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01004, 1)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListMenu_SortRecentHouses);
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = "The list items Sort menu of Recent Houses is not as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.SortHouses_Menu), _message);
                Flag = false;
            }
            else
            {
                _message = "The list items Sort menu of Recent Houses is expected.";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.SortHouses_Menu), _message);
            }


            // 1) Sort by Recently Added
            // Newest Added House plan should display at the top of the list, then descend from top > bottom, Newest > oldest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Recently Added.</b></font>");
            DashboardPage.Instance.SortHouses_Menu.SelectItem(0);
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_HOUSES);
            DashboardPage.Instance.WaitingRecentHousesLoad();
            //System.Threading.Thread.Sleep(10000);
            /*DashboardPage.Instance.WaitingRecentHousesLoad();
            if (!DashboardPage.Instance.IsListDisplayRecentlyHousesAdded)
            {
                _message = "Newest Added House plans do not display at the top of the list. The newest added houses are: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListNewestAddedHouses);
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.Houses_Grid), _message);
                Flag = false;
            }
            else
            {
                _message = "Newest Added House plans are displayed at the top of the list";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.Houses_Grid), _message);
            }*/


            // 2) View(House name) Details
            // Selecting the View(House name) Details link(s) should direct the user to the House Details page for which house was selected from the list:
            var targetHouse_URL = DashboardPage.Instance.GetHousesURL(0);
            var targetHouse_Name = DashboardPage.Instance.GetListHouseName_RecentHouses[0];
            CommonHelper.OpenLinkInNewTab(targetHouse_URL);
            CommonHelper.SwitchTab(1);
            BasePage.PageLoad();
            if (!DashboardPage.Instance.IsHouseDetailsPageOpened(targetHouse_Name))
            {
                _message = "Details link(s) house does not direct the user to the House Details page.";
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = string.Format("Selecting the View({0}) Details link(s) direct the user to the House Details page for which '{0}' house was selected from the list", targetHouse_Name);
                ExtentReportsHelper.LogPass(_message);
            }


            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            // 3) Expand carats on listed Houses
            // Clicking a carat next to a House name should expand details for that house while closing the detailed view of other houses in the view:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Expand carats on listed Houses.</b></font>");
            DashboardPage.Instance.ClickingCaratToExpand_House(1);
            if (!DashboardPage.Instance.IsHouseExpanded(1))
            {
                _message = "Clicking a carat next to a House name, the current house does not expand details.";
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.Houses_Grid), _message);
                Flag = false;
            }
            else
            {
                _message = "Clicking a carat next to a House name, the current house expands details as expected.";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.Houses_Grid), _message);
            }
            if (DashboardPage.Instance.IsHouseExpanded(0))
            {
                _message = "Clicking a carat next to a House name, the previous house does not collapse details.";
                ExtentReportsHelper.LogWarning(CommonHelper.CaptureScreen(DashboardPage.Instance.Houses_Grid), _message);
                Flag = false;
            }
            else
            {
                _message = "Clicking a carat next to a House name, the other house collapses details as expected.";
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(DashboardPage.Instance.Houses_Grid), _message);
            }

            // 4) See all Houses
            // The e 'See all Houses' link should be available as seen in the below example:
            // Clicking the link should direct the user tohttp://dev.bimpipeline.com/Dashboard/Builder/Houses/:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>See all Houses.</b></font>");
            DashboardPage.Instance.ViewAllHouses_Btn.Click();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL_FROM_DASHBOARD;
            if (!AssertHelper.AreEqual(expectedURL, DashboardPage.Instance.CurrentURL))
            {
                _message = "See all Houses link does not direct the user to the index of all Houses.";
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "Successfully navigated to the Houses index from See all Houses link.";
                ExtentReportsHelper.LogPass(_message);
            }

            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }

    }
}
