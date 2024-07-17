using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.TaxGroup;
using Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_IV
{
    public class D01_A_PIPE_31157 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private VendorData vendor;
        private const string NewVendorName = "RT_QA_New_Vendor_D01A";
        private const string NewVendorCode = "D01A";

        TradesData trade;
        private const string NewBuildingTradeName = "RT_QA_New_BuildingTradeVendor_D01A";
        private const string NewBuildingTradeCode = "D01A";

        TaxGroupData taxGroup;
        private const string TaxGroupName = "RT_QA_New_TaxGroup_D01A";

        [SetUp]
        public void Setup()
        {
            trade = new TradesData()
            {
                Code = NewBuildingTradeCode,
                TradeName = NewBuildingTradeName,
                TradeDescription = NewBuildingTradeName,
                IsBuilderVendor = false,
                Vendor = "",
                BuildingPhases = "",
                SchedulingTasks = ""
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Add New Trade test data.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(trade);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Add New tax group test data.</b></font>");
            taxGroup = new TaxGroupData()
            {
                Name = TaxGroupName
            };
            TaxGroupPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.TaxGroups);
            TaxGroupPage.Instance.FindItemInGridWithTextContains("Tax Group", taxGroup.Name);
            if (TaxGroupPage.Instance.IsItemInGrid(taxGroup.Name) is false)
            {
                TaxGroupPage.Instance.ClickAddToOpenTaxGroupModal();
                AddTaxGroupModal.Instance.AddTaxGroupModal.EnterTaxGroupName(taxGroup.Name).Save();
            }
        }
        [Test]
        public void D01_A_Costing_Vendors_Details()
        {
            vendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode,
                Trade = trade.TradeName,
                Contact = "Contact",
                Email = "test@test.com",
                Address1 = "Address1",
                Address2 = "Address2",
                Address3 = "Address3",
                City = "City",
                State = "State",
                Zip = "Zip",
                Phone = "Phone",
                AltPhone = "AltPhone",
                MobilePhone = "MobilePhone",
                Fax = "Fax",
                Url = "Url",
                TaxGroup = taxGroup.Name,
                EnablePrecision = true
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Add new vendor data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Go to vendor detail page.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                vendor.Email = "test@test.com";
                vendor.Address1 = "testAddress";
                vendor.City = "testCity";
                vendor.State = "testState";
                vendor.Zip = "testZip";
                vendor.Address2 = "testAddress2";
                vendor.Address3 = "testAddress3";
                vendor.Phone = "testPhone";
                vendor.AltPhone = "testAltPhone";
                vendor.MobilePhone = "testMobilePhone";
                vendor.Fax = "testFax";
                vendor.Url = "testUrl";
                vendor.TaxGroup = taxGroup.Name + " (0.000%)";
                vendor.EnablePrecision = false;

                VendorPage.Instance.SelectVendor("Name", NewVendorName);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3: Update vendor details with new values.</b></font>");
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendor);
                var expectedMessage = NewVendorName + " saved successfully";
                var message = VendorDetailPage.Instance.GetLastestToastMessage();
                if(expectedMessage == message)
                {
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>" + message + "</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>" + message + "</b></font>");
                }
            }
        }

        [TearDown]
        public void ClearData()
        {
            DeleteTradeRelations(NewBuildingTradeName);
            DeleteTrade(NewBuildingTradeName);
            DeleteVendor();
        }

        private void DeleteTradeRelations(string tradeName)
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            CommonHelper.RefreshPage();
            CommonHelper.CaptureScreen();
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", tradeName);
                TradeVendorPage.Instance.LeftMenuNavigation("Vendors", true);
                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", NewVendorName);
                }
            }
        }

        private void DeleteVendor()
        {
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName);
            }
        }

        private void DeleteTrade(string tradeName)
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                TradesPage.Instance.DeleteItemInGrid("Trade", tradeName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }
        }
    }
}
