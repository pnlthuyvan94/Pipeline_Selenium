using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
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
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;
namespace Pipeline.Testing.Script.Section_III
{
    class C01_G_PIPE_31232 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private JobData jobData;
        OptionData _option;
        HouseData HouseData;
        CommunityData communityData;
        LotData _lotdata;
        JobQuantitiesData jobQuantities;

        private string importFileDir;
        private readonly int[] indexs = new int[] { };
        private string exportFileName;

        private const string OPTION = "OPTION";
        private static string OPTION_NAME_DEFAULT = "QA_RT_Option02_Automation";
        private static string OPTION_CODE_DEFAULT = "0200";
        private readonly string COMMUNITY_CODE_DEFAULT = "_001";
        private readonly string COMMUNITY_NAME_DEFAULT = "HN-QA-Community";
        private readonly string HOUSE_NAME_DEFAULT = "HN-QA-House-02";
        private readonly string JOB_NAME_DEFAULT = "QA_RT_Job02_Automation";

        string[] OptionData = { OPTION_NAME_DEFAULT };

        private const string JOB_BOM_VIEW_MODE = "Option/Phase/Product";

        readonly string EXPORT_JOBBOM_TO_CSV = "Export CSV";
        readonly string EXPORT_JOBBOM_TO_EXCEL = "Export Excel";
        readonly string EXPORT_JOBBOM_TO_XML = "Export XML";
        readonly string EXPORT_JOBBOM_TO_XML_WITH_TRACE = "Export XML w/ Trace";

        private ProductData productData_Option_1;
        private ProductData productData_Option_2;
        private ProductData productData_Option_3;

        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToOption3;

        private ProductData productData_House_1;
        private ProductData productData_House_2;
        private ProductData productData_House_3;

        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToHouse3;

        private ProductToOptionData productToHouseBOM1;
        private ProductToOptionData productToHouseBOM2;
        private ProductToOptionData productToHouseBOM3;

        private HouseQuantitiesData houseQuantities;
        private HouseQuantitiesData houseQuantities_JobBOM;

