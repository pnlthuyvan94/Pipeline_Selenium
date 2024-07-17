using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
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
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Assigments;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.Calculations;
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.Communities.Options;

namespace Pipeline.Testing.Script.Section_V
{
    class A1_PIPE_31216 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_V);
        }
        OptionData _option;
        OptionData _option_parameter;
        private ProductData _product;
        private ProductData getNewproduct;
        CommunityData _community;
        SeriesData _series;
        HouseData _housedata;
        CalculationData calculationData;


        private int totalItems;
        private int totalAdvanceHouseBOMItems;
        private string exportFileName;

        private const string EXPORT_XML_MORE_MENU = "XML";
        private const string EXPORT_CSV_MORE_MENU = "CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Excel";
        private const string ImportType = "Pre-Import Modification";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";

        private readonly string PARAMETER_DEFAULT = "SWG";

        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_01";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House03_Automation";

        private readonly string OPTION_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private readonly string OPTION_CODE_DEFAULT = "0100";

        private readonly string OPTION_CUSTOM_NAME_DEFAULT = "QA_RT_Option_Custom_Parameter_Automation";
        private readonly string OPTION_CUSTOM_CODE_DEFAULT = "0102";

        private readonly string OPTION_BASE_NAME_DEFAULT = "BASE";
        private readonly string OPTION_BASE_CODE_DEFAULT = "00001";

        private readonly string BUILDINGPHASE_DEFAULT = "Au02-QA_RT_New_Building_Phase_02_Automation";
        private readonly string BUILDINGPHASE_SUBCOMPONENT_DEFAULT = "Au01-QA_RT_New_Building_Phase_01_Automation";


        private readonly string PRODUCT_SUBCOMPONENT_NAME_DEFAULT = "QA_RT_New_Product_Automation_02";
        private readonly string STYLE_NAME_DEFAULT = "QA_RT_New_Style_Auto";

        private readonly string PRODUCT_CUSTOM_NAME_DEFAULT = "QA_RT_Product_Custom_Parameter_Parent";
        private readonly string PARAMETER_BASE_NAME_DEFAULT = "SWG=abc";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";
        List<string> Options = new List<string>() { "BASE", "QA_RT_Option_Custom_Parameter_Automation" };
        private string[] elevations = { "BASE" };
        private const string FILTERED_TO_ALL = "ALL";
        private const string FILTERED_TO_BASE_AND_ELEVATIONS = "BASEAndElevations";
        private const string FILTERED_TO_SHOW_HOUSE_ONLY = "ShowHouseOnly";
        private const string FILTERED_TO_SHOW_GLOBAL_ONLY = "ShowGlobalOnly";

        private ProductData productData_Option_1;
        private ProductData productData_Option_2;
        private ProductData productData_Option_3;

        private ProductData productData_Option_Custom_Parent;
        private ProductData productData_Option_Subcomponent;

        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToOption3;

        private ProductToOptionData productToOptionCustom_Parent;
        private ProductToOptionData productToOptionCustom_Subcomponent;

        private ProductData productData_House_1;
        private ProductData productData_House_2;
        private ProductData productData_House_3;

        private ProductData productData_House_OptionCustom;

        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToHouse3;


        private ProductToOptionData productToHouse_OptionCustom;
        private ProductToOptionData productToHouseQuantities_OptionCustomParameter;

        private ProductToOptionData productToHouseBOM1;
        private ProductToOptionData productToHouseBOM2;
        private ProductToOptionData productToHouseBOM3;


        private HouseQuantitiesData houseQuantities;
        private HouseQuantitiesData houseQuantities_OptionCustomParameter;
        private HouseQuantitiesData houseQuantities_HouseBOM_OptionCustomParameter;
        private HouseQuantitiesData houseQuantities_HouseBOM;
        private HouseQuantitiesData houseQuantities_HouseBOM_OptionCustom;

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
            _option_parameter = new OptionData()
            {
                Name = "QA_RT_Option_Custom_Parameter_Automation",
                Number = "0102",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };
            _product = new ProductData()
            {
                Name = "QA_RT_Product_Custom_Parameter_Parent",
                Manufacture = "QA_RT_New_Manu_Auto",
                Style = "QA_RT_New_Style_Auto",
                Code = "QAA2",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "Au02-QA_RT_New_Building_Phase_02_Automation"
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
                HouseName = "QA_RT_House03_Automation",
                SaleHouseName = "QA_RT_House03_Sales_Name",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "300",
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
            };

            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_02",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "20.00",
                Unit = "NONE"
            };
            productData_Option_3 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_03",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE"

            };
            productData_Option_Custom_Parent = new ProductData()
            {
                Name = "QA_RT_Product_Custom_Parameter_Parent",
                Style = "QA_RT_New_Style_Auto",
                Unit = "BF",
                Use = "NONE",
                Quantities = "11.00",
                Parameter = "SWG=abc"
            };
            productData_Option_Subcomponent = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_02",
                Style = "QA_RT_New_Style_Auto",
                Unit = "BF",
                Use = "NONE",
                Quantities = "14.00",
                Parameter = "SWG=abc"
            };
            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_1 }
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_2 }
            };

            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_3 }
            };

            productToOptionCustom_Parent = new ProductToOptionData()
            {
                BuildingPhase = "Au02-QA_RT_New_Building_Phase_02_Automation",
                ProductList = new List<ProductData> { productData_Option_Custom_Parent }
            };
            productToOptionCustom_Subcomponent = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_Subcomponent }
            };
            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1) { Quantities = "10.00" };

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2) { Quantities = "20.00" };

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_House_3 = new ProductData(productData_Option_3) { Quantities = "30.00" };


            productData_House_OptionCustom = new ProductData(productData_Option_Subcomponent) { Quantities = "35.00" };

            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };
            productToHouse3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3 } };

            productToHouse_OptionCustom = new ProductToOptionData(productToOptionCustom_Subcomponent) { ProductList = new List<ProductData> { productData_House_OptionCustom } };
            productToHouseQuantities_OptionCustomParameter = new ProductToOptionData(productToOptionCustom_Subcomponent) { ProductList = new List<ProductData> { productData_Option_Custom_Parent, productData_Option_Subcomponent } };


            // There is no House quantities 
            houseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2, productToHouse3 }
            };

            houseQuantities_OptionCustomParameter = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_CUSTOM_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseQuantities_OptionCustomParameter }
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
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM2, productToHouseBOM3 }
            };

            houseQuantities_HouseBOM_OptionCustom = new HouseQuantitiesData(houseQuantities_OptionCustomParameter)
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_CUSTOM_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_OptionCustom }
            };
            houseQuantities_HouseBOM_OptionCustomParameter = new HouseQuantitiesData(houseQuantities_OptionCustomParameter)
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_CUSTOM_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_OptionCustom }
            };

            // Go to Option default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
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

            CommunityDetailPage.Instance.LeftMenuNavigation("Options");
            string[] OptionData = { OPTION_BASE_NAME_DEFAULT + " - " + OPTION_BASE_CODE_DEFAULT };
            CommunityOptionData communityOptionData = new CommunityOptionData()
            {
                OtherMasterOptions = OptionData,
                SalePrice = "0.00",
                isAvailableToAllHouse=true
            };

            CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.EqualTo, OPTION_BASE_NAME_DEFAULT);
            System.Threading.Thread.Sleep(2000);
            if (CommunityOptionPage.Instance.IsItemInGrid("Option", OPTION_BASE_NAME_DEFAULT) is false)
            {
                CommunityOptionPage.Instance.OpenAddCommunityOptionModal();
                CommunityOptionPage.Instance.AddCommunityOptionModal.EnterOption(OPTION_BASE_NAME_DEFAULT + " - " + OPTION_BASE_CODE_DEFAULT).AddCommunityOption(communityOptionData);
                CommunityOptionPage.Instance.WaitCommunityOptionGridLoad();
            }
            CommunityOptionPage.Instance.LeftMenuNavigation("Products");
            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();

            //Prepare Series Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare to Series Page.</font>");
            // Go to the Series default page
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);

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

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CALCULATION_URL);

            calculationData = new CalculationData()
            {
                Description = "10",
                Calculation = "QTY +10"
            };

            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.EqualTo, calculationData.Description);

            // Create a new Calculation if not existing.
            if (CalculationPage.Instance.IsItemInGrid("Description", calculationData.Description) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Create Calation {calculationData.Description}.</b></font>");
                CalculationPage.Instance.CreateNewCalculation(calculationData);
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_31216\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_31216\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }

        [Test]
        [Category("Section_V")]
        public void A1_PipelineBOM_House_BOM_Export()
        {

            //Group by Parameters settings is turned false
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Group by Parameters settings is turned false.</font><b>");
            //Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change House BOM Product Orientation is turned false.</b></font>");
            BOMSettingPage.Instance.Check_House_BOM_Product_Orientation(false);
            BOMSettingPage.Instance.Check_Only_See_Assigned_Options_on_a_HouseBOM(true);
            //Navigate to House default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to House default page.</font><b>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            //2. Hover over Assets  > click Houses then click a House that will be used for testing.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Hover over Assets  > click Houses then click a House that will be used for testing..</font><b>");
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

            //3. Once navigated to House Details page click House BOM tab in the left nav panel.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Once navigated to House Details page click House BOM tab in the left nav panel.</b></font>");
            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");

            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_BASE_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_BASE_NAME_DEFAULT + " - " + OPTION_BASE_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", _community.Name) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(_community.Name);
            }
            //Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            //Import House Quantities
            //Delete Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_BASE_NAME_DEFAULT + ", " + OPTION_CUSTOM_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_BASE_NAME_DEFAULT + ", " + OPTION_CUSTOM_NAME_DEFAULT);
            }


            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "HouseQuantities_PIPE_31216.xml");

            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Quantities.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            string HouseQuantities_url = HouseQuantitiesDetailPage.Instance.CurrentURL;

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
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            //Navigate To House BOM
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");
            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberHouseBOMDetailItem();

            //Generate House BOM and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Generate House BOM.</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities.communityCode + "-" + houseQuantities.communityName);

            //Verify quantities 
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM);


            //4.Choose a Community in the Community dropdown then next to dropdown click filtered by "ALL".
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4: Choose a Community in the Community dropdown then next to dropdown click filtered by ALL.</font>");

            //Export Modal Filtered by "All" Option
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Modal Filtered by All Option.</font>");
            HouseBOMDetailPage.Instance.ViewModalFilteredAndVerifyFilterToInExport(FILTERED_TO_ALL, COMMUNITY_NAME_DEFAULT);

            //Export Modal Filtered by "BASE + Elevations" Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Modal Filtered by BASE + Elevations Option.</font>");
            HouseBOMDetailPage.Instance.ViewModalFilteredAndVerifyFilterToInExport(FILTERED_TO_BASE_AND_ELEVATIONS, COMMUNITY_NAME_DEFAULT);

            //Export Modal Filtered by "ShowHouseOnly" Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Modal Filtered by ShowHouseOnly Option.</font>");
            HouseBOMDetailPage.Instance.ViewModalFilteredAndVerifyFilterToInExport(FILTERED_TO_SHOW_HOUSE_ONLY, COMMUNITY_NAME_DEFAULT);

            //Export Modal Filtered by "ShowGlobalOnly" Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Modal Filtered by ShowGlobalOnly Option.</font>");
            HouseBOMDetailPage.Instance.ViewModalFilteredAndVerifyFilterToInExport(FILTERED_TO_SHOW_GLOBAL_ONLY, COMMUNITY_NAME_DEFAULT);

            CommonHelper.RefreshPage();

            //6. In "Export House BOM" modal click "Building Phases" that will be exported then click one of the 3 yellow Export buttons.
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step 6: In Export House BOM modal click Building Phases that will be exported then click one of the 3 yellow Export buttons.</font>");

            // Get export file name
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get export file name.</font>");
            exportFileName = CommonHelper.GetExportFileName(ExportType.House_BOM.ToString(), COMMUNITY_NAME_DEFAULT, HOUSE_NAME_DEFAULT);

            //7. EXPORT XML. After export go to folder where the export was downloaded to. 
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step 7: EXPORT XML . After export go to folder where the export was downloaded to.</font>");

            //Export XML By FILTERED_TO_ALL
            ExtentReportsHelper.LogInformation("<font color='lavender'>Export XML By FILTERED_TO_ALL.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_ALL);
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", TableType.XML);
            
            //Export XML By FILTERED_TO_BASE_AND_ELEVATIONS
            ExtentReportsHelper.LogInformation("<font color='lavender'>Export XML By FILTERED_TO_BASE_AND_ELEVATIONS.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_BASE_AND_ELEVATIONS);

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (1)", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (1)", TableType.XML);

            //Export XML By  FILTERED_TO_SHOW_HOUSE_ONLY
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export XML By FILTERED_TO_SHOW_HOUSE_ONLY.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_SHOW_HOUSE_ONLY);

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (2)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (2)", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (2)", TableType.XML);

            //Export XML By FILTERED_TO_SHOW_GLOBAL_ONLY 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export XML By FILTERED_TO_SHOW_GLOBAL_ONLY.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_SHOW_GLOBAL_ONLY);
            
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (3)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (3)", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (3)", TableType.XML);

            //8.EXPORT CSV. After export go to folder where the export was downloaded to.
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step 8: EXPORT CSV. After export go to folder where the export was downloaded to.</font>");

            //Export CSV By FILTERED_TO_ALL
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV By FILTERED_TO_ALL.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_ALL);

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName, string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Export CSV By FILTERED_TO_BASE_AND_ELEVATIONS
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV By FILTERED_TO_BASE_AND_ELEVATIONS.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_BASE_AND_ELEVATIONS);

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_BASEAndElevations", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_BASEAndElevations", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_BASEAndElevations", TableType.CSV);

            //Export CSV By FILTERED_TO_SHOW_HOUSE_ONLY
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV By FILTERED_TO_SHOW_HOUSE_ONLY.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_SHOW_HOUSE_ONLY);

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowHouseOnly", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowHouseOnly", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowHouseOnly", TableType.CSV);

            //Export CSV By FILTERED_TO_SHOW_GLOBAL_ONLY 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV By FILTERED_TO_SHOW_GLOBAL_ONLY.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_SHOW_GLOBAL_ONLY);

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowGlobalOnly", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowGlobalOnly", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowGlobalOnly", TableType.CSV);

            //9. EXPORT Excel . After export go to folder where the export was downloaded to
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step 9: EXPORT Excel. After export go to folder where the export was downloaded to.</font>");
            //Click On FILTERED_TO_ALL
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel By FILTERED_TO_ALL.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_ALL);
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName, string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);

            //Export Excel By FILTERED_TO_BASE_AND_ELEVATIONS
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel By FILTERED_TO_BASE_AND_ELEVATIONS.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_BASE_AND_ELEVATIONS);
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_BASEAndElevations", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_BASEAndElevations", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_BASEAndElevations", TableType.XLSX);

            //Export Excel By FILTERED_TO_SHOW_HOUSE_ONLY
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel By FILTERED_TO_SHOW_HOUSE_ONLY.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_SHOW_HOUSE_ONLY);
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowHouseOnly", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowHouseOnly", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            // HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowHouseOnly", TableType.XLSX);

            //Export Excel By FILTERED_TO_SHOW_GLOBAL_ONLY 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel By FILTERED_TO_SHOW_GLOBAL_ONLY.</font>");
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_SHOW_GLOBAL_ONLY);
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowGlobalOnly", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowGlobalOnly", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBom_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}_FilteredTo_ShowGlobalOnly", TableType.XLSX);

            //10.Generate House BOM when product has Subcomponent and dependent Option - with “Group by Parameter” setting is turned OFF - for Basic House BOM and export Basic House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10: Generate House BOM when product has Subcomponent and dependent Option - with “Group by Parameter” setting is turned OFF - for Basic House BOM and export Basic House BOM.</b></font>");

            // Go to Option default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_parameter.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_parameter.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_parameter);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_parameter.Name);
            }

            //Navigate to Option Assignments
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Assignments.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Add House Into Option.</font>");
            OptionDetailPage.Instance.LeftMenuNavigation("Assignments");
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", _housedata.HouseName) is false)
            {
                // Add House
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(_housedata.HouseName);
                string expectedHouseMsg = "House(s) added to house successfully";
                if (expectedHouseMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
                //AssignmentDetailPage.Instance.CloseAddHouseModal();
                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", _housedata.HouseName))
                {
                    ExtentReportsHelper.LogFail($"House with name {_housedata.HouseName} is NOT add to this Option successfully");
                    //Assert.Fail();
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Add Community Into Option.</font>");
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                //Assign Community to this Global Option
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITY_NAME_DEFAULT);
                string expectedCommunityMsg = "Option(s) added to community successfully";
                if (expectedCommunityMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                    //AssignmentDetailPage.Instance.CloseToastMessage();
                }
                //AssignmentDetailPage.Instance.CloseAddCommunityModal();
                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITY_NAME_DEFAULT} is NOT add to this Option successfully");
                }
            }

            // Navigate setting/product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate setting/product Turn OFF Show Show Category on Add Product SubcomponentModal.</font>");
            ProductSubcomponentPage.Instance.NavigateURL("Products/Settings/Default.aspx");
            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(false);

            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Products/Products/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
            }
            else
            {

                //Add button and Populate all values and save new product
                ProductPage.Instance.ClickAddToProductIcon();
                string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
                Assert.That(ProductDetailPage.Instance.IsPageDisplayed(expectedURL), "Product detail page isn't displayed");

                ExtentReportsHelper.LogInformation("Populate all values and save new product");
                // Select the 'Save' button on the modal;
                getNewproduct = ProductDetailPage.Instance.CreateAProduct(_product);

                // Verify new Product in header
                Assert.That(ProductDetailPage.Instance.IsCreateSuccessfully(getNewproduct), "Create new Product unsuccessfully");
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
            }

            //Navigate To Subcomponents
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //Add subcomponent with type is Basic 
            ExtentReportsHelper.LogInformation(null, "<b> Add subcomponent with type is Basic </b>");
            // Click add subcomponent
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(_product.BuildingPhase)
                                            .SelectStyleOfProduct(_product.Style)
                                            .SelectChildBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectChildStyleOfSubComponent(STYLE_NAME_DEFAULT)
                                            .SelectCalculationSubcomponent(calculationData.Description + " " + $"({calculationData.Calculation})")
                                            .SelectOptionSubcomponent(_option_parameter.Name + " - " + _option_parameter.Number)
                                            .ClickSaveProductSubcomponent();
            //Verify add subcomponent

            string expectedMess = "Successfully added new subcomponent!";
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BUILDINGPHASE_SUBCOMPONENT_DEFAULT);
            ProductSubcomponentPage.Instance.VerifyItemInGrid("Calculation", calculationData.Description + " " + $"({calculationData.Calculation})");
            ProductSubcomponentPage.Instance.VerifyOptionsListInGrid(BUILDINGPHASE_SUBCOMPONENT_DEFAULT, OPTION_CUSTOM_NAME_DEFAULT + "-" + OPTION_CUSTOM_CODE_DEFAULT);

            CommonHelper.OpenURL(HouseQuantities_url);

            // Verify items in the grid view are same as the expected setting data or not.
            ExtentReportsHelper.LogInformation(null, $"The set up data for Option quantities on product <b>'{houseQuantities.optionName}'</b> is not displayed in quantities grid.</font>");
            //Navigate To House Import
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");

            //Delete Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_BASE_NAME_DEFAULT + ", " + OPTION_CUSTOM_NAME_DEFAULT, "HouseQuantities_OptionCustom_PIPE_31216.xml");

            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_OptionCustomParameter.communityCode + "-" + houseQuantities_OptionCustomParameter.communityName);
            foreach (ProductToOptionData housequantity in houseQuantities_OptionCustomParameter.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_OptionCustomParameter.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_OptionCustomParameter.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Add SWG = abc Into Item In Quantities Grid.</font>");
            HouseQuantitiesDetailPage.Instance.ClickEditItemInQuantitiesGrid(BUILDINGPHASE_DEFAULT, PRODUCT_CUSTOM_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.AddParameterInQuantitiesGrid(PARAMETER_BASE_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.ClickEditItemInQuantitiesGrid(BUILDINGPHASE_SUBCOMPONENT_DEFAULT, PRODUCT_SUBCOMPONENT_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.AddParameterInQuantitiesGrid(PARAMETER_BASE_NAME_DEFAULT);
            foreach (ProductToOptionData housequantity in houseQuantities_OptionCustomParameter.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", item.Parameter) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The Parameter with name {item.Parameter} on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The Parameter with name {item.Parameter} on this page is NOT same as expectation. " +
                            $"<br>The expected Quantities: {item.Parameter}</br></font>");
                }
            }

            //Navigate To House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate To House BOM.</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");

            string HouseBOM_url = HouseBOMDetailPage.Instance.CurrentURL;

            //Click On FILTERED_TO_FILTERED_TO_ALL
            HouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_ALL);

            //Generate House BOM and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Generate House BOM with Group by Parameters settings is turned on.</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_OptionCustomParameter.communityCode + "-" + houseQuantities_OptionCustomParameter.communityName);

            //Verify quantities 
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_OptionCustom);

            CommonHelper.RefreshPage();
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName);

            // Verify Quantities And Product In BOM Trace 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify Quantities And Product In BOM Trace .</font>");
            HouseBOMDetailPage.Instance.ViewBOMtrace(houseQuantities_HouseBOM_OptionCustom);

            //Export XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export XML House BOM.</font>");
            //Back to House BOM page, click on Gear icon and click Export
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (4)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (4)", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (4)", TableType.XML);

            //Export CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV House BOM.</font>");
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName + " (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName + " (1)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName + " (1)", TableType.CSV);

            //Export Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel House BOM.</font>");

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName + " (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName + " (1)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName + " (1)", TableType.XLSX);

            //	11.Generate House BOM when product has Subcomponent and dependent Option
            //	- with “Group by Parameter” setting is turned OFF - for Advanced House BOM and export Advanced House BOM

            // Go to Advanced House BOM, select Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11: Generate House BOM when product has Subcomponent and dependent Option - with “Group by Parameter” setting is turned OFF - for Advanced House BOM and export Advanced House BOM.</b></font>");
            HouseBOMDetailPage.Instance.ClickOnAdvancedHouseBOMView();

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectAdvanceCommunity(houseQuantities_HouseBOM_OptionCustom.communityCode + "-" + houseQuantities_HouseBOM_OptionCustom.communityName);
            HouseBOMDetailPage.Instance.SelectOptions(Options);

            //Get Total Number Advance HouseBOM Item
            ExtentReportsHelper.LogInformation(null, $"Get Total Number Advance HouseBOM Item.</font>");
            totalAdvanceHouseBOMItems = HouseBOMDetailPage.Instance.GetTotalNumberAdvanceHouseBOMItem();
            ExtentReportsHelper.LogInformation(null, $"Generate Advance Job BOM.</font>");
            HouseBOMDetailPage.Instance.GenerateAdvancedHouseBOM();
            HouseBOMDetailPage.Instance.LoadHouseAdvanceQuantities();

            //Filter Option In Grid
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM_OptionCustom.optionName);
            foreach (ProductToOptionData housequantity in houseQuantities_HouseBOM_OptionCustom.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM_OptionCustom.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_HouseBOM_OptionCustom.optionName}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                    // Verify Quantities And Product In BOM Trace 
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify Quantities And Product In BOM Trace.</font>");
                    HouseBOMDetailPage.Instance.ViewAdvancedBOM(item.Name, item.Quantities);
                }

            }

            //Export XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export XML House BOM.</font>");
            //GenerateAdvanceHouseBOM(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName, Options);
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (5)", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (5)", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (5)", TableType.XML);

            //Export CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV House BOM.</font>");

            //GenerateAdvanceHouseBOM(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName, Options);
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", totalAdvanceHouseBOMItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", TableType.CSV);

            //Export Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel House BOM.</font>");
            GenerateAdvanceHouseBOM(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName, Options);
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", totalAdvanceHouseBOMItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", TableType.XLSX);


            //12. Generate House BOM when product has Subcomponent and dependent Option
            //- with “Group by Parameter” setting is turned ON - for Basic House BOM and export Basic House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12: Generate House BOM when product has Subcomponent and dependent Option- with “Group by Parameter” setting is turned ON - for Basic House BOM and export Basic House BOM.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);

            //Verify ability to turn on Group by parameters setting
            SettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT);

            CommonHelper.OpenURL(HouseBOM_url);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName);
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGridWithParameter(houseQuantities_HouseBOM_OptionCustom);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName);
            //View BOM Trace
            // Verify Quantities And Product In BOM Trace 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify Quantities And Product In BOM Trace .</font>");
            HouseBOMDetailPage.Instance.ViewBOMtrace(houseQuantities_HouseBOM_OptionCustomParameter);

            //Export XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export XML House BOM.</font>");
            //Back to House BOM page, click on Gear icon and click Export
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_OptionCustom.communityCode + "-" + houseQuantities_HouseBOM_OptionCustom.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (6)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (6)", 0, ExportTitleFileConstant.HOUSEBOMPRODUCTWITHPARAMETER_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (6)", TableType.XML);


            //Export CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel House BOM.</font>");
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_OptionCustom.communityCode + "-" + houseQuantities_HouseBOM_OptionCustom.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName + " (2)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName + " (2)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCTWITHPARAMETER_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName + " (2)", TableType.CSV);

            //Export Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel House BOM.</font>");
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_OptionCustom.communityCode + "-" + houseQuantities_HouseBOM_OptionCustom.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName + " (2)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName + " (2)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCTWITHPARAMETER_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName + " (2)", TableType.XLSX);

            //13. Generate House BOM when product has Subcomponent and dependent Option
            //- with “Group by Parameter” setting is turned ON - for Advanced House BOM and export Advanced House BOM
            //Go to Advanced House BOM, select Community, select both dependent and BASE Options, then click on “Load Selected Product(s)” button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 13: Generate House BOM when product has Subcomponent and dependent Option- with “Group by Parameter” setting is turned ON - for Advanced House BOM and export Advanced House BOMGo to Advanced House BOM, select Community, select both dependent and BASE Options, then click on “Load Selected Product(s)” button.</b></font>");
            GenerateAdvanceHouseBOM(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName, Options);
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM_OptionCustomParameter.optionName);

            //Check if the quantities should be sum up under specific Parameter
            foreach (ProductToOptionData housequantity in houseQuantities_HouseBOM_OptionCustomParameter.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM_OptionCustomParameter.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("SWG", item.Parameter.Substring(item.Parameter.IndexOf("=") + 1)) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_HouseBOM_OptionCustomParameter.optionName}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected SWG: {item.Parameter.Substring(item.Parameter.IndexOf("=") + 1)}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");

                    // Verify Quantities And Product In BOM Trace 
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify Quantities And Product In BOM Trace .</font>");
                    HouseBOMDetailPage.Instance.ViewAdvancedBOM(item.Name, item.Quantities);
                }

            }

            //Export XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export xml Advance BOM.</font>");
            //GenerateAdvanceHouseBOM(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName, Options);
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (7)", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (7)", 0, ExportTitleFileConstant.HOUSEBOMPRODUCTWITHPARAMETER_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (7)", TableType.XML);

            //Export CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV Advance House BOM.</font>");
            //GenerateAdvanceHouseBOM(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName, Options);
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", totalAdvanceHouseBOMItems, ExportTitleFileConstant.HOUSEBOMPRODUCTWITHPARAMETER_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", TableType.CSV);

            //Export Excel

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel Advance House BOM.</font>");
            //GenerateAdvanceHouseBOM(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName, Options);
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", totalAdvanceHouseBOMItems, ExportTitleFileConstant.HOUSEBOMPRODUCTWITHPARAMETER_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", TableType.XLSX);

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

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_BASE_NAME_DEFAULT + ", " + OPTION_CUSTOM_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_BASE_NAME_DEFAULT + ", " + OPTION_CUSTOM_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_HouseBOM_OptionCustomParameter.communityCode + "-" + houseQuantities_HouseBOM_OptionCustomParameter.communityName);

            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            //Delete SubComponent 
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
                //Navigate To Subcomponents
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                ProductSubcomponentPage.Instance.ClickDeleteInGird(BUILDINGPHASE_SUBCOMPONENT_DEFAULT);
                string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
                if (act_mess == "Successfully deleted subcomponent")
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} subcomponent </b></font color>");
                }
                else
                    ExtentReportsHelper.LogFail($"<b> Cannot delete {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} subcomponent </b>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }

        }

        private void VerifyToastMessage(string expectedMess, string columnToVerify, string value)
        {
            string act_Message = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            if (act_Message == expectedMess)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Successfully added new subcomponent</b></font color>");
            }
            else
            {
                // Can't get toast message then verify the item on the grid view
                if (ProductSubcomponentPage.Instance.VerifyItemInGrid(columnToVerify, value) is false)
                {
                    ExtentReportsHelper.LogFail("Failed add new subcomponent");
                    ProductSubcomponentPage.Instance.CloseToastMessage();
                }
            }
        }
        private void GenerateAdvanceHouseBOM(string selectedCommunity, IList<string> Options)
        {
            // Go to Advanced House BOM, select Community
            HouseBOMDetailPage.Instance.ClickOnAdvancedHouseBOMView();
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectAdvanceCommunity(selectedCommunity);

            //Select Options
            HouseBOMDetailPage.Instance.SelectOptions(Options);
            HouseBOMDetailPage.Instance.GenerateAdvancedHouseBOM();
        }
    }
}

