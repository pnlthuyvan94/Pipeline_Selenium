using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductAssignment;
using Pipeline.Testing.Pages.Import;
using Pipeline.Common.Constants;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Manufactures;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class B01_E_RT_01248 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private string productName = "ProductDetail_AutomationTesting_DoNotDelete";
        ProductAssignmentData _convertFromproduct;
        ProductAssignmentData _convertToproduct;
        private ProductData product;
        [SetUp]
        public void CreateTestData()
        {
            product = new ProductData()
            {
                Name = "ProductDetail_AutomationTesting_DoNotDelete",
                Manufacture = "Armstrong",
                Style = "carpet",
                Code = "5555",
                Description = "Description for testing",
                Notes = "Notes for testing",
                Unit = "NONE",
                SKU = "1234",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
                BuildingPhase = "004-HN-Phase-4"
            };

            _convertFromproduct = new ProductAssignmentData()
            {
                ConvertFromStyle = "ALL",
                ConvertFromSpecSetGroup= "CH_SpecSet",
                ConvertFromSpetSet= "Spec Set 1",
                ConvertFromUse="UseA",
                ConverFromBuildingGroup= "-001-Training Group",
                ConvertFromBuildingPhase= "-888-JRoll",
                ConvertFromProduct= "FND_MS_2x6 - 2x6 Mud Sill - 1 ply",
                ConvertFromNewStyle= "GENERIC",
                ConvertFromNewUse= "UseA8",
                ConvertFromCalculation= "NONE"

            };
            _convertToproduct = new ProductAssignmentData()
            {
                ConvertToNewStyle = "carpet",
                ConvertToSpecSetGroup= "Hai Nguyen Spec 2",
                ConvertToSpecSet="1",
                ConvertToNewUse="UseA7",
                ConvertToBuildingGroup= "_001-WT_2426_L2",
                ConvertToBuildingPhase= "1161-QA Only Phase-0",
                ConvertToProduct= "FND_MS_2x6 - 2x6 Mud Sill - 1 ply",
                ConverToStyle="ALL",
                ConvertToUse= "UseD",
                ConvertToCalculation= "NONE"
            };


            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers, true, true);
            CommonHelper.SwitchLastestTab();

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "Armstrong"
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
                Name = "carpet",
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
                Code = "101",
                Name = "Hai Nguyen Building Group"
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_01248\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_01248\\Pipeline_Products_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }

        [Test]
        [Category("Section_IV")]
        public void B01_E_Estimating_DetailPage_Product_ProductAssignment()
        {
            // Step 1: Navigate Assets > Option Selection Group and open Option Selection Group Detail page
            ExtentReportsHelper.LogInformation("<b>Step 1: Navigate Estimating/ Product.</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);

            //  Click the Product to which you would like to select
            ExtentReportsHelper.LogInformation(null, "Click the Product to which you would like to select");
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productName);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productName) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productName);
            }
            else
            {
                ProductPage.Instance.CreateNewProduct(product);
            }

            // Verify open the product detail
            if (ProductDetailPage.Instance.IsSaveProductSuccessful(productName) == false)
            {
                ExtentReportsHelper.LogFail("<font color ='red'><b>Can't open the Product Details</b></font>");
            }
            ExtentReportsHelper.LogPass($"<font color ='green'><b>Opened successfully the Product Details.</b></font>");

            // Step 2: In the Product Details, edit the valid of Product; click the 'Save' button; verify updated successfully
            ExtentReportsHelper.LogInformation(null, "<b>Step 2: click the Document on the left navigation.</b>");
            ProductDetailPage.Instance.LeftMenuNavigation("Product Assignment");
            // Verify opened successfully Product Assignment page
            ProductAssignmentPage.Instance.IsProductAssignmentPage();

            //Step 3: Verify opened successfully the Spec Sets Assignment page
            ExtentReportsHelper.LogInformation("<b>Step 3: Verify opened successfully the Spec Sets Assignment page</b>");
            // Verify spec set product assignment page is display 
            ProductAssignmentPage.Instance.VerifyViewProductAssignmentPage("Spec Sets Assignment");
            //Step 4. Click the '+' button to add Convert From/ Convert To Product
            ExtentReportsHelper.LogInformation(null,"<b>Step 4: Click the '+' button to add Convert From/ Convert To Product</b>");
            ExtentReportsHelper.LogInformation(null,"<b>a. Convert from </b>");
            ProductAssignmentPage.Instance.VerifyAddSpecSetConvertForm(_convertFromproduct);
            // Saved successfully; received a message successfully
            if (ProductAssignmentPage.Instance.GetLastestToastMessage() == "Spec Set Item added successfully.")
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Saved successfully; received a message successfully</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'>< b>Saved spec set item failed");
            // Edit spec set item
            ExtentReportsHelper.LogInformation("<b>Edit item in Convert From grid </b>");
                ProductAssignmentPage.Instance.EditConverFrom("Spec Set Group", _convertFromproduct.ConvertFromSpecSetGroup);
            // Saved successfully; received a message successfully
            if (ProductAssignmentPage.Instance.GetLastestToastMessage() == "Item updated!")
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Edited successfully the Convert From; Received a message successfully</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'>< b>Edit spec set item failed");
            ProductAssignmentPage.Instance.CloseToastMessage();

            // Deleted successfully Convert From
            ExtentReportsHelper.LogInformation("<b>Delete item in Convert From grid </b>");
            ProductAssignmentPage.Instance.DeleteConverFrom("Spec Set Group", _convertFromproduct.ConvertFromSpecSetGroup);
            // Saved successfully; received a message successfully
            if (ProductAssignmentPage.Instance.GetLastestToastMessage() == "Item deleted!")
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Deleted successfully Convert From</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'>< b>Delete spec set item failed");
            ProductAssignmentPage.Instance.CloseToastMessage();

            // Convert To
            ExtentReportsHelper.LogInformation(null,"<b>b. Convert To </b>");
            ProductAssignmentPage.Instance.VerifyAddSpecSetConvertTo(_convertToproduct);
            // Saved successfully; received a message successfully
            if (ProductAssignmentPage.Instance.GetLastestToastMessage() == "Spec Set Item added successfully.")
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Saved successfully; received a message successfully</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'>< b>Saved spec set item failed");
            // Edit spec set item
            ExtentReportsHelper.LogInformation("<b>Edit item in Convert To grid </b>");
            ProductAssignmentPage.Instance.EditConverTo("Spec Set Group", _convertToproduct.ConvertToSpecSetGroup);
            // Saved successfully; received a message successfully
            if (ProductAssignmentPage.Instance.GetLastestToastMessage() == "Item updated!")
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Edited successfully the Convert To; Received a message successfully</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'>< b>Edit spec set item failed");
            ProductAssignmentPage.Instance.CloseToastMessage();

            // Deleted successfully Convert From
            ExtentReportsHelper.LogInformation("<b>Delete item in Convert To grid </b>");
            ProductAssignmentPage.Instance.DeleteConverTo("Spec Set Group", _convertToproduct.ConvertToSpecSetGroup);
            // Saved successfully; received a message successfully
            if (ProductAssignmentPage.Instance.GetLastestToastMessage() == "Item deleted!")
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Deleted successfully Convert To</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'>< b>Delete spec set item failed");
            ProductAssignmentPage.Instance.CloseToastMessage();

            // Step 5: From the View dropdown list, select the 'Estimating Assignment'; verify the data page
            ExtentReportsHelper.LogInformation(null, "<b>Step 5: From the View dropdown list, select the 'Estimating Assignment'; verify the data page.</b>");
            // Verify opened successfully Estimating Assignment page
            ProductAssignmentPage.Instance.VerifyViewProductAssignmentPage("Estimating Assignment");
           
            // Step 6: From the View dropdown list, select the 'BOM Assignment'; verify the data page
            ExtentReportsHelper.LogInformation(null, "<b>Step 6: From the View dropdown list, select the 'BOM Assignment'; verify the data page.</b>");
            // Verify opened successfully BOM Assignment page
            ProductAssignmentPage.Instance.VerifyViewProductAssignmentPage("BOM Assignment");
           
        }
    }    

}
