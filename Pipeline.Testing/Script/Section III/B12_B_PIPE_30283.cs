using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Category;
using Pipeline.Testing.Pages.Estimating.Category.CategoryDetail;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B12_B_PIPE_30283 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private const string EXPORT_CSV_MORE_MENU = "Export CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";

        private const string CATEGORY_IMPORT = "Categories Import";
        private const string PRODUCT_TO_CATEGORIES_IMPORT = "Products To Categories Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string IMPORT_VIEW = "Product Attributes";
        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private int totalItems;
        private string exportFileName;

        private CategoryData newCategory_1;
        private CategoryData newCategory_2;
        private CategoryData existingCategory_1;
        private CategoryData existingCategory_2;

        private IList<CategoryData> newCategoryList;
        private IList<CategoryData> existingCategoryList;

        private const string EIXISTING_CATEGORY_NAME_01 = "QA_RT_Existing_Category_Auto_01";
        private const string EIXISTING_CATEGORY_NAME_02 = "QA_RT_Existing_Category_Auto_02";

        private const string NEW_CATEGORY_NAME_01 = "QA_RT_New_Category_Auto_01";
        private const string NEW_CATEGORY_NAME_02 = "QA_RT_New_Category_Auto_02";

        private int totalItemFromCSVImportFile = 2;


        [SetUp]
        public void SetUp()
        {
            // Make sure current transfer seperation character is ','
            // Step 0.1: Navigate to Setting page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Navigate to Setting page.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            // Step 0.2: Navigate to Category page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Navigate to Category page.</b></font>");
            CategoryPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Categories);


            newCategory_1 = new CategoryData()
            {
                Name = NEW_CATEGORY_NAME_01
            };
            newCategory_2 = new CategoryData()
            {
                Name = NEW_CATEGORY_NAME_02
            };

            newCategoryList = new List<CategoryData>
            {
                newCategory_1,
                newCategory_2
            };

            // Step 0.3: Delete Category {RT_QA_New_Category_Auto} that's going to be imported on step 2.1
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.3: Delete Category '{newCategory_1.Name}' and '{newCategory_2.Name}' that's going to be imported on step 2.1.</b></font>");
            foreach (CategoryData data in newCategoryList)
            {
                CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (CategoryPage.Instance.IsItemInGrid("Name", data.Name) is true)
                    CategoryPage.Instance.DeleteCategory(data.Name);
            }

            // Clear filter
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);

            existingCategory_1 = new CategoryData()
            {
                Name = EIXISTING_CATEGORY_NAME_01
            };
            existingCategory_2 = new CategoryData()
            {
                Name = EIXISTING_CATEGORY_NAME_02
            };

            existingCategoryList = new List<CategoryData>
            {
                existingCategory_1,
                existingCategory_2
            };

            // Step 0.4: Make sure these Categories {RT_QA_New_Category_Auto_01, RT_QA_New_Category_Auto_02} are existing by importing, to import existing one on step 2.2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Make sure these Categories {RT_QA_New_Category_Auto_01, RT_QA_New_Category_Auto_02} are existing by importing, to import existing one on step.</b></font>");
            CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES);
            CommonHelper.SwitchLastestTab();

            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_ATTRIBUTES_VIEW, ImportGridTitle.OPTION_CATEGORY_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_CATEGORY_IMPORT} grid view to import.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_30283\\Pipeline_Categories.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_CATEGORY_IMPORT, importFile);

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

            // Clear focus from Estimating menu by reloading page
            CommonHelper.RefreshPage();
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);


            // Step 0.5: Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Step 0.5: Get the total items on the UI.</b></font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = CategoryPage.Instance.GetTotalNumberItem();


            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();

            // Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.Categories.ToString());

            // Download baseline files before comparing files
            CategoryPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            CategoryPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);


            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Create a new Manufacturer to import Product.</b></font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers, true, true);

            CommonHelper.SwitchLastestTab();

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_RT_New_Manu_Auto"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            // Can't import new Style then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Create a new Style to import Product.</b></font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "QA_RT_New_Style_Auto",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }


            // Step 0.8: Create a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Create a new Building Group to import Product.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "12111111",
                Name = "QA_RT_New_Building_Group_Auto_01"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }


            // Step 0.9: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9: Import Building Phase to import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_30283\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(BUILDING_GROUP_PHASE_IMPORT, importFile);


            // Step 0.10: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10: Import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_30283\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);


            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
}

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void B12_B_Estimating_Categories_Export_Import_Categories()
        {
            /******************************** Export Category *************************************/

            // Step 1.1: Export CSV file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Export CSV file and make sure the export file existed.</b></font>");
            CategoryPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.CATEGORY_TITLE);
            //CategoryPage.Instance.CompareExportFile(exportFileName, TableType.CSV);


            // Step 1.2: Export Excel file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Export Excel file and make sure the export file existed.</b></font>");
            CategoryPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.CATEGORY_TITLE);
            //CategoryPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);


            // Step 2: Open Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open Import page.</b></font>");
            CategoryPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Categories, true, true);
            CommonHelper.SwitchLastestTab();
            CategoryPage.Instance.OpenImportPage(BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES);

            /******************************** Import Category *************************************/

            // Click View Drop down list and verify Categories Import grid view
            // Don't import any file if can't find Category Import grid
            if (ProductsImportPage.Instance.IsImportGridDisplay(IMPORT_VIEW, CATEGORY_IMPORT) is false)
                return;


            // Step 2.1: Import new Categories - New Category does not exist in system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Import new Category - New Category does not exist in system.</b></font>");
            string CategoryImportFile = "\\DataInputFiles\\Estimating\\Categories\\Pipeline_Categories_New_Categories.csv";
            ProductsImportPage.Instance.ImportValidData(CATEGORY_IMPORT, CategoryImportFile);


            // Step 2.2: Import existing Categories - All data exist in systems
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Import existing Categories - All data exist in systems.</b></font>");
            CategoryImportFile = "\\DataInputFiles\\Estimating\\Categories\\Pipeline_Categories_Existing_Categories.csv";
            ProductsImportPage.Instance.ImportValidData(CATEGORY_IMPORT, CategoryImportFile);


            // Step 2.3: Wrong format import file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3: Wrong format import file.</b></font>");
            string importFile = "\\DataInputFiles\\Estimating\\Categories\\Pipeline_Categories_Wrong_format.csv";
            string expectedErrorMess = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ProductsImportPage.Instance.ImportInvalidData(CATEGORY_IMPORT, importFile, expectedErrorMess);


            // Step 2.4: File without Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4: File without Data.</b></font>");
            importFile = "\\DataInputFiles\\Estimating\\Categories\\Pipeline_Categories_Without_Data.csv";
            ProductsImportPage.Instance.ImportValidData(CATEGORY_IMPORT, importFile);


            // Step 2.5: File without header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.5: File without header.</b></font>");
            expectedErrorMess = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            importFile = "\\DataInputFiles\\Estimating\\Categories\\Pipeline_Categories_Without_header.csv";
            ProductsImportPage.Instance.ImportInvalidData(CATEGORY_IMPORT, importFile, expectedErrorMess);


            // Step 2.6: File has the “character” between fields don’t match with the configure
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.6: File has the “character” between fields don’t match with the configure.</b></font>");
            expectedErrorMess = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
            importFile = "\\DataInputFiles\\Estimating\\Categories\\Pipeline_Categories_Invalid_Transfer_Separater_Charater.csv";
            ProductsImportPage.Instance.ImportInvalidData(CATEGORY_IMPORT, importFile, expectedErrorMess);



            /******************************** Import Products to Categories *************************************/
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.7: Import Products to Categories.</b></font>");
            string productToCategoryImportFile = "\\DataInputFiles\\Estimating\\Categories\\Pipeline_ProductToCategory_Valid_Data.csv";
            ProductsImportPage.Instance.ImportValidData(PRODUCT_TO_CATEGORIES_IMPORT, productToCategoryImportFile);


            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

            /******************************** Export Products to Category *************************************/


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.8: Export Products to Category.</b></font>");
            // Back to Category detail page and export product
            // If there is no item in grid then return and don't export product to category
            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, EIXISTING_CATEGORY_NAME_01);
            if (CategoryPage.Instance.IsItemInGrid("Name", EIXISTING_CATEGORY_NAME_01) is false)
                return;

            // Remove all products on categỏy detail page is it's existing
            CategoryPage.Instance.SelectItemInGrid("Name", EIXISTING_CATEGORY_NAME_01);

            // Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.ProductToCategory.ToString());

            // Download baseline file
            CategoryDetailPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);

            // Remove focus from more menu by checking the title
            CategoryDetailPage.Instance.RemoveFocusFromMoreMenu();

            CategoryDetailPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
            CategoryDetailPage.Instance.RemoveFocusFromMoreMenu();

            // Export and compare it
            CategoryDetailPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItemFromCSVImportFile, ExportTitleFileConstant.PRODUCT_TO_CATEGORY_TITLE);
            //CategoryDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            System.Threading.Thread.Sleep(2000);

            if (CategoryDetailPage.Instance.getNumberProductOnGrid() != 0)
            {
                CategoryDetailPage.Instance.DeleteAllProduct();
                CategoryDetailPage.Instance.WaitGridLoad();
            }
        }
        #endregion


        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Navigate to Category page.</b></font>");
            CategoryPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Categories);

            // Delete Category that's going to be imported on step 2.3
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Delete Category that's going to be imported on step 2.3.</b></font>");
            foreach (CategoryData data in newCategoryList)
            {
                CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (CategoryPage.Instance.IsItemInGrid("Name", data.Name) is true)
                    CategoryPage.Instance.DeleteCategory(data.Name);
            }


            // Step 3.3: Delete Product on {EIXISTING_CATEGORY_NAME_01} 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.3: Delete all Product on Category '{existingCategory_1.Name}'.</b></font>");

            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, existingCategory_1.Name);
            if (CategoryPage.Instance.IsItemInGrid("Name", existingCategory_1.Name) is true)
            {
                // Open Category detail page and delete all products
                CategoryPage.Instance.SelectItemInGrid("Name", existingCategory_1.Name);
                CategoryDetailPage.Instance.DeleteAllProduct();
                CategoryDetailPage.Instance.WaitGridLoad();
            }

        }
    }
}
