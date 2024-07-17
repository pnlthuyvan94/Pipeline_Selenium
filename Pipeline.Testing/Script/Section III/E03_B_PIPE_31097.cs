using NUnit.Framework;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Purchasing.CostCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_III
{
    public class E03_B_PIPE_31097 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private CostCodeData newCostCodeData;
        private const string NewCostCodeDataName = "RT_QA_New_CostCode_E03B";
        private const string NewCostCodeDataDescription = "RT_QA_New_CostCode_E03B";

        private string exportFileName;
        private const string ExportCsv = "Export CSV";
        private const string ExportExcel = "Export Excel";

        private const string CostCodesImport = "Cost Code Import";

        [SetUp]
        public void Setup()
        {
            //Initialize test data 
            newCostCodeData = new CostCodeData()
            {
                Name = NewCostCodeDataName,
                Description = NewCostCodeDataDescription
            };
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);
            CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCodeData.Name);
            if (CostCodePage.Instance.IsItemInGrid("Name", newCostCodeData.Name) is false)
            {
                CostCodePage.Instance.AddCostCodes(newCostCodeData, "Cost Code " + newCostCodeData.Name + " created successfully!", false);
            }
        }

        [Test]
        public void E03_B_Purchasing_Cost_Codes_Export_Import()
        {
            //Navigate to Main Menu > PURCHASING > COST CODES
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0 Navigate to Main Menu > PURCHASING > COST CODES.</b></font>");
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);

            //Click on Utilities icon in the upper right part of the page and select Export CSV option from the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Click Utilities button.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Export Cost Codes.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Export CSV.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName("")}CostCodes";
            VendorProductPage.Instance.ExportFile(ExportCsv, exportFileName, 0, ExportTitleFileConstant.COST_CODES);
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Export Excel.</b></font>");
            VendorProductPage.Instance.ExportFile(ExportExcel, exportFileName, 0, ExportTitleFileConstant.COST_CODES);
            CommonHelper.RefreshPage();

            //Add a new entry on the exported .csv file and import
            string importFile = "";
            string expectedErrorMessage = "";
            PurchasingImportPage.Instance.OpenImportPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Import Cost Codes.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Import Cost Code.</b></font>");
            if (PurchasingImportPage.Instance.IsImportLabelDisplay(CostCodesImport) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {CostCodesImport} grid view to import new Cost Code.</font>");

            importFile = "\\DataInputFiles\\Purchasing\\CostCodesImport\\Pipeline_CostCodes.csv";
            ImportValidData(CostCodesImport, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.1:  Import Cost Code Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\CostCodesImport\\Pipeline_CostCodes.txt";
            expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(CostCodesImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.2:  Import Cost Code format import file.</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\CostCodesImport\\Pipeline_CostCodes_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(CostCodesImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.3:  Import Cost Code File without header.</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\CostCodesImport\\Pipeline_CostCodes_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(CostCodesImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.4:  Import Cost Code File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\CostCodesImport\\Pipeline_CostCodes_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
            ImportInvalidData(CostCodesImport, importFile, expectedErrorMessage);

            //Navigate to Main Menu > PURCHASING > COST CODES
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);
            CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCodeData.Name);
            if (CostCodePage.Instance.IsItemInGrid("Name", newCostCodeData.Name) is true)
            {
                ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Imported Cost Codes show in grid.<b></font>");
                System.Threading.Thread.Sleep(2000);
                CommonHelper.CaptureScreen();
            }
        }


        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: Remove Cost Code.</b></font>");
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);
            CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCodeData.Name);
            if (CostCodePage.Instance.IsItemInGrid("Name", newCostCodeData.Name) is true)
            {
                CostCodePage.Instance.DeleteCostCodesByName(newCostCodeData.Name);
            }
        }

        private void ImportValidData(string importGridTitle, string fullFilePath)
        {
            string actualMessage = PurchasingImportPage.Instance.ImportFile(importGridTitle, fullFilePath);

            string expectedMessage = "Import complete.";
            if (expectedMessage.ToLower().Contains(actualMessage.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The valid file was NOT imported." +
                    $"<br>The toast message is: {actualMessage}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid file was imported successfully and the toast message indicated success.</b></font>");

        }

        private void ImportInvalidData(string importGridTitlte, string fullFilePath, string expectedFailedData)
        {
            string actualMessage = PurchasingImportPage.Instance.ImportFile(importGridTitlte, fullFilePath);

            if (actualMessage.ToLower().Contains(expectedFailedData.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file failed to import and the toast message indicated failure.</b></font>");

        }
    }
}
