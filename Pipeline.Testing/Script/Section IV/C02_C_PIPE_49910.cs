using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class C02_C_PIPE_49910 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string EXPORT_XML_MORE_MENU = "XML";
        private const string EXPORT_CSV_MORE_MENU = "CSV";

        private const string EXPORT_EXCEL_MORE_MENU = "Excel";
        private const string EXPORT_NORMAL_VIEW_STYLE = "Normal";
        private const string EXPORT_FLAT_VIEW_STYLE = "Flat";
        private const string JOB_NAME_DEFAULT = "QA_RT_Job_Automation";

        private const string OPTION_NAME_DEFAULT = "QA_RT_Option_Automation";
        private const string OPTION_CODE_DEFAULT = "0001";

        private const string SIGN = "-";

        private const string COMMUNITY_CODE_DEFAULT = "Automation";
        private const string COMMUNITY_NAME_DEFAULT = "QA_RT_Community_Automation";

        private const string HOUSE_NAME_DEFAULT = "QA_RT_House_Automation";
        private const string HOUSE_CODE_DEFAULT = "0001";

        private const string OPTION = "OPTION";

        private JobData jobData;
        JobQuantitiesData jobQuantities;

        private ProductData _productDataOption1;
        private ProductData _productDataOption2;
        private ProductData _productDataOption3;
        private ProductData _productDataOption4;

        private ProductToOptionData _productToOption1;
        private ProductToOptionData _productToOption2;
        private ProductToOptionData _productToOption3;
        private ProductToOptionData _productToOption4;

        private ProductData _productDataHouse1;
        private ProductData _productDataHouse2;
        private ProductData _productDataHouse3;
        private ProductData _productDataHouse4;

        private ProductToOptionData _productToHouse1;
        private ProductToOptionData _productToHouse2;
        private ProductToOptionData _productToHouse3;
        private ProductToOptionData _productToHouse4;

        private HouseQuantitiesData houseQuantities;

        [SetUp]
        public void SetupTestData()
        {
            jobData = new JobData()
            {
                Name = "QA_RT_Job_Automation",
                Community = "Automation-QA_RT_Community_Automation",
                House = "0001-QA_RT_House_Automation",
                Lot = "_001 - Sold",
                Orientation = "Left"
            };

            jobQuantities = new JobQuantitiesData()
            {
                Option = OPTION_NAME_DEFAULT,
                BuildingPhase = { "Au_1-RT_QA_New_Building_Phase_Auto_01" },
                Source = "Pipeline",
                Products = { "RT_QA_New_Product_Auto_01", "RT_QA_New_Product_Auto_02", "RT_QA_New_Product_Auto_03", "RT_QA_New_Product_Auto_04" },
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantity = { "10.00", "20.00", "30.00", "40.00" }
            };

            _productDataOption1 = new ProductData()
            {
                Name = "RT_QA_New_Product_Auto_01",
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantities = "10.00",
                Unit = "NONE"
            };

            _productDataOption2 = new ProductData()
            {
                Name = "RT_QA_New_Product_Auto_02",
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantities = "20.00",
                Unit = "NONE"
            };
            _productDataOption3 = new ProductData()
            {
                Name = "RT_QA_New_Product_Auto_03",
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE"
            };
            _productDataOption4 = new ProductData()
            {
                Name = "RT_QA_New_Product_Auto_04",
                Style = "RT_QA_New_Style_Auto",
                Use = "NONE",
                Quantities = "40.00",
                Unit = "NONE"
            };

            _productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "Au_1-RT_QA_New_Building_Phase_Auto_01",
                ProductList = new List<ProductData> { _productDataOption1 }
            };

            _productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "Au_1-RT_QA_New_Building_Phase_Auto_01",
                ProductList = new List<ProductData> { _productDataOption2 }
            };

            _productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "Au_1-RT_QA_New_Building_Phase_Auto_01",
                ProductList = new List<ProductData> { _productDataOption3 }
            };

            _productToOption4 = new ProductToOptionData()
            {
                BuildingPhase = "Au_1-RT_QA_New_Building_Phase_Auto_01",
                ProductList = new List<ProductData> { _productDataOption3 }
            };

            _productDataHouse1 = new ProductData(_productDataOption1);

            _productDataHouse2 = new ProductData(_productDataOption2);

            _productDataHouse3 = new ProductData(_productDataOption3);

            _productDataHouse4 = new ProductData(_productDataOption4);

            _productToHouse1 = new ProductToOptionData(_productToOption1) { ProductList = new List<ProductData> { _productDataHouse1 } };
            _productToHouse2 = new ProductToOptionData(_productToOption2) { ProductList = new List<ProductData> { _productDataHouse2 } };
            _productToHouse3 = new ProductToOptionData(_productToOption3) { ProductList = new List<ProductData> { _productDataHouse3 } };
            _productToHouse4 = new ProductToOptionData(_productToOption4) { ProductList = new List<ProductData> { _productDataHouse4 } };

            houseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { _productToHouse1, _productToHouse2, _productToHouse3, _productToHouse4 }
            };
        }

        [Test]
        public void C02_C_Jobs_Details_Pages_All_Jobs_BOM_Quantities_The_Style_Manufacture_tag_column_do_not_show_on_the_Job_quantities_export_for_XML_EXCEL_CSV_file()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.1 Go to 1 Job Details > Quantities</font>");
            // Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string transferSeparationCharacter = ','.ToString();

            MainSettingPage.Instance.SetTransferSeparationCharactertatus(transferSeparationCharacter);

            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", JOB_NAME_DEFAULT))
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", JOB_NAME_DEFAULT);
            }

            JobPage.Instance.CreateJob(jobData);

            //Check Header in BreadsCrumb 
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(JOB_NAME_DEFAULT))
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The Header is displayed correctly in Breadcrumbs.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Header is displayed incorrectly in Breadcrumbs.</font>");
            }

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: " +
                    $"<b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = OPTION_CODE_DEFAULT + SIGN + OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                JobOptionPage.Instance.ClickApproveConfig();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.2. Click Export XML Quantities</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.3. Select Source and click Export</font>");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(jobQuantities);
            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyItemWithStyleOnJobQuantitiesGrid(houseQuantities);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.4. Check the exported XML file," +
                " to make sure the Manufacturer/Style is included</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.5. Go to Expanded Mode of Job Quantities" +
                " to compare the results</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.6. Check other quantities" +
                " of specific Manufacturer/Style</font>");
            JobQuantitiesPage.Instance.DownloadBaselineJobQuantitiesFile(EXPORT_XML_MORE_MENU, string.Empty, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}_Pipeline");
            JobQuantitiesPage.Instance.ExportJobQuantitiesFile(EXPORT_XML_MORE_MENU, string.Empty, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}_Pipeline", 0, string.Empty);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>II.2 Export CSV/Excel Quantities</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>II.2.1 Export CSV file type with Normal view type</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>II.2.2 Check the result</font>");
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get export file name.</font>");
            JobQuantitiesPage.Instance.DownloadBaselineJobQuantitiesFile(EXPORT_CSV_MORE_MENU, EXPORT_NORMAL_VIEW_STYLE,
                $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_NORMAL_VIEW_STYLE}");
            JobQuantitiesPage.Instance.ExportJobQuantitiesFile(EXPORT_CSV_MORE_MENU, EXPORT_NORMAL_VIEW_STYLE,
                $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_NORMAL_VIEW_STYLE}", 0, ExportTitleFileConstant.JOB_QUANTITIES_NORMAL_TITLE);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>II.2.3 Export EXCEL file type with Flat view type</font>");
            JobQuantitiesPage.Instance.DownloadBaselineJobQuantitiesFile(EXPORT_EXCEL_MORE_MENU, EXPORT_FLAT_VIEW_STYLE,
                $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_FLAT_VIEW_STYLE}");
            JobQuantitiesPage.Instance.ExportJobQuantitiesFile(EXPORT_EXCEL_MORE_MENU,
                EXPORT_FLAT_VIEW_STYLE, $"Pipeline_Quantities_{JOB_NAME_DEFAULT}" + $"_{EXPORT_FLAT_VIEW_STYLE}", 0, ExportTitleFileConstant.JOB_QUANTITIES_FLAT_TITLE);
        }
    }
}
