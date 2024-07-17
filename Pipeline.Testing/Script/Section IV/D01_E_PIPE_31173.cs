

using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
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
using Pipeline.Testing.Pages.Purchasing.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;

namespace Pipeline.Testing.Script.Section_IV
{
    class D01_E_PIPE_31173 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private VendorData vendor;
        private const string NewVendorName = "RT_QA_New_Vendor_D01E";
        private const string NewVendorCode = "D01E";

        TradesData trade;
        private const string NewBuildingTradeName = "RT_QA_New_BuildingTradeVendor_D01E";
        private const string NewBuildingTradeCode = "D01E";

        TaxGroupData taxGroup;
        private const string TaxGroupName = "RT_QA_New_TaxGroup_D01E";

        private DivisionData division;
        private const string NewDivisionName = "RT_QA_New_Division_D01E";

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_D01E";
        private const string NewBuildingGroupCode = "D01E";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_D01E";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_D01E";
        private const string NewBuildingPhaseCode = "D01E";

        private CommunityData newCommunityData;
        private const string NewCommunityName = "RT_QA_New_Community_D01E";

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
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
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
                Email = "d01e@test.com",
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
            CommonHelper.RefreshPage();

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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Add new community.</b></font>");
            newCommunityData = new CommunityData()
            {
                Name = NewCommunityName,
                Division = NewDivisionName,
                City = "City",
                Code = "Code",
                CityLink = "https://cl.com",
                Township = "Township",
                County = "Country",
                State = "State",
                Zip = "00000",
                SchoolDistrict = "SchoolDistrict",
                SchoolDistrictLink = "http://sdl.com",
                Status = "Open",
                Description = "D01E",
                DrivingDirections = "D01E",
                Slug = "Slug",
            };

            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is false)
            {
                CommunityPage.Instance.CreateCommunity(newCommunityData);
            }
            CommonHelper.RefreshPage();
        }

        [Test]
        [Category("Section_IV")]
        public void D01_E_Costing_DetailPages_Vendors_Communities()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to Costing > Vendors link.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: Select Vendor to which you like to select.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: On the Vendor Side Navigation menu, click the 'Divisions' to open the Division data page.</b></font>");
                VendorDetailPage.Instance.LeftMenuNavigation("Communities");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Verify if Communities are listed by Building Phase.</b></font>");
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogPass("<font color='green'> Communities are listed by Building Phase</font>");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0 Navigate to Purchasing > Building Phases.</b></font>");
            Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0 From “Code” column, search one of the building phase from Communities page.</b></font>");
            Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, newBuildingPhase.Code);
            System.Threading.Thread.Sleep(2000);
            if (Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.IsItemInGrid("Code", newBuildingPhase.Code) is true)
            {
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogPass("<font color='green'>Entry displayed in grid.</font>");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.0 Select the code of the building phase and verify if the page redirects to Building Phase Details page.</b></font>");
                Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.ClickItemInGrid("Code", newBuildingPhase.Code);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.0 Scroll down and view the Vendors pane.</b></font>");
                BuildingPhaseDetailPage.Instance.FilterItemInVendorGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (BuildingPhaseDetailPage.Instance.IsItemInVendorGrid("Vendor Name", NewVendorName) is true)
                {
                    ExtentReportsHelper.LogPass("<font color='green'>Vendor is available in the list.</font>");

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.0 Click Trash icon and confirm deletion.</b></font>");
                    BuildingPhaseDetailPage.Instance.DeleteItemInVendorsGrid("Vendor Name", NewVendorName);
                    string _actualMessage = BuildingPhaseDetailPage.Instance.GetLastestToastMessage();
                    ExtentReportsHelper.LogPass("<font color='green'>" + _actualMessage + "</font>");
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.0 Navigate back to vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);

                VendorDetailPage.Instance.LeftMenuNavigation("Communities");
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.0: Verify if the building phase is no longer part of the list in the table grid.</b></font>");
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogPass("<font color='green'> Building phase is no longer part of the list in the table grid</font>");
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.0 Navigate back to building phase.</b></font>");
            Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            CommonHelper.CaptureScreen();
            Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                System.Threading.Thread.Sleep(4000);
                CommonHelper.CaptureScreen();
                Pipeline.Testing.Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.ClickItemInGrid("Name", NewBuildingPhaseName);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 13.0 Scroll down to Vendors pane and click button and verify if Add Vendor to Phase modal is displayed.</b></font>");
                BuildingPhaseDetailPage.Instance.ClickAddVendorToPhaseModal();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 14.0 Select vendor from the list then click Save button.</b></font>");
                BuildingPhaseDetailPage.Instance.AddVendorToPhaseModal.SelectVendor(NewVendorCode + "-" + NewVendorName, 1);
                System.Threading.Thread.Sleep(3000);
                BuildingPhaseDetailPage.Instance.AddVendorToPhaseModal.Save();
                CommonHelper.RefreshPage();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 15.0 Verify if correct toast notification is displayed.</b></font>");
                string _actualMessage = BuildingPhaseDetailPage.Instance.GetLastestToastMessage();
                ExtentReportsHelper.LogPass("<font color='green'>" + _actualMessage + "</font>");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 16.0 Navigate back to vendor page.</b></font>");
                VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
                VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
                {
                    VendorPage.Instance.SelectVendor("Name", NewVendorName);

                    VendorDetailPage.Instance.LeftMenuNavigation("Communities");
                    CommonHelper.CaptureScreen();
                    ExtentReportsHelper.LogPass("<font color='green'> Building phase is included on the list in the table grid</font>");
                }
            }
        }

        [TearDown]
        public void ClearData()
        {
            DeleteTradeRelations(NewBuildingTradeName);
            DeleteTrade(NewBuildingTradeName);
            DeleteVendorInPhase();
            DeleteVendorInDivision();
            DeleteBuildingPhaseInVendors();
            DeleteBuildingPhase();
            DeleteBuildingGroup();
            DeleteVendor();
            DeleteCommunity();
            DeleteDivision();
        }

        private void DeleteVendorInPhase()
        {
            Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, newBuildingPhase.Code);
            System.Threading.Thread.Sleep(2000);
            if (Pipeline.Testing.Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.IsItemInGrid("Code", newBuildingPhase.Code) is true)
            {
                Pages.Purchasing.BuildingPhases.BuildingPhasesPage.Instance.ClickItemInGrid("Code", newBuildingPhase.Code);
                BuildingPhaseDetailPage.Instance.FilterItemInVendorGrid("Vendor Name", GridFilterOperator.EqualTo, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (BuildingPhaseDetailPage.Instance.IsItemInVendorGrid("Vendor Name", NewVendorName) is true)
                {
                    BuildingPhaseDetailPage.Instance.DeleteItemInVendorsGrid("Vendor Name", NewVendorName);
                }
            }
        }

        private void DeleteBuildingGroup()
        {
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
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

        private void DeleteCommunity()
        {
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName))
                CommunityPage.Instance.DeleteCommunity(NewCommunityName);
        }

    }
}
