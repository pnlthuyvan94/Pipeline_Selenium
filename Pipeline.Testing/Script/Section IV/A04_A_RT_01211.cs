using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A04_A_RT_01211 : BaseTestScript
    {
        private HouseData houseData_Old;
        private HouseData houseData_New;
        // Set up the Test Section Name for each Test case
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        [SetUp] // Pre-condition
        public void GetOldTestData()
        {
            houseData_Old = new HouseData()
            {
                HouseName = "QA_RT_Auto_House_RT_01211",
                SaleHouseName = "QA_RT_Auto_House_RT_01211_Sales_Name",
                Series = "RT_Series_DoNot_Delete",
                PlanNumber = "1211",
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

            // Step 1: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to House default page.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Filter house with name {houseData_Old.HouseName} and create if it doesn't exist.<b></b></font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData_Old.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData_Old.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(houseData_Old);
            }
            else
            {
                // Delete before create new data
                HousePage.Instance.DeleteHouse(houseData_Old.HouseName);
                // Create a new house
                HousePage.Instance.CreateHouse(houseData_Old);
            }
        }
        [Test]
        [Category("Section_IV")]
        public void A04_A_Assets_DetailPage_House_Details_UpdateHouse()
        {
            // Step 2: Populate all values
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2: Update house with new value</b></font>");
            houseData_New = new HouseData()
            {
                HouseName = "QA_RT_Auto_House_RT_01211_Update",
                SaleHouseName = "QA_RT_Auto_House_RT_01211_Sales_Name_Update",
                Series = "Visions",
                PlanNumber = "1211",
                BasePrice = "2000000",
                SQFTBasement = "2",
                SQFTFloor1 = "2",
                SQFTFloor2 = "2",
                SQFTHeated = "2",
                SQFTTotal = "8",
                Style = "Multi Family",
                Stories = "1",
                Bedrooms = "2",
                Bathrooms = "1",
                Garage = "2 Car",
                Description = "Test_Update"

            };
            HouseDetailPage.Instance.CreateUpdateAHouse(houseData_New);
            string updateMsg = $"House { houseData_New.HouseName} saved successfully!";
            if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                ExtentReportsHelper.LogPass(null, updateMsg);

            HouseDetailPage.Instance.RefreshPage();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3: Refreshing page and verify new value.</b></font>");

            // Verify items
            ExtentReportsHelper.LogPass($"'{HouseDetailPage.Instance.IsSavedWithCorrectValue(houseData_New)}'");

            // Verify new House in header
            //Assert.That(HouseDetailPage.Instance.IsHouseNameDisplaySuccessfullyOnBreadScrumb(houseData_New.HouseName, houseData_New.PlanNumber), "Create new House unsuccessfully");
            if (HouseDetailPage.Instance.IsHouseNameDisplaySuccessfullyOnBreadScrumb(houseData_New.HouseName, houseData_New.PlanNumber))
            {
                ExtentReportsHelper.LogPass("Create new House successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail("Create new House unsuccessfully");
            }

            // Step 4: Back to list of house and verify new item in grid view
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4: Back to list of house and verify new item in grid view.</b></font>");
            //HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Insert name to filter and click filter by Contain value
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData_New.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData_New.HouseName) is true)
                ExtentReportsHelper.LogPass($"New House was not display on grid., '{houseData_New.HouseName}'");
            else
                ExtentReportsHelper.LogFail("Can't find new house on the grid view.");

        }

        [TearDown]
        public void DeleteHouse()
        {
            // Step 5: Back to House default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5: Back to House default page and delete data</b></font>");
            //HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Filter old and new house then delete it
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData_Old.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData_Old.HouseName))
                HousePage.Instance.DeleteHouse(houseData_Old.HouseName);

            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData_New.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData_New.HouseName))
                HousePage.Instance.DeleteHouse(houseData_New.HouseName);
        }
    }
}
