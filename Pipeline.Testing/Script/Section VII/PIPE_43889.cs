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
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Jobs.Job.Vendors;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeUser;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using Pipeline.Testing.Pages.UserMenu.User;

namespace Pipeline.Testing.Script.Section_VII
{
    public class PIPE_43889 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VII);
        }

        private JobData jobdata;
        private SeriesData seriesData;
        private HouseData houseData;
        private CommunityData communityData;
        private BuildingGroupData buildingGroupData;
        private BuildingPhaseData buildingPhaseData;
        private ProductData productData;
        private VendorData vendorData;
        private VendorData vendorData2;
        private TradesData tradeData;
        private TradesData tradeDataBuilderVendor;
        private UserData userdata;
        private UserData userdata2;
        private OptionQuantitiesData optionQuantitiesData;
        private DivisionData newDivision;

        private const string newDivisionName = "RT_QA_New_Division_43889";

        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_43889";
        private const string NewBuildingGroupCode = "43889";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_43889";
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_43889";
        private const string NewBuildingPhaseCode = "4388";

        private const string baseOptionName = "BASE";
        private const string baseOptionNumber = "00001";

        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "4388";
        private const string laborItemCost = "$10.00";
        private const string materialItemCost = "$20.00";

        private const bool IsBuilderVendorEnabled = false; //set to TRUE when the Builder Vendor Feature is enabled in Carbonite

        [SetUp]
        public void SetupTestData()
        {
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Series_43889",
                Code = "43889",
                Description = "RT_QA_Series_43889"
            };
            houseData = new HouseData()
            {
                PlanNumber = "4388",
                HouseName = "RT_QA_House_43889",
                SaleHouseName = "RT_QA_House_43889",
                Series = "RT_QA_Series_43889"
            };


            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };

            
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_43889",
                Code = "RT_QA_Community_43889"
            };

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
            productData = new ProductData()
            {
                Name = "RT_QA_New_Product_43889",
                Manufacture = genericManufacturer,
                Style = genericStyle,
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE",
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
            };
            vendorData = new VendorData()
            {
                Name = "RT_QA_New_Vendor_43889",
                Code = "43889"
            };
            vendorData2 = new VendorData()
            {
                Name = "RT_QA_New_Vendor_438892",
                Code = "438892"
            };
            optionQuantitiesData = new OptionQuantitiesData()
            {
                OptionName = baseOptionName,
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                ProductName = productData.Name,
                ProductDescription = "RT_QA_New_Product_43889",
                Style = genericStyle,
                Condition = false,
                Use = string.Empty,
                Quantity = "100.00",
                Source = "Pipeline"
            };
            tradeData = new TradesData()
            {
                Code = "43889",
                TradeName = "RT_QA_New_BuildingTrade_43889",
                TradeDescription = "RT_QA_New_BuildingTrade_43889",
                Vendor = vendorData.Name,
                BuildingPhases = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                SchedulingTasks = "",
                IsBuilderVendor = false
            };
            userdata = new UserData()
            {
                UserName = "RT_QA_New_User_43889",
                Password = "123456",
                ConfirmPass = "123456",
                Email = "RT43889@gmail.com",
                Role = "Prospect",
                Active = "TRUE",
                FirstName = "RTQA",
                LastName = "43889",
                Phone = "1228706916",
                Ext = "2",
                Cell = "14",
                Fax = "987654321",
                Address1 = "14 Tan Hai",
                Address2 = "14/01 Tan Hai",
                City = "HCM",
                State = "IN",
                Zip = "1"
            };
            userdata2 = new UserData()
            {
                UserName = "RT_QA_New_User_438892",
                Password = "123456",
                ConfirmPass = "123456",
                Email = "RT438892@gmail.com",
                Role = "Prospect",
                Active = "TRUE",
                FirstName = "RTQA",
                LastName = "438892",
                Phone = "1228706916",
                Ext = "2",
                Cell = "14",
                Fax = "987654321",
                Address1 = "14 Tan Hai",
                Address2 = "14/01 Tan Hai",
                City = "HCM",
                State = "IN",
                Zip = "1"
            };
            tradeDataBuilderVendor = new TradesData()
            {
                Code = "43889BV",
                TradeName = "RT_QA_New_BuildingTrade_43889BV",
                TradeDescription = "RT_QA_New_BuildingTrade_43889BV",
                IsBuilderVendor = true,
                BuilderVendor = userdata.FirstName + " " + userdata.LastName + " - " + userdata.Email,
                SchedulingTasks = ""
            };

            jobdata = new JobData()
            {
                Name = "RT_QA_Job_43889",
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Lot = "RT_QA_Lot_43889"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Add new Series test data.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, seriesData.Name);
            System.Threading.Thread.Sleep(5000);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                SeriesPage.Instance.CreateSeries(seriesData);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(houseData);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add BASE Option to new House test data.</b></font>");
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


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(communityData);
            }


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
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10: Add new Building Phase test data.</b></font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Add new Vendor test data.</b></font>");



            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendorData);
            }

            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData2.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData2.Name) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendorData2);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12: Add new Building Phase to Vendors.</b></font>");
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

            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData2.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData2.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData2.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                if (VendorBuildingPhasePage.Instance.IsItemExist(buildingPhaseData.Code) is false)
                {
                    VendorBuildingPhasePage.Instance.AddBuildingPhase(buildingPhaseData.Code);
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Add new Product test data.</b></font>");

            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, productData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is false)
            {
                ProductPage.Instance.CreateNewProduct(productData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.14: Add new Product to BASE OPTION.</b></font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.15: Add new Vendor Product cost data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Products", true);
                VendorProductPage.Instance.UpdateCostingforVerdor(buildingPhaseData.Name, productCode, materialItemCost, laborItemCost);
            }


            if(IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.16: Add test Users.</b></font>");

                UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
                UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, userdata.UserName);
                if (UserPage.Instance.IsItemInGrid("Username", userdata.UserName) is false)
                {
                    UserPage.Instance.ClickAddUserButton();
                    UserPage.Instance.CreateNewUser(userdata);
                }

                UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
                UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, userdata2.UserName);
                if (UserPage.Instance.IsItemInGrid("Username", userdata2.UserName) is false)
                {
                    UserPage.Instance.ClickAddUserButton();
                    UserPage.Instance.CreateNewUser(userdata2);
                }
            }
           

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.17: Add new Trade qualified as vendor.</b></font>");

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(tradeData);
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeData.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeData.TradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", tradeData.TradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, vendorData2.Name);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", vendorData2.Name) is false)
                {
                    TradeVendorPage.Instance.ShowAddVendorToTradeModal();
                    string[] vendorsList = { vendorData2.Name };
                    TradeVendorPage.Instance.AddVendorToTradeModal.SelectVendors(vendorsList);
                    TradeVendorPage.Instance.AddVendorToTradeModal.Save();
                    System.Threading.Thread.Sleep(1000);
                }
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
                System.Threading.Thread.Sleep(10000);
                CommonHelper.CaptureScreen();
                VendorAssignmentsPage.Instance.EditCommunityVendor(communityData.Name, vendorData.Name);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
            }

            if(IsBuilderVendorEnabled)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.19: Add new Trade qualified as builder vendor.</b></font>");

                TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
                TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeDataBuilderVendor.TradeName);
                System.Threading.Thread.Sleep(2000);
                if (TradesPage.Instance.IsItemInGrid("Trade", tradeDataBuilderVendor.TradeName) is false)
                {
                    TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                    TradesPage.Instance.CreateTrade(tradeDataBuilderVendor);

                }

                TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
                TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeDataBuilderVendor.TradeName);
                System.Threading.Thread.Sleep(2000);
                if (TradesPage.Instance.IsItemInGrid("Trade", tradeDataBuilderVendor.TradeName) is true)
                {
                    TradesPage.Instance.SelectItemInGrid("Trade", tradeDataBuilderVendor.TradeName);
                    TradeDetailPage.Instance.LeftMenuNavigation("Builder Users", true);
                    TradeBuilderUserPage.Instance.FilterItemInGrid("Builder User Email", GridFilterOperator.EqualTo, userdata2.Email);
                    if (TradeBuilderUserPage.Instance.IsItemInGrid("Builder User Email", userdata2.Email) is false)
                    {
                        TradeBuilderUserPage.Instance.ShowAddUserToTradeModal();
                        TradeBuilderUserPage.Instance.AddUserToTradeModal.SelectUser(userdata2.FirstName + " " + userdata2.LastName + " - " + userdata2.Email);
                        TradeBuilderUserPage.Instance.AddUserToTradeModal.Save();
                        System.Threading.Thread.Sleep(1000);
                    }

                }

                TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
                TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeDataBuilderVendor.TradeName);
                System.Threading.Thread.Sleep(2000);
                if (TradesPage.Instance.IsItemInGrid("Trade", tradeDataBuilderVendor.TradeName) is true)
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.20: Assign vendor to trade qualified as builder vendor at community level.</b></font>");
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
                    VendorAssignmentsPage.Instance.FilterItemInGrid("Trades", GridFilterOperator.EqualTo, tradeDataBuilderVendor.TradeName);
                    VendorAssignmentsPage.Instance.WaitGridLoad();
                    System.Threading.Thread.Sleep(10000);
                    CommonHelper.CaptureScreen();
                    VendorAssignmentsPage.Instance.EditCommunityVendor(communityData.Name, userdata.FirstName + " " + userdata.LastName);
                    VendorAssignmentsPage.Instance.WaitGridLoad();
                    System.Threading.Thread.Sleep(5000);
                    CommonHelper.CaptureScreen();
                }
            }
        }

        [Test]
        public void Job_Vendor_Trade_Logic()
        {
            
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
                JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM", true);
                JobBOMPage.Instance.GenerateJobBOM();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Navigate to Job Vendors page.</b></font>");
                JobBOMPage.Instance.LeftMenuNavigation("Vendors", true);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Verify if columns are available in the grid.</b></font>");
                if (JobVendorsPage.Instance.IsColumnFoundInGrid("Trades") is true)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Trades column is found in the grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Trades column is not found in the grid.</b></font>");

                if (JobVendorsPage.Instance.IsColumnFoundInGrid("Vendor") is true)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Vendor column is found in the grid.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Vendor column is not found in the grid.</b></font>");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Edit the Job Vendor for the trade.</b></font>");
                string expectedMessage = "Successfully updated Vendor Assignments for " + tradeData.TradeName + "!";
                string actualMessage = "";
                JobVendorsPage.Instance.FilterItemInJobVendorGrid("Trades", GridFilterOperator.EqualTo, tradeData.TradeName);
                if(JobVendorsPage.Instance.IsItemInGrid("Trades", tradeData.TradeName) is true)
                {
                    actualMessage = JobVendorsPage.Instance.EditItemInGrid("Trades", tradeData.TradeName, vendorData2.Name);
                    if (expectedMessage == actualMessage)
                    {
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>" + actualMessage + "</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>The vendor assignment failed to update.</b></font>");
                    }
                }
                if (IsBuilderVendorEnabled)
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Edit the Job Vendor for the builder vendor trade.</b></font>");
                    expectedMessage = "Successfully updated Vendor Assignments for " + tradeDataBuilderVendor.TradeName + "!";
                    JobVendorsPage.Instance.FilterItemInJobVendorGrid("Trades", GridFilterOperator.EqualTo, tradeDataBuilderVendor.TradeName);
                    if (JobVendorsPage.Instance.IsItemInGrid("Trades", tradeDataBuilderVendor.TradeName) is true)
                    {
                        actualMessage = JobVendorsPage.Instance.EditItemInGrid("Trades", tradeDataBuilderVendor.TradeName, userdata2.FirstName + " " + userdata2.LastName);
                        if (expectedMessage == actualMessage)
                        {
                            ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>" + actualMessage + "</b></font>");
                        }
                        else
                        {
                            ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>The vendor assignment failed to update.</b></font>");
                        }
                    }
                        
                }
                

                //regenerate job estimates verify the vendor is updated
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Generate Job BOM and Estimates.</b></font>");
                JobVendorsPage.Instance.LeftMenuNavigation("Estimate", true);
                JobEstimatePage.Instance.GenerateBomAndEstimates();
                System.Threading.Thread.Sleep(5000);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Verify if the Vendor used is from the Job Level.</b></font>");
                bool vendorsMatch = JobEstimatePage.Instance.CheckVendorForPhase(buildingPhaseData.Code + "-" + buildingPhaseData.Name, vendorData2.Name);
                if (vendorsMatch)
                {
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>The expected vendor is displayed in the Job Estimates for the updated trade.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>The expected vendor is NOT displayed in the Job Estimates for the updated trade.</b></font>");
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
                System.Threading.Thread.Sleep(20000);
            }

            //ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Delete test product from BASE Option.</b></font>");
            //ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            //ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, productData.Name);
            //System.Threading.Thread.Sleep(2000);
            //if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is true)
            //{
            //    ProductPage.Instance.DeleteProduct(productData.Name);
            //}

            //DeleteTradeRelations(tradeData.TradeName);
            //DeleteTrade(tradeData.TradeName);

            //DeleteTrade(tradeDataBuilderVendor.TradeName);

            //DeleteVendor(vendorData.Name);
            //DeleteVendor(vendorData2.Name);

            //DeleteBuildingPhase();
            //DeleteBuildingGroup();

            //DeleteUser(userdata.UserName);
            //DeleteUser(userdata2.UserName);



        }

        private void DeleteTradeRelations(string tradeName)
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            CommonHelper.RefreshPage();
            CommonHelper.CaptureScreen();
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", tradeName);
                TradeDetailPage.Instance.LeftMenuNavigation("Building Phases", true);

                TradeBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
                System.Threading.Thread.Sleep(2000);
                if (TradeBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName))
                {
                    TradeBuildingPhasePage.Instance.DeleteItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
                }
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            CommonHelper.RefreshPage();
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", tradeName);
                TradeVendorPage.Instance.LeftMenuNavigation("Vendors", true);
                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, vendorData.Name);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", vendorData.Name))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", vendorData.Name);
                }
            }

            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            CommonHelper.RefreshPage();
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                TradesPage.Instance.SelectItemInGrid("Trade", tradeName);
                TradeVendorPage.Instance.LeftMenuNavigation("Vendors", true);
                TradeVendorPage.Instance.FilterItemInGrid("Vendor Name", GridFilterOperator.EqualTo, vendorData2.Name);
                if (TradeVendorPage.Instance.IsItemInGrid("Vendor Name", vendorData2.Name))
                {
                    TradeVendorPage.Instance.DeleteItemInGrid("Vendor Name", vendorData2.Name);
                }
            }
        }
        private void DeleteTrade(string tradeName)
        {
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, tradeName);
            if (TradesPage.Instance.IsItemInGrid("Trade", tradeName) is true)
            {
                TradesPage.Instance.DeleteItemInGrid("Trade", tradeName);
                CommonHelper.RefreshPage();
            }
        }

        private void DeleteVendor(string vendorName)
        {
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, vendorName);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorName) is true)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", vendorName);
                VendorPage.Instance.WaitGridLoad();
            }
        }

        private void DeleteBuildingGroup()
        {
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is true)
            {
                BuildingGroupPage.Instance.DeleteItemInGrid("Name", NewBuildingGroupName);
                BuildingGroupPage.Instance.WaitGridLoad();
            }
        }

        private void DeleteBuildingPhase()
        {
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
                BuildingPhasePage.Instance.WaitGridLoad();
            }
        }

        private void DeleteUser(string userName)
        {
            UserPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Users);
            UserPage.Instance.FilterItemInGrid("Username", GridFilterOperator.EqualTo, userName);
            if (UserPage.Instance.IsItemInGrid("Username", userName))
            {
                UserPage.Instance.DeleteItemInGrid("Username", userName);
            }
        }
    }
}
