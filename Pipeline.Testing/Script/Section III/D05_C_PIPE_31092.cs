using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
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
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Costing.CostingEstimate;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_III
{
    public class D05_C_PIPE_31092 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private JobData jobdata;
        private SeriesData seriesData;
        private HouseData houseData;
        private CommunityData communityData;
        private BuildingGroupData buildingGroupData;
        private BuildingPhaseData buildingPhaseData;
        private ProductData productData;
        private VendorData vendorData;
        private TradesData tradeData;
        private DivisionData newDivision;
        private const string newDivisionName = "RT_QA_New_Division_D05C";
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_D05C";
        private const string NewBuildingGroupCode = "D05C";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_D05C";
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_D05C";
        private const string NewBuildingPhaseCode = "D05C";
        private const string baseOptionName = "BASE";
        private const string baseOptionNumber = "00001";
        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "D05C";
        private const string laborItemCost = "$10.00";
        private const string materialItemCost = "$20.00";

        private const string ExportJobEstimateCSV = "Export Job Estimate to CSV";
        private const string ExportJobEstimateXls = "Export Job Estimate to Excel";
        private string exportFileName = "";

        [SetUp]
        public void SetupTestData()
        {
            //create new series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Series_D05C",
                Code = "D05C",
                Description = "RT_QA_Series_D05C"
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.0 Add new Series test data.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, seriesData.Name);
            System.Threading.Thread.Sleep(5000);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                SeriesPage.Instance.CreateSeries(seriesData);
            }
            //create new house
            houseData = new HouseData()
            {
                PlanNumber = "D05C",
                HouseName = "RT_QA_House_D05C",
                SaleHouseName = "RT_QA_House_D05C",
                Series = "RT_QA_Series_D05C"
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Add new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(houseData);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Add BASE Option to new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Options");
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", baseOptionName) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(baseOptionName + " - " + baseOptionNumber);
                }
            }
            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };
            //create new community
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_D05C",
                Code = "RT_QA_Community_D05C"
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(communityData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add new community to new division.</b></font>");
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
            //add house to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new House to Community.</b></font>");
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
            string[] optionData = { baseOptionName + " - " + baseOptionNumber };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add Base option to new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
                CommunityDetailPage.Instance.LeftMenuNavigation("Options");
                if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", baseOptionName) is false)
                {
                    CommunityOptionPage.Instance.AddCommunityOption(optionData);
                }
                if (CommunityOptionPage.Instance.IsCommunityHouseOptionInGrid("Option", baseOptionName) is false)
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
            buildingGroupData = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };
            buildingPhaseData = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Add new Building Group test data.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add new Building Phase test data.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(buildingPhaseData.Code)
                                          .EnterPhaseName(buildingPhaseData.Name)
                                          .EnterAbbName(buildingPhaseData.AbbName)
                                          .EnterDescription(buildingPhaseData.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(buildingPhaseData.BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableYes();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Add new Vendor test data.</b></font>");
            vendorData = new VendorData()
            {
                Name = "RT_QA_New_Vendor_D05C",
                Code = "D05C"
            };
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendorData);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Add new Building Phase to Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                if (VendorBuildingPhasePage.Instance.IsItemExist(buildingPhaseData.Code) is false)
                {
                    VendorBuildingPhasePage.Instance.AddBuildingPhase(buildingPhaseData.Code);
                }
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Add new Product test data.</b></font>");
            productData = new ProductData()
            {
                Name = "RT_QA_New_Product_D05C",
                Style = genericStyle,
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE",
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
                Manufacture = genericManufacturer
            };
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, productData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is false)
            {
                ProductPage.Instance.CreateNewProduct(productData);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Add new Product to BASE OPTION.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, baseOptionName);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", baseOptionName) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", baseOptionName);
                OptionDetailPage.Instance.LeftMenuNavigation("Products", true);
                ProductsToOptionPage.Instance.FilterOptionQuantitiesInGrid("Product", GridFilterOperator.EqualTo, productData.Name);
                System.Threading.Thread.Sleep(5000);
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", productData.Name) is false)
                {
                    OptionQuantitiesData optionQuantitiesData = new OptionQuantitiesData()
                    {
                        OptionName = baseOptionName,
                        BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                        ProductName = productData.Name,
                        ProductDescription = "RT_QA_New_Product_D05C",
                        Style = genericStyle,
                        Condition = false,
                        Use = string.Empty,
                        Quantity = "100.00",
                        Source = "Pipeline"
                    };
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
                }
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9: Add new Vendor Product cost data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                VendorProductPage.Instance.UpdateCostingforVerdor(buildingPhaseData.Name, productCode, materialItemCost, laborItemCost);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10: Add new Trades test data.</b></font>");
            tradeData = new TradesData()
            {
                Code = "D05C",
                TradeName = "RT_QA_New_BuildingTrade_D05C",
                TradeDescription = "RT_QA_New_BuildingTrade_D05C",
                Vendor = vendorData.Name,
                BuildingPhases = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                SchedulingTasks = ""
            };
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(tradeData);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Assign vendor to trade at community level.</b></font>");
                TradesPage.Instance.ClickVendorAssignments();
                System.Threading.Thread.Sleep(2000);
                VendorAssignmentsPage.Instance.SelectDivision(newDivisionName, 1);
                System.Threading.Thread.Sleep(2000);
                string[] communities = { communityData.Name };
                VendorAssignmentsPage.Instance.SelectCommunities(communities);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.ClickLoadVendors();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.ClickLoadVendors();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, tradeData.TradeName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                string editCommunityVendorMsg = VendorAssignmentsPage.Instance.EditCommunityVendor(communityData.Name, vendorData.Name);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
            }

            jobdata = new JobData()
            {
                Name = "RT_QA_Job_D05C",
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Lot = "RT_QA_Lot_D05C"
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Create new Job.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is false)
            {
                JobPage.Instance.CreateJob(jobdata);
            }
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Approve job configurations if not yet approved.</b></font>");
                JobDetailPage.Instance.LeftMenuNavigation("Options", true);
                JobOptionPage.Instance.ClickApproveConfig();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Apply System Quantities if not yet applied.</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Quantities", true);
                JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Generate Job BOM.</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Job BOM", true);
                JobBOMPage.Instance.GenerateJobBOM();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Generate Job BOM and Estimates.</b></font>");
                JobBOMPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
            }
        }

        [Test]
        public void D05_C_Costing_Estimate_Job_Estimates()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Go to Costing Estimates Page.</b></font>");

            CostingEstimatesPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.CostingEstimate);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Click Job Estimates button.</b></font>");
            CostingEstimatesPage.Instance.ClickJobEstimates();
            CommonHelper.CaptureScreen();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Select Job " + jobdata.Name + ".</b></font>");
            CostingEstimatesPage.Instance.SelectJob(jobdata.Name);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Select latest Generated BOM.</b></font>");
            CostingEstimatesPage.Instance.SelectLatestGeneratedBom();
            CommonHelper.CaptureScreen();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: View by Phase.</b></font>");
            CostingEstimatesPage.Instance.SelectView("Phase");

            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.JobEstimate.ToString())}_{jobdata.Name}_PhasesView";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Export Job Estimates viewed by Phase.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4.1: Export Job Estimates to CSV File.</b></font>");
            CostingEstimatesPage.Instance.ExportFile2(ExportJobEstimateCSV, exportFileName, 0, ExportTitleFileConstant.JOB_ESTIMATES_TITLE);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4.2: Export Job Estimates to Excel File.</b></font>");           
            CostingEstimatesPage.Instance.ExportFile2(ExportJobEstimateXls, exportFileName, 0, ExportTitleFileConstant.JOB_ESTIMATES_TITLE, false);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: View by Option.</b></font>");
            CostingEstimatesPage.Instance.SelectView("Option");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Export Job Estimates viewed by Options.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName(ExportType.JobEstimate.ToString())}_{jobdata.Name}_OptionsView";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6.1: Export Job Estimates to CSV File.</b></font>");
            CostingEstimatesPage.Instance.ExportFile2(ExportJobEstimateCSV, exportFileName, 0, ExportTitleFileConstant.JOB_ESTIMATES_TITLE);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6.2: Export Job Estimates to Excel File.</b></font>");
            CostingEstimatesPage.Instance.ExportFile2(ExportJobEstimateXls, exportFileName, 0, ExportTitleFileConstant.JOB_ESTIMATES_TITLE, false);

            CommonHelper.RefreshPage();
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Delete test Job.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.DeleteJob(jobdata.Name);
            }
        }

    }
}