        [SetUp]
        public void GetData()
        {
            jobData = new JobData()
            {
                Name = "QA_RT_Job02_Automation",
                Community = "_001-HN-QA-Community",
                House = "_002-HN-QA-House-02",
                Lot = "_001 - Sold",
                Orientation = "None"
            };

            var optionType = new List<bool>()
            {
                false, false, false
            };
            _option = new OptionData()
            {
                Name = "QA_RT_Option02_Automation",
                Number = "0200",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };

            HouseData = new HouseData()
            {
                HouseName = "HN-QA-House-02",
                SaleHouseName = "The house which created by Hai Nguyen",
                Series = "Hai Nguyen Series",
                PlanNumber = "_002",
                BasePrice = "1000000.00",
                SQFTBasement = "200",
                SQFTFloor1 = "200",
                SQFTFloor2 = "200",
                SQFTHeated = "0",
                SQFTTotal = "0",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "Hai Nguyen create house - testing"
            };

            communityData = new CommunityData()
            {
                Name = "HN-QA-Community",
                Division = "None",
                Code = "_001",
                City = "Ho Chi Minh",
                CityLink = "https://hcm.com",
                Township = "Ho Chi Minh",
                County = "Texas",
                State = "TX",
                Zip = "70000",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Community from automation test v1",
                Slug = "HN-QA-Community",
            };

            _lotdata = new LotData()
            {
                Number = "_001",
                Status = "Sold"
            };

            jobQuantities = new JobQuantitiesData()
            {
                Option = OPTION_NAME_DEFAULT,
                BuildingPhase = { "Au_1-RT_QA_New_Building_Phase_Auto_01" },
                Source = "Pipeline",
                Products = { "RT_QA_New_Product_Auto_01", "RT_QA_New_Product_Auto_02", "RT_QA_New_Product_Auto_03" },
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantity = { "10.00", "20.00", "30.00" }

            };
            productData_Option_1 = new ProductData()
            {
                Name = "RT_QA_New_Product_Auto_01",
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantities = "10.00",
                Unit = "NONE",
            };

            productData_Option_2 = new ProductData()
            {
                Name = "RT_QA_New_Product_Auto_02",
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantities = "20.00",
                Unit = "NONE",
            };
            productData_Option_3 = new ProductData()
            {
                Name = "RT_QA_New_Product_Auto_03",
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE",
            };

            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "Au_1-RT_QA_New_Building_Phase_Auto_01",
                ProductList = new List<ProductData> { productData_Option_1 }
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "Au_1-RT_QA_New_Building_Phase_Auto_01",
                ProductList = new List<ProductData> { productData_Option_2 }
            };

            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "Au_1-RT_QA_New_Building_Phase_Auto_01",
                ProductList = new List<ProductData> { productData_Option_3 }
            };

            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2);

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_House_3 = new ProductData(productData_Option_3);

            // There is no House quantities 4

            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };
            productToHouse3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3 } };

            houseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2, productToHouse3 }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"
            ProductData productData_HouseBOM_1 = new ProductData(productData_Option_1);
            ProductData productData_HouseBOM_2 = new ProductData(productData_Option_2);
            ProductData productData_HouseBOM_3 = new ProductData(productData_Option_3);

            productToHouseBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };

            productToHouseBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_HouseBOM_2 } };

            productToHouseBOM3 = new ProductToOptionData(productToHouse3) { ProductList = new List<ProductData> { productData_HouseBOM_3 } };

            houseQuantities_JobBOM = new HouseQuantitiesData(houseQuantities)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM2, productToHouseBOM3 },
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

            // Prepare data for Option Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data for Option Data.</font>");

            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter Item In Grid
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"The Option with name { _option.Name} is displayed in grid.");
            }
            else
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed.</font>");
                }
                // Create Option - Click 'Save' Button
                OptionPage.Instance.AddOptionModal.AddOption(_option);
                string _expectedMessage = $"Option Number is duplicated.";
                string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Option with name { _option.Name} and Number {_option.Number}.");
                }
                BasePage.PageLoad();
            }

            //Prepare data for Community Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data for Community Data.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

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
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Import Lot into Community.</font>");
                CommonHelper.SwitchLastestTab();
                LotPage.Instance.ImportExporFromMoreMenu("Import");
                importFileDir = "Pipeline_Lots_In_Community.csv";
                LotPage.Instance.ImportFile("Lot Import", $@"\DataInputFiles\Assets\Community\{importFileDir}");
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

            //Prepare data for House Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data for House Data.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {HouseData.HouseName} and create if it doesn't exist.");
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
            ExtentReportsHelper.LogInformation(null, $"Switch to House/ Options page. Add option '{OPTION_NAME_DEFAULT}' to house '{HOUSE_NAME_DEFAULT}' if it doesn't exist.");
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
                Name = "RT_QA_New_Manu_Auto"
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
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "RT_QA_New_Style_Auto",
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
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "12111111",
                Name = "QA_RT_New_Building_Group_Auto_01"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_31232\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_31232\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }
        [Test]
        [Category("Section_III")]
        public void C01_G_ALLJobs_Exports_JobBOM_NativeExports()
        {

            //1.Navigation to http://betaautomated.bimpipeline.com/Dashboard/Login.aspx  , select ALL JOBS from JOBS dropdown list on Main menu
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Config Setting Page</font>");
            // Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();

            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            //Navigate to Jobs menu > All Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Jobs menu > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a new Job.</font>");
                JobPage.Instance.CreateJob(jobData);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"The Job {jobData.Name} is exited");
                JobPage.Instance.DeleteJob(jobData.Name);
                JobPage.Instance.CreateJob(jobData);
            }

            //Check Header in BreadsCrumb 
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(jobData.Name))
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The Header is present correctly.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Header is present incorrectly.</font>");
            }

            // Step 2: Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2: Switch to Job/ Options page. Add Option '{OPTION_NAME_DEFAULT}' to job if it doesn't exist.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = OPTION_CODE_DEFAULT + "-" + OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }

            //Step 3: Switch to Job/ Quantities page. Apply System Quantities.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(jobQuantities);

            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(houseQuantities_JobBOM);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4 :Switch to Job/ Job BOM page. Generate Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");
            JobBOMPage.Instance.VerifyJobBomPageIsDisplayed("Generated BOMs");

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: On Job Side Navigation, click the Job BOM to open the Job BOM page.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_JobBOM);

            //Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.Job_BOM.ToString(), JOB_NAME_DEFAULT);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: Verify the Export/Import functions<b>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1: Verify 'EXPORT CSV' function.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);
            JobBOMPage.Instance.DownloadBaseLineJobBOMFile(EXPORT_JOBBOM_TO_CSV, exportFileName + "_OptionsView");
            JobBOMPage.Instance.ExportJobBOMFile(EXPORT_JOBBOM_TO_CSV, exportFileName + "_OptionsView", 0, ExportTitleFileConstant.JOBBOM_TITLE);
            //JobBOMPage.Instance.CompareExportFile(exportFileName + "_OptionsView", TableType.CSV);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2: Verify 'EXPORT Excel' function.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);
            JobBOMPage.Instance.DownloadBaseLineJobBOMFile(EXPORT_JOBBOM_TO_EXCEL, exportFileName + "_OptionsView");
            JobBOMPage.Instance.ExportJobBOMFile(EXPORT_JOBBOM_TO_EXCEL, exportFileName + "_OptionsView", 0, ExportTitleFileConstant.JOBBOM_TITLE);
            //JobBOMPage.Instance.CompareExportFile(exportFileName + "_OptionsView", TableType.XLSX);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.3: Verify 'EXPORT XML' function.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);
            JobBOMPage.Instance.DownloadBaseLineJobBOMFile(EXPORT_JOBBOM_TO_XML, exportFileName + "_OptionsView_Config");
            JobBOMPage.Instance.ExportJobBOMFile(EXPORT_JOBBOM_TO_XML, exportFileName + "_OptionsView_Config", 0, ExportTitleFileConstant.JOBBOM_TITLE);
            //JobBOMPage.Instance.CompareExportFile(exportFileName + "_OptionsView_Config", TableType.XML);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.4: Verify 'Export XML w/ Trace' function.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);
            JobBOMPage.Instance.DownloadBaseLineJobBOMFile(EXPORT_JOBBOM_TO_XML_WITH_TRACE, exportFileName + "_OptionsView_Config_WithTrace");
            JobBOMPage.Instance.ExportJobBOMFile(EXPORT_JOBBOM_TO_XML_WITH_TRACE, exportFileName + "_OptionsView_Config_WithTrace", 0, ExportTitleFileConstant.JOBBOM_TITLE);
            //JobBOMPage.Instance.CompareExportFile(exportFileName + "_OptionsView_Config_WithTrace", TableType.XML);
        }


        [TearDown]
        public void DeleteData()
        {
            //Delete Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Delete a new Job.<b>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData.Name);
            JobPage.Instance.WaitGridLoad();
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }
        }

    }
}
