using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;
using NUnit.Framework;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.Communities.Lot;

namespace Pipeline.Testing.Script.Section_IV
{
    class C02_E_PIPE_36127 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        OptionData _option;
        SeriesData _series;
        HouseData _housedata;
        LotData _lotdata;
        private JobData jobData;
        private OptionQuantitiesData optionQuantitiesData;

        private CommunityQuantitiesData communityQuantitiesData1;
        private CommunityQuantitiesData communityQuantitiesData2;
        private CommunityQuantitiesData communityQuantitiesData3;

        private readonly int[] indexs = new int[] { };
        string[] OptionData = { OPTION_NAME_DEFAULT };
        string HouseDetail_url;

        private readonly string COMMUNITY1_CODE_DEFAULT = "Auto01";
        private readonly string COMMUNITY1_NAME_DEFAULT = "QA_Community01_Automation";

        private readonly string COMMUNITY2_CODE_DEFAULT = "Auto02";
        private readonly string COMMUNITY2_NAME_DEFAULT = "QA_Community02_Automation";

        private readonly string COMMUNITY3_CODE_DEFAULT = "Auto03";
        private readonly string COMMUNITY3_NAME_DEFAULT = "QA_Community03_Automation";

        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House11_Automation";

        private const string OPTION = "OPTION";
        private static string OPTION_NAME_DEFAULT = "QA_RT_Option03_Automation";
        private static string OPTION_CODE_DEFAULT = "0300";

        private static string PHASE1_VALUE = "RT01-QA_RT_BuildingPhase01_Auto";
        private static string PHASE2_VALUE = "RT01-QA_RT_BuildingPhase01_Auto";
        private static string PHASE3_VALUE = "RT01-QA_RT_BuildingPhase01_Auto";

        private readonly string PARAMETER_DEFAULT = "SWG";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";
        private const string ImportType = "Pre-Import Modification";

        private ProductData productData_Option_1;
        private ProductData productData_Option_2;

        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;

        private ProductData productData_House_1;
        private ProductData productData_House_2;

        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;

        private HouseQuantitiesData houseQuantities_DefaultCommunity;
        private HouseQuantitiesData houseQuantities_SpecificCommunity;

        private ProductData productData_Job1;
        private ProductData productData_Job2;

        private ProductToOptionData productToOption_Job1;
        private ProductToOptionData productToOption_Job2;

        private HouseQuantitiesData JobQuantities_DefaultCommunity;

        private ProductData productData_Specificommunity_Job1;
        private ProductData productData_Specificommunity_Job2;
        private ProductData productData_Specificommunity_Job3;

        private ProductToOptionData productToOption_Specificommunity_Job1;
        private ProductToOptionData productToOption_Specificommunity_Job2;
        private ProductToOptionData productToOption_Specificommunity_Job3;

        private HouseQuantitiesData HouseQuantities_Job_SpecificCommunity;

        private HouseQuantitiesData JobQuantities_SpecificCommunity;

        #region"Set up data"
        [SetUp]
        public void SetUpData()
        {
            jobData = new JobData()
            {
                Name = "QA_RT_Job11_Automation",
                Community = "Auto02-QA_Community02_Automation",
                House = "0110-QA_RT_House11_Automation",
                Lot = "_111 - Sold",
                Orientation = "None"
            };

            _lotdata = new LotData()
            {
                Number = "_111",
                Status = "Sold"
            };


            var optionType = new List<bool>()
            {
                false, false, false
            };

            _option = new OptionData()
            {
                Name = "QA_RT_Option03_Automation",
                Number = "0300",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };

            _housedata = new HouseData()
            {
                HouseName = "QA_RT_House11_Automation",
                SaleHouseName = "QA_RT_House11_Automation",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "0110",
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


            _series = new SeriesData()
            {
                Name = "QA_RT_Serie3_Automation",
                Code = "",
                Description = "Please no not remove or modify"
            };


            communityQuantitiesData1 = new CommunityQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE,
                ProductName = "QA_RT_Product01_Auto",
                Style = "QA_RT_Style_Automation",
                Use = "NONE",
                Quantity = "1.00",
                Source = "Pipeline"
            };

            communityQuantitiesData2 = new CommunityQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE,
                ProductName = "QA_RT_Product02_Auto",
                Style = "QA_RT_Style_Automation",
                Use = "NONE",
                Quantity = "2.00",
                Source = "Pipeline"
            };

