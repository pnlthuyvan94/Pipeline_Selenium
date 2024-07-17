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
using Pipeline.Testing.Pages.Assets.Options.Assigments;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Budget;
using Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders;
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Pathway.Assets;
using Pipeline.Testing.Pages.Purchasing.PurchaseOrders.ManageAllPurchaseOrders;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using Pipeline.Testing.Pages.Settings.Purchasing;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System;
using System.Windows.Forms;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E01_A_PIPE_29731 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private JobData jobdata;
        private SeriesData seriesData;
        private HouseData houseData;
        private CommunityData communityData;
        private OptionData optionData;
        private BuildingGroupData newBuildingGroup;
        private BuildingPhaseData newBuildingPhase;
        private ProductData productData;
        private OptionQuantitiesData optionQuantitiesData;
        private VendorData vendorData;
        private DivisionData newDivision;
        private TradesData tradeData;

        private const string newDivisionName = "RT_QA_New_Division_E01A";

        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E01A";
        private const string NewBuildingGroupCode = "E01A";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E01A";
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_E01A";
        private const string NewBuildingPhaseCode = "E01A";

        private const string baseOptionName = "BASE";
        private const string baseOptionNumber = "00001";

        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "E01A";
        private const string laborItemCost = "10.00";
        private const string materialItemCost = "20.00";

        [SetUp]
        public void SetupTestData()
        {
            Random rndNo = new Random();
            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };

            //create new series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Series_E01A",
                Code = "E01A",
                Description = "RT_QA_Series_E01A"
            };
            //create new house
            houseData = new HouseData()
            {
                PlanNumber = "E01A",
                HouseName = "RT_QA_House_E01A",
                SaleHouseName = "RT_QA_House_E01A",
                Series = "RT_QA_Series_E01A",
                BasePrice = "0.00",
                Description = "RT_QA_Series_E01A"
            };
            //create new community
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_E01A",
                Code = "RT_QA_Community_E01A"
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
            productData = new ProductData()
            {
                Name = "RT_QA_New_Product_E01A",
                Manufacture = genericManufacturer,
                Style = genericStyle,
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE",
                BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
            };

            optionQuantitiesData = new OptionQuantitiesData()
            {
                OptionName = baseOptionName,
                BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                ProductName = productData.Name,
                ProductDescription = "RT_QA_New_Product_E01A",
                Style = genericStyle,
                Condition = false,
                Use = string.Empty,
                Quantity = "100.00",
                Source = "Pipeline"
            };
            vendorData = new VendorData()
            {
                Name = "RT_QA_New_Vendor_E01A",
                Code = "E01A"
            };
            tradeData = new TradesData()
            {
                Code = "E01A",
                TradeName = "RT_QA_New_BuildingTrade_E01A",
                TradeDescription = "RT_QA_New_BuildingTrade_E01A",
                Vendor = vendorData.Name,
                BuildingPhases = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                SchedulingTasks = "",
                IsBuilderVendor = false
            };

            jobdata = new JobData()
            {
                Name = "RT_QA_Job_E01A" + rndNo.Next(1000).ToString(),
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Lot = "RT_QA_Lot_E01A"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Series test data.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, seriesData.Name);
            System.Threading.Thread.Sleep(5000);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                SeriesPage.Instance.CreateSeries(seriesData);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(houseData);
            }

            string expectedMsg = "";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add an new Option with 'Global' button of selected Option</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, baseOptionName);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, baseOptionNumber);
            if (OptionPage.Instance.IsItemInGrid("Name", baseOptionName) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", baseOptionName);
                OptionPage.Instance.LeftMenuNavigation("Assignments");
                AssignmentDetailPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
                if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", houseData.HouseName) is false)
                {
                    AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(houseData.HouseName);
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add BASE Option to new House test data.</b></font>");
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


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(communityData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Add new community to new division.</b></font>");
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
            string[] optionData = { baseOptionName + " - " + baseOptionNumber };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8 Add Base option to new Community test data.</b></font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9: Add new Building Group test data.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10: Add new Building Phase test data.</b></font>");
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
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableYes();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Add new Product test data.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, productData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is false)
            {
                ProductPage.Instance.CreateNewProduct(productData);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12: Add new Product to BASE OPTION.</b></font>");
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
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
                }

            }


            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendorData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Add new Building Phase to Vendors.</b></font>");
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
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.14: Add new Vendor Product cost data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                VendorProductPage.Instance.UpdateCostingforVerdor(newBuildingPhase.Name, productCode, materialItemCost, laborItemCost);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.15: Add new Trade qualified as vendor.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(tradeData, false);
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.16: Assign vendor to trade at community level.</b></font>");
                TradesPage.Instance.ClickVendorAssignments();
                System.Threading.Thread.Sleep(2000);
                VendorAssignmentsPage.Instance.SelectDivision(newDivisionName, 1);
                System.Threading.Thread.Sleep(10000);
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
                System.Threading.Thread.Sleep(10000);
                CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.EditCommunityVendor(communityData.Name, vendorData.Name);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.17: Add new Job test data.</b></font>");
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
                JobDetailPage.Instance.LeftMenuNavigation("Options", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.18: Approve job configurations.</b></font>");
                JobOptionPage.Instance.ClickApproveConfig();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.19: Apply System Quantities if not yet applied.</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Quantities", true);
                JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

                JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM", true);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.20: Generate Job BOM.</b></font>");
                JobBOMPage.Instance.GenerateJobBOM();

                JobBOMPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);
            }

            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.ActiveJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Budget", true);

                //Click on Budget page
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.21: Create Budget.</b></font>");
                JobDetailPage.Instance.LeftMenuNavigation("Budget", true);
                JobBudgetPage.Instance.CreateBudget();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.22: Update Product cost data.</b></font>");
                VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
                VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
                System.Threading.Thread.Sleep(2000);
                if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
                {
                    VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                    VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                    VendorProductPage.Instance.UpdateCostingforVerdor(newBuildingPhase.Name, productData.Name, materialItemCost, laborItemCost);
                }

                JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.ActiveJobs);
                JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.23: Generate Job BOM.</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Job BOM", true);
                JobBOMPage.Instance.GenerateJobBOM();
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.24: Generate Estimate.</b></font>");
                JobDetailPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.25: Change the cost of one of the products in ‘Budget’.</b></font>");
                VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
                VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
                System.Threading.Thread.Sleep(2000);
                if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
                {
                    VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                    VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                    VendorProductPage.Instance.UpdateCostingforVerdor(NewBuildingPhaseCode + "-" + newBuildingPhase.Name, productData.Name, "25", "25");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.24: Generate Job BOM.</b></font>");
                JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.ActiveJobs);
                JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                JobOptionPage.Instance.LeftMenuNavigation("Job BOM", true);
                JobBOMPage.Instance.GenerateJobBOM();
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.25: Generate Estimate.</b></font>");
                JobDetailPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.26: Generate Purchase Order.</b></font>");
                JobBudgetPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);
                CreatePurchaseOrdersPage.Instance.SetDefaultView("Phase");
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                CreatePurchaseOrdersPage.Instance.CollapseAllGrid();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15: Select on that BP and click on Create PO(s) for Selected button. Verify if able to create PO successfully.</b></font>");
                CreatePurchaseOrdersPage.Instance.CreatePOForSelectedInBP(NewBuildingPhaseCode + "-" + newBuildingPhase.Name, true);
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();

            }

        }
        [Test]
        public void E01_A_Purchasing_Detail_Pages_Purchase_Orders_All_Purchase_Orders()
        {
            //Navigate to Purchasing > Purchase orders > All purchase orders
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to Purchasing > Purchase orders > All purchase orders page.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Verify we can open Manage All Purchase Orders.</b></font>");
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.ManageAllPurchaseOrders);
            ManageAllPurchaseOrdersPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (ManageAllPurchaseOrdersPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Filtered successfully.</font color>");
                ExtentReportsHelper.LogPass($"<font color ='green'>Verified we can open Manage All Purchase Orders.</font color>");

                //On the right-hand side corner, select "view and print PO" Click the button and we can view the PO and we can export to PDF, CSV, Excel, Tiff, and web Archive
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: On the right-hand side corner, select \"view and print PO\".</b></font>");
                ManageAllPurchaseOrdersPage.Instance.ViewAndPrintPO();

                //Cancel PO
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: On the right-hand side corner, select \"view and print PO\".</b></font>");
                ManageAllPurchaseOrdersPage.Instance.ClickToOpenChangeStatusModal();
                ManageAllPurchaseOrdersPage.Instance.CancelPO();
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Delete Job test data.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", jobdata.Name);
            }
        }
    }
}
