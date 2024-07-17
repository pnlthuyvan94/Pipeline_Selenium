using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A02_B_PIPE_22388 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private const string EXPORT_XML_MORE_MENU = "Export XML";
        private const string EXPORT_CSV_MORE_MENU = "Export CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";
        private const string COMMUNITY_IMPORT = "Community Import";
        private const string NEW_COMMUNITY_NAME = "RT_QA_New_Community_Auto";

        private int totalItems;
        private string exportFileName;

        private CommunityData communityData_1;
        private CommunityData communityData_2;

        [SetUp]
        public void SetUp()
        {
            // Make sure current transfer seperation character is ','
            // Step 0.1: Navigate to Setting page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Navigate to Setting page.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);


            // Step 0.2: Navigate to Community page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Navigate to Community page.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);


            // Step 0.3: Delete community {RT_QA_New_Community_Auto} that's going to be imported on step 0.3
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Delete community '{NEW_COMMUNITY_NAME}' that's going to be imported on step 0.3.</b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NEW_COMMUNITY_NAME);
            if (CommunityPage.Instance.IsItemInGrid("Name", NEW_COMMUNITY_NAME) is true)
                CommunityPage.Instance.DeleteCommunity(NEW_COMMUNITY_NAME);

            // Clear filter
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);

            // Step 0.4: Make sure these communities {RT_QA_New_Community_Auto_01, RT_QA_New_Community_Auto_02} are existing, to import existing one on step 2.2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Make sure these communities {RT_QA_New_Community_Auto_01, RT_QA_New_Community_Auto_02} are existing by importing, to import existing one on step 2.2.</b></font>");
            CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            CommonHelper.SwitchLastestTab();

            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_IMPORT} grid view to import new Options.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_22388\\Pipeline_Communities.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_IMPORT, importFile);

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

            // Step 0.5: Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Step 0.5: Get the total items on the UI.</b></font>");
            CommonHelper.RefreshPage();
            CommonHelper.ScrollToEndOfPage();
            totalItems = CommunityPage.Instance.GetTotalNumberItem();


            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();

            // Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.Communities.ToString());

            // Download baseline files before comparing files
            CommunityPage.Instance.DownloadBaseLineFile(EXPORT_XML_MORE_MENU, exportFileName);
            CommunityPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            CommunityPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void A02_B_Assets_Communities_Export_Import_Communities()
        {
            string expectedExportTitle = string.Empty;

            // Step 1: Export XML file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Export XML file and make sure the export file existed.</b></font>");
            CommunityPage.Instance.ExportFile(EXPORT_XML_MORE_MENU, exportFileName, 0, expectedExportTitle);
            //CommunityPage.Instance.CompareExportFile(exportFileName, TableType.XML);


            // Step 1.2: Export CSV file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Export CSV file and make sure the export file existed.</b></font>");
            CommunityPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.COMMUNITY_TITLE);
            //CommunityPage.Instance.CompareExportFile(exportFileName, TableType.CSV);


            // Step 1.3: Export Excel file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Export Excel file and make sure the export file existed.</b></font>");
            CommunityPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.COMMUNITY_TITLE);
            //CommunityPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);


            // Step 2: Open Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open Import page.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities, true, true);
            CommonHelper.SwitchLastestTab();
            CommunityPage.Instance.OpenImportPage(BaseMenuUrls.COMMUNITY_IMPORT_URL);

            // Don't import any file if can't find Community Import grid
            if (BuilderImportPage.Instance.IsImportGridDisplay(COMMUNITY_IMPORT) is false)
                return;


            // Step 2.1: Wrong format import file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Wrong format import file.</b></font>");
            string communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Wrong_Format.csv";
            string expectedErrorMess = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            BuilderImportPage.Instance.ImportInvalidData(COMMUNITY_IMPORT, communityImportFile, expectedErrorMess);


            // Step 2.2: Normal file - All data exist in systems - Import file that exported on step 1.2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Normal file - All data exist in systems - Import file that exported on step 1.2.</b></font>");
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Existing_Community.csv";
            BuilderImportPage.Instance.ImportValidData(COMMUNITY_IMPORT, communityImportFile);


            // Step 2.3: Normal file - New Community not exist in systems
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3: Normal file - New Community not exist in systems.</b></font>");
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_New_Community.csv";
            BuilderImportPage.Instance.ImportValidData(COMMUNITY_IMPORT, communityImportFile);


            // Step 2.4: File with multi data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4: File with multi data.</b></font>");
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Multiple_Communities.csv";
            BuilderImportPage.Instance.ImportValidData(COMMUNITY_IMPORT, communityImportFile);


            // Step 2.5: File without Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.5: File without Data.</b></font>");
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Without_Data.csv";
            BuilderImportPage.Instance.ImportValidData(COMMUNITY_IMPORT, communityImportFile);


            // Step 2.6: File without header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.6: File without header.</b></font>");
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Without_Header.csv";
            expectedErrorMess = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            BuilderImportPage.Instance.ImportInvalidData(COMMUNITY_IMPORT, communityImportFile, expectedErrorMess);


            // Step 2.7: File missing the Transfer Separator Character
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.7: File missing the Transfer Separation Character.</b></font>");
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Missing_Transfer_Separator_Charater.csv";
            expectedErrorMess = "There was an error reading the file on line #2 - Index was outside the bounds of the array.";
            BuilderImportPage.Instance.ImportInvalidData(COMMUNITY_IMPORT, communityImportFile, expectedErrorMess);


            // Step 2.8: Empty file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.8: Empty file.</b></font>");
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Empty_File.csv";
            expectedErrorMess = "The import file is empty.";
            BuilderImportPage.Instance.ImportInvalidData(COMMUNITY_IMPORT, communityImportFile, expectedErrorMess);


            // Step 2.9: File missing the Close Character
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.9: File missing the Close Character.</b></font>");
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Missing_Close_Charater.csv";
            expectedErrorMess = "Line 2 cannot be parsed using the current Delimiters.";
            BuilderImportPage.Instance.ImportInvalidData(COMMUNITY_IMPORT, communityImportFile, expectedErrorMess);


            // Step 2.10: File has the “character” between fields don’t match with the configure
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.10: File has the “character” between fields don’t match with the configure.</b></font>");
            expectedErrorMess = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Invalid_Transfer_Separater_Charater.csv";
            BuilderImportPage.Instance.ImportInvalidData(COMMUNITY_IMPORT, communityImportFile, expectedErrorMess);


            // Step 2.11: Duplicate community name
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.11: Duplicate community name.</b></font>");
            expectedErrorMess = "Failed to import line 2 due to duplicate community found.\r\nFailed to import line 3 due to duplicate community found.";
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Duplicate_Community_Name.csv";
            BuilderImportPage.Instance.ImportInvalidData(COMMUNITY_IMPORT, communityImportFile, expectedErrorMess);


            // Step 2.12: Duplicate community code:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.12: Duplicate community code.</b></font>");
            expectedErrorMess = "There was an error reading the file on line #3 - Community Code 001 is a duplicate.";
            communityImportFile = "\\DataInputFiles\\Assets\\Community\\Pipeline_Communities_Duplicate_Community_Code.csv";
            BuilderImportPage.Instance.ImportInvalidData(COMMUNITY_IMPORT, communityImportFile, expectedErrorMess);

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }
        #endregion


        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Navigate to Community page.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Delete community that's going to be imported on step 2.3
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Delete community that's going to be imported on step 2.3.</b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NEW_COMMUNITY_NAME);
            if (CommunityPage.Instance.IsItemInGrid("Name", NEW_COMMUNITY_NAME) is true)
                CommunityPage.Instance.DeleteCommunity(NEW_COMMUNITY_NAME);
        }
    }
}
