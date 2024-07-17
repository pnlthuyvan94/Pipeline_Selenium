
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.CommunityHouseBOM;
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Estimating.BOMLogicRules;
using Pipeline.Testing.Pages.Estimating.BOMLogicRules.BOMLogicRuleDetail;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Script.TestData;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_XIII
{
    class A01_A_PIPE_49920 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_XIII);
        }
        private JobData jobData;
        BuildingPhaseData ParentBuildingPhaseData;
        BuildingPhaseData ChildBuildingPhaseData;
        SpecSetData SpecSetData;

        CommunityQuantitiesData communityQuantitiesData;
        private BOMLogicRuleData BOMLogicRuleData;
        private BOMLogicRuleDetailData BOMLogicRuleDetailData;

        private const string FILTERED_TO_ALL = "ALL";
        readonly string HOUSES_NAME = "Houses";
        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_01";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";

        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House04_Automation";
        private readonly string HOUSE_CODE_DEFAULT = "400";

        private const string OPTION = "OPTION";
        private static string OPTION_NAME_DEFAULT = "Option_QA_RT";
        private static string OPTION_CODE_DEFAULT = "ORT";

        private static string OPTION1_NAME_DEFAULT = "Option_QA_RT1";
        private static string OPTION1_CODE_DEFAULT = "ORT1";

        private static string OPTION2_NAME_DEFAULT = "Option_QA_RT2";
        private static string OPTION2_CODE_DEFAULT = "ORT2";

        List<string> Options = new List<string>() { OPTION_NAME_DEFAULT, OPTION1_NAME_DEFAULT, OPTION2_NAME_DEFAULT };

        public const string JOB_NAME_DEFAULT = "QA_RT_Job_49920_Automation";

        private const string ImportType = "Pre-Import Modification";
        private const string JOB_BOM_VIEW_MODE = "Option/Phase/Product";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";

        private const string PRODUCT1_NAME_DEFAULT = "QA_RT_Product01";
        private const string PRODUCT2_NAME_DEFAULT = "QA_RT_Product02";
        private const string PRODUCT3_NAME_DEFAULT = "QA_RT_Product03";
        private const string PRODUCT4_NAME_DEFAULT = "QA_RT_Product04";
        private const string PRODUCT5_NAME_DEFAULT = "QA_RT_Product05";
        private const string PRODUCT6_NAME_DEFAULT = "QA_RT_Product06";
        private const string PRODUCT7_NAME_DEFAULT = "QA_RT_Product07";
        private const string PRODUCT8_NAME_DEFAULT = "QA_RT_Product08";
        private const string PRODUCT9_NAME_DEFAULT = "QA_RT_Product09";
        private const string PRODUCT10_NAME_DEFAULT = "QA_RT_Product10";
        private const string PRODUCT11_NAME_DEFAULT = "QA_RT_Product11";
        private const string PRODUCT12_NAME_DEFAULT = "QA_RT_Product12";

        private const string StyleOfProduct = "QA_RT_New_Style_Auto";

        private const string PARENT_BUILDINGPHASE_NAME_DEFAULT = "QA_RT_Parent_BuildingPhase_Automation";
        private const string BuildingPhaseOfProduct = "9921-QA_RT_Child_BuildingPhase_Automation";
        private const string BuildingPhaseOfSubcomponent = "9920-QA_RT_Parent_BuildingPhase_Automation";
        
        string ChildBuidingPhase_url;
        List<string> listProductofParentBuildingPhase = new List<string>() { PRODUCT1_NAME_DEFAULT, PRODUCT2_NAME_DEFAULT, PRODUCT3_NAME_DEFAULT, PRODUCT4_NAME_DEFAULT, PRODUCT5_NAME_DEFAULT, PRODUCT6_NAME_DEFAULT };
        List<string> listProductofChildBuildingPhase = new List<string>() { PRODUCT7_NAME_DEFAULT, PRODUCT8_NAME_DEFAULT, PRODUCT9_NAME_DEFAULT, PRODUCT10_NAME_DEFAULT, PRODUCT11_NAME_DEFAULT, PRODUCT12_NAME_DEFAULT };       
        
        private ProductData productData_Option_1;
        private ProductData productData_Option_2;
        private ProductData productData_Option_3;
        private ProductData productData_Option_4;
        private ProductData productData_Option_5;
        private ProductData productData_Option_6;
        private ProductData productData_Option_7;

        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToOption3;
        private ProductToOptionData productToOption4;
        private ProductToOptionData productToOption5;
        private ProductToOptionData productToOption6;
        private ProductToOptionData productToOption7;

        private ProductData productData_House_1;
        private ProductData productData_House_2;
        private ProductData productData_House_3;
        private ProductData productData_House_4;
        private ProductData productData_House_5;
        private ProductData productData_House_6;
        private ProductData productData_House_7;

        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToHouse3;
        private ProductToOptionData productToHouse4;
        private ProductToOptionData productToHouse5;
        private ProductToOptionData productToHouse6;
        private ProductToOptionData productToHouse7;

        private ProductToOptionData productToHouseBOM1;
        private ProductToOptionData productToHouseBOM2;
        private ProductToOptionData productToHouseBOM3;
        private ProductToOptionData productToHouseBOM4;
        private ProductToOptionData productToHouseBOM5;
        private ProductToOptionData productToHouseBOM6;
        private ProductToOptionData productToHouseBOM7;

        private HouseQuantitiesData houseQuantities;
        private HouseQuantitiesData houseQuantities_HouseBOM1;
        private HouseQuantitiesData houseQuantities_HouseBOM2;
        private HouseQuantitiesData houseQuantities_HouseBOM3;
        private HouseQuantitiesData houseQuantities_HouseBOM4;

        //JobBOM

        private HouseQuantitiesData JobQuantities1;
        private HouseQuantitiesData JobQuantities2;

        private ProductToOptionData productToJobBOM1;
        private ProductToOptionData productToJobBOM2;
        private ProductToOptionData productToJobBOM3;
        private ProductToOptionData productToJobBOM4;
        private ProductToOptionData productToJobBOM5;
        private ProductToOptionData productToJobBOM6;

        private HouseQuantitiesData houseQuantities_JobBOM1;
        private HouseQuantitiesData houseQuantities_JobBOM2;
        private HouseQuantitiesData houseQuantities_JobBOM3;


        [SetUp]
        public void GetData()
        {

            ParentBuildingPhaseData = new BuildingPhaseData()
            {
                Code = "9920",
                Name = "QA_RT_Parent_BuildingPhase_Automation",
                BuildingGroupName = "QA_RT_New_Building_Group_Auto_01",
                BuildingGroupCode = "12111111",
                Taxable = false,
            };

            ChildBuildingPhaseData = new BuildingPhaseData()
            {
                Code = "9921",
                Name = "QA_RT_Child_BuildingPhase_Automation",
                BuildingGroupName = "QA_RT_New_Building_Group_Auto_01",
                BuildingGroupCode = "12111111",
                Taxable = false,
            };

            SpecSetData = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_PIPE_44920_Automation",
                SpectSetName = "QA_RT_SpecSet_PIPE_44920_Automation",
                OriginalPhase = "9921-QA_RT_Child_BuildingPhase_Automation",
                OriginalProduct = "QA_RT_Product09",
                OriginalProductStyle = "QA_RT_New_Style_Auto",
                OriginalProductUse = "ALL",
                NewPhase = "9920-QA_RT_Parent_BuildingPhase_Automation",
                NewProduct = "QA_RT_Product04",
                NewProductStyle = "QA_RT_New_Style_Auto",
                NewProductUse = "NONE",
                ProductCalculation = "NONE"

            };

            communityQuantitiesData = new CommunityQuantitiesData()
            {
                OptionName = OPTION1_NAME_DEFAULT,
                BuildingPhase = "9920-QA_RT_Parent_BuildingPhase_Automation",
                ProductName = PRODUCT4_NAME_DEFAULT,
                Style = "QA_RT_New_Style_Auto",
                Condition = true,
                Use = "NONE",
                Quantity = "4.00",
                Source = "Pipeline"
            };

            BOMLogicRuleData = new BOMLogicRuleData()
            {
                RuleName = "QA_RT_BOM_Logic_Rule_Automation",
                RuleDescription = "QA_RT_BOM_Logic_Rule_Automation",
                SortOrder = "0",
                Execution = "Pre Product Assembly"
            };

            BOMLogicRuleDetailData = new BOMLogicRuleDetailData()
            {
                ConditionKey = "Building Phase",
                ConditionKeyAttribute = "Building Phase Name",
                Operator = "EQUAL",
                ConditionValue = new List<string>() { "QA_RT_Parent_BuildingPhase_Automation" },
                ActionKey= "Add Product"
            };

            jobData = new JobData()
            {
                Name = "QA_RT_Job_49920_Automation",
                Community = "Automation_01-QA_RT_Community01_Automation",
                House = "400-QA_RT_House04_Automation",
                Lot = "_111 - Sold",
                Orientation = "Left",
            };

            productData_Option_1 = new ProductData()
            {
                Name= "QA_RT_Product01",
                Style= "QA_RT_New_Style_Auto",
                Use="NONE",
                Quantities="1.00"
            };

            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_Product07",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "7.00"
            };

            productData_Option_3 = new ProductData()
            {
                Name = "QA_RT_Product08",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "8.00"
            };

            productData_Option_4 = new ProductData()
            {
                Name = "QA_RT_Product09",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "9.00"
            };

            productData_Option_5 = new ProductData()
            {
                Name = "QA_RT_Product04",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "4.00"
            };

            productData_Option_6 = new ProductData()
            {
                Name = "QA_RT_Product05",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "1.00"
            };

            productData_Option_7 = new ProductData()
            {
                Name = "QA_RT_Product05",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "4.00"
            };


            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "9920-QA_RT_Parent_BuildingPhase_Automation",
                ProductList = new List<ProductData> { productData_Option_1 },
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "9921-QA_RT_Child_BuildingPhase_Automation",
                ProductList = new List<ProductData> { productData_Option_2 },
            };

            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "9921-QA_RT_Child_BuildingPhase_Automation",
                ProductList = new List<ProductData> { productData_Option_3 },
            };

            productToOption4 = new ProductToOptionData()
            {
                BuildingPhase = "9921-QA_RT_Child_BuildingPhase_Automation",
                ProductList = new List<ProductData> { productData_Option_4 },
            };

            productToOption5 = new ProductToOptionData()
            {
                BuildingPhase = "9920-QA_RT_Parent_BuildingPhase_Automation",
                ProductList = new List<ProductData> { productData_Option_5 },
            };

            productToOption6 = new ProductToOptionData()
            {
                BuildingPhase = "9920-QA_RT_Parent_BuildingPhase_Automation",
                ProductList = new List<ProductData> { productData_Option_6 },
            };

            productToOption7 = new ProductToOptionData()
            {
                BuildingPhase = "9920-QA_RT_Parent_BuildingPhase_Automation",
                ProductList = new List<ProductData> { productData_Option_7 },
            };

            /****************************** Create Product quantities on House ******************************/

            productData_House_1 = new ProductData(productData_Option_1) ;
            productData_House_2 = new ProductData(productData_Option_2) ;
            productData_House_3 = new ProductData(productData_Option_3) ;
            productData_House_4 = new ProductData(productData_Option_4);
            productData_House_5 = new ProductData(productData_Option_5);
            productData_House_6 = new ProductData(productData_Option_6);
            productData_House_7 = new ProductData(productData_Option_7);

            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };
            productToHouse3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3 } };
            productToHouse4 = new ProductToOptionData(productToOption4) { ProductList = new List<ProductData> { productData_House_4 } };
            productToHouse5 = new ProductToOptionData(productToOption5) { ProductList = new List<ProductData> { productData_House_5 } };
            productToHouse6 = new ProductToOptionData(productToOption6) { ProductList = new List<ProductData> { productData_House_6 } };
            productToHouse7 = new ProductToOptionData(productToOption7) { ProductList = new List<ProductData> { productData_House_7 } };
            // There is no House quantities 
            houseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2, productToHouse3, productToHouse4 }
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_1 = new ProductData(productData_Option_1);

            ProductData productData_HouseBOM_2 = new ProductData(productData_Option_2);
            
            //Product Subcomponent
            ProductData productData_HouseBOM_3 = new ProductData(productData_Option_3) { Name= PRODUCT2_NAME_DEFAULT };
            
            //SpecSet
            ProductData productData_HouseBOM_4 = new ProductData(productData_Option_4) { Name = PRODUCT4_NAME_DEFAULT };
            
            //Community Qty
            ProductData productData_HouseBOM_5 = new ProductData(productData_Option_5);
            
            //BLR 1
            ProductData productData_HouseBOM_6 = new ProductData(productData_Option_6);
            
            //BLR 2
            ProductData productData_HouseBOM_7 = new ProductData(productData_Option_7);

            productToHouseBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };

            productToHouseBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_HouseBOM_2 } };
            
            //Product Subcomponent
            productToHouseBOM3 = new ProductToOptionData(productToHouse3) { ProductList = new List<ProductData> { productData_HouseBOM_3 },BuildingPhase= "9920-QA_RT_Parent_BuildingPhase_Automation" };
            
            //SpecSet
            productToHouseBOM4 = new ProductToOptionData(productToHouse4) { ProductList = new List<ProductData> { productData_HouseBOM_4 }, BuildingPhase = "9920-QA_RT_Parent_BuildingPhase_Automation" };
            //Community Qty
            productToHouseBOM5 = new ProductToOptionData(productToHouse5) { ProductList = new List<ProductData> { productData_HouseBOM_5 } };
            
            //BLR 1
            productToHouseBOM6 = new ProductToOptionData(productToHouse6) { ProductList = new List<ProductData> { productData_HouseBOM_6 } };
            //BLR 2
            productToHouseBOM7 = new ProductToOptionData(productToHouse7) { ProductList = new List<ProductData> { productData_HouseBOM_7 } };

            houseQuantities_HouseBOM1 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                houseName = HOUSE_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM2, productToHouseBOM3, productToHouseBOM4 },
            };

            houseQuantities_HouseBOM2 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                houseName = HOUSE_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> {productToHouseBOM5 },
            };

            houseQuantities_HouseBOM3 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                houseName = HOUSE_NAME_DEFAULT,
                dependentCondition = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM6  }
            };

            houseQuantities_HouseBOM4 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                houseName = HOUSE_NAME_DEFAULT,
                dependentCondition = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> {  productToHouseBOM7 }
            };

            //Verify Community BOM

            JobQuantities1 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2, productToHouse3, productToHouse4 }
            };

            JobQuantities2 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse5 }
            };

            ProductData productData_Job_6 = new ProductData(productData_Option_6) { Quantities="5.00"};

            productToJobBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { productData_HouseBOM_1 }};

            productToJobBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_HouseBOM_2 } };


            //Product Subcomponent
            productToJobBOM3 = new ProductToOptionData(productToHouse3) { ProductList = new List<ProductData> { productData_HouseBOM_3 }, BuildingPhase = "9920-QA_RT_Parent_BuildingPhase_Automation" };

            //SpecSet
            productToJobBOM4 = new ProductToOptionData(productToHouse4) { ProductList = new List<ProductData> { productData_HouseBOM_4 }, BuildingPhase = "9920-QA_RT_Parent_BuildingPhase_Automation" };

            productToJobBOM5 = new ProductToOptionData(productToHouse5) { ProductList = new List<ProductData> { productData_HouseBOM_5 } };


            productToJobBOM6 = new ProductToOptionData(productToHouse6) { ProductList = new List<ProductData> { productData_Job_6 } };


            houseQuantities_JobBOM1 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToJobBOM1, productToJobBOM2, productToJobBOM3, productToJobBOM4 },
            };

            houseQuantities_JobBOM2 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToJobBOM5},
            };

            houseQuantities_JobBOM3 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToJobBOM6 },
            };

        }
        [Test]
        [Category("Section_XIII")]
        public void Epic_Workflow_Remove_Parent_BuildingPhase_Fuctionality_From_Pipeline_Estimating_To_Allow_Costing_Fuctionality()
        {

            //1.Prepare Parent Building Phase and Child Building Phase and add products to them
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.Prepare Parent Building Phase and Child Building Phase and add products to them.</font>");
            //Parent Building Phase
            ExtentReportsHelper.LogInformation(null, "Parent Building Phase");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);

            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, ParentBuildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", ParentBuildingPhaseData.Code) is true)
            {
                BuildingPhasePage.Instance.ClickItemInGrid("Code", ParentBuildingPhaseData.Code);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add Product to Building Phase.</b></font>");
                
                foreach(string Product in listProductofParentBuildingPhase)
                {
                    if (BuildingPhaseDetailPage.Instance.IsItemInGrid("Product Name", Product) is false)
                    {
                        BuildingPhaseDetailPage.Instance.ClickAddProductToPhaseModal();
                        BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SelectProduct(Product, 1);
                        System.Threading.Thread.Sleep(3000);
                        BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SelectTaxStatus("Phase", 1);
                        System.Threading.Thread.Sleep(3000);
                        BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SetDefault(true);
                        BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.Save();
                    }
                }

            }

            //Child Building Phase
            ExtentReportsHelper.LogInformation(null, "Child Building Phase");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, ChildBuildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", ChildBuildingPhaseData.Code) is true)
            {
                BuildingPhasePage.Instance.ClickItemInGrid("Code", ChildBuildingPhaseData.Code);
                ChildBuidingPhase_url = BuildingPhaseDetailPage.Instance.CurrentURL;
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add Product to Building Phase.</b></font>");
                foreach (string Product in listProductofChildBuildingPhase)
                {
                    if (BuildingPhaseDetailPage.Instance.IsItemInGrid("Product Name", Product) is false)
                    {
                        BuildingPhaseDetailPage.Instance.ClickAddProductToPhaseModal();
                        BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SelectProduct(Product, 1);
                        System.Threading.Thread.Sleep(3000);
                        BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SelectTaxStatus("Phase", 1);
                        System.Threading.Thread.Sleep(3000);
                        BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SetDefault(true);
                        BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.Save();
                    }
                }

            }


            //2. Verify the product is not added in Child Phase after generating House BOM and Community House BOM contain Parent Phase 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2. Verify the product is not added in Child Phase after generating House BOM and Community House BOM contain Parent Phase .</font>");
            //a. The added product is in Parent Phase
            //b.The added product is in Child Phase


            //c. The added product is in Child Phase, contains subcomponent with Child Phase is in Parent Product, Parent Phase is in Subcomponent
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.c The added product is in Child Phase, contains subcomponent with Child Phase is in Parent Product, Parent Phase is in Subcomponent</font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating");
            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(false);
            string expectedMess = "Successfully added new subcomponent!";

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT8_NAME_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT8_NAME_DEFAULT))
                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT8_NAME_DEFAULT);
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(BuildingPhaseOfProduct)
                                            .SelectStyleOfProduct(StyleOfProduct)
                                            .SelectChildBuildingPhaseOfSubComponent(BuildingPhaseOfSubcomponent)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT2_NAME_DEFAULT)
                                            .SelectChildStyleOfSubComponent(StyleOfProduct)
                                            .ClickSaveProductSubcomponent();
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BuildingPhaseOfSubcomponent);

            //d. The added Product is in Child Phase and is in Spec Set Product Conversion with Child Phase is in Original Product, Parent Phase is in New Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.d. The added Product is in Child Phase and is in Spec Set Product Conversion with Child Phase is in Original Product, Parent Phase is in New Product</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData.GroupName);
            }

            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData.GroupName);
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData.SpectSetName);
            // Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            //Add Product and Style to Spec Set
            ExtentReportsHelper.LogInformation(null, "Add Product and Style to Spec Set.");
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(SpecSetData);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(SpecSetData);

            //Assign Spec Set to House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.1c. Assign Spec Set to House.</font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(HOUSES_NAME, (HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT), SpecSetData.SpectSetName, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT));

            //e.The added product is in Parent Phase and from Community Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.e.The added product is in Parent Phase and from Community Quantities</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, COMMUNITY_NAME_DEFAULT);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {COMMUNITY_NAME_DEFAULT} is displayed in grid.</font>");
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY_NAME_DEFAULT);
            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();
            if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesData.BuildingPhase, communityQuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesData);
            }

            //f. The added product is in Parent Phase and also in BOM Logic Rule
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.f. The added product is in Parent Phase and also in BOM Logic Rule</font>");
            BOMLogicRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BOMLogicRules);
            BOMLogicRulePage.Instance.FilterItemInGrid("Rule Name", GridFilterOperator.Contains, BOMLogicRuleData.RuleName);
            if (BOMLogicRulePage.Instance.IsItemInGrid("Rule Name", BOMLogicRuleData.RuleName) is true)
            {
                BOMLogicRulePage.Instance.DeleteBOMLogicRule(BOMLogicRuleData);
            }

            BOMLogicRulePage.Instance.CreateNewBOMLogicRule(BOMLogicRuleData);
            BOMLogicRulePage.Instance.SelectItemInGrid("Rule Name", BOMLogicRuleData.RuleName);

            BOMLogicRuleDetailPage.Instance.ClickAddToShowCreateACondition();
            BOMLogicRuleDetailPage.Instance.SelectCondition(BOMLogicRuleDetailData);
            BOMLogicRuleDetailPage.Instance.SelectOperator(BOMLogicRuleDetailData.Operator);
            BOMLogicRuleDetailPage.Instance.SelectConditionValueForBuildingPhase(PARENT_BUILDINGPHASE_NAME_DEFAULT);
            BOMLogicRuleDetailPage.Instance.Save();
            BOMLogicRuleDetailPage.Instance.CloseModal();

            BOMLogicRuleDetailPage.Instance.ClickAddToActions();
            BOMLogicRuleDetailPage.Instance.SelectAction(BOMLogicRuleDetailData);
            BOMLogicRuleDetailPage.Instance.SelectActionValue(PARENT_BUILDINGPHASE_NAME_DEFAULT, PRODUCT5_NAME_DEFAULT, StyleOfProduct, OPTION2_NAME_DEFAULT);
            BOMLogicRuleDetailPage.Instance.SaveCreateAction();

            //Add all above products (except criteria e and f) to House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Add all above products (except criteria e and f) to House Quantities</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");

            HouseDetailPage.Instance.LeftMenuNavigation("Import");

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_49920.xml");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            foreach (ProductToOptionData housequantity in houseQuantities.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)

                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            //Basic mode
            ExtentReportsHelper.LogInformation(null, "Basic mode");
            //Generate House BOM and verify report BOM show correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Generate House BOM and verify report BOM show correctly</font>");
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("House BOM");
            string HouseBOM_url = HouseBOMDetailPage.Instance.CurrentURL;
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM1.communityCode + "-" + houseQuantities_HouseBOM1.communityName);

            //House Qty, Subcomponent, SpecSets, House Qty
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM1);
            //Community Qty
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM2);
            //BLR 1
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM3);
            //BLR 2
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM4);

            //Open Child Building Phase details page and verify product in Parent Building phase is not added in that
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Open Child Building Phase details page and verify product in Parent Building phase is not added in that</font>");

            CommonHelper.OpenURL(ChildBuidingPhase_url);

            foreach(string product in listProductofParentBuildingPhase)
            {
                if (BuildingPhaseDetailPage.Instance.IsItemInGrid("Product Name", product) is false)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>Product of Parent Building Phase: {product} is not display in Child Building Phase.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Product of Parent Building Phase: {product} is still display in Child Building Phase.</font>");
                }
            }

            //Advanced mode
            CommonHelper.OpenURL(HouseBOM_url);
            ExtentReportsHelper.LogInformation(null, "Advanced mode");
            //Generate House BOM and verify report BOM show correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Generate House BOM and verify report BOM show correctly</font>");
            // Go to Advanced House BOM, select Community
            HouseBOMDetailPage.Instance.ClickOnAdvancedHouseBOMView();
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectAdvanceCommunity(houseQuantities_HouseBOM1.communityCode + "-" + houseQuantities_HouseBOM1.communityName);
            HouseBOMDetailPage.Instance.SelectOptions(Options);

            HouseBOMDetailPage.Instance.GenerateAdvancedHouseBOM();
            HouseBOMDetailPage.Instance.LoadHouseAdvanceQuantities();
            //Filter Option In Grid
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM1.optionName);
            foreach (ProductToOptionData housequantity in houseQuantities_HouseBOM1.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM1.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_HouseBOM1.optionName}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }

            //Filter Option In Grid
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM2.optionName);
            foreach (ProductToOptionData housequantity in houseQuantities_HouseBOM2.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM2.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_HouseBOM2.optionName}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }

            //Filter Option In Grid
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM3.optionName);
            foreach (ProductToOptionData housequantity in houseQuantities_HouseBOM3.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM3.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Dependent Condition", houseQuantities_HouseBOM3.dependentCondition) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_HouseBOM3.optionName}" +
                            $"<br>The expected Dependent Condition: {houseQuantities_HouseBOM3.dependentCondition}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }

            //Filter Option In Grid
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM4.optionName);
            foreach (ProductToOptionData housequantity in houseQuantities_HouseBOM4.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities_HouseBOM4.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Dependent Condition", houseQuantities_HouseBOM4.dependentCondition) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_HouseBOM3.optionName}" +
                            $"<br>The expected Dependent Condition: {houseQuantities_HouseBOM4.dependentCondition}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }

            //Open Child Building Phase details page and verify product in Parent Building phase is not added in that
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Open Child Building Phase details page and verify product in Parent Building phase is not added in that</font>");
            CommonHelper.OpenURL(ChildBuidingPhase_url);

            foreach (string product in listProductofParentBuildingPhase)
            {
                if (BuildingPhaseDetailPage.Instance.IsItemInGrid("Product Name", product) is false)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>Product of Parent Building Phase: {product} is not display in Child Building Phase.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Product of Parent Building Phase: {product} is still display in Child Building Phase.</font>");
                }
            }
            //Open Community of this House, go to House BOM page

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, COMMUNITY_NAME_DEFAULT);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY_NAME_DEFAULT) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY_NAME_DEFAULT);
            }
            CommunityDetailPage.Instance.LeftMenuNavigation("House BOM");


            //Generate Selected
            // Generate Community House BOM and verify report BOM show correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Generate Community House BOM and verify report BOM show correctly</font>");
            CommunityHouseBOMDetailPage.Instance.GenerateHouseBOM();
            CommunityHouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_ALL);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM1);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM2);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM3);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM4);

            //Open Child Building Phase details page and verify product in Parent Building phase is not added in that
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Open Child Building Phase details page and verify product in Parent Building phase is not added in that</font>");
            CommonHelper.OpenURL(ChildBuidingPhase_url);

            foreach (string product in listProductofParentBuildingPhase)
            {
                if (BuildingPhaseDetailPage.Instance.IsItemInGrid("Product Name", product) is false)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>Product of Parent Building Phase: {product} is not display in Child Building Phase.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Product of Parent Building Phase: {product} is still display in Child Building Phase.</font>");
                }
            }
            //3.Verify the product is not added in Child Phase after generating Job BOM contain Parent Phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.Verify the product is not added in Child Phase after generating Job BOM contain Parent Phase</font>");
            //Navigate to Jobs menu > All Jobs
            ExtentReportsHelper.LogInformation(null, "Navigate to Jobs menu > All Jobs.");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }

            JobPage.Instance.CreateJob(jobData);

            //Check Header in BreadsCrumb 
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(jobData.Name) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The Header is present correctly.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Header is present incorrectly.</font>");
            }

            // Step 2: Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Job/ Options page. Add Option '{OPTION_NAME_DEFAULT}' to job if it doesn't exist.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }
            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION_NAME_DEFAULT) is false && JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION1_NAME_DEFAULT) is false && JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION2_NAME_DEFAULT) is false)
            {
                string[] selectedOption = { OPTION_CODE_DEFAULT + "-" + OPTION_NAME_DEFAULT, OPTION1_CODE_DEFAULT + "-" + OPTION1_NAME_DEFAULT, OPTION2_CODE_DEFAULT + "-" + OPTION2_NAME_DEFAULT };

                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(JobQuantities1);
            CommonHelper.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(JobQuantities2);

            //Open Job have a relationship with House and Community at step 2:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Open Job have a relationship with House and Community at step 2:</font>");
            //Add products to Job Quantities by Apply System Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Add products to Job Quantities by Apply System Quantities</font>");
            //Go to Job BOM page, generate Job BOM and verify report BOM show correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to Job BOM page, generate Job BOM and verify report BOM show correctly</font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");
            JobBOMPage.Instance.VerifyJobBomPageIsDisplayed("Generated BOMs");
            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_MODE);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_JobBOM1);
            CommonHelper.RefreshPage();
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_JobBOM2);
            CommonHelper.RefreshPage();
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_JobBOM3);

            //Open Child Building Phase details page and verify product in Parent Building phase is not added in that
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Open Child Building Phase details page and verify product in Parent Building phase is not added in that</font>");
            CommonHelper.OpenURL(ChildBuidingPhase_url);

            foreach (string product in listProductofParentBuildingPhase)
            {
                if (BuildingPhaseDetailPage.Instance.IsItemInGrid("Product Name", product) is false)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>Product of Parent Building Phase: {product} is not display in Child Building Phase.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Product of Parent Building Phase: {product} is still display in Child Building Phase.</font>");
                }
            }
        }

        [TearDown]
        public void DeleteData()
        {

            //Delete File House Quantities
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            //Insert name to filter and click filter by House Name
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_CODE_DEFAULT + ", " + OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_CODE_DEFAULT + ", " + OPTION_NAME_DEFAULT);
            }


            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities.communityCode + '-' + houseQuantities.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
            //Delete SubComponent 
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT8_NAME_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT8_NAME_DEFAULT) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT8_NAME_DEFAULT);
                //Navigate To Subcomponents
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                ProductSubcomponentPage.Instance.ClickDeleteInGird(BuildingPhaseOfSubcomponent);
                string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
                if (act_mess == "Successfully deleted subcomponent")
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BuildingPhaseOfSubcomponent} subcomponent </b></font color>");
                }
                else
                    ExtentReportsHelper.LogFail($"<b> Cannot delete {BuildingPhaseOfSubcomponent} subcomponent </b>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
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
