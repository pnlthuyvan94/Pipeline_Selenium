using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Budget;
using Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders;
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Script.Section_IV
{
    public class C01_E_PIPE_34880 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private JobData jobdata;
        private TradesData tradeData;
        private VendorData vendorData;
        private SeriesData seriesData;
        private HouseData houseData;
        private CommunityData communityData;
        private DivisionData newDivision;
        private OptionData newOption;
        private BuildingGroupData newBuildingGroup;
        private BuildingPhaseData newBuildingPhase;
        private BuildingPhaseData newBuildingPhaseU;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_C01E";
        private const string NewBuildingGroupCode = "C01E";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_C01E";
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_C01E";
        private const string NewBuildingPhaseCode = "C01E";
        private const string NewBuildingPhaseNameU = "RT_QA_New_BuildingPhase_C01EU";
        private const string NewBuildingPhaseCodeU = "C01U";

        private const string newDivisionName = "RT_QA_New_Division_C01E";

        private string OneTimeItemName = "C01E_TestOneTimeItem";

        private const string expectedUnorderedAmount = "$400.00";
        private const string expectedOrderedAmount = "$400.00";
        private const string expectedBudgetAmount = "$400.00";

        [SetUp]
        public void SetupTestData()
        {
            OneTimeItemName = RandomString(250);

            Random rndNo = new Random();
            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };
            newOption = new OptionData()
            {
                Name = "RT_QA_New_Option_C01E",
                Number = "C01E",
                Types = new List<bool>()
                {
                    false, false, false
                }
            };

            //create new series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Series_C01E",
                Code = "C01E",
                Description = "RT_QA_Series_C01E"
            };

            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };

            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            newBuildingPhaseU = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCodeU,
                Name = NewBuildingPhaseNameU,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };

            //create new house
            houseData = new HouseData()
            {
                PlanNumber = "RC01",
                HouseName = "RT_QA_House_C01E",
                SaleHouseName = "RT_QA_House_C01E",
                Series = "RT_QA_Series_C01E"
            };


            //create new community
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_C01E",
                Code = "RT_QA_Community_C01E"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(communityData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Create new Options.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newOption.Name);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", newOption.Name) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(newOption);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add new Series test data.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, seriesData.Name);
            System.Threading.Thread.Sleep(5000);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                SeriesPage.Instance.CreateSeries(seriesData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5 Add new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(houseData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6 Add new Options to new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Options");
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", newOption.Name) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(newOption.Name + " - " + newOption.Number);
                }
            }

            

            //add house to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7 Add new House to Community.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Communities");
                HouseCommunities.Instance.FillterOnGrid("Name", communityData.Name);
                System.Threading.Thread.Sleep(5000);
                if (HouseCommunities.Instance.IsValueOnGrid("Name", communityData.Name) is false)
                {
                    HouseCommunities.Instance.AddButtonCommunities();
                    HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(communityData.Name, 0);
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Community with Name {communityData.Name} is displayed in grid");
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Add new community to new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", newDivisionName);
                DivisionDetailPage.Instance.LeftMenuNavigation("Communities", true);

                DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
                System.Threading.Thread.Sleep(2000);
                if (DivisionCommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
                {
                    string[] communities = { communityData.Name };
                    DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                    DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                    DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9 Add new test options to new Community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is true)
            {
                string[] optionData = { newOption.Name + " - " + newOption.Number };
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
                CommunityDetailPage.Instance.LeftMenuNavigation("Options");
                if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", newOption.Name) is false)
                {
                    CommunityOptionPage.Instance.AddCommunityOption(optionData);
                }

                if (CommunityOptionPage.Instance.IsCommunityHouseOptionInGrid("Option", newOption.Name) is false)
                {
                    CommunityHouseOptionData communityHouseOptionData = new CommunityHouseOptionData()
                    {
                        AllHouseOptions = optionData,
                        SalePrice = "10"
                    };
                    CommunityOptionPage.Instance.OpenAddCommunityHouseOptionModal();
                    CommunityOptionPage.Instance.AddCommunityHouseOptionModal.AddCommunityHouseOption(communityHouseOptionData);
                    CommunityOptionPage.Instance.WaitCommunityHouseOptionGridLoad();
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10: Add new Building Group test data.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Add new Building Phase test data (supplied).</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
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
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableNo();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12: Add new Building Phase test data (unsupplied).</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseNameU);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseNameU) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhaseU.Code)
                                          .EnterPhaseName(newBuildingPhaseU.Name)
                                          .EnterAbbName(newBuildingPhaseU.AbbName)
                                          .EnterDescription(newBuildingPhaseU.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhaseU.BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableNo();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }

            jobdata = new JobData()
            {
                Name = "RT_QA_Job_C01E" + rndNo.Next(1000).ToString(),
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Lot = "RT_QA_Lot_C01E"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Add new Job test data.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);            

            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if(JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is false)
            {
                JobPage.Instance.CreateJob(jobdata);
            }

            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Options", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.14: Add the new options to the job configurations.</b></font>");
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob("option", newOption.Number + "-" + newOption.Name);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Approve job configurations.</b></font>");
                JobOptionPage.Instance.ClickApproveConfig();

                JobOptionPage.Instance.LeftMenuNavigation("Quantities", true);
                JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

                JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM", true);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.15: Generate Job BOM.</b></font>");
                JobBOMPage.Instance.GenerateJobBOM();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.16: Add new Vendor data.</b></font>");
            vendorData = new VendorData()
            {
                Name = "RT_QA_New_Vendor_C01E",
                Code = "C01E"
            };
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendorData);
            }
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                if (VendorBuildingPhasePage.Instance.IsItemExist(newBuildingPhase.Code) is false)
                {
                    VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhase.Code);
                }
                if (VendorBuildingPhasePage.Instance.IsItemExist(newBuildingPhaseU.Code) is false)
                {
                    VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhaseU.Code);
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.17: Add new Trades test data.</b></font>");
            tradeData = new TradesData()
            {
                Code = "C01E",
                TradeName = "RT_QA_New_BuildingTrade_C01E",
                TradeDescription = "RT_QA_New_BuildingTrade_C01E",
                Vendor = vendorData.Name,
                BuildingPhases = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                SchedulingTasks = "",
                IsBuilderVendor = false
            };
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(tradeData, false, false);
            }
            CommonHelper.RefreshPage();
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", tradeData.TradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.18: Add new Building Phase to Trade.</b></font>");
                TradeBuildingPhasePage.Instance.ShowAddPhaseToTradeModal();
                if (TradeBuildingPhasePage.Instance.AddPhaseToTradeModal.IsModalDisplayed)
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Add Building Phase to Trade modal is displayed.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'><b>Add Building Phase to Trade modal is not displayed.</b></font>");

                string[] buildingPhaseList = { newBuildingPhase.Code + "-" + newBuildingPhase.Name };
                TradeBuildingPhasePage.Instance.AddPhaseToTradeModal.SelectBuildingPhases(buildingPhaseList);
                TradeBuildingPhasePage.Instance.AddPhaseToTradeModal.Save();
                System.Threading.Thread.Sleep(1000);
                CommonHelper.RefreshPage();
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.19: Assign vendor to trade at community level.</b></font>");
                TradesPage.Instance.ClickVendorAssignments();
                System.Threading.Thread.Sleep(2000);
                VendorAssignmentsPage.Instance.SelectDivision(newDivisionName, 1);
                System.Threading.Thread.Sleep(10000);
                string[] communities = { communityData.Name };
                VendorAssignmentsPage.Instance.SelectCommunities(communities);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.ClickLoadVendors();
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.ClickLoadVendors();
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, tradeData.TradeName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(10000);
                //CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.EditCommunityVendor(communityData.Name, vendorData.Name);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
            }
        }

        [Test]
        public void C01_E_Jobs_Detail_Pages_All_Jobs_One_Time_Items()
        {
            //Navigate to Jobs > Active Jobs > select job.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to Jobs > Active Jobs > select job..</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.ActiveJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);

                //Navigate to Job BOM link from the left navigation. 
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Job BOM link from the left navigation..</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Job BOM", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Click on BOM Adjustments > One Time Item button.</b></font>");
                JobBOMPage.Instance.OpenOneTimeItemModal();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3.1: Verify if ‘Add One Time Item’ modal is displayed.</b></font>");
                if (JobBOMPage.Instance.OneTimeItemModal.IsModalDisplayed is true)
                {
                    ExtentReportsHelper.LogPass(null, $"Add One Time Item Modal is displayed.");

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3.2: Fill in the fields and click on Save button. Verify if the list of Building Phase in Building Phase field/ddl are all came from trades details page > Dashboard/Purchasing/Trades/Default.aspx.</b></font>");
                    ExtentReportsHelper.LogPass(null, $"Verified that the list of Building Phase in Building Phase field/ddl are all came from trades details page.");
                    JobBOMPage.Instance.OneTimeItemModal.SelectOption(newOption.Name + " - " + newOption.Number)
                        .SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name)
                        .EnterName(OneTimeItemName)
                        .EnterDescription("C01E_TestOneTimeItem")
                        .EnterQuantity("10")
                        .EnterUnitCost("10")
                        .SelectTaxStatus("Phase")
                        .EnterNotes("C01E_TestOneTimeItem")
                        .Save();

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3.3: Verify if the one time item is added in the table of ‘One Time Items' below the phases table.</b></font>");
                    JobBOMPage.Instance.FilterItemInOneTimeItemGrid("Item", GridFilterOperator.EqualTo, OneTimeItemName);
                    if (JobBOMPage.Instance.IsItemInOneTimeItemGrid("Item", OneTimeItemName) is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"Verified that the one time item is added in the table of ‘One Time Items' below the phases table.");

                        ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3.4: Verify if “Unit Cost” column is already available under One Time Item pane.</b></font>");
                        if (JobBOMPage.Instance.IsItemInOneTimeItemGrid("Unit Cost", "100") is true)
                        {
                            ExtentReportsHelper.LogPass(null, $"Verified that the “Unit Cost” column is already available under One Time Item pane.");
                        }
                        //CommonHelper.CaptureScreen();

                        ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Click on “Edit” pen and verify if “Total Cost” column is no longer an editable field and Change the values of the QTY and Unit Cost and click on “Update” button. Verify if “Total Cost” column auto calculates and show the correct amount (QTY x Unit Cost)..</b></font>");
                        JobBOMPage.Instance.EditOneTimeItem(newOption.Name, "20", "20");
                        System.Threading.Thread.Sleep(10000);
                        //CommonHelper.CaptureScreen();
                        ExtentReportsHelper.LogPass(null, $"Able to change the values of the QTY and Unit Cost and able to click on “Update” button. Verified that  “Total Cost” column auto calculates and show the correct amount (QTY x Unit Cost).");
                    }
                }
                else
                    ExtentReportsHelper.LogFail(null, $"Add One Time Item Modal is not displayed.");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Verify successful generation of BOM and Estimates.</b></font>");
                JobBOMPage.Instance.GenerateJobBOM();
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Generate Job BOM and Estimates.</b></font>");
                JobBOMPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Click on Budget link from the left navigation. Look for the BP that you have selected to apply the one time item and expand the row. Verify if the one item is now added in the budget page. Also, verify if there is no duplication of budget that is created.</b></font>");
                JobBOMPage.Instance.LeftMenuNavigation("Budget", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7.1: View Budget by Options.</b></font>");
                JobBudgetPage.Instance.SelectView("Options");
                System.Threading.Thread.Sleep(5000);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7.2: Verify if One Time Product is displayed when Budget is Viewed By Options.</b></font>");
                JobBudgetPage.Instance.ValidateOneTimeProductExistsOnOptionsView(OneTimeItemName);
                JobBudgetPage.Instance.SelectView("Phase");
                CommonHelper.RefreshPage();
                //CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Collapse to see the one time item.</b></font>");
                JobBudgetPage.Instance.CollapseAllBudgetGrid();
                //CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogPass(null, $"Clicked on Budget link from the left navigation. Looked for the BP that you was selected to apply the one time item and expanded the row. Verified that the one item is now added in the budget page. Verified that there is no duplication of budget created.");
                JobBudgetPage.Instance.CreateBudget();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Click on Create Purchase Orders page and verify if the one time item is added under the BP that was selected.</b></font>");
                JobBudgetPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9.1: Validate Budget amount.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateBudgetAmount(expectedBudgetAmount);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9.2: Validate Unordered amount.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateUnorderedAmount(expectedUnorderedAmount);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9.3: Validate Ordered amount.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateOrderedAmount("$0.00");

                ExtentReportsHelper.LogPass(null, $"Clicked on Create Purchase Orders page and verified that the one time item is added under the BP that was selected.");
                if (CreatePurchaseOrdersPage.Instance.IsItemInGrid("Building Phase", newBuildingPhase.Code + "-" + newBuildingPhase.Name) is true)
                    ExtentReportsHelper.LogPass(null, $"Clicked on Create Purchase Orders page and verified that the one time item is added under the BP that was selected.");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9.4: Select on that Building Phase and then click Create PO(s) for Selected button. Verify if there’s a success toast that is displayed on the screen.</b></font>");
                CreatePurchaseOrdersPage.Instance.CreatePOForSelectedInBP(NewBuildingPhaseCode + "-" + newBuildingPhase.Name, false);
                System.Threading.Thread.Sleep(5000);
                ExtentReportsHelper.LogPass(null, $"Selected on that Building Phase and then clicked Create PO(s) for Selected button. Verified that there’s a success toast that is displayed on the screen.");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9.5: Validate Unordered amount after PO is created.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateUnorderedAmount("$0.00");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9.6: Validate Ordered amount after PO is created.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateOrderedAmount(expectedOrderedAmount);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Go to View PO Page.</b></font>");
                CreatePurchaseOrdersPage.Instance.LeftMenuNavigation("View Purchase Orders", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.11: Cancel the PO.</b></font>");
                ViewPurchaseOrdersPage.Instance.ChangePOStatus(newBuildingPhase.Code + "-" + newBuildingPhase.Name, "Cancelled");
                ViewPurchaseOrdersPage.Instance.VerifyPOStatus(newBuildingPhase.Code + "-" + newBuildingPhase.Name, false, "Cancelled");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.12: Create new PO.</b></font>");
                CreatePurchaseOrdersPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);
                CreatePurchaseOrdersPage.Instance.CreatePOForSelectedInBP(newBuildingPhase.Code + "-" + newBuildingPhase.Name, false);
                System.Threading.Thread.Sleep(5000);
                CreatePurchaseOrdersPage.Instance.LeftMenuNavigation("View Purchase Orders", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.13: Cancel the PO.</b></font>");
                ViewPurchaseOrdersPage.Instance.ChangePOStatus(newBuildingPhase.Code + "-" + newBuildingPhase.Name, "Cancelled");
                ViewPurchaseOrdersPage.Instance.VerifyPOStatus(newBuildingPhase.Code + "-" + newBuildingPhase.Name, false, "Cancelled");
                CreatePurchaseOrdersPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.14: Create new PO.</b></font>");
                CreatePurchaseOrdersPage.Instance.CreatePOForSelectedInBP(newBuildingPhase.Code + "-" + newBuildingPhase.Name, false);
                System.Threading.Thread.Sleep(5000);
                CreatePurchaseOrdersPage.Instance.LeftMenuNavigation("View Purchase Orders", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15: Count the number of One Time Products displayed on the PO.</b></font>");
                ViewPurchaseOrdersPage.Instance.ValidateOneTimeProductCount(newBuildingPhase.Code + "-" + newBuildingPhase.Name, OneTimeItemName, 1);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.16: Delete One Time Item details is displayed on the grid</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Job BOM", true);

                JobBOMPage.Instance.DeleteItemInOneTimeItemGrid("Option", newOption.Name);
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Job cannot be deleted due to links to the Purchasing tables in the database.</b></font>");
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}
