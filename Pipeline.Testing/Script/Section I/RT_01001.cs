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
    public class RT_01001 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_I);
        }

        bool Flag = true;
        private void NavigateToDashboardPage()
        {
            // Check current page
            if (!string.Equals(DashboardPage.Instance.CurrentURL, BaseDashboardUrl, System.StringComparison.OrdinalIgnoreCase))
                DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);
        }

        [SetUp]
        public void SetupTest()
        {
            NavigateToDashboardPage();
            
        }

        [Test]
        [Category("Section_I"), Order(1)]
        public void A_DashBoardOverviewPanel()
        {
            Jobs();
            Houses();
            Communities();
            Options();
            Products();
            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }

        private void Jobs()
        {
            string _message;
            // Click on Jobs to show list
            DashboardPage.Instance.ClickJobToShowList();
            // Verify that list contains 2 item "View Jobs" and "Create New Job"
            string expectedList = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01001, 11)["Value"];
            string actualList = CommonHelper.CastListToString(DashboardPage.Instance.GetListJobs);
            if (!AssertHelper.AreEqual(expectedList, actualList))
            {
                _message = "Jobs's items are not as expected. Actual results: " + actualList;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "After cliking on the Jobs button, the list items are contains 'View Jobs' and 'Create New Job' as expected.";
                ExtentReportsHelper.LogPass(_message);
            }

            // View Jobs and verify the URL
            DashboardPage.Instance.ClickJobToShowList().ViewJobs();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_JOB_URL;
            string actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(expectedURL, actualURL))
            {
                _message = "The Jobs URL is not correct. Actual results: " + actualURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "The Job URL is correct. Actual results:" + actualURL;
                ExtentReportsHelper.LogPass(_message);
            }
            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);

            // Create a New Job and verify the URL
            DashboardPage.Instance.ClickJobToShowList().CreateNewJob();
            expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_JOB_URL;
            actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(expectedURL, actualURL))
            {
                _message = "Create a New Job URL is not correct. Actual results: " + actualURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "Create a New Job URL is correct. Actual results: " + actualURL;
                ExtentReportsHelper.LogPass(_message);
            }

            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);
        }

        private void Options()
        {
            string _message;
            // Click on Options to show list
            DashboardPage.Instance.ClickOptionToShowList();
            // Verify that list contains 2 item "View Options" and "Create New Option"
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01001, 14)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListOptions);
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = "Options's items are not as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "After cliking on the Options button, the list items are contains 'View Options' and 'Create New Option' as expected.";
                ExtentReportsHelper.LogPass(_message);
            }
            DashboardPage.Instance.ClickOptionToShowList().ViewOptions();
            string _expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL_FROM_DASHBOARD;
            string _actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(_expectedURL, _actualURL))
            {
                _message = "The Options URL is not correct. Actual results: " + _expectedURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "The Options URL is correct. Actual results: " + _actualURL;
                ExtentReportsHelper.LogPass(_message);
            }

            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);

            // Create a New Option and verify the URL
            DashboardPage.Instance.ClickOptionToShowList().CreateNewOption();
            _expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_OPTION_URL;
            _actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(_expectedURL, _actualURL))
            {
                _message = "Create a New Option URL is not correct. Actual results: " + _expectedURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "Create a New Option URL is correct. Actual results: " + _actualURL;
                ExtentReportsHelper.LogPass(_message);
                ExtentReportsHelper.LogInformation("Currently, we change the workflow of creating option, so we take a note here to update the script later!");
            }

            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);
        }

        private void Communities()
        {
            string _message;
            // Click on Communities to show list
            DashboardPage.Instance.ClickCommunityToShowList();
            // Verify that list contains 2 item "View Communities" and "Create New Community"
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01001, 13)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListCommunities);
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = "Communities's items are not as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "After cliking on the Communities button, the list items are contains 'View Communities' and 'Create New Community' as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogPass(_message);
            }

            DashboardPage.Instance.ClickCommunityToShowList().ViewCommunities();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL_FROM_DASHBOARD;
            string actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(expectedURL, actualURL))
            {
                _message = "The Communities URL is not correct. Actual results: " + actualURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "The Communities URL is correct. Actual results: " + actualURL;
                ExtentReportsHelper.LogPass(_message);
            }
            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);

            // Create a New Community and verify the URL
            DashboardPage.Instance.ClickCommunityToShowList().CreateNewCommunity();
            expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_COMMUNITY_URL;
            actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(expectedURL, actualURL))
            {
                _message = "Create a new Community URL is not correct. Actual results: " + actualURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "Create a New Community URL is correct. Actual results: " + actualURL;
                ExtentReportsHelper.LogPass(_message);
            }
            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);
        }

        private void Houses()
        {
            string _message;
            // Click on Houses to show list
            DashboardPage.Instance.ClickHouseToShowList();
            // Verify that list contains 2 item "View Houses" and "Create New House"
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01001, 12)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListHouses);
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = "House's items are not as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "After cliking on the Houses button, the list items are contains 'View Houses' and 'Create New House' as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogPass(_message);
            }

            DashboardPage.Instance.ClickHouseToShowList().ViewHouses();
            string _expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL_FROM_DASHBOARD;
            string _actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(_expectedURL, _actualURL))
            {
                _message = "Houses URL is not correct. Actual results: " + _actualURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "Houses URL is correct. Actual results: " + _actualURL;
                ExtentReportsHelper.LogPass(_message);
            }
            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);

            // Create a New House and verify the URL
            DashboardPage.Instance.ClickHouseToShowList().CreateNewHouse();
            _expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_HOUSE_URL;
            _actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(_expectedURL, _actualURL))
            {
                _message = "Create a New House URL is not correct. Actual results: " + _actualURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "Create a New House URL is correct. Actual results: " + _actualURL;
                ExtentReportsHelper.LogPass(_message);
            }

            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);
        }

        private void Products()
        {
            string _message;
            // Click on Products to show list
            DashboardPage.Instance.ClickProductToShowList();
            // Verify that list contains 2 item "View Products" and "Create New Product"
            string _expected = ExcelFactory.GetRow(DashboardPage.Instance.TestData_RT01001, 15)["Value"];
            string _actual = CommonHelper.CastListToString(DashboardPage.Instance.GetListProducts);
            if (!AssertHelper.AreEqual(_expected, _actual))
            {
                _message = "Products's list items are not as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "After cliking on the Products button, the list items are contains 'View Products' and 'Create New Product' as expected. Actual results: " + _actual;
                ExtentReportsHelper.LogPass(_message);
            }

            DashboardPage.Instance.ClickProductToShowList().ViewProducts();
            string _expectedURL = BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL;
            string _actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(_expectedURL, _actualURL))
            {
                _message = "Products URL is not as expected. Actual results: " + _actualURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "Products URL is correct. Actual results: " + _actualURL;
                ExtentReportsHelper.LogPass(_message);
            }

            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);

            // Create a New Product and verify the URL
            DashboardPage.Instance.ClickProductToShowList().CreateNewProduct();
            _expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
            _actualURL = DashboardPage.Instance.CurrentURL;
            if (!AssertHelper.AreEqual(_expectedURL, _actualURL))
            {
                _message = "Create a New Products URL is not as expected. Actual results: " + _actualURL;
                ExtentReportsHelper.LogWarning(_message);
                Flag = false;
            }
            else
            {
                _message = "Create a New Products URL is correct. Actual results: " + _actualURL;
                ExtentReportsHelper.LogPass(_message);
            }

            // Back to Dashboard
            DashboardPage.Instance.SelectMenu(MenuItems.DASHBOARD, true);
        }
    }
}
