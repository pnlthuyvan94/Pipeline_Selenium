using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
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


namespace Pipeline.Testing.Script.Section_X
{
    class UAT_HOTFIX_PIPE_32020 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_X);
        }

        OptionData _option;
        CommunityData _community;
        SeriesData _series;
        HouseData _housedata;

        private int totalItems;
        private string exportFileName;

        private readonly int[] indexs = new int[] { };
        private const string EXPORT_CSV_MORE_MENU = "CSV";
        private const string ImportType = "Pre-Import Modification";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";

        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_01";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House01_Automation";
        private readonly string OPTION_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private readonly string OPTION_CODE_DEFAULT = "0100";

        private readonly string PARAMETER_DEFAULT = "SWG";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private ProductData productData_Option_1;
        private ProductData productData_Option_2;
        private ProductData productData_Option_3;

        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToOption3;

        private ProductData productData_House_1;
        private ProductData productData_House_2;
        private ProductData productData_House_3;

        private ProductData productDataNoParameter_House;

        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToHouse3
            ;
        private ProductToOptionData productParameterToHouse;

        private ProductToOptionData productToHouseBOM1;
        private ProductToOptionData productToHouseBOM2;
        private ProductToOptionData productToHouseBOM3;

        private HouseQuantitiesData houseQuantities;
        private HouseQuantitiesData houseQuantities_HouseBOM;
        private HouseQuantitiesData houseQuantitiesNoParameter_HouseBOM;

        [SetUp]
        public void GetData()
        {
            var optionType = new List<bool>()
            {
                false, false, false
            };

            _option = new OptionData()
            {
                Name = "QA_RT_Option01_Automation",
                Number = "0100",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };

            _community = new CommunityData()
            {
                Name = "QA_RT_Community01_Automation",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "Automation_01",
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
                Slug = "R-QA-Only-Community-Auto",
            };

            _housedata = new HouseData()
            {
                HouseName = "QA_RT_House01_Automation",
                SaleHouseName = "QA_RT_House01_Sales_Name",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "3000",
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

            productData_Option_1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "10.00",
                Unit = "NONE",
                Parameter = "SWG=L"
            };

            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "20.00",
                Unit = "NONE",
                Parameter = "SWG=R"
            };
            productData_Option_3 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE",
                Parameter = "SWG=Y"
            };


            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_1 },
                ParameterList = new List<ProductData> { productData_Option_1 }
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_2 },
                ParameterList = new List<ProductData> { productData_Option_2 }
            };

            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_3 },
                ParameterList = new List<ProductData> { productData_Option_3 }
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1) { Quantities = "10.00" };

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2) { Quantities = "20.00" };

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_House_3 = new ProductData(productData_Option_3) { Quantities = "30.00" };

            productDataNoParameter_House = new ProductData(productData_Option_1) { Quantities = "60.00" };



            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };
            productToHouse3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3 } };
            productParameterToHouse = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productDataNoParameter_House } };

            // There is no House quantities 
            houseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2, productToHouse3 }
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_1 = new ProductData(productData_Option_1);
            ProductData productData_HouseBOM_2 = new ProductData(productData_Option_2);
            ProductData productData_HouseBOM_3 = new ProductData(productData_Option_3);

            productToHouseBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };

            productToHouseBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_HouseBOM_2 } };

            productToHouseBOM3 = new ProductToOptionData(productToHouse3) { ProductList = new List<ProductData> { productData_HouseBOM_3 } };

            houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM2, productToHouseBOM3 },
            };

            houseQuantitiesNoParameter_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productParameterToHouse }
            };
            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
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

            //Prepare Community Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Community Page.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {_community.Name} is displayed in grid.</font>");
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);
            }
            else
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(_community);
                string _expectedMessage = $"Could not create Community with name {_community.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { _community.Name}.");
                }

            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();

            //Prepare Series Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare to Series Page.</font>");
            // Go to the Series default page
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

            // Filter the created series 
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _series.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", _series.Name) is false)
            {
                // Create a new series
                SeriesPage.Instance.CreateSeries(_series);
            }

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_RT_New_Manu_Auto"
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
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "QA_RT_New_Style_Auto",
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
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "12111111",
                Name = "QA_RT_New_Building_Group_Auto_01"
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_32020\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_32020\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            
        }
        [Test]
        [Category("Section_X")]
        public void UAT_HotFix_HouseBom_Export_DoesNot_Match_The_Display_With_CustomerParameters()
        {

            //Test Case 1: Verify House BOM and exported House BOM when Group by Parameters settings is turned on
            //Step 1.1 :Group by Parameters settings is turned on
            //Make sure current transfer seperation character is ','
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Settings > Group by Parameters settings is turned on.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();

            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            string settingBOM_url = SettingPage.Instance.CurrentURL;
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change House BOM Product Orientation is turned false.</b></font>");
            BOMSettingPage.Instance.Check_House_BOM_Product_Orientation(false);

            //Navigate to House default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House default page.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {_housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _housedata.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", _housedata.HouseName) is false)
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

            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", _community.Name) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(_community.Name, indexs);
            }

            //Step 1.2: Add House Qty with Parameters
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Add House Qty with Parameters.</b></font>");
            HouseCommunities.Instance.LeftMenuNavigation("Import");

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }
            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "HouseQuantities_PIPE_32020.xml");

            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the set up data for product quantities on House.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", item.Parameter) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)

                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Parameter: {item.Parameter}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            //Navigate To House BOM
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the url of House BOM
            string HouseBOM_url = HouseBOMDetailPage.Instance.CurrentURL;

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberItem();

            //Generate House BOM and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step 1.3: Generate House BOM with Group by Parameters settings is turned on.</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM.communityCode + "-" + houseQuantities_HouseBOM.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM.communityCode + "-" + houseQuantities_HouseBOM.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGridWithParameter(houseQuantities_HouseBOM);

            // Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.House_BOM.ToString(), COMMUNITY_NAME_DEFAULT, HOUSE_NAME_DEFAULT);

            //Step 1.4: Export House BOM, with all BP and 1 specific BP with Group by Parameters settings is turned on
            //Export CSV file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4 : Export House BOM, with all BP and 1 specific BP with Group by Parameters settings is turned on.</b></font>");
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName, string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCTWITHPARAMETER_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Test Case 2: Verify House BOM and exported House BOM when Group by Parameters settings is turned off
            //Step 2.1 :Group by Parameters settings is turned on
            CommonHelper.OpenURL(settingBOM_url);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to Settings > Step 2.1 : Group by Parameters settings is turned off.</b></font>");
            BOMSettingPage.Instance.SelectGroupByParameter(false, PARAMETER_DEFAULT);

            //Generate House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to Settings > Step 2.2 : Generate House BOM with Group by Parameters settings is turned off.</b></font>");
            CommonHelper.OpenURL(HouseBOM_url);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantitiesNoParameter_HouseBOM.communityCode + '-' + houseQuantitiesNoParameter_HouseBOM.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM.communityCode + "-" + houseQuantities_HouseBOM.communityName);

            //Verify House BOM is generating fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantitiesNoParameter_HouseBOM);

            //Step 2.3: Export House BOM, with all BP and 1 specific BP with Group by Parameters settings is turned off
            //Export CSV file and make sure the export file existed.
            //Download Dulicate FileName
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3 : Export House BOM, with all BP and 1 specific BP with Group by Parameters s4ettings is turned off.</b></font>");
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (1)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"{exportFileName} (1)", TableType.CSV);
        }
        [TearDown]
        public void DeleteData()
        {
            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities.communityCode + '-' + houseQuantities.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
        }
    }

}

