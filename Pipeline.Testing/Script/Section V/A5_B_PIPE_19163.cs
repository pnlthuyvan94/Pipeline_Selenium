using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.Options.Bid_Costs;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Estimating.Products;
using System.Collections.Generic;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;

namespace Pipeline.Testing.Script.Section_V
{
    public partial class A5_B_PIPE_19163 : BaseTestScript
    {
        private static string COMMUNITY_CODE_DEFAULT = "0330";
        private static string COMMUNITY_NAME_DEFAULT = "QA_RT_Community";
        private static string COMMUNITY_VALUE = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT;

        private static string OPTION_NAME_DEFAULT = "R-QA Only Option Automation";
        private static string OPTION_CODE_DEFAULT = "97531";

        private static string HOUSE_NAME_DEFAULT = "RT-QA_HOUSE";
        private static string HOUSE_CODE_DEFAULT = "RT1";

        private static string JOB_NUMBER_DEFAULT = "Automation_Job_PhaseBid_Supplemental";

        private static string BUILDING_PHASE_NAME_DEFAULT = "PERMITS and FEES";
        private static string BUILDING_PHASE_CODE_DEFAULT = "1000";
        private static string PHASE_VALUE = BUILDING_PHASE_CODE_DEFAULT + "-" + BUILDING_PHASE_NAME_DEFAULT;

        private static string PRODUCT_NAME_DEFAULT = "Auto-Prod-DONOTUSE";
        private static string PRODUCT_DESCRIPTION_DEFAULT = "This is the product for automation purpose, please DO NOT USE OR DELETE";

        private const string JOB_BOM_VIEW_MODE = "Option/Phase/Product";

        private OptionBuildingPhaseData optionBuildingPhaseData;
        //private static double ALLOWANCE = 10.00;

