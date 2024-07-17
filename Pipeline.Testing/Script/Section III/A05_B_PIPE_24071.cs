using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Import;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_III
{
    class A05_B_PIPE_24071 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        private const string EXPORT_XML_MORE_MENU = "XML";
        private const string EXPORT_CSV_MORE_MENU = "CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Excel";
        private const string OPTION_NAME = "RT_04";
        private const string OPTION_NAME2 = "RT_III_1";
        private const string OPTION_NUMBER1 = "24072";
        private const string OPTION_NUMBER2 = "24073";


        OptionData option;
        [SetUp]
        public void CreateTestData()
        {

            option = new OptionData()
            {
                Name = "QA_RT_Option_24071_Automation",
                Number = "24071",
            };
        }
        [Test]
        [Category("Section_III")]
        public void A05_B_Assets_Options_Export_Import_Options()
        {
            //I.Export
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.Export.</font><b>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            CommonHelper.ScrollToEndOfPage();
            int totalItems = OptionPage.Instance.GetTotalNumberItem();

            //Verify if the following selections are available in the dropdown list:Export XML, Export CSV, Export Excel, Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify if the following selections are available in the dropdown list:Export XML, Export CSV, Export Excel, Import.</font><b>");
            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();
            // Get export file name
            string exportFileName = CommonHelper.GetExportFileName(ExportType.Options.ToString());

            //1.Options to XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.1: Options to XML.</font><b>");
            OptionPage.Instance.DownloadBaseLineFile(EXPORT_XML_MORE_MENU, exportFileName);
            OptionPage.Instance.ExportFile(EXPORT_XML_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.OPTION_TITLE);
            //2.Options to CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2.Options to CSV.</font><b>");
            OptionPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            OptionPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.OPTION_TITLE);
            
            //3.Options to excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.3.Options to excel.</font><b>");
            OptionPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
            OptionPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.OPTION_TITLE);

            //II. Import File:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II. Import File:.</font><b>");
            //1.Full header with no data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.1 Full header with no data</font><b>");
            OptionPage.Instance.ImportExporFromMoreMenu("Import");
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_IMPORT} grid view to import new Options.</font>");

            string importFile = $"\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_1.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);

            //2.Name and code don't exist in the system.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.2.Name and code don't exist in the system.</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, option.Number);
            if (OptionPage.Instance.IsItemInGrid("Number", option.Number) is false)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Option with number {option.Number} is not existed in system");
            }


            OptionPage.Instance.ImportExporFromMoreMenu("Import");
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_2.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, option.Number);
            if (OptionPage.Instance.IsItemInGrid("Number", option.Number) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Option with number {option.Number} was existed in system After Import file successful");
            }

            //3.Name exists in the system with "Number" field is no data ( Without Code)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.3 Name exists in the system with Number field is no data ( Without Code)</font><b>");

            OptionPage.Instance.ImportExporFromMoreMenu("Import");
            string expectedErrorMess = "Option Numbers is Null or Empty on Line #2 .";
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_3.csv";
            BuilderImportPage.Instance.ImportInvalidData(ImportGridTitle.OPTION_IMPORT, importFile, expectedErrorMess);

            //4.Name doesn't exist in the system and "Number" field exists in the system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.4.Name doesn't exist in the system and Number field exists in the system</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME);
            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME) is false)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Option with Name {OPTION_NAME} is not in system");
            }
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, option.Number);
            if (OptionPage.Instance.IsItemInGrid("Number", option.Number) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Option with number {option.Number} was existed in system");
            }
            OptionPage.Instance.ImportExporFromMoreMenu("Import");
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_4.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);

            //5.Without Options Name.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.5 Without Options Name.</font><b>");
             expectedErrorMess = "Option Name on Line #2 is not an acceptable Option Name.";
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_5.csv";
            BuilderImportPage.Instance.ImportInvalidData(ImportGridTitle.OPTION_IMPORT, importFile, expectedErrorMess);

            //6.Without header row in the file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.6 Without header row in the file</font><b>");
            expectedErrorMess = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_6.csv";
            BuilderImportPage.Instance.ImportInvalidData(ImportGridTitle.OPTION_IMPORT, importFile, expectedErrorMess);
            
            //7.Empty file:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.7 Empty file:</font><b>");
            expectedErrorMess = "Object reference not set to an instance of an object.";
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_7.csv";
            BuilderImportPage.Instance.ImportInvalidData(ImportGridTitle.OPTION_IMPORT, importFile, expectedErrorMess);

            //8. Missing any field of the header
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.8. Missing any field of the header</font><b>");
            expectedErrorMess = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_8.csv";
            BuilderImportPage.Instance.ImportInvalidData(ImportGridTitle.OPTION_IMPORT, importFile,expectedErrorMess);

            //9.Missing the “,” character between fields
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.9 Missing the “,” character between fields</font><b>");
            expectedErrorMess = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_9.csv";
            BuilderImportPage.Instance.ImportInvalidData(ImportGridTitle.OPTION_IMPORT, importFile, expectedErrorMess);

            //10.The “character” between fields don’t match with the configure.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.10 The “character” between fields don’t match with the configure.</font><b>");
            expectedErrorMess = "Line 2 cannot be parsed using the current Delimiters.";
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_10.csv";
            BuilderImportPage.Instance.ImportInvalidData(ImportGridTitle.OPTION_IMPORT, importFile, expectedErrorMess);

            //11.Import duplicate name on the file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.11.Import duplicate name on the file</font><b>");
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_11.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, OPTION_NUMBER1);
            if (OptionPage.Instance.IsItemInGrid("Number", OPTION_NUMBER1) && OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import file successfuly with duplicate name Option with Number{OPTION_NUMBER1}");
            }

            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME);
            if (OptionPage.Instance.IsItemInGrid("Number", OPTION_NUMBER2) & OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import file successfuly with duplicate name Option with Number{OPTION_NUMBER2}");
            }
       

            //12. File missing the Close Character
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II.12. File missing the Close Character</font><b>");
            OptionPage.Instance.ImportExporFromMoreMenu("Import");
            expectedErrorMess = "Line 2 cannot be parsed using the current Delimiters.";
            importFile = "\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_II_12.csv";
            BuilderImportPage.Instance.ImportInvalidData(ImportGridTitle.OPTION_IMPORT, importFile, expectedErrorMess);
            
            //III.1 Has Options got same number but different name with Options already in the system has name and number.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step III.1 Has Options got same number but different name with Options already in the system has name and number.</font><b>");
            OptionPage.Instance.ImportExporFromMoreMenu("Import");
            importFile = $"\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_III_1.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, OPTION_NUMBER1);
            if (OptionPage.Instance.IsItemInGrid("Number", OPTION_NUMBER1) && OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME2) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import file successfuly with Has Options got same number but different name with Options");
            }

            //III.2 Has Options got same number and name with Options already in the system has name and number.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step III.2 Has Options got same number and name with Options already in the system has name and number.</font><b>");
            OptionPage.Instance.ImportExporFromMoreMenu("Import");
            importFile = $"\\DataInputFiles\\Import\\PIPE_24071\\Pipeline_Options_III_2.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, OPTION_NUMBER1);
            if (OptionPage.Instance.IsItemInGrid("Number", OPTION_NUMBER1) && OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME2) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import file successfuly with Has Options got same number but different name with Options");
            }
        }

        [TearDown]
        public void CleanUpData()
        {
            //Delete Data
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, option.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", option.Name))
            {
                OptionPage.Instance.DeleteItemInGrid("Name", option.Name);
                string successfulMess = $"Option {option.Name} deleted successfully!";
                string actualResults = OptionPage.Instance.GetLastestToastMessage();
                if (successfulMess.Equals(actualResults))
                {
                    ExtentReportsHelper.LogPass("Option deleted successfully!");
                    OptionPage.Instance.CloseToastMessage();
                }
                else if (!string.IsNullOrEmpty(actualResults))
                {
                    ExtentReportsHelper.LogFail("Option could not be deleted!");
                }
                else
                {
                    ExtentReportsHelper.LogFail("Option deletion message was not as expected: " + actualResults);
                }
            }

            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, OPTION_NUMBER1);
            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME))
            {
                OptionPage.Instance.DeleteItemInGrid("Name", OPTION_NAME);
            }
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, OPTION_NUMBER2);
            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME))
            {
                OptionPage.Instance.DeleteItemInGrid("Name", OPTION_NAME);
            }

            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, OPTION_NUMBER1);
            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME2))
            {
                OptionPage.Instance.DeleteItemInGrid("Name", OPTION_NAME2);
            }
        }
    }
}
