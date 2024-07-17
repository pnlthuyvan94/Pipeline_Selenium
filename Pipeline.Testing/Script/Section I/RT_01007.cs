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
    public partial class RT_01007 : BaseTestScript
    {
        bool Flag = true;
        private const string RECENT_OPTION = "Recent Options";

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
        [Category("Section_I"), Order(7)]
        public void G_RecentOptionsPanel()
        {
            string _message;
            // 0) Click on the Sort by button and verify the Sort by menu
            DashboardPage.Instance.SortOptions_Btn.Click();
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01007, 1)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListMenu_SortOptions);
            //CommonHelper.ScrollToEndOfPage();
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = "The list items Sort menu of Options is not as expected. Expeted results: " + _expected;
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "The list items Sort menu of Options is displayed as expected.";
                ExtentReportsHelper.LogPass( _message);
            }
            // 1) Sort by Username (A-Z)	
            // Name should display alphabetically in A-Z order from top to bottom:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Username (A-Z).</b></font>");
            DashboardPage.Instance.SortOptions_Menu.SelectItem("Sort by Name (A-Z)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_OPTION);
            /*DashboardPage.Instance.WaitingOptionsLoad();
            //CommonHelper.ScrollToEndOfPage();
            if (!DashboardPage.Instance.IsNameAscending_Options)
            {
                _message = "Name of Options do not display alphabetically in A-Z order from top to bottom. (This can occur sometimes depending on server is running with Dev database or Beta databse) Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListOptionsFromDB_ByNameASC);
                //Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Name of Options are displayed alphabetically in A-Z order from top to bottom.";
                ExtentReportsHelper.LogPass( _message);
            }*/

            // 2) Sort by Username (Z-A)	
            // Username should display alphabetically in Z-A order from top to bottom:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Username (Z-A).</b></font>");
            DashboardPage.Instance.SortOptions_Menu.SelectItem("Sort by Name (Z-A)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_OPTION);

            /*DashboardPage.Instance.WaitingOptionsLoad();
            //CommonHelper.ScrollToEndOfPage();
            if (!DashboardPage.Instance.IsNameDescending_Options)
            {
                _message = "Name of Options do not display alphabetically in Z-A order from top to bottom. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListOptionsFromDB_ByNameDESC);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Name of Options are displayed alphabetically in Z-A order from top to bottom.";
                ExtentReportsHelper.LogPass( _message);
            }*/


            // 3) Sort by Date Create (Newest)	
            // Newest Options should display at the top of the list,then descend from top>bottom, Newest>oldest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Date Create (Newest).</b></font>");
            DashboardPage.Instance.SortOptions_Menu.SelectItem("Sort by Newest");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_OPTION);

            /*DashboardPage.Instance.WaitingOptionsLoad();
            //CommonHelper.ScrollToEndOfPage();
            if (!DashboardPage.Instance.IsNewest_Options)
            {
                _message = "Newest Options do not display at the top of the list, then descend from top>bottom, Newest > Oldest. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListOptionsFromDB_Newest);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Newest Options are displayed at the top of the list, then descend from top>bottom, Newest > Oldest.";
                ExtentReportsHelper.LogPass( _message);
            }*/


            // 4) Sort by Date Create (Oldest)	
            // Oldest Options should display at the top of the list,then ascend from top>bottom, Oldest>Newest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Date Create (Oldest).</b></font>");
            DashboardPage.Instance.SortOptions_Menu.SelectItem("Sort by Oldest");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_OPTION);
            
            /*DashboardPage.Instance.WaitingOptionsLoad();
            //CommonHelper.ScrollToEndOfPage();
            if (!DashboardPage.Instance.IsOldest_Options)
            {
                _message = "Oldest Options do not display at the top of the list, then descend from top>bottom, Oldest > Newest. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListOptionsFromDB_Oldest);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Oldest Options are displayed at the top of the list, then descend from top>bottom, Oldest > Newest.";
                ExtentReportsHelper.LogPass( _message);
            }*/


            // 5) Clicking the link should direct the user to the following page as seen in this example:
            // http://dev.bimpipeline.com/Dashboard/Builder/Options/
            // Selecting a Option link directs the user to the Option details page for that Option:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Clicking the link should direct the user to Option detail page.</b></font>");
            var targetOption_URL = DashboardPage.Instance.GetOptionURL(0);
            var targetOption_Name = DashboardPage.Instance.GetListOptionsNameOnGrid[0];
            CommonHelper.OpenLinkInNewTab(targetOption_URL);
            CommonHelper.SwitchTab(1);
            BasePage.PageLoad();
            bool result = DashboardPage.Instance.IsOptionsDetailsPageOpened(targetOption_Name);
            if (!result)
            {
                _message = "Details link(s) Option does not direct the user to the Option Details page. Please review it again.";
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Details link(s) Option direct the user to the Option Details page.";
                ExtentReportsHelper.LogPass( _message);
            }

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            // 6) See all Options	
            // The 'See all Options' link (highlighted below) should be present:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>See all Options.</b></font>");
            DashboardPage.Instance.ViewAllOptions_Btn.Click();
            BasePage.PageLoad();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL;
            string actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(expectedURL, actualURL))
            {
                _message = "After clicking on the 'See all Options' the page does not direct to the Options URL. Actual result: " + actualURL;
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "After clicking on the 'See all Options' the page direct to the Options URL as expected.";
                ExtentReportsHelper.LogPass( _message);
            }

            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }

    }
}
