using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Import;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class C01_PIPE_21014 : BaseTestScript
    {
        private readonly string IMPORT_PRODUCT_NAME = "QA_Product_01_Automation";
        private readonly string IMPORT_NON_EXIST_PRODUCT = "QA_Product_01_Automation New";
        private readonly bool IS_OPTION_SPECIFIED = false;

        private JobData jobData;
        HouseData HouseData;
        CommunityData communityData;
        LotData _lotdata;

        private const string OPTION = "OPTION";
        private static string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";
        private static string OPTION_NAME_DEFAULT = "Option_QA_RT_Automation";
        private static string OPTION_CODE_DEFAULT = "Auto_Number";

        private static string HOUSE_NAME_DEFAULT = "QA_RT_House04_Automation";

        string[] OptionData = { OPTION_NAME_DEFAULT };

        private readonly int[] indexs = new int[] { };
        private string importFileDir;

        private JobImportQuantitiesData expectedData_EmptyPhase;
        private JobImportQuantitiesData expectedData_NonExistProduct;
        private JobImportQuantitiesData expectedData_ExistBuildingPhase;
        private JobImportQuantitiesData expectedData_NonExistBuildingPhase;



        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        [SetUp]
        public void SetUp()
        {
            jobData = new JobData()
            {
                Name = "RT-QA_JOB_Import_Quantity",
                Community = "Automation_01-QA_RT_Community01_Automation",
                House = "400-QA_RT_House04_Automation",
                Lot = "_111 - Sold",
                Orientation = "Left",
            };

            HouseData = new HouseData()
            {
                HouseName = "QA_RT_House04_Automation",
                SaleHouseName = "QA_RT_House04_Sales_Name",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "400",
                BasePrice = "1000000",
                SQFTBasement = "1",
                SQFTFloor1 = "1",
                SQFTFloor2 = "2",
                SQFTHeated = "3",
                SQFTTotal = "7",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "Test"
            };

            communityData = new CommunityData()
            {
                Name = "QA_RT_Community01_Automation",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "Automation_01",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Nothing to say v1",
                DrivingDirections = "Nothing to say v2",
                Slug = "R-QA-Only-Community-Auto"
            };

            _lotdata = new LotData()
            {
                Number = "_111",
                Status = "Sold"
            };

            expectedData_EmptyPhase = new JobImportQuantitiesData()
            {
                Option = "RECONCILED",
                BuildingPhaseCode = "QA_1",
                BuildingPhaseName = "QA_BuildingPhase_01_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Quantities = 250
            };

            expectedData_NonExistProduct = new JobImportQuantitiesData()
            {
                Option = "RECONCILED",
                BuildingPhaseCode = "QA_2",
                BuildingPhaseName = "QA_BuildingPhase_02_Automation",
                ProductName = IMPORT_NON_EXIST_PRODUCT,
                Quantities = 260
            };

            expectedData_ExistBuildingPhase = new JobImportQuantitiesData()
            {
                Option = "RECONCILED",
                BuildingPhaseCode = "QA_3",
                BuildingPhaseName = "QA_BuildingPhase_03_Automation",
                ProductName = "QA_Product_03_Automation",
                Quantities = 270
            };

            expectedData_NonExistBuildingPhase = new JobImportQuantitiesData()
            {
                Option = "RECONCILED",
                BuildingPhaseCode = "QA_4",
                BuildingPhaseName = "QA_BuildingPhase_04_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Quantities = 280
            };

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Turn OFF Sage 300 and MS NAV.<b></b></font>");
            CommunityPage.Instance.SetSage300AndNAVStatus(false);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.2: Open Lot page, verify Lot button displays or not.<b></b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Try to open Lot URL of any community and verify Add lot button
            CommonHelper.OpenURL(BaseDashboardUrl + "/Builder/Communities/Lots.aspx?cid=1");
            if (LotPage.Instance.IsAddLotButtonDisplay() is false)
            {
                ExtentReportsHelper.LogWarning(null, $"<font color='lavender'><b>Add lot button doesn't display to continue testing. Stop this test script.</b></font>");
                Assert.Ignore("Add lot button doesn't display after set NAV and Sage 300 to Running. Stop this test script");
            }

            // Delete prduct before import a new one
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.3: Navigate to Estimating > Product and filter product '{IMPORT_NON_EXIST_PRODUCT}' and delete if it's existing.</b></font>");

            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            // Filter and delete product
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, expectedData_NonExistProduct.ProductName);
            if (ProductPage.Instance.IsItemInGrid("Product Name", IMPORT_NON_EXIST_PRODUCT))
            {
                ProductPage.Instance.DeleteProduct(expectedData_NonExistProduct.ProductName);
            }


            //Import Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Import Option.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_OPTION);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_IMPORT} grid view to import new Options.</font>");

            string importOptionFile = "\\DataInputFiles\\Import\\PIPE_21014\\ImportOption\\Pipeline_Options.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importOptionFile);

            //Navigate To Community Page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Community default page.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter community with name {communityData.Name} and create if it doesn't exist.</b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (!CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);
            }
            else
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
            }

            //Add Option into Community
            ExtentReportsHelper.LogInformation(null, "Add Option into Community.");
            CommunityDetailPage.Instance.LeftMenuNavigation("Options");
            CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
            if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION_NAME_DEFAULT) is false)
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData);
            }


            //Naviage To Community Lot
            CommunityDetailPage.Instance.LeftMenuNavigation("Lots");
            string LotPageUrl = LotPage.Instance.CurrentURL;
            if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
            {
                ExtentReportsHelper.LogInformation($"Lot with Number {_lotdata.Number} and Status {_lotdata.Status} is displayed in grid");
            }
            else
            {
                //Import Lot in Community
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Open Import page.</b></font>");
                CommonHelper.SwitchLastestTab();
                LotPage.Instance.ImportExporFromMoreMenu("Import");
                importFileDir = "Pipeline_Lots_In_Community.csv";
                LotPage.Instance.ImportFile("Lot Import", $@"\DataInputFiles\Import\PIPE_21014\ImportLot\{importFileDir}");
                CommonHelper.OpenURL(LotPageUrl);

                //Check Lot Numbet in grid 
                if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
                {
                    ExtentReportsHelper.LogPass("Import Lot File is successful");
                }
                else
                {
                    ExtentReportsHelper.LogFail("Import Lot File is unsuccessful");
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Create a new Series.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);
            SeriesData seriesData = new SeriesData()
            {
                Name = "QA_RT_Serie3_Automation",
                Code = "",
                Description = "Please no not remove or modify"
            };

            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                // Create a new series
                SeriesPage.Instance.CreateSeries(seriesData);
            }

            //Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House default page.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {HouseData.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HouseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(HouseData);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"House with Name {HouseData.HouseName} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HouseData.HouseName);

            }

            // Navigate House Option And Add Option into House
            ExtentReportsHelper.LogInformation(null, $"Switch to House/ Options page. Add option '{OPTION_NAME_DEFAULT}'  to house '{HOUSE_NAME_DEFAULT}' if it doesn't exist.");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }


            //Navigate House Communities And Check Community Data on grid
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");
            HouseCommunities.Instance.FillterOnGrid("Name", COMMUNITY_NAME_DEFAULT);
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT, indexs);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Community with Name {COMMUNITY_NAME_DEFAULT} is displayed in grid");
            }

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_Manu_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            // Prepare a new Style to import Product.
            // Can't import new Style then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
            StyleData styleData = new StyleData()
            {
                Name = "QA_Style_Automation",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);

            BuildingGroupData buildingGroupData1 = new BuildingGroupData()
            {
                Code = "_0001",
                Name = "QA_Building_Group_Automation"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData1.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData1.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData1);
            }


            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importBuildingPhaseFile = "\\DataInputFiles\\Import\\PIPE_21014\\ImportBuildingPhase\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importBuildingPhaseFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importProductFile = "\\DataInputFiles\\Import\\PIPE_21014\\ImportProduct\\Pipeline_Products_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void C01_E_Jobs_AllJobs_Import_JobQuantities_NoOptionSpecified()
        {
            // Step 1.1: Navigate to Jobs > All Jobs and filter job 'RT-QA_JOBBOM' and delete if it's existing
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Jobs > All Jobs and filter job 'RT-QA_JOBBOM' and delete if it's existing.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData.Name);
            JobPage.Instance.WaitGridLoad();

            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name))
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }

            // Step 1.2: Create a new job with name 'RT-QA_JOB_Import_Quantity' 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Create a new job with name 'RT-QA_JOB_Import_Quantity'.</b></font>");
            JobPage.Instance.CreateJob(jobData);

            // Step 1.3: Open Option page from left navigation and approve config
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Open Option page from left navigation and approve config.</b></font>");
            JobOptionPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed is false)
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            ExtentReportsHelper.LogPass(null, "<font color='green'><b>Job > Option page displays correctly.</b></font>");


            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION_NAME_DEFAULT) is false)
            {
                string[] selectedOption = { OPTION_CODE_DEFAULT + "-" + OPTION_NAME_DEFAULT };

                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }

            // Step 2.1: Open Import page from left navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Open Import page from left navigation.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Import", false);
            if (JobImportPage.Instance.IsImportPageDisplayed is false)
                ExtentReportsHelper.LogFail("<font color='red'>Import Job Quantities page doesn't display or title is incorrect.</font>");
            ExtentReportsHelper.LogPass(null, "<font color='green'><b>Import Job Quantities page displays correctly.</b></font>");

            // Step 2.2: Select No Option Specified button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Select No Option Specified button.</b></font>");
            JobImportPage.Instance.ClickNoOptionSpecified();



            // Step 3 - 4: Import file with EMPTY Building Phase code and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 3 - 4: Import file with EMPTY Building Phase code and verify it. -------------------------</b></font>");
            string importFileName = "QA_JOB_Import_Quantity_WithoutPhaseCode.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_OPTION_SPECIFIED);

            // Step 5.2: Expand grid view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2: Expand grid view.</b></font>");
            JobImportPage.Instance.ExpandProductQuantitiesOnImportGrid();

            // Step 5.3: Verify Product Quantities on the import grid.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3: Verify Product Quantities on the import grid.</b></font>");
            JobImportPage.Instance.VerifyProductQuantitiesToImport(expectedData_EmptyPhase);


            // Step 6.1: Click Finish Import button. Expected: Import successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1: Click Finish Import button. Expected: Import successfully.</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            // Step 6.2: Open the Job Quantities page on a new tab. All products should be shown correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2: Open the Job Quantities page on a new tab. All products should be shown correctly.</b></font>");

            var jobQuantity_URL = JobImportPage.Instance.GetJobQuantitiesURL();
            CommonHelper.OpenLinkInNewTab(jobQuantity_URL);
            CommonHelper.SwitchTab(1);
            JobImportPage.Instance.VerifyQuantitiesOnJobGrid(expectedData_EmptyPhase);


            // Step 7: Import file with NOT EXISTING PRODUCT on PL and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 7: Import file with NOT EXISTING PRODUCT on PL and verify it. -------------------------</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.1: Upload job quantities file again.</b></font>");
            CommonHelper.SwitchTab(0);
            importFileName = "QA_JOB_Import_Quantity_Productnotexist.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_OPTION_SPECIFIED);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.2: Verify a new panel display with non existing product. Continue create product.</b></font>");
            JobImportPage.Instance.ProcessNonExistProductQuantities(expectedData_NonExistProduct);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.3: Click Finish Impor button. Expected: Import successfully</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.4: Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");
            CommonHelper.SwitchTab(1);
            CommonHelper.RefreshPage();
            JobImportPage.Instance.VerifyQuantitiesOnJobGrid(expectedData_NonExistProduct);



            // Step 8: Import file with EXISTING BUILDING PHASE ON PL and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 8: Import file with EXISTING BUILDING PHASE ON PL and verify it. -------------------------</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.1: Upload job quantities file again.</b></font>");
            CommonHelper.SwitchTab(0);
            importFileName = "QA_JOB_Import_Quantity_WithPhaseCode.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_OPTION_SPECIFIED);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.2: Verify a new panel display with non existing product. Continue create product.</b></font>");
            JobImportPage.Instance.VerifyProductQuantitiesToImport(expectedData_ExistBuildingPhase);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.3: Click Finish Impor button. Expected: Import successfully</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.4: Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");
            CommonHelper.SwitchTab(1);
            CommonHelper.RefreshPage();
            JobImportPage.Instance.VerifyQuantitiesOnJobGrid(expectedData_ExistBuildingPhase);



            // Step 9: Import file with NOT EXISTING BUILDING PHASE ON PL and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 9: Import file with NOT EXISTING BUILDING PHASE ON PL and verify it. -------------------------</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.1: Upload job quantities file again.</b></font>");
            CommonHelper.SwitchTab(0);
            importFileName = "QA_JOB_Import_Quantity_PhaseCodenotexist.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_OPTION_SPECIFIED);

            // Import with non-existing building phase => get default phase of product and display it
            expectedData_NonExistBuildingPhase.BuildingPhaseCode = "QA_4";
            expectedData_NonExistBuildingPhase.BuildingPhaseName = "QA_BuildingPhase_04_Automation";


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.2: Verify a new panel display with non existing product. Continue create product.</b></font>");
            JobImportPage.Instance.VerifyProductQuantitiesToImport(expectedData_NonExistBuildingPhase);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.3: Click Finish Impor button. Expected: Import successfully</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.4: Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");
            CommonHelper.SwitchTab(1);
            CommonHelper.RefreshPage();
            JobImportPage.Instance.VerifyQuantitiesOnJobGrid(expectedData_NonExistBuildingPhase);


            // Close current tab
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

        }
        #endregion

        [TearDown]
        public void TearDownScript()
        {
            // Step 10.1: Delete current job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.1: Delete current job.</b></font>");

            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData.Name);
            JobPage.Instance.WaitGridLoad();
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name))
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }

            // Step 10.2: Delete product from step 7
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.2: Delete product from step 7.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);

            // Filter and delete product
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, expectedData_NonExistProduct.ProductName);
            if (ProductPage.Instance.IsItemInGrid("Product Name", IMPORT_NON_EXIST_PRODUCT))
            {
                ProductPage.Instance.DeleteProduct(expectedData_NonExistProduct.ProductName);
            }
        }
    }
}
