using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Dashboard;
using System;

namespace Pipeline.Testing.Script.Section_I
{
    [TestFixture]
    public partial class RT_01006 : BaseTestScript
    {
        bool Flag = true;
        private const string RECENT_COMMUNITIES = "Recent Communities";

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
        [Category("Section_I"), Order(6)]
        public void F_RecentCommunitiesPanel()
        {
            string _message;
            // 0) Click on the Sort by button and verify the Sort by menu
            DashboardPage.Instance.SortCommunities_Btn.Click();
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01006, 1)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListMenu_SortCommunities);
            CommonHelper.MoveToElementWithoutCaptureAndCenter(DashboardPage.Instance._communities_Grid.GetWrappedControl());
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = "The list items Sort menu of Communities  is not as expected. Actual results: " + _actual;
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "The list items Sort menu of Communities  is displayed as expected.";
                ExtentReportsHelper.LogPass( _message);
            }

            // 1) Sort by Username (A-Z)	
            // Name should display alphabetically in A-Z order from top to bottom:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Username (A-Z).</b></font>");
            DashboardPage.Instance.SortCommunities_Menu.SelectItem("Sort by Name (A-Z)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_COMMUNITIES);

            /*bool _result = VerifyCommunitiesFinishedSorting("A-Z");
            if (!_result)
            {
                _message = "Name of Communities do not display alphabetically in A-Z order from top to bottom. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListCommunitiesFromDB_ByNameASC);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "The list items Sort menu of Communities  is displayed as expected.";
                ExtentReportsHelper.LogPass( _message);
            }*/

            // 2) Sort by Username (Z-A)	
            // Username should display alphabetically in Z-A order from top to bottom:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Username (Z-A).</b></font>");
            DashboardPage.Instance.SortCommunities_Menu.SelectItem("Sort by Name (Z-A)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_COMMUNITIES);
            /*_result = VerifyCommunitiesFinishedSorting("Z-A");
            if (!_result)
            {
                _message = "Name of Communities do not display alphabetically in Z-A order from top to bottom. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListCommunitiesFromDB_ByNameDESC);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Name of Communities displays alphabetically in Z-A order from top to bottom.";
                ExtentReportsHelper.LogPass( _message);
            }*/

            // 3) Sort by Date Create (Newest)	
            // Newest Communities should display at the top of the list,then descend from top>bottom, Newest>oldest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Date Create (Newest).</b></font>");
            DashboardPage.Instance.SortCommunities_Menu.SelectItem("Sort by Newest");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_COMMUNITIES);
            /*_result = VerifyCommunitiesFinishedSorting("Newest");
            if (!_result)
            {
                _message = "Newest Communities do not display at the top of the list, then descend from top>bottom, Newest > Oldest. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListCommunitiesFromDB_Newest);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Newest Communities is displayed at the top of the list.";
                ExtentReportsHelper.LogPass( _message);
            }*/

            // 4) Sort by Date Create (Oldest)	
            // Oldest Communities should display at the top of the list,then ascend from top>bottom, Oldest>Newest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Date Create (Oldest).</b></font>");
            DashboardPage.Instance.SortCommunities_Menu.SelectItem("Sort by Oldest");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_COMMUNITIES);
            /*_result = VerifyCommunitiesFinishedSorting("Oldest");
            if (!_result)
            {
                _message = "Oldest Communities do not display at the top of the list, then descend from top>bottom,Oldest > Newest. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListCommunitiesFromDB_Oldest);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Oldest Communities is displayed at the top of the list.";
                ExtentReportsHelper.LogPass( _message);
            }*/


            // 5) Clicking the link should direct the user to the following page as seen in this example:
            // http://dev.bimpipeline.com/Dashboard/Builder/Communities/
            // Selecting a community link directs the user to the community details page for that community:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Clicking the link, should direct to community detail page.</b></font>");
            var targetCommunity_URL = DashboardPage.Instance.GetCommunityURL(0);
            var targetCommunity_Name = DashboardPage.Instance.GetListCommunitiesNameOnGrid[0];
            CommonHelper.OpenLinkInNewTab(targetCommunity_URL);
            CommonHelper.SwitchTab(1);
            BasePage.PageLoad();
            bool _result = DashboardPage.Instance.IsCommunitiesDetailsPageOpened(targetCommunity_Name);
            if (!_result)
            {
                _message = "Details link(s) Community does not direct the user to the Community Details page. Please review it again.";
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Details link(s) Community direct the user to the Community Details page.";
                ExtentReportsHelper.LogPass( _message);
            }

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            // 6) See all Communities	
            // The 'See all Communities' link (highlighted below) should be present:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>See all Communities.</b></font>");
            DashboardPage.Instance.ViewAllCommunities_Btn.Click();
            BasePage.PageLoad();
            string _expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL_FROM_DASHBOARD;
            string _actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(_expectedURL, _actualURL))
            {
                _message = "After clicking 'See all Communities' link, the URL does not as expected. Actual: " + _actualURL;
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "After clicking 'See all Communities' link, the URL direct as expected.";
                ExtentReportsHelper.LogPass( _message);
            }
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }

        private bool VerifyCommunitiesFinishedSorting(string sorting_type = "A-Z")
        {
            bool isPageLoaded;

            try
            {
                DashboardPage.Instance.WaitingCommunitiesLoad();
            }
            catch (Exception ex)
            {
                string _message = "Failed to wait for the communities elements to finish rendering:  " + ex.StackTrace.ToString();
                ExtentReportsHelper.LogWarning(_message);
            }

            System.Threading.Thread.Sleep(667);

            isPageLoaded = BasePage.JQueryLoad();

            System.Threading.Thread.Sleep(667);

            //make sure the menu is still within view
            CommonHelper.MoveToElementWithoutCaptureAndCenter(DashboardPage.Instance._communities_Grid.GetWrappedControl());

            System.Threading.Thread.Sleep(667);
            
            if (!isPageLoaded) return false;

            switch (sorting_type)
            {
                case "A-Z":
                    return DashboardPage.Instance.IsNameAscending_Communities;
                case "Z-A":
                    return DashboardPage.Instance.IsNameDescending_Communities;
                case "Newest":
                    return DashboardPage.Instance.IsNewest_Communities;
                case "Oldest":
                    return DashboardPage.Instance.IsOldest_Communities;
                default:
                    return false;
            }
        }
    }
}
