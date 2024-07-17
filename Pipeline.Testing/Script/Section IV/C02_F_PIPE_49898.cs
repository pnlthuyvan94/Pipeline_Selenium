
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class C02_F_PIPE_49898 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string EXPORT_CSV_MORE_MENU = "CSV";

        private const string EXPORT_EXCEL_MORE_MENU = "Excel";
        private const string EXPORT_NORMAL_VIEW_STYLE = "Normal";
        private const string EXPORT_FLAT_VIEW_STYLE = "Flat";
        public const string JOB_NAME_DEFAULT = "QA_RT_Job_Automation";

        public const string OPTION_NAME_DEFAULT = "QA_RT_Option_Automation";
        public const string OPTION_CODE_DEFAULT = "0001";

        public const string COMMUNITY_CODE_DEFAULT = "Automation";
        public const string COMMUNITY_NAME_DEFAULT = "QA_RT_Community_Automation";

        public const string HOUSE_NAME_DEFAULT = "QA_RT_House_Automation";
        public const string HOUSE_CODE_DEFAULT = "0001";

        private const string OPTION = "OPTION";
        private JobData jobData;
        JobQuantitiesData jobQuantities;

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

        private HouseQuantitiesData houseQuantities;
        [SetUp]
        public void Setup()
        {
            jobData = new JobData()
            {
                Name = "QA_RT_Job_Automation",
                Community = "Automation-QA_RT_Community_Automation",
                House = "0001-QA_RT_House_Automation",
                Lot = "_001 - Sold",
                Orientation = "Left",
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_49898\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_49898\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();

        }
        [Test]
        [Category("Section_IV")]
        public void C02_F_Jobs_DetailPages_AllJobs_BOMQuantities_The_manufacturer_value_displayed_wrong_with_csv_excel_mode_in_case_the_product_has_different_USE_value()
        {
            //I. Export CSV file + Normal view
            //1. Prepare a Product has 3 Styles
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I. Export CSV file + Normal view</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.1. Prepare a Product has 3 Styles</font>");
            // Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();

            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            //2. Create a new Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.2. Create a new Job</font>");
            //Navigate to Jobs menu > All Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Jobs menu > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", JOB_NAME_DEFAULT) is true)
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", JOB_NAME_DEFAULT);
            }
            JobPage.Instance.CreateJob(jobData);

            //Check Header in BreadsCrumb 
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(JOB_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The Header is present correctly.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Header is present incorrectly.</font>");
            }

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
            //3. Add these Quantities to the Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.3. Add these Quantities to the Job</font>");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");
            int totalItems = JobQuantitiesPage.Instance.GetTotalNumberJobQuantitiesItem();
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(jobQuantities);

            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(houseQuantities);
            // Get export file name
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get export file name.</font>");
            string exportFileName = CommonHelper.GetExportFileName($"Pipeline_Quantities_{JOB_NAME_DEFAULT}");
            //4. Click Export CSV/Excel Quantities button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.4. Click Export CSV/Excel Quantities button</font>");
            //5. Tick check all
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.5. Tick check all</font>");
            JobQuantitiesPage.Instance.DownloadBaselineJobQuantitiesFile(EXPORT_CSV_MORE_MENU, EXPORT_NORMAL_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}"+ $"_{EXPORT_NORMAL_VIEW_STYLE}");
            JobQuantitiesPage.Instance.ExportJobQuantitiesFile(EXPORT_CSV_MORE_MENU, EXPORT_NORMAL_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_NORMAL_VIEW_STYLE}", 0, ExportTitleFileConstant.JOB_QUANTITIES_NORMAL_TITLE);
            //6. Check exported file, especially Style and Manufacturer values
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.6. Check exported file, especially Style and Manufacturer values</font>");
            //II. Export CSV file + Flat view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>II. Export CSV file + Flat view</font>");
            //1. Click Export CSV/Excel Quantities button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>II.1. Click Export CSV/Excel Quantities button</font>");
            //2. Tick check all
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>II.2. Tick check all</font>");
            //3. Check exported file, especially Style and Manufacturer values
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>II.3. Check exported file, especially Style and Manufacturer values</font>");
            JobQuantitiesPage.Instance.DownloadBaselineJobQuantitiesFile(EXPORT_CSV_MORE_MENU, EXPORT_FLAT_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_FLAT_VIEW_STYLE}");
            JobQuantitiesPage.Instance.ExportJobQuantitiesFile(EXPORT_CSV_MORE_MENU, EXPORT_FLAT_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_FLAT_VIEW_STYLE}", 0, ExportTitleFileConstant.JOB_QUANTITIES_FLAT_TITLE);
            //III. Export Excel file + Normal view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III. Export Excel file + Normal view</font>");
            //1. Click Export CSV/Excel Quantities button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III.1. Click Export CSV/Excel Quantities button</font>");
            //2. Tick check all
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III.2. Tick check all</font>");
            //3. Check exported file, especially Style and Manufacturer values
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III.3. Check exported file, especially Style and Manufacturer values</font>");
            JobQuantitiesPage.Instance.DownloadBaselineJobQuantitiesFile(EXPORT_EXCEL_MORE_MENU, EXPORT_NORMAL_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_NORMAL_VIEW_STYLE}");
            JobQuantitiesPage.Instance.ExportJobQuantitiesFile(EXPORT_EXCEL_MORE_MENU, EXPORT_NORMAL_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_NORMAL_VIEW_STYLE}", 0, ExportTitleFileConstant.JOB_QUANTITIES_NORMAL_TITLE);
            //4 . Export Excel file + Flat view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III.4 . Export Excel file + Flat view</font>");
            //1. Click Export CSV/Excel Quantities button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III.4.1. Click Export CSV/Excel Quantities button</font>");
            //2. Tick check all
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III.4.2. Tick check all</font>");
            //3. Check exported file, especially Style and Manufacturer values
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III.4.3. Check exported file, especially Style and Manufacturer values</font>");
            JobQuantitiesPage.Instance.DownloadBaselineJobQuantitiesFile(EXPORT_EXCEL_MORE_MENU, EXPORT_FLAT_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_FLAT_VIEW_STYLE}");
            JobQuantitiesPage.Instance.ExportJobQuantitiesFile(EXPORT_EXCEL_MORE_MENU, EXPORT_FLAT_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_FLAT_VIEW_STYLE}", 0, ExportTitleFileConstant.JOB_QUANTITIES_FLAT_TITLE);

        }
    }
}
