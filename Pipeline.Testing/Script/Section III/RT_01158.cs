using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.CommunityVendor;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
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
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Costing.OptionBidCost;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class D03_RT_01158 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private readonly int[] indexs = new int[] { };

        private const string BUILDINGPHASE1_NAME_DEFAULT = "Pre-RT-Option-Bid-Phase-01";
        private const string BUILDINGPHASE1_CODE_DEFAULT = "0366";

        private const string BUILDINGPHASE2_NAME_DEFAULT = "Pre-RT-Option-Bid-Phase-02";
        private const string BUILDINGPHASE2_CODE_DEFAULT = "0367";

        private const string BUILDINGPHASE3_NAME_DEFAULT = "Pre-RT-Option-Bid-Phase-03";
        private const string BUILDINGPHASE3_CODE_DEFAULT = "0368";

        private const string BUILDINGPHASE4_NAME_DEFAULT = "Pre-RT-Option-Bid-Phase-04";
        private const string BUILDINGPHASE4_CODE_DEFAULT = "0369";

        private const string BUILDINGPHASE5_NAME_DEFAULT = "Pre-RT-Option-Bid-Phase-05";
        private const string BUILDINGPHASE5_CODE_DEFAULT = "0370";


        private VendorData newVendor;
        private const string NewVendorName = "QA_RT_VendorBid01";
        private const string NewVendorCode = "Bid01";

        private const string NewOptionName1 = "QA_RT_OptionBid01_Automation";
        private const string NewOptionNumber1 = "Bid01";

        private const string NewOptionName2 = "QA_RT_OptionBid02_Automation";
        private const string NewOptionNumber2 = "Bid02";

        private const string NewOptionName3 = "QA_RT_OptionBid03_Automation";
        private const string NewOptionNumber3 = "Bid03";

        private const string NewOptionName4 = "QA_RT_OptionBid04_Automation";
        private const string NewOptionNumber4 = "Bid04";

        private const string NewOptionName5 = "QA_RT_OptionBid05_Automation";
        private const string NewOptionNumber5 = "Bid05";

        private const string OPTION = "OPTION";

        string[] OptionData = { NewOptionName5 };

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "Hai Nguyen Building Group";
        private const string NewBuildingGroupCode = "101";
        private const string NewBuildingGroupDescription = "Testing Create a Building Group Lorem ipsum dolor sit amet; consectetur adipiscing elit. Proin facilisis ac augue et accumsan metus.";

        private const string NewHouseName2 = "QA_RT_HouseBid02_Automation";
        private const string NewHousePlanNo2 = "Bid2";

        private const string NewHouseName3 = "QA_RT_HouseBid03_Automation";
        private const string NewHousePlanNo3 = "Bid3";

        private const string NewHouseName4 = "QA_RT_HouseBid04_Automation";
        private const string NewHousePlanNo4 = "Bid4";

        private const string NewHouseName5 = "QA_RT_HouseBid05_Automation";
        private const string NewHousePlanNo5 = "Bid5";

        private const string NewCommunityName1 = "QA_RT_CommunityBid01_Automation";
        private const string NewCommunityName2 = "QA_RT_CommunityBid02_Automation";
        private const string NewCommunityName3 = "QA_RT_CommunityBid03_Automation";
        private const string NewCommunityName4 = "QA_RT_CommunityBid04_Automation";
        private const string NewCommunityName5 = "QA_RT_CommunityBid05_Automation";


        DivisionData newDivision;
        private const string DivisionName = "CG Visions";

        private static string OPTION_TIER = "Option";
        private static string HOUSE_TIER = "House";
        private static string COMMUNITY_TIER = "Community";
        private static string COMMUNITY_HOUSE_TIER = "Community House";

        private static string OPTION_VALUE = "QA_RT_OptionBid01_Automation";
        private static string HOUSE_VALUE = "QA_RT_HouseBid02_Automation";
        private static string COMMUNITY_VALUE = "QA_RT_CommunityBid03_Automation";
        private static string COMMUNITY_HOUSE_BUILDINGPHASE_VALUE = "0369-Pre-RT-Option-Bid-Phase-04";
        private static string JOB_BUILDINGPHASE_VALUE = "0370-Pre-RT-Option-Bid-Phase-05";

        OptionBidCostData _optionBidCost;
        JobData _jobdata;
        [SetUp]
        public void GetData()
        {


            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
            };

            newDivision = new DivisionData()
            {
                Name = DivisionName
            };


            _optionBidCost = new OptionBidCostData()
            {
                Tier1Option = "QA_RT_OptionBid01_Automation - Bid01",
                Tier2House = "Bid2-QA_RT_HouseBid02_Automation",
                Tier2Option = "QA_RT_OptionBid02_Automation - Bid02",
                Tier3Commmunity = "Automation_Bid03-QA_RT_CommunityBid03_Automation",
                Tier3Option = "QA_RT_OptionBid03_Automation - Bid03",
                Tier4Community = "Automation_Bid04-QA_RT_CommunityBid04_Automation",
                Tier4House = "Bid4-QA_RT_HouseBid04_Automation",
                Tier4Option = "QA_RT_OptionBid04_Automation - Bid04",
                Tier1Cost = "10",
                Tier2Cost = "20",
                Tier3Cost = "30",
                Tier4Cost = "40",
                Job = "QA_RT_JobBid_Automation",
                JobOption = "QA_RT_OptionBid05_Automation - Bid05",
                JobCost = "50",
                JobCost2 = "0",
                Vendor = "QA_RT_VendorBid01",
                BuildingPhase1 = "0366-Pre-RT-Option-Bid-Phase-01",
                BuildingPhase2 = "0367-Pre-RT-Option-Bid-Phase-02",
                BuildingPhase3 = "0368-Pre-RT-Option-Bid-Phase-03",
                BuildingPhase4 = "0369-Pre-RT-Option-Bid-Phase-04",
                BuildingPhase5 = "0370-Pre-RT-Option-Bid-Phase-05"

            };

            _jobdata = new JobData()
            {
                Name = "QA_RT_JobBid_Automation",
                Community = "Automation_Bid05-QA_RT_CommunityBid05_Automation",
                House = "Bid5-QA_RT_HouseBid05_Automation",
                Lot = "_111 - Sold",
                Orientation = "Left",
            };

            
             // Prepare a new Building Group to import Product
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
             BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

             BuildingGroupData buildingGroupData = new BuildingGroupData()
             {
                 Code = "101",
                 Name = "Hai Nguyen Building Group",
                 Description= "Testing Create a Building Group Lorem ipsum dolor sit amet; consectetur adipiscing elit. Proin facilisis ac augue et accumsan metus."
             };
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
             if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
             {
                 // Open a new tab and create a new Category
                 BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
             }

             //Prepare data: Import Building Phase to import Product
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
             if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                 ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

             string importFile = "\\DataInputFiles\\Import\\RT_01158\\Pipeline_BuildingPhases.csv";
             ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

             //Import House
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Import House.</b></font>");
             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
             if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_IMPORT) is false)
                 ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_IMPORT} grid view to import House.</font>");

             importFile = "\\DataInputFiles\\Import\\RT_01158\\Pipeline_Houses.csv";
             BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_IMPORT, importFile);


             //Import Option
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Import Option.</b></font>");
             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_OPTION);
             if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_IMPORT) is false)
                 ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_IMPORT} grid view to import new Options.</font>");

             string importOptionFile = "\\DataInputFiles\\Import\\RT_01158\\Pipeline_Options.csv";
             BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importOptionFile);

             //Create the Vendor
             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_VENDORS_URL);

             VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newVendor.Name);
             if (VendorPage.Instance.IsItemInGrid("Name", newVendor.Name) is false)
             {

                 VendorPage.Instance.ClickAddToVendorIcon();
                 VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);

                 ExtentReportsHelper.LogInformation(null, "Verify new Vendor in header");
                 if (VendorDetailPage.Instance.IsCreateSuccessfully(newVendor) is false)
                 {
                     ExtentReportsHelper.LogFail($"<font color='red'>Create new Vendor unsuccessfully.</font>");
                 }
                 else
                 {
                     ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create Vendor Product.</b></font>");
                 }

             }
             else
             {
                 VendorPage.Instance.SelectVendor("Name", NewVendorName);
             }

             //Add Building phase in the Vendor

             VendorDetailPage.Instance.LeftMenuNavigation("Building Phases");
             VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE1_NAME_DEFAULT);
             if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT) is false)
             {
                 VendorBuildingPhasePage.Instance.AddBuildingPhase(BUILDINGPHASE1_CODE_DEFAULT);
                 VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();
             }

             VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE2_NAME_DEFAULT);
             if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE2_CODE_DEFAULT + "-" + BUILDINGPHASE2_NAME_DEFAULT) is false)
             {
                 VendorBuildingPhasePage.Instance.AddBuildingPhase(BUILDINGPHASE2_CODE_DEFAULT);
                 VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();
             }


             VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE3_NAME_DEFAULT);
             if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE3_CODE_DEFAULT + "-" + BUILDINGPHASE3_NAME_DEFAULT) is false)
             {
                 VendorBuildingPhasePage.Instance.AddBuildingPhase(BUILDINGPHASE3_CODE_DEFAULT);
                 VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();
             }


             VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE4_NAME_DEFAULT);
             if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE4_CODE_DEFAULT + "-" + BUILDINGPHASE4_NAME_DEFAULT) is false)
             {
                 VendorBuildingPhasePage.Instance.AddBuildingPhase(BUILDINGPHASE4_CODE_DEFAULT);
                 VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();
             }

              VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE5_NAME_DEFAULT);
             if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE5_CODE_DEFAULT + "-" + BUILDINGPHASE5_NAME_DEFAULT) is false)
             {
                 VendorBuildingPhasePage.Instance.AddBuildingPhase(BUILDINGPHASE5_CODE_DEFAULT);
                 VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();
             }

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Import House.</b></font>");
             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
             if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_IMPORT) is false)
                 ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_IMPORT} grid view to import new Options.</font>");

             importFile = "\\DataInputFiles\\Import\\RT_01158\\Pipeline_Houses.csv";
             BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_IMPORT, importFile);

             //setup house data and add option to house -> TIER 2
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Create a new Series.</b></font>");
             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);
             SeriesData seriesData = new SeriesData()
             {
                 Name = "QA_RT_Serie3_Automation",
                 Code = "",
                 Description = "Please no not remove or modify"
             };

             SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
             if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
             {
                 // Create a new series
                 SeriesPage.Instance.CreateSeries(seriesData);
             }

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add new House.</b></font>");
             HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
             HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewHouseName2);
             if (HousePage.Instance.IsItemInGrid("Name", NewHouseName2) is true)
             {
                 HousePage.Instance.SelectItemInGridWithTextContains("Name", NewHouseName2);
             }

             HouseDetailPage.Instance.LeftMenuNavigation("Options");

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add option to House.</b></font>");
             HouseOptionDetailPage.Instance.FilterItemInOptionnGrid("Name", GridFilterOperator.EqualTo, NewOptionName2);
             if (HouseOptionDetailPage.Instance.IsItemInHouseOptionGrid("Name", NewOptionName2) is false)
             {
                 HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(NewOptionName2 + " - " + NewOptionNumber2);
             }



             //setup community data and add option to community -> TIER 3
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add new Community.</b></font>");
             // Make sure these communities are existing, to import existing one on step 2.2
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Make sure these communities are existing by importing</b></font>");
             CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
             CommonHelper.SwitchLastestTab();

             if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_IMPORT) is false)
                 ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_IMPORT} grid view to import new Options.</font>");

             importFile = "\\DataInputFiles\\Import\\RT_01158\\Pipeline_Communities.csv";
             BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_IMPORT, importFile);

             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName3);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName3) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName3);
             }

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add option to Community.</b></font>");
             CommunityDetailPage.Instance.LeftMenuNavigation("Options");
             string[] OptionDataBid03 = { NewOptionName3};

             CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, NewOptionName3);
             if (CommunityOptionPage.Instance.IsItemInGrid("Option", NewOptionName3) is false)
             {
                 CommunityOptionPage.Instance.AddCommunityOption(OptionDataBid03);
             }

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add community to House.</b></font>");
             HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
             HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewHouseName3);
             if (HousePage.Instance.IsItemInGrid("Name", NewHouseName3) is true)
             {
                 HousePage.Instance.SelectItemInGridWithTextContains("Name", NewHouseName3);
                 HouseDetailPage.Instance.LeftMenuNavigation("Communities");
                 HouseCommunities.Instance.FillterOnGrid("Name", NewCommunityName3);
                 if (HouseCommunities.Instance.IsValueOnGrid("Name", NewCommunityName3) is false)
                 {
                     HouseCommunities.Instance.AddButtonCommunities();
                     HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(NewCommunityName3, 0);
                 }

                 HouseCommunities.Instance.LeftMenuNavigation("Options");

                 if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", NewOptionName3) is false)
                 {
                     HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(NewOptionName3 + " - " + NewOptionNumber3);
                 }

             }


             //Add community to division
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add community to division.</b></font>");
             DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
             DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, DivisionName);
             if (DivisionPage.Instance.IsItemInGrid("Division", DivisionName) is true)
             {
                 DivisionPage.Instance.SelectItemInGrid("Division", DivisionName);

             }
             else
             {
                 // Create a new one if it doesn't exist
                 DivisionPage.Instance.CreateDivision(newDivision);
             }

             DivisionDetailPage.Instance.LeftMenuNavigation("Communities");
             DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName3);
             if (DivisionCommunityPage.Instance.IsItemInGrid("Name", NewCommunityName3) is false)
             {
                 string[] communities = { NewCommunityName3 };
                 DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                 DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                 DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
             }

             //add vendor to division
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to division.</b></font>");
             DivisionCommunityPage.Instance.LeftMenuNavigation("Vendors");
             DivisionVendorPage.Instance.FilterItemInDivisionVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
             if (DivisionVendorPage.Instance.IsItemInDivisionVendorGrid("Name", NewVendorName) is false)
             {
                 string[] vendors = { NewVendorName };
                 DivisionVendorPage.Instance.OpenDivisionVendorModal();
                 DivisionVendorPage.Instance.DivisionVendorModal.SelectDivisionVendor(vendors);
                 DivisionVendorPage.Instance.DivisionVendorModal.Save();
             }

             //add vendor to community > costing> vendor assignments
             DivisionVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE3_NAME_DEFAULT);
             if (DivisionVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE3_NAME_DEFAULT) is true)
             {
                 DivisionVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE3_NAME_DEFAULT, NewVendorName);
             }


             //add vendor to community
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community.</b></font>");
             CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName3);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName3) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName3);
                 CommunityDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                 CommunityVendorPage.Instance.FilterItemInCommunityVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
                 if (CommunityVendorPage.Instance.IsItemInCommunityVendorGrid("Name", NewVendorName) is false)
                 {
                     string[] vendors = { NewVendorName };
                     CommunityVendorPage.Instance.OpenCommunityVendorModal();
                     CommunityVendorPage.Instance.CommunityVendorModal.SelectCommunityVendor(vendors);
                     CommunityVendorPage.Instance.CommunityVendorModal.Save();
                 }

                 //add vendor to community > costing> vendor assignments
                 ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community > costing> vendor assignments.</b></font>");
                 CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE3_NAME_DEFAULT);
                 if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE3_NAME_DEFAULT + "-" + BUILDINGPHASE3_NAME_DEFAULT) is true)
                 {
                     CommunityVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE3_CODE_DEFAULT, NewVendorName);
                 }
             }
             //add vendor to community
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community.</b></font>");
             CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName3);
             System.Threading.Thread.Sleep(2000);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName3) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName3);
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
                 CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE3_NAME_DEFAULT);
                 System.Threading.Thread.Sleep(2000);
                 if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE3_CODE_DEFAULT + "-" + BUILDINGPHASE3_NAME_DEFAULT) is true)
                 {
                     CommunityVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE3_NAME_DEFAULT, NewVendorName);
                 }
             }

             //setup community data and add option to community -> TIER 4
             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName4);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName4) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName4);
             }

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add option to Community.</b></font>");
             CommunityDetailPage.Instance.LeftMenuNavigation("Options");
             string[] OptionDataBid04 = { NewOptionName4 };

             CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.EqualTo, NewOptionName4);
             if (CommunityOptionPage.Instance.IsItemInGrid("Option", NewOptionName4) is false)
             {
                 CommunityOptionPage.Instance.AddCommunityOption(OptionDataBid04);
             }

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add community to House.</b></font>");
             HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
             HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewHouseName4);
             if (HousePage.Instance.IsItemInGrid("Name", NewHouseName4) is true)
             {
                 HousePage.Instance.SelectItemInGridWithTextContains("Name", NewHouseName4);
                 HouseDetailPage.Instance.LeftMenuNavigation("Communities");
                 HouseCommunities.Instance.FillterOnGrid("Name", NewCommunityName4);
                 if (HouseCommunities.Instance.IsValueOnGrid("Name", NewCommunityName4) is false)
                 {
                     HouseCommunities.Instance.AddButtonCommunities();
                     HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(NewCommunityName4, 0);
                 }

                 HouseCommunities.Instance.LeftMenuNavigation("Options");

                 if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", NewOptionName4) is false)
                 {
                     HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(NewOptionName4 + " - " + NewOptionNumber4);
                 }

             }


             //Add community to division
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add community to division.</b></font>");
             DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
             DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, DivisionName);
             if (DivisionPage.Instance.IsItemInGrid("Division", DivisionName) is true)
             {
                 DivisionPage.Instance.SelectItemInGrid("Division", DivisionName);

             }
             else
             {
                 // Create a new one if it doesn't exist
                 DivisionPage.Instance.CreateDivision(newDivision);
             }

             DivisionDetailPage.Instance.LeftMenuNavigation("Communities");
             DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName4);
             if (DivisionCommunityPage.Instance.IsItemInGrid("Name", NewCommunityName4) is false)
             {
                 string[] communities = { NewCommunityName4 };
                 DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                 DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                 DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
             }

             //add vendor to division
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to division.</b></font>");
             DivisionCommunityPage.Instance.LeftMenuNavigation("Vendors");
             DivisionVendorPage.Instance.FilterItemInDivisionVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
             if (DivisionVendorPage.Instance.IsItemInDivisionVendorGrid("Name", NewVendorName) is false)
             {
                 string[] vendors = { NewVendorName };
                 DivisionVendorPage.Instance.OpenDivisionVendorModal();
                 DivisionVendorPage.Instance.DivisionVendorModal.SelectDivisionVendor(vendors);
                 DivisionVendorPage.Instance.DivisionVendorModal.Save();
             }

             //add vendor to community > costing> vendor assignments
             DivisionVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE4_NAME_DEFAULT);
             if (DivisionVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE4_NAME_DEFAULT) is true)
             {
                 DivisionVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE4_NAME_DEFAULT, NewVendorName);
             }


             //add vendor to community
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community.</b></font>");
             CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName4);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName4) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName4);
                 CommunityDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                 CommunityVendorPage.Instance.FilterItemInCommunityVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
                 if (CommunityVendorPage.Instance.IsItemInCommunityVendorGrid("Name", NewVendorName) is false)
                 {
                     string[] vendors = { NewVendorName };
                     CommunityVendorPage.Instance.OpenCommunityVendorModal();
                     CommunityVendorPage.Instance.CommunityVendorModal.SelectCommunityVendor(vendors);
                     CommunityVendorPage.Instance.CommunityVendorModal.Save();
                 }

                 //add vendor to community > costing> vendor assignments
                 ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community > costing> vendor assignments.</b></font>");
                 CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE4_NAME_DEFAULT);
                 if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE4_CODE_DEFAULT + "-" + BUILDINGPHASE4_NAME_DEFAULT) is true)
                 {
                     CommunityVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE4_NAME_DEFAULT, NewVendorName);
                 }
             }
             //add vendor to community
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community.</b></font>");
             CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName4);
             System.Threading.Thread.Sleep(2000);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName4) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName4);
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
                 CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE4_NAME_DEFAULT);
                 System.Threading.Thread.Sleep(2000);
                 if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE4_CODE_DEFAULT + "-" + BUILDINGPHASE4_NAME_DEFAULT) is true)
                 {
                     CommunityVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE4_NAME_DEFAULT, NewVendorName);
                 }
             }



             //setup community data and add option to community -> TIER 5
             CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName5);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName5) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName5);
             }

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add option to Community.</b></font>");
             CommunityDetailPage.Instance.LeftMenuNavigation("Options");
             string[] OptionDataBid05 = { NewOptionName5 };

             CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.EqualTo, NewOptionName5);
             if (CommunityOptionPage.Instance.IsItemInGrid("Option", NewOptionName5) is false)
             {
                 CommunityOptionPage.Instance.AddCommunityOption(OptionDataBid05);
             }

             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add community to House.</b></font>");
             HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
             HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewHouseName5);
             if (HousePage.Instance.IsItemInGrid("Name", NewHouseName5) is true)
             {
                 HousePage.Instance.SelectItemInGridWithTextContains("Name", NewHouseName5);
                 HouseDetailPage.Instance.LeftMenuNavigation("Communities");
                 HouseCommunities.Instance.FillterOnGrid("Name", NewCommunityName5);
                 if (HouseCommunities.Instance.IsValueOnGrid("Name", NewCommunityName5) is false)
                 {
                     HouseCommunities.Instance.AddButtonCommunities();
                     HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(NewCommunityName5, 0);
                 }

                 HouseCommunities.Instance.LeftMenuNavigation("Options");

                 if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", NewOptionName5) is false)
                 {
                     HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(NewOptionName5 + " - " + NewOptionNumber5);
                 }

             }


             //Add community to division
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add community to division.</b></font>");
             DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
             DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, DivisionName);
             if (DivisionPage.Instance.IsItemInGrid("Division", DivisionName) is true)
             {
                 DivisionPage.Instance.SelectItemInGrid("Division", DivisionName);

             }

             DivisionDetailPage.Instance.LeftMenuNavigation("Communities");
             DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName5);
             if (DivisionCommunityPage.Instance.IsItemInGrid("Name", NewCommunityName5) is false)
             {
                 string[] communities = { NewCommunityName5 };
                 DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                 DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                 DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
             }
             DivisionCommunityPage.Instance.LeftMenuNavigation("Vendors");
             //add vendor to community > costing> vendor assignments
             DivisionVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE5_NAME_DEFAULT);
             if (DivisionVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE5_NAME_DEFAULT) is true)
             {
                 DivisionVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE5_NAME_DEFAULT, NewVendorName);
             }


             //add vendor to community
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community.</b></font>");
             CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewCommunityName5);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName5) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName5);
                 CommunityDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                 CommunityVendorPage.Instance.FilterItemInCommunityVendorGrid("Name", GridFilterOperator.EqualTo, NewVendorName);
                 if (CommunityVendorPage.Instance.IsItemInCommunityVendorGrid("Name", NewVendorName) is false)
                 {
                     string[] vendors = { NewVendorName };
                     CommunityVendorPage.Instance.OpenCommunityVendorModal();
                     CommunityVendorPage.Instance.CommunityVendorModal.SelectCommunityVendor(vendors);
                     CommunityVendorPage.Instance.CommunityVendorModal.Save();
                 }

                 //add vendor to community > costing> vendor assignments
                 ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community > costing> vendor assignments.</b></font>");
                 CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE5_NAME_DEFAULT);
                 if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE5_CODE_DEFAULT + "-" + BUILDINGPHASE5_NAME_DEFAULT) is true)
                 {
                     CommunityVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE5_NAME_DEFAULT, NewVendorName);
                 }
             }
             //add vendor to community
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add vendor to community.</b></font>");
             CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName5);
             System.Threading.Thread.Sleep(2000);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName5) is true)
             {
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName5);
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
                 CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE5_NAME_DEFAULT);
                 System.Threading.Thread.Sleep(2000);
                 if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDINGPHASE5_CODE_DEFAULT + "-" + BUILDINGPHASE5_NAME_DEFAULT) is true)
                 {
                     CommunityVendorPage.Instance.EditVendorAssignments(BUILDINGPHASE5_NAME_DEFAULT, NewVendorName);
                 }
             }

             CommonHelper.RefreshPage();

             ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Turn OFF Sage 300 and MS NAV.<b></b></font>");
             CommunityPage.Instance.SetSage300AndNAVStatus(false);

             ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.2: Open Lot page, verify Lot button displays or not.<b></b></font>");
             CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

             // Try to open Lot URL of any community and verify Add lot button
             CommonHelper.OpenURL(BaseDashboardUrl + "/Builder/Communities/Lots.aspx?cid=1");
             if (LotPage.Instance.IsAddLotButtonDisplay() is false)
             {
                 ExtentReportsHelper.LogWarning(null, $"<font color='lavender'><b>Add lot button doesn't display to continue testing. Stop this test script.</b></font>");
                 Assert.Ignore("Add lot button doesn't display after set NAV and Sage 300 to Running. Stop this test script");
             }


             //Navigate To Community Page
             ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Community default page.</font>");
             CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

             ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter community with name {NewCommunityName5} and create if it doesn't exist.</b></font>");
             CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewCommunityName5);
             if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName5) is true)
             {
                 //Select Community with Name
                 CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName5);
             }

             LotData _lotdata = new LotData()
             {
                 Number = "_111",
                 Status = "Sold"
             };

             //Naviage To Community Lot
             CommunityDetailPage.Instance.LeftMenuNavigation("Lots");
             string LotPageUrl = LotPage.Instance.CurrentURL;
             if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
             {
                 ExtentReportsHelper.LogInformation($"Lot with Number {_lotdata.Number} and Status {_lotdata.Status} is displayed in grid");
             }
             else
             {
                 //Import Lot in Community
                 ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Open Import page.</b></font>");
                 CommonHelper.SwitchLastestTab();
                 LotPage.Instance.ImportExporFromMoreMenu("Import");
                 string importFileDir = "Pipeline_Lots_In_Community.csv";
                 LotPage.Instance.ImportFile("Lot Import", $"\\DataInputFiles\\Import\\RT_01158\\{importFileDir}");
                 CommonHelper.OpenURL(LotPageUrl);

                 //Check Lot Numbet in grid 
                 if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
                 {
                     ExtentReportsHelper.LogPass("Import Lot File is successful");
                 }
                 else
                 {
                     ExtentReportsHelper.LogFail("Import Lot File is unsuccessful");
                 }
             }




             JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

             // Step 2 - 3: Populate all values

             JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
             JobPage.Instance.FilterItemInGrid("Job Number", _jobdata.Name);
             if (JobPage.Instance.IsItemInGrid("Job Number", _jobdata.Name) is true)
             {
                JobPage.Instance.SelectItemInGrid("Job Number", _jobdata.Name);
             }
             else
             {
                 JobPage.Instance.CreateJob(_jobdata);
             }

              ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Job/ Options page. Add Option '{NewOptionName5}' to job if it doesn't exist.</b></font>");

             JobDetailPage.Instance.LeftMenuNavigation("Options", false);
             if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
             {
                 ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
             }
             else
             {
                 ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
             }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", NewOptionName5) is false)
            {
                string selectedOption = NewOptionNumber5 + "-" + NewOptionName5;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }

             // Close current tab
             CommonHelper.CloseAllTabsExcludeCurrentOne();
             
            
        }

        [Test]
        [Category("Section_III")]
        public void D04_Costing_AddAnOptionBidCost()
        {

            //I. Setup the data
            //1. Create the data

            ExtentReportsHelper.LogInformation("1. Go to the Option Bid Cost page");
            OptionBidCostPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.OptionBidCost);

            ExtentReportsHelper.LogInformation($"2. Select {NewVendorName} ");
            OptionBidCostPage.Instance.SelectVendor(NewVendorName);
            
            //Verify the error message display when adding the Cost Tier without having any allowance value
            ExtentReportsHelper.LogInformation("3. Click 'Add' button on the 1st grid");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();

            ExtentReportsHelper.LogInformation(" Cost Tier 1: Select the Option and Buildingphase > Input Bid cost and save");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 1 - Option");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier1Option(_optionBidCost.Tier1Option);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(_optionBidCost.BuildingPhase1);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(double.Parse(_optionBidCost.Tier1Cost));
            var actualErrorMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            string expectedErrorMessage = "Could not add Bid Cost because an allowance has not been setup for that Option/Building Phase.";
            // Verify create new Bid Cost
            if (actualErrorMessage == expectedErrorMessage)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }

            //Cost Tier 2: Select the House, Option and Buildingphase > Input Bid cost and save

            ExtentReportsHelper.LogPass("<font color='lavender'><b>Cost Tier 2: Select the House, Option and Buildingphase > Input Bid cost and save</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 2 - House");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2House(_optionBidCost.Tier2House);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2Option(_optionBidCost.Tier2Option);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(_optionBidCost.BuildingPhase2);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(double.Parse(_optionBidCost.Tier2Cost));
            actualErrorMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            ExtentReportsHelper.LogInformation("10. Click on Save button. Verified the successfully toast message is displayed");
            // Verify create new Bid Cost
            if (actualErrorMessage == expectedErrorMessage)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }

            //Cost Tier 3: Select the Community, Option and Buildingphase > Input Bid cost and save
            ExtentReportsHelper.LogPass("<font color='lavender'><b>Cost Tier 3: Select the Community, Option and Buildingphase > Input Bid cost and save </b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 3 - Community");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Community(_optionBidCost.Tier3Commmunity);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Option(_optionBidCost.Tier3Option);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(_optionBidCost.BuildingPhase3);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(double.Parse(_optionBidCost.Tier3Cost));
            actualErrorMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            ExtentReportsHelper.LogInformation("13. Click on Save button. Verified the successfully toast message is displayed");
            // Verify create new Bid Cost
            if (actualErrorMessage == expectedErrorMessage)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }

            //Cost Tier 4: Select the Community, House, Option and Buildingphase > Input Bid cost and save
            ExtentReportsHelper.LogPass("<font color='lavender'><b>Cost Tier 4: Select the Community, House, Option and Buildingphase > Input Bid cost and save</b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 4 - Community House");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Community(_optionBidCost.Tier4Community);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4House(_optionBidCost.Tier4House);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Option(_optionBidCost.Tier4Option);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(_optionBidCost.BuildingPhase4);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(double.Parse(_optionBidCost.Tier4Cost));
            actualErrorMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();

            // Verify create new Bid Cost
            if (actualErrorMessage == expectedErrorMessage)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }


            //1. Verify User can add Allowance on the “Add Bid Cost” popup 
            //Tier 1 and Tier3 used the same allowance
            ExtentReportsHelper.LogPass("<font color='lavender'><b>1. Verify User can add Allowance on the “Add Bid Cost” popup </ b></font>");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 1 - Option");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier1Option(NewOptionName1 + " - " + NewOptionNumber1);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT);
              
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


            // Edit Bid Cost
            ExtentReportsHelper.LogInformation("Click the Edit button, verify the Edit function.");
            string actualMessageEditOption = OptionBidCostPage.Instance.EditItemBidCostGrid("Building Phase", _optionBidCost.BuildingPhase1, "11", "Tier1", OPTION_TIER);
            if (actualMessageEditOption == "Cost updated for Cost Tier 1.")
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Edited successfully, received a toast message </b></font>");
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot edit Cost Tier 1.Actual expect: {actualMessageEditOption}</b></font>");



            //================= Select Tier 2 on the Cost Tier dropdown list and select item in modal. =======================//

            ExtentReportsHelper.LogInformation("Select Tier 2 on the Cost Tier dropdown list and select item in modal.");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 2 - House");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2House(_optionBidCost.Tier2House);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier2Option(_optionBidCost.Tier2Option);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(_optionBidCost.BuildingPhase2);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(double.Parse(_optionBidCost.Tier2Cost));
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            ExtentReportsHelper.LogInformation("10. Click on Save button. Verified the successfully toast message is displayed");
            // Verify create new Bid Cost
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");


            OptionBidCostPage.Instance.CloseToastMessage();
            ExtentReportsHelper.LogInformation("11. Click the Edit button, verify the Edit function.");

            OptionBidCostPage.Instance.EditItemBidCostGrid("House", HOUSE_VALUE, "22", "Tier2", HOUSE_TIER);
            string actualMessageEditHouse = OptionBidCostPage.Instance.GetLastestToastMessage();
            if (actualMessageEditHouse == "Cost updated for Cost Tier 1. Cost updated for Cost Tier 2.")
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Edited successfully, received a toast message </b></font>");
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot edit Cost Tier 2. Actual expect: {actualMessageEditHouse}</b></font>");


            //--------------------Select Tier 3 on the Cost Tier dropdown list and select item in modal.------------------//

            ExtentReportsHelper.LogInformation("12. Select Tier 3 on the Cost Tier dropdown list and select item in modal.");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 3 - Community");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Community(_optionBidCost.Tier3Commmunity);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier3Option(_optionBidCost.Tier3Option);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(_optionBidCost.BuildingPhase3);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(double.Parse(_optionBidCost.Tier3Cost));
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();
            ExtentReportsHelper.LogInformation("13. Click on Save button. Verified the successfully toast message is displayed");
            // Verify create new Bid Cost
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");

            OptionBidCostPage.Instance.CloseToastMessage();
            
            //Edit tier 3
            ExtentReportsHelper.LogInformation("Click the Edit button, verify the Edit function.");
            OptionBidCostPage.Instance.EditItemBidCostGrid("Community", COMMUNITY_VALUE, "33", "Tier3", COMMUNITY_TIER);
            string actualMessageEditCommunity = OptionBidCostPage.Instance.GetLastestToastMessage();
            if (actualMessageEditCommunity == "Cost updated for Cost Tier 1. Cost updated for Cost Tier 3.")
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Edited successfully, received a toast message </b></font>");
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot edit Cost Tier 3. Actual expect: {actualMessageEditCommunity}</b></font>");

            //========================= Select Tier 4 on the Cost Tier dropdown list and select item in modal =========================//

            ExtentReportsHelper.LogInformation("Select Tier 4 on the Cost Tier dropdown list and select item in modal.");
            OptionBidCostPage.Instance.ClickAddToOpenBidCostModal();
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectCostTier("Tier 4 - Community House");
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Community(_optionBidCost.Tier4Community);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4House(_optionBidCost.Tier4House);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectTier4Option(_optionBidCost.Tier4Option);
            OptionBidCostPage.Instance.AddOptionBidCostModal.SelectBuildingPhase(_optionBidCost.BuildingPhase4);
            OptionBidCostPage.Instance.AddOptionBidCostModal.EnterBidCostAmount(double.Parse(_optionBidCost.Tier4Cost));
            actualMessage = OptionBidCostPage.Instance.AddOptionBidCostModal.Save();

            ExtentReportsHelper.LogInformation("16. Click on Save button. Verified the successfully toast message is displayed");
            // Verify create new Bid Cost
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");

            string actualMessageEditCommunityHouse = OptionBidCostPage.Instance.EditItemBidCostGrid("Building Phase", COMMUNITY_HOUSE_BUILDINGPHASE_VALUE, "44", "Tier4", COMMUNITY_HOUSE_TIER);
            if (actualMessageEditCommunityHouse == "Cost updated for Cost Tier 1. Cost updated for Cost Tier 2. Cost updated for Cost Tier 3. Cost updated for Cost Tier 4.")
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Edited successfully, received a toast message </b></font>");
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot edit Cost Tier 4. Actual expect: {actualMessageEditCommunityHouse}</b></font>");


            //6/Verify User can delete Cost tier.
            ExtentReportsHelper.LogInformation("28. Remove a Option Bid Cost");
            OptionBidCostPage.Instance.SelectTier(OPTION_TIER);
            if ((OptionBidCostPage.Instance.IsItemInBidCostGrid("Building Phase", _optionBidCost.BuildingPhase1)))
            {
                string actualMessageRemovedOption = OptionBidCostPage.Instance.RemoveItemBidCostGrid("Building Phase", _optionBidCost.BuildingPhase1);
                if (actualMessageRemovedOption == "Cost removed for Cost Tier 1.")
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b> The Option bid cost is removed out of grid data; received a toast message </b></font>");
                }
                else
                    ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot remove Cost Tier 1 . Actual Result: {actualMessageRemovedOption}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>The Option bid cost is not present in grid</b></font>");
            }

            OptionBidCostPage.Instance.SelectTier(HOUSE_TIER);
            if ((OptionBidCostPage.Instance.IsItemInBidCostGrid("House", HOUSE_VALUE)))
            {
                string actualMessageRemovedHouse = OptionBidCostPage.Instance.RemoveItemBidCostGrid("House", HOUSE_VALUE);
                if (actualMessageRemovedHouse == "Cost removed for Cost Tier 2.")
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b> The House bid cost is removed out of grid data; received a toast message </b></font>");
                }
                else
                    ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot remove Cost Tier 2 .Actual Result:{actualMessageRemovedHouse} </b></font>");
            }

            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>The House bid cost is not present in grid</b></font>");
            }

            OptionBidCostPage.Instance.SelectTier(COMMUNITY_TIER);
            if ((OptionBidCostPage.Instance.IsItemInBidCostGrid("Community", COMMUNITY_VALUE)))
            {
                string actualMessageRemovedCommunity = OptionBidCostPage.Instance.RemoveItemBidCostGrid("Community", COMMUNITY_VALUE);
                if (actualMessageRemovedCommunity == "Cost removed for Cost Tier 3.")
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b> The Community bid cost is removed out of grid data; received a toast message </b></font>");
                }
                else
                    ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot remove Cost Tier 3. Actual Result:{actualMessageRemovedCommunity} </b></font>");
            }

            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>The Community bid cost is not present in grid</b></font>");
            }
            OptionBidCostPage.Instance.SelectTier(COMMUNITY_HOUSE_TIER);
            if ((OptionBidCostPage.Instance.IsItemInBidCostGrid("Building Phase", COMMUNITY_HOUSE_BUILDINGPHASE_VALUE)))
            {
                string actualMessageRemovedCommunityHouse = OptionBidCostPage.Instance.RemoveItemBidCostGrid("Building Phase", COMMUNITY_HOUSE_BUILDINGPHASE_VALUE);
                if (actualMessageRemovedCommunityHouse == "Cost removed for Cost Tier 4.")
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b> The Community House bid cost is removed out of grid data; received a toast message </b></font>");
                }
                else
                    ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot remove Cost Tier 4. Actual Result:{actualMessageRemovedCommunityHouse} </b></font>");
            }

            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>The Community House bid cost is not present in grid</b></font>");
            }

            //III.Verify the 2nd grid

            //1. Add Job Bid cost successfully

            ExtentReportsHelper.LogInformation("21,22. Scroll to the 2nd grid, click on add button. Verified the 'Add Bid Cost' modal is displayed and select item in modal");
            OptionBidCostPage.Instance.ClickAddToOpenJobBidCostModal();

            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectJob(_optionBidCost.Job);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectOption(_optionBidCost.JobOption);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.SelectBuildingPhase(_optionBidCost.BuildingPhase5);
            OptionBidCostPage.Instance.AddJobOptionBidCostModal.EnterBidCostAmount(double.Parse(_optionBidCost.JobCost));
            actualMessage = OptionBidCostPage.Instance.AddJobOptionBidCostModal.Save();
            ExtentReportsHelper.LogInformation("23. Click on Save button");

            // Verify create new Job Cost
            if (actualMessage == expectedMessageSuccess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Save succcessfully and received toast messsage </b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b> Cannot create new Bid Cost </b></font>");

            string actualMessageEditJob = OptionBidCostPage.Instance.EditItemJobBidCostGrid("Building Phase", JOB_BUILDINGPHASE_VALUE, "55", "Tier5");

            if (actualMessageEditJob == "Cost updated for Cost Tier 1. Cost updated for Cost Tier 2. Cost updated for Cost Tier 3. Cost updated for Cost Tier 4. Cost updated for Cost Tier 5.")
            {
                ExtentReportsHelper.LogPass("<font color='green'><b> Edited successfully, received a toast message </b></font>");
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot edit Cost Tier 5.Actual expect: {actualMessageEditJob}</b></font>");

            if ((OptionBidCostPage.Instance.IsItemInJobBidCostGrid("Building Phase", JOB_BUILDINGPHASE_VALUE)))
            {
                string actualMessageRemovedJob = OptionBidCostPage.Instance.RemoveItemJobBidCostGrid("Building Phase", JOB_BUILDINGPHASE_VALUE);
                if (actualMessageRemovedJob == "Cost removed for Cost Tier 1. Cost removed for Cost Tier 2. Cost removed for Cost Tier 3. Cost removed for Cost Tier 4. Cost removed for Cost Tier 5.")
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b> The job bid cost is removed out of grid data; received a toast message </b></font>");
                }
                else
                    ExtentReportsHelper.LogFail($"<font color='red'><b> Cannot remove Cost Tier 5. Actual Result:{actualMessageRemovedJob} </b></font>");
            }

            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>The Job bid cost is not present in grid</b></font>");
            }

        }
    }
}

