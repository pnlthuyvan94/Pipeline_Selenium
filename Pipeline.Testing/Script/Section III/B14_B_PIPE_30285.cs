using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;


namespace Pipeline.Testing.Script.Section_III
{
    class B14_B_PIPE_30285 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private const string EXPORT_CSV_MORE_MENU = "Export Selected Spec Sets with Groups CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Selected Spec Sets with Groups Excel";

        private const string EXPORT_GROUPS_CSV_MORE_MENU = "Export Selected Spec Set Group Information CSV";
        private const string EXPORT_GROUPS_EXCEL_MORE_MENU = "Export Selected Spec Set Group Information Excel";

        private const string IMPORT_TITLE_MORE_MENU = "Spec Set Group / Spec Set Creation Import";

        readonly string SPECSET_GROUP_IMPORT_NAME = "Door Hardware Type_New Specset Group";

        readonly string SPECSET_GROUP_IMPORT_NAME_4 = "RT2023";

        readonly string SPECSET_GROUP_IMPORT_NAME_5 = "Door Hardware Type";

        readonly string SPECSET_GROUP_IMPORT_NAME_11 = "RT2023";

        private string exportFileName;

        [Test]
        [Category("Section_III")]
        public void B14_B_Estimating_SpecSetGroups_Export_Import_SpecSetGroups()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Make sure current transfer seperation character is '*'</font><b>");
            //Make sure current transfer seperation character is ''
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            // Step I.1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/ProductAssemblies/SpecSets/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.1: navigate to Spec Set Page.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);

            // Get export file name
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Get export file name.</b></font>");
            exportFileName = CommonHelper.GetExportFileName(ExportType.SpecsetGroups.ToString());

            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();

