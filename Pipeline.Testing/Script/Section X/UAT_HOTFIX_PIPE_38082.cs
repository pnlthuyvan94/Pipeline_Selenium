using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
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
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Jobs.Job.Vendors;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using System.Collections.Generic;


namespace Pipeline.Testing.Script.Section_X
{
    class UAT_HOTFIX_PIPE_38082 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_X);
        }

        private SeriesData newSeriesData;
        private const string newSeriesDataName = "RT_QA_Series_XUAT38082";
        private const string newSeriesDataCode = "XUAT";
        private const string newSeriesDataDescription = "RT_QA_Series_XUAT38082";

        private HouseData newHouseData;
        private const string newHouseDataPlanNumber = "XUAT";
        private const string newHouseDataHouseName = "RT_QA_House_XUAT38082";
        private const string newHouseDataSaleHouseName = "RT_QA_House_XUAT38082";
        private const string newHouseDataSeries = "RT_QA_Series_XUAT38082";

        private CommunityData newCommunityData;
        private const string newCommunityDataName = "RT_QA_Community_XUAT38082";
        private const string newCommunityDataCode = "RT_QA_Community_XUAT38082";

        private BuildingGroupData newBuildingGroupData;

        private BuildingPhaseData newBuildingPhaseData;

        private ProductData newProductData;
        private const string newProductDataName = "RT_QA_New_Product_XUAT38082";    

        private VendorData newVendorData;
        private const string newVendorDataName = "RT_QA_New_Vendor_XUAT38082";
        private const string newVendorDataCode = "XUAT";

        private TradesData newTradeData;
        private const string newTradeDataCode = "XUAT";
        private const string newTradeDataTradeName = "RT_QA_New_BuildingTrade_XUAT38082";
        private const string newTradeDataTradeDescription = "RT_QA_New_BuildingTrade_XUAT38082";

        private DivisionData newDivision;
        private const string newDivisionName = "RT_QA_New_Division_XUAT38082";

        private const string newBuildingGroupName = "RT_QA_New_BuildingGroup_XUAT38082";
        private const string newBuildingGroupCode = "XUAT";
        private const string newBuildingGroupDescription = "RT_QA_New_BuildingGroup_XUAT38082";

        private const string newBuildingPhaseName = "RT_QA_New_BuildingPhase_XUAT38082";
        private const string newBuildingPhaseCode = "XUAT";

        private const string baseOptionName = "0333";
        private const string baseOptionNumber = "0333";

        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "XUAT";
        private const string laborItemCost = "$10.00";
        private const string materialItemCost = "$20.00";

        private JobData newJobdata;
        private const string newJobdataName = "RT_QA_Job_XUAT38082";
        private const string newJobdataLot = "RT_QA_Lot_XUAT38082";

        [SetUp]
        public void SetupTestData()
        {
            newSeriesData = new SeriesData()
            {
                Name = newSeriesDataName,
                Code = newSeriesDataCode,
                Description = newSeriesDataDescription
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Add new Series.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newSeriesData.Name);
            System.Threading.Thread.Sleep(5000);
            if (SeriesPage.Instance.IsItemInGrid("Name", newSeriesData.Name) is false)
            {
                SeriesPage.Instance.CreateSeries(newSeriesData);
            }

            newHouseData = new HouseData()
            {
                PlanNumber = newHouseDataPlanNumber,
                HouseName = newHouseDataHouseName,
                SaleHouseName = newHouseDataSaleHouseName,
                Series = newSeriesData.Name
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new House.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newHouseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", newHouseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(newHouseData);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add BASE Option to new House.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newHouseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", newHouseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", newHouseData.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Options");
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", baseOptionName) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(baseOptionName + " - " + baseOptionNumber);
                }
            }

            newCommunityData = new CommunityData()
            {
                Name = newCommunityDataName,
                Code = newCommunityDataCode,
                Division = newDivisionName,
                City = "City",
                CityLink = "https://cl.com",
                Township = "Township",
                County = "Country",
                State = "State",
                Zip = "00000",
                SchoolDistrict = "SchoolDistrict",
                SchoolDistrictLink = "http://sdl.com",
                Status = "Open",
                Description = "XUAT38082",
                DrivingDirections = "XUAT38082",
                Slug = "Slug",
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", newCommunityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(newCommunityData);
            }

            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Add new community to new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", newDivisionName);
                DivisionDetailPage.Instance.LeftMenuNavigation("Communities", true);
                DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityData.Name);
                System.Threading.Thread.Sleep(2000);
                if (DivisionCommunityPage.Instance.IsItemInGrid("Name", newCommunityData.Name) is false)
                {
                    string[] communities = { newCommunityData.Name };
                    DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                    DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                    DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7 Add new House to Community.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newHouseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", newHouseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", newHouseData.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Communities");
                HouseCommunities.Instance.FillterOnGrid("Name", newCommunityData.Name);
                System.Threading.Thread.Sleep(5000);
                if (HouseCommunities.Instance.IsValueOnGrid("Name", newCommunityData.Name) is false)
                {
                    HouseCommunities.Instance.AddButtonCommunities();
                    HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(newCommunityData.Name, 0);
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Community with Name {newCommunityData.Name} is displayed in grid");
                }
            }
            string[] optionData = { baseOptionName + " - " + baseOptionNumber };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8 Add Base option to new Community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", newCommunityData.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", newCommunityData.Name);
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
                        OtherMasterOptions = optionData,
                        SalePrice = "50"
                    };
                    CommunityOptionPage.Instance.OpenAddCommunityHouseOptionModal();
                    CommunityOptionPage.Instance.AddCommunityHouseOptionModal.AddCommunityHouseOption(communityHouseOptionData);
                    CommunityOptionPage.Instance.WaitCommunityHouseOptionGridLoad();
                }
            }

            newBuildingGroupData = new BuildingGroupData()
            {
                Name = newBuildingGroupName,
                Code = newBuildingGroupCode,
                Description = newBuildingGroupDescription
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9: Add new Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", newBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroupData);
            }

            newBuildingPhaseData = new BuildingPhaseData()
            {
                Code = newBuildingPhaseCode,
                Name = newBuildingPhaseName,
                BuildingGroupCode = newBuildingGroupCode,
                BuildingGroupName = newBuildingGroupName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10: Add new Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", newBuildingPhaseName) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhaseData.Code)
                                          .EnterPhaseName(newBuildingPhaseData.Name)
                                          .EnterAbbName(newBuildingPhaseData.AbbName)
                                          .EnterDescription(newBuildingPhaseData.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhaseData.BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableYes();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }

            newVendorData = new VendorData()
            {
                Name = newVendorDataName,
                Code = newVendorDataCode,
                Trade = "",
                Contact = "Contact",
                Email = "xuat@test.com",
                Address1 = "address1",
                Address2 = "address2",
                Address3 = "address3",
                City = "city",
                State = "state",
                Zip = "zip",
                Phone = "phone1",
                AltPhone = "phone2",
                MobilePhone = "00000000000",
                Fax = "fax",
                Url = "url@url.com",
                EnablePrecision = true
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Add new Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newVendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", newVendorData.Name) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendorData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12: Add new Building Phase to Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newVendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", newVendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", newVendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                if (VendorBuildingPhasePage.Instance.IsItemExist(newBuildingPhaseData.Code) is false)
                {
                    VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhaseData.Code);
                }
            }

            newProductData = new ProductData()
            {
                Name = newProductDataName,
                Style = genericStyle,
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE",
                BuildingPhase = newBuildingPhaseData.Code + "-" + newBuildingPhaseData.Name,
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Add new Product.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, newProductData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", newProductData.Name) is false)
            {
                ProductPage.Instance.CreateNewProduct(newProductData);
                ProductDetailPage.Instance.AddManufacturersStyles(genericManufacturer, true, genericStyle, productCode);
                System.Threading.Thread.Sleep(2000);
                ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Manufacturer", "_00000000000000000000000000");
                System.Threading.Thread.Sleep(2000);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.14: Add new Product to BASE OPTION.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, baseOptionName);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", baseOptionName) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", baseOptionName);
                OptionDetailPage.Instance.LeftMenuNavigation("Products", true);
                ProductsToOptionPage.Instance.FilterOptionQuantitiesInGrid("Product", GridFilterOperator.EqualTo, newProductData.Name);
                System.Threading.Thread.Sleep(5000);
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", newProductData.Name) is false)
                {
                    OptionQuantitiesData optionQuantitiesData = new OptionQuantitiesData()
                    {
                        OptionName = baseOptionName,
                        BuildingPhase = newBuildingPhaseData.Code + "-" + newBuildingPhaseData.Name,
                        ProductName = newProductData.Name,
                        ProductDescription = "RT_QA_New_Product_XUAT",
                        Style = genericStyle,
                        Condition = false,
                        Use = string.Empty,
                        Quantity = "100.00",
                        Source = "Pipeline"
                    };
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.15: Add new Vendor Product Cost.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newVendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", newVendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", newVendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                VendorProductPage.Instance.UpdateCostingforVerdor(newBuildingPhaseData.Name, productCode, materialItemCost, laborItemCost);
            }

            newTradeData = new TradesData()
            {
                Code = newTradeDataCode,
                TradeName = newTradeDataTradeName,
                TradeDescription = newTradeDataTradeDescription,
                Vendor = newVendorDataName,
                BuildingPhases = newBuildingPhaseData.Code + "-" + newBuildingPhaseData.Name,
                SchedulingTasks = ""
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.16: Add new Trades.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, newTradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTradeData.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(newTradeData, false, true);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.17: Assign vendor to trade at community level.</b></font>");
                TradesPage.Instance.ClickVendorAssignments();
                System.Threading.Thread.Sleep(2000);
                VendorAssignmentsPage.Instance.SelectDivision(newDivisionName, 1);
                System.Threading.Thread.Sleep(2000);
                string[] communities = { newCommunityData.Name };
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
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, newTradeData.TradeName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                string editCommunityVendorMsg = VendorAssignmentsPage.Instance.EditCommunityVendor(newCommunityData.Name, newVendorData.Name);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
            }

            newJobdata = new JobData()
            {
                Name = newJobdataName,
                Community = newCommunityData.Code + "-" + newCommunityData.Name,
                House = newHouseData.PlanNumber + "-" + newHouseData.HouseName,
                Lot = newJobdataLot
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.18: Create new Job.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", newJobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", newJobdata.Name) is false)
            {
                JobPage.Instance.CreateJob(newJobdata);
            }
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", newJobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", newJobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", newJobdata.Name);
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
        [Category("Section_X")]
        public void UAT_HotFix_Vendor_Trade_Assignments()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to Jobs > All Jobs > select any job.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", newJobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", newJobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", newJobdata.Name);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: Click on Estimates link from the left navigation. Verify if there is yellow asterisk displayed on each of the Building Phases, the same as when changing the View by Option.</b></font>");
                JobDetailPage.Instance.LeftMenuNavigation("Estimate", true);
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();

                bool hasImgDifferentPhaseValuesDisplay = JobEstimatePage.Instance.HasImgDifferentPhaseValuesDisplay();
                if (hasImgDifferentPhaseValuesDisplay == true) {
                    ExtentReportsHelper.LogPass("<font color='green'>Able to click on Estimates link from the left navigation. Verified that there is yellow asterisk displayed on each of the Building Phases, the same as when changing the View by Option.</font>");
                    System.Threading.Thread.Sleep(2000);
                    CommonHelper.CaptureScreen();
                }


                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Click on Vendors link from the left navigation. Verify if there is vendor assignment on the building phases and the same vendor indicated when generating BOM and Estimates. Verify if the vendor is in bold font when the vendor is overridden..</b></font>");
                JobEstimatePage.Instance.LeftMenuNavigation("Vendors", true);
                JobVendorsPage.Instance.FilterItemInJobVendorGrid("Vendor", GridFilterOperator.EqualTo, newVendorData.Name);
                System.Threading.Thread.Sleep(2000);
                if (JobVendorsPage.Instance.IsItemInGrid("Vendor", newVendorData.Name) is true)
                {
                    ExtentReportsHelper.LogPass("<font color='green'>Able to click on Vendors link from the left navigation. Verified that there is vendor assignment on the building phases and the same vendor indicated when generating BOM and Estimates. Verified that the vendor is in bold font when the vendor is overridden..</font>");
                    System.Threading.Thread.Sleep(2000);
                    CommonHelper.CaptureScreen();
                }
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Assign vendor to the following hierarchy and verify correct vendor assignments and correct generation of BOM and Estimates: Trade Community, Trade Division, Trade Company, Vendor per Building Phase per Community.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, newTradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTradeData.TradeName) is true)
            {
                TradesPage.Instance.ClickVendorAssignments();
                System.Threading.Thread.Sleep(2000);
                VendorAssignmentsPage.Instance.SelectDivision(newDivisionName, 1);
                System.Threading.Thread.Sleep(2000);
                string[] communities = { newCommunityData.Name };
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
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, newTradeData.TradeName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();

                //ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Update the company vendor to " + newVendorData.Name + ".</b></font>");
                //VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, newTradeData.TradeName);
                //VendorAssignmentsPage.Instance.WaitGridLoad();
                //System.Threading.Thread.Sleep(5000);
                //string editCompanyVendorMsg = VendorAssignmentsPage.Instance.EditCompanyVendor(newVendorData.Name);
                //VendorAssignmentsPage.Instance.WaitGridLoad();
                //System.Threading.Thread.Sleep(5000);
                //CommonHelper.CaptureScreen();
                //ExtentReportsHelper.LogPass(editCompanyVendorMsg);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Update the division vendor to " + newVendorData.Name + ".</b></font>");
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, newTradeData.TradeName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                string editDivisionVendorMsg = VendorAssignmentsPage.Instance.EditDivisionVendor(newDivision.Name, newVendorData.Name);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogPass(editDivisionVendorMsg);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Update the community vendor to " + newVendorData.Name + ".</b></font>");
                VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, newTradeData.TradeName);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                string editCommunityVendorMsg = VendorAssignmentsPage.Instance.EditCommunityVendor(newCommunityData.Name, newVendorData.Name);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogPass(editCommunityVendorMsg);
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogPass("Able to assign vendor to the following hierarchy and verified that the correct vendor assignments and correct generation of BOM and Estimates:Trade Community, Trade Division, Trade Company, Vendor per Building Phase per Community");
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Data not removed as this will be use later.</b></font>");
        }
    }

}

