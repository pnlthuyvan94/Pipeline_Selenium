using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Category;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_IV
{
    class B01_G_PIPE_34177 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private static string PRODUCT_NAME = "QA_RT_Product1_PIPE_34177_Automation";
        private readonly string BUILDINGPHASE_SUBCOMPONENT_DEFAULT = "QA_1-QA_BuildingPhase_01_Automation";
        private readonly string PRODUCT_SUBCOMPONENT_NAME_DEFAULT = "QA_RT_Product2_PIPE_34177_Automation";

        private readonly string CATEGORY_NAME_DEFAULT = "Category_5";

        private readonly string STYLE1_NAME_DEFAULT = "QA_Style_PIPE1_Automation";
        private readonly string STYLE2_NAME_DEFAULT = "QA_Style_PIPE2_Automation";
        private readonly string STYLE3_NAME_DEFAULT = "QA_Style_PIPE3_Automation";
        private readonly string STYLE4_NAME_DEFAULT = "QA_Style_PIPE4_Automation";
        private readonly string STYLE5_NAME_DEFAULT = "QA_Style_PIPE5_Automation";


        private readonly string MANUFACTURE1_NAME_DEFAULT = "QA_Manu_PIPE1_Automation";
        private readonly string MANUFACTURE2_NAME_DEFAULT = "QA_Manu_PIPE2_Automation";
        private readonly string MANUFACTURE3_NAME_DEFAULT = "QA_Manu_PIPE3_Automation";
        private readonly string MANUFACTURE4_NAME_DEFAULT = "QA_Manu_PIPE4_Automation";
        private readonly string MANUFACTURE5_NAME_DEFAULT = "QA_Manu_PIPE5_Automation";

        private readonly string PRODUCTCODE_NAME_DEFAULT = "";
        private readonly bool IS_CREATED = true;
        private readonly bool IS_NOT_CREATE = false;
        private ProductData product1;
        private ProductData product2;
        string ProductSubcomponentUrl;
        [SetUp]
        public void GetTestData()
        {

            product1 = new ProductData()
            {
                Name = "QA_RT_Product1_PIPE_34177_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation"
            };

            product2 = new ProductData()
            {
                Name = "QA_RT_Product2_PIPE_34177_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation"
            };

            //Prepare a new Category to import Product
            ExtentReportsHelper.LogInformation(null, "Prepare a new Category to import Product.");
            CategoryPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Categories);

            CategoryData CategoryData = new CategoryData()
            {
                Name = "Category_5",
                Parent = "NONE"
            };

            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, CategoryData.Name);
            if (CategoryPage.Instance.IsItemInGrid("Name", CategoryData.Name) is false)
            {
                CategoryPage.Instance.CreateNewCategory(CategoryData.Name, CategoryData.Parent);
            }

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "Prepare a new Manufacturer to import Product.");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Style to import Product.");
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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Building Group to import Product.");
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
            ExtentReportsHelper.LogInformation(null, "Prepare data: Import Building Phase to import Product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_34177\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "Prepare Data: Import Product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_34177\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            //Delete Data Before Start Test script
            //Delete Style In Product Detail 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Style In Product Detail .</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, product2.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", product2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", product2.Name);

                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE5_NAME_DEFAULT) is true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", STYLE5_NAME_DEFAULT);
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }

                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE1_NAME_DEFAULT) is true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", STYLE1_NAME_DEFAULT);
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }

                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE2_NAME_DEFAULT) is true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", STYLE2_NAME_DEFAULT);
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }

                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE5_NAME_DEFAULT) is true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", STYLE5_NAME_DEFAULT);
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }

                //Delete Style And Manufacture
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Style And Manufacture.</font>");
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
                StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, STYLE1_NAME_DEFAULT);
                if (StylePage.Instance.IsItemInGrid("Name", STYLE1_NAME_DEFAULT) is true)
                {
                    // If Style doesn't exist then create a new one
                    StylePage.Instance.DeleteItemInGrid("Name", STYLE1_NAME_DEFAULT);
                    if ("Style was deleted!" == StylePage.Instance.GetLastestToastMessage())
                    {
                        ExtentReportsHelper.LogPass($"Product Style {STYLE1_NAME_DEFAULT} deleted successfully.");
                    }
                }
                StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, STYLE2_NAME_DEFAULT);
                if (StylePage.Instance.IsItemInGrid("Name", STYLE2_NAME_DEFAULT) is true)
                {
                    // If Style doesn't exist then create a new one
                    StylePage.Instance.DeleteItemInGrid("Name", STYLE2_NAME_DEFAULT);
                    if ("Style was deleted!" == StylePage.Instance.GetLastestToastMessage())
                    {
                        ExtentReportsHelper.LogPass($"Product Style {STYLE2_NAME_DEFAULT} deleted successfully.");
                    }
                }

                StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, STYLE5_NAME_DEFAULT);
                if (StylePage.Instance.IsItemInGrid("Name", STYLE5_NAME_DEFAULT) is true)
                {
                    // If Style doesn't exist then create a new one
                    StylePage.Instance.DeleteItemInGrid("Name", STYLE5_NAME_DEFAULT);
                    if ("Style was deleted!" == StylePage.Instance.GetLastestToastMessage())
                    {
                        ExtentReportsHelper.LogPass($"Product Style {STYLE5_NAME_DEFAULT} deleted successfully.");
                    }
                }

                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);
                ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, MANUFACTURE1_NAME_DEFAULT);
                if (ManufacturerPage.Instance.IsItemInGrid("Name", MANUFACTURE1_NAME_DEFAULT) is true)
                {
                    // If Manu doesn't exist then create a new one
                    ManufacturerPage.Instance.DeleteManufacturer(MANUFACTURE1_NAME_DEFAULT);
                }

                ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, MANUFACTURE2_NAME_DEFAULT);
                if (ManufacturerPage.Instance.IsItemInGrid("Name", MANUFACTURE2_NAME_DEFAULT) is true)
                {
                    // If Manu doesn't exist then create a new one
                    ManufacturerPage.Instance.DeleteManufacturer(MANUFACTURE2_NAME_DEFAULT);
                }

                ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, MANUFACTURE5_NAME_DEFAULT);
                if (ManufacturerPage.Instance.IsItemInGrid("Name", MANUFACTURE5_NAME_DEFAULT) is true)
                {
                    // If Manu doesn't exist then create a new one
                    ManufacturerPage.Instance.DeleteManufacturer(MANUFACTURE5_NAME_DEFAULT);
                }

                // Close current tab
                CommonHelper.CloseAllTabsExcludeCurrentOne();
            }
        }
        [Test]
        [Category("Section_IV")]
        public void B01_G_Estimating_DetailPage_Products_Subcomponents_Adding_Subcomponent_Product_add_Create_Style_Buttons()
        {
            //1. Verify that the inline will be displayed when user click on “Add Style“ button at Basic tab
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1. Verify that the inline will be displayed when user click on “Add Style“ button at Basic tab.</font>");
            // Navigate setting/product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate setting/product Turn OFF Show Show Category on Add Product SubcomponentModal.</font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(true);
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, product1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", product1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", product1.Name);

                //Navigate To Subcomponents
                ExtentReportsHelper.LogInformation(null, "<b> Navigate To Subcomponents</b>");
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
                ProductSubcomponentUrl = ProductSubcomponentPage.Instance.CurrentURL;
            }

            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //Add subcomponent with type is Basic 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add subcomponent with type is Basic</b></font color>");
            // Click add subcomponent
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .VerifyAddNewStyleInSubcomponent();
            ProductSubcomponentPage.Instance.Close_Modal("Add Product");

            //2. Verify that the inline will be displayed when user click on “Add Style“ button at Advanced tab
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2. Verify that the inline will be displayed when user click on “Add Style“ button at Advanced tab.</font>");

            //Add subcomponent with type is Advanced 
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Advanced")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .VerifyAddNewStyleInSubcomponent();

            ProductSubcomponentPage.Instance.Close_Modal("Add Product");

            //3. Verify that can add new Manufacturer, Style and Product Code successfully on the inline at Basic tab
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3. Verify that can add new Manufacturer, Style and Product Code successfully on the inline at Basic tab.</font>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .AddNewStyleInSubcomponent(STYLE1_NAME_DEFAULT, MANUFACTURE1_NAME_DEFAULT, PRODUCTCODE_NAME_DEFAULT, IS_CREATED);

            //4. Verify that can add new Manufacturer, Style and Product Code successfully on the inline at Advanced tab
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4. Verify that can add new Manufacturer, Style and Product Code successfully on the inline at Advanced tab.</font>");

            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Advanced")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .AddNewStyleInSubcomponent(STYLE2_NAME_DEFAULT, MANUFACTURE2_NAME_DEFAULT, PRODUCTCODE_NAME_DEFAULT, IS_CREATED);

            //5. Verify that system doesn’t create new data of Manufacturer, Style and Product Code on the inline at Basic tab
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5. Verify that system doesn’t create new data of Manufacturer, Style and Product Code on the inline at Basic tab.</font>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .AddNewStyleInSubcomponent(STYLE3_NAME_DEFAULT, MANUFACTURE3_NAME_DEFAULT, PRODUCTCODE_NAME_DEFAULT, IS_NOT_CREATE);

            //6. Verify that system doesn’t create new data of Manufacturer, Style and Product Code on the inline at Advanced tab
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 6. Verify that system doesn’t create new data of Manufacturer, Style and Product Code on the inline at Advanced tab.</font>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Advanced")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .AddNewStyleInSubcomponent(STYLE4_NAME_DEFAULT, MANUFACTURE4_NAME_DEFAULT, PRODUCTCODE_NAME_DEFAULT, IS_NOT_CREATE);
            ProductSubcomponentPage.Instance.Close_Modal("Add Product");

            //7. Verify that new data of Manufacturer, Style and Product Code will display on Product detail page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 7. Verify that new data of Manufacturer, Style and Product Code will display on Product detail page.</font>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .AddNewStyleInSubcomponent(STYLE5_NAME_DEFAULT, MANUFACTURE5_NAME_DEFAULT, PRODUCTCODE_NAME_DEFAULT, IS_CREATED);
            ProductSubcomponentPage.Instance.Close_Modal("Add Product");

            //Open Product Name And Check Style In Product Deatail
            ExtentReportsHelper.LogInformation(null, "Open Product Name And Check Style In Product Deatail.");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, product2.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", product2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", product2.Name);

                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE5_NAME_DEFAULT) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Style with name {STYLE5_NAME_DEFAULT} is show in Product Detail.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Style with name {STYLE5_NAME_DEFAULT} is not show in Product Detail..</b></font>");
                }
            }
            //8. Verify error message when user inserts duplicated Manufacturer and Style Basic tab
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 8. Verify error message when user inserts duplicated Manufacturer and Style Basic tab.</font>");
            CommonHelper.OpenURL(ProductSubcomponentUrl);
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .AddNewStyleInSubcomponentWithInvalid(STYLE1_NAME_DEFAULT, MANUFACTURE1_NAME_DEFAULT);
            ProductSubcomponentPage.Instance.Close_Modal("Add Product");

            //9. Verify error message when user inserts duplicated Manufacturer and Style Advanced tab
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 9. Verify error message when user inserts duplicated Manufacturer and Style Advanced tab.</font>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Advanced")
                                            .SelectBuildingPhaseOfProduct(product1.BuildingPhase)
                                            .SelectStyleOfProduct(product1.Style)
                                            .SelectCatelogy(CATEGORY_NAME_DEFAULT)
                                            .InputProductSubcomponent(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .AddNewStyleInSubcomponentWithInvalid(STYLE2_NAME_DEFAULT, MANUFACTURE2_NAME_DEFAULT);
            ProductSubcomponentPage.Instance.Close_Modal("Add Product");
        }
    }
}


