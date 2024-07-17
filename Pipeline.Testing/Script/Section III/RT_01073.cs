using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.Series;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A04_RT_01073 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        HouseData HouseData;
        SeriesData SeriesData;
        [SetUp]
        public void CreateTestData()
        {
            //Populate all values
            HouseData = new HouseData()
            {
                HouseName = "QA_RT_House_01073_Automation",
                SaleHouseName = "QA_RT_House_01073_Automation",
                Series = "QA_RT_Series_1077_Automation",
                PlanNumber = "1073",
                BasePrice = "1000000",
                SQFTBasement = "1",
                SQFTFloor1 = "1",
                SQFTFloor2 = "2",
                SQFTHeated = "3",
                SQFTTotal = "7",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "Test"
            };

            SeriesData = new SeriesData()
            {
                Name = "QA_RT_Series_1077_Automation",
                Code = "1077",
                Description = "Please do not delete this series, use for the automation purpose"
            };

            //Delete House Name Before Create New House
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            // Insert name to filter and click filter by Contain value
            HousePage.Instance.EnterHouseNameToFilter("Name", HouseData.HouseName);

            if (HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName))
            {
                HousePage.Instance.DeleteItemInGrid("Name", HouseData.HouseName);
                string successfulMess = $"House {HouseData.PlanNumber} {HouseData.HouseName} deleted successfully!";
                string actualMsg = HousePage.Instance.GetLastestToastMessage();
                if (successfulMess.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass("House deleted successfully!");
                    HousePage.Instance.CloseToastMessage();
                }
                else
                {
                    if (HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName))
                        ExtentReportsHelper.LogFail("The House could not be deleted.");
                    else
                        ExtentReportsHelper.LogPass("House deleted successfully!");
                }
            }

            //Navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            //click on "+" Add button
            HousePage.Instance.ClickAddToHouseIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_HOUSE_URL;
            Assert.That(HouseDetailPage.Instance.IsPageDisplayed(expectedURL), "House detail page isn't displayed");
            if (HouseDetailPage.Instance.IsSeriesInList(HouseData.Series) == true)
            {
                ExtentReportsHelper.LogInformation($"The Series Data with Name {SeriesData.Name} is exited in List");
            }
            else
            {
                //Navigate to this URL:  https://pipeline-dev45.sstsandbox.com/develop/Dashboard/Builder/Series/Default.aspx
                SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
                SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, SeriesData.Name);
                if (SeriesPage.Instance.IsItemInGrid("Name", SeriesData.Name) is true)
                {
                    ExtentReportsHelper.LogInformation($"The Series Data with Name {SeriesData.Name} is exted on grid");
                }
                else
                {
                    // Create a new series
                    SeriesPage.Instance.CreateSeries(SeriesData);
                }
            }

        }
        [Test]
        [Category("Section_III")]
        public void A04_Assets_AddAHouse()
        {

            // Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            // Check data in grid
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Filter house with name {HouseData.HouseName} and create if it doesn't exist.<b></b></font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HouseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName))
            {
                // Create a new house
                HousePage.Instance.DeleteHouse(HouseData.HouseName);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>house with name {HouseData.HouseName} isn't exit in grid.<b></b></font>");
            }

            // Step 2: click on "+" Add button
            HousePage.Instance.ClickAddToHouseIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_HOUSE_URL;
            if (HouseDetailPage.Instance.IsPageDisplayed(expectedURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>House create page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>House create page is displayed</b></font>");
            }

            // Step 3: Populate all values

            HouseDetailPage.Instance.CreateUpdateAHouse(HouseData);

            // 4. Select the 'Save' button on the modal;
            HouseDetailPage.Instance.Save();

            // 5. Verify new House in header
            //string breadcrumbTitle = ExcelFactory.GetRow(HousePage.Instance.MetaData, 22)[valueToFindColumn];
            Assert.That(HouseDetailPage.Instance.IsHouseNameDisplaySuccessfullyOnBreadScrumb(HouseData.HouseName, HouseData.PlanNumber), "Create new House unsuccessfully");
            ExtentReportsHelper.LogPass("Create successful House");

            // 6. Back to list of house and verify new item in grid view
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HouseData.HouseName);
            bool isFound = HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName);
            Assert.That(isFound, string.Format("New House \"{0} \" was not display on grid.", HouseData.HouseName));
        }

        [TearDown]
        public void CleanUpItem()
        {

            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            // Insert name to filter and click filter by Contain value
            HousePage.Instance.EnterHouseNameToFilter("Name", HouseData.HouseName);

            if (HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName))
            {
                HousePage.Instance.DeleteItemInGrid("Name", HouseData.HouseName);
                string successfulMess = $"House {HouseData.PlanNumber} {HouseData.HouseName} deleted successfully!";
                string actualMsg = HousePage.Instance.GetLastestToastMessage();
                if (successfulMess.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass("House deleted successfully!");
                    HousePage.Instance.CloseToastMessage();
                }
                else
                {
                    if (HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName))
                        ExtentReportsHelper.LogFail("The House could not be deleted.");
                    else
                        ExtentReportsHelper.LogPass("House deleted successfully!");
                }
            }
        }
    }
}
