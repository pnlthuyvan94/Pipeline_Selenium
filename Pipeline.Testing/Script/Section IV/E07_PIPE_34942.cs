using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase;
using Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E07_PIPE_34942 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E07";
        private const string NewBuildingGroupCode = "RE07";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E07";

        private BuildingPhaseData newBuildingPhase1;
        private const string NewBuildingPhaseName1 = "RT_QA_New_BuildingPhase_E071";
        private const string NewBuildingPhaseCode1 = "RE17";

        private BuildingPhaseData newBuildingPhase2;
        private const string NewBuildingPhaseName2 = "RT_QA_New_BuildingPhase_E072";
        private const string NewBuildingPhaseCode2 = "RE27";

        private BuildingPhaseData newBuildingPhase3;
        private const string NewBuildingPhaseName3 = "RT_QA_New_BuildingPhase_E073";
        private const string NewBuildingPhaseCode3 = "RE37";

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTrade_E07";
        private const string NewBuildingTradeCode = "RT07";

        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_E07";
        private const string NewVendorCode = "RE07";

        private VendorData newVendor2;
        private const string NewVendorName2 = "RT_QA_New_Vendor_E27";
        private const string NewVendorCode2 = "RE27";

        private VendorData newVendor3;
        private const string NewVendorName3 = "RT_QA_New_Vendor_E37";
        private const string NewVendorCode3 = "RE37";

        //Scheduling Tasks data needs to be hard-coded since these are sent by Scheduling to Pipeline
        //Pipeline has no entry page for Scheduling
        private const string SchedulingTask1 = "Site Prep";
        private const string SchedulingTask2 = "Footings";
        private const string SchedulingTask3 = "Foundation";

        private DivisionData newDivision;
        private const string newDivisionName = "RT_QA_New_Division_E07";

        private CommunityData newCommunity;
        private const string newCommunityName = "RT_QA_New_Community_E07";

        private const string editVendorExpectedMessage = "Vendor Assignments have been updated successfully.";

        private string exportFileName;
        private const string ExportCsvMenu = "Export CSV";
        private const string ExportExcelMenu = "Export Excel";

        private const string TradeToPhaseImport = "Trade to Building Phase Import";
        private const string TradeToVendorImport = "Trade to Vendor Import";
        private const string TradeToSchedulingImport = "Trade to Scheduling Task Import";

        TradesData _trade;
        [SetUp]
        public void Setup()
        {
            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };

            newBuildingPhase1 = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode1,
                Name = NewBuildingPhaseName1,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            newBuildingPhase2 = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode2,
                Name = NewBuildingPhaseName2,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            newBuildingPhase3 = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode3,
                Name = NewBuildingPhaseName3,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
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

            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };

            newCommunity = new CommunityData()
            {
                Name = newCommunityName
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (!BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName))
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add First Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName1);
            System.Threading.Thread.Sleep(2000);
            if (!BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName1))
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                     .EnterPhaseCode(newBuildingPhase1.Code)
                                     .EnterPhaseName(newBuildingPhase1.Name)
                                     .EnterAbbName(newBuildingPhase1.AbbName)
                                     .EnterDescription(newBuildingPhase1.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase1.BuildingGroup);

                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }
           

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add Second Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName2);
            System.Threading.Thread.Sleep(2000);
            if (!BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName2))
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhase2.Code)
                                          .EnterPhaseName(newBuildingPhase2.Name)
                                          .EnterAbbName(newBuildingPhase2.AbbName)
                                          .EnterDescription(newBuildingPhase2.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase2.BuildingGroup);

                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Add Third Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName3);
            System.Threading.Thread.Sleep(2000);
            if (!BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName3))
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhase3.Code)
                                          .EnterPhaseName(newBuildingPhase3.Name)
                                          .EnterAbbName(newBuildingPhase3.AbbName)
                                          .EnterDescription(newBuildingPhase3.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase3.BuildingGroup);

                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add first vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Add second vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName2);
            System.Threading.Thread.Sleep(2000);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName2))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor2);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Add third vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewVendorName3);
            System.Threading.Thread.Sleep(2000);
            if (!VendorPage.Instance.IsItemInGrid("Name", NewVendorName3))
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor3);
            }

            _trade = new TradesData()
            {
                Code = NewBuildingTradeCode,
                TradeName = NewBuildingTradeName,
                TradeDescription = NewBuildingTradeName,
                Vendor = NewVendorName,
                BuildingPhases = NewBuildingPhaseCode1 + "-" + NewBuildingPhaseName1,
                SchedulingTasks = SchedulingTask1
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Delete pre-existing Trades Test Data.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                DeleteTradeRelations();
                CommonHelper.RefreshPage();
                DeleteTrade();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add new community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", newCommunityName) is false)
            {
                CommunityPage.Instance.CreateCommunity(newCommunity);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add new community to new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", newDivisionName);
                DivisionDetailPage.Instance.LeftMenuNavigation("Communities", true);

                DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityName);
                System.Threading.Thread.Sleep(2000);
                if (DivisionCommunityPage.Instance.IsItemInGrid("Name", newCommunityName) is false)
                {
                    string[] communities = { newCommunityName };
                    DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                    DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                    DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
                }
            }

        }

        [Test]
        public void E07_Purchasing_Building_Trades()
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Check if Building Trade Add Modal is displayed.</b></font>");
            TradesPage.Instance.ClickAddToOpenCreateTradeModal();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Populate trade details in the modal.</b></font>");
            TradesPage.Instance.CreateTrade(_trade);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Check if new trade is displayed on the grid.</b></font>");
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.Contains, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                ExtentReportsHelper.LogPass(null, $"Building Trade " + _trade.TradeName + " is displayed on the grid.");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Edit a trade via inline edit.</b></font>");
                string updateTradeName = _trade.TradeName + "_updated";
                var expectedUpdateMessage = "Trade " + _trade.Code + " " + updateTradeName + " saved successfully!";

                var actualUpdateMessage = TradesPage.Instance.EditTrade("Trade", _trade.TradeName, updateTradeName, NewBuildingPhaseCode2 + "-" + NewBuildingPhaseName2, "");
                if (expectedUpdateMessage == actualUpdateMessage)
                {
                    ExtentReportsHelper.LogPass(actualUpdateMessage);
                    _trade.TradeName = updateTradeName;
                }
            }
            else
            {
                ExtentReportsHelper.LogWarning($"<font color = 'red'>Building Trade  {_trade.TradeName} is not displayed on the grid.</font>");
            }


            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Navigate to Trade Details page and update the description and name.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", _trade.TradeName);
                TradeDetailPage.Instance.EnterTradeDescription(NewBuildingTradeName + "_updated");
                TradeDetailPage.Instance.EnterTradeName(NewBuildingTradeName);
                TradeDetailPage.Instance.Save();
                string buildingTradeDetailToastMessage = TradeDetailPage.Instance.GetLastestToastMessage();
                string expectedToastMessage = "Trade " + NewBuildingTradeCode + " " + NewBuildingTradeName + " saved successfully!";
                if (buildingTradeDetailToastMessage == expectedToastMessage)
                {
                    ExtentReportsHelper.LogPass(null, buildingTradeDetailToastMessage);
                    _trade.TradeName = NewBuildingTradeName;
                }                    
                else
                    ExtentReportsHelper.LogFail(null, buildingTradeDetailToastMessage);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0: Go to Trades default page and click on Vendor Assignments button.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.ClickVendorAssignments();
            System.Threading.Thread.Sleep(2000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1: Select new Division " + newDivisionName + ".</b></font>");
            VendorAssignmentsPage.Instance.SelectDivision(newDivisionName, 1);
            System.Threading.Thread.Sleep(2000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2: Select new Community " + newCommunityName + ".</b></font>");
            string[] communities = { newCommunityName };
            VendorAssignmentsPage.Instance.SelectCommunities(communities);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.3: Load Vendors.</b></font>");
            VendorAssignmentsPage.Instance.ClickLoadVendors();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            VendorAssignmentsPage.Instance.ClickLoadVendors();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.4: Filter trades column to show new Trade " + _trade.TradeName + ".</b></font>");
            VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, _trade.TradeName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.5: Update the division vendor to " + NewVendorName + ".</b></font>");
            string editDivisionVendorMsg = VendorAssignmentsPage.Instance.EditDivisionVendor(newDivisionName, NewVendorName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            if (editDivisionVendorMsg == editVendorExpectedMessage)
                ExtentReportsHelper.LogPass(editDivisionVendorMsg);
            else
                ExtentReportsHelper.LogFail(editDivisionVendorMsg);


            VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, _trade.TradeName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.6: Update the community vendor to " + NewVendorName + ".</b></font>");
            string editCommunityVendorMsg = VendorAssignmentsPage.Instance.EditCommunityVendor(newCommunityName, NewVendorName);
            VendorAssignmentsPage.Instance.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
            if (editCommunityVendorMsg == editVendorExpectedMessage)
                ExtentReportsHelper.LogPass(editCommunityVendorMsg);
            else
                ExtentReportsHelper.LogFail(editCommunityVendorMsg);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Navigate to Trade to Building Phases page.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", _trade.TradeName);

                TradeDetailPage.Instance.LeftMenuNavigation("Building Phases", true);

                TradeBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName1);
                System.Threading.Thread.Sleep(2000);
                if (TradeBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode1 + "-" + NewBuildingPhaseName1))
                {
                    ExtentReportsHelper.LogPass(null, NewBuildingPhaseCode1 + "-" + NewBuildingPhaseName1 + " is displayed on the grid.");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Add new Building Phase to Trade.</b></font>");
                TradeBuildingPhasePage.Instance.ShowAddPhaseToTradeModal();
                if (TradeBuildingPhasePage.Instance.AddPhaseToTradeModal.IsModalDisplayed)
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Add Building Phase to Trade modal is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='green'><b>Add Building Phase to Trade modal is not displayed.</b></font>");

                string[] buildingPhaseList = { NewBuildingPhaseCode3 + "-" + NewBuildingPhaseName3 };
                TradeBuildingPhasePage.Instance.AddPhaseToTradeModal.SelectBuildingPhases(buildingPhaseList);
                TradeBuildingPhasePage.Instance.AddPhaseToTradeModal.Save();
                System.Threading.Thread.Sleep(1000);
                CommonHelper.RefreshPage();

                TradeBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName3);
                System.Threading.Thread.Sleep(2000);
                if (TradeBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode3 + "-" + NewBuildingPhaseName3))
                {
                    ExtentReportsHelper.LogPass(null, NewBuildingPhaseCode3 + "-" + NewBuildingPhaseName3 + " is displayed on the grid.");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.3: Trades To Building Phase Import/Export.</b></font>");
                exportFileName = $"{CommonHelper.GetExportFileName(ExportType.TradesToBuildingPhases.ToString())}";

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.4: Export Trades To Building Phase to CSV File.</b></font>");
                TradeBuildingPhasePage.Instance.ExportFile(ExportCsvMenu, exportFileName, 0, ExportTitleFileConstant.TRADE_TO_PHASE_TITLE);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.5: Export Trades To Building Phase to Excel File.</b></font>");
                TradeBuildingPhasePage.Instance.ExportFile(ExportExcelMenu, exportFileName, 0, ExportTitleFileConstant.TRADE_TO_PHASE_TITLE);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.6: Import Trades To Building Phase.</b></font>");
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.TRADES_IMPORT_URL);
                if (TradesImportPage.Instance.IsImportLabelDisplay(TradeToPhaseImport) is false)
                    ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {TradeToPhaseImport} grid view to import new trades to phase.</font>");

                string importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToPhase\\Pipeline_TradesToBuildingPhases.csv";
                ImportValidData(TradeToPhaseImport, importFile);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.7:  Import Trades To Building Phase Wrong file type.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToPhase\\Pipeline_TradesToBuildingPhases.txt";
                string expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
                ImportInvalidData(TradeToPhaseImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.8:  Import Trades To Building Phase Wrong format import file.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToPhase\\Pipeline_TradesToBuildingPhases_Wrong_Format.csv";
                expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
                ImportInvalidData(TradeToPhaseImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.9:  Import Trades To Building Phase File without header.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToPhase\\Pipeline_TradesToBuildingPhases_No_Header.csv";
                expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
                ImportInvalidData(TradeToPhaseImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.10:  Import Trades To Building Phase File has the “character” between fields don’t match with the configuration.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToPhase\\Pipeline_TradesToBuildingPhases_Invalid_Separator.csv";
                expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
                ImportInvalidData(TradeToPhaseImport, importFile, expectedErrorMessage);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Navigate to Trade to Vendors page.</b></font>");

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", _trade.TradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Vendors", true);

                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.Contains, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName))
                {
                    ExtentReportsHelper.LogPass(null, NewVendorName + " is displayed on the grid.");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Add new Vendor to Trade.</b></font>");
                TradeVendorPage.Instance.ShowAddVendorToTradeModal();
                if (TradeVendorPage.Instance.AddVendorToTradeModal.IsModalDisplayed)
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Add Vendor to Trade modal is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='green'><b>Add Vendor to Trade modal is not displayed.</b></font>");

                string[] vendorsList = { NewVendorName3 };
                TradeVendorPage.Instance.AddVendorToTradeModal.SelectVendors(vendorsList);
                TradeVendorPage.Instance.AddVendorToTradeModal.Save();
                System.Threading.Thread.Sleep(1000);
                CommonHelper.RefreshPage();

                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.Contains, NewVendorName3);
                System.Threading.Thread.Sleep(2000);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", NewVendorName3))
                {
                    ExtentReportsHelper.LogPass(null, NewVendorName3 + " is displayed on the grid.");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3: Trades To Vendor Import/Export.</b></font>");
                exportFileName = $"{CommonHelper.GetExportFileName(ExportType.TradesToVendors.ToString())}";

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.4: Export Trades To Vendor to CSV File.</b></font>");
                TradeVendorPage.Instance.ExportFile(ExportCsvMenu, exportFileName, 0, ExportTitleFileConstant.TRADE_TO_VENDOR_TITLE);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.5: Export Trades To Vendor to Excel File.</b></font>");
                TradeVendorPage.Instance.ExportFile(ExportExcelMenu, exportFileName, 0, ExportTitleFileConstant.TRADE_TO_VENDOR_TITLE);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.6: Import Trades To Vendor.</b></font>");
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.TRADES_IMPORT_URL);
                if (TradesImportPage.Instance.IsImportLabelDisplay(TradeToVendorImport) is false)
                    ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {TradeToVendorImport} grid view to import new trades to phase.</font>");

                string importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors.csv";
                ImportValidData(TradeToVendorImport, importFile);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.7:  Import Trades To Vendor Wrong file type.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors.txt";
                string expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
                ImportInvalidData(TradeToVendorImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.8:  Import Trades To Vendor Wrong format import file.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors_Wrong_Format.csv";
                expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
                ImportInvalidData(TradeToVendorImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.9:  Import Trades To Vendor File without header.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors_No_Header.csv";
                expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
                ImportInvalidData(TradeToVendorImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.10:  Import Trades To Vendor File has the “character” between fields don’t match with the configuration.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToVendor\\Pipeline_TradesToVendors_Invalid_Separator.csv";
                expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
                ImportInvalidData(TradeToVendorImport, importFile, expectedErrorMessage);

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1: Navigate to Trade to Scheduling Task page.</b></font>");

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", _trade.TradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Scheduling Tasks", true);

                TradeSchedulingTaskPage.Instance.FilterItemInGrid("Scheduling Tasks", GridFilterOperator.Contains, SchedulingTask1);
                System.Threading.Thread.Sleep(2000);
                if (TradeSchedulingTaskPage.Instance.IsItemInGrid("Scheduling Tasks", SchedulingTask1))
                {
                    ExtentReportsHelper.LogPass(null, SchedulingTask1 + " is displayed on the grid.");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2: Add new Scheduling Tasks to Trade.</b></font>");
                TradeSchedulingTaskPage.Instance.ShowAddSchedulingTasksToTradeModal();
                if (TradeSchedulingTaskPage.Instance.AddSchedulingTaskToTradeModal.IsModalDisplayed)
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Add Scheduling Task to Trade modal is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='green'><b>Add Scheduling Task to Trade modal is not displayed.</b></font>");

                string[] tasksList = { SchedulingTask3 };
                TradeSchedulingTaskPage.Instance.AddSchedulingTaskToTradeModal.SelectSchedulingTasks(tasksList);
                TradeSchedulingTaskPage.Instance.AddSchedulingTaskToTradeModal.Save();
                System.Threading.Thread.Sleep(1000);
                CommonHelper.RefreshPage();

                TradeSchedulingTaskPage.Instance.FilterItemInGrid("Scheduling Tasks", GridFilterOperator.Contains, SchedulingTask3);
                System.Threading.Thread.Sleep(2000);
                if (TradeSchedulingTaskPage.Instance.IsItemInGrid("Scheduling Tasks", SchedulingTask3))
                {
                    ExtentReportsHelper.LogPass(null, SchedulingTask3 + " is displayed on the grid.");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3: Trades To Scheduling Import/Export.</b></font>");
                exportFileName = $"{CommonHelper.GetExportFileName(ExportType.TradesToSchedulingTasks.ToString())}";

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.4: Export Trades Scheduling Vendor to CSV File.</b></font>");
                TradeSchedulingTaskPage.Instance.ExportFile(ExportCsvMenu, exportFileName, 0, ExportTitleFileConstant.TRADE_TO_SCHEDULING_TITLE);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.5: Export Trades To Scheduling to Excel File.</b></font>");
                TradeSchedulingTaskPage.Instance.ExportFile(ExportExcelMenu, exportFileName, 0, ExportTitleFileConstant.TRADE_TO_SCHEDULING_TITLE);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.6: Import Trades To Scheduling.</b></font>");
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.TRADES_IMPORT_URL);
                if (TradesImportPage.Instance.IsImportLabelDisplay(TradeToSchedulingImport) is false)
                    ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {TradeToSchedulingImport} grid view to import new trades to phase.</font>");

                string importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToScheduling\\Pipeline_TradesToScheduling.csv";
                ImportValidData(TradeToSchedulingImport, importFile);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.7:  Import Trades To Scheduling Wrong file type.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToScheduling\\Pipeline_TradesToScheduling.txt";
                string expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
                ImportInvalidData(TradeToSchedulingImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.8:  Import Trades To Scheduling Wrong format import file.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToScheduling\\Pipeline_TradesToScheduling_Wrong_Format.csv";
                expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
                ImportInvalidData(TradeToSchedulingImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.9:  Import Trades To Scheduling File without header.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToScheduling\\Pipeline_TradesToScheduling_No_Header.csv";
                expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
                ImportInvalidData(TradeToSchedulingImport, importFile, expectedErrorMessage);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.10:  Import Trades To Scheduling File has the “character” between fields don’t match with the configuration.</b></font>");
                importFile = "\\DataInputFiles\\Purchasing\\TradesImport\\TradesToScheduling\\Pipeline_TradesToScheduling_Invalid_Separator.csv";
                expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
                ImportInvalidData(TradeToSchedulingImport, importFile, expectedErrorMessage);
            }

            

        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.0 Tear down test data.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.1 Delete Building Trades.</b></font>");
            DeleteTradeRelations();
            DeleteTrade();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.2 Delete Building Group.</b></font>");
            DeleteBuildingGroup();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.3 Delete Building Phase.</b></font>");
            DeleteBuildingPhase();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.4 Delete Vendor.</b></font>");
            DeleteVendor();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.5 Delete Community.</b></font>");
            DeleteCommunity();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.6 Delete Division.</b></font>");
            DeleteDivision();
        }

        private void DeleteBuildingGroup()
        {
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is true)
            {
                BuildingGroupPage.Instance.DeleteItemInGrid("Name", NewBuildingGroupName);
                BuildingGroupPage.Instance.WaitGridLoad();
            }
        }
        private void DeleteBuildingPhase()
        {
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName1);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName1) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName1);
            }

            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName2);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName2) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName2);
            }

            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName3);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName3) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName3);
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

            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName2);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName2) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName2);
            }

            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName3);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName3) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", NewVendorName3);
            }
        }

        private void DeleteTradeRelations()
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade",GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", _trade.TradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Building Phases", true);

                TradeBuildingPhasePage.Instance.DeleteAllBuildingPhases();
                System.Threading.Thread.Sleep(2000);                
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", _trade.TradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Vendors", true);

                TradeVendorPage.Instance.DeleteAllVendors();
                System.Threading.Thread.Sleep(2000);
            }

        }
        private void DeleteTrade()
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, _trade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", _trade.TradeName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                TradesPage.Instance.DeleteItemInGrid("Trade", _trade.TradeName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }
        }

        private void DeleteDivision()
        {
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                DivisionPage.Instance.DeleteItemInGrid("Division", newDivisionName);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.RefreshPage();
            }
        }

        private void DeleteCommunity()
        {
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", newCommunityName) is true)
            {
                System.Threading.Thread.Sleep(2000);
                CommunityPage.Instance.DeleteItemInGrid("Name", newCommunityName);
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

            if (expectedFailedData.ToLower().Contains(actualMessage.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file failed to import and the toast message indicated failure.</b></font>");

        }
    }
}
