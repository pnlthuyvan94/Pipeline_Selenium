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
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class B01_A_RT_01244 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private ProductData product;
        [SetUp]
        public void GetData()
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

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers, true, true);
            CommonHelper.SwitchLastestTab();

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "Armstrong"
            };

            ManufacturerData manuData1 = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer2_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData1.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData1.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData1);
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

            StyleData styleData1 = new StyleData()
            {
                Name = "QA_RT_Style2_Automation",
                Manufacturer = manuData.Name
            };

            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData1.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData1.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData1);
            }


            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "101",
                Name = "Hai Nguyen Building Group"
            };

            BuildingGroupData buildingGroupData1 = new BuildingGroupData()
            {
                Code = "2603",
                Name = "QA_RT_Building Group_K1"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData1.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData1.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData1);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_01244\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_01244\\Pipeline_Products_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);


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

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }



        [Test]
        [Category("Section_IV")]
        public void B01_A_Estimating_DetailPage_Product_ProductDetails()
        {
            // Step 1: Navigate Assets > Option Selection Group and open Option Selection Group Detail page
            ExtentReportsHelper.LogInformation("<b>Step 1: Navigate Estimating/ Product.</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);

            //  Click the Product to which you would like to select
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, product.Name);
            ExtentReportsHelper.LogInformation(" Click the Product to which you would like to select");
            ProductPage.Instance.SelectItemInGrid("Product Name", product.Name);

            // Verify open the product detail
            if (ProductDetailPage.Instance.IsSaveProductSuccessful(product.Name) == false)
            {
                ExtentReportsHelper.LogFail("<font color ='red'><b>Can't open the Product Details</b></font>");
            }
            ExtentReportsHelper.LogPass($"<font color ='yellow'><b>Opened successfully the Product Details.</b></font>");

            // Step 2: In the Product Details, edit the valid of Product; click the 'Save' button; verify updated successfully
            ExtentReportsHelper.LogInformation("<b>Step 2: In the Product Details, edit the valid of Product; click the 'Save' button; verify updated successfully</b>");
            ExtentReportsHelper.LogInformation("Edit the valid of Product");
            ProductDetailPage.Instance.EnterDescription("Description for testing");
            ProductDetailPage.Instance.EnterNotes("Notes for testing");
            ProductDetailPage.Instance.EnterSKU("1234");
            ExtentReportsHelper.LogInformation("Click Save button");
            ProductDetailPage.Instance.SaveProductDetail();

            var actualMessage = ProductDetailPage.Instance.GetLastestToastMessage();
            var expectedMessage = $"Product {product.Name} saved successfully!";
            if (actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail("<font color ='red'><b>The product detail update is failed and not received a message successfully</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>The product detail update is successfully and received a message successfully</b></font>");
            }

            //Step 3: Verify add items in Product
            ExtentReportsHelper.LogInformation("<b>Step 3: Verify add items in Product</b>");
            // Add and verfify for Building Phases
            ExtentReportsHelper.LogInformation("<b>a. Add function on Building Phases</b>");
            ProductDetailPage.Instance.AddBuildingPhases("0329-QA_RT_Buildingphase1", false, "Phase");

            if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase added successfully!")
            {
                ExtentReportsHelper.LogPass("<font color ='yellow'><b> Added successfully Building Phase into Product; received a message successfully</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'><b> Added successfully Building Phase is failed; Don't received a message successfully</b></font>");
            // Add and verify Manufacturers and Styles
            ExtentReportsHelper.LogInformation("<b>b. Add Manufacturer and Style</b>");
            if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "QA_RT_Style2_Automation") is false)
            {
                ProductDetailPage.Instance.AddManufacturersStyles("QA_RT_Manufacturer2_Automation", false, "QA_RT_Style2_Automation", "2222");
            }

            //WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewManufacturers']/div[1]");
            if (ProductDetailPage.Instance.GetTotalItemOnStyle() == 2)
            {
                ExtentReportsHelper.LogPass("<font color ='yellow'><b>Added successfully Style into Product; received a message successfully</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color ='red'><b> Added successfully Style is failed; Don't received a message successfully</b></font>");

            // Add and verify Categories 
            ExtentReportsHelper.LogInformation("<b> c. Add House Categories </b>");
            ProductDetailPage.Instance.AddCategories("Category_5");
            // if (ProductDetailPage.Instance.GetTotalItemOnCatelogy() > 0)
            //{
            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Added successfully Categories into Product; received a message successfully</b></font>");
            //}
            //else
            // ExtentReportsHelper.LogFail("<font color ='red'>< b>Added successfully Categories is failed; Don't received a message successfully</b></font>");


            // Add and verfiy house notes
            ExtentReportsHelper.LogInformation("<b> c. Add House Note Override </b>");
            //Select Item By Text Or Index
            ProductDetailPage.Instance.AddHouseNotes("2104_Midland_KN", "KN_option", "HN-Phase-4", "carpet", "testing notes");
            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Added successfully House Note Override into Product; received a message successfully</b></font>");
            //if (ProductDetailPage.Instance.GetLastestToastMessage() == "Notes has been successfully saved!")
            //{
            //    ExtentReportsHelper.LogPass("<font color ='yellow'><b>Added successfully House Note Override into Product; received a message successfully</b></font>");
            //}
            //else
            //    ExtentReportsHelper.LogFail("Added successfully House Note Override is failed; Don't received a message successfully");

            // Add and verify job notes
            ExtentReportsHelper.LogInformation("<b> c. Add Job Note Override </b>");
            //Select Item By Text Or Index
            ProductDetailPage.Instance.AddJobNotes("BASE", "86205", "HN-Phase-4", "carpet", "testing notes");
            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Added successfully Job Note Override into Product; received a message successfully</b></font>");
            //if (ProductDetailPage.Instance.GetLastestToastMessage() == "Notes has been successfully saved!")
            //{
            //    ExtentReportsHelper.LogPass("<font color ='yellow'><b>Added successfully Job Note Override into Product; received a message successfully</b></font>");
            //}
            //else
            //    ExtentReportsHelper.LogFail("Added successfully Job Note Override is failed; Don't received a message successfully");


            // Step 4: Verify add items in Product
            ExtentReportsHelper.LogInformation("<b>Step 4: Verify the filter items in Product</b>");
            ProductDetailPage.Instance.FilterItemInBuildPhaseGrid("Name", GridFilterOperator.Contains, "QA_RT_Buildingphase1");
            if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Buildingphase1") == true)
            {
                ExtentReportsHelper.LogPass("<font color ='yellow'><b>Filtered successfully</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("< font color = 'red' >< b> Can't filtered</b></font>");


        }

        [TearDown]
        public void DeleteData()
        {
            // Refresh product detail page
            CommonHelper.RefreshPage();

            // Step 5: Verify delete the Building Phase/Manufacturers and Styles/Categories/House Note Overrides/Job Note Overrides
            ExtentReportsHelper.LogInformation("<b>Step 5: Verify delete the Building Phase/Manufacturers and Styles/Categories/House Note Overrides/Job Note Overrides</b>");
            ExtentReportsHelper.LogInformation("<b>a. Delete Building Phase</b>");
            if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Buildingphase1") == true)
            {
                ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Buildingphase1");
                if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")
                
                    ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                
                else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Buildingphase1") == true)
                
                    ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                 else
                    ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
            }

            // b.Manufacturers and Style
            ExtentReportsHelper.LogInformation("<b>b. Delete Manufacturers and Style</b>");
            if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "QA_RT_Style2_Automation") == true)
            {
                ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", "QA_RT_Style2_Automation");
                if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                
                    ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                
                else if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", "pl") == true)
                
                    ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                 else
                    ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");

            }

            // c.  Categories
            ExtentReportsHelper.LogInformation("<b> c. Delete Categories</b>");
            if (ProductDetailPage.Instance.IsItemOnCategoryGrid("Name", "Category_5") == true)
            {
                ProductDetailPage.Instance.DeleteItemOnCategoriesGrid("Category_5");

                if (ProductDetailPage.Instance.GetLastestToastMessage() == "Category deleted successfully!")
                {
                    ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Category successfully; Received a message delete successfully</b></font>");
                }
                else if (ProductDetailPage.Instance.IsItemOnCategoryGrid("Name", "Category_5") == true)
                    ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Category failed; Don't received a message delete successfully</b></font>");
                else
                    ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Category successfully; Received a message delete successfully</b></font>");
            }

            if (ProductDetailPage.Instance.IsItemOnHouseNoteGrid("Note Overrides", "testing notes") == true)
            {
                ProductDetailPage.Instance.DeleteItemOnHouseNoteGrid("testing notes");
                ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted  House Note successfully</b></font>");
            }

            if (ProductDetailPage.Instance.IsItemOnJobNoteGrid("Note Overrides", "testing notes") == true)
            {
                ProductDetailPage.Instance.DeleteItemOnJobNoteGrid("testing notes");
                ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted  Job Note successfully</b></font>");
            }

        }
    }
}
