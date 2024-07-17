using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.CustomOptions;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionProduct;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BOMLogicRules;
using Pipeline.Testing.Pages.Estimating.BOMLogicRules.BOMLogicRuleDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Estimating.Category;
using Pipeline.Testing.Pages.Estimating.Category.CategoryDetail;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Units;
using Pipeline.Testing.Pages.Estimating.Units.UnitDetail;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_VII
{
    public class NON_UAT_HOTFIX_PIPE_33017 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VII);
        }
        CommunityData _community;
        OptionData _option;
        HouseData HouseData;

        private CommunityQuantitiesData communityQuantitiesData;
        private OptionQuantitiesData optionQuantitiesData;
        private OptionQuantitiesData optionHouseOptionQuantitiesData;

        private CustomOptionData customOption;

        private ProductData _product;

        private SpecSetData SpecSetData;
        private SpecSetData NewSpecSetData;
        private BOMLogicRuleData BOMLogicRuleData;
        private BOMLogicRuleDetailData BOMLogicRuleDetailData;
        private BOMLogicRuleDetailData NewBOMLogicRuleDetailData;
        private UnitData _unitData;
        private CategoryData _categoryData;

        private readonly string COMMUNITY_CODE_DEFAULT = "Auto_33017";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_Community_PIPE_33017_Automation";

        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House_PIPE_33017__Automation";
        private readonly int[] indexs = new int[] { };

        private const string OPTION = "OPTION";
        private static string OPTION_NAME_DEFAULT = "QA_RT_Option_PIPE_33017_Automation";
        private static string OPTION_CODE_DEFAULT = "3017";
        string[] OptionData = { OPTION_NAME_DEFAULT };

        private static string PHASE1_VALUE = "RT01-QA_RT_BuildingPhase01_Auto";
        private static string PHASE2_VALUE = "RT02-QA_RT_BuildingPhase02_Auto";
        private static string PHASE3_VALUE = "RT03-QA_RT_BuildingPhase03_Auto";
        private static string PHASE4_VALUE = "RT04-QA_RT_BuildingPhase04_Auto";
        private static string PHASE5_VALUE = "RT05-QA_RT_BuildingPhase05_Auto";
        private static string PHASE5_CODE = "RT05";

        private static string Product1 = "QA_RT_Product01_Auto";
        private static string Product2 = "QA_RT_Product02_Auto";
        private static string Product3 = "QA_RT_Product03_Auto";
        private static string Product4 = "QA_RT_Product04_Auto";
        private static string Product5 = "QA_RT_Product05_Auto";
        private static string Product6 = "QA_RT_Product06_Auto";
        private static string Product7 = "QA_RT_Product07_Auto";
        private static string Product8 = "QA_RT_Product08_Auto";
        private static string Product9 = "QA_RT_Product09_Auto";
        private static string Product10 = "QA_RT_Product10_Auto";
        private static string Product11 = "QA_RT_Product11_Auto";
        private static string Product12 = "QA_RT_Product12_Auto";

        private readonly string BUILDINGPHASE_DEFAULT = "RT02-QA_RT_BuildingPhase02_Auto";
        private readonly string BUILDINGPHASE_SUBCOMPONENT1_DEFAULT = "RT03-QA_RT_BuildingPhase03_Auto";
        private readonly string BUILDINGPHASE_SUBCOMPONENT2_DEFAULT = "RT04-QA_RT_BuildingPhase04_Auto";

        private readonly string StyleOfProduct = "QA_RT_Style_Automation";


        private readonly string BUILDINGPHASE_TO_DEFAULT = "QA_3-QA_BuildingPhase_03_Automation";

        private readonly string PRODUCT_SUBCOMPONENT_NAME_DEFAULT = "QA_RT_Product05_Auto";
        private readonly string STYLE_NAME_DEFAULT = "QA_RT_Style_Automation";


        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private List<string> productList = new List<string>() { Product1, Product4 };
        private IList<string> ListBuildPhase;

        [SetUp]
        public void GetData()
        {

            var optionType = new List<bool>()
            {
                false, false, false
            };

            _option = new OptionData()
            {
                Name = "QA_RT_Option_PIPE_33017_Automation",
                Number = "3017",
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
                Name = "QA_Community_PIPE_33017_Automation",
                Division = "None",
                Code = "Auto_33017",
                City = "Ho Chi Minh",
                CityLink = "https://hcm.com",
                Township = "Ho Chi Minh",
                County = "Texas",
                State = "TX",
                Zip = "70000",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Community from automation test v1",
                Slug = "QA_Community_PIPE_33017_Automation",
            };


            HouseData = new HouseData()
            {
                HouseName = "QA_RT_House_PIPE_33017__Automation",
                SaleHouseName = "QA_RT_House_PIPE_33017__Automation",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "3017",
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

            communityQuantitiesData = new CommunityQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE,
                ProductName = "QA_RT_Product01_Auto",
                Style = "QA_RT_Style_Automation",
                Use = "NONE",
                Quantity = "1.00",
                Source = "Pipeline"
            };

            optionQuantitiesData = new OptionQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE,
                ProductName = "QA_RT_Product02_Auto",
                ProductDescription = string.Empty,
                Style = "QA_RT_Style_Automation",
                Condition = false,
                Use = "NONE",
                Quantity = "2.00",
                Source = "Pipeline"
            };

            optionHouseOptionQuantitiesData = new OptionQuantitiesData()
            {
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE,
                Category = "NONE",
                ProductName = "QA_RT_Product03_Auto",
                ProductDescription = "Product Description",
                Style = "QA_RT_Style_Automation",
                Condition = false,
                Use = string.Empty,
                Quantity = "3.00",
                Source = "Pipeline"
            };

            customOption = new CustomOptionData()
            {
                Code = "QA_RT_CustomOption_PIPE_33017",
                Description = "Regression test create Custom Option Update",
                Structural = bool.Parse("FALSE"),
                Price = double.Parse("999")
            };

            _product = new ProductData()
            {
                Name = "QA_RT_Product04_Auto",
                Manufacture = "QA_RT_Manufacturer_Automation",
                BuildingPhase = PHASE2_VALUE
            };

            SpecSetData = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_33017_Automation",
                SpectSetName = "QA_RT_SpecSet_33017_Automation",
                OriginalPhase = PHASE1_VALUE,
                OriginalCategory = "NONE",
                OriginalProduct = Product1,
                OriginalProductStyle = StyleOfProduct,
                OriginalProductUse = "UseD",
                NewPhase = PHASE2_VALUE,
                NewCategory = "NONE",
                NewProduct = Product4,
                NewProductStyle = StyleOfProduct,
                NewProductUse = "UseA",
                ProductCalculation = "NONE"
            };

            NewSpecSetData = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_33017_Automation",
                SpectSetName = "QA_RT_SpecSet_33017_Automation",
                OriginalPhase = PHASE3_VALUE,
                OriginalProduct = Product5,
                OriginalProductStyle = StyleOfProduct,
                OriginalProductUse = "UseA",
                NewPhase = PHASE4_VALUE,
                NewProduct = Product8,
                NewProductStyle = StyleOfProduct,
                NewProductUse = "UseD",
                ProductCalculation = "NONE"
            };

            BOMLogicRuleData = new BOMLogicRuleData()
            {
                RuleName = "QA_RT_BOM_Logic_Rule_Automation",
                RuleDescription = "QA_RT_BOM_Logic_Rule_Automation",
                SortOrder = "1",
                Execution = "During Product Assembly"
            };

            BOMLogicRuleDetailData = new BOMLogicRuleDetailData()
            {
                ConditionKey = "Product",
                ConditionKeyAttribute = "Product Name",
                Operator = "EQUAL",
                ConditionValue = new List<string>() { Product1, Product2 }
            };

            NewBOMLogicRuleDetailData = new BOMLogicRuleDetailData()
            {
                ConditionValue = new List<string>() { Product3, Product4 },
                ConditionKey = "Product",
                ConditionKeyAttribute = "Product Name",
            };

            _unitData = new UnitData()
            {
                Name = "Data for automation testing",
                Abbreviation = "Unit_017",
                Message = "Unit created successfully!"
            };

            _categoryData = new CategoryData()
            {
                Name = "QA_RT_Category_PiPE_33017_Auto",
                Parent = "NONE",

            };

            
            // Go to Option default page
            ExtentReportsHelper.LogInformation(null, "Navigate to Option Page.");
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

            //Prepare data for Community Data
            ExtentReportsHelper.LogInformation(null, "Prepare data for Community Data.");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            ExtentReportsHelper.LogInformation(null, $"Filter community with name {_community.Name} and create if it doesn't exist.");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (!CommunityPage.Instance.IsItemInGrid("Name", _community.Name))
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(_community);

            }
            else
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);
            }

            //Add Option into Community
            ExtentReportsHelper.LogInformation(null, "Add Option into Community.");
            CommunityDetailPage.Instance.LeftMenuNavigation("Options");
            CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
            if (!CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION_NAME_DEFAULT))
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData);
            }


            ExtentReportsHelper.LogInformation(null, "Create a new Series.");
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
            ExtentReportsHelper.LogInformation(null, "Navigate to House default page.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {HouseData.HouseName} and create if it doesn't exist.");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HouseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(HouseData);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"House with Name {HouseData.HouseName} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HouseData.HouseName);

            }

            // Navigate House Option And Add Option into House
            ExtentReportsHelper.LogInformation(null, $"Switch to House/ Options page. Add option '{OPTION_NAME_DEFAULT}'  to house '{HOUSE_NAME_DEFAULT}' if it doesn't exist.");
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

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "Prepare a new Manufacturer to import Product.");
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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Style to import Product.");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Building Group to import Product.");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

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

            //Prepare Unit
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_UNITS_URL);
            ExtentReportsHelper.LogInformation($"Filter new item {_unitData.Name} in the grid view.");
            UnitPage.Instance.FilterItemInGrid("Abbreviation", GridFilterOperator.Contains, _unitData.Abbreviation);
            if (!UnitPage.Instance.IsItemInGrid("Abbreviation", _unitData.Abbreviation))
            {
                UnitPage.Instance.ClickAddToShowUnitModal();
                UnitPage.Instance.CreateUnitAndVerify(_unitData.Abbreviation, _unitData.Name, _unitData.Message);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "Prepare data: Import Building Phase to import Product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_33017\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "Prepare Data: Import Product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_33017\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            
            
        }
        [Test]
        [Category("Section_VII")]
        public void NON_UAT_HotFix_Improve_Product_Drop_Down_Selections_for_Large_Data_Sets()
        {
           
            //1.Open Community Products page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.Open Community Products page, click “+” on the right.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);
            }

            //2.Click on Product field then verify the list of products that is shown 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.Click on Product field then verify the list of products that is shown.</font>");
            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            //3.Enter the value to filter product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.Enter the value to filter product.</font>");
            //4.Select another Building Phase that contains a large of products then verify the list of products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4.Select another Building Phase that contains a large of products then verify the list of products.</font>");
            CommunityProductsPage.Instance.CheckFunctionalProductModal();
            
            //5.Add value into all fields and click Save button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5.Add value into all fields and click Save button.</font>");
            ExtentReportsHelper.LogInformation(null, $"<b>Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE1_VALUE}'.</b>");
            if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesData.BuildingPhase, communityQuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesData);
            }



            //6.Open Option Quantities page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 6.Open Option Quantities page, click “+” on the right.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", _option.Name);
            }

            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase .");

            // Filter product and verify item on the grid
            ProductsToOptionPage.Instance.FilterOptionQuantitiesInGrid("Product", GridFilterOperator.EqualTo, optionQuantitiesData.ProductName);

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", optionQuantitiesData.BuildingPhase) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
            }

            //7.Open House Option Quantities page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 7.Open House Option Quantities page, click “+” on the right.</font>");
            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Building Phase", optionHouseOptionQuantitiesData.BuildingPhase) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionHouseOptionQuantitiesData);
            }


            //8. Open Custom Option Quantities page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 8. Open Custom Option Quantities page, click “+” on the right.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL);
            // Filter
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, customOption.Code);

            if (CustomOptionPage.Instance.IsItemInGrid("Code", customOption.Code) is true)
            {
                CustomOptionPage.Instance.SelectItemInGrid("Code", customOption.Code);
            }
            else
            {
                CustomOptionPage.Instance.CreateCustomOption(customOption);
            }

            CustomOptionDetailPage.Instance.LeftMenuNavigation("Products");

            //Click the '+' button on page to open 'Add Product' modal; Add Product to Custom Option
            ExtentReportsHelper.LogInformation(null, "Click the '+' button on page to open 'Add Product' modal; Add Product to Custom Option");
            CustomOptionProduct.Instance.Click_AddButton();
            ListBuildPhase = CustomOptionProduct.Instance.GetListItem("//select[contains(@id,'ddlBuildingPhases')]/option");
            CustomOptionProduct.Instance.AddProduct(ListBuildPhase[1]);
            ExtentReportsHelper.LogInformation($"Add product with {ListBuildPhase[1]}");
            string _expectedCustomOptionMessage = "Custom Option Product created successfully!";
            string actualCustomOptionMsg = CustomOptionProduct.Instance.GetLastestToastMessage();
            if (_expectedCustomOptionMessage.Equals(actualCustomOptionMsg))
            {
                ExtentReportsHelper.LogPass($"Added successfully the Product to Custom Option");
                CustomOptionProduct.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail("Add unsucessfully the Product to Custom Option");
            }

            CustomOptionProduct.Instance.CloseAddProduct();
            
            //9.Open Product Subcomponents page, click “+” on the right - Select Basic mode
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 9.Open Product Subcomponents page, click “+” on the right - Select Basic mode.</font>");
            ExtentReportsHelper.LogInformation(null, "Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check.");

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
            }

            //Navigate To Subcomponents
            ExtentReportsHelper.LogInformation(null, "Navigate To Subcomponents");
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            string ProductSubcomponent_url = ProductSubcomponentPage.Instance.CurrentURL;

            ExtentReportsHelper.LogInformation(null, "Show Category on Add Spec Set Product Conversion Modal - TURN OFF ");
            // Navigate setting/product
            ProductSubcomponentPage.Instance.NavigateURL("Products/Settings/Default.aspx");
            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(false);
            CommonHelper.OpenURL(ProductSubcomponent_url);
            ExtentReportsHelper.LogInformation(null, "Add subcomponent with type is Basic ");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(BUILDINGPHASE_DEFAULT)
                                            .SelectChildBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT1_DEFAULT)
                                            .InputProductSubcomponentWithoutCategory(Product5)
                                            .SelectChildStyleOfSubComponent(StyleOfProduct)
                                            .ClickSaveProductSubcomponent();
            //Verify add subcomponent
            string expectedMess = "Successfully added new subcomponent!";
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BUILDINGPHASE_SUBCOMPONENT1_DEFAULT);

            //10. Open Product Subcomponents page, click “+” on the right - Select Advanced mode
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 10. Open Product Subcomponents page, click “+” on the right - Select Advanced mode.</font>");
            ExtentReportsHelper.LogInformation(null, "<b> Add subcomponent with type is Advance </b>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Advanced")
                                            .SelectBuildingPhaseOfProduct(BUILDINGPHASE_DEFAULT)
                                            .SelectStyleOfProduct(StyleOfProduct)
                                            .SelectChildBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT2_DEFAULT)
                                            .InputProductSubcomponentWithoutCategory(Product8)
                                            .ClickSaveProductSubcomponent();
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BUILDINGPHASE_SUBCOMPONENT2_DEFAULT);
            ProductSubcomponentPage.Instance.Close_Modal("Add Product");

            //11.Open Product Subcomponents page, click Edit icon on the product row
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 11.Open Product Subcomponents page, click Edit icon on the product row.</font>");
            ProductSubcomponentPage.Instance.ClickEditInGird(BUILDINGPHASE_SUBCOMPONENT1_DEFAULT);
            ProductSubcomponentPage.Instance.ClickSaveEditSubcomponent();
            string act_Message = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            string expected_UpdatedMessage = "Subcomponent successfully updated.";
            if (act_Message == expected_UpdatedMessage)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Successfully edited subcomponent</b></font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>{expected_UpdatedMessage}</font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }


            //12.Open Copy Subcomponents modal with Selective Copy mode
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 12.Open Copy Subcomponents modal with Selective Copy mode.</font>");
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
                .SelectiveCopyOrBatchCopy("SelectiveCopy");
            //13.Click on Product field then verify the list of products that is shown
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 13.Click on Product field then verify the list of products that is shown.</font>");
            ProductSubcomponentPage.Instance.ProductToCopyFrom(_product.Name);
            //14. Enter the value to filter product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 14. Enter the value to filter product.</font>");
            //15. Click the size of list at the bottom
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 15. Click the size of list at the bottom.</font>");
            //16. Select value into all fields and click Save Selective Copy button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 16. Select value into all fields and click Save Selective Copy button.</font>");
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectBuildingPhaseFrom(BUILDINGPHASE_DEFAULT);
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectSubcomponentToCopyFrom(PHASE3_VALUE);
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectSubcomponentToCopyFrom(PHASE4_VALUE);
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectBuildingPhaseTo(PHASE4_VALUE);
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectProduct(Product9);
            ProductSubcomponentPage.Instance.CopySubcomponentModal_ClickSave();

            string act_CopyMessage = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            string expected_CopyMessage = "2 subcomponent(s) successfully copied.";
            if (act_CopyMessage == expected_CopyMessage)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b> Subcomponent was Copied successfully </b></font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>{act_CopyMessage}</font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }

            //17. Open Copy Subcomponents modal with Batch Copy mode
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 17. Open Copy Subcomponents modal with Batch Copy mode.</font>");
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
            .SelectiveCopyOrBatchCopy("Batch Copy");

            //Open Copy Subcomponent modal successfully, verify Product dropdown field with Product name current
            ExtentReportsHelper.LogInformation(null, "Open Copy Subcomponent modal successfully, verify Product dropdown field with Product name current");
            ProductSubcomponentPage.Instance.ProductToCopyFrom(_product.Name);
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectBuildingPhaseInBatchCopy(string.Empty);
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectStyleInBatchCopy(string.Empty);

            //18. Click on Product field then verify the list of products that is shown
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 18. Click on Product field then verify the list of products that is shown.</font>");
            //19. Enter the value to filter product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 19. Enter the value to filter product.</font>");
            //20. Select another Product then verify the list of products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 20. Select another Product then verify the list of products.</font>");
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectProductToInBatchCopy(Product10);
            //21. Select value into all fields and click Save Batch copy button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 21. Select value into all fields and click Save Batch copy button.</font>");
            ProductSubcomponentPage.Instance.CopySubcomponentModal_ClickSaveInBatchCopy();

            act_CopyMessage = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            expected_CopyMessage = "The subcomponent(s) successfully copied.";
            if (act_CopyMessage == expected_CopyMessage)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b> Subcomponent was Copied successfully </b></font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>{act_CopyMessage}</font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }
            
            //22.Open Building Phase Products page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 22.Open Building Phase Products page, click “+” on the right.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, PHASE5_CODE);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", PHASE5_CODE) is true)
            {
                BuildingPhasePage.Instance.ClickItemInGrid("Code", PHASE5_CODE);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add Product to Building Phase.</b></font>");
                if (BuildingPhaseDetailPage.Instance.IsItemInGrid("Product Name", Product9) is true)
                {
                    BuildingPhaseDetailPage.Instance.DeleteItemInProductsGrid("Product Name", Product9);
                }

                BuildingPhaseDetailPage.Instance.ClickAddProductToPhaseModal();
                BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SelectProduct(Product9, 1);
                System.Threading.Thread.Sleep(3000);
                BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SelectTaxStatus("Phase", 1);
                System.Threading.Thread.Sleep(3000);
                BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SetDefault(true);
                BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.Save();
            }

            //23. Open Spec Sets Product Conversion page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 23. Open Spec Sets Product Conversion page, click “+” on the right.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData.GroupName);
            }

            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData.GroupName);

            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            Assert.That(SpecSetDetailPage.Instance.IsModalDisplayed(), "The add new spect set modal is not displayed");
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData.SpectSetName);

            //Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(SpecSetData);
            if ($"Created Spec Set Product ({SpecSetData.OriginalProduct}) In Spec Set ({SpecSetData.SpectSetName})" != SpecSetDetailPage.Instance.GetLastestToastMessage())
                Console.WriteLine(SpecSetDetailPage.Instance.GetLastestToastMessage());
            ExtentReportsHelper.LogInformation("Created the Product Conversation in Spec Set.");
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(SpecSetData);

            //24. Click Edit button on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 24. Click Edit button on the right.</font>");
            SpecSetDetailPage.Instance.EditItemProductConversionsInGrid(SpecSetData.OriginalProduct);
            SpecSetDetailPage.Instance.UpdateProductConversion(NewSpecSetData);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(NewSpecSetData);

            //25.Open Product detail page page, click to the breadcrumb
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 25.Open Product detail page page, click to the breadcrumb.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
            }
            
            //26. Open BOM Logic rule page page, click “+” icon of the Conditions section
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 26. Open BOM Logic rule page page, click “+” icon of the Conditions section.</font>");
            BOMLogicRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BOMLogicRules);
            BOMLogicRulePage.Instance.FilterItemInGrid("Rule Name", GridFilterOperator.Contains, BOMLogicRuleData.RuleName);
            if (BOMLogicRulePage.Instance.IsItemInGrid("Rule Name", BOMLogicRuleData.RuleName) is true)
            {
                BOMLogicRulePage.Instance.DeleteBOMLogicRule(BOMLogicRuleData);
            }

            BOMLogicRulePage.Instance.CreateNewBOMLogicRule(BOMLogicRuleData);
            BOMLogicRulePage.Instance.SelectItemInGrid("Rule Name", BOMLogicRuleData.RuleName);

            BOMLogicRuleDetailPage.Instance.ClickAddToShowCreateACondition();
            BOMLogicRuleDetailPage.Instance.SelectCondition(BOMLogicRuleDetailData);

            //27. Select Operator is EQUAL
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 27. Select Operator is EQUAL.</font>");
            BOMLogicRuleDetailPage.Instance.SelectOperator(BOMLogicRuleDetailData.Operator);

            //28. Select Operator is IN
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 28. Select Operator is IN.</font>");
            BOMLogicRuleDetailPage.Instance.SelectOperator("IN");
            BOMLogicRuleDetailPage.Instance.CloseModal();

            //29. Click on Product field then verify the list of products that is shown
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 29. Click on Product field then verify the list of products that is shown.</font>");
            //30. Enter the value to filter product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 30. Enter the value to filter product.</font>");
            //31. Select another Product then verify the list of products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 31. Select another Product then verify the list of products.</font>");

            BOMLogicRuleDetailPage.Instance.ClickAddToShowCreateACondition();
            BOMLogicRuleDetailPage.Instance.SelectCondition(BOMLogicRuleDetailData);
            BOMLogicRuleDetailPage.Instance.SelectOperator("IN");
            BOMLogicRuleDetailPage.Instance.CheckFunctionalProductModal();
            BOMLogicRuleDetailPage.Instance.CloseModal();
            
            //32. Add value into all fields and click Save button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 32. Add value into all fields and click Save button.</font>");
            
            string searchProduct = "QA_RT_Product0";
            BOMLogicRuleDetailPage.Instance.ClickAddToShowCreateACondition();
            BOMLogicRuleDetailPage.Instance.SelectCondition(BOMLogicRuleDetailData);
            BOMLogicRuleDetailPage.Instance.SelectOperator("IN");
            BOMLogicRuleDetailPage.Instance.SelectConditionValueForProduct(searchProduct, BOMLogicRuleDetailData.ConditionValue);
            BOMLogicRuleDetailPage.Instance.Save();
            string actual_ConditionMsg = BOMLogicRuleDetailPage.Instance.GetLastestToastMessage();
            string expected_ConditionMsg = "Condition was created successfully.";
            if (actual_ConditionMsg.Equals(expected_ConditionMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Condition was created successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Condition is create unsuccessfully. Actual message: <i>{actual_ConditionMsg}</i></font>");
            }


            //33. Click Edit icon on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 33. Click Edit icon on the right.</font>");

            string ConditionValue = "QA_RT_Product02_Auto, QA_RT_Product01_Auto";
            if (BOMLogicRuleDetailPage.Instance.IsItemInGrid("Condition Value", ConditionValue))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Condition with Name {ConditionValue} is displayed correctly.</b></font>");
                BOMLogicRuleDetailPage.Instance.ClickEditItemGrid("Condition Value", ConditionValue);
                BOMLogicRuleDetailPage.Instance.UpdateConditionValueForProduct(searchProduct, NewBOMLogicRuleDetailData.ConditionValue);

                actual_ConditionMsg = BOMLogicRuleDetailPage.Instance.GetLastestToastMessage();
                expected_ConditionMsg = "Condition was updated successfully.";
                if (actual_ConditionMsg.Equals(expected_ConditionMsg))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Condition was updated successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The Condition is update unsuccessfully. Actual message: <i>{actual_ConditionMsg}</i></font>");
                }

            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Condition with Name {ConditionValue} is display incorrectly.</i></font>");
            }


            //34. Select Operator is NOT EQUAL
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 34. Select Operator is NOT EQUAL.</font>");
            BOMLogicRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BOMLogicRules);
            BOMLogicRulePage.Instance.FilterItemInGrid("Rule Name", GridFilterOperator.Contains, BOMLogicRuleData.RuleName);
            if (BOMLogicRulePage.Instance.IsItemInGrid("Rule Name", BOMLogicRuleData.RuleName) is true)
            {
                BOMLogicRulePage.Instance.DeleteBOMLogicRule(BOMLogicRuleData);
                BOMLogicRulePage.Instance.CreateNewBOMLogicRule(BOMLogicRuleData);
            }

            BOMLogicRulePage.Instance.SelectItemInGrid("Rule Name", BOMLogicRuleData.RuleName);


            BOMLogicRuleDetailPage.Instance.ClickAddToShowCreateACondition();
            BOMLogicRuleDetailPage.Instance.SelectCondition(BOMLogicRuleDetailData);
            BOMLogicRuleDetailPage.Instance.SelectOperator("NOT EQUAL");

            //35. Select Operator is NOT IN
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 35. Select Operator is NOT IN.</font>");
            BOMLogicRuleDetailPage.Instance.SelectOperator("NOT IN");
            BOMLogicRuleDetailPage.Instance.CloseModal();
            
            //36. Open Unit Products page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 36. Open Unit Products page, click “+” on the right.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_UNITS_URL);
            ExtentReportsHelper.LogInformation($"Filter new item {_unitData.Name} in the grid view.");
            UnitPage.Instance.FilterItemInGrid("Abbreviation", GridFilterOperator.Contains, _unitData.Abbreviation);
            if (!UnitPage.Instance.IsItemInGrid("Abbreviation", _unitData.Abbreviation))
            {
                UnitPage.Instance.ClickAddToShowUnitModal();
                UnitPage.Instance.CreateUnitAndVerify(_unitData.Abbreviation, _unitData.Name, _unitData.Message);
            }

            UnitPage.Instance.SelectItemInGrid("Abbreviation", _unitData.Abbreviation);

            if (UnitDetailPage.Instance.IsDisplayDataCorrectly(_unitData) is true)
                ExtentReportsHelper.LogPass($"The Unit detail page of item: {_unitData.Name} displays correctly.");

            //Delete Data In Grid
            foreach (string productName in productList)
            {
                UnitDetailPage.Instance.DeleteItemInGrid("Product Name", productName);
            }

            //37. Click on Product field then verify the list of products that is shown
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 37. Click on Product field then verify the list of products that is shown.</font>");
            //38. Enter the value to filter product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 38. Enter the value to filter product.</font>");
            //39. Select another Building Phase that contains a large of products then verify the list of products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 39. Select another Building Phase that contains a large of products then verify the list of products.</font>");
            UnitDetailPage.Instance.CheckFunctionalProductModal();
            
            UnitDetailPage.Instance.OpenAddProductToUnitModal();
            
            if (!UnitDetailPage.Instance.AddProductToUnitModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Product to Unit\" modal doesn't display.");
                return;
            }

            //40. Select Product 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 40. Select Product.</font>");
            //41. Add value into all fields and click Save button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 41. Add value into all fields and click Save button.</font>");
            ExtentReportsHelper.LogInformation("Select product in the list and click save button.");
            UnitDetailPage.Instance.AddProductToUnitModal.AddProductToUnit(productList);

            var expectedMessage = "Products were successfully added.";
            var actualMessage = UnitDetailPage.Instance.GetLastestToastMessage();

            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add Product to Unit unsuccessfully. Actual messsage: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add Product to Unit successfully.");
            }
         
            //42. Open Categories Products page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 42. Open Categories Products page, click “+” on the right.</font>");
            ExtentReportsHelper.LogInformation("Navigate Estimating > Category and open Category Detail page.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CATEGORIES_URL);

            // Delete the category that has the same updated name to create a new one later
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _categoryData.Name);
            if (CategoryPage.Instance.IsItemInGrid("Name", _categoryData.Name) is true)
            {
                // Open detail page, if there are any product that assigned to category, then delete it.
                CategoryPage.Instance.SelectItemInGrid("Name", _categoryData.Name);
            }
            else
            {
                CategoryPage.Instance.CreateNewCategory(_categoryData.Name, _categoryData.Parent);
                CategoryPage.Instance.SelectItemInGrid("Name", _categoryData.Name);
            }

            //Add Product To Category
            CategoryDetailPage.Instance.AddProductToCategory();

            // Select 2 first items from the list
            CategoryDetailPage.Instance.AddProductToCategoryModal.AddProductToCategory(PHASE1_VALUE, Product1);
            CategoryDetailPage.Instance.WaitGridLoad();

             expectedMessage = "Products were successfully added.";
             actualMessage = CategoryDetailPage.Instance.GetLastestToastMessage();

            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add Product to Category unsuccessfully. Actual messsage: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add Product to Category successfully.");
            }

            
            //43. Open House Option Quantities page, verify that Product field must list out all relative product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 43. Open House Option Quantities page, verify that Product field must list out all relative product.</font>");
            ExtentReportsHelper.LogInformation(null, "Show Category on Add Option Product Modal - TURN ON ");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);

            EstimatingPage.Instance.Check_Show_On_Add_Option_Product_Modal(true);

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", _option.Name);
            }
            OptionDetailPage.Instance.LeftMenuNavigation("Products");

            //44. Add value into all fields and click Save button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 44. Add value into all fields and click Save button.</font>");
            ProductsToOptionPage.Instance.DeleteAllHouseOptionQuantities();
            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Building Phase", optionHouseOptionQuantitiesData.BuildingPhase) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionHouseOptionQuantitiesData);
            }

            //45 .Open Community Products page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 45 .Open Community Products page, click “+” on the right.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);
            }

            CommunityOptionPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE1_VALUE}'.</b></font>");

            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();

            //46. Click on Product field then the list of products that is shown  , Select a Product contains ' character in the Description
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 46. Click on Product field then the list of products that is shown  , Select a Product contains ' character in the Description.</font>");
            //47. Add value into all fields and click Save button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 47. Add value into all fields and click Save button.</font>");
            if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesData.BuildingPhase, communityQuantitiesData.ProductName) is false)
            {

                // Add a new option quantitiy if it doesn't exist
                CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesData);
            }

            //48.Open Spec Sets Product Conversion page, click “+” on the right
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 48.Open Spec Sets Product Conversion page, click “+” on the right.</font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating");
            EstimatingPage.Instance.Check_Show_Category_On_Product_Conversion(true);

            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData.GroupName);
            }


            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData.GroupName);

            ExtentReportsHelper.LogInformation(null, "<b>Add a Spec Set> Verify the spec set is added successfully</b>");
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData.SpectSetName);

            //Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            //49. Add value into all fields and click Save button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 49. Add value into all fields and click Save button.</font>");
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithCategory(SpecSetData);
            ExtentReportsHelper.LogInformation("Created the Product Conversation in Spec Set.");
            
        }

        [TearDown]
        public void DeleteData()
        {
            
            //Delete Product In Community Quantities
            ExtentReportsHelper.LogInformation(null, "Delete Product In Community Quantities.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);
                CommunityDetailPage.Instance.LeftMenuNavigation("Products");
                CommunityProductsPage.Instance.DeleteAllCommunityQuantities();
            }

            //Delete Product In Option Quantities
            ExtentReportsHelper.LogInformation(null, "Delete Product In Option Quantities.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", _option.Name);

                OptionDetailPage.Instance.LeftMenuNavigation("Products");

                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", optionQuantitiesData.BuildingPhase) is true)
                {
                    ProductsToOptionPage.Instance.DeleteItemInGrid("Building Phase", optionQuantitiesData.BuildingPhase);
                    ProductsToOptionPage.Instance.WaitOptionQuantitiesLoadingIcon();
                }

                ProductsToOptionPage.Instance.DeleteAllHouseOptionQuantities();
            }

            //Delete Product In CustomOption
            ExtentReportsHelper.LogInformation(null, "Delete Product In CustomOption.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL);
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, customOption.Code);

            if (CustomOptionPage.Instance.IsItemInGrid("Code", customOption.Code) is true)
            {
                CustomOptionPage.Instance.SelectItemInGrid("Code", customOption.Code);
                CustomOptionDetailPage.Instance.LeftMenuNavigation("Products");
                CustomOptionProduct.Instance.DeleteItemOnProductGird("Building Phase", ListBuildPhase[1].Trim());
                if (CustomOptionProduct.Instance.IsItemGird("Building Phase", ListBuildPhase[1].Trim()) is true)
                    ExtentReportsHelper.LogFail("Can't delete product on the grid view");
                else
                    ExtentReportsHelper.LogPass(null, "Deleted product successfully");
            }

            //Delete Product In Product Subcomponent
            ExtentReportsHelper.LogInformation(null, "Delete Product In Product Subcomponent.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
                ProductSubcomponentPage.Instance.DeleteAllProductSubcomponent();
            }

            //Delete Product In Product Subcomponent After Click Copy Subcomponent By Selective
            ExtentReportsHelper.LogInformation(null, "Delete Product In Product Subcomponent After Click Copy Subcomponent.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, Product9);
            if (ProductPage.Instance.IsItemInGrid("Product Name", Product9) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", Product9);
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
                ProductSubcomponentPage.Instance.DeleteAllProductSubcomponent();
            }

            //Delete Product In Product Subcomponent After Click Copy Subcomponent By Batch Copy
            ExtentReportsHelper.LogInformation(null, "Delete Product In Product Subcomponent After Click Copy Subcomponent By Batch Copy.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, Product10);
            if (ProductPage.Instance.IsItemInGrid("Product Name", Product10) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", Product10);
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
                ProductSubcomponentPage.Instance.DeleteAllProductSubcomponent();
            }

            //Delete Product In Unit Detail
            ExtentReportsHelper.LogInformation(null, "Delete Product In Unit Detail.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_UNITS_URL);
            ExtentReportsHelper.LogInformation($"Filter new item {_unitData.Name} in the grid view.");
            UnitPage.Instance.FilterItemInGrid("Abbreviation", GridFilterOperator.Contains, _unitData.Abbreviation);
            if (UnitPage.Instance.IsItemInGrid("Abbreviation", _unitData.Abbreviation) is true)
            {
                UnitPage.Instance.SelectItemInGrid("Abbreviation", _unitData.Abbreviation);

                ExtentReportsHelper.LogInformation("Delete product out of unit.");

                foreach (string productName in productList)
                {
                    UnitDetailPage.Instance.DeleteItemInGrid("Product Name", productName);

                    var expectedMessage = "Product successfully removed.";
                    var actualMessage = UnitDetailPage.Instance.GetLastestToastMessage();

                    if (actualMessage == expectedMessage)
                    {
                        ExtentReportsHelper.LogPass($"Product {productName} deleted successfully!");
                        UnitDetailPage.Instance.CloseToastMessage();
                    }
                    else
                    {
                        if (UnitDetailPage.Instance.IsItemInGrid("Product Name", productName))
                            ExtentReportsHelper.LogFail($"Product {productName} can't be deleted!");
                        else
                            ExtentReportsHelper.LogPass($"Product {productName} deleted successfully!");

                    }
                }

            }
            
            //Delete Product In Category Detail
            ExtentReportsHelper.LogInformation(null, "Delete Product In Category Detail.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CATEGORIES_URL);
            // Delete the category that has the same updated name to create a new one later
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _categoryData.Name);
            if (CategoryPage.Instance.IsItemInGrid("Name", _categoryData.Name) is true)
            {
                // Open detail page, if there are any product that assigned to category, then delete it.
                CategoryPage.Instance.SelectItemInGrid("Name", _categoryData.Name);

                if (CategoryDetailPage.Instance.getNumberProductOnGrid() != 0)
                {
                    CategoryDetailPage.Instance.DeleteAllProduct();
                    CategoryDetailPage.Instance.WaitGridLoad();
                }
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
