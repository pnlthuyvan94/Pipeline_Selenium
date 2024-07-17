

using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionVendors;
using Pipeline.Testing.Pages.Costing.TaxGroup;
using Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Pathway.Assets;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;

namespace Pipeline.Testing.Script.Section_IV
{
    class D01_D_PIPE_31169 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private VendorData vendor;
        private const string NewVendorName = "RT_QA_New_Vendor_D01D";
        private const string NewVendorCode = "D01D";

        TradesData trade;
        private const string NewBuildingTradeName = "RT_QA_New_BuildingTradeVendor_D01D";
        private const string NewBuildingTradeCode = "D01D";

        TaxGroupData taxGroup;
        private const string TaxGroupName = "RT_QA_New_TaxGroup_D01D";

        private DivisionData division;
        private const string NewDivisionName = "RT_QA_New_Division_D01D";

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_D01D";
        private const string NewBuildingGroupCode = "D01D";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_D01D";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_D01D";
        private const string NewBuildingPhaseCode = "D01D";


        [SetUp]
        public void SetUpData()
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
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add New Trade test data.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(trade);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add New tax group test data.</b></font>");
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

            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add new Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhase.Code)
                                          .EnterPhaseName(newBuildingPhase.Name)
                                          .EnterAbbName(newBuildingPhase.AbbName)
                                          .EnterDescription(newBuildingPhase.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase.BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }

            vendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode,
                Trade = trade.TradeName,
                Contact = "Contact",
                Email = "d01d@test.com",
                Address1 = "address1",
                Address2 = "address2",
                Address3 = "address3",
                City = "city",
                State = "state",
                Zip = "zip",
                Phone = "phone1",
                AltPhone = "phone2",
                MobilePhone = "00000000000",
                Fax = "fax",
                Url = "url@url.com",
                TaxGroup = taxGroup.Name,
                EnablePrecision = true
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add new vendor data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendor);
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6 Add Building Phase to Vendor.</b></font>");
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", vendor.Name);
            VendorPage.Instance.SelectVendor("Name", vendor.Name);
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is false)
            {
                System.Threading.Thread.Sleep(1500);
                VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhase.Code);
                VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();
                System.Threading.Thread.Sleep(2000);
            }

            division = new DivisionData()
            {
                Name = NewDivisionName
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, NewDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", NewDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(division);
            }
            CommonHelper.RefreshPage();
        }

        [Test]
        [Category("Section_IV")]
        public void D01_D_Costing_DetailPages_Vendors_Division()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: On the Vendors  data page, click the Vendor to which you would like to select.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Go to Costing menu and select Vendors.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Click the Vendor to which you like to select.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Go to Vendor Detail Page.</b></font>");
                VendorPage.Instance.SelectVendor("Name", NewVendorName);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: On the Vendor Side Navigation menu, click the 'Divisions' to open the Division data page.</b></font>");
                VendorDetailPage.Instance.LeftMenuNavigation("Divisions");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: From ASSETS/DIVISIONS, select a Division; open the Vendors/Costing page in the Side Navigation.</b></font>");
            AssetsPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.SelectItemInGrid("Division", NewDivisionName);
            DivisionDetailPage.Instance.LeftMenuNavigation("Vendors", true);
            DivisionVendorPage.Instance.FilterItemInDivisionVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionVendorPage.Instance.IsItemInDivisionVendorGrid("Name", NewVendorName) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Add Vendor to Division.</b></font>");
                string[] vendorList = { NewVendorName };
                DivisionVendorPage.Instance.AssignVendorToDivision(vendorList);
                System.Threading.Thread.Sleep(2000);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: Verify the Division is shown on the Vendor Division page.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);
                VendorDetailPage.Instance.LeftMenuNavigation("Divisions");
            }
        }

        [TearDown]
        public void ClearData()
        {
            DeleteTradeRelations(NewBuildingTradeName);
            DeleteTrade(NewBuildingTradeName);
            DeleteVendorInDivision();
            DeleteBuildingPhaseInVendors();
            DeleteBuildingPhase();
            DeleteBuildingGroup();
            DeleteVendor();
            DeleteDivision();
        }

        private void DeleteBuildingGroup()
        {
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter,string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is true)
            {
                BuildingGroupPage.Instance.DeleteItemInGrid("Name", NewBuildingGroupName);
                BuildingGroupPage.Instance.WaitGridLoad();
            }
        }

        private void DeleteBuildingPhase()
        {
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
                BuildingPhasePage.Instance.WaitGridLoad();
            }
        }

        private void DeleteBuildingPhaseInVendors()
        {
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", vendor.Name);
            VendorPage.Instance.SelectVendor("Name", vendor.Name);
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is true)
            {
                System.Threading.Thread.Sleep(1500);
                VendorBuildingPhasePage.Instance.DeleteItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
            }
        }

        private void DeleteVendorInDivision()
        {
            AssetsPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.SelectItemInGrid("Division", NewDivisionName);
            DivisionDetailPage.Instance.LeftMenuNavigation("Vendors", true);
            DivisionVendorPage.Instance.FilterItemInDivisionVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionVendorPage.Instance.IsItemInDivisionVendorGrid("Name", NewVendorName) is true)
            {
                DivisionVendorPage.Instance.DeleteItemInDivisionVendorGrid("Name", NewVendorName);
            }
            CommonHelper.RefreshPage();
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

        private void DeleteDivision()
        {
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, NewDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", NewDivisionName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                DivisionPage.Instance.DeleteItemInGrid("Division", NewDivisionName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }
        }

    }
}
