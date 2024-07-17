using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionGroup;

namespace Pipeline.Testing.Script.Demo
{
    public partial class OptionGroup_Export_BeyondCompare : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_Demo);
        }

        private const string EXPORT_CSV_MORE_MENU = "Export CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";

        [SetUp]
        public void SetUp()
        {
            // Do nothing
        }

        #region"Test case"
        [Test]
        [Category("Section_Demo")]
        public void Demo_OptionGroup_Export_BeyondCompare()
        {
            string exportFileName = CommonHelper.GetExportFileName(ExportType.OptionGroups.ToString());

            // Step 1: Navigate to Option Group page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Option Group page.</b></font>");
            OptionGroupPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionGroups);

            // Step 2: Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Step 2: Get the total items on the UI.</b></font>");
            CommonHelper.ScrollToEndOfPage();
            int totalItems = OptionGroupPage.Instance.GetTotalNumberItem();

            
            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();

            // Download baseline files before comparing files
            OptionGroupPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            OptionGroupPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);


            // Step 3: Export CSV file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Export CSV file and make sure the export file existed.</b></font>");
            OptionGroupPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems);
            //OptionGroupPage.Instance.CompareExportFile(exportFileName, TableType.CSV);


            // Step 4: Export Excel file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Export Excel file and make sure the export file existed.</b></font>");
            OptionGroupPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems);
            //OptionGroupPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);
        }
        #endregion

        [TearDown]
        public void ClearData()
        {
           // Do nothing
        }
    }
}
