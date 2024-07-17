using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Common.Constants;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Estimating.Category;
using Pipeline.Testing.Pages.Estimating.Category.CategoryDetail;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.Assets.Options;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class B01_D_RT_01247 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private ProductData _product;
        private ProductData getNewproduct;
        private OptionData _option;
        private string productName = "QA_RT_Product_Subcomponent1_Automation";

        private readonly string BuildingPhaseOfProduct1 = "QA_1-QA_BuildingPhase_01_Automation";
        private readonly string BuildingPhaseOfProduct2 = "QA_2-QA_BuildingPhase_02_Automation";
        private readonly string BuildingPhaseOfProduct3 = "QA_3-QA_BuildingPhase_03_Automation";
        private readonly string BuildingPhaseOfProduct4 = "QA_4-QA_BuildingPhase_04_Automation";
        private readonly string BuildingPhaseOfProduct6 = "QA_6-QA_BuildingPhase_06_Automation";

        private readonly string StyleOfProduct = "QA_Style_Automation";

        private readonly string Category2 = "QA_Category_02_Automation";

        private readonly string PRODUCT2 = "QA_Product_02_Automation";
        private readonly string PRODUCT3 = "QA_Product_03_Automation";
        private readonly string PRODUCT4 = "QA_Product_04_Automation";
        private readonly string PRODUCT5 = "QA_Product_05_Automation";
        private readonly string Product6 = "QA_Product_06_Automation";

        private readonly string OPTION_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private readonly string OPTION_CODE_DEFAULT = "0100";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        [SetUp]
        public void SetUp()
        {
            _product = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent1_Automation",
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
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name) is false)
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_RT_01247\\Pipeline_BuildingPhases.csv";
            ImportValidData(BUILDING_GROUP_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_RT_01247\\Pipeline_Products.csv";
            ImportValidData(PRODUCT_IMPORT, importFile);

            // Prepare a new Category 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Category.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CATEGORIES_URL);
            CategoryData CategoryData = new CategoryData()
            {
                Name = "QA_Category_02_Automation",
                Parent = "NONE"
            };

            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, CategoryData.Name);
            if (CategoryPage.Instance.IsItemInGrid("Name", CategoryData.Name) is false)
            {
                CategoryPage.Instance.CreateNewCategory(CategoryData.Name, CategoryData.Parent);

            }

            CategoryPage.Instance.SelectItemInGrid("Name", CategoryData.Name);

            ExtentReportsHelper.LogInformation("Step 3: Add Product to Category.");
            if (CategoryDetailPage.Instance.IsItemInGrid("Product Name", PRODUCT2) is false)
            {
                AddProductToCategory(BuildingPhaseOfProduct2, PRODUCT2);
            }
            if (CategoryDetailPage.Instance.IsItemInGrid("Product Name", PRODUCT3) is false)
            {
                AddProductToCategory(BuildingPhaseOfProduct3, PRODUCT3);
            }

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

        }




        [Test]
        [Category("Section_IV")]
        public void B01_E_Estimating_DetailPage_Product_Subcomponents()
        {
            // Step 1: Navigate to Pipeline products; Create a product
            ExtentReportsHelper.LogInformation("<b>Step 1: Navigate to Pipeline products; Create a product.</b>");
            //ProductSubcomponentPage.Instance.NavigateURL("ProductAssemblies/Products/ProductSubcomponents.aspx?pid=27195");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            //Step 2: In Products data page, click the Product to which you would like to select
            ExtentReportsHelper.LogInformation("<b>Step 2: In Products data page, click the Product to which you would like to select.</b>");
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
                if (ProductDetailPage.Instance.IsCreateSuccessfully(getNewproduct) is true)
                {
                    ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, "<font color ='red'>Create isn't successful Product.</font>");
                }
            }


            // Step 3: In Product Side Navigation, click the Subcomponents
            ExtentReportsHelper.LogInformation(null, "<b>Step 3: In Product Side Navigation, click the Subcomponents</b>");
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            string ProductSubcomponent_url = ProductSubcomponentPage.Instance.CurrentURL;
            //Step 4: Opened successfully the Subcomponents data page
            ExtentReportsHelper.LogInformation(null, "<b>Step 4: Opened successfully the Subcomponents data page</b>");

            // Verify opened successfully Product Assignment page
            ProductSubcomponentPage.Instance.IsProductSubcomponentPage();

            ExtentReportsHelper.LogInformation(null, "<b>Step 5. Click the '+' button to add Subcomponents into Product</b>");

            //Step 6. Show Category on Add Spec Set Product Conversion Modal - TURN ON

            ExtentReportsHelper.LogInformation(null, "<b> A. Show Category on Add Spec Set Product Conversion Modal - TURN ON </b>");
            // Navigate setting/product
            ProductSubcomponentPage.Instance.NavigateURL("Products/Settings/Default.aspx");
            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(true);
            CommonHelper.OpenURL(ProductSubcomponent_url);

            //Add subcomponent with type is Basic 
            ExtentReportsHelper.LogInformation(null, "<b> Add subcomponent with type is Basic </b>");
            // Click add subcomponent
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(BuildingPhaseOfProduct1)
                                            .SelectCatelogy(Category2)
                                            .InputProductSubcomponent(PRODUCT2)
                                            .SelectBuildingPhaseOfSubComponent(BuildingPhaseOfProduct2)
                                            .SelectOptionSubcomponent(_option.Name + " - " + _option.Number)
                                            .ClickSaveProductSubcomponent();
            //Verify add subcomponent
            string expectedMess = "Successfully added new subcomponent!";
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BuildingPhaseOfProduct1);

            // Click add subcomponent with type is Advanced
            ExtentReportsHelper.LogInformation(null, "<b> Add subcomponent with type is Advance </b>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Advanced")
                                            .SelectBuildingPhaseOfProduct(BuildingPhaseOfProduct1)
                                            .SelectCatelogy(Category2)
                                            .InputProductSubcomponent(PRODUCT3)
                                            .SelectBuildingPhaseOfSubComponent(BuildingPhaseOfProduct3)
                                            .SelectOptionSubcomponent(_option.Name + " - " + _option.Number)
                                            .ClickSaveProductSubcomponent();
            //Verify add subcomponent
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BuildingPhaseOfProduct3);

            ExtentReportsHelper.LogInformation(null, "<b> B. Show Category on Add Spec Set Product Conversion Modal - TURN OFF </b>");
            // Navigate setting/product
            ProductSubcomponentPage.Instance.NavigateURL("Products/Settings/Default.aspx");
            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(false);
            CommonHelper.OpenURL(ProductSubcomponent_url);
            ExtentReportsHelper.LogInformation(null, "<b> Add subcomponent with type is Basic </b>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(BuildingPhaseOfProduct1)
                                            .SelectChildBuildingPhaseOfSubComponent(BuildingPhaseOfProduct4)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT4)
                                            .SelectChildStyleOfSubComponent(StyleOfProduct)
                                            .SelectOptionSubcomponent(_option.Name + " - " + _option.Number)
                                            .ClickSaveProductSubcomponent();
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BuildingPhaseOfProduct4);

            ExtentReportsHelper.LogInformation(null, "<b> Add subcomponent with type is Advance </b>");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Advanced")
                                            .SelectBuildingPhaseOfProduct(BuildingPhaseOfProduct1)
                                            .SelectStyleOfProduct(StyleOfProduct)
                                            .SelectChildBuildingPhaseOfSubComponent(BuildingPhaseOfProduct6)
                                            .InputProductSubcomponentWithoutCategory(Product6)
                                            .SelectOptionSubcomponent(_option.Name + " - " + _option.Number)
                                            .ClickSaveProductSubcomponent();
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BuildingPhaseOfProduct6);
            ProductSubcomponentPage.Instance.Close_Modal("Add Product");

            ExtentReportsHelper.LogInformation(null, "<b> Click the 'Edit' button to update Product Subcomponents information </b>");
            ProductSubcomponentPage.Instance.ClickEditInGird(BuildingPhaseOfProduct2);
            //ProductSubcomponentPage.Instance.SelectUseEitSubcomponent("KN_Use");
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

            ExtentReportsHelper.LogInformation(null, "<b> Verify the 'Assign Show Product' button on page </b>");
            ProductSubcomponentPage.Instance.AssignShowProductButton_Click()
                                            .AssignProductModal_SelectBuildingPhase(BuildingPhaseOfProduct1)
                                            .AssignProductModal_ClickSave();
            string act_AssignMessage = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            string expected_AssignMessage = "Successfully updated Show Product";
            if (act_AssignMessage == expected_AssignMessage)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b> Assigned show Product successfully </b></font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>{act_AssignMessage}</font color>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }

            ExtentReportsHelper.LogInformation(null, "<b> 6. Verify the 'Copy Subcomponents' button </b>");
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
                                            .CopySubcomponentModal_SelectBuildingPhaseFrom(_product.BuildingPhase)
                                            .CopySubcomponentModal_SelectSubcomponentToCopyFrom(BuildingPhaseOfProduct2)
                                            .CopySubcomponentModal_SelectSubcomponentToCopyFrom(BuildingPhaseOfProduct3)
                                            .CopySubcomponentModal_SelectBuildingPhaseTo(BuildingPhaseOfProduct4)
                                            .CopySubcomponentModal_SelectProduct(PRODUCT4)
                                            .CopySubcomponentModal_ClickSave();
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

            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT4);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT4) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT4);
            }
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

            ExtentReportsHelper.LogPass("<font color ='green'><b> Open the copied Product; the Subcomponent is displayed successfully </b></font color>");
            ExtentReportsHelper.LogInformation("Delete Subcomponent be copied");
            ProductSubcomponentPage.Instance.ClickDeleteInGird(BuildingPhaseOfProduct2);
            ProductSubcomponentPage.Instance.ClickDeleteInGird(BuildingPhaseOfProduct3);

            //Step 7: Verify the Subcomponents filter
            ExtentReportsHelper.LogInformation(null, "<b> Step 7. Verify the Subcomponents filter </b>");
            CommonHelper.OpenURL(ProductSubcomponent_url);
            ProductSubcomponentPage.Instance.FilterSubcomponentInGird(BuildingPhaseOfProduct1);
            ExtentReportsHelper.LogPass("<font color ='green'><b> Verify the Subcomponents filter is successfully </b></font color>");
            // Refresh subcomponent detail page
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<b>The ability to delete the unassigned newly created item from the UI </b>");
            DeleteSubcomponent(BuildingPhaseOfProduct2);
            DeleteSubcomponent(BuildingPhaseOfProduct3);
            DeleteSubcomponent(BuildingPhaseOfProduct4);
            DeleteSubcomponent(BuildingPhaseOfProduct6);

            //Step 8. The options displayed on copy modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8. The options displayed on copy modal</b></font color>");
            //Go to product detail/Subcomponents > Click on the “Copy Subcomponent> Select the data and check
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
            }

            //Navigate To Subcomponents
            ExtentReportsHelper.LogInformation(null, "<b> Navigate To Subcomponents</b>");
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            ExtentReportsHelper.LogInformation(null, "Create a subcomponent inside a product, remember to add dependent Option above, and check result");
            // Click add subcomponent
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(_product.BuildingPhase)
                                            .SelectStyleOfProduct(_product.Style)
                                            .SelectChildBuildingPhaseOfSubComponent(BuildingPhaseOfProduct2)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT2)
                                            .SelectChildStyleOfSubComponent(StyleOfProduct)
                                            .SelectOptionSubcomponent(_option.Name + " - " + _option.Number)
                                            .ClickSaveProductSubcomponent();
            //Verify Options List In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Options List In Grid</b></font color>");
            ProductSubcomponentPage.Instance.VerifyOptionsListInGrid(BuildingPhaseOfProduct2, OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);

            //Click on the “Copy Subcomponent” button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click on the “Copy Subcomponent” button</b></font color>");
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
                                            .CopySubcomponentModal_SelectBuildingPhaseFrom(BuildingPhaseOfProduct1)
                                            .CopySubcomponentModal_SelectSubcomponentToCopyFrom(BuildingPhaseOfProduct2);

            //Subcomponents: Filter the data and check the option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Subcomponents: Filter the data and check the option</b></font color>");
            ProductSubcomponentPage.Instance.VerifyOptionsListInCopySubcomponent(BuildingPhaseOfProduct2, OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            //On the copy Subcomponents: Filter the data and select he product to “Copy to”     
            ExtentReportsHelper.LogInformation(null, "<b>On the copy Subcomponents: Filter the data and select he product to “Copy to”     </b>");
            ProductSubcomponentPage.Instance.CopySubcomponentModal_SelectBuildingPhaseTo(BuildingPhaseOfProduct3)
                                                .CopySubcomponentModal_SelectProduct(PRODUCT5)
                                                .CopySubcomponentModal_ClickSave();
            act_CopyMessage = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            expected_CopyMessage = "1 subcomponent(s) successfully copied.";
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
            //Step 9: The description product show on the copy modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9: The description product show on the copy modal.</font><b>");
            //Verify Options List In Grid
            ProductSubcomponentPage.Instance.VerifyOptionsListInGrid(BuildingPhaseOfProduct2, OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            //The description product show on the copy modal

            //Show Subcomponent Description is turned false
            //The description product show on the copy modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Show Subcomponent Description is turned false.</font><b>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            string EstimatingSetting_url = EstimatingPage.Instance.CurrentURL;
            EstimatingPage.Instance.VerifySettingEstimatingPageIsDisplayed();
            EstimatingPage.Instance.Check_Show_Subcomponent_Description(false);

            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT5);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT5) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT5);
            }
            //Navigate To Subcomponent Page
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            //Verify Subcomponent Name In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Subcomponent Name In Grid.</font><b>");
            ProductSubcomponentPage.Instance.VerifySubcomponentNameOnGrid(PRODUCT2, string.Empty);
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
            .CopySubcomponentModal_SelectBuildingPhaseFrom(BuildingPhaseOfProduct3);
            ProductSubcomponentPage.Instance.VerifySubcomponentNameInCopySubcomponentModal(PRODUCT2 + " - " + "QA Testing");

            //Show Subcomponent Description is turned true
            ExtentReportsHelper.LogInformation(null, "Show Subcomponent Description is turned true");
            //.Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Show Subcomponent Description is turned true.</font><b>");
            CommonHelper.OpenURL(EstimatingSetting_url);
            EstimatingPage.Instance.Check_Show_Subcomponent_Description(true);

            //Go to product detail/Subcomponents > Click on the “Copy Subcomponent> Select the data and check
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Go to product detail/Subcomponents > Click on the “Copy Subcomponent> Select the data and check.</font><b>");
            //Go to Product Detail Page
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT5);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT5) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT5);
            }
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

            //Verify Subcomponent Name With show Description
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Subcomponent Name With show Description.</font><b>");
            ProductSubcomponentPage.Instance.VerifySubcomponentNameOnGrid(PRODUCT2, " - " + "QA Testing");
            //Click on the “Copy Subcomponent” button
            ProductSubcomponentPage.Instance.CopySubComponentButton_Click()
            .CopySubcomponentModal_SelectBuildingPhaseFrom(BuildingPhaseOfProduct3);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Subcomponent Name In Copy SubComponent Modal With show Description.</font><b>");
            ProductSubcomponentPage.Instance.VerifySubcomponentNameInCopySubcomponentModal(PRODUCT2 + " - " + "QA Testing");
            //Refresh Page
            CommonHelper.RefreshPage();

            //Step 10: Select the trash can to delete the Subcomponent; select OK to confirm; verify successfully delete
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10: Select the trash can to delete the Subcomponent; select OK to confirm; verify successfully delete.</font><b>");
            ProductSubcomponentPage.Instance.ClickDeleteInGird(BuildingPhaseOfProduct2);
            string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            if (act_mess == "Successfully deleted subcomponent")
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BuildingPhaseOfProduct2} subcomponent </b></font color>");
            }
            else
                ExtentReportsHelper.LogFail($"<b> Cannot delete {BuildingPhaseOfProduct2} subcomponent </b>");
            ProductSubcomponentPage.Instance.CloseToastMessage();

            //Step 11: Navigate to Pipeline products. Select the trash can to delete the Product; select OK to confirm; verify successfully delete
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11: Navigate to Pipeline products. Select the trash can to delete the Product; select OK to confirm; verify successfully delete.</font><b>");
            //.Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product.Name);
                //Navigate To Subcomponents
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                ProductSubcomponentPage.Instance.ClickDeleteInGird(BuildingPhaseOfProduct2);
                act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
                if (act_mess == "Successfully deleted subcomponent")
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BuildingPhaseOfProduct2} subcomponent </b></font color>");
                }
                else
                    ExtentReportsHelper.LogFail($"<b> Cannot delete {BuildingPhaseOfProduct2} subcomponent </b>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }
        }
        public void DeleteSubcomponent(string subcomponent)
        {
            ProductSubcomponentPage.Instance.ClickDeleteInGird(subcomponent);
            string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            if (act_mess == "Successfully deleted subcomponent")
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {subcomponent} subcomponent </b></font color>");
            }
            else
                ExtentReportsHelper.LogFail($"<b> Cannot delete {subcomponent} subcomponent </b>");
            ProductSubcomponentPage.Instance.CloseToastMessage();
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

        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change Subcomponent Description is turned false.</font><b>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            EstimatingPage.Instance.Check_Show_Subcomponent_Description(false);

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CATEGORIES_URL);
            CategoryData CategoryData = new CategoryData()
            {
                Name = "QA_Category_02_Automation",
                Parent = "NONE"
            };
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, CategoryData.Name);
            if (CategoryPage.Instance.IsItemInGrid("Name", CategoryData.Name) is true)
            {
                CategoryPage.Instance.SelectItemInGrid("Name", CategoryData.Name);

                if (CategoryDetailPage.Instance.getNumberProductOnGrid() != 0)
                {
                    CategoryDetailPage.Instance.DeleteAllProduct();
                    CategoryDetailPage.Instance.WaitGridLoad();
                }

            }

        }
        private void ImportValidData(string importGridTitlte, string fullFilePath)
        {
            string actualMess = ProductsImportPage.Instance.ImportFile(importGridTitlte, fullFilePath);

            string expectedMess = "Import complete.";
            if (expectedMess.ToLower().Contains(actualMess.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The valid file was NOT imported." +
                    $"<br>The toast message is: {actualMess}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid file was imported successfully and the toast message indicated success.</b></font>");
        }
        private void AddProductToCategory(string BuildingPhase, string Product)
        {
            CategoryDetailPage.Instance.AddProductToCategory();

            if (!CategoryDetailPage.Instance.AddProductToCategoryModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Product to Category\" modal doesn't display.");
            }

            // Select 2 first items from the list
            CategoryDetailPage.Instance.AddProductToCategoryModal.AddProductToCategory(BuildingPhase, Product);
            CategoryDetailPage.Instance.WaitGridLoad();

            var expectedMessage = "Products were successfully added.";
            var actualMessage = CategoryDetailPage.Instance.GetLastestToastMessage();

            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add Product to Category unsuccessfully. Actual messsage: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add Product to Category successfully.");
            }
            // CategoryDetailPage.Instance.AddProductToCategoryModal.CloseModal();
        }
    }
}
