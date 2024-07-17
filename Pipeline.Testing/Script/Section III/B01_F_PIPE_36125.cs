using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_III
{
    class B01_F_PIPE_36125 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private readonly string PRODUCT1_DEFAULT = "Product01_Automtion";
        private readonly string PRODUCT2_DEFAULT = "Product02_Automtion";
        private readonly string MANUFACTURE2_DEFAULT = "QA_RT_Manufacturer_Automation";
        private readonly string STYLE2_DEFAULT = "QA_RT_Style_Automation";

        [SetUp]
        public void GetData()
        {

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
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "1222222",
                Name = "QA_RT_BuildingGroup_Automation"
            };
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_36125\\ImportBuildingPhase\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

        }

        [Test]
        [Category("Section_III")]
        public void B01_F_Estimating_Products_Product_Import_Show_More_Error_Messages()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Make sure current transfer seperation character is '*'</font><b>");
            //Make sure current transfer seperation character is ''
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            //I.Verify the error log table show when import the file has invalid data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.Verify the error log table show when import the file has invalid data</font><b>");
            //1.Export and import file again
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.1.Export and import file again (Automation can skip this because it has a performance issue)</font><b>");

            //2.The import file without header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.2.The import file without header</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_36125\\ImportProduct\\Pipeline_Products_I_2.csv";
            List<string> ListMessage = new List<string>() { "Failed to import file due to an error in the data format. Column headers do not match expected values." };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);


            //3.The import file with: Invalid buildingphase, manufacture/style 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.3.The import file with: Invalid buildingphase, manufacture/style </font><b>");
            //Go to the import product and process import
            importFile = "\\DataInputFiles\\Import\\PIPE_36125\\ImportProduct\\Pipeline_Products_I_3.csv";
            ListMessage = new List<string>() { "The Building Phase could not be found", "Style Name is required", "Manufacturer Name is required" };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //II.Verify the message “Import Complete” show, when import the file has valid data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.Verify the message “Import Complete” show, when import the file has valid data</font><b>");
            //II.1. Prepare import file: The import file has new value and old value
            //II.2. Go to the Estimating / Products / Click on “Utilities” button / Click on “Import”
            //II.3. On the product import Page: Process import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.3. On the product import Page: Process import</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_36125\\ImportProduct\\Pipeline_Products_II.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
            //II.4. Check data: Add and edit the product successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.4. Check data: Add and edit the product successfully</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT1_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT1_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Product With Name {PRODUCT1_DEFAULT} is displayed in grid.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogPass(null, $"<font color ='red'>Product With Name {PRODUCT1_DEFAULT} is not displayed in grid.</font>");
            }

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT2_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT2_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Product With Name {PRODUCT2_DEFAULT} is displayed in grid.</b></font>");

                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT2_DEFAULT);
                {
                    // Check Manufacturers and Style In Product
                    ExtentReportsHelper.LogInformation("<b>Check Manufacturers and Style In Product</b>");
                    if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE2_DEFAULT) is true && ProductDetailPage.Instance.IsItemOnManufacturerGrid("Manufacturer", MANUFACTURE2_DEFAULT) is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>The Manufacture with {MANUFACTURE2_DEFAULT} And Style With Name {STYLE2_DEFAULT} is displayed in Product.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color ='red'>The Manufacture with {MANUFACTURE2_DEFAULT} And Style With Name {STYLE2_DEFAULT} is not displayed in Product.</font>");
                    }
                }
            }
            else
            {
                ExtentReportsHelper.LogPass(null, $"<font color ='red'>Product With Name {PRODUCT2_DEFAULT} is not displayed in grid.</font>");
            }


            //III.Verify the error log table hidden when user clicks on the “X” icon or “Close” button
            //III.1. Import the file has wrong data
            //III.2. Click on the “X” icon: Hidden the error log table and keep the import product page
            //III.3. Import the file has wrong data again
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.Verify the error log table hidden when user clicks on the “X” icon or “Close” button</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            ListMessage = new List<string>() { "The Building Phase could not be found" };
            importFile = "\\DataInputFiles\\Import\\PIPE_36125\\ImportProduct\\Pipeline_Products_III.csv";
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);
            //III.4. Click on the “Close” button: Hidden the error log table and keep the import product page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.4. Click on the “Close” button: Hidden the error log table and keep the import product page</font><b>");
            ProductsImportPage.Instance.CloseErrorTable();
        }

        [TearDown]
        public void ClearData()
        {
            //Delete Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Delete Product</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT1_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT1_DEFAULT) is true)
            {
                ProductPage.Instance.DeleteProduct(PRODUCT1_DEFAULT);
            }

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT2_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT2_DEFAULT) is true)
            {
                ProductPage.Instance.DeleteProduct(PRODUCT2_DEFAULT);
            }

        }
    }
}
