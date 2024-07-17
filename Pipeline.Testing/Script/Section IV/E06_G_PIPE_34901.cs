using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E06_G_PIPE_34901 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTrade_E06G";
        private const string NewBuildingTradeCode = "E06G";

        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_E16G";
        private const string NewVendorCode = "E16G";

        private VendorData newVendor2;
        private const string NewVendorName2 = "RT_QA_New_Vendor_E26G";
        private const string NewVendorCode2 = "E26G";

        private VendorData newVendor3;
        private const string NewVendorName3 = "RT_QA_New_Vendor_E36G";
        private const string NewVendorCode3 = "E36G";

        private string exportFileName;
        private const string ExportCsvMenu = "Export CSV";
        private const string ExportExcelMenu = "Export Excel";
        private const string TradeToVendorImport = "Trade to Vendor Import";

        [SetUp]
        public void Setup()
        {
            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
            };
            newVendor2 = new VendorData()
            {
                Name = NewVendorName2,
                Code = NewVendorCode2
            };
            newVendor3 = new VendorData()
            {
                Name = NewVendorName3,
                Code = NewVendorCode3
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add first vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add second vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName2);
            System.Threading.Thread.Sleep(2000);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName2))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor2);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add third vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName3);
            System.Threading.Thread.Sleep(2000);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName3))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor3);
            }
        }
        [Test]
        public void E06_G_Purchasing_Trades_Vendors_Export_Import()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Create new Building Trade test data.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);

            TradesData _trade = new TradesData()
            {
                Code = NewBuildingTradeCode,
                TradeName = NewBuildingTradeName,
                TradeDescription = NewBuildingTradeName,
                Vendor = "",
                BuildingPhases = "",
                SchedulingTasks = "",
                IsBuilderVendor = false
            };
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);           
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(_trade, false, false);
            }
            CommonHelper.RefreshPage();
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                ExtentReportsHelper.LogPass(null, $"Building Trade " + NewBuildingTradeName + " is displayed on the grid.");
                TradesPage.Instance.SelectItemInGrid("Trade", NewBuildingTradeName);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Navigate to Trade to Vendors page.</b></font>");

                TradeDetailPage.Instance.LeftMenuNavigation("Vendors", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Add new Vendors to Trade.</b></font>");
                TradeVendorPage.Instance.ShowAddVendorToTradeModal();
                if (TradeVendorPage.Instance.AddVendorToTradeModal.IsModalDisplayed)
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Add Vendor to Trade modal is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='green'><b>Add Vendor to Trade modal is not displayed.</b></font>");
                string[] vendorsList = { NewVendorName, NewVendorName2, NewVendorName3 };
                TradeVendorPage.Instance.AddVendorToTradeModal.SelectVendors(vendorsList);
                TradeVendorPage.Instance.AddVendorToTradeModal.Save();
                System.Threading.Thread.Sleep(1000);
                CommonHelper.RefreshPage();


                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Export Trades To Vendor to CSV File.</b></font>");
                exportFileName = $"{CommonHelper.GetExportFileName(ExportType.TradesToVendors.ToString())}";
                TradeVendorPage.Instance.ExportFile(ExportCsvMenu, exportFileName, 0, ExportTitleFileConstant.TRADE_TO_VENDOR_TITLE);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Export Trades To Vendor to Excel File.</b></font>");
                TradeVendorPage.Instance.ExportFile(ExportExcelMenu, exportFileName, 0, ExportTitleFileConstant.TRADE_TO_VENDOR_TITLE);
                CommonHelper.RefreshPage();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Import Trades To Vendor.</b></font>");
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.TRADES_IMPORT_URL);
                if (TradesImportPage.Instance.IsImportLabelDisplay(TradeToVendorImport) is false)
                    ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {TradeToVendorImport} grid view to import new trades to phase.</font>");

                string importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors.csv";
                ImportValidData(TradeToVendorImport, importFile);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7:  Import Trades To Vendor Wrong file type.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors.txt";
                string expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
                ImportInvalidData(TradeToVendorImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8:  Import Trades To Vendor Wrong format import file.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors_Wrong_Format.csv";
                expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
                ImportInvalidData(TradeToVendorImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9:  Import Trades To Vendor File without header.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors_No_Header.csv";
                expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
                ImportInvalidData(TradeToVendorImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10:  Import Trades To Vendor File has the “character” between fields don’t match with the configuration.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors_Invalid_Separator.csv";
                expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
                ImportInvalidData(TradeToVendorImport, importFile, expectedErrorMessage);
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color = 'red'>Building Trade  {NewBuildingTradeName} is not displayed on the grid.</font>");
            }             
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Tear down test data.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Delete Building Trades.</b></font>");
            DeleteTradeRelations();
            DeleteTrade();
        }

        private void DeleteTradeRelations()
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            CommonHelper.RefreshPage();
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", NewBuildingTradeName);
                TradeVendorPage.Instance.LeftMenuNavigation("Vendors", true);
                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", NewVendorName);
                    System.Threading.Thread.Sleep(2000);
                }
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            CommonHelper.RefreshPage();
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", NewBuildingTradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Vendors", true);

                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName2);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName2))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", NewVendorName2);
                    System.Threading.Thread.Sleep(2000);
                }
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            CommonHelper.RefreshPage();
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", NewBuildingTradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Vendors", true);

                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName3);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName3))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", NewVendorName3);
                    System.Threading.Thread.Sleep(2000);
                }
            }
        }
        private void DeleteTrade()
        {
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
    }
}
