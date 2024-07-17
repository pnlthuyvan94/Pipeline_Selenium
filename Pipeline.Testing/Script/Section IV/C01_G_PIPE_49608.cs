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
using Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders;
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using Pipeline.Testing.Pages.Settings.Sage300CRE;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class C01_G_PIPE_49608 : BaseTestScript
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

        private const string newDivisionName = "RT_QA_New_Division_C01G";

        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_C01G";
        private const string NewBuildingGroupCode = "C01G";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_C01G";
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_C01G";
        private const string NewBuildingPhaseCode = "C01G";

        //private const string baseOptionName = "BASE";
        //private const string baseOptionNumber = "00001";

        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "C01G";
        private const string laborItemCost = "0.00";
        private const string materialItemCost = "20.00";
        private bool isSageRunning = false;

        private const string expectedUnorderedAmount = "$7,500.00";
        private const string expectedOrderedAmount = "$7,500.00";
        private const string expectedBudgetAmount = "$3,000.00";


        [SetUp]
        public void SetupTestData()
        {
            Random rndNo = new Random();
            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };
            var optionType = new List<bool>()
            {
                false, false, false
            };
            newOption = new OptionData()
            {
                Name = "RT_QA_New_Option_C01G",
                Number = "C01G",
                Types = optionType
            };
            newOption2 = new OptionData()
            {
                Name = "RT_QA_New_Option2_C01G",
                Number = "C01G2",
                Types = optionType
            };
            //create new series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Series_C01G",
                Code = "C01G",
                Description = "RT_QA_Series_C01G"
            };
            //create new house
            houseData = new HouseData()
            {
                PlanNumber = "C01G",
                HouseName = "RT_QA_House_C01G",
                SaleHouseName = "RT_QA_House_C01G",
                Series = "RT_QA_Series_C01G",
                BasePrice = "0.00",
                Description = "RT_QA_Series_C01G"
            };
            //create new community
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_C01G",
                Code = "RT_QA_Community_C01G"
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
                Name = "RT_QA_New_Product_C01G",
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
                OptionName = newOption.Name,
                BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                ProductName = productData.Name,
                ProductDescription = "RT_QA_New_Product_C01G",
                Style = genericStyle,
                Condition = false,
                Use = string.Empty,
                Quantity = "100.00",
                Source = "Pipeline"
            };

            optionQuantitiesData2 = new OptionQuantitiesData()
            {
                OptionName = newOption2.Name,
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
                Name = "RT_QA_New_Vendor_C01G",
                Code = "C01G"
            };
            tradeData = new TradesData()
            {
                Code = "C01G",
                TradeName = "RT_QA_New_BuildingTrade_C01G",
                TradeDescription = "RT_QA_New_BuildingTrade_C01G",
                Vendor = vendorData.Name,
                BuildingPhases = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                SchedulingTasks = "",
                IsBuilderVendor = false
            };

            jobdata = new JobData()
            {
                Name = "RT_QA_Job_C01G" + rndNo.Next(1000).ToString(),
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Lot = "RT_QA_Lot_C01G"
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

            //string expectedMsg = "";
            //ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add an new Option with 'Global' button of selected Option</b></font>");
            //OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            //OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, baseOptionName);
            //OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, baseOptionNumber);
            //if (OptionPage.Instance.IsItemInGrid("Name", baseOptionName) is true)
            //{
            //    OptionPage.Instance.SelectItemInGrid("Name", baseOptionName);
            //    OptionPage.Instance.LeftMenuNavigation("Assignments");
            //    AssignmentDetailPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            //    if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", houseData.HouseName) is false)
            //    {
            //        AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(houseData.HouseName);
            //    }
            //}

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5 Add new Options to new House test data.</b></font>");
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
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10 Add new test options to new Community.</b></font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Add new Building Group test data.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12: Add new Building Phase test data.</b></font>");
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


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Add new Product test data.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, productData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is false)
            {
                ProductPage.Instance.CreateNewProduct(productData);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.14: Add new Product to the new Options.</b></font>");
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
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
                }
            }

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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.15: Add new Building Phase to Vendors.</b></font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.16: Add new Vendor Product cost data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                VendorProductPage.Instance.UpdateCostingforVerdor(newBuildingPhase.Name, productCode, materialItemCost, laborItemCost);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.17: Add new Trade qualified as vendor.</b></font>");
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
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.18: Assign vendor to trade at community level.</b></font>");
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


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.19: Add new Job test data.</b></font>");
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

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.20: Add the new options to the job configurations.</b></font>");
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob("option", newOption.Number + "-" + newOption.Name, newOption2.Number + "-" + newOption2.Name);
                CommonHelper.RefreshPage();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.21: Approve job configurations.</b></font>");
                JobOptionPage.Instance.ClickApproveConfig();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.22: Apply System Quantities if not yet applied.</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Quantities", true);
                JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

                JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM", true);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.23: Generate Job BOM.</b></font>");
                JobBOMPage.Instance.GenerateJobBOM();

                JobBOMPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);
            }
        }
        [Test]
        public void C01_G_Jobs_Details_Pages_All_Jobs_Create_Purchase_Orders()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.0: Check if Sage is running.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            Sage300CREPage.Instance.LeftMenuNavigation("Sage300CRE");
            isSageRunning = Sage300CREPage.Instance.IsSageRunning();

            //Navigate to Jobs > Active Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to Jobs > Active Jobs page.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            //CommonHelper.CaptureScreen();
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                //Select a job
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Select a job and navigate to the Budget page.</b></font>");
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1.1: Create Budget.</b></font>");
                JobDetailPage.Instance.LeftMenuNavigation("Budget", true);
                JobBudgetPage.Instance.CreateBudget();
                //Click on Budget page

                //Navigate to Create Purchase Orders page and verify if there is no DB Null error displayed on the screen
                //Verify there is still no warning icon shown
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Navigate to Create Purchase Orders page and verify if there is no DB Null error displayed on the screen.</b></font>");
                JobBudgetPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);
                CreatePurchaseOrdersPage.Instance.SetDefaultView("Phase");
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                CreatePurchaseOrdersPage.Instance.CollapseAllGrid();
                System.Threading.Thread.Sleep(5000);

                //Change the cost of one of the products under vendor then generate BOM and Estimates both in JOB BOM page and in Estimates page. Verify if able to successfully create BOM and Estimate
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Navigate to vendor page and update Product cost data.</b></font>");
                VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
                VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
                System.Threading.Thread.Sleep(2000);
                if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
                {
                    VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                    VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                    VendorProductPage.Instance.UpdateCostingforVerdor(newBuildingPhase.Name, productData.Name, materialItemCost, laborItemCost);
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Navigate to job and navigate to the estinate page.</b></font>");
                JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.ActiveJobs);
                JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Generate Job BOM.</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Job BOM", true);
                JobBOMPage.Instance.GenerateJobBOM();
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Generate Estimate.</b></font>");
                JobDetailPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);

                //Navigate back to ‘Create Purchase Order’s page. Verify if there is a warning icon under Budget column with the tooltip verbiage 
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Navigate back to ‘Create Purchase Order’s page. Verify if there is a warning icon under Budget column with the tooltip verbiage.</b></font>");
                JobBudgetPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);
                CreatePurchaseOrdersPage.Instance.SetDefaultView("Phase");
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                CreatePurchaseOrdersPage.Instance.CollapseAllGrid();
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                CreatePurchaseOrdersPage.Instance.CheckJobInformationTooltip();

                //Change the cost of one of the products in ‘Budget’ page from the left nav under one of the building phase that still has Unbudgeted amount then generate BOM and Estimates both in JOB BOM page and in Estimates page. Verify if able to successfully create BOM and Estimates
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Change the cost of one of the products in ‘Budget’.</b></font>");
                VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
                VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
                System.Threading.Thread.Sleep(2000);
                if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
                {
                    VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                    VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                    VendorProductPage.Instance.UpdateCostingforVerdor(NewBuildingPhaseCode + "-" + newBuildingPhase.Name, productData.Name, "25", "25");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Navigate back to job and navigate to the estinate page.</b></font>");
                JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.ActiveJobs);
                JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Navigate back to ‘Create Purchase Order’s page. Verify if there is a warning icon under Budget column.</b></font>");
                JobBudgetPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);
                CreatePurchaseOrdersPage.Instance.SetDefaultView("Phase");
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                CreatePurchaseOrdersPage.Instance.CollapseAllGrid();
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                CreatePurchaseOrdersPage.Instance.CheckJobInformationTooltip();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.11: Generate Job BOM.</b></font>");
                JobOptionPage.Instance.LeftMenuNavigation("Job BOM", true);
                JobBOMPage.Instance.GenerateJobBOM();
                System.Threading.Thread.Sleep(5000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.12: Generate Estimate.</b></font>");
                JobDetailPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);

                //Navigate back to ‘Create Purchase Order’s page. Verify if there is a warning icon under Budget column
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.13: Navigate back to ‘Create Purchase Order’s page. Verify if there is a warning icon under Budget column.</b></font>");
                JobBudgetPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);
                CreatePurchaseOrdersPage.Instance.SetDefaultView("Phase");
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                CreatePurchaseOrdersPage.Instance.CollapseAllGrid();
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.14: Verify if there is an icon under Budget column.</b></font>");
                CreatePurchaseOrdersPage.Instance.CheckJobInformationTooltip();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15: Check if Vendor marked as TBD cannot create PO.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15.1: Update vendor into a TBD.</b></font>");
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.SetTBD(true);
                VendorDetailPage.Instance.Save();
            }
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            //CommonHelper.CaptureScreen();
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15.2: Verify if PO can be created.</b></font>");
                bool canCreatePO = CreatePurchaseOrdersPage.Instance.CanCreatePO(NewBuildingPhaseCode + "-" + newBuildingPhase.Name);
                if (!canCreatePO)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>PO cannot be created for TBD Vendors.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>PO can be created for TBD Vendors.</b></font>");
            }
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15.3: Set Vendor back to not TBD.</b></font>");
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.SetTBD(false);
                VendorDetailPage.Instance.Save();
            }

            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            //CommonHelper.CaptureScreen();
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17: Create PO.</b></font>");
                JobBudgetPage.Instance.LeftMenuNavigation("Create Purchase Orders", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17.1: Validate Budget amount.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateBudgetAmount(expectedBudgetAmount);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17.2: Validate Unordered amount.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateUnorderedAmount(expectedUnorderedAmount);
                
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17.3: Validate Ordered amount.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateOrderedAmount("$0.00");

                //Verufy all view types works when there is variance
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17.4.1: Verify all view types works when there is variance.</b></font>");
                CreatePurchaseOrdersPage.Instance.VerifyVarianceModalForAllSelectedViewType(NewBuildingPhaseCode + "-" + newBuildingPhase.Name);

                //Select on that BP and click on Create PO(s) for Selected button. Verify if able to create PO successfully
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17.4.2: Select on that BP and click on Create PO(s) for Selected button. Verify if able to create PO successfully.</b></font>");
                CreatePurchaseOrdersPage.Instance.CreatePOForSelectedInBP(NewBuildingPhaseCode + "-" + newBuildingPhase.Name, true);
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();

                //Verify in Create PO page, the other BP that has $0.00 Budget, it does not have a tick box so that users are not allowed to create PO for BP with unbudgeted amount
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17.5: Verify in Create PO page, the other BP that has $0.00 Budget, it does not have a tick box so that users are not allowed to create PO for BP with unbudgeted amount.</b></font>");
                CreatePurchaseOrdersPage.Instance.CheckIfBPHasPO(NewBuildingPhaseCode + "-" + newBuildingPhase.Name);
                System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17.6: Validate Unordered amount after PO is created.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateUnorderedAmount("$0.00"); 

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.17.7: Validate Ordered amount after PO is created.</b></font>");
                CreatePurchaseOrdersPage.Instance.ValidateOrderedAmount(expectedOrderedAmount);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.18: Verify in View PO page, the initial status should be Issued. If Sync to Sage is enabled, sent to accounting icon must be displayed.</b></font>");
                CreatePurchaseOrdersPage.Instance.LeftMenuNavigation("View Purchase Orders");
                ViewPurchaseOrdersPage.Instance.VerifyPOStatus(NewBuildingPhaseCode + "-" + newBuildingPhase.Name, isSageRunning, "Issued");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.19: Verify in View PO page, the Product's quantity, sub-total and total cost are combined for all options in the phase.</b></font>");
                ViewPurchaseOrdersPage.Instance.VerifyProductDetails("150.00", "$25.00", "$3,750.00", "$0.00", "$3,750.00");

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
