using NUnit.Framework;
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
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.Bid_Costs;
using Pipeline.Testing.Pages.Costing.OptionBidCost;
using Pipeline.Testing.Pages.Costing.OptionBidCost.AddOptionBidCost;
using Pipeline.Testing.Pages.Costing.OptionBidCost.HistoricCosting;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_VII
{
    public class PIPE_34506 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VII);
        }

        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_FHB01";
        private const string NewVendorCode = "RFHB";

        private OptionData newOption;
        private const string NewOptionName = "RT_QA_New_Option_FHB01";
        private const string NewOptionNumber = "RFHB";

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_FHB01";
        private const string NewBuildingGroupCode = "RFHB";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_FHB01";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_FHB01";
        private const string NewBuildingPhaseCode = "RFHB";

        private HouseData newHouse;
        private const string NewHouseName = "RT_QA_New_House_FHB01";
        private const string NewHousePlanNo = "RFHB";

        private CommunityData newCommunity;
        private const string NewCommunityName = "RT_QA_New_Community_FHB01";

        private const string DivisionName = "CG Visions";

        private string exportFileName;
        private const string ExportCsvOption = "Export CSV - Option";
        private const string ExportExcelOption = "Export Excel - Option";

        private const string ExportCsvHouse = "Export CSV - House";
        private const string ExportExcelHouse = "Export Excel - House";

        private const string ExportCsvCommunity = "Export CSV - Community";
        private const string ExportExcelCommunity = "Export Excel - Community";

        private const string ExportCsvCommunityOptionHouse = "Export CSV - Community Option House";
        private const string ExportExcelCommunityOptionHouse = "Export Excel - Community Option House";

        private const string HistoricBidCostImportOption = "Option (Tier 1)";
        private const string HistoricBidCostImportHouse = "House (Tier 2)";
        private const string HistoricBidCostImportCommunity = "Community (Tier 3)";
        private const string HistoricBidCostImportCommunityHouseOption = "Community House Option (Tier 4)";

        [SetUp]
        public void Setup()
        {
            //Initialize test data objects
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

            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
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

            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Create new Option.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewOptionName);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", NewOptionName) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(newOption);
            }

            OptionPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new Building Phase.</b></font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Create new Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5 Add Building Phase to Vendor.</b></font>");
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", newVendor.Name);
            VendorPage.Instance.SelectVendor("Name", newVendor.Name);
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode +"-" + NewBuildingPhaseName) is false)
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
        public void Costing_Future_And_Historic_Bid_Cost()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1 Navigate to Option Bid Cost page.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);
            VendorDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2 Verify if History Costing button is displayed.</b></font>");
            if (OptionBidCostPage.Instance.IsHistoryCostingButtonDisplayed() is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>Historic Costing button is displayed.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Historic Costing button on the current page.</font>");


            //Setup Tier 1 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3 Setup Tier 1 test data.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 1 - Option");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier1Option(NewOptionName + " - " + NewOptionNumber);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
            //OptionBidCostPage.Instance.AddOptionBidCostModal.ClickAddAllowance();
            //OptionBidCostPage.Instance.AddOptionBidCostModal.EnterAllowanceText(10);
            //OptionBidCostPage.Instance.AddOptionBidCostModal.ClickSaveAllowance();
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(10);
            string expectedMessageSuccess = $"Succesfully added Bid Cost";
            var actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            //Setup Tier 2

            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4.1 Check if previous data is retained after saving.</b></font>");
            OptionBidCostPage.Instance.AddOptionBidCostModal.CheckPreviousDataRetained("Tier 1 - Option", NewOptionName + " - " + NewOptionNumber, NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4.2 Check if data is retained after clicking Cancel button.</b></font>");
            OptionBidCostPage.Instance.AddOptionBidCostModal.Cancel();
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.CheckPreviousDataRetained("Tier 1 - Option", NewOptionName + " - " + NewOptionNumber, NewBuildingPhaseCode + "-" + NewBuildingPhaseName);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4 Setup Tier 2 test data.</b></font>");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 2 - House");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2House(NewHousePlanNo + "-" + NewHouseName);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2Option(NewOptionName + " - " + NewOptionNumber);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
            //OptionBidCostPage.Instance.AddOptionBidCostModal.ClickAddAllowance();
            //OptionBidCostPage.Instance.AddOptionBidCostModal.EnterAllowanceText(10);
            //OptionBidCostPage.Instance.AddOptionBidCostModal.ClickSaveAllowance();
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(10);
            expectedMessageSuccess = $"Succesfully added Bid Cost";
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            //Setup Tier 3
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5 Setup Tier 3 test data.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 3 - Community");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Community(NewCommunityName + "-" + NewCommunityName);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Option(NewOptionName + " - " + NewOptionNumber);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
            //OptionBidCostPage.Instance.AddOptionBidCostModal.ClickAddAllowance();
            //OptionBidCostPage.Instance.AddOptionBidCostModal.EnterAllowanceText(10);
            //OptionBidCostPage.Instance.AddOptionBidCostModal.ClickSaveAllowance();
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(10);
            expectedMessageSuccess = $"Succesfully added Bid Cost";
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            //Setup Tier 4
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6 Setup Tier 4 test data.</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 4 - Community House");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Community(NewCommunityName + "-" + NewCommunityName);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4House(NewHousePlanNo + "-" + NewHouseName);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Option(NewOptionName + " - " + NewOptionNumber);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
            //OptionBidCostPage.Instance.AddOptionBidCostModal.ClickAddAllowance();
            //OptionBidCostPage.Instance.AddOptionBidCostModal.EnterAllowanceText(10);
            //OptionBidCostPage.Instance.AddOptionBidCostModal.ClickSaveAllowance();
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(10);
            expectedMessageSuccess = $"Succesfully added Bid Cost";
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");
            OptionBidCostPage.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Navigate to Historical Bid Cost page.</b></font>");
            OptionBidCostPage.Instance.ClickHistoricCostingBtn();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Verify if data is visible in Historical Costing page.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.IsItemInHistoricCostGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Export Historical Bid Cost.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.HistoricalBidCosts.ToString())}_Option_{NewVendorName}";

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Export CSV Tier 1 - Option.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ExportFile(ExportCsvOption, exportFileName, 1, ExportTitleFileConstant.FUTUREBIDCOST_OPTION);
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Export Excel Tier 1 - Option.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ExportFile(ExportExcelOption, exportFileName, 1, ExportTitleFileConstant.FUTUREBIDCOST_OPTION);
            CommonHelper.RefreshPage();

            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.HistoricalBidCosts.ToString())}_House_{NewVendorName}";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.3: Export CSV Tier 2 - House.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ExportFile(ExportCsvHouse, exportFileName, 1, ExportTitleFileConstant.FUTUREBIDCOST_HOUSE);
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.4: Export Excel Tier 2 - House.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ExportFile(ExportExcelHouse, exportFileName, 1, ExportTitleFileConstant.FUTUREBIDCOST_HOUSE);
            CommonHelper.RefreshPage();

            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.HistoricalBidCosts.ToString())}_Community_{NewVendorName}";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.5: Export CSV Tier 3 - Community.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ExportFile(ExportCsvCommunity, exportFileName, 1, ExportTitleFileConstant.FUTUREBIDCOST_COMMUNITY);
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.6: Export Excel Tier 3 - _Community_.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ExportFile(ExportExcelCommunity, exportFileName, 1, ExportTitleFileConstant.FUTUREBIDCOST_COMMUNITY);
            CommonHelper.RefreshPage();

            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.HistoricalBidCosts.ToString())}_CommunityOptionHouse_{NewVendorName}";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.7: Export CSV Tier 4 - Community.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ExportFile(ExportCsvCommunityOptionHouse, exportFileName, 1, ExportTitleFileConstant.FUTUREBIDCOST_COMMUNITYOPTIONHOUSE);
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.8: Export Excel Tier 4 - _Community Option House_.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ExportFile(ExportExcelCommunityOptionHouse, exportFileName, 1, ExportTitleFileConstant.FUTUREBIDCOST_COMMUNITYOPTIONHOUSE);
            CommonHelper.RefreshPage();

            CostingImportPage.Instance.OpenImportPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Import Historical Bid Cost.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Import Tier 1- Option.</b></font>");

            if (CostingImportPage.Instance.IsImportLabelDisplay(HistoricBidCostImportOption) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {HistoricBidCostImportOption} grid view to import new trades to phase.</font>");

            string importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Option.csv";
            ImportValidData(HistoricBidCostImportOption, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1.1:  Import Historical Bid Costs Option Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Option.txt";
            string expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(HistoricBidCostImportOption, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1.2:  Import Historical Bid Cost Option  Wrong format import file.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Option_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(HistoricBidCostImportOption, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1.3:  Import Historical Bid Cost Option File without header.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Option_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(HistoricBidCostImportOption, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1.4:  Import Historical Bid Cost Option File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Option_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
            ImportInvalidData(HistoricBidCostImportOption, importFile, expectedErrorMessage);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Import Tier 2- House.</b></font>");
            if (CostingImportPage.Instance.IsImportLabelDisplay(HistoricBidCostImportHouse) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {HistoricBidCostImportHouse} grid view to import new trades to phase.</font>");

            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_House.csv";
            ImportValidData(HistoricBidCostImportHouse, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.1:  Import Historic Bid Cost Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_House.txt";
            expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(HistoricBidCostImportHouse, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.2:  Import Historic Bid Cost format import file.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_House_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(HistoricBidCostImportHouse, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.3:  Import Historic Bid Cost File without header.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_House_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(HistoricBidCostImportHouse, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.4:  Import Historic Bid Cost File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_House_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
            ImportInvalidData(HistoricBidCostImportHouse, importFile, expectedErrorMessage);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3: Import Tier 3- Community.</b></font>");
            if (CostingImportPage.Instance.IsImportLabelDisplay(HistoricBidCostImportCommunity) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {HistoricBidCostImportCommunity} grid view to import new trades to phase.</font>");

            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Community.csv";
            ImportValidData(HistoricBidCostImportCommunity, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3.1:  Import Historic Bid Cost Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Community.txt";
            expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(HistoricBidCostImportCommunity, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3.2:  Import Historic Bid Cost Wrong format import file.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Community_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(HistoricBidCostImportCommunity, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3.3:  Import Historic Bid Cost File without header.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Community_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(HistoricBidCostImportCommunity, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3.4:  Import Historic Bid Cost File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_Community_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
            ImportInvalidData(HistoricBidCostImportCommunity, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.4: Import Tier 4- Community House Option.</b></font>");
            if (CostingImportPage.Instance.IsImportLabelDisplay(HistoricBidCostImportCommunityHouseOption) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {HistoricBidCostImportCommunityHouseOption} grid view to import new trades to phase.</font>");

            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_CommunityOptionHouse.csv";
            ImportValidData(HistoricBidCostImportCommunityHouseOption, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.4.1:  Import Community House Option Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_CommunityOptionHouse.txt";
            expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(HistoricBidCostImportCommunityHouseOption, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.4.2:  Import Community House Option Wrong format import file.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_CommunityOptionHouse_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(HistoricBidCostImportCommunityHouseOption, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.4.3:  Import Community House Option File without header.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_CommunityOptionHouse_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(HistoricBidCostImportCommunityHouseOption, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.4.4:  Import Community House Option File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\HistoricalBidCostImport\\Pipeline_HistoricalBidCosts_CommunityOptionHouse_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking here.";
            ImportInvalidData(HistoricBidCostImportCommunityHouseOption, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0 Go back to Historic Costing page.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);
            VendorDetailPage.Instance.LeftMenuNavigation("Bid Costs", true);
            OptionBidCostPage.Instance.ClickHistoricCostingBtn();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1 Verify if Option/Description/Condition column is displayed in Historical Costing Grid.</b></font>");
            CommonHelper.CaptureScreen();
            if (OptionBidCostPage.Instance.HistoricCostingPage.IsColumnFoundInGrid("Option/Description/Condition"))
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Option/Description/Condition column is found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Option/Description/Condition column is not found in the grid.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2 Verify if data is editable in Historical Costing page.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.EditFutureBidCostRecord(NewBuildingPhaseCode + "-" + NewBuildingPhaseName, 20);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3 Navigate to option bid cost page.</b></font>");
            OptionBidCostPage.Instance.HistoricCostingPage.ClickBackToPreviousBtn();

        }


        [TearDown]
        public void ClearData()
        {            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0 No Tear Down of Data because the existing data will re used in the Imports. Bid Costs Id are required.</b></font>");
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
