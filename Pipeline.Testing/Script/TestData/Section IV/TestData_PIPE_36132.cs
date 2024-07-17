using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.AvailablePlan;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.Quantities.HouseOptionCopyQuantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using System.Collections.Generic;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.Options.Products;

namespace Pipeline.Testing.Script.TestData_Section_IV
{
    class TestData_PIPE_36132 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);
        }

        OptionData optionData1, optionData2;
        CommunityData communityData;
        SeriesData seriesData;
        HouseData houseData1, houseData2;
        BuildingGroupData buildingGroupData;
        BuildingPhaseData buildingPhaseData;
        ManufacturerData manuData;
        StyleData styleData;
        ProductData productData;

        private const string ImportType = "Pre-Import Modification";

        [Test]
        public void SetUpTestData_A04_R_Assets_Detail_Pages_Houses_Quantities_Copy_House_Quantities_need_to_include_parameters()
        {

            //--PART 1 Create information

            var optionTypeGlobalF = new List<bool>()
            {
                false, false, false
            };
            var optionTypeGlobalT = new List<bool>()
            {
                false, false, true
            };
            optionData1 = new OptionData()
            {
                Name = "RT_QA_Automation_Option1_36132",
                Number = string.Empty,
                SquareFootage = 0,
                Description = "FOR 188",
                SaleDescription = "188_1",
                OptionGroup = "",
                OptionRoom = string.Empty,
                CostGroup = "All Rooms",
                OptionType = "Design",
                Price = 188.00,
                Types = optionTypeGlobalF
            };
            optionData2 = new OptionData()
            {
                Name = "RT_QA_Automation_Option2_36132",
                Number = string.Empty,
                SquareFootage = 0,
                Description = "FOR 188",
                SaleDescription = "188_2",
                OptionGroup = "",
                OptionRoom = string.Empty,
                CostGroup = "All Rooms",
                OptionType = "Design",
                Price = 188.00,
                Types = optionTypeGlobalT
            };
            //Create Community 
            communityData = new CommunityData()
            {
                Name = "RT_QA_Automation_Community_36132",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "ComAu2",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "for 188",
                DrivingDirections = "for 188",
                Slug = "Community-Auto",
            };

            //Create Series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Automation_SeriesData_36132",
                Code = "6132",
                Description = "For 188 sprint"
            };

            //Create house (2 houses)
            houseData1 = new HouseData()
            {
                HouseName = "RT_QA_Automation_House1_36132",
                SaleHouseName = "Name188_1",
                Series = seriesData.Name,
                PlanNumber = "0912",
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
                Description = "Testsp188"
            };
            houseData2 = new HouseData()
            {
                HouseName = "RT_QA_Automation_House2_36132",
                SaleHouseName = "Name188_2",
                Series = seriesData.Name,
                PlanNumber = "0913",
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
                Description = "Testsp188_2"
            };

            //Create building group, building phase
            buildingGroupData = new BuildingGroupData()
            {
                Code = "188",
                Name = "SPA_188"
            };

            buildingPhaseData = new BuildingPhaseData()
            {
                Code = "1881",
                Name = "RT_QA_Automation_BuildingPhase_36132",
                BuildingGroupName = buildingGroupData.Name,
                BuildingGroupCode = buildingGroupData.Code,
                Taxable = false,
            };

            //Create Manufacturer
            manuData = new ManufacturerData()
            {
                Name = "RT_QA_Manufacturer_36132"
            };
            //Create Style
            styleData = new StyleData()
            {
                Name = "RT_QA_Style_36132",
                Manufacturer = manuData.Name
            };

            //Create product
            productData = new ProductData()
            {
                Name = "RT_QA_Automation_Production_36132",
                Description = "Pro_188",
                Notes = string.Empty,
                Code = string.Empty,
                Unit = string.Empty,
                SKU = string.Empty,
                RoundingUnit = string.Empty,
                RoundingRule = string.Empty,
                Waste = string.Empty,
                Supplemental = false,
                Manufacture = manuData.Name,
                Style = styleData.Name,
                Category = string.Empty,
                BuildingPhase = buildingPhaseData.Name,
                Use = string.Empty,
                Quantities = "22",
                Parameter = string.Empty
            };

            // -----ACTIONS TO CREATE DATA from INFORMATION ABOVE ------
            // 1.Setup options, creating 2 options, option1 for normal, option2 for global
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> 1. Create {optionData1.Name} as normal and {optionData2.Name} as global</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData1.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData1.Name) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(optionData1);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option two {optionData1.Name} is displayed in grid.</font>");
            }
            //Option2 as global
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData2.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData2.Name) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(optionData2);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option two {optionData2.Name} is displayed in grid.</font>");
            }
            //2. Create a community (specific)
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>2. Create Specific Community with name as {communityData.Name}.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {communityData.Name} is displayed in grid.</font>");
            }
            else
            {
                //Create a new community (specific one)
                CommunityPage.Instance.CreateCommunity(communityData);
                string _expectedMessage = $"Could not create Community with name {communityData.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { communityData.Name}.");
                }
            }

            //3. Create Series
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>3. Create Series.</font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
            if (!SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name))
            {
                //Create a new series
                SeriesPage.Instance.CreateSeries(seriesData);
            }

            //4.Create two houses
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>4. Create two houses as {houseData1.HouseName} and {houseData2.HouseName} .</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            //For the First house
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {houseData1.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData1.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData1.HouseName) is false)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(houseData1);
                string updateMsg = $"House {houseData1.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }

            //Second house
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {houseData2.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData2.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData2.HouseName) is false)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(houseData2);
                string updateMsg = $"House {houseData2.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }

            //5. Add two options 1 and 2 to house1
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>5. Add two options to house1</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData1.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData1.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Options");
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", optionData1.Name) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(optionData1.Name + " - " + optionData1.Number);
                    HouseOptionDetailPage.Instance.WaitAddOptionModalLoadingIcon();
                    ExtentReportsHelper.LogInformation(null, $"{optionData1.Name} has been added to {houseData1.HouseName}");
                }

                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", optionData2.Name) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(optionData2.Name + " - " + optionData2.Number);
                    HouseOptionDetailPage.Instance.WaitAddOptionModalLoadingIcon();
                    ExtentReportsHelper.LogInformation(null, $"{optionData2.Name} has been added to {houseData1.HouseName}");
                }

                //6. Add house1, and house2, first standing at house1, into the specific community
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>6. Add two houses to the specific community </font>");
                HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");
                HouseCommunities.Instance.FillterOnGrid("Name", communityData.Name);
                if (HouseCommunities.Instance.IsValueOnGrid("Name", communityData.Name) is false)
                {
                    HouseCommunities.Instance.AddButtonCommunities();
                    HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(communityData.Name);
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Community with Name {communityData.Name} is displayed in grid");
                }

                //Now switch to house2 to add this specific community
                CommonFuncs.SwitchToAnotherOne(houseData2.HouseName);
                HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");
                if (HouseCommunities.Instance.IsValueOnGrid("Name", communityData.Name) is false)
                {
                    HouseCommunities.Instance.AddButtonCommunities();
                    HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(communityData.Name);
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Community with Name {communityData.Name} is displayed in grid");
                }
            }


            //7. Create new building group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>7. Create a building phase</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingGroupData.Name);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", buildingGroupData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Building group has been existed</b></font>");
            }
            else
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //8. Create building phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>8. Create a building phase </font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", buildingPhaseData.Code) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPhaseCode(buildingPhaseData.Code).EnterPhaseName(buildingPhaseData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(buildingGroupData.Code + "-" + buildingGroupData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }

            //9. Create new manufacturer
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>9. Create Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            //10. Create a style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>10. Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            //11.Prepare Data: Import Product, before building phase has been created by UI
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>11. Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            string importProductFile = "\\DataInputFiles\\Import\\PIPE_36132\\Import_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);
        }
    }
}
