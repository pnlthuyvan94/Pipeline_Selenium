using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_III
{
    class B01_B_PIPE_19516 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        string importFile;

        private const string EXPORT_XML_MORE_MENU = "Export Products to XML";
        private const string EXPORT_CSV_MORE_MENU = "Export Products to CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Products to Excel";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private readonly string PRODUCT1_NAME_DEFAULT = "Product_PIPE_19516_Automtion";
        private readonly string PRODUCT2_NAME_DEFAULT = "Product_PIPE_19516_2_Automtion";

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

            string importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);
            
        }

        [Test]
        [Category("Section_III")]
        public void B01_B_Estimating_Products_Export_Import_Products()
        {

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Make sure current transfer seperation character is '*'</font><b>");
            //Make sure current transfer seperation character is ''
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            //I. Verify the Products Export function
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I. Verify the Products Export function</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            CommonHelper.ScrollToEndOfPage();
            int totalItems = ProductPage.Instance.GetTotalNumberItem();
            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();
            // Get export file name
            string exportFileName = CommonHelper.GetExportFileName(ExportType.Products.ToString());

            //I1. Export Products to XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I1. Export Products to XML</font><b>");
            // Download baseline files before comparing files
            ProductPage.Instance.DownloadBaseLineProuductFile(EXPORT_XML_MORE_MENU, exportFileName);
            ProductPage.Instance.ExportProductFile(EXPORT_XML_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.PRODUCT);
            //I2. Export Products to CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I2. Export Products to CSV</font><b>");
            // Download baseline files before comparing files
            ProductPage.Instance.DownloadBaseLineProuductFile(EXPORT_CSV_MORE_MENU, exportFileName);
            ProductPage.Instance.ExportProductFile(EXPORT_CSV_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.PRODUCT);

            //I3. Export Products to Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I3. Export Products to Excel</font><b>");
            // Download baseline files before comparing files
            ProductPage.Instance.DownloadBaseLineProuductFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
            ProductPage.Instance.ExportProductFile(EXPORT_EXCEL_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.PRODUCT);

            CommonHelper.RefreshPage();

            //II. Verify the Products Import function
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II. Verify the Products Import function</font><b>");
            //a. Normal file - All data exist in systems
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.a. Normal file - All data exist in systems</font><b>");
            ProductPage.Instance.ImportExporFromMoreMenu("Import");

            string expectedURL = BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT;
            if (ProductsImportPage.Instance.IsPageDisplayed(expectedURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import Product page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Import Product page is displayed</b></font>");
            }

            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIa.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            //b. Normal file - New data not exist in systems
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.b. Normal file - New data not exist in systems</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIb.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
                     
            //c. File without Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.c. File without Data</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIc.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            //d. File without header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.d. File without header</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IId.csv";
            List<string> ListMessage = new List<string>() { "Failed to import file due to an error in the data format. Column headers do not match expected values." };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //e. File without Product Name
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.e. File without Product Name</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIe.csv";
            ListMessage = new List<string>() { "Product with name could not be found" };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //f. File without Style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.f. File without Style</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIf.csv";
            ListMessage = new List<string>() { "Style Name is required" };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //g. File with Style not exist in system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.g. File with Style not exist in system</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIg.csv";
            ListMessage = new List<string>() { "The Style could not be found" };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //h. File without Building Phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.h. File without Building Phase</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIh.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
            CommonHelper.RefreshPage();

            //i. File with Building Phase not exist in system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.i. File with Building Phase not exist in system</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIi.csv";
            ListMessage = new List<string>() { "The Building Phase could not be found" };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //k. File with duplicate Name - Name not exist in systems
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.k. File with duplicate Name - Name not exist in systems</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIk.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            //l. File wrong the Transfer Separation Character
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.l. File wrong the Transfer Separation Character</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIl.csv";
            ListMessage = new List<string>() { "Line 2 cannot be parsed using the current Delimiters." };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //m. File missing the Close Character
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.m. File missing the Close Character</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIm.csv";
            ListMessage = new List<string>() { "Line 2 cannot be parsed using the current Delimiters." };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //n. File missing the header column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.n. File missing the header column</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIn.csv";
            ListMessage = new List<string>() { "Failed to import file due to an error in the data format. Column headers do not match expected values." };
            ProductsImportPage.Instance.ImportInvalidFormat(ImportGridTitle.PRODUCT_IMPORT, importFile, ListMessage);

            //x. Empty file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.x. Empty file</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_19516\\Pipeline_Products_IIx.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

        }

        [TearDown]
        public void ClearData()
        {
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT1_NAME_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT1_NAME_DEFAULT) is true)
            {
                ProductPage.Instance.DeleteProduct(PRODUCT1_NAME_DEFAULT);
            }
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT2_NAME_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT2_NAME_DEFAULT) is true)
            {
                ProductPage.Instance.DeleteProduct(PRODUCT2_NAME_DEFAULT);
            }
        }
        }
    }
