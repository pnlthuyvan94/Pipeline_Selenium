using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Calculations;
using Pipeline.Testing.Pages.Estimating.Calculations.CalculationDetail;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_IV
{
    public class B08_RT_01258 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CalculationData calculationData;
        private CalculationData calculationData_DetailPage;
        private CalculationData calculationData_CurrentData;
        private ProductData product;
        private string current_calculation_url;
        readonly string IMPORT_PRODUCT_NAME = "Product_Calculation_Automation_DoNotDelete";
        readonly string SPECSET_GROUP_IMPORT_NAME = "SpecSetGroup_Automation_DoNotDelete";
        readonly string BUILDINGPHASE_NAME = "1162-QA ONLY - Phase 1";
        readonly string SPECSET_IMPORT_NAME = "SpecSet_01";


        [SetUp]
        public void GetData()
        {
            calculationData = new CalculationData()
            {
                Description = "RT-QA Auto Calculation",
                Calculation = "*4.167"
            };

            calculationData_DetailPage = new CalculationData()
            {
                Description = "RT_Calculation_Update_Detail_Page",
                Calculation = "* 5.5"
            };

            product = new ProductData()
            {
                Name = "Product_Calculation_Automation_DoNotDelete",
                Manufacture = "GENERIC",
                Style = "GENERIC",
                Code = "QAP1",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "Product using for automation testing",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "1161-QA Only Phase-0"
            };

            // Step 0: Navigate to Setting page and update 'Transfer Separation Character'
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0: Navigate to Setting page and update 'Transfer Separation Character'.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.SelectTransferSeparationCharacter(",");

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers, true, true);
            CommonHelper.SwitchLastestTab();

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer1_Automation"
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
                Name = "QA_RT_Style1_Automation",
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
                Code = "620",
                Name = "QA Only Building Group",
                Description = "QA Only - Do not modify or delete"
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
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Estimating\\Calculations\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_31232\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);


            // Step 1: Navigate to Estimate menu > Units
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Estimate menu > Calculation.</b></font>");
            CalculationPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Calculations);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Delete data before create a new data.</b></font>");
            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.EqualTo, calculationData_DetailPage.Description);
            if (CalculationPage.Instance.IsItemInGrid("Description", calculationData_DetailPage.Description) is true)
            {
                CalculationPage.Instance.DeleteCalculation(calculationData_DetailPage);
                var actualMessage = CalculationPage.Instance.GetLastestToastMessage();
                var expectedMessage = "Unable to delete this calculation.";
                if (actualMessage == expectedMessage)
                {
                    ExtentReportsHelper.LogInformation($"Can't delete Calculation Name {calculationData_DetailPage.Description}");
                    // Step 4.2: Back to Spec Set page and delete it
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Back to Spec Set page and delete it.</b></font>");
                    SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
                    SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME)
                        .DeleteItemInGrid("Name", SPECSET_GROUP_IMPORT_NAME);

                    if (SpecSetPage.Instance.GetLastestToastMessage().ToLower().Contains("successful"))
                        ExtentReportsHelper.LogPass("<font color = 'green'><b>Spec Set Group deleted successfully!</b></font>");
                    else
                        ExtentReportsHelper.LogPass("<font color = 'red'>Spec Set Group failed to delete!</font>");

                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Delete Calculation Name {calculationData_DetailPage.Description} is successfully.");
                }
            }


            CalculationPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Calculations);
            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.EqualTo, calculationData.Description);

            // Create a new Calculation if not existing.
            if (!CalculationPage.Instance.IsItemInGrid("Description", calculationData.Description))
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.1: Create Calation {calculationData.Description}.</b></font>");
                CalculationPage.Instance.CreateNewCalculation(calculationData);
            }
            else
            {
                CalculationPage.Instance.DeleteCalculation(calculationData);
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.1: Create Calation {calculationData.Description}.</b></font>");
                CalculationPage.Instance.CreateNewCalculation(calculationData);
            }

            calculationData_CurrentData = calculationData;
        }

        [Test]
        [Category("Section_IV")]
        public void B08_A_Estimating_DetailPage_Calculations_CalculationDetails()
        {
            // Step 2: Select item to open Calculation detail page
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2: Select '{calculationData_CurrentData.Description}' item to open Calculation detail page.</b></font>");
            CalculationPage.Instance.SelectItemInGrid("Description", calculationData_CurrentData.Description);

            // Verify open Calculation detail page display correcly
            CalculationDetailPage.Instance.IsDisplayDataCorrectly(calculationData_CurrentData);

            // Step 2.1 and 2.2: Update Calculation detail page with valid data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Update Calculation detail page with valid data.</b></font>");
            CalculationDetailPage.Instance.UpdateCalculation(calculationData_DetailPage);
            current_calculation_url = CalculationPage.Instance.CurrentURL;
            var actualMessage = CalculationDetailPage.Instance.GetLastestToastMessage();
            var expectedMessage = "Calculation edited successfully.";
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
                calculationData_CurrentData = calculationData_DetailPage;
            }
            else if (expectedMessage == actualMessage)
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Update Calculation successfully. The toast message same as expected.</b></font>");

                // Refresh page and verify new updated item.
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Refresh page and verify new updated item.</b></font>");
                CalculationDetailPage.Instance.RefreshPage();
                // System.Threading.Thread.Sleep(3000);
                CalculationDetailPage.Instance.IsDisplayDataCorrectly(calculationData_DetailPage);
                calculationData_CurrentData = calculationData_DetailPage;
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Toast message must be same as expected.</font>" +
                    $"<br>Expected: {expectedMessage}<br>Actual: {actualMessage}</br>");
            }

            string calculation_Full_Name = $"{calculationData_DetailPage.Description} (QTY {calculationData_DetailPage.Calculation})";


            // Step 3.1: Add the Calculation to Product Subcomponents using current calculation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Add the Calculation to Product Subcomponents using current calculation<b>.</b></font>");
            AddSubcomponentCalculations(calculation_Full_Name);


            // Step 3.2: Add the Calculation to Style and Product Conversions on Spec set detail page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Add the Calculation to Style Conversions on Spec set detail page.</b></font>");
            AddConversionCalculation(calculation_Full_Name);

        }


        private void AddSubcomponentCalculations(string usingCalculation)
        {
            // Step 3.1.1: Navigate to Product page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1.1: Navigate to Product page.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            BasePage.PageLoad();

            //ProductPage.Instance.ImportExportProduct("Import");
            /*
                        // Create a new product if it's not existing
                        ProductPage.Instance.EnterProductNameToFilter("Product Name", product.Name);
                        if (ProductPage.Instance.IsItemInGrid("Product Name", product.Name) is false)
                        {
                            // Create a new product
                            ProductPage.Instance.CreateNewProduct(product);
                        } else
                        {
                            // Open Prododuct detail page
                            ProductPage.Instance.SelectItemInGrid("Product Name", product.Name);
                        }

                        // Navigate to Subcomponent page
                        ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                        //Add subcomponent with type is Basic 
                        ExtentReportsHelper.LogInformation(null, "<b> Add subcomponent with type is Basic </b>");
                        // Click add subcomponent
                        ProductSubcomponentPage.Instance.ClickAdd_btn();
                        ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                                        .SelectBuildingPhaseOfProduct(BuildingPhaseOfProduct)
                                                        .SelectCatelogy("Category_2")
                                                        .InputProductSubcomponent("Chinh_Product-Chinh Description for Test")
                                                        .ClickSaveProductSubcomponent();

                */
            // Step 3.1.2: Import product with name 'Product_Calculation_Automation_DoNotDelete'

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1.2: Import product.</b></font>");
            ProductPage.Instance.ImportExportProduct("Import");

            string productImportFile = "\\DataInputFiles\\Estimating\\Calculations\\Pipeline_Products.csv";
            ProductPage.Instance.ImportFile("Products", "Products Import", productImportFile);


            // Step 3.1.3: Import subcomponent
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1.3: Import subcomponent.</b></font>");
            string productSubcomponentImportFile = "\\DataInputFiles\\Estimating\\Calculations\\Pipeline_Subcomponents.csv";
            ProductPage.Instance.ImportFile("Products", "Subcomponents Import", productSubcomponentImportFile);


            // Step 3.1.4: Verify new subcomponent that using the current Calculation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.1.4: Verify new subcomponent " +
                $"that using the current Calculation '{usingCalculation}'.</b></font>");

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            BasePage.PageLoad();

            ProductPage.Instance.EnterProductNameToFilter("Product Name", IMPORT_PRODUCT_NAME)
                       .SelectItemInGrid("Product Name", IMPORT_PRODUCT_NAME);
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            ProductSubcomponentPage.Instance.VerifyItemInGrid("Calculation", usingCalculation);


            // Step 3.1.5: Back to Calculation detail page and verify Subcomponent Calculation grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1.5: Back to Calculation detail page and verify Subcomponent Calculation grid.</b></font>");
            CommonHelper.OpenURL(current_calculation_url);
            if (CalculationDetailPage.Instance.IsIemInProductSubcomponentGrid("Parent", IMPORT_PRODUCT_NAME) is true)
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Calculation display correctly on Subcomponent Calculations grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='red'>Can't display Calculation on Product Subcomponent Calculations grid.</font>");
        }

        private void AddConversionCalculation(string usingCalculation)
        {
            // Step 3.2.1: Navigate to Spec set group page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2.1: Navigate to Spec set group page.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);

            // Change page size to 100
            SpecSetPage.Instance.ChangeSpecSetPageSize(100);

            CommonHelper.ScrollToBeginOfPage();

            // Step 3.2.2: Add Product Conversion through import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2.2: Add Product Conversion through import page.</b></font>");
            // This is the same name as csv import file
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");

            string productImportFile = "\\DataInputFiles\\Estimating\\Calculations\\Pipeline_SpecSetProducts.csv";
            SpecSetPage.Instance.ImportFile("SpecSets", "Spec Sets Product Import", productImportFile);


            // Step 3.2.3: Verify new product conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2.3: Verify new product conversion.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME)
                       .SelectItemInGrid("Name", SPECSET_GROUP_IMPORT_NAME);

            // Click Expand all button and verify product conversion item in grid
            SpecSetDetailPage.Instance.ExpandAllSpecSet();
            SpecSetDetailPage.Instance.IsIemOnProductConversionGrid(SPECSET_IMPORT_NAME, "Calculation", usingCalculation);


            // Step 3.2.4: Add Style Conversion to current spec set by manual
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2.4: Add Style Conversion to current spec set by manual.</b></font>");
            AddStyleConversionCalculation();


            // Step 3.2.5: Verify new Style conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2.5: Verify new style conversion.</b></font>");
            if (SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SPECSET_IMPORT_NAME, "Calculation", usingCalculation) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Calculation with value {usingCalculation} displayed correctly on the Style Conversion grid.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Calculation with value {usingCalculation} on the Style Conversion grid.</font>");

            // Step 3.2.6: Back to Calculation detail page and verify Style and Product Conversion grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2.6: Back to Calculation detail page and verify Style and Product Conversion grid.</b></font>");
            CommonHelper.OpenURL(current_calculation_url);

            if (CalculationDetailPage.Instance.IsIemInStyleConversionGrid("Spec Set Group", SPECSET_GROUP_IMPORT_NAME) is true)
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Calculation display correctly on Style Conversion Calculation grid.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color='red'>Can't display Calculation on Style Conversion Calculation grid.</font>");


            if (CalculationDetailPage.Instance.IsIemInProductConversionGrid("Spec Set Group", SPECSET_GROUP_IMPORT_NAME) is true)
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Calculation display correctly on Product Conversion Calculation grid.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color='red'>Can't display Calculation on Product Conversion Calculation grid.</font>");
        }

        private void AddStyleConversionCalculation()
        {
            SpecSetDetailPage.Instance.ClickAddNewConversationStyle();
            SpecSetDetailPage.Instance.SelectOriginalManufacture("Hai Nguyen Manufacturer");
            SpecSetDetailPage.Instance.SelectOriginalStyle("Vintage");
            SpecSetDetailPage.Instance.SelectOriginalUse("UseA");
            SpecSetDetailPage.Instance.SelectNewManufacture("General");
            SpecSetDetailPage.Instance.SelectNewStyle("General");
            SpecSetDetailPage.Instance.SelectNewUse("UseA");
            SpecSetDetailPage.Instance.SelectStyleCalculation(calculationData_DetailPage.Description + " (QTY " + calculationData_DetailPage.Calculation + ")");
            SpecSetDetailPage.Instance.PerformInsertStyle();
        }


        [TearDown]
        public void DeleteData()
        {
            // Step 4.1: Back to product page and delete it by Style
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Step 4.1: Back to product page and delete it by Style.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, IMPORT_PRODUCT_NAME);
            ProductPage.Instance.SelectItemInGrid("Product Name", IMPORT_PRODUCT_NAME);
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            ProductSubcomponentPage.Instance.FilterSubcomponentInGird(BUILDINGPHASE_NAME);
            ProductSubcomponentPage.Instance.ClickDeleteInGird(BUILDINGPHASE_NAME);
            string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            if (act_mess == "Successfully deleted subcomponent")
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {IMPORT_PRODUCT_NAME} subcomponent </b></font color>");
            }
            else
                ExtentReportsHelper.LogFail($"<b> Cannot delete {IMPORT_PRODUCT_NAME} subcomponent </b>");
            ProductSubcomponentPage.Instance.CloseToastMessage();
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, IMPORT_PRODUCT_NAME);
            ProductPage.Instance.DeleteItemInGrid("Product Name", IMPORT_PRODUCT_NAME)
                .DeleteByAll();

            if (ProductPage.Instance.GetLastestToastMessage().ToLower().Contains("successful"))
                ExtentReportsHelper.LogPass("<font color = 'green'><b>Product deleted successfully!</b></font>");
            else
                ExtentReportsHelper.LogPass("<font color = 'red'>Product failed to delete!</font>");

            System.Threading.Thread.Sleep(1000);

            // Step 4.2: Back to Spec Set page and delete it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Back to Spec Set page and delete it.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.ChangeSpecSetPageSize(20);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SPECSET_GROUP_IMPORT_NAME))
            {
                SpecSetPage.Instance.DeleteSpecSet(SPECSET_GROUP_IMPORT_NAME);
                System.Threading.Thread.Sleep(1000);
            }

            // Step 4.3: Back to Calculation page and delete it.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3: Back to Calculation page and delete it.</b></font>");
            CalculationPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Calculations);

            CalculationPage.Instance.FilterItemInGrid("Description", GridFilterOperator.EqualTo, calculationData_CurrentData.Description);
            CalculationPage.Instance.DeleteCalculation(calculationData_CurrentData);
        }

    }
}
