using LinqToExcel.Extensions;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductResources;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Pipeline.Common.Constants;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Estimating.Manufactures;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class B01_B_RT_01245 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        readonly string sourcePathImage = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\DataInputFiles\Resources\";
        private IList<string> dirArrayImage = new List<string>();
        private readonly IList<string> listNameImage = new List<string>();
        private ProductData oldData;
        private ProductResourceData resourceData;


        [SetUp]
        public void GetData()
        {
            // Get list images
            dirArrayImage = Directory.GetFiles(sourcePathImage);
            foreach (var nameDoc in dirArrayImage)
            {
                string nameItem = Path.GetFileName(nameDoc);
                listNameImage.Add(nameItem);
            }

            // Verify that old product is existing or not. If not => Create new one

            oldData = new ProductData()
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
                BuildingPhase = "1161-QA Only Phase-0",
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_01245\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);


            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ExtentReportsHelper.LogInformation($"Filter old Product /'{oldData.Name}/' in the grid view.");
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, oldData.Name);

            if (!ProductPage.Instance.IsItemInGrid("Product Name", oldData.Name))
            {
                // Create new Product
                ProductPage.Instance.ClickAddToProductIcon();
                ProductDetailPage.Instance.CreateAProduct(oldData);
            }

            resourceData = new ProductResourceData()
            {
                Title = "Test Image",
                UpdatedTitle = "Test Image_Update",
                PrimaryResource = true.ToString().ToLower().Equals("true") ? true : false
            };
        }

        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void B01_B_Estimating_DetailPage_Products_Resource()
        {
            // Step 1.1: Navigate to product Url
            ExtentReportsHelper.LogInformation($" Step 1.1: Navigate to this Product page.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);

            var _productUrl = BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL;
            if (ProductDetailPage.Instance.IsPageDisplayed(_productUrl) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Product detail page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Product detail page is displayed</b></font>");
            }

            // Step 1.2: Filter and Select item to open Option Room detail page
            ExtentReportsHelper.LogInformation($" Step 1.2: Filter Select {oldData.Name} item to open Product detail page.");
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, oldData.Name);
            ProductPage.Instance.SelectItemInGrid("Product Name", oldData.Name);

            // Verify open Product detail page display correcly
            if (ProductDetailPage.Instance.IsCreateSuccessfully(oldData) is true)
                ExtentReportsHelper.LogPass($"The Product detail page of item: {oldData.Name} displays correctly.");

            // Step 2: Open Resource navigation
            ExtentReportsHelper.LogInformation(" Step 2: Open Resource navigation.");
            ProductDetailPage.Instance.LeftMenuNavigation("Resources");

            // Step 3: Click Add button and upload resource
            ExtentReportsHelper.LogInformation(" Step 3: Click Add button and updload resource");
            ProductResourcePage.Instance.InsertResources(resourceData, dirArrayImage.ToArray());

            // Step 4: Edit to update Resource information
            ExtentReportsHelper.LogInformation(" Step 4:  Edit to update Resource information");
            UpdateResourceTitle();

            // Step 5: Click resource and verify hyperlink
            ExtentReportsHelper.LogInformation(" Step 5:  Click resource and verify hyperlink");
            ProductResourcePage.Instance.VerifyHyperLink(listNameImage.ToArray());

            // Step 6: Delete resource item
            ExtentReportsHelper.LogInformation(" Step 6:  Delete resource item");
            ProductResourcePage.Instance.DeleteResource(listNameImage.ToArray());
        }

        private void UpdateResourceTitle()
        {
            var updatedresourceName = listNameImage[0];
            if (ProductResourcePage.Instance.UpdateResources(resourceData, updatedresourceName))
            {
                var actualMessage = ProductResourcePage.Instance.GetLastestToastMessage();
                var expectedMessage = "Resource saved successfully!";
                if (actualMessage == string.Empty)
                {
                    ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
                }
                else if (expectedMessage == actualMessage)
                {
                    ExtentReportsHelper.LogPass($"Update resource {updatedresourceName} with new title {resourceData.UpdatedTitle} successfully. The toast message same as expected.");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
                }
            }
            else
            {
                ExtentReportsHelper.LogFail($"Update resource {updatedresourceName} with new title {resourceData.UpdatedTitle} unsuccessfully.");
            }
        }

        [TearDown]
        public void DeleteProduct()
        {
            // Step 7: Back to Product page and delete current product
            ExtentReportsHelper.LogInformation($"Navigate to this Product page to delete current product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);

            // Filter and delete product
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, oldData.Name);
            ExtentReportsHelper.LogInformation("Delele product.");
            ProductPage.Instance.DeleteProduct(oldData.Name);
        }

        #endregion
    }
}
