using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_III
{
    class B02_B_PIPE_22393 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private const string EXPORT_XML_MORE_MENU = "Export XML";
        private const string EXPORT_CSV_MORE_MENU = "Export CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";

        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";
        private const string BUILDING_GROUP_NAME = "Testing Import";
        private const string BUILDING_GROUP_NAME1 = "Testing Import 1";
        private const string BUILDING_GROUP_NAME2 = "Testing Import 2";
        private const string BUILDING_GROUP_NAME3 = "Testing Import 3";
        private const string BUILDING_GROUP_NAME4 = "Testing Import 4";
        private const int BUILDING_GROUP_NAME_TOTAL = 4;


        [Test]
        [Category("Section_III")]
        public void B02_B_Estimating_BuildingGroups_Export_Import_BuildingGroups()
        {          
            //21. Click on Utilities icon
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 21. Click on Utilities icon.</font><b>");
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Get the total items on the UI.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            CommonHelper.ScrollToEndOfPage();
            int totalItems = BuildingGroupPage.Instance.GetTotalNumberItem();

            //22. Verify if the following selections are available in the dropdown list:Export XML, Export CSV, Export Excel, Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 22. Verify if the following selections are available in the dropdown list:Export XML, Export CSV, Export Excel, Import.</font><b>");
            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();
            // Get export file name
            string exportFileName = CommonHelper.GetExportFileName(ExportType.BuildingGroups.ToString());
            //23. Select Export XML from the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 23. Select Export XML from the dropdown list.</font><b>");
            
            //24. Verify if you are able to download a .xml file: Pipeline_BuildingGroups.xml
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 24. Verify if you are able to download a .xml file: Pipeline_BuildingGroups.xml.</font><b>");
            // Download baseline files before comparing files
            BuildingGroupPage.Instance.DownloadBaseLineFile(EXPORT_XML_MORE_MENU, exportFileName);

            //25. Verify if the file contains the correct data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 25. Verify if the file contains the correct data.</font><b>");
            BuildingGroupPage.Instance.ExportFile(EXPORT_XML_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.BUILDINGGROUPS_TITLE);
            
            //26. Select Export CSV from the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 26. Select Export CSV from the dropdown list.</font><b>");
            
            //27. Verify if you are able to download a .csv file: Pipeline_BuildingGroups.csv
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 27. Verify if you are able to download a .csv file: Pipeline_BuildingGroups.csv.</font><b>");
            // Download baseline files before comparing files
            BuildingGroupPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);

            //28. Verify if the file contains the correct data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 28. Verify if the file contains the correct data.</font><b>");
            BuildingGroupPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.BUILDINGGROUPS_TITLE);
            //29. Select Export Excel from the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 29. Select Export Excel from the dropdown list.</font><b>");
            
            //30. Verify if you are able to download a .xlsx file: Pipeline_BuildingGroups.xlsx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 30. Verify if you are able to download a .xlsx file: Pipeline_BuildingGroups.xlsx.</font><b>");
            // Download baseline files before comparing files
            BuildingGroupPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);

            //31. Verify if the file contains the correct data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 31. Verify if the file contains the correct data.</font><b>");
            BuildingGroupPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU,exportFileName, totalItems, ExportTitleFileConstant.BUILDINGGROUPS_TITLE);

            //32. Select Import from the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 32. Select Import from the dropdown list.</font><b>");
            BuildingGroupPage.Instance.ImportExporFromMoreMenu("Import");
            
            //33. Verify if transfer/imports page is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 33. Verify if transfer/imports page is displayed.</font><b>");
            string expectedURL = BaseDashboardUrl + "/Products/transfers/imports/products.aspx?view=BuildingGroupsAndPhases";
            if (ProductsImportPage.Instance.IsPageDisplayed(expectedURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import Building Group And Phase page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Import Building Group And Phase page is displayed</b></font>");
            }
            
            //34. Verify if Building Group/Phases Import pane is available in imports page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 34. Verify if Building Group/Phases Import pane is available in imports page.</font><b>");
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {BUILDING_GROUP_PHASE_VIEW} grid view to import new bulding phase.</font>");
            
            //35. Download the attached import file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 35. Download the attached import file.</font><b>");
            
            //36. Click on Choose File button and select the downloaded .csv file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 36. Click on Choose File button and select the downloaded .csv file.</font><b>");
            string importFile = "\\DataInputFiles\\Import\\PIPE_22393\\Pipeline_BuildingPhases_Automation.csv";
            
            //37, Click on Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 37, Click on Import button.</font><b>");
            
            //38. Verify if correct success message displayed on the import pane: Import complete.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 38. Verify if correct success message displayed on the import pane: Import complete..</font><b>");
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);
            
            //39. Navigate back to this page > /Dashboard/Products/BuildingGroups/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 39. Navigate back to this page > /Dashboard/Products/BuildingGroups/Default.aspx.</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);
            
            //40. Filter from “Name” column this filter keyword: Testing Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 40. Filter from “Name” column this filter keyword: Testing Import.</font><b>");
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, BUILDING_GROUP_NAME);
            
            //41. Verify if the filter results shows all the imported building groups:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 41. Verify if the filter results shows all the imported building groups:</font><b>");
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", BUILDING_GROUP_NAME1) is true
                && BuildingGroupPage.Instance.IsItemInGrid("Name", BUILDING_GROUP_NAME2) is true
                && BuildingGroupPage.Instance.IsItemInGrid("Name", BUILDING_GROUP_NAME3) is true
                && BuildingGroupPage.Instance.IsItemInGrid("Name", BUILDING_GROUP_NAME4) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import Building Group And Phase page is successfully and show in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import Building Group And Phase page is unsuccessfully and isn't show in grid.</font>");
            }

            //42. Select all then click on Bulk Actions > Delete Selected option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 42. Select all then click on Bulk Actions > Delete Selected option</font><b>");
            BuildingGroupPage.Instance.DeleteItemSelectedInGrid(BUILDING_GROUP_NAME_TOTAL);
            
            //43. Verify if correct success toast notification message is displayed on the page: [x] of [y] selected Building Groups deleted successfully.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 43. Verify if correct success toast notification message is displayed on the page: [x] of [y] selected Building Groups deleted successfully.</font><b>");
            string expectedDeletedMessage = $"{BUILDING_GROUP_NAME_TOTAL} of {BUILDING_GROUP_NAME_TOTAL} selected Building Groups deleted successfully.";
            string actualDeletedMessage = BuildingGroupDetailPage.Instance.GetLastestToastMessage();
            if (actualDeletedMessage == expectedDeletedMessage)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Delete Building Group successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color ='red'>Delete Building Group unsuccessfully.<br>Actual result :{actualDeletedMessage}<br></font>");
            }

            //44. Verify if all the building groups are no longer in the table grid and displays No records to display.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 44. Verify if all the building groups are no longer in the table grid and displays No records to display.</font><b>");
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", BUILDING_GROUP_NAME1) is false
             && BuildingGroupPage.Instance.IsItemInGrid("Name", BUILDING_GROUP_NAME2) is false
            && BuildingGroupPage.Instance.IsItemInGrid("Name", BUILDING_GROUP_NAME3) is false
             && BuildingGroupPage.Instance.IsItemInGrid("Name", BUILDING_GROUP_NAME4) is false)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Delete Building Group And Phase page is successfully and show in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Delete Building Group And Phase page is unsuccessfully and isn't show in grid.</font>");
            }

            //45. Navigate back to / Dashboard / Products / transfers / imports / products.aspx ? view = BuildingGroupsAndPhases
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 45. Navigate back to / Dashboard / Products / transfers / imports / products.aspx ? view = BuildingGroupsAndPhases.</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);

            //46. In Building Group/Phases Import pane, click Choose File button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 46. In Building Group/Phases Import pane, click Choose File button.</font><b>");
            
            //47. Choose the file that you have downloaded in step #30
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 47. Choose the file that you have downloaded in step #30.</font><b>");
            
            //48. Click Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 48. Click Import button.</font><b>");
            
            //49. Verify if correct error validation message is displayed: Failed to import file due to wrong file format. File must be .csv format.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 49. Verify if correct error validation message is displayed: Failed to import file due to wrong file format. File must be .csv format..</font><b>");
            string expectedErrorMess = "Failed to import file due to wrong file format. File must be .csv format.";
            string ImportFile = "\\DataInputFiles\\Import\\PIPE_22393\\Pipeline_BuildingPhasesErrorFile_Automation.xlsx";
            ProductsImportPage.Instance.ImportInvalidData(ImportGridTitle.BUILDING_PHASE_IMPORT, ImportFile, expectedErrorMess);
        }

    }
}
