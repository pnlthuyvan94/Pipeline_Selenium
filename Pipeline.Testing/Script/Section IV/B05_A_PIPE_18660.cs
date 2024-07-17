using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.CustomOptions;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionProduct;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Uses;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;
using Pipeline.Testing.Pages.Estimating.Uses.UseAssignment;
using Pipeline.Testing.Pages.Estimating.Uses.UseDetail;
using Pipeline.Testing.Pages.Estimating.Worksheet;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetProducts;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Script.TestData;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class B05_A_PIPE_18660 : BaseTestScript
    {

        private UsesData _useData;
        private UseAssignmentData UseAssignmentsData;
        private UseAssignmentData proDuctConversionData;
        private UseAssignmentData styleConversionData;

        private string USE_NAME = "QA_Auto_Use";
        private readonly string USE_NAME_DEFAULT = "NONE";

        private ProductData product;
        private ProductData getNewproduct;
        private ProductData subcomponentData;

        private static string PRODUCT_NAME = "QA_RT_New_Product_Automation_01";
        private readonly string BUILDINGPHASE_SUBCOMPONENT_DEFAULT = "Au01-QA_RT_New_Building_Phase_01_Automation";
        private readonly string PRODUCT_SUBCOMPONENT_NAME_DEFAULT = "QA_RT_New_Product_Automation_02";
        private readonly string STYLE_NAME_DEFAULT = "QA_RT_New_Style_Auto";

        private readonly int[] indexs = new int[] { };
        List<string> getListUseInJobQuantities;
        List<string> getListUseInWorksheet;
        private ProductData productData_Option_1;
        private ProductToOptionData productToOption1;
        private ProductData productData_House_1;
        private ProductToOptionData productToHouse1;
        private HouseQuantitiesData houseQuantities_SpecificCommunity;

        private const string HOUSE_NAME_DEFAULT = TestDataCommon.HOUSE_NAME_DEFAULT;
        private readonly string HOUSE_CODE_DEFAULT = TestDataCommon.HOUSE_CODE_DEFAULT;

        private const string ImportType = "Pre-Import Modification";
        private readonly string PRODUCT_NAME_DEFAULT = "QA_RT_New_Product_Automation_01";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";


        private const string COMMUNITY_CODE_DEFAULT = TestDataCommon.COMMUNITY_CODE_DEFAULT;
        private const string COMMUNITY_NAME_DEFAULT = TestDataCommon.COMMUNITY_NAME_DEFAULT;
        private readonly string BUILDINGPHASE_DEFAULT = "Au01-QA_RT_New_Building_Phase_01_Automation";

        private JobData activejobdata;
        private static string PHASE_VALUE = "Au01-QA_RT_New_Building_Phase_01_Automation";
        private const string JOB_NAME_DEFAULT = "QA_RT_Job_Automation";

        private const string JOB_CLOSE = "CLOSE JOB";
        private const string JOB_OPEN = "OPEN JOB";

        JobQuantitiesData jobQuantities;
        JobQuantitiesData jobQuantities_NoneUse;
        private JobData closedjobdata;

        private ProductData productData_Option;
        private ProductToOptionData productToOption;
        private ProductData productData_House;

        private ProductToOptionData productToHouse;
        private HouseQuantitiesData JobQuantities;
        private HouseQuantitiesData JobQuantities_NoneUse;
        private const string OPTION = "OPTION";

        private OptionQuantitiesData optionPhaseQuantitiesData;
        private OptionQuantitiesData optionPhaseQuantitiesData_Update;

        public const string OPTION_NAME_DEFAULT = TestDataCommon.OPTION_NAME_DEFAULT;
        public const string OPTION_CODE_DEFAULT = TestDataCommon.OPTION_CODE_DEFAULT;

        private CustomOptionData _customOption;
        private readonly string CUSTOM_OPTION_NAME_DEFAULT = "QA_RT_CustomOption_Automation";

        private WorksheetData WorksheetData;
        private WorksheetProductsData WorksheetProductsData;
        private readonly string WORKSHEET_NAME_DEFAULT = "QA_RT_Worksheet_Automation";

        private readonly string BUILDINGPHASE_DEFAULT_WORKSHEET = "Au01-QA_RT_New_Building_Phase_01_Automation";
        private readonly string PRODUCT_DEFAULT_WORKSHEET = "QA_RT_New_Product_Automation_01";

        SpecSetData SpecSetData;

        private readonly string SPECSET_NAME_DEFAULT = "QA_RT_SpecSetGroup_Automation";

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        [SetUp]
        public void SetUpTest()
        {
            _useData = new UsesData()
            {
                Name = USE_NAME,
                Description = "QA Automation test",
                SortOrder = "0"
            };

            UseAssignmentsData = new UseAssignmentData()
            {

                Assignments_Header = { "Subcomponents", "Houses", "Active Jobs", "Closed Jobs", "Options", "Custom Options", "Worksheets" },
                Conversion_Header = { "Spec Set Product Conversions", "Spec Set Style Conversions" },
                Product = "QA_RT_New_Product_Automation_02",
                Houses = HOUSE_NAME_DEFAULT,
                Subcomponent = PRODUCT_SUBCOMPONENT_NAME_DEFAULT,
                BuildingPhase = BUILDINGPHASE_DEFAULT,
                SpectSetName = SPECSET_NAME_DEFAULT,
                ActiveJobs = JOB_NAME_DEFAULT,
                ClosedJobs = JOB_NAME_DEFAULT,
                Options = OPTION_NAME_DEFAULT,
                CustomOptions = CUSTOM_OPTION_NAME_DEFAULT,
                Worksheets = WORKSHEET_NAME_DEFAULT,
            };


            product = new ProductData()
            {
                Name = "QA_RT_Product_PIPE_18660_Automation",
                Manufacture = "QA_RT_New_Manu_Auto",
                Style = "QA_RT_New_Style_Auto",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "1.1",
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation"
            };


            productData_Option_1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "DEFAULT",
                Use = "QA_Auto_Use",
                Quantities = "10.00",
                Unit = "NONE"
            };


            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = PHASE_VALUE,
                ProductList = new List<ProductData> { productData_Option_1 }
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1) { Quantities = "10.00" };


            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };

            // There is no House quantities 
            houseQuantities_SpecificCommunity = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1 }
            };


            activejobdata = new JobData()
            {
                Name = JOB_NAME_DEFAULT,
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT,                
                Lot = "_001 - Sold",
                Orientation = "Left",
            };

            closedjobdata = new JobData()
            {
                Name = JOB_NAME_DEFAULT,
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT,
                Lot = "_001 - Sold",
                Orientation = "Left",
            };

            jobQuantities = new JobQuantitiesData()
            {
                Option = OPTION_NAME_DEFAULT,
                BuildingPhase = { PHASE_VALUE },
                Source = "Pipeline",
                Products = { "QA_RT_New_Product_Automation_01"},
                Style = "QA_RT_New_Style_Auto",
                Use = USE_NAME,
                Quantity = { "10.00"}

            };
            jobQuantities_NoneUse = new JobQuantitiesData()
            {
                Option = OPTION_NAME_DEFAULT,
                BuildingPhase = { PHASE_VALUE },
                Source = "Pipeline",
                Products = { "QA_RT_New_Product_Automation_01" },
                Style = "QA_RT_New_Style_Auto",
                Use = USE_NAME,
                Quantity = { "10.00" }
            };
            optionPhaseQuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE_VALUE,
                ProductName = "QA_RT_New_Product_Automation_01",
                ProductDescription = "Product Description",
                Style = "QA_RT_New_Style_Auto",
                Condition = false,
                Use = USE_NAME,
                Quantity = "15.00",
                Source = "Pipeline"
            };
            optionPhaseQuantitiesData_Update = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE_VALUE,
                ProductName = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                ProductDescription = "Product Description",
                Condition = false,
                Use = USE_NAME_DEFAULT,
                Quantity = "20.00",
                Source = "Pipeline"
            };

            productData_Option = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = USE_NAME,
                Quantities = "10.00",
                Unit = "NONE",
            };

            productToOption = new ProductToOptionData()
            {
                BuildingPhase = PHASE_VALUE,
                ProductList = new List<ProductData> { productData_Option }
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House = new ProductData(productData_Option);

            productToHouse = new ProductToOptionData(productToOption) { ProductList = new List<ProductData> { productData_House } };


            JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse }
            };


            _customOption = new CustomOptionData()
            {
                Code = "QA_RT_CustomOption_Automation",
                Description = "Test for CustomOption BOM",
                Structural = bool.Parse("true"),
                Price = double.Parse("10.00")
            };

            WorksheetData = new WorksheetData()
            {
                Name = "QA_RT_Worksheet_Automation",
                Code = "789"
            };

            WorksheetProductsData = new WorksheetProductsData()
            {
                BuildingPhase = { "Au01-QA_RT_New_Building_Phase_01_Automation" },
                Products = { "QA_RT_New_Product_Automation_01"},
                Style = "QA_RT_New_Style_Auto",
                Use = USE_NAME,
                Quantity = { "10.00", "20.00", "30.00" }

            };

            SpecSetData = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_Automation",
                SpectSetName = "QA_RT_SpecSet_Automation",
                OriginalManufacture = "QA_RT_Manufacturer4_Automation",
                OriginalStyle = "QA_RT_Style4_Automation",
                OriginalUse = "QA_RT_Use4_Automation",
                NewManufacture = "QA_RT_Manufacturer3_Automation",
                NewStyle = "QA_RT_Style3_Automation",
                NewUse = USE_NAME,
                StyleCalculation="NONE",
                OriginalPhase = "123-QA_RT_BuildingPhase1_Automation",
                OriginalProduct = "QA_RT_Product1_Automation",
                OriginalProductStyle = "QA_RT_Style1_Automation",
                OriginalProductUse = "QA_RT_Use1_Automation",
                NewPhase = "124-QA_RT_BuildingPhase2_Automation",
                NewProduct = "QA_RT_Product2_Automation",
                NewProductStyle = "QA_RT_Style2_Automation",
                NewProductUse = USE_NAME,
                ProductCalculation="NONE"

            };
            proDuctConversionData = new UseAssignmentData()
            {
                SpectSetName = "QA_RT_SpecSet_Automation",
                OriginalPhase = "123-QA_RT_BuildingPhase1_Automation",
                OriginalProduct = "QA_RT_Product1_Automation",
                OriginalProductStyle = "QA_RT_Style1_Automation",
                OriginalProductUse = "QA_RT_Use1_Automation",
                NewPhase = "124-QA_RT_BuildingPhase2_Automation",
                NewProduct = "QA_RT_Product2_Automation",
                NewProductStyle = "QA_RT_Style2_Automation",
                NewProductUse = USE_NAME,
                
            };

            styleConversionData = new UseAssignmentData()
            {
                SpectSetName = "QA_RT_SpecSet_Automation",
                OriginalManufacture = "QA_RT_Manufacturer4_Automation",
                OriginalStyle = "QA_RT_Style4_Automation",
                OriginalUse = "QA_RT_Use4_Automation",
                NewManufacture = "QA_RT_Manufacturer3_Automation",
                NewStyle = "QA_RT_Style3_Automation",
                NewUse = USE_NAME,
            };

        }


        [Test]
        [Category("Section_IV")]
        public void B05_A_Estimating_Detail_Pages_Uses_Assignments()
        {

            //1. From ESTIMATING/USES, click the Use to which you would like to select
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1. From ESTIMATING/USES, click the Use to which you would like to select</b></font>");
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _useData.Name);
            if (UsesPage.Instance.IsItemInGrid("Name", _useData.Name) is false)
            {
                UsesPage.Instance.CreateNewUse(_useData);
            }

            UsesPage.Instance.SelectItemInGrid("Name", _useData.Name);

            //In Use Side Navigation, click the Assignments; verify open successfully the Assignments page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2. In Use Side Navigation, click the Assignments; verify open successfully the Assignments page</b></font>");
            UseDetailPage.Instance.LeftMenuNavigation("Assignments");
            string UseAssignmentUrl = UseAssignment.Instance.CurrentURL;
            
            UseAssignment.Instance.VerifyUsesDetailPage("Uses", _useData.Name);
            UseAssignment.Instance.VerifyAssignmentsPage(UseAssignmentsData.Assignments_Header, UseAssignmentsData.Conversion_Header);

            //3a. Assign Use to Subcomponents; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3a. Assign Use to Subcomponents; verify item displayed in the Assignment page</b></font>");
            // Navigate setting/product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate setting/product Turn OFF Show Show Category on Add Product SubcomponentModal.</font>");
            ProductSubcomponentPage.Instance.NavigateURL("Products/Settings/Default.aspx");
            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(false);
            
            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Products/Products/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", product.Name);
            }
            else
            {

                //Add button and Populate all values and save new product
                ProductPage.Instance.ClickAddToProductIcon();
                string expectedprouductURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
                Assert.That(ProductDetailPage.Instance.IsPageDisplayed(expectedprouductURL), "Product detail page isn't displayed");

                ExtentReportsHelper.LogInformation("Populate all values and save new product");
                // Select the 'Save' button on the modal;
                getNewproduct = ProductDetailPage.Instance.CreateAProduct(product);

                // Verify new Product in header
                Assert.That(ProductDetailPage.Instance.IsCreateSuccessfully(getNewproduct), "Create new Product unsuccessfully");
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
            }

            //Navigate To Subcomponents
            ExtentReportsHelper.LogInformation(null, "<b> Navigate To Subcomponents</b>");
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            string ProductSubcomponentUrl = ProductSubcomponentPage.Instance.CurrentURL;
            
            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //Add subcomponent with type is Basic 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add subcomponent with type is Basic</b></font color>");
            // Click add subcomponent
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(product.BuildingPhase)
                                            .SelectStyleOfProduct(product.Style)
                                            .SelectChildBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectChildStyleOfSubComponent(STYLE_NAME_DEFAULT)
                                            .SelectChildUseOfSubComponent(USE_NAME)
                                            .ClickSaveProductSubcomponent();

            //Verify add subcomponent
            string expectedMess = "Successfully added new subcomponent!";
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BUILDINGPHASE_SUBCOMPONENT_DEFAULT);

            ProductSubcomponentPage.Instance.VerifyUseDataInSubcomponent(_useData.Name);

            //Navigate To Use Assignment 
            CommonHelper.OpenURL(UseAssignmentUrl);
            if (UseAssignment.Instance.IsItemInSubcomponentGrid("Subcomponents", UseAssignmentsData.Subcomponent) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.Subcomponent} Item is displayed in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.Subcomponent} Item is not display in Grid</font>");
            }

            //3b. Assign Use to Houses; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3b. Assign Use to Houses; verify item displayed in the Assignment page</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }
            //Go to Assets/Houses/House detail/Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.1: Go to Assets/Houses/House detail/Import.font>");
            //Once navigated to House Details page click House BOM tab in the left nav panel.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Once navigated to House Details page click House BOM tab in the left nav panel.</b></font>");
            
            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            //Navigate To Import House Quantities
            //Processing the import with specific community
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Processing the import with specific community.</font>");

            //Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }
            //Processing the import with default community
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_18660.xml");

            //Go to House quantities check data
            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to House quantities check data.</font>");
            //Navigate To HouseQuantities
            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");
            string HouseQuantitiesDetailUrl = HouseQuantitiesDetailPage.Instance.CurrentURL;
            
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + "-" + houseQuantities_SpecificCommunity.communityName);
            foreach (ProductToOptionData housequantity in houseQuantities_SpecificCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_SpecificCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_SpecificCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }


            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", _useData.Name) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {_useData.Name} Item  is displayed In House Quantities Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {_useData.Name} Item In Quantities Grid is displayed In House Quantities Grid</b></font>");
            }

            //Navigate To Use Assignment 
            CommonHelper.OpenURL(UseAssignmentUrl);
            if (UseAssignment.Instance.IsItemInHouseGrid("Houses", UseAssignmentsData.Houses) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.Houses} Item is displayed In Use Assignment Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.Houses} Item is not display In Use Assignment Grid</font>");
            }

            //3c. Assign Use to Active Jobs; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3c. Assign Use to Active Jobs; verify item displayed in the Assignment page</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", activejobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", activejobdata.Name) is true)
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", activejobdata.Name);
            }
            //Select the 'Save' button on the modal;
            JobPage.Instance.CreateJob(activejobdata);

            //Check Header in BreadsCrumb 
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(activejobdata.Name) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Header is present correctly.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Header is present incorrectly.</font>");
            }

            //Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, $"Switch to Job/ Options page. Add Option '{OPTION_NAME_DEFAULT}' to job if it doesn't exist.");

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

            //Switch to Job/ Quantities page. Apply System Quantities.
            ExtentReportsHelper.LogInformation(null, $"witch to Job/ Quantities page. Apply System Quantities.");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");
            string JobQuantitiesUrl = JobQuantitiesPage.Instance.CurrentURL;
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            var getListUseInJobQuantities = JobQuantitiesPage.Instance.AddQuantitiesWithUseInGrid(jobQuantities);
            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();

            //Navigate To Use Assignment 
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, getListUseInJobQuantities[0]);
            if (UsesPage.Instance.IsItemInGrid("Name", getListUseInJobQuantities[0]) is true)
            {
                UsesPage.Instance.SelectItemInGrid("Name", getListUseInJobQuantities[0]);
            }
            UseDetailPage.Instance.LeftMenuNavigation("Assignments");

            if (UseAssignment.Instance.IsItemInActiveJobsGrid("Active Jobs", UseAssignmentsData.ActiveJobs) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.ActiveJobs} Item is displayed In Active Job Grid </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.ActiveJobs} Item is not displayIn Active Job Grid</font>");
            }

            //3d. Assign Use to Closed Jobs; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3d. Assign Use to Closed Jobs; verify item displayed in the Assignment page</b></font>");
            
            //Navigate to Job Quantities 
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", activejobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", activejobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", activejobdata.Name);
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch current job to status 'Close'.</b></font>");
                JobDetailPage.Instance.OpenOrCloseJob(JOB_OPEN);
            }


            //Navigate To Use Assignment 
            //CommonHelper.OpenURL(UseAssignmentUrl);
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, getListUseInJobQuantities[0]);
            if (UsesPage.Instance.IsItemInGrid("Name", getListUseInJobQuantities[0]) is true)
            {
                UsesPage.Instance.SelectItemInGrid("Name", getListUseInJobQuantities[0]);
            }
            UseDetailPage.Instance.LeftMenuNavigation("Assignments");

            if (UseAssignment.Instance.IsItemInCloseJobsGrid("Closed Jobs", UseAssignmentsData.ClosedJobs) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.ClosedJobs} Item  Grid is displayed In Closed Jobs </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.ClosedJobs} Item is not display In Closed Jobs</b></font>");
            }

            //3e. Assign Use to Options; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3e. Assign Use to Options; verify item displayed in the Assignment page</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);

            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME_DEFAULT) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", OPTION_NAME_DEFAULT);
            }

            //Navigate To Option Product
            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            string ProductsToOptionUrl = ProductsToOptionPage.Instance.CurrentURL;

            ExtentReportsHelper.LogInformation(null, $"Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE_VALUE}'.");

            if (!ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE_VALUE) is true)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionPhaseQuantitiesData);
            }

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Use", _useData.Name) is true)
            {
                ExtentReportsHelper.LogPass(null, $"The Use Data of Option Quantities is displayed with value <font color='green'><b>{_useData.Name}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"The The Use Data of Option Quantities is wrong displayed with value <font color='red'><b>{_useData.Name}</b></font>");
            }
 

            //Navigate To Use Assignment 
            CommonHelper.OpenURL(UseAssignmentUrl);
            if (UseAssignment.Instance.IsItemInOptionGrid("Options", UseAssignmentsData.Options) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.Options} Item is displayed In Options Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.Options} Item is not display In Options Grid</font>");
            }

            //3f. Assign Use to Custom Option; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3f. Assign Use to Custom Option; verify item displayed in the Assignment page</b></font>");
            CustomOptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.CustomOptions);
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, _customOption.Code);
            if (CustomOptionPage.Instance.IsItemInGrid("Code", _customOption.Code) is false)
            {
                // click on "+" Add button
                CustomOptionPage.Instance.GetItemOnHeader(DashboardContentItems.Add).Click();
                var _expectedCreateCOURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_CUSTOM_OPTION_URL;
                Assert.That(CustomOptionDetailPage.Instance.IsPageDisplayed(_expectedCreateCOURL), "Custom Option create page isn't displayed");

                // Create CO - Click 'Save' Button
                CustomOptionDetailPage.Instance.AddCustomOption(_customOption);
                string _expectedMessage = $"Could not create Custom Option with name {_customOption.Code}.";
                string actualMsg = CustomOptionDetailPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Custom Option with name { _customOption.Code}.");
                    CustomOptionDetailPage.Instance.CloseToastMessage();
                    Assert.Fail($"Could not create Custom Option with name { _customOption.Code}.");
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Actual message: {actualMsg}");
                }
            }
            else
            {
                CustomOptionPage.Instance.SelectItemInGrid("Code", _customOption.Code);
            }

            //Navigate Custom Option Detail to Custom Option Product

            CustomOptionDetailPage.Instance.LeftMenuNavigation("Products");

            string CustomOptionProductUrl = CustomOptionProduct.Instance.CurrentURL;

            if (CustomOptionProduct.Instance.IsItemCustomOptionQuantitiesGrid("Building Phase", BUILDINGPHASE_DEFAULT) is false)
            {
                // Create product for custom option 1
                CustomOptionProduct.Instance.Click_AddButton();
                CustomOptionProduct.Instance.AddCustomOptionProduct(BUILDINGPHASE_DEFAULT, USE_NAME, "10");
            }

            if (CustomOptionProduct.Instance.IsItemCustomOptionQuantitiesGrid("Use", _useData.Name) is true)
            {
                ExtentReportsHelper.LogPass(null, $"The Use Data of Custom Option Product is displayed with value <font color='green'><b>{_useData.Name}</b></font>");

            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"The The Use Data of Custom Option Product is wrong displayed with value <font color='red'><b>{_useData.Name}</b></font>");
            }
            //Navigate To Use Assignment 
            CommonHelper.OpenURL(UseAssignmentUrl);
            if (UseAssignment.Instance.IsItemInCustomOptionGrid("Custom Options", UseAssignmentsData.CustomOptions) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.CustomOptions} Item is displayed In Custom Options Grid</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.CustomOptions} Item is not display In Custom Options Grid </font>");
            }


            //3g. Assign Use to Worksheets; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3g. Assign Use to Worksheets; verify item displayed in the Assignment page.</b></font>");
            WorksheetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);

            // Verify the created item and delete if it's existing
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", WorksheetData.Name);
            if (WorksheetPage.Instance.IsItemInGrid("Name", WorksheetData.Name) is true)
            {
                WorksheetPage.Instance.DeleteWorkSheet(WorksheetData.Name);
            }
            //click on "+" Add button
            WorksheetPage.Instance.ClickAddWorksheetIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_WORKSHEET_URL;
            Assert.That(WorksheetDetailPage.Instance.IsPageDisplayed(expectedURL), "Worksheet detail page isn't displayed");

            // Populate all values
            WorksheetDetailPage.Instance.EnterWorksheetName(WorksheetData.Name)
                .EnterWorksheetCode(WorksheetData.Code);

            //Select the 'Save' button on the modal;
            WorksheetDetailPage.Instance.Save();
            CommonHelper.RefreshPage();
            //Verify new Worksheet in header
            Assert.That(WorksheetDetailPage.Instance.IsSaveWorksheetSuccessful(WorksheetData.Name), "Create new Worksheet unsuccessfully");
            ExtentReportsHelper.LogPass("Create successful Worksheet");

            //Navigate To WorksheetProducts Page
            WorksheetDetailPage.Instance.LeftMenuNavigation("Products");

            var getListUseInWorksheet =  WorksheetProductsPage.Instance.AddQuantitiesInGrid(WorksheetProductsData);
            CommonHelper.RefreshPage();
            if (WorksheetProductsPage.Instance.IsUseInGrid(getListUseInWorksheet[0]) is true)
            {
                ExtentReportsHelper.LogPass(null, $"Create Use With Name {getListUseInWorksheet[0]} Data In Worksheet Products is successfully in grid.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"Create Use With Name {getListUseInWorksheet[0]} Data In Worksheet Products is unsuccessfully in grid.</b></font>");
            }
            //Navigate To Use Assignment 
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, getListUseInWorksheet[0]);
            if (UsesPage.Instance.IsItemInGrid("Name", getListUseInWorksheet[0]) is true)
            {
                UsesPage.Instance.SelectItemInGrid("Name", getListUseInWorksheet[0]);
            }

            UseDetailPage.Instance.LeftMenuNavigation("Assignments");
            string UseInWorksheetProductUrl = UseAssignment.Instance.CurrentURL;

            if (UseAssignment.Instance.IsItemInWorkSheetGrid("Worksheets", UseAssignmentsData.Worksheets) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.Worksheets} Item is displayed In Worksheets Grid </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.Worksheets} Item is not display In Worksheets Grid </b></font>");
            }


            //3h. Assign Use to Spec set product conversions; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3h. Assign Use to Spec set product conversions; verify item displayed in the Assignment page</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            //Insert name to filter and click filter by Contain value and open Spec Set Group page
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData.GroupName);
            }


            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData.GroupName);
            string SpecSetDetailUrl = SpecSetDetailPage.Instance.CurrentURL;
            
            //Click add 
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();

            Assert.That(SpecSetDetailPage.Instance.IsModalDisplayed(), "The add new spect set modal is not displayed");

            //Create new spect set
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData.SpectSetName);

            // SpecSetDetailPage.Instance.CloseCreateSpecSetModal();

            //Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            // Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(SpecSetData);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(SpecSetData);
            SpecSetDetailPage.Instance.IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "NewProductUse", SpecSetData.NewProductUse);

            //3i. Assign Use to Spec set style conversions; verify item displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3i. Assign Use to Spec set style conversions; verify item displayed in the Assignment page.</b></font>");
            //Add New Conversation Style
            SpecSetDetailPage.Instance.AddStyleConversion(SpecSetData);
            SpecSetDetailPage.Instance.VerifyAddStyleConversionInGrid(SpecSetData);
            SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "NewUse", SpecSetData.NewUse);

            //Navigate To Use Assignment 
            CommonHelper.OpenURL(UseAssignmentUrl);

            //Verify Spec Set Product Conversions In Use
            if (UseAssignment.Instance.IsItemInProductConversionGrid("Spec Set", proDuctConversionData.SpectSetName) is true
                && UseAssignment.Instance.IsItemInProductConversionGrid("Original Building Phase", proDuctConversionData.OriginalPhase) is true
                && UseAssignment.Instance.IsItemInProductConversionGrid("Original Product", proDuctConversionData.OriginalProduct) is true
                && UseAssignment.Instance.IsItemInProductConversionGrid("Original Style", proDuctConversionData.OriginalProductStyle) is true
                && UseAssignment.Instance.IsItemInProductConversionGrid("Original Use", proDuctConversionData.OriginalProductUse) is true
                && UseAssignment.Instance.IsItemInProductConversionGrid("New Building Phase", proDuctConversionData.NewPhase) is true
                && UseAssignment.Instance.IsItemInProductConversionGrid("New Product", proDuctConversionData.NewProduct) is true
                && UseAssignment.Instance.IsItemInProductConversionGrid("New Style", proDuctConversionData.NewProductStyle) is true
                && UseAssignment.Instance.IsItemInProductConversionGrid("New Use", proDuctConversionData.NewProductUse) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The Spec Set Product Conversions <b>'{proDuctConversionData.SpectSetName}'</b> is correct.</font>");
            else
                ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected Original Building Phase: {proDuctConversionData.OriginalPhase}" +
                    $"<br>The expected Original Product: {proDuctConversionData.OriginalProduct}" +
                    $"<br>The expected Original Style: {proDuctConversionData.OriginalProductStyle}" +
                    $"<br>The expected Original Use: {proDuctConversionData.OriginalProductUse}" +
                    $"<br>The expected New Building Phase: {proDuctConversionData.NewPhase}"+
                    $"<br>The expected New Product: {proDuctConversionData.NewProduct}" +
                    $"<br>The expected New Style: {proDuctConversionData.NewProductStyle}" +
                    $"<br>The expected New Use: {proDuctConversionData.NewProductUse}</br></font>");

            //Verify Spec Set Style Conversions In Use

            if (UseAssignment.Instance.IsItemInStyleConversionGrid("Spec Set", styleConversionData.SpectSetName) is true
                && UseAssignment.Instance.IsItemInStyleConversionGrid("Original Style", styleConversionData.OriginalStyle) is true
                && UseAssignment.Instance.IsItemInStyleConversionGrid("Original Use", styleConversionData.OriginalUse) is true
                && UseAssignment.Instance.IsItemInStyleConversionGrid("New Style", styleConversionData.NewStyle) is true
                && UseAssignment.Instance.IsItemInStyleConversionGrid("New Use", styleConversionData.NewUse) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The Spec Set Product Conversions <b>'{styleConversionData.SpectSetName}'</b> is correct.</font>");
            else
                ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected Original Style: {styleConversionData.OriginalStyle}" +
                    $"<br>The expected Original Use: {styleConversionData.OriginalUse}" +
                    $"<br>The expected New Style: {styleConversionData.NewStyle}" +
                    $"<br>The expected New Use: {styleConversionData.NewUse}</br></font>");

            //4a. Unassign the Use to Subcomponents; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4a. Unassign the Use to Subcomponents; verify item not displayed in the Assignment page.</b></font>");
            CommonHelper.OpenURL(ProductSubcomponentUrl);

            ProductSubcomponentPage.Instance.ClickEditInGird(BUILDINGPHASE_SUBCOMPONENT_DEFAULT);
            ProductSubcomponentPage.Instance.UpdateChildUseOfSubComponent(USE_NAME_DEFAULT)
                                            .ClickSaveEditSubcomponent();
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BUILDINGPHASE_SUBCOMPONENT_DEFAULT);

            ProductSubcomponentPage.Instance.VerifyUseDataInSubcomponent(USE_NAME_DEFAULT);

            //Navigate To Use Assignment
            CommonHelper.OpenURL(UseAssignmentUrl);
            if (UseAssignment.Instance.IsItemInSubcomponentGrid("Subcomponents", UseAssignmentsData.Subcomponent) is false)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.Subcomponent} Item is not displayed In Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.Subcomponent} Item In Grid is still display in grid </b></font>");
            }

            
            //4b. Unassign the Use to Houses; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4b. Unassign the Use to Houses; verify item not displayed in the Assignment page.</b></font>");

            CommonHelper.OpenURL(HouseQuantitiesDetailUrl);
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + "-" + houseQuantities_SpecificCommunity.communityName);
            HouseQuantitiesDetailPage.Instance.ClickEditItemInQuantitiesGrid(BUILDINGPHASE_DEFAULT, PRODUCT_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.EditUseInQuantitiesGrid(USE_NAME_DEFAULT);
            if (HouseQuantitiesDetailPage.Instance.GetLastestToastMessage().Contains("The House Quantities were updated successfully") is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantities in {HOUSE_NAME_DEFAULT} were updated a success </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Quantities in {HOUSE_NAME_DEFAULT} were not updated a successlly </font>" +
                    $"<br>The actual result: {HouseQuantitiesDetailPage.Instance.GetLastestToastMessage()}</br></font>");
            }

            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", USE_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {USE_NAME_DEFAULT} Item is not displayed In House Quantities Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {USE_NAME_DEFAULT} Item In Grid is still display in House Quantities Grid </b></font>");
            }
            //Navigate To Use Assignment
            CommonHelper.OpenURL(UseAssignmentUrl);
            if (UseAssignment.Instance.IsItemInHouseGrid("Houses", UseAssignmentsData.Houses) is false)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.Houses} Item is not displayed In Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.Houses} Item is still display In Grid</b></font>");
            }

            //4c. Unassign the Use to Active Jobs; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4c. Unassign the Use to Active Jobs; verify item not displayed in the Assignment page.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", activejobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", activejobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", activejobdata.Name);
                ExtentReportsHelper.LogInformation(null, $"Switch current job to status 'Open'.");
                JobDetailPage.Instance.OpenOrCloseJob(JOB_OPEN);

            }

            CommonHelper.OpenURL(JobQuantitiesUrl);
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(jobQuantities_NoneUse);
            //Navigate To Use Assignment
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, getListUseInJobQuantities[0]);
            if (UsesPage.Instance.IsItemInGrid("Name", getListUseInJobQuantities[0]) is true)
            {
                UsesPage.Instance.SelectItemInGrid("Name", getListUseInJobQuantities[0]);
            }
            UseDetailPage.Instance.LeftMenuNavigation("Assignments");
            if (UseAssignment.Instance.IsItemInActiveJobsGrid("Active Jobs", UseAssignmentsData.ActiveJobs) is false)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.ActiveJobs} Item is not displayed In ActiveJob Grid </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.ActiveJobs} Item is displayed In ActiveJob Grid </b></font>");
            }

            //4d. Unassign the Use to Closed Jobs; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4d. Unassign the Use to Closed Jobs; verify item not displayed in the Assignment page.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", activejobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", activejobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", activejobdata.Name);
                ExtentReportsHelper.LogInformation(null, $"Switch current job to status 'Open'.");
                JobDetailPage.Instance.OpenOrCloseJob(JOB_CLOSE);
            }

            CommonHelper.OpenURL(JobQuantitiesUrl);
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(jobQuantities_NoneUse);

            //Navigate To Use Assignment
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, getListUseInJobQuantities[0]);
            if (UsesPage.Instance.IsItemInGrid("Name", getListUseInJobQuantities[0]) is true)
            {
                UsesPage.Instance.SelectItemInGrid("Name", getListUseInJobQuantities[0]);
            }
            UseDetailPage.Instance.LeftMenuNavigation("Assignments");

            if (UseAssignment.Instance.IsItemInCloseJobsGrid("Closed Jobs", UseAssignmentsData.ClosedJobs) is false)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.ClosedJobs} Item is not displayed In Closed Jobs Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.ClosedJobs} Item is displayed In Closed Jobs Grid</b></font>");
            }

            //4e. Unassign the Use to Options; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4e. Unassign the Use to Options; verify item not displayed in the Assignment page.</b></font>");
            CommonHelper.OpenURL(ProductsToOptionUrl);

            //Edit Use 
            ProductsToOptionPage.Instance.EditItemInGrid("Product", optionPhaseQuantitiesData.ProductName);
            ProductsToOptionPage.Instance.UpdateItemInGrid(optionPhaseQuantitiesData_Update);

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Use", USE_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"The Use Data of Option Quantities is displayed with value <font color='green'><b>{USE_NAME_DEFAULT}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"The The Use Data of Option Quantities is NOT displayed with value <font color='red'><b>{USE_NAME_DEFAULT}</b></font>");
            }

            //Navigate To Use Assignment
            CommonHelper.OpenURL(UseAssignmentUrl);
            if (UseAssignment.Instance.IsItemInOptionGrid("Options", UseAssignmentsData.Options) is false)
            {
                ExtentReportsHelper.LogPass(null, $"The {UseAssignmentsData.Options} Item In Options Grid is NOT displayed with Title <font color='green'><b>{UseAssignmentsData.Options}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"The {UseAssignmentsData.Options} Item In Options Grid is displayed with Title <font color='red'><b>{UseAssignmentsData.Options}</b></font>");
            }

            //4f. Unassign the Use to Custom Option; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4f. Unassign the Use to Custom Option; verify item not displayed in the Assignment page.</b></font>");

            CommonHelper.OpenURL(CustomOptionProductUrl);
            CustomOptionProduct.Instance.EditItemOnProductGird("Product", optionPhaseQuantitiesData.ProductName);
            CustomOptionProduct.Instance.UpdateCustomOptionQuantities("QA_RT_New_Style_Auto", USE_NAME_DEFAULT, "10","Pipeline");

            if (CustomOptionProduct.Instance.IsItemCustomOptionQuantitiesGrid("Use", USE_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"The Use Data of Option Quantities is displayed with value <font color='green'><b>{USE_NAME_DEFAULT}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"The The Use Data of Option Quantities is not displayed with value <font color='red'><b>{USE_NAME_DEFAULT}</b></font>");
            }

            //Navigate To Use Assignment
            CommonHelper.OpenURL(UseAssignmentUrl);
            if (UseAssignment.Instance.IsItemInCustomOptionGrid("Custom Options", UseAssignmentsData.CustomOptions) is false)
            {
                ExtentReportsHelper.LogPass(null, $"The {UseAssignmentsData.CustomOptions} Item is not displayed In CustomO ptions Grid </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"The {UseAssignmentsData.CustomOptions} Item is displayed In Custom Options Grid</b></font>");
            }
 

            //4g. Unassign the Use to Worksheets; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4g. Unassign the Use to Worksheets; verify item not displayed in the Assignment page</b></font>");

            WorksheetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", WorksheetData.Name);
            if (WorksheetPage.Instance.IsItemInGrid("Name", WorksheetData.Name) is true)
            {
                WorksheetPage.Instance.SelectItemInGrid("Name", WorksheetData.Name);

                //Navigate To WorksheetProducts Page
                WorksheetDetailPage.Instance.LeftMenuNavigation("Products");

                WorksheetProductsPage.Instance.EditItemInGrid(PRODUCT_DEFAULT_WORKSHEET);

                WorksheetProductsPage.Instance.UpdateItemInGrid(USE_NAME_DEFAULT, "10.000");

                CommonHelper.RefreshPage();
                if (WorksheetProductsPage.Instance.IsUseInGrid(USE_NAME_DEFAULT) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"Update Use With Name {USE_NAME_DEFAULT} Data In Worksheet Products is successfully in grid.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"Update Use With Name {USE_NAME_DEFAULT} Data In Worksheet Products is unsuccessfully in grid.</b></font>");
                }
            }

            //Navigate To Use Assignment
            CommonHelper.OpenURL(UseInWorksheetProductUrl);

            if (UseAssignment.Instance.IsItemInWorkSheetGrid("Worksheets", UseAssignmentsData.Worksheets) is false)
            {
                ExtentReportsHelper.LogPass(null, $"The {UseAssignmentsData.Worksheets} Item is not displayed In WorkSheets Grid </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"The {UseAssignmentsData.Worksheets} Item is displayed In WorkSheets Grid</b></font>");
            }

            //4h. Unassign the Use to Spec set product conversions; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4h. Unassign the Use to Spec set product conversions; verify item not displayed in the Assignment page</b></font>");

            CommonHelper.OpenURL(SpecSetDetailUrl);
            //Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            SpecSetDetailPage.Instance.EditItemProductConversionsInGrid(SpecSetData.NewProduct);
            SpecSetDetailPage.Instance.Update_SelectNewProductUse(USE_NAME_DEFAULT);
            SpecSetDetailPage.Instance.UpdateProduct();
            SpecSetDetailPage.Instance.IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "NewProductUse", USE_NAME_DEFAULT);

            //4i. Unassign the Use to Spec set style conversions; verify item not displayed in the Assignment page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4i. Unassign the Use to Spec set style conversions; verify item not displayed in the Assignment page</b></font>");

            //Expand all
            SpecSetDetailPage.Instance.EditItemOnStyleConversionsInGrid(SpecSetData.OriginalStyle);
            SpecSetDetailPage.Instance.Update_SelectNewUse(USE_NAME_DEFAULT);
            SpecSetDetailPage.Instance.UpdateStyle();
            SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "NewUse", USE_NAME_DEFAULT);

            //Navigate To Use Assignment
            CommonHelper.OpenURL(UseAssignmentUrl);

            if (UseAssignment.Instance.IsItemInProductConversionGrid("New Use", proDuctConversionData.NewProductUse) is false)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {proDuctConversionData.NewProductUse} Item In Spec Set Product Conversions Grid is not display in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {proDuctConversionData.NewProductUse} Item In Spec Set Product Conversions Grid is still display in grid</b></font>");
            }

            if (UseAssignment.Instance.IsItemInStyleConversionGrid("New Use", styleConversionData.NewUse) is false)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {styleConversionData.NewUse} Item In Spec Set Style Conversions Grid is not display in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {styleConversionData.NewUse} Item In Spec Set Style Conversions Grid is still displayed in grid</b></font>");
            }

            //5. Verify the filter of Houses in Assignments page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5. Verify the filter of Houses in Assignments page</b></font>");

            //Navigate To Use Assignment 
            CommonHelper.OpenURL(HouseQuantitiesDetailUrl);
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + "-" + houseQuantities_SpecificCommunity.communityName);
            HouseQuantitiesDetailPage.Instance.ClickEditItemInQuantitiesGrid(BUILDINGPHASE_DEFAULT, PRODUCT_NAME_DEFAULT);
            HouseQuantitiesDetailPage.Instance.EditUseInQuantitiesGrid(USE_NAME);
            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", USE_NAME) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {USE_NAME} Item is not displayed In Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {USE_NAME} Item In Grid is still display in grid </b></font>");
            }
            CommonHelper.OpenURL(UseAssignmentUrl);

            UseAssignment.Instance.FilterItemInHouseGrid("Houses", GridFilterOperator.Contains, UseAssignmentsData.Houses);
            if (UseAssignment.Instance.IsItemInHouseGrid("Houses", UseAssignmentsData.Houses) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The {UseAssignmentsData.Houses} Item is displayed In Use Assignment Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The {UseAssignmentsData.Houses} Item is not display In Use Assignment Grid</b></font>");
            }

        }
        [TearDown]
        public void DeleteData()
        {

            ExtentReportsHelper.LogInformation(null, "Delete Use Data");
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _useData.Name);
            if (UsesPage.Instance.IsItemInGrid("Name", _useData.Name) is true)
            {
                UsesPage.Instance.DeleteItemInGrid("Name", _useData.Name);
            }
            //Refresh Page
            CommonHelper.RefreshPage();
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            EstimatingPage.Instance.Check_Show_Subcomponent_Description(false);

            //Delete SubComponent 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete SubComponent Name {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} .</font><b>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, product.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", product.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", product.Name);
                //Navigate To Subcomponents
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                ProductSubcomponentPage.Instance.ClickDeleteInGird(BUILDINGPHASE_SUBCOMPONENT_DEFAULT);
                string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
                if (act_mess == "Successfully deleted subcomponent")
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} subcomponent </b></font color>");
                }
                else
                    ExtentReportsHelper.LogFail($"<b> Cannot delete {BUILDINGPHASE_SUBCOMPONENT_DEFAULT} subcomponent </b>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }

            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "Delete File House Quantities");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + "-" + houseQuantities_SpecificCommunity.communityName);

            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            //Delete Product In Option Quantities
            ExtentReportsHelper.LogInformation(null, "Delete Product In Option Quantities.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);

            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME_DEFAULT) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", OPTION_NAME_DEFAULT);

                OptionDetailPage.Instance.LeftMenuNavigation("Products");

                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", optionPhaseQuantitiesData.BuildingPhase) is true)
                {
                    ProductsToOptionPage.Instance.DeleteItemInGrid("Building Phase", optionPhaseQuantitiesData.BuildingPhase);
                    ProductsToOptionPage.Instance.WaitOptionQuantitiesLoadingIcon();
                }
            }

            //Delete CustomOption Product
            ExtentReportsHelper.LogInformation(null, "Delete CustomOption Product");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL);
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, _customOption.Code);

            if (CustomOptionPage.Instance.IsItemInGrid("Code", _customOption.Code) is true)
            {
                CustomOptionPage.Instance.SelectItemInGrid("Code", _customOption.Code);
                CustomOptionDetailPage.Instance.LeftMenuNavigation("Products");
                CustomOptionProduct.Instance.DeleteItemOnProductGird("Building Phase", BUILDINGPHASE_DEFAULT_WORKSHEET);
                if (CustomOptionProduct.Instance.IsItemGird("Building Phase", BUILDINGPHASE_DEFAULT_WORKSHEET) is true)
                    ExtentReportsHelper.LogFail("Can't delete product on the grid view");
                else
                    ExtentReportsHelper.LogPass(null, "Deleted product successfully");
            }

        }


        private void VerifyToastMessage(string expectedMess, string columnToVerify, string value)
        {
            string act_Message = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            if (act_Message == expectedMess)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Successfully added new subcomponent</b></font color>");
            }
            else
            {
                // Can't get toast message then verify the item on the grid view
                if (ProductSubcomponentPage.Instance.VerifyItemInGrid(columnToVerify, value) is false)
                {
                    ExtentReportsHelper.LogFail("Failed add new subcomponent");
                    ProductSubcomponentPage.Instance.CloseToastMessage();
                }
            }
        }

    }

}


