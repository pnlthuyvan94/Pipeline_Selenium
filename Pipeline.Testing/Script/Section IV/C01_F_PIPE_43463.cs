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
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using Pipeline.Testing.Pages.Settings.Purchasing;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class C01_F_PIPE_43463 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private JobData jobdata;
        private SeriesData seriesData;
        private HouseData houseData;
        private CommunityData communityData;
        private OptionData newOption;
        private OptionData newOption2;
        private BuildingGroupData newBuildingGroup;
        private BuildingPhaseData newBuildingPhase;
        private ProductData productData;
        private OptionQuantitiesData optionQuantitiesData;
        private OptionQuantitiesData optionQuantitiesData2;
        private VendorData vendorData;
        private DivisionData newDivision;
        private TradesData tradeData;

        private const string newDivisionName = "RT_QA_New_Division_C01F";

        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_C01F";
        private const string NewBuildingGroupCode = "C01F";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_C01F";
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_C01F";
        private const string NewBuildingPhaseCode = "C01F";

        //private const string baseOptionName = "BASE";
        //private const string baseOptionNumber = "00001";

        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "C01F";
        private const string laborItemCost = "0.00";
        private const string materialItemCost = "20.00";
        private const string expectedEstimateAmount = "$3,000.00";
        private const string expectedUnbudgetedAmount = "$3,000.00";
        private const string expectedBudgetAmount = "$3,000.00";

        [SetUp]
        public void SetupTestData()
        {

            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };

            //create new series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Series_C01F",
                Code = "C01F",
                Description = "RT_QA_Series_C01F"
            };
            //create new house
            houseData = new HouseData()
            {
                PlanNumber = "C01F",
                HouseName = "RT_QA_House_C01F",
                SaleHouseName = "RT_QA_House_C01F",
                Series = "RT_QA_Series_C01F"
            };

            var optionType = new List<bool>()
            {
                false, false, false
            };
            newOption = new OptionData()
            {
                Name = "RT_QA_New_Option_C01F",
                Number = "C01F",
                Types = optionType
            };
            newOption2 = new OptionData()
            {
                Name = "RT_QA_New_Option2_C01F",
                Number = "C01F2",
                Types = optionType
            };
            //create new community
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_C01F",
                Code = "RT_QA_Community_C01F"
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
                Name = "RT_QA_New_Product_C01F",
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
                OptionName = newOption2.Name,
                BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                ProductName = productData.Name,
                ProductDescription = "RT_QA_New_Product_C01F",
                Style = genericStyle,
                Condition = false,
                Use = string.Empty,
                Quantity = "100.00",
                Source = "Pipeline"
            };

            optionQuantitiesData2 = new OptionQuantitiesData()
            {
                OptionName = newOption.Name,
                BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                ProductName = productData.Name,
                ProductDescription = "RT_QA_New_Product_C01F",
                Style = genericStyle,
                Condition = false,
                Use = string.Empty,
                Quantity = "50.00",
                Source = "Pipeline"
            };
            vendorData = new VendorData()
            {
                Name = "RT_QA_New_Vendor_C01F",
                Code = "C01F"
            };
            tradeData = new TradesData()
            {
                Code = "C01F",
                TradeName = "RT_QA_New_BuildingTrade_C01F",
                TradeDescription = "RT_QA_New_BuildingTrade_C01F",
                Vendor = vendorData.Name,
                BuildingPhases = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                SchedulingTasks = "",
                IsBuilderVendor = false
            };

            jobdata = new JobData()
            {
                Name = "RT_QA_Job_C01F",
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Lot = "RT_QA_Lot_C01F"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Create new Options.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newOption.Name);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", newOption.Name) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(newOption);
            }

            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newOption2.Name);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", newOption2.Name) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(newOption2);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new Series test data.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, seriesData.Name);
            System.Threading.Thread.Sleep(5000);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                SeriesPage.Instance.CreateSeries(seriesData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(houseData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5 Add test Options to new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Options");
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", newOption2.Name) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(newOption2.Name + " - " + newOption2.Number);
                }

                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", newOption.Name) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(newOption.Name + " - " + newOption.Number);
                }

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(communityData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Add new community to new division.</b></font>");
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
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8 Add new House to Community.</b></font>");
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


                string[] optionData2 = { newOption2.Name + " - " + newOption2.Number };
                if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", newOption2.Name) is false)
                {
                    CommunityOptionPage.Instance.AddCommunityOption(optionData2);
                }

                if (CommunityOptionPage.Instance.IsCommunityHouseOptionInGrid("Option", newOption2.Name) is false)
                {
                    CommunityHouseOptionData communityHouseOptionData2 = new CommunityHouseOptionData()
                    {
                        AllHouseOptions = optionData2,
                        SalePrice = "10"
                    };
                    CommunityOptionPage.Instance.OpenAddCommunityHouseOptionModal();
                    CommunityOptionPage.Instance.AddCommunityHouseOptionModal.AddCommunityHouseOption(communityHouseOptionData2);
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Add new Building Phase test data.</b></font>");
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


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12: Add new Product test data.</b></font>");

            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, productData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is false)
            {
                ProductPage.Instance.CreateNewProduct(productData);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Add new Product to the new Options.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newOption2.Name);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", newOption2.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", newOption2.Name);
                OptionDetailPage.Instance.LeftMenuNavigation("Products", true);
                ProductsToOptionPage.Instance.FilterOptionQuantitiesInGrid("Product", GridFilterOperator.EqualTo, productData.Name);
                System.Threading.Thread.Sleep(5000);
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", productData.Name) is false)
                {
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
                }
            }

            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newOption.Name);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", newOption.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", newOption.Name);
                OptionDetailPage.Instance.LeftMenuNavigation("Products", true);
                ProductsToOptionPage.Instance.FilterOptionQuantitiesInGrid("Product", GridFilterOperator.EqualTo, productData.Name);
                System.Threading.Thread.Sleep(5000);
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", productData.Name) is false)
                {
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData2);
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.14: Add new Building Phase to Vendors.</b></font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.15: Add new Vendor Product cost data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                VendorProductPage.Instance.UpdateCostingforVerdor(newBuildingPhase.Name, productCode, materialItemCost, laborItemCost);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.16: Add new Trade qualified as vendor.</b></font>");

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(tradeData, false, false);
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.17: Assign vendor to trade at community level.</b></font>");
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


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.18: Add new Job test data.</b></font>");
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

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.19: Add the new options to the job configurations.</b></font>");
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob("option", newOption.Number + "-" + newOption.Name, newOption2.Number + "-" + newOption2.Name);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.20: Approve job configurations.</b></font>");
                JobOptionPage.Instance.ClickApproveConfig();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.21: Apply System Quantities if not yet applied.</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Quantities", true);
                JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

                JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM", true);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.22: Generate Job BOM.</b></font>");
                JobBOMPage.Instance.GenerateJobBOM();

                JobBOMPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);
            }
        }
        [Test]
        public void C01_F_Jobs_Details_Pages_All_Jobs_Budget()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to the Jobs Default page.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Select the test job and navigate to the Budget page.</b></font>");
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Budget", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2.1: Verify if Product Names are visible before creating the budget.</b></font>");
                JobBudgetPage.Instance.SelectView("Phase");
                JobBudgetPage.Instance.IsProductNameVisibleOnPhasesView(productData.Name);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Select Options in the View By dropdown.</b></font>");
                JobBudgetPage.Instance.SelectView("Options");
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Collapse/Expand all cascading grids.</b></font>");
                JobBudgetPage.Instance.CollapseAllGrid();

                //verify grids has needed columns.
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Verify if columns exists in Options View Grid.</b></font>");
                if (JobBudgetPage.Instance.IsColumnFoundInOptionsGrid("Option"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Option column is found in the Options grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Option column is not found in the Options grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionsGrid("Number"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Number column is found in the Options grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Number column is not found in the Options grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionsGrid("Description"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Description column is found in the Options grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Description column is not found in the Options grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionsGrid("Estimate"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Estimate column is found in the Options grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Estimate column is not found in the Options grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionsGrid("Unbudgeted"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Unbudgeted column is found in the Options grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Unbudgeted column is not found in the Options grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionsGrid("Budget"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Budget column is found in the Options grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Budget column is not found in the Options grid.</b></font>");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Verify if columns exists in Options View Building Phase Grid.</b></font>");
                if (JobBudgetPage.Instance.IsColumnFoundInOptionBPDetailsGrid("Building Phase"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Building Phase column is found in the Options View Building Phase Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Building Phase column is not found in the Options View Building Phase Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionBPDetailsGrid("Phase Bid"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Phase Bid column is found in the Options View Building Phase Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Phase Bid column is not found in the Options View Building Phase Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionBPDetailsGrid("Product Cost"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Product Cost column is found in the Options View Building Phase Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Product Cost column is not found in the Options View Building Phase Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionBPDetailsGrid("Bid Cost"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Bid Cost column is found in the Options View Building Phase Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Bid Cost column is not found in the Options View Building Phase Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionBPDetailsGrid("Total"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Total column is found in the Options View Building Phase Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Total column is not found in the Options View Building Phase Grid.</b></font>");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Verify if columns exists in Options View Product Grid.</b></font>");
                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Product"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Product column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Product column is not found in the Options View Product Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Code"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Code column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Code column is not found in the Options View Product Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Description"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Description column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Description column is not found in the Options View Product Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Line Item Type"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Line Item Type column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Line Item Type column is not found in the Options View Product Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Qty Needed"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Qty Needed column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Qty Needed column is not found in the Options View Product Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Qty Budgeted"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Qty Budgeted column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Qty Budgeted column is not found in the Options View Product Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Qty Needed To Budget"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Qty Needed To Budget	 column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Qty Needed To Budget	 column is not found in the Options View Product Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Unit Cost"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Unit Cost column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Unit Cost column is not found in the Options View Product Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionProductDetailsGrid("Cost If Budgeted"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Cost If Budgeted column is found in the Options View Product Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Cost If Budgeted column is not found in the Options View Product Grid.</b></font>");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Verify if columns exists in Options View Vendor Grid.</b></font>");
                if (JobBudgetPage.Instance.IsColumnFoundInOptionVendorDetailsGrid("Date Created"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Date Created column is found in the Options View Vendor Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Date Created column is not found in the Options View Vendor Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionVendorDetailsGrid("Qty"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Qty column is found in the Options View Vendor Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Qty column is not found in the Options View Vendor Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionVendorDetailsGrid("Line Item Type"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Line Item Type column is found in the Options View Vendor Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Line Item Type column is not found in the Options View Vendor Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionVendorDetailsGrid("Subtotal"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Subtotal column is found in the Options View Vendor Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Subtotal column is not found in the Options View Vendor Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionVendorDetailsGrid("Tax"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Tax column is found in the Options View Vendor Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Tax column is not found in the Options View Vendor Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionVendorDetailsGrid("Total"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Total column is found in the Options View Vendor Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Total column is not found in the Options View Vendor Grid.</b></font>");

                if (JobBudgetPage.Instance.IsColumnFoundInOptionVendorDetailsGrid("Vendor Name"))
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendor Name column is found in the Options View Vendor Grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Vendor Name column is not found in the Options View Vendor Grid.</b></font>");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Navidate to Users Menu > Settings > Purchasing.</b></font>");
                SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
                SettingPage.Instance.LeftMenuNavigation("Purchasing");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Set the Default Job Budget View to Options.</b></font>");
                PurchasingPage.Instance.SetDefaultJobBudgetView("Options");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.11: Verify if Default Job Budget view is set to Options in Job Budget page.</b></font>");

                JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
                JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
                if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
                {
                    JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                    JobDetailPage.Instance.LeftMenuNavigation("Budget", true);
                    var defaultBudgetView = JobBudgetPage.Instance.GetDefaultBudgetView();
                    if(defaultBudgetView == "Options")
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Default Budget View is Options.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Default Budget View is not Options</b></font>");

                }

                SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
                SettingPage.Instance.LeftMenuNavigation("Purchasing");
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.12: Set the Default Job Budget View to Phase.</b></font>");
                PurchasingPage.Instance.SetDefaultJobBudgetView("Phase");

                JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
                JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
                if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
                {
                    JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                    JobDetailPage.Instance.LeftMenuNavigation("Budget", true);                    

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.13: Validate the Estimate amount.</b></font>");
                    JobBudgetPage.Instance.ValidateEstimateAmount(expectedEstimateAmount);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.14: Validate the Unbudgeted amount.</b></font>");
                    JobBudgetPage.Instance.ValidateUnbudgetedAmount(expectedUnbudgetedAmount);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15: Generate the Job Budget.</b></font>");
                    JobBudgetPage.Instance.CreateBudget();

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15.1: Verify if Product Names are visible after creating the budget.</b></font>");
                    JobBudgetPage.Instance.SelectView("Phase");
                    JobBudgetPage.Instance.IsProductNameVisibleOnPhasesView(productData.Name);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.16: Validate the Budgeted amount.</b></font>");
                    JobBudgetPage.Instance.ValidateBudgetedAmount(expectedBudgetAmount);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17: Validate the Estimate amount.</b></font>");
                    JobBudgetPage.Instance.ValidateEstimateAmount(expectedEstimateAmount);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.18: Validate the Unbudgeted amount.</b></font>");
                    JobBudgetPage.Instance.ValidateUnbudgetedAmount("$0.00");
                }

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
