using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A04_B_RT_01212 : BaseTestScript
    {
        private HouseData old_TestData;
        private OptionData _optionData_ELEV_A;
        private OptionData _optionData_ELEV_B;
        private OptionData _optionData_3_Car_Garage;
        private OptionData _optionData_2_Car_Side;
        private OptionData[] _optionData_List;
        // Set up the Test Section Name for each Test case
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private string[] elevations = { "ELEV-A", "ELEV-B" };
        private string[] options = { "000 Test", "000 Test1" };

        [SetUp] // Pre-condition
        public void GetOldTestData()
        {
            old_TestData = new HouseData()
            {
                HouseName = "QA_RT_Auto_House_RT_01212",
                SaleHouseName = "RegressionTest_House_Sales_Name",
                Series = "RT_Series_DoNot_Delete",
                PlanNumber = "1212",
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

            var optionType = new List<bool>()
            {
                true, false, false
            };
          
            _optionData_ELEV_A = new OptionData()
            {
                Name = "ELEV-A",
                Number = "10001",
                Description = "Elevation A",
                SaleDescription = "Elevation A",
                OptionGroup = "Elevations",
                CostGroup = "Elevations",
                OptionType = "Structural",
                Price = 0.00,
                Types = optionType
            };
             _optionData_ELEV_B = new OptionData()
            {
                Name = "ELEV-B",
                Number = "10002",
                 Description = "Elevation B",
                 SaleDescription = "Elevation B",
                 OptionGroup = "Elevations",
                 CostGroup = "Elevations",
                 OptionType = "Structural",
                 Price = 0.00,
                Types = optionType
            };
            _optionData_3_Car_Garage = new OptionData()
            {
                Name = "3_Car_Garage",
                Number = "3CCCC",
                 Description = "3 Car Garage",
                 SaleDescription = "3 Car Garage",
                 OptionGroup = "Garages",
                 OptionType = "Structural",
                 Price = 0.00,
                Types = optionType
            };
            _optionData_2_Car_Side = new OptionData()
            {
                Name = "2_Car_Side",
                Number = "10202",
                 Description = "2 Car Side Garage",
                 SaleDescription = "2 Car Side Garage",
                 OptionGroup = "Garages",
                 OptionType = "Structural",
                 Price = 0.00,
                Types = optionType
            };

            _optionData_List = new OptionData[]
            {
                _optionData_ELEV_A,
                _optionData_ELEV_B,
                _optionData_2_Car_Side,
                _optionData_3_Car_Garage
            };

            //STep 0 Create new Option
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Add an new Option with 'Global' button of selected Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Create Option name 'ELEV-A'</b></font>");
            foreach (var item in _optionData_List)
            {
                OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, item.Name);
                if (OptionPage.Instance.IsItemInGrid("Name", item.Name) is false)
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Create new Option</b></font>");
                    // Create a new one
                    OptionPage.Instance.CreateNewOption(item);
                }
                else
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Do nothing</b></font>");
                    // If option isn't existing then do nothing
                }
            }

            // Step 1: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            //HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            // Insert name to filter and click filter by Contain value
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, old_TestData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", old_TestData.HouseName)is false)
            {
                // Step 2: click on "+" Add button
                HousePage.Instance.CreateHouse(old_TestData);
            }
            else
            {
                // Delete before create new data
                HousePage.Instance.DeleteHouse(old_TestData.HouseName);
                // Create a new house
                HousePage.Instance.CreateHouse(old_TestData);
            }

            // Close all tab exclude the current one
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }

        [Test]
        [Category("Section_IV")]
        public void A04_B_Assets_DetailPage_House_Options()
        {
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            // Elevations
            // Click add to show Add elevation modal
            HouseOptionDetailPage.Instance.ClickAddElevationToShowModal().InsertElevationToHouse(elevations);
            string expectedMsg = "Option(s) added to house successfully";
            string actualMsg = HouseOptionDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("Elevation(s) added to house successfully");
                //HouseOptionDetailPage.Instance.CloseToastMessage();
            }

            //HouseOptionDetailPage.Instance.CloseElevationModal();
            if (!HouseOptionDetailPage.Instance.IsItemInElevationGridWithTextContains("Name", elevations))
                ExtentReportsHelper.LogFail("Elevation(s) is NOT add to house successfully");

            ExtentReportsHelper.LogInformation(null, "================= Try to edit Sqft =================");
            HouseOptionDetailPage.Instance.ClickEditOnElevation("Name", elevations.First());
            HouseOptionDetailPage.Instance.EditItemOnElevationGridAndVerify("Name", elevations.First(), "Sqft", "9999");

            ExtentReportsHelper.LogInformation(null, "================= Try to edit Price =================");
            HouseOptionDetailPage.Instance.ClickEditOnElevation("Name", elevations.Last());
            HouseOptionDetailPage.Instance.EditItemOnElevationGridAndVerify("Name", elevations.Last(), "Price", "9876.00");

            // Upload img
            ExtentReportsHelper.LogInformation(null, "================= Try to upload file to elevation =================");

            string localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\UploadImg";
            string[] filePaths = Directory.GetFiles(localPath, "*.*", SearchOption.TopDirectoryOnly);

            HouseOptionDetailPage.Instance.ClickImgButtonToShowUploadSection(elevations.First());
            foreach (var namePath in filePaths)
            {
                var extentionName = Path.GetExtension(namePath);
                ExtentReportsHelper.LogInformation(null, $"================= Try to upload file type <font color='green'><b>{extentionName}</b></font> =================");
                HouseOptionDetailPage.Instance.UploadImgForElevationAndVerify(namePath);
            }

            HouseOptionDetailPage.Instance.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "================= Find elevation on grid =================");
         //   HouseOptionDetailPage.Instance.FilterItemInElevationGrid("Name", GridFilterOperator.Contains, elevations.First());
         
            if (!HouseOptionDetailPage.Instance.IsItemInElevationGridWithTextContains("Name", elevations.First()))
                ExtentReportsHelper.LogFail($"Elevation with name <b>{elevations.First()}</b> is NOT displayed after filtered on Elevation Grid.");
            HouseOptionDetailPage.Instance.OpenElevationInNewTabAndVerify(elevations.First());

            ExtentReportsHelper.LogInformation(null, "================= Try to delete on elevation grid =================");
            HouseOptionDetailPage.Instance.DeleteItemInElevation("Name", elevations.First());
            expectedMsg = $"Option {elevations.First()} successfully removed from this House";
            actualMsg = HouseOptionDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Elevation with name <b>{elevations.First()}</b> is deleted successfully.");
                //HouseOptionDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (!HouseOptionDetailPage.Instance.IsItemInElevationGridWithTextContains(elevations.First()))
                    ExtentReportsHelper.LogPass($"Elevation with name <b>{elevations.First()}</b> is deleted successfully.");
                else
                {
                    ExtentReportsHelper.LogFail($"Elevation with name <b>{elevations.First()}</b> is NOT delete successfully.<br>Expected: <b>{expectedMsg}</b>.<br>Actual: <b>{actualMsg}</b>");
                    //HouseOptionDetailPage.Instance.CloseToastMessage();
                }
            }

         //   HouseOptionDetailPage.Instance.FilterItemInElevationGrid("Name", GridFilterOperator.NotIsEmpty, string.Empty);
            HouseOptionDetailPage.Instance.DeleteItemInElevation("Name", elevations.Last());
            expectedMsg = $"Option {elevations.Last()} successfully removed from this House";
            actualMsg = HouseOptionDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Elevation with name <b>{elevations.Last()}</b> is deleted successfully.");
                //HouseOptionDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (!HouseOptionDetailPage.Instance.IsItemInElevationGridWithTextContains(elevations.Last()))
                    ExtentReportsHelper.LogPass($"Elevation with name <b>{elevations.Last()}</b> is deleted successfully.");
                else
                {
                    ExtentReportsHelper.LogFail($"Elevation with name <b>{elevations.Last()}</b> is NOT delete successfully.");
                    //HouseOptionDetailPage.Instance.CloseToastMessage();
                }
            }

            // Option
            // Click add to show Add Option modal
            HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(options);
            expectedMsg = "Option(s) added to house successfully";
            actualMsg = HouseOptionDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("Option(s) added to house successfully");
                //HouseOptionDetailPage.Instance.CloseToastMessage();
            }

            //HouseOptionDetailPage.Instance.CloseOptionModal();
            if (!HouseOptionDetailPage.Instance.IsItemInOptionGridWithTextContains("Name", options))
                ExtentReportsHelper.LogFail("Option(s) is NOT add to house successfully");

            ExtentReportsHelper.LogInformation(null, "================= Try to edit Sqft =================");
            HouseOptionDetailPage.Instance.ClickEditOnOption("Name", options.First());
            HouseOptionDetailPage.Instance.EditItemOnOptionGridAndVerify("Name", options.First(), "Sqft", "8888");

            ExtentReportsHelper.LogInformation(null, "================= Try to edit Price =================");
            HouseOptionDetailPage.Instance.ClickEditOnOption("Name", options.Last());
            HouseOptionDetailPage.Instance.EditItemOnOptionGridAndVerify("Name", options.Last(), "Price", "1234.00");

            ExtentReportsHelper.LogInformation(null, "================= Try to filter on Option grid =================");
            HouseOptionDetailPage.Instance.FilterItemInOptionnGrid("Name", GridFilterOperator.Contains, options.First());
            if (!HouseOptionDetailPage.Instance.IsItemInOptionGridWithTextContains("Name", options.First()))
                ExtentReportsHelper.LogFail($"Option with name <b>{options.First()}</b> is NOT displayed after filtered on Option Grid.");
            HouseOptionDetailPage.Instance.OpenOptionInNewTabAndVerify(options.First());
            ExtentReportsHelper.LogInformation(null, "================= Try to delete on Option grid =================");
            HouseOptionDetailPage.Instance.DeleteItemInOption("Name", options.First());
            expectedMsg = $"Option {options.First()} successfully removed from this House";
            actualMsg = HouseOptionDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Option with name <b>{options.First()}</b> is deleted successfully.");
                //HouseOptionDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (!HouseOptionDetailPage.Instance.IsItemInOptionGridWithTextContains(options.First()))
                    ExtentReportsHelper.LogPass($"Option with name <b>{options.First()}</b> is deleted successfully.");
                else
                {
                    ExtentReportsHelper.LogFail($"Option with name <b>{options.First()}</b> is NOT delete successfully.");
                    //HouseOptionDetailPage.Instance.CloseToastMessage();
                }
            }

            HouseOptionDetailPage.Instance.FilterItemInOptionnGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            HouseOptionDetailPage.Instance.DeleteItemInOption("Name", options.Last());
            expectedMsg = $"Option {options.Last()} successfully removed from this House";
            actualMsg = HouseOptionDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Option with name <b>{options.Last()}</b> is deleted successfully.");
                //HouseOptionDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (!HouseOptionDetailPage.Instance.IsItemInOptionGridWithTextContains(options.Last()))
                    ExtentReportsHelper.LogPass($"Option with name <b>{options.Last()}</b> is deleted successfully.");
                else
                {
                    ExtentReportsHelper.LogFail($"Option with name <b>{options.Last()}</b> is NOT delete successfully.");
                    // HouseOptionDetailPage.Instance.CloseToastMessage();
                }
            }
        }

        [TearDown]
        public void UpdateName()
        {
            // Back to House default page and delete data
            //HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            // Filter house then delete it
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, old_TestData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", old_TestData.HouseName))
                HousePage.Instance.DeleteHouse(old_TestData.HouseName);
        }
    }
}
