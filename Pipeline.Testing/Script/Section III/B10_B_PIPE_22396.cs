using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Uses;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B10_B_PIPE_22396 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private const string EXPORT_CSV_MORE_MENU = "Export CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";
        private const string USE_IMPORT = "Uses Import";
        private const string IMPORT_VIEW = "Product Attributes";
        
        private int totalItems;
        private string exportFileName;

        private UsesData newUseData_1;
        private UsesData newUseData_2;
        private UsesData existingUseData_1;
        private UsesData existingUseData_2;

        private IList<UsesData> newUseList;
        private IList<UsesData> existingUseList;


        [SetUp]
        public void SetUp()
        {
            // Make sure current transfer seperation character is ','
            // Step 0.1: Navigate to Setting page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Navigate to Setting page.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            // Step 0.2: Navigate to Use page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Navigate to Use page.</b></font>");
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);


            newUseData_1 = new UsesData()
            {
                Name = "RT_QA_New_Use_Auto_01"
            };
            newUseData_2 = new UsesData()
            {
                Name = "RT_QA_New_Use_Auto_02"
            };

            newUseList = new List<UsesData>
            {
                newUseData_1,
                newUseData_2
            };

            // Step 0.3: Delete Use {RT_QA_New_Use_Auto} that's going to be imported on step 2.1
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.3: Delete Use '{newUseData_1.Name}' and '{newUseData_2.Name}' that's going to be imported on step 2.1.</b></font>");
            foreach (UsesData data in newUseList)
            {
                UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (UsesPage.Instance.IsItemInGrid("Name", data.Name) is true)
                    UsesPage.Instance.DeleteUses(data.Name);
            }

            existingUseData_1 = new UsesData()
            {
                Name = "RT_QA_Existing_Use_Auto_01"
            };
            existingUseData_2 = new UsesData()
            {
                Name = "RT_QA_Existing_Use_Auto_02"
            };

            existingUseList = new List<UsesData>
            {
                existingUseData_1,
                existingUseData_2
            };


            // Step 0.4: Make sure these Uses {RT_QA_New_Use_Auto_01, RT_QA_New_Use_Auto_02} are existing, to import existing one on step 2.2
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Make sure these Uses {RT_QA_New_Use_Auto_01, RT_QA_New_Use_Auto_02} are existing, to import existing one on step 2.2</b></font>");
            CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES);
            CommonHelper.SwitchLastestTab();
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_ATTRIBUTES_VIEW, ImportGridTitle.USE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.USE_IMPORT} grid view to import Use.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_22396\\Pipeline_ProductUses.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.USE_IMPORT, importFile);

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

            // Step 0.5: Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Step 0.5: Get the total items on the UI.</b></font>");
            CommonHelper.RefreshPage();
            CommonHelper.ScrollToEndOfPage();
            totalItems = UsesPage.Instance.GetTotalNumberItem();


            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();

            // Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.ProductUses.ToString());

            // Download baseline files before comparing files
            UsesPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            UsesPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void B10_B_Estimating_Uses_Export_Import_Uses()
        {
            // Step 1.1: Export CSV file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Export CSV file and make sure the export file existed.</b></font>");
            UsesPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.USE_TITLE);
            //UsesPage.Instance.CompareExportFile(exportFileName, TableType.CSV);


            // Step 1.2: Export Excel file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Export Excel file and make sure the export file existed.</b></font>");
            UsesPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.USE_TITLE);
            //UsesPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);


            // Step 2: Open Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open Import page.</b></font>");
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses, true, true);
            CommonHelper.SwitchLastestTab();
            UsesPage.Instance.OpenImportPage(BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES);

            // Click View Drop down list and verify Uses Import grid view
            // Don't import any file if can't find Use Import grid
            if (ProductsImportPage.Instance.IsImportGridDisplay(IMPORT_VIEW, USE_IMPORT) is false)
                return;


            // Step 2.1: Import new Uses - New Use does not exist in system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Import new Use - New Use does not exist in system.</b></font>");
            string useImportFile = "\\DataInputFiles\\Estimating\\Uses\\Pipeline_ProductUse_New_Use.csv";
            ProductsImportPage.Instance.ImportValidData(USE_IMPORT, useImportFile);


            // Step 2.2: Import existing Uses - All data exist in systems
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Import existing Uses - All data exist in systems.</b></font>");
            useImportFile = "\\DataInputFiles\\Estimating\\Uses\\Pipeline_ProductUse_Existing_Use.csv";
            ProductsImportPage.Instance.ImportValidData(USE_IMPORT, useImportFile);


            // Step 2.3: Wrong format import file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3: Wrong format import file.</b></font>");
            string UseImportFile = "\\DataInputFiles\\Estimating\\Uses\\Pipeline_ProductUse_Wrong_Format.csv";
            string expectedErrorMess = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ProductsImportPage.Instance.ImportInvalidData(USE_IMPORT, UseImportFile, expectedErrorMess);


            // Step 2.4: File without Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4: File without Data.</b></font>");
            UseImportFile = "\\DataInputFiles\\Estimating\\Uses\\Pipeline_ProductUses_Without_Data.csv";
            ProductsImportPage.Instance.ImportValidData(USE_IMPORT, UseImportFile);


            // Step 2.5: File without header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.5: File without header.</b></font>");
            expectedErrorMess = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            UseImportFile = "\\DataInputFiles\\Estimating\\Uses\\Pipeline_ProductUses_Without_Header.csv";
            ProductsImportPage.Instance.ImportInvalidData(USE_IMPORT, UseImportFile, expectedErrorMess);


            // Step 2.6: File has the “character” between fields don’t match with the configure
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.6: File has the “character” between fields don’t match with the configure.</b></font>");
            expectedErrorMess = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
            UseImportFile = "\\DataInputFiles\\Estimating\\Uses\\Pipeline_ProductUses_Invalid_Transfer_Separater_Charater.csv";
            ProductsImportPage.Instance.ImportInvalidData(USE_IMPORT, UseImportFile, expectedErrorMess);

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }
        #endregion


        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Navigate to Use page.</b></font>");
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);

            // Delete Use that's going to be imported on step 2.3
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Delete Use that's going to be imported on step 2.3.</b></font>");
            foreach (UsesData data in newUseList)
            {
                UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (UsesPage.Instance.IsItemInGrid("Name", data.Name) is true)
                    UsesPage.Instance.DeleteUses(data.Name);
            }
        }
    }
}