            /******************************** Export SpecSet Group *************************************/
            // Step I.2: Export with Checked on the checkbox of the Specset group list page.
            // Step I.2.1 : Export Selected Spec Sets with Group CSV.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2: Export with Checked on the checkbox of the Specset group list page.</b></font>");
            // Download baseline files before comparing files
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2.1: Download baseline files before comparing files.</b></font>");
            SpecSetPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            SpecSetPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.SPECSETGROUP_TITLE);
            //SpecSetPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            // Step I.2.2: Export Selected Spec Sets with Group EXCEL.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2.2: Export Selected Spec Sets with Group EXCEL.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2.3: Download baseline files before comparing files.</b></font>");
            SpecSetPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
            SpecSetPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.SPECSETGROUP_TITLE);
            //SpecSetPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);

            // Step I.2.3: Export Selected Spec Sets Group Information with Group CSV.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2.3: Export Selected Spec Sets Group Information with Group CSV.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2.4: Download baseline files before comparing files.</b></font>");
            SpecSetPage.Instance.DownloadBaseLineFile(EXPORT_GROUPS_CSV_MORE_MENU, "Pipeline_SpecSetProducts_AllSpecSetGroups");
            SpecSetPage.Instance.ExportFile(EXPORT_GROUPS_CSV_MORE_MENU, "Pipeline_SpecSetProducts_AllSpecSetGroups", 0, ExportTitleFileConstant.SPECSETPRODUCTGROUP_TITLE);
            //SpecSetPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2.4: Export Selected Spec Sets Group Information with Group EXCEL.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2.5: Download baseline files before comparing files.</b></font>");
            SpecSetPage.Instance.DownloadBaseLineFile(EXPORT_GROUPS_EXCEL_MORE_MENU, "Pipeline_SpecSetProducts_AllSpecSetGroups");
            SpecSetPage.Instance.ExportFile(EXPORT_GROUPS_EXCEL_MORE_MENU, "Pipeline_SpecSetProducts_AllSpecSetGroups", 0, ExportTitleFileConstant.SPECSETPRODUCTGROUP_TITLE);
            //SpecSetPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);
            
            //II.Spec Set Group / Spec Set Creation Import work correctly
            //1.Verify this import will Add any new Spec Set Groups and leave existing groups in place. No Spec Set Groups are deleted.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.1.Verify this import will Add any new Spec Set Groups and leave existing groups in place. No Spec Set Groups are deleted.</b></font>");
            // This is the same name as csv import file
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            string productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_1_1.csv";
            SpecSetPage.Instance.ImportFile("SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SPECSET_GROUP_IMPORT_NAME) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME} is exited in Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME} isn't exited in Grid</font>");
            }

            //2.Verify the import will Add Spec Sets to the Spec Set Groups. If a Spec Set is not included in the import but its Spec Set Group is, that Spec Set will be deleted.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.2:Verify the import will Add Spec Sets to the Spec Set Groups. If a Spec Set is not included in the import but its Spec Set Group is, that Spec Set will be deleted.</b></font>");
            var listSpecSet = new List<string>() { "QA_Lever", "Knob" };
            var newlistSpecSet = new List<string>() { "QA_Lever123", "Knob", "New Specset" };
            SpecSetPage.Instance.VerifySpecSetInSpecSetGroup(SPECSET_GROUP_IMPORT_NAME, listSpecSet);
            // This is the same name as csv import file
            CommonHelper.ScrollToBeginOfPage();
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_2.csv";
            SpecSetPage.Instance.ImportFile("SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SPECSET_GROUP_IMPORT_NAME) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME} is exited in Grid</b></font>");

                SpecSetPage.Instance.VerifySpecSetInSpecSetGroup(SPECSET_GROUP_IMPORT_NAME, newlistSpecSet);
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME} isn't exited in Grid</font>");
            }

            //3.Import file has empty value on “Spec Set Default“ and this record has “Default Group” column is TRUE
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.3.Import file has empty value on “Spec Set Default“ and this record has “Default Group” column is TRUE</b></font>");
            CommonHelper.ScrollToBeginOfPage();
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_3.csv";
            SpecSetPage.Instance.ImportErrorFile("Spec Set Default On Line #2 Is Not an acceptable Spec Set Default.", "SpecSets", "Spec Set Group / Spec Set Creation Import", productImportFile);

            //4.Import file with empty value “Default Group” column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.4.Import file with empty value “Default Group” column.</b></font>");
            listSpecSet = new List<string>() {  "Knob" };
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            CommonHelper.ScrollToBeginOfPage();
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_4.csv";
            SpecSetPage.Instance.ImportFile("SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME_4);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SPECSET_GROUP_IMPORT_NAME_4) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME_4} is exited in Grid</b></font>");
                SpecSetPage.Instance.VerifySpecSetInSpecSetGroup(SPECSET_GROUP_IMPORT_NAME_4, listSpecSet);

            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME_4} isn't exited in Grid</font>");
            }

            //5.Import file has duplicate value on “Spec Set“ column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.5.Import file has duplicate value on “Spec Set“ column</b></font>");
            listSpecSet = new List<string>() { "Level","Knob" };
            CommonHelper.ScrollToBeginOfPage();
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_5.csv";
            SpecSetPage.Instance.ImportErrorFile("Spec Sets are duplicated. Please check your file Line #3.", "SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME_5);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SPECSET_GROUP_IMPORT_NAME_5) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME_5} is exited in Grid</b></font>");
                SpecSetPage.Instance.VerifySpecSetInSpecSetGroup(SPECSET_GROUP_IMPORT_NAME_5, listSpecSet);
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME_5} isn't exited in Grid</font>");
            }

            //6.Without header row in the file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.6.Without header row in the file</b></font>");
            CommonHelper.ScrollToBeginOfPage();
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_6.csv";
            SpecSetPage.Instance.ImportErrorFile("Failed to import file due to an error in the data format. Column headers do not match expected values.", "SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);

            //7.Empty file:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.7.Empty file</b></font>");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_7.csv";
            SpecSetPage.Instance.ImportErrorFile(string.Empty,"SpecSets", "Spec Set Group / Spec Set Creation Import", productImportFile);


            //8.Missing any field of the header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.8.Missing any field of the header</b></font>");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_8.csv";
            SpecSetPage.Instance.ImportErrorFile("Failed to import file due to an error in the data format. Column headers do not match expected values.", "SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);

            //9.Missing the “,” character between fields
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.9.Missing the “,” character between fields.</b></font>");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_9.csv";
            SpecSetPage.Instance.ImportErrorFile("Failed to import file due to an error in the data format. Column headers do not match expected values.", "SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);

            //10.The “character” between fields don’t match with the configure.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.10.The “character” between fields don’t match with the configure.</font><b>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Make sure current transfer seperation character is '*'</font><b>");
            //Make sure current transfer seperation character is ''
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            seperationCharacter = "-";
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_10.csv";
            SpecSetPage.Instance.ImportErrorFile("Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.", "SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);

            //11.Import file without has value on column: Spec Set Default, SpecSet with “Default Group” is FALSE
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.11.Import file without has value on column: Spec Set Default, SpecSet with “Default Group” is FALSE.</font><b>");
            listSpecSet = new List<string>() { "Level","Knob" };
            newlistSpecSet = new List<string>() { string.Empty };
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME_11);
            SpecSetPage.Instance.VerifySpecSetInSpecSetGroup(SPECSET_GROUP_IMPORT_NAME_11, listSpecSet);
            CommonHelper.ScrollToBeginOfPage();
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_11.csv";
            SpecSetPage.Instance.ImportFile("SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPECSET_GROUP_IMPORT_NAME_11);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SPECSET_GROUP_IMPORT_NAME_11) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME_11} is exited in Grid</b></font>");

                SpecSetPage.Instance.VerifySpecSetInSpecSetGroup(SPECSET_GROUP_IMPORT_NAME_11, newlistSpecSet);
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The SpecSet Group Data with Name {SPECSET_GROUP_IMPORT_NAME_11} isn't exited in Grid</font>");
            }

            //12.Import file with empty value “Specset Group” column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.12.Import file with empty value “Specset Group” column.</font><b>");
            CommonHelper.ScrollToBeginOfPage();
            SpecSetPage.Instance.ImportExporOnSpecSetGroup("Import");
            productImportFile = "\\DataInputFiles\\Import\\PIPE_30285\\ImportSpecSet\\Pipeline_SpecSetGroups_12.csv";
            SpecSetPage.Instance.ImportErrorFile("Spec Set Group On Line #2 Is Not an acceptable Spec Set Group.", "SpecSets", IMPORT_TITLE_MORE_MENU, productImportFile);
            
        }

    }

}
