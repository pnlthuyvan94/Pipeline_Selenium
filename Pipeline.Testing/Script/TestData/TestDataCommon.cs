using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.TestData
{
    public class TestDataCommon : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        OptionData option;
        CommunityData community;
        SeriesData series;
        HouseData housedata;
        LotData lotdata;
        DivisionData division;

        public const string DIVISON_NAME_DEFAULT = "QA_RT_Divsion_Automation";

        public const string JOB_NAME_DEFAULT = "QA_RT_Job_Automation";

        public const string OPTION_NAME_DEFAULT = "QA_RT_Option_Automation";
        public const string OPTION_CODE_DEFAULT = "0001";

        public const string COMMUNITY_CODE_DEFAULT = "Automation";
        public const string COMMUNITY_NAME_DEFAULT = "QA_RT_Community_Automation";

        public const string HOUSE_NAME_DEFAULT = "QA_RT_House_Automation";
        public const string HOUSE_CODE_DEFAULT = "0001";

        string[] OptionData = { OPTION_NAME_DEFAULT };

        private readonly int[] indexs = new int[] { };

        [Test]
        public void SetUpTestData()
        {
            division = new DivisionData()
            {
                Name = "QA_RT_Divsion_Automation",
                Address = "3990 IN 38",
                City = "Lafayette",
                State = "IN",
                Zip = "47905",
                Description = "Create a new Division",
            };
            var optionType = new List<bool>()
            {
                false, false, false
            };

            option = new OptionData()
            {

                Name = "QA_RT_Option_Automation",
                Number = "0001",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Types = optionType,
                Price = 0.00
            };

            community = new CommunityData()
            {
                Name = "QA_RT_Community_Automation",
                Division = "QA_RT_Divsion_Automation",
                City = "Ho Chi Minh",
                Code = "Automation",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Nothing to say v1",
                DrivingDirections = "Nothing to say v2",
                Slug = "QA_RT_Community_Automation"
            };


            housedata = new HouseData()
            {
                HouseName = "QA_RT_House_Automation",
                SaleHouseName = "The house which created by QA",
                Series = "QA_RT_Serie_Automation",
                PlanNumber = "0001",
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

            series = new SeriesData()
            {
                Name = "QA_RT_Serie_Automation",
                Code = "",
                Description = "Please no not remove or modify"
            };

            lotdata = new LotData()
            {
                Number = "_001",
                Status = "Sold"
            };

            ExtentReportsHelper.LogInformation(null, $"Open setting page, Turn OFF Sage 300 and MS NAV.");
            CommunityPage.Instance.SetSage300AndNAVStatus(false);

            ExtentReportsHelper.LogInformation(null, $"Open Lot page, verify Lot button displays or not.");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Try to open Lot URL of any community and verify Add lot button
            CommonHelper.OpenURL(BaseDashboardUrl + "/Builder/Communities/Lots.aspx?cid=1");
            if (LotPage.Instance.IsAddLotButtonDisplay() is false)
            {
                ExtentReportsHelper.LogWarning(null, $"<font color='lavender'><b>Add lot button doesn't display to continue testing. Stop this test script.</b></font>");
                Assert.Ignore("Add lot button doesn't display after set NAV and Sage 300 to Running. Stop this test script");
            }
            // Prepare data for Division Data
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, division.Name);
            if (DivisionPage.Instance.IsItemInGrid("Division", division.Name) is false)
            {
                //Create a new Divisions
                DivisionPage.Instance.CreateDivision(division);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, option.Number);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", option.Name) is false)
            {

                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed.</font>");
                }
                // Create Option - Click 'Save' Button
                OptionPage.Instance.AddOptionModal.AddOption(option);
                string _expectedMessage = $"Option Number is duplicated.";
                string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Option with name { option.Name} and Number {option.Number}.");
                    Assert.Fail($"Could not create Option.");
                }
                BasePage.PageLoad();

                return;                
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option with name { option.Name} is displayed in grid.</font>");



            //Prepare Community Data
            ExtentReportsHelper.LogInformation(null, "Prepare Community Page.");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {community.Name} is displayed in grid.</font>");
                CommunityPage.Instance.SelectItemInGrid("Name", community.Name);
            }
            else
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(community);
                string _expectedMessage = $"Could not create Community with name {community.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { community.Name}.");
                }

            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();

            //Add Option into Community
            ExtentReportsHelper.LogInformation(null, "Add Option into Community.");
            CommunityDetailPage.Instance.LeftMenuNavigation("Options");
            CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
            if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION_NAME_DEFAULT) is false)
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData);
            }
            //Navigate To Community Lot
            CommunityDetailPage.Instance.LeftMenuNavigation("Lots");
            string LotPageUrl = LotPage.Instance.CurrentURL;
            if (LotPage.Instance.IsItemInGrid("Number", lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", lotdata.Status))
            {
                ExtentReportsHelper.LogInformation($"Lot with Number {lotdata.Number} and Status {lotdata.Status} is displayed in grid");
            }
            else
            {
                //Import Lot in Community
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Open Import page.</b></font>");
                CommonHelper.SwitchLastestTab();
                LotPage.Instance.ImportExporFromMoreMenu("Import");
                string importFileDir = "Pipeline_Lots_In_Community.csv";
                LotPage.Instance.ImportFile("Lot Import", $@"\DataInputFiles\\Import\\TestData\\{importFileDir}");
                CommonHelper.OpenURL(LotPageUrl);

                //Check Lot Numbet in grid 
                if (LotPage.Instance.IsItemInGrid("Number", lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", lotdata.Status))
                {
                    ExtentReportsHelper.LogPass("Import Lot File is successful");
                }
                else
                {
                    ExtentReportsHelper.LogFail("Import Lot File is unsuccessful");
                }
            }
            //Prepare Series Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare to Series Page.</font>");
            // Go to the Series default page
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);

            // Filter the created series 
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, series.Name);

            // Verify the item is display in list
            if (!SeriesPage.Instance.IsItemInGrid("Name", series.Name))
            {
                // Create new series to test
                SeriesPage.Instance.ClickAddToSeriesModal();

                Assert.That(SeriesPage.Instance.AddSeriesModal.IsModalDisplayed(), "Add Series modal is not displayed.");

                SeriesPage.Instance.AddSeriesModal
                                         .EnterSeriesName(series.Name)
                                         .EnterSeriesCode(series.Code)
                                         .EnterSeriesDescription(series.Description);


                // Select the 'Save' button on the modal;
                SeriesPage.Instance.AddSeriesModal.Save();

                // Verify successful save and appropriate success message.
                string _expectedMessage = "Series " + series.Name + " created successfully!";
                string _actualMessage = SeriesPage.Instance.AddSeriesModal.GetLastestToastMessage();
                if (_expectedMessage.Equals(_actualMessage))
                {
                    ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                    SeriesPage.Instance.CloseToastMessage();
                }
                else
                    ExtentReportsHelper.LogFail($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");

                // Close modal
                //SeriesPage.Instance.AddSeriesModal.CloseModal();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Serires with name {series.Name} is displayed in grid.</font>");
            }

            //Prepare data for House Data
            ExtentReportsHelper.LogInformation(null, "Prepare data for House Data.");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {housedata.HouseName} and create if it doesn't exist.");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, housedata.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", housedata.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(housedata);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"House with Name {housedata.HouseName} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", housedata.HouseName);

            }

            // Navigate House Option And Add Option into House
            ExtentReportsHelper.LogInformation(null, $"Switch to House/ Options page. Add option '{OPTION_NAME_DEFAULT}' to house '{HOUSE_NAME_DEFAULT}' if it doesn't exist.");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            //Navigate House Communities And Check Community Data on grid
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");
            HouseCommunities.Instance.FillterOnGrid("Name", COMMUNITY_NAME_DEFAULT);
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT, indexs);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Community with Name {COMMUNITY_NAME_DEFAULT} is displayed in grid");
            }

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }
        }
}
