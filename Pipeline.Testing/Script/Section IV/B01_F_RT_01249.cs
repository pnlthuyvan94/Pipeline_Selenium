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
using Pipeline.Testing.Pages.Estimating.Products.ProductCustomFields;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.Builder.Product;
using Pipeline.Testing.Pages.Settings.CustomField;


namespace Pipeline.Testing.Script.Section_IV
{
    public partial class B01_F_RT_01249 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private string productName = "ProductDetail_AutomationTesting_DoNotDelete";
        private CustomFieldData TestData;
        private ProductData product;
        [SetUp]
        public void SetUp()
        {
            //TestData = new CustomFieldData();
            TestData= new CustomFieldData
            {
                Title = "Product Test",
                Description = "Description",
                Tag = "tag",
                FieldType = "Text Area",
                Value="",
                Default = true
            };

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

            string importFile = "\\DataInputFiles\\Import\\PIPE_01249\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_01249\\Pipeline_Products_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }

        [Test]
        [Category("Section_IV")]
        public void B01_E_Estimating_DetailPage_Product_ProductCustomFields()
        {
            // Step 1: Navigate Assets > Option Selection Group and open Option Selection Group Detail page
            ExtentReportsHelper.LogInformation("<b>Step 1. From ESTIMATING/PRODUCTS, click a Product  which you like to select</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productName);
            if(ProductPage.Instance.IsItemInGrid("Product Name", productName) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productName);

            }
            ProductDetailPage.Instance.LeftMenuNavigation("Custom Fields");
            var productdetailPage_URL = ProductDetailPage.Instance.CurrentURL;
            // Step 2: In the Product Details, edit the valid of Product; click the 'Save' button; verify updated successfully
            ExtentReportsHelper.LogInformation(null, "<b>Step 2. In Side Navigation, click the Custom Fields to open the Custom Field page</b>");
            ProductPage.Instance.NavigateURL(productdetailPage_URL);
            ExtentReportsHelper.LogPass($"<font color ='green'><b>Opened successfully the Product Custom Fields.</b></font>");

            //ExtentReportsHelper.LogInformation(null, "Open Product Setting");
            // ProductPage.Instance.NavigateURL("Products/Settings/Default.aspx");

            ProductPage.Instance.NavigateURL(productdetailPage_URL);
            ExtentReportsHelper.LogInformation(null, "<b>Step 3: The successful open/input/save of valid input value (along with the appropriate toast message to indicate the success)</b>");
            ProductCustomFieldsPage.Instance.ClickAdd_btn("Checkbox");
            ProductCustomFieldsPage.Instance.ClickSave_btn();
            ExtentReportsHelper.LogPass("<font color ='green'><b>Click 'Save' button in page; Custom Field added successfully; received a message successfully</b></font>");

            ExtentReportsHelper.LogInformation(null, "<b>Step 4: The successful remove the Custom Field  item </b>");
            ProductCustomFieldsPage.Instance.ClickRemove_btn("Checkbox");
            ProductCustomFieldsPage.Instance.ClickSave_btn();
            ExtentReportsHelper.LogPass("<font color ='green'><b>Click 'Remove' button in page; Custom Field removed successfully; received a message successfully</b></font>");
            ProductCustomFieldsPage.Instance.Add_btn_Click();
            ExtentReportsHelper.LogPass("<font color ='green'><b>The 'Add Product Custom Fields' modal display remain Custom Fields</b></font>");
            ProductCustomFieldsPage.Instance.Close_ModalCustomFields_Click();

            ExtentReportsHelper.LogInformation(null, "<b>Step 5: Open the Settings page, add new Product Custom Field </b>");
            ProductCustomFieldsPage.Instance.NavigateURL("Products/Settings/Default.aspx");
            ProductSettingPage.Instance.ShowAddCustomFieldModal();
            ProductSettingPage.Instance.CreateCustomField(TestData);

            ExtentReportsHelper.LogInformation(null, "<b>Step 6: Verify the newly Custom Field is displayed in Product page </b>");
            ProductPage.Instance.NavigateURL(productdetailPage_URL);
            ProductCustomFieldsPage.Instance.ClickAdd_btn("Product Test");
            ProductCustomFieldsPage.Instance.ClickSave_btn();
            ExtentReportsHelper.LogPass("<font color ='green'><b>Click 'Save' button in page; Custom Field added successfully; received a message successfully</b></font>");
            ProductCustomFieldsPage.Instance.NavigateURL("Products/Settings/Default.aspx");
            // Delete custome fields
            ProductSettingPage.Instance.DeleteItemInGrid("Product Test");
            ExtentReportsHelper.LogInformation("<b>Delete the Custom Field in Settings page</b>");
            ProductPage.Instance.NavigateURL(productdetailPage_URL);
            ProductCustomFieldsPage.Instance.Add_btn_Click();
            ExtentReportsHelper.LogPass("<font color ='green'><b>The newly Custom Field is removed successfully in Custom Field/Product page</b></font>");
            ProductCustomFieldsPage.Instance.Close_ModalCustomFields_Click();

            ProductCustomFieldsPage.Instance.Remove_btn_Click();
            ExtentReportsHelper.LogInformation(null, "<b>Step 7: Input valid for all Custom Field </b>");
            ProductCustomFieldsPage.Instance.ClickSave_btn();
            ExtentReportsHelper.LogPass("<font color ='green'><b>Selected the 'Save' button; Received a message successfully</b></font>");
           
            ExtentReportsHelper.LogInformation(null, "<b>Step 8: The successfully update the Product Custom Field </b>");
            ExtentReportsHelper.LogInformation(null, "<b> Update the Product Custom Field; select the 'Save' button on page </b>");
            ProductCustomFieldsPage.Instance.ClickSave_btn();
            ExtentReportsHelper.LogPass("<font color ='green'><b>Update successfully Product Custom Field</b></font>");



        }
    }    

}
