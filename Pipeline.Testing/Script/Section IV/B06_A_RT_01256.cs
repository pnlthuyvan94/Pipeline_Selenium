using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Units;
using Pipeline.Testing.Pages.Estimating.Units.UnitDetail;
using Pipeline.Testing.Pages.Import;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class B06_RT_01256 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private UnitData oldData;
        private UnitData newValidTestData;
        private UnitData newInValidTestData;
        private List<string> productList;

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private const string PRODUCT1_NAME = "QA_RT_ProductUnit01_Auto";
        private const string PRODUCT2_NAME = "QA_RT_ProductUnit02_Auto";
        [SetUp]
        public void GetData()
        {
            oldData = new UnitData()
            {
                Name = "Data using for automation testing",
                Abbreviation = "Ab_Auto"
            };

            newValidTestData = new UnitData()
            {
                Name = "Data using for automation testing. Updating",
                Abbreviation = "AbUpdate"
            };

            newInValidTestData = new UnitData()
            {
                Name = string.Empty,
                Abbreviation = "AbUpdate"
            };

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "Prepare a new Manufacturer to import Product.");
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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Style to import Product.");
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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Building Group to import Product.");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

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

            UnitPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Units);
            ExtentReportsHelper.LogInformation($"Filter new item {oldData.Name} in the grid view.");
            UnitPage.Instance.FilterItemInGrid("Abbreviation", GridFilterOperator.Contains, oldData.Abbreviation);
            if (UnitPage.Instance.IsItemInGrid("Abbreviation", oldData.Abbreviation)is false)
            {
                UnitPage.Instance.ClickAddToShowUnitModal();
                // Load simple data from excel and add to model
                UnitPage.Instance.AddUnitModal.AddAbbreviation(oldData.Abbreviation).AddName(oldData.Name);

                // 4. Select the 'Save' button on the modal;
                UnitPage.Instance.AddUnitModal.Save();
                //UnitPage.Instance.GetLastestToastMessage();
                // Verify successful save and appropriate success message.
               // UnitPage.Instance.CloseToastMessage();
                UnitPage.Instance.FilterItemInGrid("Abbreviation", GridFilterOperator.EqualTo, oldData.Abbreviation);
            }
            // Filter New item
            ExtentReportsHelper.LogInformation($"Filter new item {newValidTestData.Name} in the grid view.");
            UnitPage.Instance.DeleteUnit(newValidTestData);

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "Prepare data: Import Building Phase to import Product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_01256\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "Prepare Data: Import Product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_01256\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
        }

        [Test]
        [Category("Section_IV")]
        public void B06_A_Estimating_DetailPage_Unit_UnitDetails()
        {
            productList = new List<string>() { PRODUCT1_NAME, PRODUCT2_NAME };

            UnitPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Units);
            ExtentReportsHelper.LogInformation($"Filter new item {oldData.Name} in the grid view.");
            UnitPage.Instance.FilterItemInGrid("Abbreviation", GridFilterOperator.Contains, oldData.Abbreviation);

            // Step 1: Navigate to Estimate menu > Units
            ExtentReportsHelper.LogInformation(null, "Step 1: Navigate to Estimate menu > Units");

            var _unitURL = BaseDashboardUrl + BaseMenuUrls.VIEW_UNITS_URL;
            if (UnitPage.Instance.IsPageDisplayed(_unitURL) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Units page is displayed.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Units page isn't displayed.</font>");
            }

            // Step 2: Select item to open Unit detail page
            ExtentReportsHelper.LogInformation($" Step 2: Select {oldData.Abbreviation} item to open Unit detail page.");
            UnitPage.Instance.SelectItemInGrid("Abbreviation", oldData.Abbreviation);

            // Verify open Option Type detail page display correcly
            if (UnitDetailPage.Instance.IsDisplayDataCorrectly(oldData) is true)
                ExtentReportsHelper.LogPass($"The Unit detail page of item: {oldData.Name} displays correctly.");

            // Step 3: Update Style detail page with valid data
            ExtentReportsHelper.LogInformation(" Step 3: Update Unit detail page.");
            UpdateUnit(newValidTestData);

            // Step 4: Update Style detail page with empty Name
            ExtentReportsHelper.LogInformation(" Step 4: Update Unit detail page with empty Name.");
            UpdateUnit(newInValidTestData, false);

            // Step 5: Add product to Unit
            ExtentReportsHelper.LogInformation(" Step 5: Add product to Unit.");
            AddProductToUnit();
        }

        private void UpdateUnit(UnitData newTestData, bool isUpdateValidData = true)
        {
            string expectedMessage;

            if (isUpdateValidData)
            {
                ExtentReportsHelper.LogInformation("Update Unit with valid data.");
                expectedMessage = "Product Unit updated successfully.";
            }
            else
            {
                ExtentReportsHelper.LogInformation("Update Unit with empty name or abbreviation.");
                expectedMessage = "Product Unit name or abbreviation cannot be empty.";
            }
            UnitDetailPage.Instance.UpdateUnit(newTestData);


            var actualMessage = UnitDetailPage.Instance.GetLastestToastMessage();
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
            }
            else if (expectedMessage == actualMessage)
            {
                if (isUpdateValidData)
                {
                    ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");
                }
                else
                {
                    ExtentReportsHelper.LogPass("Can't update. The toast message same as expected.");

                }
                if (UnitDetailPage.Instance.IsDisplayDataCorrectly(newValidTestData))
                    ExtentReportsHelper.LogPass("The updated data displays or reset correctly in the grid view.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
            }
        }

        private void AddProductToUnit()
        {

            foreach (string productName in productList)
            {
                if(UnitDetailPage.Instance.IsItemInGrid("Product Name", productName) is true)
                {
                    UnitDetailPage.Instance.DeleteItemInGrid("Product Name", productName);
                     string expectedDeleteMessage = "Product successfully removed.";
                    string actualDeleteMessage = UnitDetailPage.Instance.GetLastestToastMessage();

                    if (actualDeleteMessage == expectedDeleteMessage)
                    {
                        ExtentReportsHelper.LogPass($"Product {productName} deleted successfully!");
                        UnitDetailPage.Instance.CloseToastMessage();
                    }
                    else
                    {
                        if (UnitDetailPage.Instance.IsItemInGrid("Product Name", productName))
                            ExtentReportsHelper.LogFail($"Product {productName} can't be deleted!");
                        else
                            ExtentReportsHelper.LogPass($"Product {productName} deleted successfully!");

                    }
                }

            }

            UnitDetailPage.Instance.OpenAddProductToUnitModal();


            if (!UnitDetailPage.Instance.AddProductToUnitModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Product to Unit\" modal doesn't display.");
                return;
            }

            // Select 3 first items from the list
            ExtentReportsHelper.LogInformation("Select product in the list and click save button.");
            UnitDetailPage.Instance.AddProductToUnitModal.AddProductToUnit(productList);

            var expectedMessage = "Products were successfully added.";
            var actualMessage = UnitDetailPage.Instance.GetLastestToastMessage();

            if (actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add Product to Unit unsuccessfully. Actual messsage: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add Product to Unit successfully.");
            }
            //UnitDetailPage.Instance.AddProductToUnitModal.CloseModal();


            ExtentReportsHelper.LogInformation(" Step 6: Verify products added to Unit and hyperlink.");
            // Verify  new items which is added to the grid view
            ExtentReportsHelper.LogInformation("Filter new product in the grid view.");
            foreach (string item in productList)
            {
                // Insert name to filter and click filter by Contain value
                UnitDetailPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, item);
                System.Threading.Thread.Sleep(2000);

                bool isFoundItem = UnitDetailPage.Instance.IsItemInGrid("Product Name", item);
                if (!isFoundItem)
                {
                    ExtentReportsHelper.LogFail($"The Product \"{item} \" was not display on the grid.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"The Product \"{item}\" was found on the grid view.");

                    // Check hyperlink
                    ExtentReportsHelper.LogInformation("Click into product name and verify the hyperlink.");
                    UnitDetailPage.Instance.VerifyHyperlinkToProduct(item);

                    RemoveProductFromUnit(item);
                }
            }
        }

        private void RemoveProductFromUnit(string productName)
        {
            ExtentReportsHelper.LogInformation(" Step 4: Delete product out of unit.");

            UnitDetailPage.Instance.DeleteItemInGrid("Product Name", productName);           

            var expectedMessage = "Product successfully removed.";
            var actualMessage = UnitDetailPage.Instance.GetLastestToastMessage();

            if (actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Product {productName} deleted successfully!");
                UnitDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (UnitDetailPage.Instance.IsItemInGrid("Product Name", productName))
                    ExtentReportsHelper.LogFail($"Product {productName} can't be deleted!");
                else
                    ExtentReportsHelper.LogPass($"Product {productName} deleted successfully!");

            }
        }

        [TearDown]
        public void DeleteUnit()
        {
            // Step 1: Navigate to Estimate menu > Units
            ExtentReportsHelper.LogInformation($"Back to Unit default page and delete item {newValidTestData.Abbreviation}");
            UnitPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Units);

            // Filter New item
            ExtentReportsHelper.LogInformation($"Filter new item {oldData.Name} in the grid view.");
            UnitPage.Instance.DeleteUnit(newValidTestData);
        }
    }
}
