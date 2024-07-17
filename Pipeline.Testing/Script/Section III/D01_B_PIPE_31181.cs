using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.Import;
using Pipeline.Testing.Pages.Import;
using System.Threading;

namespace Pipeline.Testing.Script.Section_III
{
    public class D01_B_PIPE_31181 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private VendorData vendorData;
        private const string vendorDataName = "RT_QA_New_Vendor_D01B";
        private const string vendorDataCode = "D01B";

        private string exportFileName;
        private const string ExportCsv = "Export CSV";

        private const string VendorImport = "Vendors Import";
        private int totalVendorsOnUI;
        private readonly string importFilePath = "\\DataInputFiles\\Costing\\VendorImport";

        [SetUp]
        public void Setup()
        {
            //Initialize test data 
            vendorData = new VendorData()
            {
                Name = vendorDataName,
                Code = vendorDataCode
            };

            //Navigate to Main Menu > Costing > Vendors.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Navigate to Main Menu > Costing > Vendors.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);

            //If vendorData from SetUp is already in the grid, delete it
            DeleteVendor(vendorData.Name);

            //Get the total number of vendors on the UI
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Get the total # of Vendors in the UI.</b></font>");
            totalVendorsOnUI = GetTotalVendorsOnUI();
        }

        [Test]
        public void D01_b_Costing_Vendors_ImportExport()
        {
            //Click on Utilities icon in the upper right part of the page and select Export CSV option from the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1 Click Utilities button.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Export Vendor.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Export CSV.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName("")}Vendors";
            Thread.Sleep(5000);

            //Export the vendors
            VendorPage.Instance.ExportFile(ExportCsv, exportFileName, totalVendorsOnUI, ExportTitleFileConstant.VENDOR);
            CommonHelper.RefreshPage();

            //Add a new entry on the exported .csv file and import
            string importFile = "";
            string expectedErrorMessage = "";
            CostingImportPage.Instance.OpenImportPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Import Vendor.</b></font>");
            if (CostingImportPage.Instance.IsImportLabelDisplay(VendorImport) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {VendorImport} grid view to import new Vendor.</font>");

            //Check if valid import works
            importFile = $"{importFilePath}\\Pipeline_Vendor.csv";
            VendorsImportPage.Instance.ImportValidData(VendorImport, importFile);

            //Navigate to Main Menu > Costing > Vendors & make sure imported vendor is shown in grid
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Imported Vendor show in grid.<b></font>");
                System.Threading.Thread.Sleep(2000);
                CommonHelper.CaptureScreen();
                totalVendorsOnUI += 1; //Increment totalItemsBeforeImport to account for the added item
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Imported Vendor not shown in grid.</font>");
            }

            //Navigate back to import page
            CostingImportPage.Instance.OpenImportPage();

            //Check if duplicate import causes an error
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Import Vendor Duplicate.</b></font>");
            importFile = $"{importFilePath}\\Pipeline_Vendor.csv";
            expectedErrorMessage = "Row 2: Vendor Code: D01B already exists.";
            VendorsImportPage.Instance.ImportInvalidData(VendorImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Import Vendor wrong file type.</b></font>");
            importFile = $"{importFilePath}\\Pipeline_Vendor.txt";
            expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            VendorsImportPage.Instance.ImportInvalidData(VendorImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Import Vendor wrong input file format.</b></font>");
            importFile = $"{importFilePath}\\Pipeline_Vendor_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            VendorsImportPage.Instance.ImportInvalidData(VendorImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Import Vendor file without header.</b></font>");
            importFile = $"{importFilePath}\\Pipeline_Vendor_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            VendorsImportPage.Instance.ImportInvalidData(VendorImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Import Vendor file has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = $"{importFilePath}\\Pipeline_Vendor_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
            VendorsImportPage.Instance.ImportInvalidData(VendorImport, importFile, expectedErrorMessage);

            //Check if any vendors were added from failed imports
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Check if any vendors were added from failed imports.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);

            //Get the total number of vendors on the UI
            int totalVendorsAfterInvalidImport = GetTotalVendorsOnUI();
            if (totalVendorsAfterInvalidImport != (totalVendorsOnUI))
            {
                ExtentReportsHelper.LogFail(null, $"<font color ='red'><b>Vendor grid data was changed after importing invalid data. Before invalid import: {totalVendorsOnUI} after invalid import: {totalVendorsAfterInvalidImport}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendor grid data was unchanged after importing invalid data.</b></font>");
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: Remove Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            DeleteVendor(vendorData.Name);
        }

        private int GetTotalVendorsOnUI()
        {
            CommonHelper.ScrollToEndOfPage();
            Thread.Sleep(5000);
            int totalVendorsOnUI = VendorPage.Instance.GetTotalNumberItem();
            CommonHelper.ScrollToBeginOfPage();
            return totalVendorsOnUI;
        }

        private void DeleteVendor(string vendorName)
        {
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, vendorName);
            System.Threading.Thread.Sleep(5000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorName) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", vendorName);
            }
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
        }
    } 
}
