using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.EditTrade;
using Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.Settings.Purchasing;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class E06_PIPE_34422 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private BuildingGroupData newBuildingGroup;
        private BuildingPhaseData newBuildingPhase;
        private ProductData newProduct;
        private VendorData newVendor;
        private TradesData newTrade;

        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E06";
        private const string NewBuildingGroupCode = "E06";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E06";

        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_E06";
        private const string NewBuildingPhaseCode = "E06";

        private const string NewProductName = "RT_QA_New_Product_E06";
        private const string NewProductCode = "E06";

        private const string NewVendorName = "RT_QA_New_Vendor_E06";
        private const string NewVendorCode = "E06";

        private const string NewTradeName = "RT_QA_Building_Trade_E06";
        private const string NewTradeCode = "E06";
        private const string NewTradeDescription = "E06";

        private const string NewSchedulingTask = "";

        [SetUp]
        public void SetupTest()
        {
            //Add New Building Group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add New Building Group.</b></font>");
            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };

            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
                CommonHelper.RefreshPage();
                BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            }

            //Add new Building Phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add new Building Phase.</b></font>");
            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };

            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
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


            //Create new Vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Create new Vendor.</b></font>");

            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
            };

            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }


            //Add new building phase to vendor
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            //VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);
                VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                if (!VendorBuildingPhasePage.Instance.IsItemExist(newBuildingPhase.Code + "-" + newBuildingPhase.Name))
                {
                    VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhase.Code);
                    VendorBuildingPhasePage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
                }
            }

            newTrade = new TradesData()
            {
                TradeName = NewTradeName,
                Code = NewTradeCode,
                TradeDescription = NewTradeDescription,
                Vendor = newVendor.Name,
                BuildingPhases = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                SchedulingTasks = NewSchedulingTask
            };
        }


        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void E06_Purchasing_Trades_Add_A_Building_Trade()
        {
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            CommonHelper.RefreshPage();

            //Step 1
            //Navigate to Purchasing > Trades page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Purchasing > Trades page.</b></font>");
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);

            // Delete the data before update to the same name
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, newTrade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTrade.TradeName) is true)
            {
                DeleteTrade();
            }

            //Step 2
            //Open Building Trade modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open Building Trade modal.</b></font>");
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, newTrade.TradeName);
            TradesPage.Instance.ClickAddToOpenCreateTradeModal();

            //Step 3
            //Create Building Trade
            TradesPage.Instance.CreateTrade(newTrade);

            //Step 4
            //Show newly added Building Trade in the grid and edit
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Show newly added Building Trade in the grid.</b></font>");
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, newTrade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTrade.TradeName) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Show edit modal.</b></font>");
                TradesPage.Instance.ClickEditToOpenEditTradeModal(newTrade.TradeName);

                //Step 5.1
                //Edit Building Trade
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1: Click Cancel.</b></font>");
                TradesPage.Instance.ClickCancelEditTradeModal();
                CommonHelper.RefreshPage();
            }

            //Step 5.2
            //Edit Building Trade
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Edit Building Trade.</b></font>");
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, newTrade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTrade.TradeName) is true)
            {
                TradesPage.Instance.UpdateTrade(newTrade);
            }

            //Step 7
            //Delete Building Trade
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: Delete Building Trade.</b></font>");
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, newTrade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTrade.TradeName) is true)
            {
                DeleteTrade();
            }

        }

        #endregion


        [TearDown]
        public void ClearData()
        {
            DeleteVendor();
            DeleteBuildingPhase();
            DeleteBuildingGroup();
        }

        private void DeleteTrade()
        {
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, newTrade.TradeName);
            TradesPage.Instance.SelectTrade("Trade", newTrade.TradeName);
            TradeDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
            if (TradeBuildingPhasePage.Instance.IsItemInGrid("Building Phase", newBuildingPhase.Code + "-" + newBuildingPhase.Name) is true)
            {
                TradeBuildingPhasePage.Instance.DeleteItemInGrid("Building Phase", newBuildingPhase.Code + "-" + newBuildingPhase.Name);
                CommonHelper.RefreshPage();
            }

            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, newTrade.TradeName);
            TradesPage.Instance.SelectTrade("Trade", newTrade.TradeName);
            TradeDetailPage.Instance.LeftMenuNavigation("Scheduling Tasks", true);
            if (TradeSchedulingTaskPage.Instance.IsItemInGrid("Scheduling Tasks", NewSchedulingTask) is true)
            {
                TradeSchedulingTaskPage.Instance.DeleteItemInGrid("Scheduling Tasks", NewSchedulingTask);
                CommonHelper.RefreshPage();
            }

            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, newTrade.TradeName);
            TradesPage.Instance.SelectTrade("Trade", newTrade.TradeName);
            TradeDetailPage.Instance.LeftMenuNavigation("Vendors", true);
            if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", newVendor.Name) is true)
            {
                TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", newVendor.Name);
                CommonHelper.RefreshPage();
            }

            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, newTrade.TradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTrade.TradeName) is true)
            {
                TradesPage.Instance.DeleteItemInGrid("Trade", newTrade.TradeName);
                CommonHelper.RefreshPage();
            }

        }

        private void DeleteBuildingGroup()
        {
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
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

        private void DeleteVendor()
        {
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName);
                VendorPage.Instance.WaitGridLoad();
            }
        }

    }
}
