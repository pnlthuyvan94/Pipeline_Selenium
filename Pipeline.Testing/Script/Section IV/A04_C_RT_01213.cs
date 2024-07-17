using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.FloorPlans;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A04_C_RT_01213 : BaseTestScript
    {
        private HouseData houseData;
        // Set up the Test Section Name for each Test case
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        [SetUp] // Pre-condition
        public void GetOldTestData()
        {
            houseData = new HouseData()
            {
                HouseName = "QA_RT_Auto_House_RT_01213",
                SaleHouseName = "RegressionTest_House_Sales_Name",
                Series = "RT_Series_DoNot_Delete",
                PlanNumber = "1213",
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

            // Step 0: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Navigate to House default page</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.2: Filter house with name {houseData.HouseName} and create if it doesn't exist.</b></font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(houseData);
            }
            else
            {
                // Delete before create new data
                HousePage.Instance.DeleteHouse(houseData.HouseName);
                // Create a new house
                HousePage.Instance.CreateHouse(houseData);
            }
        }

        [Test]
        [Category("Section_IV")]
        public void A04_C_Assets_DetailPage_House_FloorPlans()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Floor Plans page from the leeft navigation.</b></font>");

            HouseDetailPage.Instance.LeftMenuNavigation("Floor Plans");

            var filePaths = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DataInputFiles/Resources", "*.*",
                                                       SearchOption.TopDirectoryOnly);
            var imgList = filePaths.Where(p => !p.EndsWith("pdf") && !p.EndsWith("xlsx") && !p.EndsWith("mpeg")).ToList();

            // Upload image for whole 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Upload file and verify it.</b></font>");

            foreach (FloorPlanLevel level in (FloorPlanLevel[])Enum.GetValues(typeof(FloorPlanLevel)))
            {
                foreach (var item in imgList)
                {
                    HouseFloorPlanDetailPage.Instance.UploadImageByLevelAndVerify(level, item);
                    HouseFloorPlanDetailPage.Instance.DeleteResourceByLevelAndVerify(level);
                }
            }
        }

        [TearDown]
        public void DeleteHouse()
        {
            // Step 3: Back to House default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3: Back to House default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Filter old and new house then delete it
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
                HousePage.Instance.DeleteHouse(houseData.HouseName);
        }
    }
}
