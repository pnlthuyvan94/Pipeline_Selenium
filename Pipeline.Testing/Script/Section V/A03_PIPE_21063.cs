using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.Assigments;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Pipeline.Testing.Script.Section_V
{
    class A03_PIPE_21063 : BaseTestScript
    {
        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private const string TYPE_DELETE_OPTIONQUANTITIES = "DeleteAll";

        private static string COMMUNITY_CODE_DEFAULT = "QA_New_1";
        private static string COMMUNITY_NAME_DEFAULT = "QA_New_Community_Auto";
        private static string PRODUCT_NAME = "QA_Product_01_Automation";
        private static string BUILDING_PHASE = "QA_1-QA_BuildingPhase_01_Automation";
        private static string BUILDING_PHASE_SUBCOMPONENT = "QA_2-QA_BuildingPhase_02_Automation";
        private static string OPTION_NAME = "QA_New_Option_Auto_New";
        private static string PRODUCT_SUBCOMPONENT = "QA_Product_02_Automation";
        private static string PRODUCT_SUBCOMPONENT_VERIFY = "QA_Product_02_Automation";
        private static string PRODUCT_STYLE = "QA_Style_Automation";
        private static string COMMUNITY_VALUE = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT;

        private readonly string PARAMETER_DEFAULT = "SWG";

        private CommunityData _communityData;
        private OptionData _optionData;
        private HouseData _houseData;
        private ProductData _productData;
        private OptionQuantitiesData _optionQuantitiesData;

        private OptionQuantitiesData _optionQuantitiesNewData;
        private static string BUILDING_PHASE_NEW = "QA_4-QA_BuildingPhase_04_Automation";
        private static string PRODUCT_NAME_NEW = "QA_Product_04_Automation";
        private static string QUANTITY_NEW = "1.00";

        private GlobalOptionBomQuantitesData _globalOptionBOM_QuantitiesBase;
        private GlobalOptionBomQuantitesData _globalOptionBOM_QuantitiesBase_Subcomponent;
        private GlobalOptionBomQuantitesData _globalOptionBOM_QuantitiesBase_New;
        private ProductToOptionData _option_Base_New;
        private ProductToOptionData _option_Base_1;
        private ProductToOptionData _option_Base_2_Subcomponent;
        private ProductData _productData_Option_1;
        private ProductData _productData_Option_Subcomponent;
        private ProductData _productData_Option_New;
        private HouseQuantitiesData _houseQuantities_New;
        private HouseQuantitiesData _houseQuantities_Subcomponent;

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_V);
        }

        [SetUp]
        public void SetUpTest()
        {
            _productData = new ProductData()
            {
                Name = PRODUCT_NAME,
                Manufacture = "QA_Manu_Automation",
                Style = PRODUCT_STYLE,
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = BUILDING_PHASE
            };


            _optionQuantitiesData = new OptionQuantitiesData()
            {
                OptionName = OPTION_NAME,
                BuildingPhase = BUILDING_PHASE,
                ProductName = _productData.Name,
                ProductDescription = "QA Regression Test Product - For QA Testing Only",
                Style = "QA_Style_Automation",
                Condition = false,
                Use = string.Empty,
                Quantity = "1.00",
                Source = "Pipeline"
            };

            _optionQuantitiesNewData = new OptionQuantitiesData()
            {
                OptionName = OPTION_NAME,
                BuildingPhase = BUILDING_PHASE_NEW,
                ProductName = PRODUCT_NAME_NEW,
                Quantity = QUANTITY_NEW
            };

            _productData_Option_New = new ProductData()
            {
                Name = _optionQuantitiesNewData.ProductName,
                Quantities = _optionQuantitiesNewData.Quantity,
                BuildingPhase = BUILDING_PHASE_NEW
            };

            _productData_Option_1 = new ProductData()
            {
                Name = _optionQuantitiesData.ProductName,
                Style = _optionQuantitiesData.Style,
                Quantities = _optionQuantitiesData.Quantity,
            };

            _productData_Option_Subcomponent = new ProductData()
            {
                Name = PRODUCT_SUBCOMPONENT_VERIFY,
                Style = _optionQuantitiesData.Style,
                Quantities = _optionQuantitiesData.Quantity,
            };

            _option_Base_New = new ProductToOptionData()
            {
                BuildingPhase = _optionQuantitiesNewData.BuildingPhase,
                ProductList = new List<ProductData> { _productData_Option_New }
            };

            _option_Base_1 = new ProductToOptionData()
            {
                BuildingPhase = _optionQuantitiesData.BuildingPhase,
                ProductList = new List<ProductData> { _productData_Option_1 }
            };

            _option_Base_2_Subcomponent = new ProductToOptionData()
            {
                BuildingPhase = BUILDING_PHASE_SUBCOMPONENT,
                ProductList = new List<ProductData> { _productData_Option_Subcomponent }
            };

            _houseQuantities_New = new HouseQuantitiesData()
            {
                optionName = OPTION_NAME,
                productToOption = new List<ProductToOptionData> { _option_Base_2_Subcomponent, _option_Base_New }
            };

            _houseQuantities_Subcomponent = new HouseQuantitiesData()
            {
                optionName = OPTION_NAME,
                productToOption = new List<ProductToOptionData> { _option_Base_2_Subcomponent }
            };

            _globalOptionBOM_QuantitiesBase = new GlobalOptionBomQuantitesData()
            {
                optionName = OPTION_NAME,
                productToOption = new List<ProductToOptionData> { _option_Base_1 },
            };

            _globalOptionBOM_QuantitiesBase_Subcomponent = new GlobalOptionBomQuantitesData()
            {
                optionName = OPTION_NAME,
                productToOption = new List<ProductToOptionData> { _option_Base_2_Subcomponent }
            };
            _globalOptionBOM_QuantitiesBase_New = new GlobalOptionBomQuantitesData()
            {
                optionName = OPTION_NAME,
                productToOption = new List<ProductToOptionData> { _option_Base_New }
            };

            _communityData = new CommunityData()
            {
                Name = COMMUNITY_NAME_DEFAULT,
                Division = "None",
                City = "Ho Chi Minh",
                Code = COMMUNITY_CODE_DEFAULT,
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
                Slug = COMMUNITY_NAME_DEFAULT,

            };

            var optionType = new List<bool>()
            {
                false, false, true
            };

            _optionData = new OptionData()
            {
                Name = OPTION_NAME,
                Number = "NN12C",
                Price = 100,
                Types = optionType
            };

            _houseData = new HouseData()
            {
                HouseName = "QA_New_Auto-House",
                SaleHouseName = "The house which created by QA",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "NN99",
                BasePrice = "1000000.00",
                SQFTBasement = "200",
                SQFTFloor1 = "200",
                SQFTFloor2 = "200",
                SQFTHeated = "0",
                SQFTTotal = "0",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "QA create house - testing"
            };
            
            //Pre-Step 1: Create new a Community.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Pre-Test: Navigate to Community default page.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Community Page.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _communityData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {_communityData.Name} is displayed in grid.</font>");
            }
            else
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(_communityData);
                string _expectedMessage = $"Could not create Community with name {_communityData.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { _communityData.Name}.");
                }

            }

            //Pre-Step 2: Create new a House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Pre-Test: Navigate to House Page.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);
            SeriesData seriesData = new SeriesData()
            {
                Name = "QA_RT_Serie3_Automation",
                Code = "",
                Description = "Please no not remove or modify"
            };

            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                // Create a new series
                SeriesPage.Instance.CreateSeries(seriesData);
            }

            //Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House default page.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {_houseData.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _houseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", _houseData.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(_houseData);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"House with Name {_houseData.HouseName} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", _houseData.HouseName);

            }

            //Pre-Step 3:Assign Community to House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Pre-Test: Add House to Community.</b></font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Communities");
            HouseCommunities.Instance.FillterOnGrid("Name", _communityData.Name);
            if (HouseCommunities.Instance.IsValueOnGrid("Name", _communityData.Name) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(_communityData.Name);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Community with Name {_communityData.Name} is displayed in grid");
            }


            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers, true, true);
            CommonHelper.SwitchLastestTab();

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_Manu_Automation"
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
                Name = "QA_Style_Automation",
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_21063\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(BUILDING_GROUP_PHASE_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            
        }
        [Test]
        [Category("Section_V")]
        public void A03_Generate_A_Global_Option()
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

            //Show Subcomponent Description is turned false
            //The description product show on the copy modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Show Subcomponent Description is turned false.</font><b>");
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            EstimatingPage.Instance.VerifySettingEstimatingPageIsDisplayed();
            EstimatingPage.Instance.Check_Show_Subcomponent_Description(false);

            // A.Set up the Global Option
            // A.1.In the Option Details page, turn on the 'Global' button of the selected Option
            //Step 1:Create a Global Option
            //Navigate to Options page.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>STEP A.Set up the Global Option</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>STEP A.1.In the Option Details page, turn on the 'Global' button of the selected Option</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to Options page.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);

            // Add an new Option with 'Global' button of selected Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add an new Option with 'Global' button of selected Option</b></font>");
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _optionData.Name);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, _optionData.Number);
            if (OptionPage.Instance.IsItemInGrid("Name", _optionData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Delete option if it is existing</b></font>");
                // Then create a new one
                OptionPage.Instance.SelectItemInGrid("Name", _optionData.Name);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Create an new Option</b></font>");
                // If option isn't existing then create a new one
                OptionPage.Instance.CreateNewOption(_optionData);
            }

            // A.2. Assign Community, House to Global Option
            //Go to Assigment Page
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>STEP A.2. Assign Community, House to Global Option</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Go to assigment page</b></font>");
            OptionPage.Instance.LeftMenuNavigation("Assignments");

            //Add House
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Add House {_houseData.HouseName} to Option {_optionQuantitiesData.OptionName}</b></font>");

            // Verify item in House grid
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", _houseData.HouseName) is false)
            {
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(_houseData.HouseName);
                string expectedMsg = "House(s) added to house successfully";
                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
            }

            //Add Community
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Add Community {_communityData.Name} to Option {_optionQuantitiesData.OptionName}</b></font>");

            // Verify item in Community grid
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", _communityData.Name) is false)
            {
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + _communityData.Name);
                string expectedMsg = "Option(s) added to community successfully";
                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                    //AssignmentDetailPage.Instance.CloseToastMessage();
                }
            }

            //B.Add Products to Global Option and Generate Global BOM
            //B.1.Add Product Quantities to Global Option
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>STEP B.Add Products to Global Option and Generate Global BOM</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>STEP B.1.Add Product Quantities to Global Option</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{BUILDING_PHASE}'.</b></font>");
            OptionPage.Instance.LeftMenuNavigation("Products");

            // Add a new option quantitiy if it doesn't exist
            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", BUILDING_PHASE_NEW) is true)
            {
                ProductsToOptionPage.Instance.DeleteItemInGrid("Building Phase", BUILDING_PHASE_NEW);
                ProductsToOptionPage.Instance.WaitOptionQuantitiesLoadingIcon();
            }

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", _optionQuantitiesData.BuildingPhase) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add an new option quantitiy</b></font>");
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(_optionQuantitiesData);
            }

            //B2. In the Global BOM grid, select the Community; click the 'Generate BOM' button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>STEP B2. In the Global BOM grid, select the Community; click the 'Generate BOM' button</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click to Generate BOM</b></font>");
            ProductsToOptionPage.Instance.GenerateGlobalBom(COMMUNITY_VALUE);

            //B3. Verify the Option Products is displayed successfully in the Global BOM grid
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>STEP B3. Verify the Option Products is displayed successfully in the Global BOM grid</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Verify House BOM without product quantities on the grid view.</b></font>");
            ProductsToOptionPage.Instance.VerifyItemOnGlobalBomGrid(_globalOptionBOM_QuantitiesBase, false);

            //B4.In the Products Subcomponents page, add a Subcomponent to the selected Product
            //Navigate to product page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>STEP B4.In the Products Subcomponents page, add a Subcomponent to the selected Product</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to Products Page.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Filter Product Name with name is: {_productData.Name}.</b></font>");
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _productData.Name);
            if(ProductPage.Instance.IsItemInGrid("Product Name", _productData.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _productData.Name);

                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Navigate to Subcomponent Page.</b></font>");
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Navigate to Subcomponent page
                // Click add subcomponent
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Click on button add subcomponent.</b></font>");
                ProductSubcomponentPage.Instance.ClickAdd_btn();

                //Add subcomponent with type is Basic
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Add a subcomponent with type is basic</b></font>");
                ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic");
                ProductSubcomponentPage.Instance.SelectBuildingPhaseOfProduct(BUILDING_PHASE);
                ProductSubcomponentPage.Instance.SelectStyleOfProduct(PRODUCT_STYLE);
                ProductSubcomponentPage.Instance.SelectChildBuildingPhaseOfSubComponent(BUILDING_PHASE_SUBCOMPONENT);
                ProductSubcomponentPage.Instance.InputProductSubcomponentWithoutCategory(PRODUCT_SUBCOMPONENT);
                ProductSubcomponentPage.Instance.ClickSaveProductSubcomponent();

                //Verify add subcomponent
                string expectedMess = "Successfully added new subcomponent!";
                VerifyToastMessage(expectedMess, "ParentPhase", BUILDING_PHASE);
            }


            //B5. In the Global BOM grid, generate BOM again; verify the Subcomponent Product is shown.

            //Navigate to Option Page and filter name of Option
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>STEP B5. In the Global BOM grid, generate BOM again; verify the Subcomponent Product is shown.</ b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Navigate to Option Page</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _optionData.Name);
            if(OptionPage.Instance.IsItemInGrid("Name", _optionData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Go into Option</b></font>");
                OptionPage.Instance.SelectItemInGrid("Name", _optionData.Name);

                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Navigate to 'Product' submenu</b></font>");
                OptionDetailPage.Instance.LeftMenuNavigation("Products");
                //Generate Global Option Bom
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click to Generate BOM</b></font>");
                ProductsToOptionPage.Instance.GenerateGlobalBom(COMMUNITY_VALUE);

                CommonHelper.RefreshPage();
                //verify the Subcomponent Product is shown.
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>verify the Subcomponent Product is shown in Global Option Bom</b></font>");
                ProductsToOptionPage.Instance.VerifyItemOnGlobalBomGrid(_globalOptionBOM_QuantitiesBase_Subcomponent, false);
            }

            //C.Relationship between Global Option BOM and House BOM
            // C.1.Verify the Option Products is displayed on the House BOM page
            //(If this Option is added to House and generate BOM with the same Community).

            //Navigate to House Page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>STEP C.Relationship between Global Option BOM and House BOM</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>STEP C.1.Verify the Option Products is displayed on the House BOM page</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House page</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            //ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step : filter House name is {_houseData.HouseName}</b></font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", _houseData.HouseName) is true)
            {
                //ExtentReportsHelper.LogInformation($"go to House detatil {_houseData.HouseName} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", _houseData.HouseName);

                //Navigate to House BOM
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House Bom</b></font>");
                HouseDetailPage.Instance.LeftMenuNavigation("House BOM");

                //Select community
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House Bom</b></font>");
                HouseBOMDetailPage.Instance.SelectCommunity(COMMUNITY_VALUE);

                // Verify House BOM
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify House BOM</b></font>");
                HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(_houseQuantities_Subcomponent);
            }

            //C.2. Return the Product Global Options page, add a new Product Option, not generate Global BOM
            //Navigate to Option Page and filter name of Option

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>STEP C.2. Return the Product Global Options page, add a new Product Option, not generate Global BOM</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Navigate to Option Page</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Navigate to Option Page</b></font>");
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _optionData.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _optionData.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", _optionData.Name);
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Navigate to 'Product' submenu</b></font>");
                OptionPage.Instance.LeftMenuNavigation("Products");

                //Add a new product Option
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House Bom</b></font>");
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", BUILDING_PHASE_NEW) is false)
                {
                    ProductsToOptionPage.Instance.AddOptionQuantities(_optionQuantitiesNewData);
                }
            }


            //C.3 Open the House BOM page( for the House that had the Global Option), generate BOM; verify the new added Product Option is displayed here
            //Navigate to House Page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>STEP C.3 Open the House BOM page( for the House that had the Global Option), generate BOM; verify the new added Product Option is displayed here</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House page</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>filter House name is {_houseData.HouseName}</b></font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _houseData.HouseName);
            if(HousePage.Instance.IsItemInGrid("Name", _houseData.HouseName) is true)
            {
                ExtentReportsHelper.LogInformation($"<font color='lavender'><b>Go to House detatil {_houseData.HouseName} is displayed in grid </b></font>");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", _houseData.HouseName);

                //Navigate to House BOM
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House Bom</b></font>");
                HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");

                //Select community
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House Bom</b></font>");
                HouseBOMDetailPage.Instance.SelectCommunity(COMMUNITY_VALUE);

                //Generate House Bom
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House Bom</b></font>");
                HouseBOMDetailPage.Instance.GenerateHouseBOM(COMMUNITY_VALUE);

                //Verify the new added Product Option is displayed here
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House Bom</b></font>");
                HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(_houseQuantities_New);
            }


            // Navigate to Option Page and filter name of Option
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Navigate to Option Page</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _optionData.Name);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, _optionData.Number);
            if(OptionPage.Instance.IsItemInGrid("Name", _optionData.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", _optionData.Name);

                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Navigate to 'Product' submenu</b></font>");
                OptionPage.Instance.LeftMenuNavigation("Products");

                //C.4.Verify the Product Option is updated automatically in the Global BOM grid
                //D. Verify the Product Rounding/Waste displayed in the Global BOM grid
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>STEP C.4.Verify the Product Option is updated automatically in the Global BOM grid</b></font>");
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>D. Verify the Product Rounding/Waste displayed in the Global BOM grid</b></font>");
                ProductsToOptionPage.Instance.VerifyItemOnGlobalBomGrid(_globalOptionBOM_QuantitiesBase_New, true);
                System.Threading.Thread.Sleep(1000);
            }


        }
        [TearDown]
        public void DeleteData()
        {

            //Delete SubComponent 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete SubComponent Name {BUILDING_PHASE_SUBCOMPONENT} .</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _productData.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _productData.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _productData.Name);
                //Navigate To Subcomponents
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                ProductSubcomponentPage.Instance.ClickDeleteInGird(BUILDING_PHASE_SUBCOMPONENT);
                string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
                if (act_mess == "Successfully deleted subcomponent")
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BUILDING_PHASE_SUBCOMPONENT} subcomponent </b></font color>");
                }
                else
                    ExtentReportsHelper.LogFail($"<b> Cannot delete {BUILDING_PHASE_SUBCOMPONENT} subcomponent </b>");
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

    }
}
