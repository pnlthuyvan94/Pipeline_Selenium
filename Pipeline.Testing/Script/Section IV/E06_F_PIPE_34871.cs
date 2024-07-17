using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
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
    public class E06_F_PIPE_34871 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_E06F";
        private const string NewVendorCode = "E06F";

        private VendorData newVendor2;
        private const string NewVendorName2 = "RT_QA_New_Vendor_E06F2";
        private const string NewVendorCode2 = "E06F2";

        private VendorData newVendor3;
        private const string NewVendorName3 = "RT_QA_New_Vendor_E06F3";
        private const string NewVendorCode3 = "E06F3";

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTrade_E06F";
        private const string NewBuildingTradeCode = "E06F";

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
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add first vendor test data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add second vendor test data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName2);
            System.Threading.Thread.Sleep(2000);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName2))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor2);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add third vendor test data.</b></font>");
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
        public void E06_F_Purchasing_Trades_Vendors()
        {
           
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Create new Building Trade test data.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.ClickAddToOpenCreateTradeModal();
            TradesData _trade = new TradesData()
            {
                Code = NewBuildingTradeCode,
                TradeName = NewBuildingTradeName,
                TradeDescription = NewBuildingTradeName,
                Vendor = "",
                BuildingPhases = "",
                SchedulingTasks = ""
            };

            TradesPage.Instance.CreateTrade(_trade);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is true)
            {
                ExtentReportsHelper.LogPass(null, $"Building Trade " + NewBuildingTradeName + " is displayed on the grid.");
                TradesPage.Instance.SelectItemInGrid("Trade", NewBuildingTradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Vendors", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Verify if columns are available in the grid.</b></font>");
                if (TradeVendorPage.Instance.IsColumnFoundInGrid("Vendor Name") is true)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendor Name column is found in the grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Vendor Name column is not found in the grid.</b></font>");

                if (TradeVendorPage.Instance.IsColumnFoundInGrid("Vendor Code") is true)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendor Code column is found in the grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Vendor Code column is not found in the grid.</b></font>");

                if (TradeVendorPage.Instance.IsColumnFoundInGrid("Company Vendor") is false)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Company Vendor column is not found in the grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Company Vendor column is found in the grid.</b></font>");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Verify if Buttons are available in the page.</b></font>");

                if (TradeVendorPage.Instance.IsBulkActionDisplayed is true)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Bulk Actions is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Bulk Actions is not displayed.</b></font>");

                if (TradeVendorPage.Instance.IsUtilitiesDisplayed is true)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Utilities button is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Utilities button is not displayed.</b></font>");

                if (TradeVendorPage.Instance.IsUtilitiesDisplayed is true)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Utilities button is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Utilities button is not displayed.</b></font>");

                if (TradeVendorPage.Instance.IsAddVendorTradeBtnDisplayed is true)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Add button is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Add button is not displayed.</b></font>");

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

                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.Contains, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName))
                {
                    ExtentReportsHelper.LogPass(null, NewVendorName + " is displayed on the grid.");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, NewVendorName + " is not displayed on the grid.");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Delete selected vendors via Bulk Actions.</b></font>");
                TradeVendorPage.Instance.DeleteSelectedVendors(new List<string> { NewVendorName, NewVendorName2 });
                CommonHelper.CaptureScreen();
                CommonHelper.RefreshPage();
                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName))
                {
                    ExtentReportsHelper.LogFail(null, NewVendorName + " is not deleted from the grid.");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, NewVendorName + " is deleted from the grid.");
                }
                CommonHelper.RefreshPage();
                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName2);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName2))
                {
                    ExtentReportsHelper.LogFail(null, NewVendorName2 + " is not deleted from the grid.");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, NewVendorName2 + " is deleted from the grid.");
                }
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

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
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

                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName2);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName2))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", NewVendorName2);
                    System.Threading.Thread.Sleep(2000);
                }

                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName3);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName3))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", NewVendorName3);
                    System.Threading.Thread.Sleep(2000);
                }
            }

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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2 Delete Test Vendors.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName);
            }

            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName2);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName2) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName2);
            }

            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName3);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName3) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName3);
            }

        }

    }
}
