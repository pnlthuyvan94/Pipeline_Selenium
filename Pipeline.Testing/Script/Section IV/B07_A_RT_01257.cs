using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Category;
using Pipeline.Testing.Pages.Estimating.Category.CategoryDetail;
using Pipeline.Testing.Pages.Estimating.Category.CategoryDetail.AddProductToCategory;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class B07_A_RT_01257 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CategoryData oldData;
        private CategoryData newTestData;
        private List<string> Productlist;
        private string productName = "QA_Product_01_Automation";
        private string buildingPhaseName = "QA_1-QA_BuildingPhase_01_Automation";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            oldData = new CategoryData()
            {
                Name = "QA_USE-RT",
                Parent = "NONE"
            };

            newTestData = new CategoryData()
            {
                Name = "QA_USE-RT_Update",
                Parent = "NONE"
            };

            
            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_01257\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(BUILDING_GROUP_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_01257\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(PRODUCT_IMPORT, importFile);
            
            // Step 1: Navigate Estimating > Category and open Category Detail page
            ExtentReportsHelper.LogInformation("Navigate Estimating > Category and open Category Detail page.");
            CategoryPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Categories);


            // Delete the category that has the same updated name to create a new one later
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newTestData.Name);
            if (CategoryPage.Instance.IsItemInGrid("Name", newTestData.Name) is true)
            {
                // Open detail page, if there are any product that assigned to category, then delete it.
                CategoryPage.Instance.SelectItemInGrid("Name", newTestData.Name);

                CategoryDetailPage.Instance.RemoveAllProductFromCategory(Productlist);

                // Back to default page and delete current category
                CategoryPage.Instance.BackToPreviousPage();
                CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newTestData.Name);
                CategoryPage.Instance.DeleteCategory(newTestData.Name);
            }


            // Insert name to filter and click filter by Contain value
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);

            bool isFoundOldItem = CategoryPage.Instance.IsItemInGrid("Name", oldData.Name);
            if (!isFoundOldItem)
            {
                CategoryPage.Instance.CreateNewCategory(oldData.Name, oldData.Parent);
            }
        }

        [Test]
        [Category("Section_IV")]
        public void B07_A_Estimating_DetailPage_Categories_CategoryDetails()
        {
            // Step 1: Select filter item in the grid and open detail page
            ExtentReportsHelper.LogInformation("Step 1: Open Category detail page.");
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);
            CategoryPage.Instance.SelectItemInGrid("Name", oldData.Name);

            // Verify open Category detail page displayed correcly
            if (CategoryDetailPage.Instance.IsDisplayDataCorrectly(oldData) is true)
                ExtentReportsHelper.LogPass($"The Category {oldData.Name} displays sucessfully on URL: {CategoryDetailPage.Instance.CurrentURL}");

            // Step 2: Update Category with new value
            ExtentReportsHelper.LogInformation("Step 2: Update Category with new value.");
            UpdateCategory(newTestData);

            // Step 3: Add Product to Category
            ExtentReportsHelper.LogInformation("Step 3: Add Product to Category.");
            bool isupdateSuccessful = AddProductToCategory();

            if (isupdateSuccessful)
            {
                // Step 4: Filter new products on the grid view
                ExtentReportsHelper.LogInformation("Step 4: Filter new Products in the grid view.");
                VerifyNewProductOnGrid();

                // Step 5.1: Delete single product
                ExtentReportsHelper.LogInformation($"Step 5.1: Delete Product {productName} out of Category.");
                RemoveProductFromCategory(productName);

                // Step 5.2: Delete all product
                ExtentReportsHelper.LogInformation("Step 5.2: Delete all Products out of Category.");
                CategoryDetailPage.Instance.RemoveAllProductFromCategory(Productlist);
            }
        }

        private bool AddProductToCategory()
        {
            bool isUpdateSuccessful = true;
            CategoryDetailPage.Instance.AddProductToCategory();

            if (!CategoryDetailPage.Instance.AddProductToCategoryModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Product to Category\" modal doesn't display.");
                isUpdateSuccessful = false;
            }

            // Select 2 first items from the list
            CategoryDetailPage.Instance.AddProductToCategoryModal.AddProductToCategory(buildingPhaseName, productName);
            CategoryDetailPage.Instance.WaitGridLoad();

            var expectedMessage = "Products were successfully added.";
            var actualMessage = CategoryDetailPage.Instance.GetLastestToastMessage();

            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add Product to Category unsuccessfully. Actual messsage: {actualMessage}");
                isUpdateSuccessful = false;
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add Product to Category successfully.");
            }
            // CategoryDetailPage.Instance.AddProductToCategoryModal.CloseModal();
            return isUpdateSuccessful;
        }

        private void VerifyNewProductOnGrid()
        {
            // Verify new items which is added to the grid view

                // Insert name to filter and click filter by Contain value
                CategoryDetailPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productName);

                bool isFoundItem = CategoryDetailPage.Instance.IsItemInGrid("Product Name", productName);
                if (!isFoundItem)
                {
                    ExtentReportsHelper.LogFail($"The Product \"{productName} \" was not display on the grid view.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"The Product \"{productName}\" was found on the grid view.");
                }

        }

        private void UpdateCategory(CategoryData newTestData)
        {
            CategoryDetailPage.Instance.UpdateCategory(newTestData);

            var expectedMessage = "Product Category updated successfully.";
            var actualMessage = CategoryDetailPage.Instance.GetLastestToastMessage();
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
            }
            else if (expectedMessage == actualMessage)
            {
                ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");
                if (CategoryDetailPage.Instance.IsDisplayDataCorrectly(newTestData))
                    ExtentReportsHelper.LogPass("The updated data displays correctly in the grid view.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
            }
            CategoryDetailPage.Instance.CloseToastMessage();
        }


        private void RemoveProductFromCategory(string productName)
        {
            CategoryDetailPage.Instance.DeleteItemInGrid("Product Name", productName);
            CategoryDetailPage.Instance.WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgProducts']");

            var expectedMessage = "Product successfully removed.";
            var actualMessage = CategoryDetailPage.Instance.GetLastestToastMessage();

            if (actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Product {productName} deleted successfully!");
                CategoryPage.Instance.CloseToastMessage();
            }
            else
            {
                if (CategoryPage.Instance.IsItemInGrid("Product Name", productName))
                    ExtentReportsHelper.LogFail($"Product {productName} can't be deleted from current Category!");
                else
                    ExtentReportsHelper.LogPass($"Product {productName} deleted successfully from current Category!");

            }
        }


        [TearDown]
        public void DeleteCategory()
        {
            ExtentReportsHelper.LogInformation("Step 5.2: Delete all Products out of Category.");
            CategoryDetailPage.Instance.RemoveAllProductFromCategory(Productlist);

            // Back to Building Group page and delete item
            ExtentReportsHelper.LogInformation($"Step 6: Back to Category page. Filter Category {newTestData.Name} in the grid then delete it.");
            CategoryPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Categories);

            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newTestData.Name);
            if (CategoryPage.Instance.IsItemInGrid("Name", newTestData.Name) is true)
                CategoryPage.Instance.DeleteCategory(newTestData.Name);
        }
    }
}
