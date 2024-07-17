using NUnit.Framework;
using OpenQA.Selenium;
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
using Pipeline.Testing.Pages.Costing.OptionBidCost;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_III
{
    public class D01_F_PIPE_41553 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private const double NewCostAmount = 10.00;

        private JobData newJobData;
        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_41553";
        private const string NewVendorCode = "41553";

        private OptionData newOption;
        private const string NewOptionName = "RT_QA_New_Option_41553";
        private const string NewOptionNumber = "41553";

        private OptionBidCostData newOptionBidCost;

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_41553";
        private const string NewBuildingGroupCode = "41553";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_41553";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_41553";
        private const string NewBuildingPhaseCode = "1553";

        private HouseData newHouse;
        private const string NewHouseName = "RT_QA_New_House_41553";
        private const string NewHousePlanNo = "4155";
        private static string House_Tier = "House";

        private CommunityData newCommunity;
        private const string NewCommunityName = "RT_QA_New_Community_41553";

        //private const string DivisionName = "CG Visions";

        private string exportFileName;
        private const string ExportCsv = "Export CSV";
        private const string ExportExcel = "Export Excel";

        private ProductData newproductData;

        private const string BaseCostImport = "Product Base Costs Import";
        private const string newDivisionName = "RT_QA_New_Division_41553";
        private DivisionData newDivision;

        [SetUp]
        public void Setup()
        {
            //Initialize test data 
            newJobData = new JobData()
            {
                Name = "QA_RT_Job11_Automation",
                Community = "Auto_33017-QA_Community_PIPE_33017_Automation",
                House = "0110-QA_RT_House11_Automation",
                Lot = "_111 - Sold",
                Orientation = "None",
            };

            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };

            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
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

            newOptionBidCost = new OptionBidCostData()
            {
                Job = "KN_job1",
                JobOption = "KN_OPTION 1 - 01",
                JobCost = "50",
                BuildingPhase1 = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
                BuildingPhase2 = "-001-CCCC"
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

            newproductData = new ProductData()
            {
                Name = "ProductDetail_AutomationTesting_DoNotDelete",
                Manufacture = "GENERIC",
                Style = "GENERIC",
                Code = "5555",
                Description = "Description for testing",
                Notes = "Notes for testing",
                Unit = "NONE",
                SKU = "1234",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
                BuildingPhase = newBuildingPhase.Code + "-" + newBuildingPhase.Name,
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Create new Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Create new Option.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, "");
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, "");
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewOptionName);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", NewOptionName) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(newOption);
            }

            OptionPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, newBuildingGroup.Code);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", newBuildingGroup.Code) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add new Building Phase.</b></font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5 Add Building Phase to Vendor.</b></font>");
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", newVendor.Name);
            VendorPage.Instance.SelectVendor("Name", newVendor.Name);
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is false)
            {
                System.Threading.Thread.Sleep(1500);
                VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhase.Code);
                VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();
                System.Threading.Thread.Sleep(2000);
            }

            //setup house data and add option to house
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

            //setup community data and add option to community 
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
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivision.Name);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivision.Name) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", newDivision.Name);
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
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivision.Name);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivision.Name) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", newDivision.Name);
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
                CommonHelper.RefreshPage();
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
                CommonHelper.RefreshPage();
                CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
                System.Threading.Thread.Sleep(2000);
                if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is true)
                {
                    CommunityVendorPage.Instance.EditVendorAssignments(NewBuildingPhaseName, NewVendorName);
                }
            }

            //Below is commented out because it is not necessary for the test
            //Product
            //Navigate Assets > Option Selection Group and open Option Selection Group Detail page
            ExtentReportsHelper.LogInformation("<b>Step 1: Navigate Estimating/ Product.</b>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, newproductData.Name);
            System.Threading.Thread.Sleep(2000);
            if (ProductPage.Instance.IsItemInGrid("Product Name", newproductData.Name) is false)
            {
                ProductPage.Instance.CreateNewProduct(newproductData);
            }
        }

        [Test]
        public void D01_F_Costing_Vendors_Vendor_Base_Costs_Export_Import()
        {
            //Navigate to Costing > Vendors > select vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0 Navigate to Costing and select a Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);

            //Click Base Costs link from the left navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Navigate to Base Costs link from the left navigation under Product Cost section.</b></font>");
            VendorDetailPage.Instance.LeftMenuNavigation("Base Costs", true);

            //Click on Utilities icon in the upper right part of the page and select Export CSV option from the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Export Product Base Costs.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Export CSV.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName("")}VendorBaseLevelCosts_{NewVendorName}";
            VendorProductPage.Instance.ExportFile(ExportCsv, exportFileName, 0, ExportTitleFileConstant.VENDOR_BASE_COSTS);
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Export Excel.</b></font>");
            VendorProductPage.Instance.ExportFile(ExportExcel, exportFileName, 0, ExportTitleFileConstant.VENDOR_BASE_COSTS);
            CommonHelper.RefreshPage();

            //Add a new entry on the exported .csv file and import
            string importFile = "";
            string expectedErrorMessage = "";
            CostingImportPage.Instance.OpenImportPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Import Product Base Costs.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Import Product Base Costs.</b></font>");
            if (CostingImportPage.Instance.IsImportLabelDisplay(BaseCostImport) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {BaseCostImport} grid view to import new Base Cost.</font>");

            importFile = "\\DataInputFiles\\Costing\\ProductCostsImport\\BaseCostsImport\\Pipeline_BaseCosts.csv";
            ImportValidData(BaseCostImport, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.1:  Import Base Cost Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\ProductCostsImport\\BaseCostsImport\\Pipeline_BaseCosts.txt";
            expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(BaseCostImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.2:  Import Base Cost format import file.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\ProductCostsImport\\BaseCostsImport\\Pipeline_BaseCosts_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(BaseCostImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.3:  Import Base Cost File without header.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\ProductCostsImport\\BaseCostsImport\\Pipeline_BaseCosts_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(BaseCostImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2.4:  Import Base Cost File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\ProductCostsImport\\BaseCostsImport\\Pipeline_BaseCosts_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
            ImportInvalidData(BaseCostImport, importFile, expectedErrorMessage);

            //Navigate to Costing > Vendors > select vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0 Navigate to Costing and select a Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);
            VendorDetailPage.Instance.LeftMenuNavigation("Base Costs", true);
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();

        }


        [TearDown]
        public void ClearData()
        {            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0 No Tear Down of Data because the existing data will re used in the Imports.</b></font>");
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

            if (actualMessage.ToLower().Contains(expectedFailedData.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file failed to import and the toast message indicated failure.</b></font>");

        }
    }
}
