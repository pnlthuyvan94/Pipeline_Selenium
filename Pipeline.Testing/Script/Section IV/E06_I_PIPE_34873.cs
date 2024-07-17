using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Purchasing.Trades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E06_I_PIPE_34873 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTrade_E06I";
        private string exportFileName = string.Empty;

        private const string TradeImport = "Trade Import";
        [Test]
        public void E06_I_Purchasing_Trades_Export_Import()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Purchasing Trades Landing page.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Open Export Modal.</b></font>");
            TradesPage.Instance.OpenTradesExportModal();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Export Trades to CSV File.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.Trades.ToString())}";
            TradesPage.Instance.TradesExportModal.SelectExportOption("CSV");
            TradesPage.Instance.TradesExportModal.ContinueExport("CSV", exportFileName, ExportTitleFileConstant.TRADES_TITLE);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Export Trades to Excel File.</b></font>");
            TradesPage.Instance.TradesExportModal.SelectExportOption("Excel");
            TradesPage.Instance.TradesExportModal.ContinueExport("Excel", exportFileName, ExportTitleFileConstant.TRADES_TITLE);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Export Trades To Building Phase to CSV File.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.TradesToBuildingPhases.ToString())}";
            TradesPage.Instance.TradesExportModal.SelectExportOption("CSV All Trades to Building Phase");
            TradesPage.Instance.TradesExportModal.ContinueExport("CSV All Trades to Building Phase", exportFileName, ExportTitleFileConstant.TRADE_TO_PHASE_TITLE);
            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Export Trades To Building Phase to Excel File.</b></font>");
            TradesPage.Instance.TradesExportModal.SelectExportOption("Excel All Trades to Building Phase");
            TradesPage.Instance.TradesExportModal.ContinueExport("Excel All Trades to Building Phase", exportFileName, ExportTitleFileConstant.TRADE_TO_PHASE_TITLE);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Export Trades To Vendor to CSV File.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.TradesToVendors.ToString())}";
            TradesPage.Instance.TradesExportModal.SelectExportOption("CSV All Trades to Vendor");
            TradesPage.Instance.TradesExportModal.ContinueExport("CSV All Trades to Vendor", exportFileName, ExportTitleFileConstant.TRADE_TO_VENDOR_TITLE);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Export Trades To Vendor to Excell File.</b></font>");
            TradesPage.Instance.TradesExportModal.SelectExportOption("Excel All Trades to Vendor");
            TradesPage.Instance.TradesExportModal.ContinueExport("Excel All Trades to Vendor", exportFileName, ExportTitleFileConstant.TRADE_TO_VENDOR_TITLE);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Import Trades with valid data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.TRADES_IMPORT_URL);
            if (TradesImportPage.Instance.IsImportLabelDisplay(TradeImport) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {TradeImport} grid view to import new trades.</font>");

            string importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\Trades\\Pipeline_Trades.csv";
            ImportValidData(TradeImport, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10:  Import Trades with Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\Trades\\Pipeline_Trades.txt";
            string expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(TradeImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.11:  Import Trades with Wrong format import file.</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\Trades\\Pipeline_Trades_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(TradeImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.12:  Import Trades File without header.</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\Trades\\Pipeline_Trades_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(TradeImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.13:  Import Trades File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\Trades\\Pipeline_Trades_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
            ImportInvalidData(TradeImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.14:  Import Trades File that has “invalid special character” .</b></font>");
            importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\Trades\\Pipeline_Trades_With_InvalidSpecialChracters.csv";
            expectedErrorMessage = "Trade Import does not allow special characters.";
            ImportInvalidData(TradeImport, importFile, expectedErrorMessage);
        }

        private void ImportValidData(string importGridTitle, string fullFilePath)
        {
            string actualMessage = TradesImportPage.Instance.ImportFile(importGridTitle, fullFilePath);

            string expectedMessage = "Import complete.";
            if (expectedMessage.ToLower().Contains(actualMessage.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The valid file was NOT imported." +
                    $"<br>The toast message is: {actualMessage}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid file was imported successfully and the toast message indicated success.</b></font>");

        }

        private void ImportInvalidData(string importGridTitlte, string fullFilePath, string expectedFailedData)
        {
            string actualMessage = TradesImportPage.Instance.ImportFile(importGridTitlte, fullFilePath);

            if (actualMessage.ToLower().Contains(expectedFailedData.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file failed to import and the toast message indicated failure.</b></font>");

        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Tear down test data.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Delete Building Trades.</b></font>");

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                TradesPage.Instance.DeleteItemInGrid("Trade", NewBuildingTradeName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }
        }

    }
}
