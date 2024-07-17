using NUnit.Framework;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.CommunityVendor;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionVendors;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Bid_Costs;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.Bid_Costs;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Costing.OptionBidCost;
using Pipeline.Testing.Pages.Costing.OptionBidCost.AddOptionBidCost;
using Pipeline.Testing.Pages.Costing.OptionBidCost.HistoricCosting;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhaseType;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_VII
{
    public class PIPE_38874 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VII);
        }

        private const double NewCostAmount = 10.00;

        private JobData newJobData;
        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_38874";
        private const string NewVendorCode = "38874";

        private OptionData newOption;
        private const string NewOptionName = "RT_QA_New_Option_38874";
        private const string NewOptionNumber = "38874";

        private OptionBidCostData newOptionBidCost;

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_38874";
        private const string NewBuildingGroupCode = "38874";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_38874";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_38874";
        private const string NewBuildingPhaseCode = "3887";

        private HouseData newHouse;
        private const string NewHouseName = "RT_QA_New_House_38874";
        private const string NewHousePlanNo = "3887";
        private static string House_Tier = "House";

        private CommunityData newCommunity;
        private const string NewCommunityName = "RT_QA_New_Community_38874";

        private const string DivisionName = "CG Visions";

        private string exportFileName;
        private const string ExportCsvHouse = "Export CSV Tier 2";
        private const string ExportExcelHouse = "Export Excel Tier 2";

        private const string BidCostImportHouse = "Vendor Option Bid Costs - House/Option Cost Tier 2 Import";

        [SetUp]
        public void Setup()
        {
            //Initialize test data 
            newJobData = new JobData()
            {
                Name = "QA_RT_Job11_Automation",
                Community = "Auto_33017-QA_Community_PIPE_33017_Automation",
                House = "0110-QA_RT_House11_Automation",
                Lot = "_111 - Sold",
                Orientation = "None",
            };

            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
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

            var optionType = new List<bool>()
            {
                false, false, false
            };

            newOption = new OptionData()
            {
                Name = NewOptionName,
                Number = NewOptionNumber,
                Types = optionType
            };

            newOptionBidCost = new OptionBidCostData()
            {
                Job = "KN_job1",
                JobOption = "KN_OPTION 1 - 01",
                JobCost = "50",
                BuildingPhase1 = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                BuildingPhase2 = "-001-CCCC"
            };

            newHouse = new HouseData()
            {
                HouseName = NewHouseName,
                PlanNumber = NewHousePlanNo
            };

            newCommunity = new CommunityData()
            {
                Name = NewCommunityName
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Create new Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Create new Option.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewOptionName);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", NewOptionName) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(newOption);
            }

            OptionPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);

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
            BuildingPhasePage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5 Add Building Phase to Vendor.</b></font>");
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", newVendor.Name);
            VendorPage.Instance.SelectVendor("Name", newVendor.Name);
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

            //setup house data and add option to house -> TIER 2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6 Add new House.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewHouseName);
            System.Threading.Thread.Sleep(2000);
            if (HousePage.Instance.IsItemInGrid("Name", NewHouseName) is false)
            {
                HousePage.Instance.CreateHouse(newHouse);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7 Add option to House.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewHouseName);
            System.Threading.Thread.Sleep(2000);
            if (HousePage.Instance.IsItemInGrid("Name", NewHouseName) is true)
            {
                HousePage.Instance.OpenHouseDetailPageOnNewTab(NewHouseName);
                CommonHelper.SwitchTab(1);
                HouseDetailPage.Instance.LeftMenuNavigation("Options", true);
                HouseOptionDetailPage.Instance.FilterItemInOptionnGrid("Name", GridFilterOperator.EqualTo, NewOptionName);
                System.Threading.Thread.Sleep(2000);
                if (HouseOptionDetailPage.Instance.IsItemInHouseOptionGrid("Name", NewOptionName) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(NewOptionName + " - " + NewOptionNumber);
                }

                CommonHelper.SwitchTab(0);
            }

            //setup community data and add option to community -> TIER 3
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8 Add new Community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is false)
            {
                CommunityPage.Instance.CreateCommunity(newCommunity);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9 Add option to Community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName))
            {
                CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName);
                CommunityDetailPage.Instance.LeftMenuNavigation("Options", true);
                string[] OptionData = { NewOptionName + " - " + NewOptionNumber };
                CommunityOptionData communityOptionData = new CommunityOptionData()
                {
                    OtherMasterOptions = OptionData,
                    SalePrice = "0.00"
                };
                CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.EqualTo, NewOptionName);
                System.Threading.Thread.Sleep(2000);
                if (CommunityOptionPage.Instance.IsItemInGrid("Option", NewOptionName) is false)
                {
                    CommunityOptionPage.Instance.OpenAddCommunityOptionModal();
                    CommunityOptionPage.Instance.AddCommunityOptionModal.AddCommunityOption(communityOptionData);
                    CommunityOptionPage.Instance.WaitCommunityOptionGridLoad();
                }
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10 Add community to House.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewHouseName);
            System.Threading.Thread.Sleep(2000);
            if (HousePage.Instance.IsItemInGrid("Name", NewHouseName) is true)
            {
                HousePage.Instance.OpenHouseDetailPageOnNewTab(NewHouseName);
                CommonHelper.SwitchTab(1);
                HouseDetailPage.Instance.LeftMenuNavigation("Communities", true);
                HouseCommunities.Instance.FillterOnGrid("Name", NewCommunityName);
                System.Threading.Thread.Sleep(2000);
                if (HouseCommunities.Instance.IsValueOnGrid("Name", NewCommunityName) is false)
                {
                    HouseCommunities.Instance.AddButtonCommunities();
                    HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(NewCommunityName, 0);
                }
                CommonHelper.SwitchTab(0);
            }

            //add community to division
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11 Add community to division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, DivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", DivisionName) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", DivisionName);
                DivisionDetailPage.Instance.LeftMenuNavigation("Communities", true);
                DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
                System.Threading.Thread.Sleep(2000);
                if (DivisionCommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is false)
                {
                    string[] communities = { NewCommunityName };
                    DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                    DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                    DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
                }
            }

            //add vendor to division
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12 Add vendor to division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, DivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", DivisionName) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", DivisionName);
                DivisionDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                DivisionVendorPage.Instance.FilterItemInDivisionVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (DivisionVendorPage.Instance.IsItemInDivisionVendorGrid("Name", NewVendorName) is false)
                {
                    string[] vendors = { NewVendorName };
                    DivisionVendorPage.Instance.OpenDivisionVendorModal();
                    DivisionVendorPage.Instance.DivisionVendorModal.SelectDivisionVendor(vendors);
                    DivisionVendorPage.Instance.DivisionVendorModal.Save();
                }

                //add vendor to community > costing> vendor assignments
                DivisionVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
                System.Threading.Thread.Sleep(2000);
                if (DivisionVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", NewBuildingPhaseName) is true)
                {
                    DivisionVendorPage.Instance.EditVendorAssignments(NewBuildingPhaseName, NewVendorName);
                }
            }

            //add vendor to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13 Add vendor to community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName);
                CommunityDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                CommunityVendorPage.Instance.FilterItemInCommunityVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
                System.Threading.Thread.Sleep(2000);
                if (CommunityVendorPage.Instance.IsItemInCommunityVendorGrid("Name", NewVendorName) is false)
                {
                    string[] vendors = { NewVendorName };
                    CommunityVendorPage.Instance.OpenCommunityVendorModal();
                    CommunityVendorPage.Instance.CommunityVendorModal.SelectCommunityVendor(vendors);
                    CommunityVendorPage.Instance.CommunityVendorModal.Save();
                }

                //add vendor to community > costing> vendor assignments
                CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
                System.Threading.Thread.Sleep(2000);
                if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is true)
                {
                    CommunityVendorPage.Instance.EditVendorAssignments(NewBuildingPhaseName, NewVendorName);
                }
            }
        }

        [Test]
        public void Costing_Remove_Allowance()
        {
            //Navigate to Costing > Vendors > select vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0 Select a Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);

            //Click Bid Costs link from the left navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Navigate to Option Bid Cost page.</b></font>");
            VendorDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);

            //Click the Utilities icon and select Export
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Export Bid Cost Tier 2 - House.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName("")}VendorOptionBidCost_Tier 2_{NewVendorName}";

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Export CSV Tier 2 - House.</b></font>");
            OptionBidCostPage.Instance.ExportFile(ExportCsvHouse, exportFileName, 1, ExportTitleFileConstant.VENDOR_OPTION_BIDCOST_HOUSE);
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Export Excel Tier 2 - House.</b></font>");
            OptionBidCostPage.Instance.ExportFile(ExportExcelHouse, exportFileName, 1, ExportTitleFileConstant.VENDOR_OPTION_BIDCOST_HOUSE);
            CommonHelper.RefreshPage();

            //Add a new entry on the exported .csv file and import
            string importFile = "";
            string expectedErrorMessage = "";
            CostingImportPage.Instance.OpenImportPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Import Bid Cost Tier2.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Import Tier 2- House.</b></font>");
            if (CostingImportPage.Instance.IsImportLabelDisplay(BidCostImportHouse) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {BidCostImportHouse} grid view to import new trades to phase.</font>");

            importFile = "\\DataInputFiles\\Costing\\BidCostImport\\Pipeline_BidCosts_House.csv";
            ImportValidData(BidCostImportHouse, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.1:  Import Bid Cost Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\BidCostImport\\Pipeline_BidCosts_House.txt";
            expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(BidCostImportHouse, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.2:  Import Bid Cost format import file.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\BidCostImport\\Pipeline_BidCosts_House_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(BidCostImportHouse, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.3:  Import Bid Cost File without header.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\BidCostImport\\Pipeline_BidCosts_House_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(BidCostImportHouse, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.4:  Import Bid Cost File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\BidCostImport\\Pipeline_BidCosts_House_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
            ImportInvalidData(BidCostImportHouse, importFile, expectedErrorMessage);


            //Navigate to Bid Cost and filter and search for the newly added tier 2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0 Navigate back to Bid Cost.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);
            VendorDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);

            OptionBidCostPage.Instance.SelectTier(House_Tier);
            if ((OptionBidCostPage.Instance.IsItemInBidCostGrid("House", NewHouseName)))
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> The House bid cost is in the grid data; received a toast message </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>The House bid cost is not present in grid</b></font>");
            }
            CommonHelper.RefreshPage();

            //Click on :plus: icon on Bid Costs pane and select the different tiers from Cost Tier dropdown then populate the needed information. 
            string expectedMessageSuccess = "Succesfully added Bid Cost";
            string actualMessage = "";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0 Click on :plus: icon on Bid Costs pane and select the different tiers from Cost Tier dropdown then populate the needed information. Verify if “Allowance” field and label is no longer displayed besides “Building Phase” label.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1 Tier 1 - Option.</b></font>");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 1 - Option");
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier1Option(newOption.Name + " - " + newOption.Number);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(NewCostAmount);
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            // Verify create new Bid Cost
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2 Tier 2 - House.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 2 - House");
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2House(newHouse.PlanNumber + "-" + newHouse.HouseName);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2Option(newOption.Name + " - " + newOption.Number);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(NewCostAmount);
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.3 Tier 3 - Community.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 3 - Community");
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Community(newCommunity.Name + "-" + newCommunity.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Option(newOption.Name + " - " + newOption.Number);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(NewCostAmount);
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.4 Tier 4 - Community House.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 4 - Community House");
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Community(newCommunity.Name + "-" + newCommunity.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4House(newHouse.PlanNumber + "-" + newHouse.HouseName);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Option(newOption.Name + " - " + newOption.Number);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(NewCostAmount);
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();
            CommonHelper.RefreshPage();


            //Navigate to Costing > Vendors > select vendor and on the “Jobs” pane, verify if there is no “Allowance” column on the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.0 Select a Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);
            VendorDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.ClickAddToOpenJobBidCostModal();
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectJob(newOptionBidCost.Job);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectOption(newOptionBidCost.JobOption);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectBuildingPhase(newOptionBidCost.BuildingPhase1);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.EnterBidCostAmount(double.Parse(newOptionBidCost.JobCost));
            actualMessage = OptionBidCostPage.Instance.AddJobOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");


            //Navigate to Costing > Option Bid Costs page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.0 Select a Vendor.</b></font>");
            OptionBidCostPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.OptionBidCost);
            OptionBidCostPage.Instance.SelectVendor(newVendor.Name);

            //Click on :plus: icon on Bid Costs pane and select the different tiers from Cost Tier dropdown then populate the needed information. 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.0 Click on :plus: icon on Bid Costs pane and select the different tiers from Cost Tier dropdown then populate the needed information. Verify if “Allowance” field and label is no longer displayed besides “Building Phase” label.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.1 Tier 1 - Option.</b></font>");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 1 - Option");
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier1Option(newOption.Name + " - " + newOption.Number);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(NewCostAmount);
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            // Verify create new Bid Cost
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.2 Tier 2 - House.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 2 - House");
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2House(newHouse.PlanNumber + "-" + newHouse.HouseName);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2Option(newOption.Name + " - " + newOption.Number);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(NewCostAmount);
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.3 Tier 3 - Community.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 3 - Community");
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Community(newCommunity.Name + "-" + newCommunity.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Option(newOption.Name + " - " + newOption.Number);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(NewCostAmount);
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.4 Tier 4 - Community House.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 4 - Community House");
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Community(newCommunity.Name + "-" + newCommunity.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4House(newHouse.PlanNumber + "-" + newHouse.HouseName);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Option(newOption.Name + " - " + newOption.Number);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(newBuildingPhase.Code + "-" + newBuildingPhase.Name);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(NewCostAmount);
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();
            CommonHelper.RefreshPage();

            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            OptionBidCostPage.Instance.ClickAddToOpenJobBidCostModal();
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectJob(newOptionBidCost.Job);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectOption(newOptionBidCost.JobOption);
            System.Threading.Thread.Sleep(2000);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectBuildingPhase(newOptionBidCost.BuildingPhase2);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.EnterBidCostAmount(double.Parse(newOptionBidCost.JobCost));
            actualMessage = OptionBidCostPage.Instance.AddJobOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");


            //Navigate back to Bid Costs default
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.0 Select a Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);
            VendorDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.0 Navigate to Historical Bid Cost page.</b></font>");
            OptionBidCostPage.Instance.ClickHistoricCostingBtn();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.1 Verify if data is visible in Historical Costing page.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.IsItemInHistoricCostGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.2 Navigate to option bid cost page.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ClickBackToPreviousBtn();
            CommonHelper.RefreshPage();

            //Navigate to Assets > Options > select any option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.0 Navigate to option and click on Bid Costs link from the left navigation and verify if “Allowance” column is no longer available.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newOption.Name);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", newOption.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", newOption.Name);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12.1 Navigate to Option Bid Cost page.</b></font>");
                OptionDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);
                if (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", newBuildingPhase.Code + "-" + newBuildingPhase.Name) is false)
                {
                    var optionBuildingPhaseData = new OptionBuildingPhaseData()
                    {
                        OptionName = newOption.Name,
                        BuildingPhase = new string[] { newBuildingPhase.Code + "-" + newBuildingPhase.Name },
                        Name = "Auto test",
                        Description = "Auto test",
                    };
                    // Add a new Bid costs if it does NOT exist
                    BidCostsToOptionPage.Instance.AddOptionBuildingPhase(optionBuildingPhaseData);
                }
                else
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b> Option already exist toast messsage </b></font>");

                }
            }

            System.Threading.Thread.Sleep(2000);
            CommonHelper.RefreshPage();


            //Navigate to Assets > Houses > select any house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 13.0 Navigate to Assets > Houses > select any house.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newHouse.HouseName);
            System.Threading.Thread.Sleep(2000);
            if (HousePage.Instance.IsItemInGrid("Name", NewHouseName) is false)
            {
                HousePage.Instance.CreateHouse(newHouse);
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> House already exist toast messsage </b></font>");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", newHouse.HouseName);
            }

            HouseDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);
            BidCostsToHousePage.Instance.FilterItemInGrid("Vendor", GridFilterOperator.Contains, newVendor.Name);
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            if (BidCostsToHousePage.Instance.IsOptionBuildingPhaseInGrid("Vendor", newVendor.Name) is true)
            {
                BidCostsToHousePage.Instance.EnterVendorNameToFilter("Vendor", newVendor.Name);
                BidCostsToHousePage.Instance.ClickEditItemInGrid("Vendor", newVendor.Name);
                BidCostsToHousePage.Instance.Update_HouseOptionBidCost("20");
                BidCostsToHousePage.Instance.ShowAssignedCostOnly();
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'><b> Vendor not found in House Bid Cost </b></font>");
            }


            //Navigate to Jobs > All Jobs > select job 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 14.0 Navigate to Jobs > All Jobs > select job.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", GridFilterOperator.Contains, newJobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", newJobData.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", newJobData.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.CaptureScreen();
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'><b> Job not found </b></font>");
            }

        }


        [TearDown]
        public void ClearData()
        {            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 15.0 No Tear Down of Data because the existing data will re used in the Imports. Bid Costs Id are required.</b></font>");
        }

        private void ImportValidData(string importGridTitle, string fullFilePath)
        {
            string actualMessage = CostingImportPage.Instance.ImportFile(importGridTitle, fullFilePath);

            string expectedMessage = "Import complete.";
            if (expectedMessage.ToLower().Contains(actualMessage.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The valid file was NOT imported." +
                    $"<br>The toast message is: {actualMessage}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid file was imported successfully and the toast message indicated success.</b></font>");

        }

        private void ImportInvalidData(string importGridTitlte, string fullFilePath, string expectedFailedData)
        {
            string actualMessage = CostingImportPage.Instance.ImportFile(importGridTitlte, fullFilePath);

            if (expectedFailedData.ToLower().Contains(actualMessage.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file failed to import and the toast message indicated failure.</b></font>");

        }
    }
}
