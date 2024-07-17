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
using Pipeline.Testing.Pages.Estimating.Units;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B01_RT_01081 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        private ProductData product;
        private ProductData getNewproduct;

        private UnitData oldData;

        [SetUp]
        public void GetData()
        {
            oldData = new UnitData()
            {
                Name = "Board Feet",
                Abbreviation = "BF"
            };

            product = new ProductData()
            {
                Name = "RT_Product_QA_Only",
                Manufacture = "GENERIC",
                Style = "GENERIC",
                Code = "QAP1",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "1161-QA Only Phase-0"
            };


            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers, true, true);
            CommonHelper.SwitchLastestTab();

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "GENERIC"
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
                Name = "GENERIC",
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
                Code = "_001",
                Name = "WT_2426_L2"
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
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\RT_01081\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            UnitPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Units);
            ExtentReportsHelper.LogInformation($"Filter new item {oldData.Name} in the grid view.");
            UnitPage.Instance.FilterItemInGrid("Abbreviation", GridFilterOperator.Contains, oldData.Abbreviation);
            if (!UnitPage.Instance.IsItemInGrid("Abbreviation", oldData.Abbreviation))
            {
                UnitPage.Instance.ClickAddToShowUnitModal();
                // Load simple data from excel and add to model
                UnitPage.Instance.AddUnitModal.AddAbbreviation(oldData.Abbreviation).AddName(oldData.Name);

                // 4. Select the 'Save' button on the modal;
                UnitPage.Instance.AddUnitModal.Save();
                UnitPage.Instance.GetLastestToastMessage();
                // Verify successful save and appropriate success message.
            }

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void B01_Estimating_AddaProduct()
        {

            // Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Products/Products/Default.aspx
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", product.Name))
                ProductPage.Instance.DeleteProduct(product.Name, false);

            // Step 2 - 3: click on "+" Add button and Populate all values and save new product
            ProductPage.Instance.ClickAddToProductIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
            if (ProductDetailPage.Instance.IsPageDisplayed(expectedURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Product detail page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Product detail page is displayed</b></font>");
            }

            ExtentReportsHelper.LogInformation("Populate all values and save new product");
            // Select the 'Save' button on the modal;
            getNewproduct = ProductDetailPage.Instance.CreateAProduct(product);

            // Verify new Product in header
            if (ProductDetailPage.Instance.IsCreateSuccessfully(getNewproduct) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Create new Product unsuccessfully.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
            }

            // 4-5-6. Back to list of Product and verify new item in grid view
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);

            // Insert name to filter and click filter by Contain value
            ProductPage.Instance.EnterProductNameToFilter("Product Name", product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", product.Name) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Product {product.Name} was not display on grid..</font>");
            }
        }

        [TearDown]
        public void DeleteProduct()
        {
            // Step 4: Select item and click deletete icon
            ExtentReportsHelper.LogInformation("Delele product");
            ProductPage.Instance.DeleteProduct(product.Name);
        }

        #endregion

    }
}
