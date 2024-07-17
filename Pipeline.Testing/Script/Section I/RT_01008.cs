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
    public partial class RT_01008 : BaseTestScript
    {
        bool Flag = true;
        private const string RECENT_PRODUCTS = "Recent Products";

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
        [Category("Section_I"), Order(8)]
        public void H_RecentProductsPanel()
        {
            string _message;
            // 0) Click on the Sort by button and verify the Sort by menu
            DashboardPage.Instance.SortProducts_Btn.Click();
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01008, 1)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListMenu_SortProducts);
            CommonHelper.ScrollToEndOfPage();
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = "The list items Sort menu of Products  is not as expected. Actual results: " + _actual;
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "The list items Sort menu of Products  is displayed as expected.";
                ExtentReportsHelper.LogPass(_message);
            }

            // 1) Sort by Name (A-Z)	
            // Name should display alphabetically in A-Z order from top to bottom:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Name (A-Z).</b></font>");
            DashboardPage.Instance.SortProducts_Menu.SelectItem("Sort by Name (A-Z)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_PRODUCTS);

            /*bool _result = VerifyProductsFinishedSorting("A-Z");
            if (!_result)
            {
                _message = "Name of Products do not display alphabetically in A-Z order from top to bottom. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListProductsFromDB_ByNameASC);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Name of Products displayed alphabetically in A-Z order from top to bottom.";
                ExtentReportsHelper.LogPass(_message);
            }*/

            // 2) Sort by Name (Z-A)	
            // Name should display alphabetically in Z-A order from top to bottom:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Name (Z-A).</b></font>");
            DashboardPage.Instance.SortProducts_Menu.SelectItem("Sort by Name (Z-A)");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_PRODUCTS);

            /*_result = VerifyProductsFinishedSorting("Z-A");
            if (!_result)
            {
                _message = "Name of Products do not display alphabetically in Z-A order from top to bottom. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListProductsFromDB_ByNameDESC);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Name of Products displayed alphabetically in Z-A order from top to bottom.";
                ExtentReportsHelper.LogPass(_message);
            }*/

            // 3) Sort by Date Create (Newest)	
            // Newest Products should display at the top of the list,then descend from top>bottom, Newest>oldest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Date Create (Newest).</b></font>");
            DashboardPage.Instance.SortProducts_Menu.SelectItem("Sort by Newest");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_PRODUCTS);

            /*_result = VerifyProductsFinishedSorting("Newest");
            if (!_result)
            {
                _message = "Newest Products do not display at the top of the list, then descend from top>bottom, Newest > Oldest. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListProductsFromDB_Newest);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Newest Products displayed at the top of the list, then descend from top>bottom, Newest > Oldest.";
                ExtentReportsHelper.LogPass(_message);
            }*/

            // 4) Sort by Date Create (Oldest)	
            // Oldest Products should display at the top of the list,then ascend from top>bottom, Oldest>Newest:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sort by Date Create (Oldest).</b></font>");
            DashboardPage.Instance.SortProducts_Menu.SelectItem("Sort by Oldest");
            DashboardPage.Instance.VerifyLoadingIconOnPanel(RECENT_PRODUCTS);

            /*_result = VerifyProductsFinishedSorting("Oldest");
            if (!_result)
            {
                _message = "Oldest Products do not display at the top of the list, then descend from top>bottom,Oldest > Newest. Expected results: " + CommonHelper.CastListToString(DashboardPage.Instance.GetListProductsFromDB_Oldest);
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Oldest Products displayed at the top of the list, then descend from top>bottom,Oldest > Newest.";
                ExtentReportsHelper.LogPass(_message);
            }*/


            // 5) Clicking the link should direct the user to the following page as seen in this example:
            // http://dev.bimpipeline.com/Dashboard/Builder/Products/
            // Selecting a community link directs the user to the Product details page for that community:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Clicking the link should direct the user to the Product detail page.</b></font>");
            var targetProduct_URL = DashboardPage.Instance.GetProductURL(0);
            var targetProduct_Name = DashboardPage.Instance.GetListProductsNameOnGrid[0];
            CommonHelper.OpenLinkInNewTab(targetProduct_URL);
            CommonHelper.SwitchTab(1);
            BasePage.PageLoad();
            bool result = DashboardPage.Instance.IsProductsDetailsPageOpened(targetProduct_Name);
            if (!result)
            {
                _message = "Details link(s) Product does not direct the user to the Product Details page. Please review it again.";
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "Details link(s) Product direct the user to the Product Details page.";
                ExtentReportsHelper.LogPass(_message);
            }

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            // 6) See all Products	
            // The 'See all Products' link (highlighted below) should be present:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>See all Products.</b></font>");
            DashboardPage.Instance.ViewAllProducts_Btn.Click();
            BasePage.PageLoad();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL;
            string actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(expectedURL, actualURL))
            {
                _message = "After clicking on the 'See all Products' the page does not direct to the Products URL. Actual result: " + actualURL;
                Flag = false;
                ExtentReportsHelper.LogWarning( _message);
            }
            else
            {
                _message = "After clicking on the 'See all Products' the page direct to the Products URL.";
                ExtentReportsHelper.LogPass(_message);
            }

            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }
        private bool VerifyProductsFinishedSorting(string sorting_type = "A-Z")
        {
            bool isPageLoaded;

            try
            {
                DashboardPage.Instance.WaitingProductsLoad();
            }
            catch (Exception ex)
            {
                string _message = "Failed to wait for the products elements to finish rendering:  " + ex.StackTrace.ToString();
                ExtentReportsHelper.LogWarning(_message);
            }

            System.Threading.Thread.Sleep(667);

            isPageLoaded = BasePage.JQueryLoad();

            if (!isPageLoaded) return false;

            //make sure the menu is still within view
            CommonHelper.MoveToElementWithoutCaptureAndCenter(DashboardPage.Instance.Products_Grid.GetWrappedControl());

            System.Threading.Thread.Sleep(667);

            switch (sorting_type)
            {
                case "A-Z":
                    return DashboardPage.Instance.IsNameAscending_Products;
                case "Z-A":
                    return DashboardPage.Instance.IsNameDescending_Products;
                case "Newest":
                    return DashboardPage.Instance.IsNewest_Products;
                case "Oldest":
                    return DashboardPage.Instance.IsOldest_Products;
                default:
                    return false;
            }
        }
    }
}