            communityQuantitiesData3 = new CommunityQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE,
                ProductName = "QA_RT_Product03_Auto",
                Style = "QA_RT_Style_Automation",
                Use = "NONE",
                Quantity = "3.00",
                Source = "Pipeline"
            };


            optionQuantitiesData = new OptionQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = "RT03-QA_RT_BuildingPhase03_Auto",
                ProductName = "QA_RT_Product06_Auto",
                ProductDescription = string.Empty,
                Style = "QA_RT_Style_Automation",
                Condition = false,
                Use = "NONE",
                Quantity = "5.00",
                Source = "Pipeline"
            };

            productData_Option_1 = new ProductData()
            {
                Name = "QA_RT_Product04_Auto",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "11.00",
                Unit = "NONE"
            };

            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_Product05_Auto",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "11.00",
                Unit = "NONE"
            };


            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "RT02-QA_RT_BuildingPhase02_Auto",
                ProductList = new List<ProductData> { productData_Option_1 }
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "RT03-QA_RT_BuildingPhase03_Auto",
                ProductList = new List<ProductData> { productData_Option_2 }
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2);


            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };

            // There is no House quantities 
            houseQuantities_DefaultCommunity = new HouseQuantitiesData()
            {
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1 }
            };


            houseQuantities_SpecificCommunity = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY1_CODE_DEFAULT,
                communityName = COMMUNITY1_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse2 }
            };




            productData_Job1 = new ProductData()
            {
                Name = "QA_RT_Product02_Auto",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "2.00",
                Unit = string.Empty

            };

            productData_Job2 = new ProductData()
            {
                Name = "QA_RT_Product04_Auto",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "11.00",
                Unit = "NONE"
            };


            productToOption_Job1 = new ProductToOptionData()
            {
                BuildingPhase = "RT01-QA_RT_BuildingPhase01_Auto",
                ProductList = new List<ProductData> { productData_Job1 }
            };

            productToOption_Job2 = new ProductToOptionData()
            {
                BuildingPhase = "RT02-QA_RT_BuildingPhase02_Auto",
                ProductList = new List<ProductData> { productData_Job2 }
            };

            // There is no House quantities 
            JobQuantities_DefaultCommunity = new HouseQuantitiesData()
            {
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToOption_Job1, productToOption_Job2 }
            };

            productData_Specificommunity_Job1 = new ProductData()
            {
                Name = "QA_RT_Product02_Auto",
                Use = "NONE",
                Quantities = "2.00",
                Unit = "NONE"

            };

            productData_Specificommunity_Job2 = new ProductData()
            {
                Name = "QA_RT_Product07_Auto",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "11.00",
                Unit = "NONE"
            };

            productData_Specificommunity_Job3 = new ProductData()
            {
                Name = "QA_RT_Product06_Auto",
                Use = "NONE",
                Quantities = "5.00",
                Unit = "NONE"
            };

            productToOption_Specificommunity_Job1 = new ProductToOptionData()
            {
                BuildingPhase = "RT01-QA_RT_BuildingPhase01_Auto",
                ProductList = new List<ProductData> { productData_Specificommunity_Job1 }
            };

            productToOption_Specificommunity_Job2 = new ProductToOptionData()
            {
                BuildingPhase = "RT03-QA_RT_BuildingPhase03_Auto",
                ProductList = new List<ProductData> { productData_Specificommunity_Job2 }
            };

            productToOption_Specificommunity_Job3 = new ProductToOptionData()
            {
                BuildingPhase = "RT03-QA_RT_BuildingPhase03_Auto",
                ProductList = new List<ProductData> { productData_Specificommunity_Job3 }
            };

            HouseQuantities_Job_SpecificCommunity = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY2_CODE_DEFAULT,
                communityName = COMMUNITY2_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> {productToOption_Specificommunity_Job2 }
            };

            JobQuantities_SpecificCommunity = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY2_CODE_DEFAULT,
                communityName = COMMUNITY2_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToOption_Specificommunity_Job1, productToOption_Specificommunity_Job2, productToOption_Specificommunity_Job3 }
            };

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Turn OFF Sage 300 and MS NAV.<b></b></font>");
            CommunityPage.Instance.SetSage300AndNAVStatus(false);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.2: Open Lot page, verify Lot button displays or not.<b></b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Try to open Lot URL of any community and verify Add lot button
            CommonHelper.OpenURL(BaseDashboardUrl + "/Builder/Communities/Lots.aspx?cid=1");
            if (LotPage.Instance.IsAddLotButtonDisplay() is false)
            {
                ExtentReportsHelper.LogWarning(null, $"<font color='lavender'><b>Add lot button doesn't display to continue testing. Stop this test script.</b></font>");
                Assert.Ignore("Add lot button doesn't display after set NAV and Sage 300 to Running. Stop this test script");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (!OptionPage.Instance.IsItemInGrid("Name", _option.Name))
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed.</font>");
                }
                // Create Option - Click 'Save' Button
                OptionPage.Instance.AddOptionModal.AddOption(_option);
                string _expectedMessage = $"Option Number is duplicated.";
                string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Option with name { _option.Name} and Number {_option.Number}.");
                    Assert.Fail($"Could not create Option.");
                }
                BasePage.PageLoad();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option with name { _option.Name} is displayed in grid.</font>");
            }


            // Make sure these communities are existing, to import existing one on step 2.2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Make sure these communities are existing by importing</b></font>");
            CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            CommonHelper.SwitchLastestTab();

            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_IMPORT} grid view to import new Options.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_36127\\Community\\Pipeline_Communities.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_IMPORT, importFile);


            //Prepare Series Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare to Series Page.</font>");
            // Go to the Series default page
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

            // Filter the created series 
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _series.Name);

            // Verify the item is display in list
            if (!SeriesPage.Instance.IsItemInGrid("Name", _series.Name))
            {
                // Create new series to test
                SeriesPage.Instance.ClickAddToSeriesModal();

                Assert.That(SeriesPage.Instance.AddSeriesModal.IsModalDisplayed(), "Add Series modal is not displayed.");

                SeriesPage.Instance.AddSeriesModal
                                         .EnterSeriesName(_series.Name)
                                         .EnterSeriesCode(_series.Code)
                                         .EnterSeriesDescription(_series.Description);


                // Select the 'Save' button on the modal;
                SeriesPage.Instance.AddSeriesModal.Save();

                // Verify successful save and appropriate success message.
                string _expectedMessage = "Series " + _series.Name + " created successfully!";
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
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Serires with name {_series.Name} is displayed in grid.</font>");
            }

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }


            // Prepare a new Style to import Product.
            // Can't import new Style then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
            StyleData styleData = new StyleData()
            {
                Name = "QA_RT_Style_Automation",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "_0001",
                Name = "QA_Building_Group_Automation"
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
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_36127\\BuildingPhases\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");


            importFile = "\\DataInputFiles\\Import\\PIPE_36127\\Products\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }
        #endregion

        #region"Test case"

        [Test]
        [Category("Section_IV")]
        public void C02_E_Jobs_DetailPages_AllJobs_BOMQuantities_Quantities_All_community_quantities_are_pulled_into_job_BOMs()
        {

            //Group by Parameters settings is turned false
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Group by Parameters settings is turned false.</font><b>");
            //Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.SelectGroupByParameter(false, PARAMETER_DEFAULT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);

            //Step 1: Import Products into a house with a blank style with a default community
            //Navigate to House default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House default page.</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            // Hover over Assets  > click Houses then click a House that will be used for testing.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Hover over Assets  > click Houses then click a House that will be used for testing..</font><b>");
            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {_housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _housedata.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", _housedata.HouseName) is true)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(_housedata);
                string updateMsg = $"House {_housedata.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            else
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", _housedata.HouseName);

            }
            //Get the url of House BOM
            HouseDetail_url = HouseDetailPage.Instance.CurrentURL;

            //Go to Assets/Houses/House detail/Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to Assets/Houses/House detail/Import.font>");
            //Once navigated to House Details page click House BOM tab in the left nav panel.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Once navigated to House Details page click House BOM tab in the left nav panel.</b></font>");
            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");

            //I.Setup the data:
            //1.House has multi-community:
            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.Setup the data.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.1.House has multi-community</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY1_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY1_NAME_DEFAULT, indexs);
            }

            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY2_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY2_NAME_DEFAULT, indexs);
            }

            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY3_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY3_NAME_DEFAULT, indexs);
            }

            //2.Add product community for all community added in the house:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.2.Add product community for all community added in the house.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, COMMUNITY1_NAME_DEFAULT);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY1_NAME_DEFAULT) is true)
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY1_NAME_DEFAULT);

                //Add Option into Community
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Add Option into Community.</b></font>");
                CommunityDetailPage.Instance.LeftMenuNavigation("Options");
                CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
                if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION_NAME_DEFAULT) is false)
                {
                    CommunityOptionPage.Instance.AddCommunityOption(OptionData);
                }

                CommunityOptionPage.Instance.LeftMenuNavigation("Products");
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE1_VALUE}'.</b></font>");
                if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesData1.BuildingPhase, communityQuantitiesData1.ProductName) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesData1);
                }
            }


            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, COMMUNITY2_NAME_DEFAULT);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY2_NAME_DEFAULT) is true)
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY2_NAME_DEFAULT);

                //Naviage To Community Lot
                CommunityDetailPage.Instance.LeftMenuNavigation("Lots");
                string LotPageUrl = LotPage.Instance.CurrentURL;
                if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
                {
                    ExtentReportsHelper.LogInformation($"Lot with Number {_lotdata.Number} and Status {_lotdata.Status} is displayed in grid");
                }
                else
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Import Lot into Community.</font>");
                    CommonHelper.SwitchLastestTab();
                    LotPage.Instance.ImportExporFromMoreMenu("Import");
                    string importFileDir = "Pipeline_Lots_In_Community.csv";
                    LotPage.Instance.ImportFile("Lot Import", $@"\DataInputFiles\Import\PIPE_36127\ImportLot\{importFileDir}");
                    CommonHelper.OpenURL(LotPageUrl);

                    //Check Lot Numbet in grid 
                    if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
                    {
                        ExtentReportsHelper.LogPass("Import Lot File is successful");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("Import Lot File is unsuccessful");
                    }
                }

                //Add Option into Community
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Add Option into Community.</b></font>");
                LotPage.Instance.LeftMenuNavigation("Options");
                CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
                if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION_NAME_DEFAULT) is false)
                {
                    CommunityOptionPage.Instance.AddCommunityOption(OptionData);
                }

                CommunityOptionPage.Instance.LeftMenuNavigation("Products");
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Add a new option quantity if it does NOT exist on phase '{PHASE2_VALUE}'.</b></font>");
                if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesData2.BuildingPhase, communityQuantitiesData2.ProductName) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesData2);
                }
            }


            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, COMMUNITY3_NAME_DEFAULT);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY3_NAME_DEFAULT) is true)
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY3_NAME_DEFAULT);


                //Add Option into Community
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Add Option into Community.</b></font>");
                CommunityDetailPage.Instance.LeftMenuNavigation("Options");
                CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
                if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION_NAME_DEFAULT) is false)
                {
                    CommunityOptionPage.Instance.AddCommunityOption(OptionData);
                }

                CommunityDetailPage.Instance.LeftMenuNavigation("Products");
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Add a new option quantity if it does NOT exist on phase '{PHASE3_VALUE}'.</b></font>");
                if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesData3.BuildingPhase, communityQuantitiesData3.ProductName) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesData3);
                }
            }

            //3.Go to the house quantities and add product for both the default community
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step I.3.Go to the house quantities and add product for both the default community.</b></font>");
            CommonHelper.OpenURL(HouseDetail_url);
            // Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Processing the import with default community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Processing the import with default community.</font>");

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_36127.xml");

            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to House quantities check data.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");

            foreach (ProductToOptionData housequantity in houseQuantities_DefaultCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_DefaultCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_DefaultCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }


            // Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Processing the import with default community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Processing the import with default community.</font>");

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY1_CODE_DEFAULT + "-" + COMMUNITY1_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_36127.xml");

            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");

            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + "-" + houseQuantities_SpecificCommunity.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities_SpecificCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_SpecificCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_SpecificCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            //4.Create a job: Add an option to the job and approved
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.4.Create a job: Add an option to the job and approved.</b></font>");
            //Navigate to Jobs menu > All Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to Jobs menu > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a new Job.</font>");
                JobPage.Instance.CreateJob(jobData);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"The Job {jobData.Name} is exited");
                JobPage.Instance.DeleteJob(jobData.Name);
                JobPage.Instance.CreateJob(jobData);
            }

            //Check Header in BreadsCrumb 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Check Header in BreadsCrumb.</font>");
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(jobData.Name) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The Header is present correctly.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Header is present incorrectly.</font>");
            }

            //Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Job/ Options page. Add Option '{OPTION_NAME_DEFAULT}' to job if it doesn't exist.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = OPTION_CODE_DEFAULT + "-" + OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }
            //II. Check the apply system quantities
            //1. House quantities default community
            ExtentReportsHelper.LogInformation(null, $"<font color='green'>Step II1. House quantities default community.</font>");
            //Go to the quantities page: Click on the “Apply System Quantities” button
            ExtentReportsHelper.LogInformation(null, $"<font color='green'>Go to the quantities page: Click on the “Apply System Quantities” button.</font>");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");
            //Get the url of House BOM
            string JobQuantities_url = JobQuantitiesPage.Instance.CurrentURL;

            //Delete Product Quantities in grid
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Job/ Quantities page. Delete Product Quantities in grid</b></font>");
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Job/ Quantities page. Apply System Quantities</b></font>");
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            //Check data: 
            //It will get the data from community related with job
            //Get data from House quantities default community because the house quantities specific community(Com_Lafayette26(+1)) is no data

            // Verify Product Quantities is displayed in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Job Quantities In Grid.</b></font>");
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(JobQuantities_DefaultCommunity);

            //2. House quantities Specific community
            //Setup the data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step II2. House quantities Specific community</b></font>");
            CommonHelper.OpenURL(HouseDetail_url);

            //Delete All House Quantities In Default Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Community.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");
            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(COMMUNITY2_CODE_DEFAULT + "-" + COMMUNITY2_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            // Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Processing the import with default community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Processing the import with default community.</font>");

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY2_CODE_DEFAULT + "-" + COMMUNITY2_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity2_PIPE_36127.xml");

            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");

            HouseQuantitiesDetailPage.Instance.FilterByCommunity(HouseQuantities_Job_SpecificCommunity.communityCode + "-" + HouseQuantities_Job_SpecificCommunity.communityName);

            foreach (ProductToOptionData housequantity in HouseQuantities_Job_SpecificCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", HouseQuantities_Job_SpecificCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {HouseQuantities_Job_SpecificCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", _option.Name);
            }

            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase .</b></font>");

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", optionQuantitiesData.BuildingPhase) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
            }

            CommonHelper.OpenURL(JobQuantities_url);

            //Delete Product Quantities in grid
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Job/ Quantities page. Delete Product Quantities in grid</b></font>");
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Job/ Quantities page. Add Product Quantities in grid</b></font>");
            //Add Product Quantities

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Job/ Quantities page. Apply System Quantities</b></font>");
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            // Verify Product Quantities is displayed in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Job Quantities In Grid.</b></font>");
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(JobQuantities_SpecificCommunity);

        }
        #endregion
        [TearDown]
        public void DeleteData()
        {

            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            CommonHelper.OpenURL(HouseDetail_url);
            HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Community.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(COMMUNITY1_CODE_DEFAULT + "-" + COMMUNITY1_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            HouseQuantitiesDetailPage.Instance.FilterByCommunity(COMMUNITY2_CODE_DEFAULT + "-" + COMMUNITY2_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            HouseQuantitiesDetailPage.Instance.FilterByCommunity(COMMUNITY3_CODE_DEFAULT + "-" + COMMUNITY3_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
        }

    }
}

