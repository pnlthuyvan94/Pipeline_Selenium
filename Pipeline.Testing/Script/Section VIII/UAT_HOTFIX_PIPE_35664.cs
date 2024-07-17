using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.Options;
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
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Budget;
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Purchasing.Trades;
using System.Linq;

namespace Pipeline.Testing.Script.Section_VIII
{
    public class UAT_HOTFIX_PIPE_35664 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VIII);
        }

        private JobData jobdata;
        private SeriesData seriesData;
        private HouseData houseData;
        private CommunityData communityData;
        private OptionData optionData;
        private BuildingGroupData buildingGroupData;
        private BuildingPhaseData buildingPhaseData;
        private ProductData productData;
        private VendorData vendorData;
        private TradesData tradeData;

        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_UAT35664";
        private const string NewBuildingGroupCode = "UAT35664";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_UAT35664";
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_UAT35664";
        private const string NewBuildingPhaseCode = "UAT3";

        private const string baseOptionName = "BASE";
        private const string baseOptionNumber = "00001";

        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "UAT3";
        private const string laborItemCost = "$10.00";
        private const string materialItemCost = "$20.00";

        [SetUp]
        public void SetupTestData()
        {
            //create new series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Series_UAT35664",
                Code = "UAT35664",
                Description = "RT_QA_Series_UAT35664"
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
                PlanNumber = "UAT4",
                HouseName = "RT_QA_House_UAT35664",
                SaleHouseName = "RT_QA_House_UAT35664",
                Series = "RT_QA_Series_UAT35664"
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Add new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(houseData);
            }

            //create new community
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_UAT35664",
                Code = "RT_QA_Community_UAT35664"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(communityData);
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
                Name = "RT_QA_New_Vendor_UAT35664",
                Code = "UAT35664"
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
                Name = "RT_QA_New_Product_UAT35664",
                Style = genericStyle,
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE",
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
            };
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, productData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is false)
            {
                ProductPage.Instance.CreateNewProduct(productData);

                ProductDetailPage.Instance.AddManufacturersStyles(genericManufacturer, true, genericStyle, productCode);
                System.Threading.Thread.Sleep(2000);
                ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Manufacturer", "_00000000000000000000000000");
                System.Threading.Thread.Sleep(2000);

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Add new Product to BASE OPTION.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, baseOptionName);
            System.Threading.Thread.Sleep(2000);
            if(OptionPage.Instance.IsItemInGrid("Name", baseOptionName) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", baseOptionName);
                OptionDetailPage.Instance.LeftMenuNavigation("Products", true);
                if(ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", productData.Name) is false)
                {
                    OptionQuantitiesData optionQuantitiesData = new OptionQuantitiesData()
                    {
                        OptionName = baseOptionName,
                        BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                        ProductName = productData.Name,
                        ProductDescription = "RT_QA_New_Product_UAT35664",
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
                Code = "UAT35664",
                TradeName = "RT_QA_New_BuildingTrade_UAT35664",
                TradeDescription = "RT_QA_New_BuildingTrade_UAT35664",
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
            }
            else
            {
                TradesPage.Instance.ClickEditToOpenEditTradeModal(tradeData.TradeName);
                TradesPage.Instance.UpdateTrade(tradeData);
            }
            
        }

        [Test]
        public void UAT_HotFix_Budget_Shows_Total_But_No_Line_Item_Costs()
        {
            jobdata = new JobData()
            {
                Name = "RT_QA_Job_UAT35664",
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Lot = "RT_QA_Lot_UAT35664"
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

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Create Budget.</b></font>");
                JobEstimatePage.Instance.LeftMenuNavigation("Budget", true);
                JobBudgetPage.Instance.CreateBudget();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Verify Budget line item costs are displayed.</b></font>");
                JobBudgetPage.Instance.CheckLineItemCost(productData.Name, "Labor", laborItemCost);
                JobBudgetPage.Instance.CheckLineItemCost(productData.Name, "Material", materialItemCost);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Update the Vendor Product Cost.</b></font>");
                VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
                VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
                System.Threading.Thread.Sleep(2000);
                if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
                {
                    VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                    VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                    VendorProductPage.Instance.UpdateCostingforVerdor(buildingPhaseData.Name, productCode, "$50.00", "$20.00");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Generate the Job Estimates again.</b></font>");
                JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
                JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Go to Budget.</b></font>");
                JobEstimatePage.Instance.LeftMenuNavigation("Budget", true);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Verify Budget line item costs are still displayed and the same with the snap shot data.</b></font>");
                JobBudgetPage.Instance.CheckLineItemCost(productData.Name, "Labor", laborItemCost);
                JobBudgetPage.Instance.CheckLineItemCost(productData.Name, "Material", materialItemCost);
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Delete test product from BASE Option.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, baseOptionName);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", baseOptionName) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", baseOptionName);
                OptionDetailPage.Instance.LeftMenuNavigation("Products", true);
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", productData.Name) is true)
                {
                    ProductsToOptionPage.Instance.DeleteItemInGrid("Product", productData.Name);
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Delete Job test data.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", jobdata.Name);
                System.Threading.Thread.Sleep(20000);
            }

        }
    }
}
