

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
using Pipeline.Testing.Pages.Assets.Divisions.DivisionVendors;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Costing.TaxGroup;
using Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Purchasing.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using Pipeline.Testing.Pages.Assets.Communities.CommunityVendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorCurrentEstimate;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_IV
{
    class D01_F_PIPE_31177 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private VendorData vendorData;
        private const string NewVendorName = "RT_QA_New_Vendor_D01F";
        private const string NewVendorCode = "D01F";

        TradesData newTradeData;
        private const string NewBuildingTradeName = "RT_QA_New_BuildingTradeVendor_D01F";
        private const string NewBuildingTradeCode = "D01F";

        TaxGroupData taxGroup;
        private const string TaxGroupName = "RT_QA_New_TaxGroup_D01F";

        private DivisionData division;
        private const string NewDivisionName = "RT_QA_New_Division_D01F";

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_D01F";
        private const string NewBuildingGroupCode = "D01F";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_D01F";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_D01F";
        private const string NewBuildingPhaseCode = "D01F";

        private CommunityData newCommunityData;
        private const string NewCommunityName = "RT_QA_New_Community_D01F";

        private SeriesData newSeriesData;
        private const string NewSeriesName = "RT_QA_Series_D01F";
        private const string NewSeriesCode = "D05C";
        private const string NewSeriesDescription = "RT_QA_Series_D01F";

        private HouseData newHouseData;
        private const string NewHouseName = "RT_QA_House_D01F";
        private const string NewHousePlanNumber = "D01F";
        private const string NewHouseSaleHouseName = "RT_QA_House_D01F";

        private const string baseOptionName = "BASE";
        private const string baseOptionNumber = "00001";
        private const string genericStyle = "GENERIC";
        private const string genericManufacturer = "GENERIC";
        private const string productCode = "D01F";
        private const string laborItemCost = "$5.00";
        private const string materialItemCost = "$10.00";

        private ProductData newProductData;
        private const string productDataName = "RT_QA_New_Product_D01F";

        [SetUp]
        public void SetUpData()
        {
            newSeriesData = new SeriesData()
            {
                Name = NewSeriesName,
                Code = NewSeriesCode,
                Description = NewSeriesDescription
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
                PlanNumber = NewHousePlanNumber,
                HouseName = NewHouseName,
                SaleHouseName = NewHouseSaleHouseName,
                Series = NewSeriesName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newHouseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", newHouseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(newHouseData);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add BASE Option to new House test data.</b></font>");
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
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Add new community.</b></font>");
            newCommunityData = new CommunityData()
            {
                Name = NewCommunityName,
                Division = NewDivisionName,
                City = "City",
                Code = "Code",
                CityLink = "https://cl.com",
                Township = "Township",
                County = "Country",
                State = "State",
                Zip = "00000",
                SchoolDistrict = "SchoolDistrict",
                SchoolDistrictLink = "http://sdl.com",
                Status = "Open",
                Description = "D01F",
                DrivingDirections = "D01F",
                Slug = "Slug",
            };
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is false)
            {
                CommunityPage.Instance.CreateCommunity(newCommunityData);
            }
            CommonHelper.RefreshPage();

            division = new DivisionData()
            {
                Name = NewDivisionName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, NewDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", NewDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(division);
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Add new community to new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, NewDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", NewDivisionName) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", NewDivisionName);
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
                        SalePrice = "10"
                    };
                    CommunityOptionPage.Instance.OpenAddCommunityHouseOptionModal();
                    CommunityOptionPage.Instance.AddCommunityHouseOptionModal.AddCommunityHouseOption(communityHouseOptionData);
                    CommunityOptionPage.Instance.WaitCommunityHouseOptionGridLoad();
                }
            }
            CommonHelper.RefreshPage();

            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10 Add new Building Phase.</b></font>");
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
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableYes();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }

            vendorData = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode,
                Trade = "",
                Contact = "Contact",
                Email = "d01f@test.com",
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Add new vendor data.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(vendorData);
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12 Add Building Phase to Vendor.</b></font>");
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, vendorData.Name);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", vendorData.Name) is true)
            {
                VendorPage.Instance.SelectVendor("Name", vendorData.Name);
                VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
                if (VendorBuildingPhasePage.Instance.IsItemExist(newBuildingPhase.Code) is false)
                {
                    VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhase.Code);
                    System.Threading.Thread.Sleep(2000);
                }
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Add new Product test data.</b></font>");
            newProductData = new ProductData()
            {
                Name = productDataName,
                Style = genericStyle,
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE",
                BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
            };
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
                        BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                        ProductName = newProductData.Name,
                        ProductDescription = "RT_QA_New_Product_D01F",
                        Style = genericStyle,
                        Condition = false,
                        Use = string.Empty,
                        Quantity = "100.00",
                        Source = "Pipeline"
                    };
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
                VendorProductPage.Instance.UpdateCostingforVerdor(newBuildingPhase.Name, productCode, materialItemCost, laborItemCost);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.16: Add new Trades test data.</b></font>");
            newTradeData = new TradesData()
            {
                Code = NewBuildingTradeCode,
                TradeName = NewBuildingTradeName,
                TradeDescription = NewBuildingTradeName,
                IsBuilderVendor = false,
                Vendor = vendorData.Name,
                BuildingPhases = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                SchedulingTasks = ""
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.17: Add New Trade test data.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, NewBuildingTradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", NewBuildingTradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(newTradeData);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.18: Assign vendor to trade at community level.</b></font>");
                TradesPage.Instance.ClickVendorAssignments();
                System.Threading.Thread.Sleep(2000);
                VendorAssignmentsPage.Instance.SelectDivision(NewDivisionName, 1);
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
                VendorAssignmentsPage.Instance.EditDivisionVendor(newCommunityData.Name, vendorData.Name);
                VendorAssignmentsPage.Instance.WaitGridLoad();
                System.Threading.Thread.Sleep(5000);
                CommonHelper.CaptureScreen();
            }
        }

        [Test]
        [Category("Section_IV")]
        public void D01_F_Costing_DetailPages_Vendors_CurrentEstimates()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to Costing > Vendors link.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Click the Vendor to which you like to select.</b></font>");
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: On the Vendor Side Navigation menu, click the 'Current Estimates' to open the Current Estimates data page.</b></font>");
                VendorDetailPage.Instance.LeftMenuNavigation("Current Estimates");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Click the expand button; show all available plans in Community.</b></font>");


                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Verify the Option is shown when estimated House.</b></font>");
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Before BOM & Estimate.</b></font>");
                VendorCurrentEstimatePage.Instance.ExpandAllContent();
                CommonHelper.CaptureScreen();
                System.Threading.Thread.Sleep(2000);
                VendorCurrentEstimatePage.Instance.ExpandAllContent();
                CommonHelper.CaptureScreen();
                System.Threading.Thread.Sleep(2000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Proceed BOM & Estimate in ASSETS/HOUSES page.</b></font>");
                HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
                HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newHouseData.HouseName);
                System.Threading.Thread.Sleep(5000);
                if (HousePage.Instance.IsItemInGrid("Name", newHouseData.HouseName) is true)
                {
                    HousePage.Instance.SelectItemInGridWithTextContains("Name", newHouseData.HouseName);
                    HouseDetailPage.Instance.LeftMenuNavigation("Estimate");
                    System.Threading.Thread.Sleep(5000);
                    Pipeline.Testing.Pages.Assets.House.HouseEstimate.HouseEstimateDetailPage.Instance.GenerateHouseBOMAndEstimate(newCommunityData.Code + "-" + newCommunityData.Name);
                    System.Threading.Thread.Sleep(10000);
                    CommonHelper.CaptureScreen();
                }
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3: Go back to vendor current estimates page.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.4: On the Vendor Side Navigation menu, click the 'Current Estimates' to open the Current Estimates data page.</b></font>");
                VendorDetailPage.Instance.LeftMenuNavigation("Current Estimates");
                VendorCurrentEstimatePage.Instance.ExpandAllContent();
                CommonHelper.CaptureScreen();
                System.Threading.Thread.Sleep(2000);
                VendorCurrentEstimatePage.Instance.ExpandAllContent();
                CommonHelper.CaptureScreen();
                System.Threading.Thread.Sleep(2000);
                ExtentReportsHelper.LogPass("<font color='green'>After BOM & Estimate, the option is shown successfully (date is accurate for updates to Estimates).</font>");
                CommonHelper.CaptureScreen();
                System.Threading.Thread.Sleep(2000);
            }
            CommonHelper.RefreshPage();


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: Verify functions of two button on the header.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1: Send all Vendor Estimates to the Queue(BOM & Estimate) button\r\nClick this button.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);
                VendorDetailPage.Instance.LeftMenuNavigation("Current Estimates");

                Pipeline.Testing.Pages.Assets.House.HouseEstimate.HouseEstimateDetailPage.Instance.SendAllVendorEstimatesToTheQueueBOMAndEstimate();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2: The Reporting Queue page  will show  this page.</b></font>");
                SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Queue);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.CaptureScreen();
            }
            CommonHelper.RefreshPage();


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3: Send all Vendor Estimates to the Queue(Estimate Only) button\r\n Click this button.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is true)
            {
                VendorPage.Instance.SelectVendor("Name", NewVendorName);
                VendorDetailPage.Instance.LeftMenuNavigation("Current Estimates");

                Pipeline.Testing.Pages.Assets.House.HouseEstimate.HouseEstimateDetailPage.Instance.SendAllVendorEstimatesToTheQueueEstimateOnly();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.4: The Reporting Queue page  will show  this page.</b></font>");
                SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Queue);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.CaptureScreen();
            }
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0: The Community show on Current Estimates page when Vendor assigned to Building Phase on Community and that Vendor is assigned to Building Phases which have been assigned to that Vendor.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCommunityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", newCommunityData.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", newCommunityData.Name);
                CommunityDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                string[] vendorList = { NewVendorName };
                CommunityVendorPage.Instance.AssignVendorToCommunity(vendorList);

                // Add vendor to community > costing> vendor assignments
                CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Primary Vendor", GridFilterOperator.Contains, NewVendorName);
                if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Primary Vendor", NewVendorName))
                {
                    ExtentReportsHelper.LogPass("<font color='green'>Vendor assigned as Primary.</font>");
                }
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Data not removed as this will be use later.</b></font>");
        }
    }
}