        private OptionQuantitiesData optionQuantitiesData;
        private static string OPTION_QUANTITIES = "15.00";
        private const string OPTION = "OPTION";

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_V);
        }

        ProductData productData_Base;
        ProductToOptionData productToOption_Base;
        HouseQuantitiesData houseQuantities_HouseBOM_Base;


        [SetUp]
        public void GetData()
        {
            /* --------------------- Create Data -------------------*/
            optionBuildingPhaseData = new OptionBuildingPhaseData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = new string[] { PHASE_VALUE },
                Name = "Auto test",
                Description = "Auto test",
                //Allowance = ALLOWANCE
            };

            optionQuantitiesData = new OptionQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE_VALUE,
                ProductName = PRODUCT_NAME_DEFAULT,
                ProductDescription = PRODUCT_DESCRIPTION_DEFAULT,
                Style = "Vintage",
                Condition = false,
                Use = string.Empty,
                Quantity = OPTION_QUANTITIES,
                Source = "Pipeline"
            };

            productData_Base = new ProductData()
            {
                Name = optionQuantitiesData.ProductName,
                Style = optionQuantitiesData.Style,
                Quantities = optionQuantitiesData.Quantity.ToString()
            };

            productToOption_Base = new ProductToOptionData()
            {
                BuildingPhase = optionQuantitiesData.BuildingPhase,
                PhaseBid = string.Empty,
                ProductList = new List<ProductData> { productData_Base }
            };

            houseQuantities_HouseBOM_Base = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                houseName = HOUSE_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToOption_Base }
            };


            /* --------------------- Open Page -------------------*/

            ExtentReportsHelper.LogInformation(null, "<font color='Yellow'><b>Section 0: Open 8 tab with below information.</b></font>");

            // Tab 0: Dashboard page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.0: Tab 0 is Daskboard page.</b></font>");

            // Tab 1: Setting page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Tab 1 is Setting page. Turn ON 'Job BOM Show Zero Quantities'</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings, true, true);
            CommonHelper.SwitchTab(1);
            // Turn ON "Job BOM Show Zero Quantities"
            BOMSettingPage.Instance.LeftMenuNavigation("BOM", false);
            BOMSettingPage.Instance.SelectJobBOMShowZeroQuantities(true);

            // Tab 2: Option/ Product page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Tab 2 is Option/ Product page.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options, true, true);
            CommonHelper.SwitchTab(2);

            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                // Stop test script in case there is no option with name "BASE"
                Assert.Fail($"Can't find option with name {OPTION_NAME_DEFAULT} to continue running this test script.");
            }
            OptionPage.Instance.SelectItemInGrid("Name", OPTION_NAME_DEFAULT);
            OptionPage.Instance.LeftMenuNavigation("Products");

            // Tab 3: Option/ Bid Costs page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Tab 3 is Option/ Bid Costs page.</b></font>");
            OptionPage.Instance.LeftMenuNavigation("Bid Costs", true, true);
            CommonHelper.SwitchTab(3);

            // Tab 4: House/ Options page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Tab 4 is House/ Options page.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses, true, true);
            CommonHelper.SwitchTab(4);

            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is false)
            {
                // Stop test script in case there is no option with name "RT-QA_HOUSE"
                Assert.Fail($"Can't find House with name {HOUSE_NAME_DEFAULT} to continue running this test script.");
            }

            // Go to detail page
            HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            HousePage.Instance.LeftMenuNavigation("Options");

            // Tab 5: House/ House BOM page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Tab 5 is House/ House BOM page.</b></font>");
            HousePage.Instance.LeftMenuNavigation("House BOM", false, true);
            CommonHelper.SwitchTab(5);

            // Tab 6: Job/ Options page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Tab 6 is Job/ Options page.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs, true, true);
            CommonHelper.SwitchTab(6);

            JobPage.Instance.EnterJobNameToFilter("Job Number", JOB_NUMBER_DEFAULT);
            if (JobPage.Instance.IsItemInGrid("Job Number", JOB_NUMBER_DEFAULT) is false)
            {
                // Stop test script in case there is no JOB with name "QA_JOB_40"
                Assert.Fail($"Can't find Job with number {JOB_NUMBER_DEFAULT} to continue running this test script.");
            }

            // Go to detail page
            JobPage.Instance.SelectItemInGrid("Job Number", JOB_NUMBER_DEFAULT);
            JobQuantitiesPage.Instance.LeftMenuNavigation("Options");

            // Tab 7: Job/ Quantities page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Tab 7 is Job/ Quantities page.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Quantities", false, true);
            CommonHelper.SwitchTab(7);

            // Tab 8: Job/ Job BOM page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Tab 8 is Job/ Job BOM page.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM", false, true);
            CommonHelper.SwitchTab(8);

            // Tab 9: Product/ Product detail page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9: Tab 8 is Job/ Job BOM page.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products, true, true);
            CommonHelper.SwitchTab(9);

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, PRODUCT_NAME_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT_NAME_DEFAULT) is false)
            {
                // Stop test script in case there is no product with name "RT-QA_HOUSE"
                Assert.Fail($"Can't find Product with name '{PRODUCT_NAME_DEFAULT}' to continue running this test script.");
            }

            // Go to detail page
            ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT_NAME_DEFAULT);
        }

        [Test]
        [Category("Section_V")]
        [Ignore("This test scripts will be ignored at this time and be fixed in the future.")]
        public void A5_B_PipelineBOM_HouseBOM_JobBOM_PhaseBID_Supplemental()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='Yellow'><b>Section 1: Generate BOM that option HAS Phase Bid, NO Product.</b></font>");
            BOM_PhaseBid_NoProduct();

            ExtentReportsHelper.LogInformation(null, "<font color='Yellow'><b>Section 2: Generate BOM that option HAS Phase Bid, HAS Product.</b></font>");
            BOM_PhaseBid_Product();

            ExtentReportsHelper.LogInformation(null, "<font color='Yellow'><b>Section 3: Generate BOM that option has NO Phase Bid, Has Product.</b></font>");
            BOM_NoPhaseBid_Product();

            ExtentReportsHelper.LogInformation(null, "<font color='Yellow'><b>Section 4: Generate BOM that option HAS Phase Bid, HAS Product, HAS Supplemental.</b></font>");
            BOM_PhaseBid_Product_Supplemental();

            ExtentReportsHelper.LogInformation(null, "<font color='Yellow'><b>Section 5: Generate BOM that option has NO Phase Bid, HAS Product, HAS Supplemental.</b></font>");
            BOM_NoPhaseBid_Product_Supplemental();

            ExtentReportsHelper.LogInformation(null, "<font color='Yellow'><b>Section 6: Generate BOM that option has NO Phase Bid, NO Product.</b></font>");
            BOM_NoPhaseBid_NoProduct();
        }

        private void BOM_PhaseBid_NoProduct()
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.1: Switch to Option/ Bid Costs page. Add a new Bid costs if it does NOT exist on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(3);
            //BidCostsToOptionPage.Instance.FilterOptionBuildingPhaseByDropDownInGrid("Building Phase", PHASE_VALUE);
            if (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASE_VALUE) is false)
            {
                // Add a new Bid costs if it does NOT exist
                BidCostsToOptionPage.Instance.AddOptionBuildingPhase(optionBuildingPhaseData);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Switch to Option/ Product page. Delete all option product quantity on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(2);
            ProductsToOptionPage.Instance.FilterOptionQuantitiesByDropDownInGrid("Building Phase", PHASE_VALUE);
            while (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE_VALUE) is true)
            {
                ProductsToOptionPage.Instance.DeleteItemInGrid("Building Phase", PHASE_VALUE);
                ProductsToOptionPage.Instance.WaitOptionQuantitiesLoadingIcon();
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.3: Switch to House/ Options page. Add option '{OPTION_NAME_DEFAULT}' to house '{HOUSE_NAME_DEFAULT}' if it doesn't exist.</b></font>");
            CommonHelper.SwitchTab(4);
            //HouseOptionDetailPage.Instance.FilterItemInOptionnGrid("Name", GridFilterOperator.EqualTo, OPTION_NAME_DEFAULT);
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.4: Switch to Product/ Product detail page. Make sure the current product '{PRODUCT_NAME_DEFAULT}' isn't SUPPLEMENTAL.</b></font>");
            CommonHelper.SwitchTab(9);
            ProductDetailPage.Instance.IsSupplemental(false).Save();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.5: Switch to House/ House BOM page. Generate House BOM.</b></font>");
            CommonHelper.SwitchTab(5);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(COMMUNITY_VALUE);

            // Verify BOM
            ProductToOptionData productToOption = new ProductToOptionData(productToOption_Base)
            {
                PhaseBid = "Phase Bid Only"
            };
            HouseQuantitiesData houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities_HouseBOM_Base)
            {
                productToOption = new List<ProductToOptionData> { productToOption }
            };

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.6: Verify House BOM without product quantities on the grid view.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM, false);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.7: Switch to Job/ Options page. Add Option '{OPTION_NAME_DEFAULT}' to job if it doesn't exist.</b></font>");
            CommonHelper.SwitchTab(6);
            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
            }
            JobOptionPage.Instance.ClickApproveConfig();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.8: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            CommonHelper.SwitchTab(7);
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.8: Switch to Job/ Job BOM page. Generate Job BOM and verify it.</b></font>");
            CommonHelper.SwitchTab(8);
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.9: Verify House BOM WITHOUT product quantities on the grid view.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_HouseBOM, false);
        }

        private void BOM_PhaseBid_Product()
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.1: Switch to Option/ Bid Costs page. Add a new Bid costs if it does NOT exist on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(3);
            //BidCostsToOptionPage.Instance.FilterOptionBuildingPhaseByDropDownInGrid("Building Phase", PHASE_VALUE);
            if (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASE_VALUE) is false)
            {
                // Add a new Bid costs if it does NOT exist
                BidCostsToOptionPage.Instance.AddOptionBuildingPhase(optionBuildingPhaseData);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.2: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(2);
            ProductsToOptionPage.Instance.FilterOptionQuantitiesByDropDownInGrid("Building Phase", PHASE_VALUE);
            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE_VALUE) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.4: Switch to House/ House BOM page. Generate House BOM.</b></font>");
            CommonHelper.SwitchTab(5);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(COMMUNITY_VALUE);

            // Verify BOM
            ProductToOptionData productToOption = new ProductToOptionData(productToOption_Base)
            {
                PhaseBid = "Phase Bid Only"
            };
            HouseQuantitiesData houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities_HouseBOM_Base)
            {
                productToOption = new List<ProductToOptionData> { productToOption }
            };

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.5: Verify House BOM and product quantities on the grid view.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM, true);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.6: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            CommonHelper.SwitchTab(7);
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.7: Switch to Job/ Job BOM page. Generate Job BOM and verify it.</b></font>");
            CommonHelper.SwitchTab(8);
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.8: Verify House BOM with product quantities on the grid view.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_HouseBOM, true);
        }

        private void BOM_NoPhaseBid_Product()
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.1: Switch to Option/ Bid Costs page. Delete all product building phase on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(3);
            //BidCostsToOptionPage.Instance.FilterOptionBuildingPhaseByDropDownInGrid("Building Phase", PHASE_VALUE);
            while (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASE_VALUE) is true)
            {
                // Delete all Bid costs if it exists
                BidCostsToOptionPage.Instance.DeleteOptionBuildingPhase("Building Phase", PHASE_VALUE);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.2: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(2);

            //ProductsToOptionPage.Instance.FilterOptionQuantitiesByDropDownInGrid("Building Phase", PHASE_VALUE);
            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE_VALUE) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.3: Switch to Product/ Product detail page. Make sure the current product '{PRODUCT_NAME_DEFAULT}' isn't SUPPLEMENTAL.</b></font>");
            CommonHelper.SwitchTab(9);
            ProductDetailPage.Instance.IsSupplemental(false).Save();


            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.4: Switch to House/ House BOM page. Generate House BOM.</b></font>");
            CommonHelper.SwitchTab(5);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(COMMUNITY_VALUE);

            // Verify BOM
            ProductToOptionData productToOption = new ProductToOptionData(productToOption_Base)
            {
                PhaseBid = string.Empty
            };
            HouseQuantitiesData houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities_HouseBOM_Base)
            {
                productToOption = new List<ProductToOptionData> { productToOption }
            };

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.5: Verify House BOM without product quantities on the grid view.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM, true);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.6: Verify House BOM and product quantities on the grid view.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM, true);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.7: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            CommonHelper.SwitchTab(7);
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.8: Switch to Job/ Job BOM page. Generate Job BOM and verify it.</b></font>");
            CommonHelper.SwitchTab(8);
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.9: Verify House BOM with product quantities on the grid view.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_HouseBOM, true);
        }

        private void BOM_PhaseBid_Product_Supplemental()
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.1: Switch to Option/ Bid Costs page. Delete all product building phase on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(3);
            //BidCostsToOptionPage.Instance.FilterOptionBuildingPhaseByDropDownInGrid("Building Phase", PHASE_VALUE);
            if (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASE_VALUE) is false)
            {
                // Add a new Bid costs if it does NOT exist
                BidCostsToOptionPage.Instance.AddOptionBuildingPhase(optionBuildingPhaseData);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.2: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(2);
            ProductsToOptionPage.Instance.FilterOptionQuantitiesByDropDownInGrid("Building Phase", PHASE_VALUE);
            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE_VALUE) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.3: Switch to Product/ Product detail page. Make sure the current product '{PRODUCT_NAME_DEFAULT}' is SUPPLEMENTAL.</b></font>");
            CommonHelper.SwitchTab(9);
            ProductDetailPage.Instance.IsSupplemental(true).Save();


            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.4: Switch to House/ House BOM page. Generate House BOM.</b></font>");
            CommonHelper.SwitchTab(5);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(COMMUNITY_VALUE);

            // Verify BOM
            ProductToOptionData productToOption = new ProductToOptionData(productToOption_Base)
            {
                PhaseBid = "Phase Bid with Supplementals"
            };
            HouseQuantitiesData houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities_HouseBOM_Base)
            {
                productToOption = new List<ProductToOptionData> { productToOption }
            };

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.5: Verify House BOM without product quantities on the grid view.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM, false);

            // Verify Supplemental products on the House grid view
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.6: Verify Supplemental products on the grid view.</b></font>");
            HouseBOMDetailPage.Instance.VerifySupplementalByPhase(houseQuantities_HouseBOM);


            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.7: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            CommonHelper.SwitchTab(7);
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.8: Switch to Job/ Job BOM page. Generate Job BOM and verify it.</b></font>");
            CommonHelper.SwitchTab(8);
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.9: Verify House BOM WITHOUT product quantities on the grid view.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_HouseBOM, false);

            // Verify Supplemental products on the Job grid view
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4.6: Verify Supplemental products on the grid view.</b></font>");
            JobBOMPage.Instance.VerifySupplementalByPhase(houseQuantities_HouseBOM);
        }

        private void BOM_NoPhaseBid_Product_Supplemental()
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.1: Switch to Option/ Bid Costs page. Delete all product building phases on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(3);
            //BidCostsToOptionPage.Instance.FilterOptionBuildingPhaseByDropDownInGrid("Building Phase", PHASE_VALUE);
            while (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASE_VALUE) is true)
            {
                // Delete all Bid costs if it exists
                BidCostsToOptionPage.Instance.DeleteOptionBuildingPhase("Building Phase", PHASE_VALUE);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.2: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(2);
            ProductsToOptionPage.Instance.FilterOptionQuantitiesByDropDownInGrid("Building Phase", PHASE_VALUE);
            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE_VALUE) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.3: Switch to Product/ Product detail page. Make sure the current product '{PRODUCT_NAME_DEFAULT}' is SUPPLEMENTAL.</b></font>");
            CommonHelper.SwitchTab(9);
            ProductDetailPage.Instance.IsSupplemental(true).Save();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.4: Switch to House/ House BOM page. Generate House BOM.</b></font>");
            CommonHelper.SwitchTab(5);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(COMMUNITY_VALUE);

            // Verify BOM
            ProductToOptionData productToOption = new ProductToOptionData(productToOption_Base)
            {
                PhaseBid = string.Empty
            };
            HouseQuantitiesData houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities_HouseBOM_Base)
            {
                productToOption = new List<ProductToOptionData> { productToOption }
            };

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.5: Verify House BOM and product quantities on the grid view.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM, true);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.6: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            CommonHelper.SwitchTab(7);
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.7: Switch to Job/ Job BOM page. Generate Job BOM and verify it.</b></font>");
            CommonHelper.SwitchTab(8);
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.8: Verify House BOM with product quantities on the grid view.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_HouseBOM, true);
        }

        private void BOM_NoPhaseBid_NoProduct()
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.1: Switch to Option/ Bid Costs page. Delete all product building phase on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(3);
            //BidCostsToOptionPage.Instance.FilterOptionBuildingPhaseByDropDownInGrid("Building Phase", PHASE_VALUE);
            while (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASE_VALUE) is true)
            {
                // Delete all Bid costs if it exists
                BidCostsToOptionPage.Instance.DeleteOptionBuildingPhase("Building Phase", PHASE_VALUE);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.2: Switch to Option/ Product page. Delete all option product quantity on phase '{PHASE_VALUE}'.</b></font>");
            CommonHelper.SwitchTab(2);
            ProductsToOptionPage.Instance.FilterOptionQuantitiesByDropDownInGrid("Building Phase", PHASE_VALUE);
            while (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE_VALUE) is true)
            {
                ProductsToOptionPage.Instance.DeleteItemInGrid("Building Phase", PHASE_VALUE);
                ProductsToOptionPage.Instance.WaitOptionQuantitiesLoadingIcon();
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.3: Switch to House/ House BOM page. Generate House BOM.</b></font>");
            CommonHelper.SwitchTab(5);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(COMMUNITY_VALUE);

            // Verify BOM
            HouseQuantitiesData houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities_HouseBOM_Base)
            {
                productToOption = new List<ProductToOptionData> { }
            };

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.4: Verify House BOM and product quantities on the grid view.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM, true);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.5: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            CommonHelper.SwitchTab(7);
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.6: Switch to Job/ Job BOM page. Generate Job BOM and verify it.</b></font>");
            CommonHelper.SwitchTab(8);
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.7: Verify Job BOM with product quantities on the grid view.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_HouseBOM, true);
        }

        [TearDown]
        public void DeleteData()
        {
            // Close all tab exclude the current one
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }
    }
}