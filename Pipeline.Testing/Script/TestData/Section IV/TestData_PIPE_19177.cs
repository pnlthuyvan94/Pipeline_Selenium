using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.TestData.Section_IV
{
    class TestData_PIPE_19177 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);
        }

        private string importFileDir;
        private readonly int[] indexs = new int[] { };

        DivisionData division;
        JobData jobData;
        OptionData option;
        HouseData houseData;
        CommunityData communityData;
        LotData lotdata;

        private static string OPTION_NAME_DEFAULT = "QA_RT_Option1_Automation";
        private static string OPTION_CODE_DEFAULT = "123";

        private readonly string COMMUNITY_CODE_DEFAULT = "456";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community1_Automation";

        private readonly string HOUSE_CODE_DEFAULT = "456";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House1_Automation";


        string[] OptionData = { OPTION_NAME_DEFAULT };

        [SetUp]
        public void SetUpData()
        {
            division = new DivisionData()
            {
                Name = "QA_RT_Divsion1_Automation",
                Address = "3990 IN 38",
                City = "Lafayette",
                State = "IN",
                Zip = "47905",
                Description = "Create a new Division",
            };

            jobData = new JobData()
            {
                Name = "QA_RT_Job1_Automation",
                Community = "456-QA_RT_Community1_Automation",
                House = "456-QA_RT_House1_Automation",
                Lot = "QA_RT_Lot1_Automation - Available",
                Orientation = "None"
            };

            var optionType = new List<bool>()
            {
                false, false, false
            };
            option = new OptionData()
            {
                Name = "QA_RT_Option1_Automation",
                Number = "123",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };

            houseData = new HouseData()
            {
                HouseName = "QA_RT_House1_Automation",
                SaleHouseName = "QA_RT_House1_Automation",
                Series = "QA_RT_Serie1_Automation",
                PlanNumber = "456",
                BasePrice = "0",
                SQFTBasement = "0",
                SQFTFloor1 = "0",
                SQFTFloor2 = "0",
                SQFTHeated = "0",
                SQFTTotal = "0",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "0",
                Garage = "1 Car",
                Description = "Please do not remove or modify"
            };

            communityData = new CommunityData()
            {
                Name = "QA_RT_Community1_Automation",
                Division = "QA_RT_Divsion1_Automation",
                Code = "456",
                Status = "Open",
                Description = "Community from automation test v1",
                Slug = "QA_RT_Community1_Automation",
            };

            lotdata = new LotData()
            {
                Number = "QA_RT_Lot1_Automation",
                Status = "Available",

            };
        }

        [Test]
        public void SetUpTestData_B09_A_Estimating_DetailPages_SpecSets_SpecSetGroups_Assignments()
        {
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

            // Prepare data for Option Data
            ExtentReportsHelper.LogInformation(null, "Prepare data for Option Data.");

            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter Item In Grid
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", option.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"The Option with name { option.Name} is displayed in grid.");
            }
            else
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
                }
                BasePage.PageLoad();
            }

            //Prepare data for Community Data
            ExtentReportsHelper.LogInformation(null, "Prepare data for Community Data.");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            ExtentReportsHelper.LogInformation(null, $"Filter community with name {communityData.Name} and create if it doesn't exist.");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (!CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);

            }
            else
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
            }

            //Add Option into Community
            ExtentReportsHelper.LogInformation(null, "Add Option into Community.");
            CommunityDetailPage.Instance.LeftMenuNavigation("Options");
            CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
            if (!CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION_NAME_DEFAULT))
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData);
            }

            //Naviage To Community Lot
            CommunityDetailPage.Instance.LeftMenuNavigation("Lots");
            string lotPageUrl = LotPage.Instance.CurrentURL;
            if (LotPage.Instance.IsItemInGrid("Number", lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", lotdata.Status))
            {
                ExtentReportsHelper.LogInformation($"Lot with Number {lotdata.Number} and Status {lotdata.Status} is displayed in grid");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Import Lot into Community.</font>");
                CommonHelper.SwitchLastestTab();
                LotPage.Instance.ImportExporFromMoreMenu("Import");
                importFileDir = "Pipeline_Lots_In_Community.csv";
                LotPage.Instance.ImportFile("Lot Import", $"\\DataInputFiles\\Import\\PIPE_19177\\{importFileDir}");
                CommonHelper.OpenURL(lotPageUrl);

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

            //Prepare data for House Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data for House Data.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {houseData.HouseName} and create if it doesn't exist.");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(houseData);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"House with Name {houseData.HouseName} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);

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



            //Navigate to Jobs menu > All Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Jobs menu > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a new Job.</font>");
                JobPage.Instance.CreateJob(jobData);
            }
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            //Import Use
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_ATTRIBUTES_VIEW, ImportGridTitle.USE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.USE_IMPORT} grid view to import Use.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_19177\\Pipeline_ProductUses.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.USE_IMPORT, importFile);

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData1 = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer1_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData1.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData1.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData1);
            }

            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData2 = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer2_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData1.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData1.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData2);
            }

            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData3 = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer3_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData3.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData3.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData3);
            }

            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData4 = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer4_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData4.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData4.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData4);
            }

            // Prepare a new Style to import Product.
            // Can't import new Style then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "QA_RT_Style1_Automation",
                Manufacturer = manuData1.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData2 = new StyleData()
            {
                Name = "QA_RT_Style2_Automation",
                Manufacturer = manuData2.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData2);
            }

            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData3 = new StyleData()
            {
                Name = "QA_RT_Style3_Automation",
                Manufacturer = manuData3.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData3.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData3.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData3);
            }

            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData4 = new StyleData()
            {
                Name = "QA_RT_Style4_Automation",
                Manufacturer = manuData4.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData4.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData4.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData4);
            }


            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "123",
                Name = "QA_RT_BuildingGroup1_Automation"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_19177\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_19177\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            //Import Category
            CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES);
            CommonHelper.SwitchLastestTab();

            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_ATTRIBUTES_VIEW, ImportGridTitle.OPTION_CATEGORY_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_CATEGORY_IMPORT} grid view to import.</font>");

             importFile = "\\DataInputFiles\\Import\\PIPE_19177\\Pipeline_Categories.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_CATEGORY_IMPORT, importFile);

        }
    }
}
