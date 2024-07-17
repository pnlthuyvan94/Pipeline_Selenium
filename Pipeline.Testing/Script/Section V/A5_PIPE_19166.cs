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
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.Assigments;
using Pipeline.Testing.Pages.Assets.Options.Bid_Costs;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Import;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_V
{
    public partial class A5_PIPE_19166 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_V);
        }

        private JobData jobData;
        private HouseData HouseData;
        private CommunityData communityData;
        private LotData _lotdata;
        OptionData option;

        private readonly int[] indexs = new int[] { };
        private string exportFileName;

        private const string OPTION = "OPTION";

        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_31228";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_CommunityJobBOM_PIPE_31228_Automation";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_HouseJobBOM_PIPE_228_Automation";
        private readonly string JOB_NAME_DEFAULT = "QA_RT_Job_PIPE_31128_Automation";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private const string BUILDING_PHASE1_NAME_DEFAULT = "QA_RT_Phase_01_Automation";
        private const string BUILDING_PHASE1_CODE_DEFAULT = "BF01";

        private const string BUILDING_PHASE4_NAME_DEFAULT = "QA_RT_Phase_04_Automation";
        private const string BUILDING_PHASE4_CODE_DEFAULT = "BF04";


        //1.Global option
        private static string GLOBAL_OPTION_NAME_DEFAULT = "QA_RT_OPTION-GLOBAL_AUTOMATION";
        private static string GLOBAL_OPTION_CODE_DEFAULT = "";
        private OptionData _option_global;

        private JobQuantitiesData jobQuantities_Global;

        private ProductData productData_OptionGlobal_1;
        private ProductData productData_OptionGlobal_2;

        private ProductToOptionData productToOptionGlobal_1;
        private ProductToOptionData productToOptionGlobal_2;

        private ProductData productData_House_OptionGlobal_1;
        private ProductData productData_House_OptionGlobal_2;

        private ProductToOptionData productToHouseBOM_OptionGlobal_1;
        private ProductToOptionData productToHouseBOM_OptionGlobal_2;


        private ProductToOptionData productToHouse_OptionGlobal_1;
        private ProductToOptionData productToHouse_OptionGlobal_2;

        private HouseQuantitiesData houseQuantities_OptionGlobal;
        private HouseQuantitiesData houseQuantities_OptionGlobal_JobBOM;


        // 2.Option/phase bid with no products

        private OptionData _option_phasebid1;
        private static string PHASEBID1_OPTION_NAME_DEFAULT = "QA_RT_OPTION-PHASEBID1_AUTOMATION";
        private static string PHASEBID1_OPTION_CODE_DEFAULT = "";

        private static string PHASEBID1_VALUE = "BF02-QA_RT_Phase_02_Automation";
        private OptionBuildingPhaseData optionBuildingPhaseBid1Data;
        private static double ALLOWANCE = 10.00;

        private ProductData productData_OptionPhaseBid1;
        private ProductToOptionData productToOptionPhaseBid1;
        private HouseQuantitiesData PhaseBid1OptionHouseQuantities_JobBOM;


        //3. Option/phase bid with regular products
        private OptionData _option_phasebid2;
        private static string PHASEBID2_OPTION_NAME_DEFAULT = "QA_RT_OPTION-PHASEBID2_AUTOMATION";
        private static string PHASEBID2_OPTION_CODE_DEFAULT = "";
        private static string PHASE1_VALUE_PHASEBID2 = "BF01-QA_RT_Phase_01_Automation";
        private static string PHASE2_VALUE_PHASEBID2 = "BF02-QA_RT_Phase_02_Automation";
        private static string PHASEBID_DEFAULT = "Phase Bid Only";

        private OptionBuildingPhaseData optionBuildingPhaseBid2Data;
        private OptionQuantitiesData optionPhaseBid2OptionQuantitiesData;
        private OptionQuantitiesData optionPhaseBid2HouseOptionQuantitiesData;

        private ProductData productData_OptionPhaseBid2_1;
        private ProductData productData_OptionPhaseBid2_2;
        private ProductData productData_OptionPhaseBid2_3;
        private ProductToOptionData productToOptionPhaseBid2_1;
        private ProductToOptionData productToOptionPhaseBid2_2;
        private ProductToOptionData productToOptionPhaseBid2_3;

        private JobQuantitiesData JobQuantities_PhaseBid2;

        private HouseQuantitiesData PhaseBidOption2HouseQuantities_JobBOM;

        string[] OptionData = { PHASEBID3_OPTION_NAME_DEFAULT };

        //4.Option/phase bid with supplemental products
        private static string PHASEBID3_OPTION_NAME_DEFAULT = "QA_RT_OPTION-PHASEBID3_AUTOMATION";
        private static string PHASEBID3_OPTION_CODE_DEFAULT = "";
        private static string PHASE1_VALUE_PHASEBID3 = "BF03-QA_RT_Phase_03_Automation";

        private OptionData _option_phasebid3;
        private OptionBuildingPhaseData optionBuildingPhaseBid3Data;
        private OptionQuantitiesData optionPhaseBid3OptionQuantitiesData;
        private OptionQuantitiesData optionPhaseBid3HouseOptionQuantitiesData;
        private CommunityQuantitiesData communityQuantitiesPhaseBid3Data;
        private JobQuantitiesData JobQuantities_PhaseBid3;
        private ProductData productData_OptionPhaseBid3_Product1;
        private ProductData productData_OptionPhaseBid3_Product2;
        private ProductData productData_OptionPhaseBid3_Product3;
        private ProductData productData_OptionPhaseBid3_Product4;
        private ProductToOptionData productToOptionPhaseBid3_Product1;
        private ProductToOptionData productToOptionPhaseBid3_Product2;
        private ProductToOptionData productToOptionPhaseBid3_Product3;
        private ProductToOptionData productToOptionPhaseBid3_Product4;
        private HouseQuantitiesData PhaseBidOption3HouseQuantities;

        private HouseQuantitiesData PhaseBidOption3HouseQuantities_JobBOM;

        //5.Parent/child option

        private OptionData _option_parent;
        private OptionData _option_child1;
        private OptionData _option_child2;

        private JobQuantitiesData JobQuantities_Parent;
        private JobQuantitiesData JobQuantities_Child1;
        private JobQuantitiesData JobQuantities_Child2;

        private static string PARENT_OPTION_NAME_DEFAULT = "QA_RT_OPTION-PARENT_AUTOMATION";
        private static string PARENT_OPTION_CODE_DEFAULT = "";

        //Child option
        private static string CHILD1_OPTION_NAME_DEFAULT = "QA_RT_OPTION-CHILD1_AUTOMATION";
        private static string CHILD1_OPTION_CODE_DEFAULT = "";

        private static string CHILD2_OPTION_NAME_DEFAULT = "QA_RT_OPTION-CHILD2_AUTOMATION";
        private static string CHILD2_OPTION_CODE_DEFAULT = "";

        private static string PHASE1_VALUE_PARENT = "BF04-QA_RT_Phase_04_Automation";
        private OptionQuantitiesData optionParentOptionQuantitiesData;
        private OptionQuantitiesData optionParentHouseOptionQuantitiesData;

        private OptionQuantitiesData optionChild1Option1QuantitiesData;
        private OptionQuantitiesData optionChild1Option2QuantitiesData;
        private OptionQuantitiesData optionChild1HouseOption1QuantitiesData;
        private OptionQuantitiesData optionChild1HouseOption2QuantitiesData;

        private OptionQuantitiesData optionChild2Option1QuantitiesData;
        private OptionQuantitiesData optionChild2Option2QuantitiesData;
        private OptionQuantitiesData optionChild2HouseOption1QuantitiesData;
        private OptionQuantitiesData optionChild2HouseOption2QuantitiesData;


        //OPTION-CHILD1 - JOB BOM
        private ProductData _product_optionParent1_RT09;
        private ProductData _product_optionParent1_RT10;

        private ProductData _product_optionChild1_RT11;

        private ProductData _product_optionChild1_RT13;
        private ProductData _product_optionChild1_RT14;

        //OPTION-CHILD2 - JOB BOM
        private ProductData _product_optionChild2_RT12;
        private ProductData _product_optionChild2_RT13;
        private ProductData _product_optionChild2_RT14;
        private ProductData _product_optionParent2_RT09;
        private ProductData _product_optionParent2_RT10;

        //OPTION-PARENT - JOB BOM
        private ProductData _product_optionParent_RT09;
        private ProductData _product_optionParent_RT10;
        private ProductData _product_optionParent_RT01;

        private ProductToOptionData productToOptionChild1;
        private ProductToOptionData productToOptionChild2;

        private ProductToOptionData productToOptionParent1;
        private ProductToOptionData productToOptionParent2;

        private ProductToOptionData productToHouseParent1;
        private ProductToOptionData productToHouseParent2;
        private ProductToOptionData productToHouseChild1;
        private ProductToOptionData productToHouseChild2;


        private HouseQuantitiesData ParentOptionHouseQuantities;

        private HouseQuantitiesData Child1OptionHouseQuantities;

        private HouseQuantitiesData Child2OptionHouseQuantities;

        private HouseQuantitiesData ParentOptionHouseQuantities_JobBOM;

        private HouseQuantitiesData Child1OptionHouseQuantities_JobBOM;

        private HouseQuantitiesData Child2OptionHouseQuantities_JobBOM;


        //6.Option with products assigned to a Spec Set (Use Job BOM result at TC 5 to continue)
        private SpecSetData _specsetGroup1;
        private SpecSetData _specsetGroup2;
        private ProductData _productspecset;
        private ProductData _productsubcomponent;
        private ProductData getNewproductspecset;
        private ProductData getNewproductsubcomponent;

        private readonly string BUILDINGPHASE_SUBCOMPONENT_DEFAULT = PHASE1_VALUE_PARENT;
        private readonly string PRODUCT_SUBCOMPONENT_NAME_DEFAULT = "QA_RT_Product_Subcomponent_PIPE_31228_Automation";
        private readonly string STYLE_NAME_DEFAULT = "DEFAULT";

        //SpecSet
        private ProductData _product_optionChild1_Specset;
        private ProductData _product_optionChild2_Specset;

        private ProductToOptionData productToOptionChild1_Specset;
        private ProductToOptionData productToOptionChild2_Specset;

        private ProductToOptionData productToHouseChild1_SpecSet;
        private ProductToOptionData productToHouseChild2_SpecSet;

        private HouseQuantitiesData Child1_SpecSetOptionHouseQuantities_JobBOM;
        private HouseQuantitiesData Child2_SpecSetOptionHouseQuantities_JobBOM;

        //Subcomponent

        private ProductData _product_optionChild1_Subcomponent;
        private ProductData _product_optionChild2_Subcomponent;

        private ProductToOptionData productToOptionChild1_Subcomponent;
        private ProductToOptionData productToOptionChild2_Subcomponent;

        private ProductToOptionData productToHouseChild1_Subcomponent;
        private ProductToOptionData productToHouseChild2_Subcomponent;

        private HouseQuantitiesData Child1_SubcomponentOptionHouseQuantities_JobBOM;
        private HouseQuantitiesData Child2_SubcomponentOptionHouseQuantities_JobBOM;


        //B. Verify all view by dropdown list
        private const string JOB_BOM_VIEW_PHASE = "Phase";
        private const string JOB_BOM_VIEW_OPTION = "Option";
        private const string JOB_BOM_VIEW_PHASE_PRODUCT = "Phase/Product";
        private const string JOB_BOM_VIEW_PHASE_PRODUCT_USE = "Phase/Product/Use";
        private const string JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT = "Option/Phase/Product";
        private const string JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT_USE = "Option/Phase/Product/Use";

        //View Phase/Product
        private ProductData _product_optionForView_PhaseProduct;
        private ProductToOptionData productToOptionForView_PhaseProduct;
        private ProductToOptionData productToHouseForView_PhaseProduct;
        private HouseQuantitiesData HouseQuantitiesForView_PhaseProduct_JobBOM;

        //View Phase/Product/Use
        private ProductData _product_optionForView_PhaseProductUse;
        private ProductToOptionData productToOptionForView_PhaseProductUse;
        private ProductToOptionData productToHouseForView_PhaseProductUse;
        private HouseQuantitiesData HouseQuantitiesForView_PhaseProductUse_JobBOM;

        //View Option/Phase/Product
        private ProductData _product_optionForView_OptionPhaseProduct;
        private ProductToOptionData productToOptionForView_OptionPhaseProduct;
        private ProductToOptionData productToHouseForView_OptionPhaseProduct;
        private HouseQuantitiesData HouseQuantitiesForView_OptionPhaseProduct_JobBOM;

        //View Option/Phase/Product/Use
        private ProductData _product_optionForView_OptionPhaseProductUse;
        private ProductToOptionData productToOptionForView_OptionPhaseProductUse;
        private ProductToOptionData productToHouseForView_OptionPhaseProductUse;
        private HouseQuantitiesData HouseQuantitiesForView_OptionPhaseProductUse_JobBOM;

        //C.Verify the Product Rounding/Waste on the Job BOM page
        private ProductData _product_StandradRounding;
        private ProductData _product_RoundUp;
        private ProductData _product_RoundDown;
        private ProductData _product_Waste;

        //1. Standard Rounding
        private static string PRODUCT_NAME_STANDRAD_ROUNDING = "QA_RT_Product15_JobBOM_Automation";
        private JobQuantitiesData JobQuantitiesStandradRounding;
        private static string STANDRAD_ROUNDING_OPTION_NAME_DEFAULT = "QA_RT_OPTION-CHILD1_AUTOMATION";
        private static string STANDRAD_ROUNDING_OPTION_CODE_DEFAULT = "";
        private ProductData productData_StandradRounding;

        private ProductToOptionData productToOption_StandradRounding;
        private ProductToOptionData productToHouseBOM_Option_StandradRounding;
        private ProductToOptionData productToHouseBOM_Option_StandradRounding_Negative_Value;

        private ProductToOptionData productToHouse_Option_StandradRounding;

        private HouseQuantitiesData houseQuantities_Option_StandradRounding;
        private HouseQuantitiesData houseQuantities_Option_StandradRounding_JobBOM;

        private HouseQuantitiesData houseQuantities_Option_StandradRounding_JobBOM_Negative_Value;

        //2. Always Round-Up
        private static string PRODUCT_NAME_ROUND_UP = "QA_RT_Product16_JobBOM_Automation";
        private static string ROUND_UP_ROUNDING_OPTION_NAME_DEFAULT = "QA_RT_OPTION-CHILD1_AUTOMATION";
        private static string ROUND_UP_ROUNDING_OPTION_CODE_DEFAULT = "";
        private JobQuantitiesData JobQuantitiesRoundUp;
        private ProductData productData_RoundUp;

        private ProductToOptionData productToOption_RoundUp;

        private ProductToOptionData productToHouse_Option_RoundUp;
        private ProductToOptionData productToHouseBOM_Option_RoundUp;
        private ProductToOptionData productToHouseBOM_Option_RoundUp_Negative_Value;

        private HouseQuantitiesData houseQuantities_Option_RoundUp;
        private HouseQuantitiesData houseQuantities_Option_RoundUp_JobBOM;

        //3. Always Round Down
        private static string PRODUCT_NAME_ROUND_DOWN = "QA_RT_Product17_JobBOM_Automation";
        private static string ROUND_DOWN_ROUNDING_OPTION_NAME_DEFAULT = "QA_RT_OPTION-CHILD1_AUTOMATION";
        private static string ROUND_DOWN_ROUNDING_OPTION_CODE_DEFAULT = "";
        private JobQuantitiesData JobQuantitiesRoundDown;
        private ProductData productData_RoundDown;

        private ProductToOptionData productToOption_RoundDown;
        private ProductToOptionData productToHouse_Option_RoundDown;
        private ProductToOptionData productToHouseBOM_Option_RoundDown;
        private ProductToOptionData productToHouseBOM_Option_RoundDown_Negative_Value;
        private HouseQuantitiesData houseQuantities_Option_RoundDown;
        private HouseQuantitiesData houseQuantities_Option_RoundDown_JobBOM;
        private HouseQuantitiesData houseQuantities_Option_RoundDown_JobBOM_Negative_Value;

        //4. Product Waste
        private static string PRODUCT_NAME_WASTE = "QA_RT_Product18_JobBOM_Automation";
        private static string WASTE_ROUNDING_OPTION_NAME_DEFAULT = "QA_RT_OPTION-CHILD1_AUTOMATION";
        private static string WASTE_ROUNDING_OPTION_CODE_DEFAULT = "";

        private JobQuantitiesData JobQuantitiesWaste;

        private ProductData productData_Waste;

        private ProductToOptionData productToOption_Waste;
        private ProductToOptionData productToHouse_Option_Waste;
        private ProductToOptionData productToHouseBOM_Option_Waste;

        private HouseQuantitiesData houseQuantities_Option_Waste;
        private HouseQuantitiesData houseQuantities_Option_Waste_JobBOM;

        //D. Verify BOM Adjustment functions
        private JobBOMData _AdjustJobBOMData;
        private JobBOMData _OneTimejobBOMData;

        //G. Verify all export function in the 'Utilities' dropdown list
        readonly string EXPORT_JOBBOM_TO_CSV = "Export CSV";
        readonly string EXPORT_JOBBOM_TO_EXCEL = "Export Excel";
        readonly string EXPORT_JOBBOM_TO_XML = "Export XML";
        readonly string EXPORT_JOBBOM_TO_XML_WITH_TRACE = "Export XML w/ Trace";

        //H. Verify the Import Job function
        private readonly string IMPORT_PRODUCT_NAME_Option_Specified = "QA_RT_Product19_JobBOM_Automation";
        private readonly bool IS_OPTION_SPECIFIED = true;
        private JobImportQuantitiesData Option_Specified_expectedData_ExistBuildingPhase;

        //Create Total Job Quantities 
        private ProductData Option_Specified_productData;
        private ProductToOptionData Option_Specified_productTo;
        private ProductToOptionData Option_Specified_productToHouse;
        private ProductToOptionData Option_Specified_productToHouseBOM;
        private HouseQuantitiesData Option_Specified_JobQuantities;
        private HouseQuantitiesData Option_Specified_JobQuantities_JobBOM;


        private readonly string IMPORT_PRODUCT_NAME_NoOption_Specified = "QA_RT_Product19_JobBOM_Automation";
        private readonly bool IS_NO_OPTION_SPECIFIED = false;
        private JobImportQuantitiesData NoOption_Specified_expectedData_ExistBuildingPhase;

        private ProductData NoOption_Specified_productData;
        private ProductToOptionData NoOption_Specified_productTo;
        private ProductToOptionData NoOption_Specified_productToHouse;
        private ProductToOptionData NoOption_Specified_productToHouseBOM;
        private HouseQuantitiesData NoOption_Specified_JobQuantities;
        private HouseQuantitiesData NoOption_Specified_JobQuantities_JobBOM;

        [SetUp]
        public void GetData()
        {
            //Setup Create New Data
            jobData = new JobData()
            {
                Name = "QA_RT_Job_PIPE_31128_Automation",
                Community = "Automation_31228-QA_RT_CommunityJobBOM_PIPE_31228_Automation",
                House = "1228-QA_RT_HouseJobBOM_PIPE_228_Automation",
                Lot = "_111 - Sold",
                Orientation = "None"
            };

            var optionType1 = new List<bool>()
            {
                false, false, false
            };
            option = new OptionData()
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
                Types = optionType1
            };

            HouseData = new HouseData()
            {
                HouseName = "QA_RT_HouseJobBOM_PIPE_228_Automation",
                SaleHouseName = "QA_RT_HouseJobBOM_PIPE_228_Automation",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "1228",
                BasePrice = "1000000.00",
                SQFTBasement = "0",
                SQFTFloor1 = "0",
                SQFTFloor2 = "0",
                SQFTHeated = "0",
                SQFTTotal = "0",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "house - testing"
            };

            communityData = new CommunityData()
            {
                Name = "QA_RT_CommunityJobBOM_PIPE_31228_Automation",
                Division = "None",
                Code = "Automation_31228",
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
                Slug = "QA_RT_CommunityJobBOM_PIPE_31228_Automation",
            };

            _lotdata = new LotData()
            {
                Number = "_111",
                Status = "Sold"
            };

            var optionType = new List<bool>()
            {
                false, false, true
            };


            //1.Global option
            _option_global = new OptionData()
            {
                Name = "QA_RT_OPTION-GLOBAL_AUTOMATION",
                Number = "",
                SquareFootage = 0,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 100,
                Types = optionType
            };


            jobQuantities_Global = new JobQuantitiesData()
            {
                Option = GLOBAL_OPTION_NAME_DEFAULT,
                BuildingPhase = { BUILDING_PHASE1_CODE_DEFAULT+"-"+BUILDING_PHASE1_NAME_DEFAULT },
                Source = "Pipeline",
                Products = { "QA_RT_Product01_JobBOM_Automation", "QA_RT_Product02_JobBOM_Automation" },
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantity = { "100.00", "200.00" }

            };

            productData_OptionGlobal_1 = new ProductData()
            {
                Name = "QA_RT_Product01_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE",
            };

            productData_OptionGlobal_2 = new ProductData()
            {
                Name = "QA_RT_Product02_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "200.00",
                Unit = "NONE",
            };

            productToOptionGlobal_1 = new ProductToOptionData()
            {
                BuildingPhase = $"{BUILDING_PHASE1_CODE_DEFAULT}-{BUILDING_PHASE1_NAME_DEFAULT}",
                ProductList = new List<ProductData> { productData_OptionGlobal_1 }
            };

            productToOptionGlobal_2 = new ProductToOptionData()
            {
                BuildingPhase = $"{BUILDING_PHASE1_CODE_DEFAULT}-{BUILDING_PHASE1_NAME_DEFAULT}",
                ProductList = new List<ProductData> { productData_OptionGlobal_2 }
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_OptionGlobal_1 = new ProductData(productData_OptionGlobal_1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_OptionGlobal_2 = new ProductData(productData_OptionGlobal_2);


            // There is no House quantities 4

            productToHouse_OptionGlobal_1 = new ProductToOptionData(productToOptionGlobal_1) { ProductList = new List<ProductData> { productData_House_OptionGlobal_1 } };
            productToHouse_OptionGlobal_2 = new ProductToOptionData(productToOptionGlobal_2) { ProductList = new List<ProductData> { productData_House_OptionGlobal_2 } };


            houseQuantities_OptionGlobal = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = GLOBAL_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_OptionGlobal_1, productToHouse_OptionGlobal_2 }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"
            ProductData productData_HouseBOM_1 = new ProductData(productData_OptionGlobal_1);
            ProductData productData_HouseBOM_2 = new ProductData(productData_OptionGlobal_2);

            productToHouseBOM_OptionGlobal_1 = new ProductToOptionData(productToHouse_OptionGlobal_1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };

            productToHouseBOM_OptionGlobal_2 = new ProductToOptionData(productToHouse_OptionGlobal_2) { ProductList = new List<ProductData> { productData_HouseBOM_2 } };


            houseQuantities_OptionGlobal_JobBOM = new HouseQuantitiesData(houseQuantities_OptionGlobal)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_OptionGlobal_1, productToHouseBOM_OptionGlobal_2 }
            };


            //2. Option/phase bid with no products

            _option_phasebid1 = new OptionData()
            {
                Name = "QA_RT_OPTION-PHASEBID1_AUTOMATION",
                Number = "",
                SquareFootage = 0,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 100.00,
                Types = optionType1
            };

            optionBuildingPhaseBid1Data = new OptionBuildingPhaseData()
            {
                OptionName = PHASEBID1_OPTION_NAME_DEFAULT,
                BuildingPhase = new string[] { PHASEBID1_VALUE },
                Name = "Auto test",
                Description = "Auto test",
                Allowance = ALLOWANCE
            };

            productData_OptionPhaseBid1 = new ProductData()
            {

                BuildingPhase = PHASEBID1_VALUE
            };
            productToOptionPhaseBid1 = new ProductToOptionData()
            {
                ProductList = new List<ProductData> { productData_OptionPhaseBid1 }
            };


            PhaseBid1OptionHouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = PHASEBID1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToOptionPhaseBid1 }
            };


            //3. Option/phase bid with regular products

            _option_phasebid2 = new OptionData()
            {
                Name = "QA_RT_OPTION-PHASEBID2_AUTOMATION",
                Number = "",
                SquareFootage = 0,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 100.00,
                Types = optionType1
            };

            optionBuildingPhaseBid2Data = new OptionBuildingPhaseData()
            {
                OptionName = PHASEBID2_OPTION_NAME_DEFAULT,
                BuildingPhase = new string[] { PHASE2_VALUE_PHASEBID2 },
                Name = "Auto test",
                Description = "Auto test",
                Allowance = ALLOWANCE
            };
            optionPhaseBid2OptionQuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE1_VALUE_PHASEBID2,
                ProductName = "QA_RT_Product01_JobBOM_Automation",
                ProductDescription = "Product Description",
                Category="NONE",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "10.00",
                Source = "Pipeline"
            };

            optionPhaseBid2HouseOptionQuantitiesData = new OptionQuantitiesData()
            {
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                OptionName = PHASEBID2_OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE2_VALUE_PHASEBID2,
                ProductName = "QA_RT_Product03_JobBOM_Automation",
                Category = "NONE",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "20.00",
                Source = "Pipeline"
            };


            JobQuantities_PhaseBid2 = new JobQuantitiesData()
            {
                Option = PHASEBID2_OPTION_NAME_DEFAULT,
                BuildingPhase = { PHASE2_VALUE_PHASEBID2 },
                Source = "Pipeline",
                Products = { "QA_RT_Product04_JobBOM_Automation" },
                Style = "DEFAULT",
                Use = "NONE",
                Quantity = { "30.00" }
            };

            productData_OptionPhaseBid2_1 = new ProductData()
            {
                Name = "QA_RT_Product01_JobBOM_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "10.00",
                Unit = "NONE"
            };

            productData_OptionPhaseBid2_2 = new ProductData()
            {
                Name = "QA_RT_Product03_JobBOM_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "20.00",
                Unit = "NONE"
            };
            productData_OptionPhaseBid2_3 = new ProductData()
            {
                Name = "QA_RT_Product04_JobBOM_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE"
            };


            productToOptionPhaseBid2_1 = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PHASEBID2,
                ProductList = new List<ProductData> { productData_OptionPhaseBid2_1 }
            };

            productToOptionPhaseBid2_2 = new ProductToOptionData()
            {
                BuildingPhase = PHASE2_VALUE_PHASEBID2,
                ProductList = new List<ProductData> { productData_OptionPhaseBid2_2 },
                PhaseBid = PHASEBID_DEFAULT
            };

            productToOptionPhaseBid2_3 = new ProductToOptionData()
            {
                BuildingPhase = PHASE2_VALUE_PHASEBID2,
                ProductList = new List<ProductData> { productData_OptionPhaseBid2_3 },
                PhaseBid = PHASEBID_DEFAULT
            };


            PhaseBidOption2HouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = PHASEBID2_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToOptionPhaseBid2_1, productToOptionPhaseBid2_2, productToOptionPhaseBid2_3 }
            };


            //4.Option/phase bid with supplemental products
            _option_phasebid3 = new OptionData()
            {
                Name = "QA_RT_OPTION-PHASEBID3_AUTOMATION",
                Number = "",
                SquareFootage = 0,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 100.00
            };

            optionPhaseBid3OptionQuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE1_VALUE_PHASEBID3,
                ProductName = "QA_RT_Product05_JobBOM_Automation",
                ProductDescription = "Product Description",
                Style = "QA_Style_Automation",
                Condition = false,
                Use = string.Empty,
                Quantity = "31.00",
                Source = "Pipeline"
            };

            optionBuildingPhaseBid3Data = new OptionBuildingPhaseData()
            {
                OptionName = PHASEBID3_OPTION_NAME_DEFAULT,
                BuildingPhase = new string[] { PHASE1_VALUE_PHASEBID3 },
                Name = "Auto test",
                Description = "Auto test",
                Allowance = ALLOWANCE
            };


            optionPhaseBid3HouseOptionQuantitiesData = new OptionQuantitiesData()
            {
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                OptionName = PHASEBID3_OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PHASEBID3,
                ProductName = "QA_RT_Product06_JobBOM_Automation",
                ProductDescription = "Product Description",
                Category = "NONE",
                Style = "QA_Style_Automation",
                Condition = false,
                Use = string.Empty,
                Quantity = "32.00",
                Source = "Pipeline"
            };

            communityQuantitiesPhaseBid3Data = new CommunityQuantitiesData()
            {
                OptionName = PHASEBID3_OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PHASEBID3,
                ProductName = "QA_RT_Product07_JobBOM_Automation",
                ProductDescription = "Product Description",
                Style = "QA_Style_Automation",
                Condition = false,
                Use = string.Empty,
                Quantity = "33.00",
                Source = "Pipeline"
            };


            JobQuantities_PhaseBid3 = new JobQuantitiesData()
            {
                Option = PHASEBID3_OPTION_NAME_DEFAULT,
                BuildingPhase = { PHASE1_VALUE_PHASEBID3 },
                Source = "Pipeline",
                Products = { "QA_RT_Product08_JobBOM_Automation" },
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantity = { "34.00" }
            };

            productData_OptionPhaseBid3_Product1 = new ProductData()
            {
                Name = "QA_RT_Product05_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "31.00",
                Unit = "NONE"
            };

            productData_OptionPhaseBid3_Product2 = new ProductData()
            {
                Name = "QA_RT_Product06_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "32.00",
                Unit = "NONE"
            };

            productData_OptionPhaseBid3_Product3 = new ProductData()
            {
                Name = "QA_RT_Product07_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "33.00",
                Unit = "NONE"
            };
            productData_OptionPhaseBid3_Product4 = new ProductData()
            {
                Name = "QA_RT_Product08_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "34.00",
                Unit = "NONE"
            };


            productToOptionPhaseBid3_Product1 = new ProductToOptionData()
            {
                PhaseBid = "Phase Bid with Supplementals",
                BuildingPhase = PHASE1_VALUE_PHASEBID3,
                ProductList = new List<ProductData> { productData_OptionPhaseBid3_Product1 }

            };
            productToOptionPhaseBid3_Product2 = new ProductToOptionData()
            {
                PhaseBid = "Phase Bid with Supplementals",
                BuildingPhase = PHASE1_VALUE_PHASEBID3,
                ProductList = new List<ProductData> { productData_OptionPhaseBid3_Product2 }

            };
            productToOptionPhaseBid3_Product3 = new ProductToOptionData()
            {
                PhaseBid = "Phase Bid with Supplementals",
                BuildingPhase = PHASE1_VALUE_PHASEBID3,
                ProductList = new List<ProductData> { productData_OptionPhaseBid3_Product3 }

            };
            productToOptionPhaseBid3_Product4 = new ProductToOptionData()
            {
                PhaseBid = "Phase Bid with Supplementals",
                BuildingPhase = PHASE1_VALUE_PHASEBID3,
                ProductList = new List<ProductData> { productData_OptionPhaseBid3_Product4 }

            };

            PhaseBidOption3HouseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = PHASEBID3_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToOptionPhaseBid3_Product1, productToOptionPhaseBid3_Product2, productToOptionPhaseBid3_Product3, productToOptionPhaseBid3_Product4 }

            };
            PhaseBidOption3HouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = PHASEBID3_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToOptionPhaseBid3_Product1, productToOptionPhaseBid3_Product2, productToOptionPhaseBid3_Product3, productToOptionPhaseBid3_Product4 }
            };


            //5.Parent/child option

            _option_parent = new OptionData()
            {
                Name = "QA_RT_OPTION-PARENT_AUTOMATION",
                Number = "",
                SquareFootage = 0,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType1
            };

            _option_child1 = new OptionData()
            {
                Name = "QA_RT_OPTION-CHILD1_AUTOMATION",
                Number = "",
                SquareFootage = 0,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType1
            };

            _option_child2 = new OptionData()
            {
                Name = "QA_RT_OPTION-CHILD2_AUTOMATION",
                Number = "",
                SquareFootage = 0,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType1
            };


            optionParentHouseOptionQuantitiesData = new OptionQuantitiesData()
            {
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                OptionName = PARENT_OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product10_JobBOM_Automation",
                ProductDescription = "Product Description",
                Category = "NONE",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "54.00",
                Source = "Pipeline"
            };

            optionChild1HouseOption1QuantitiesData = new OptionQuantitiesData()
            {
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                OptionName = CHILD1_OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product13_JobBOM_Automation",
                ProductDescription = "Product Description",
                Category = "NONE",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "55.00",
                Source = "Pipeline"
            };

            optionChild1HouseOption2QuantitiesData = new OptionQuantitiesData()
            {
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                OptionName = CHILD1_OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product14_JobBOM_Automation",
                ProductDescription = "Product Description",
                Category = "NONE",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "66.00",
                Source = "Pipeline"
            };

            optionParentOptionQuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product09_JobBOM_Automation",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "11.00",
                Source = "Pipeline"
            };

            optionChild1Option1QuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product10_JobBOM_Automation",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "-7.00",
                Source = "Pipeline"
            };

            optionChild1Option2QuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product11_JobBOM_Automation",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "55.00",
                Source = "Pipeline"
            };

            optionChild2Option1QuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product10_JobBOM_Automation",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "-3.00",
                Source = "Pipeline"
            };

            optionChild2Option2QuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product12_JobBOM_Automation",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "64.00",
                Source = "Pipeline"
            };

            optionChild2HouseOption1QuantitiesData = new OptionQuantitiesData()
            {
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                OptionName = CHILD1_OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product13_JobBOM_Automation",
                ProductDescription = "Product Description",
                Category = "NONE",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "99.00",
                Source = "Pipeline"
            };

            optionChild2HouseOption2QuantitiesData = new OptionQuantitiesData()
            {
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                OptionName = CHILD1_OPTION_NAME_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductName = "QA_RT_Product14_JobBOM_Automation",
                ProductDescription = "Product Description",
                Category = "NONE",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "88.00",
                Source = "Pipeline"
            };
            JobQuantities_Parent = new JobQuantitiesData()
            {
                Option = PARENT_OPTION_NAME_DEFAULT,
                BuildingPhase = { BUILDING_PHASE1_CODE_DEFAULT + "-" + BUILDING_PHASE1_NAME_DEFAULT },
                Source = "Pipeline",
                Products = { "QA_RT_Product01_JobBOM_Automation" },
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantity = { "100.00" }
            };

            //OPTION-CHILD1
            _product_optionParent1_RT09 = new ProductData()
            {
                Name = "QA_RT_Product09_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "11.00"
            };

            _product_optionParent1_RT10 = new ProductData()
            {
                Name = "QA_RT_Product10_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "47.00"
            };

            _product_optionChild1_RT11 = new ProductData()
            {
                Name = "QA_RT_Product11_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "55.00"
            };

            _product_optionChild1_RT13 = new ProductData()
            {
                Name = "QA_RT_Product13_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "55.00"
            };

            _product_optionChild1_RT14 = new ProductData()
            {
                Name = "QA_RT_Product14_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "66.00"
            };


            //OPTION-CHILD2
            _product_optionChild2_RT12 = new ProductData()
            {
                Name = "QA_RT_Product12_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "64.00"
            };
            _product_optionChild2_RT13 = new ProductData()
            {
                Name = "QA_RT_Product13_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "99.00"
            };

            _product_optionChild2_RT14 = new ProductData()
            {
                Name = "QA_RT_Product14_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "88.00"
            };

            _product_optionParent2_RT09 = new ProductData()
            {
                Name = "QA_RT_Product09_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "11.00"
            };

            _product_optionParent2_RT10 = new ProductData()
            {
                Name = "QA_RT_Product10_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "51.00"
            };

            //OPTION-PARENT
            _product_optionParent_RT09 = new ProductData()
            {
                Name = "QA_RT_Product09_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "11.00"
            };

            _product_optionParent_RT10 = new ProductData()
            {
                Name = "QA_RT_Product10_JobBOM_Automation",
                Style = "DEFAULT",
                Quantities = "54.00"
            };

            _product_optionParent_RT01 = new ProductData()
            {
                Name = "QA_RT_Product01_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Quantities = "100.00"
            };

            productToOptionChild1 = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionParent1_RT09, _product_optionParent1_RT10, _product_optionChild1_RT11, _product_optionChild1_RT13, _product_optionChild1_RT14 }
            };

            productToOptionChild2 = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionChild2_RT12, _product_optionChild2_RT13, _product_optionChild2_RT14, _product_optionParent2_RT09, _product_optionParent2_RT10 }
            };

            productToOptionParent1 = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionParent_RT09, _product_optionParent_RT10 }
            };

            productToOptionParent2 = new ProductToOptionData()
            {
                BuildingPhase = $"{BUILDING_PHASE1_CODE_DEFAULT}-{BUILDING_PHASE1_NAME_DEFAULT}",
                ProductList = new List<ProductData> { _product_optionParent_RT01 }
            };

            productToHouseChild1 = new ProductToOptionData(productToOptionChild1);
            productToHouseChild2 = new ProductToOptionData(productToOptionChild2);
            productToHouseParent1 = new ProductToOptionData(productToOptionParent1);
            productToHouseParent2 = new ProductToOptionData(productToOptionParent2);

            Child1OptionHouseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseChild1 }
            };

            Child2OptionHouseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD2_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseChild2 }
            };
            ParentOptionHouseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = PARENT_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseParent1, productToHouseParent2 }
            };

            /****************************** Create Product quantities on House ******************************/
            // There is no House quantities 4


            Child1OptionHouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseChild1 }
            };

            Child2OptionHouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD2_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseChild2 }
            };
            ParentOptionHouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = PARENT_OPTION_NAME_DEFAULT,
            };

            //6.Option with products assigned to a Spec Set (Use Job BOM result at TC 5 to continue)
            _productspecset = new ProductData()
            {

                Name = "QA_RT_Product_SpecSet_PIPE_31228_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.00",
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT

            };

            _productsubcomponent = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent_PIPE_31228_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.00",
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT
            };

            _specsetGroup1 = new SpecSetData()
            {

                GroupName = "QA_RT_SpecSetGroup01_Automation",
                SpectSetName = "QA_RT_SpecSet_Automation",
                OriginalPhase = PHASE1_VALUE_PARENT,
                OriginalProduct = "QA_RT_Product11_JobBOM_Automation",
                OriginalProductStyle = "QA_Style_Automation",
                OriginalProductUse = "ALL",
                NewPhase = PHASE1_VALUE_PARENT,
                NewProduct = "QA_RT_Product_SpecSet_PIPE_31228_Automation",
                NewProductStyle = "DEFAULT",
                NewProductUse = "NONE",
                ProductCalculation = "NONE"
            };

            _specsetGroup2 = new SpecSetData()
            {
                SpectSetName = "QA_RT_SpecSetGroup02_Automation",
                OriginalPhase = PHASE1_VALUE_PARENT,
                OriginalProduct = "QA_RT_Product12_JobBOM_Automation",
                OriginalProductStyle = "QA_Style_Automation",
                OriginalProductUse = "ALL",
                NewPhase = PHASE1_VALUE_PARENT,
                NewProduct = "QA_RT_Product_SpecSet_PIPE_31228_Automation",
                NewProductStyle = "DEFAULT",
                NewProductUse = "NONE",
                ProductCalculation = "NONE"
            };

            JobQuantities_Child1 = new JobQuantitiesData()
            {
                Option = CHILD1_OPTION_NAME_DEFAULT,
                BuildingPhase = { PHASE1_VALUE_PARENT },
                Source = "Pipeline",
                Products = { "QA_RT_Product_SpecSet_PIPE_31228_Automation" },
                Style = "DEFAULT",
                Use = "NONE",
                Quantity = { "56.00" }
            };

            JobQuantities_Child2 = new JobQuantitiesData()
            {
                Option = CHILD2_OPTION_NAME_DEFAULT,
                BuildingPhase = { PHASE1_VALUE_PARENT },
                Source = "Pipeline",
                Products = { "QA_RT_Product_SpecSet_PIPE_31228_Automation" },
                Style = "DEFAULT",
                Use = "NONE",
                Quantity = { "76.00" }
            };

            //Create Job BOM By SpecSet product

            _product_optionChild1_Specset = new ProductData()
            {
                Name = "QA_RT_Product_SpecSet_PIPE_31228_Automation",
                Quantities = "56.00"
            };

            _product_optionChild2_Specset = new ProductData()
            {
                Name = "QA_RT_Product_SpecSet_PIPE_31228_Automation",
                Quantities = "76.00"
            };

            productToOptionChild1_Specset = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionChild1_Specset }
            };


            productToOptionChild2_Specset = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionChild2_Specset }
            };

            /****************************** Create Product quantities on House ******************************/
            // There is no House quantities 4

            productToHouseChild1_SpecSet = new ProductToOptionData(productToOptionChild1_Specset);
            productToHouseChild2_SpecSet = new ProductToOptionData(productToOptionChild2_Specset);

            Child1_SpecSetOptionHouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseChild1_SpecSet }
            };

            Child2_SpecSetOptionHouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD2_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseChild2_SpecSet }
            };


            //Create Job BOM By SpecSet product
            _product_optionChild1_Subcomponent = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent_PIPE_31228_Automation",
                Quantities = "56.00",
            };
            _product_optionChild2_Subcomponent = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent_PIPE_31228_Automation",
                Quantities = "76.00",
            };

            productToOptionChild1_Subcomponent = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionChild1_Subcomponent }
            };


            productToOptionChild2_Subcomponent = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionChild2_Subcomponent }
            };

            /****************************** Create Product quantities on House ******************************/
            // There is no House quantities 4


            productToHouseChild1_Subcomponent = new ProductToOptionData(productToOptionChild1_Subcomponent);
            productToHouseChild2_Subcomponent = new ProductToOptionData(productToOptionChild2_Subcomponent);


            Child1_SubcomponentOptionHouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseChild1_Subcomponent }
            };

            Child2_SubcomponentOptionHouseQuantities_JobBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD2_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseChild2_Subcomponent }
            };

            //B.Verify all view by dropdown list
            // View Phase/Product
            _product_optionForView_PhaseProduct = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent_PIPE_31228_Automation",
                Quantities = "132.00"
            };


            productToOptionForView_PhaseProduct = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionForView_PhaseProduct }
            };

            productToHouseForView_PhaseProduct = new ProductToOptionData(productToOptionForView_PhaseProduct);



            HouseQuantitiesForView_PhaseProduct_JobBOM = new HouseQuantitiesData(Child1_SubcomponentOptionHouseQuantities_JobBOM)
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseForView_PhaseProduct }
            };
            //View Phase/Product/Use 

            _product_optionForView_PhaseProductUse = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent_PIPE_31228_Automation",
                Quantities = "132.00"
            };


            productToOptionForView_PhaseProductUse = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionForView_PhaseProductUse }
            };

            productToHouseForView_PhaseProductUse = new ProductToOptionData(productToOptionForView_PhaseProductUse);

            HouseQuantitiesForView_PhaseProductUse_JobBOM = new HouseQuantitiesData(Child1_SubcomponentOptionHouseQuantities_JobBOM)
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseForView_PhaseProductUse }
            };

            //View Option/Phase/Product

            _product_optionForView_OptionPhaseProduct = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent_PIPE_31228_Automation",
                Quantities = "56.00"
            };


            productToOptionForView_OptionPhaseProduct = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionForView_OptionPhaseProduct }
            };

            productToHouseForView_OptionPhaseProduct = new ProductToOptionData(productToOptionForView_OptionPhaseProduct);

            HouseQuantitiesForView_OptionPhaseProduct_JobBOM = new HouseQuantitiesData(Child1_SubcomponentOptionHouseQuantities_JobBOM)
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseForView_OptionPhaseProduct }
            };
            //View Option/Phase/Product/Use

            _product_optionForView_OptionPhaseProductUse = new ProductData()
            {
                Name = "QA_RT_Product_Subcomponent_PIPE_31228_Automation",
                Quantities = "56.00"
            };


            productToOptionForView_OptionPhaseProductUse = new ProductToOptionData()
            {
                BuildingPhase = PHASE1_VALUE_PARENT,
                ProductList = new List<ProductData> { _product_optionForView_OptionPhaseProductUse }
            };

            productToHouseForView_OptionPhaseProductUse = new ProductToOptionData(productToOptionForView_OptionPhaseProductUse);

            HouseQuantitiesForView_OptionPhaseProductUse_JobBOM = new HouseQuantitiesData(Child1_SubcomponentOptionHouseQuantities_JobBOM)
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = CHILD1_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseForView_OptionPhaseProductUse }
            };
            //C. Verify the Product Rounding/Waste on the Job BOM page
            //a.1. Standard Rounding
            JobQuantitiesStandradRounding = new JobQuantitiesData()
            {
                Option = STANDRAD_ROUNDING_OPTION_NAME_DEFAULT,
                BuildingPhase = { BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT },
                Source = "Pipeline",
                Products = { "QA_RT_Product15_JobBOM_Automation" },
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantity = { "17.40" }
            };
            _product_StandradRounding = new ProductData()
            {
                Name = "QA_RT_Product15_JobBOM_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT
            };

            productData_StandradRounding = new ProductData()
            {
                Name = "QA_RT_Product15_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "17.40",
                Unit = "NONE",
            };

            productToOption_StandradRounding = new ProductToOptionData()
            {
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT,
                ProductList = new List<ProductData> { productData_StandradRounding }
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field

            // There is no House quantities 

            productToHouse_Option_StandradRounding = new ProductToOptionData(productToOption_StandradRounding) { ProductList = new List<ProductData> { productData_StandradRounding } };

            houseQuantities_Option_StandradRounding = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = STANDRAD_ROUNDING_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_Option_StandradRounding }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_Option_StandradRounding = new ProductData(productData_StandradRounding) { Quantities = "17.00" };
            productToHouseBOM_Option_StandradRounding = new ProductToOptionData(productToHouse_Option_StandradRounding) { ProductList = new List<ProductData> { productData_HouseBOM_Option_StandradRounding } };


            houseQuantities_Option_StandradRounding_JobBOM = new HouseQuantitiesData(houseQuantities_Option_StandradRounding)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Option_StandradRounding }
            };

            //b.1 Standard Rounding
            ProductData productData_HouseBOM_Option_StandradRounding_Negative_Value = new ProductData(productData_StandradRounding) { Quantities = "17.00" };

            productToHouseBOM_Option_StandradRounding_Negative_Value = new ProductToOptionData(productToHouse_Option_StandradRounding) { ProductList = new List<ProductData> { productData_HouseBOM_Option_StandradRounding_Negative_Value } };

            houseQuantities_Option_StandradRounding_JobBOM_Negative_Value = new HouseQuantitiesData(houseQuantities_Option_StandradRounding)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Option_StandradRounding_Negative_Value }
            };

            //a.2. Always Round-Up
            JobQuantitiesRoundUp = new JobQuantitiesData()
            {
                Option = ROUND_UP_ROUNDING_OPTION_NAME_DEFAULT,
                BuildingPhase = { BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT },
                Source = "Pipeline",
                Products = { "QA_RT_Product16_JobBOM_Automation" },
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantity = { "17.40" }
            };

            _product_RoundUp = new ProductData()
            {
                Name = "QA_RT_Product16_JobBOM_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Always Round Up",
                Waste = "0.00",
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT
            };


            productData_RoundUp = new ProductData()
            {
                Name = "QA_RT_Product16_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "17.40",
                Unit = "NONE",
            };

            productToOption_RoundUp = new ProductToOptionData()
            {
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT,
                ProductList = new List<ProductData> { productData_RoundUp }
            };

            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            // There is no House quantities 4

            productToHouse_Option_RoundUp = new ProductToOptionData(productToOption_RoundUp) { ProductList = new List<ProductData> { productData_RoundUp } };

            houseQuantities_Option_RoundUp = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = ROUND_UP_ROUNDING_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_Option_RoundUp }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_Option_RoundUp = new ProductData(productData_RoundUp) { Quantities = "18.00" };

            productToHouseBOM_Option_RoundUp = new ProductToOptionData(productToHouse_Option_RoundUp) { ProductList = new List<ProductData> { productData_HouseBOM_Option_RoundUp } };



            houseQuantities_Option_RoundUp_JobBOM = new HouseQuantitiesData(houseQuantities_Option_RoundUp)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Option_RoundUp }
            };

            //b.2. Always Round-Up
            ProductData productData_HouseBOM_Option_RoundUp_Negative_Value = new ProductData(productData_RoundUp) { Quantities = "18.00" };

            productToHouseBOM_Option_RoundUp_Negative_Value = new ProductToOptionData(productToHouse_Option_RoundUp) { ProductList = new List<ProductData> { productData_HouseBOM_Option_RoundUp_Negative_Value } };

            houseQuantities_Option_StandradRounding_JobBOM_Negative_Value = new HouseQuantitiesData(houseQuantities_Option_RoundUp)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Option_RoundUp_Negative_Value }
            };

            //a.3. Always Round Down

            JobQuantitiesRoundDown = new JobQuantitiesData()
            {
                Option = ROUND_DOWN_ROUNDING_OPTION_NAME_DEFAULT,
                BuildingPhase = { BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT },
                Source = "Pipeline",
                Products = { "QA_RT_Product17_JobBOM_Automation" },
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantity = { "17.60" }
            };

            _product_RoundDown = new ProductData()
            {
                Name = "QA_RT_Product17_JobBOM_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                Code = "",
                Description = "QA Regression Test Product - For QA Testing Only",
                Notes = "QA Testing Only",
                Unit = "BF",
                RoundingUnit = "1",
                RoundingRule = "Always Round Down",
                Waste = "0.00",
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT
            };

            productData_RoundDown = new ProductData()
            {
                Name = "QA_RT_Product17_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "17.60",
                Unit = "NONE",
            };


            productToOption_RoundDown = new ProductToOptionData()
            {
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT,
                ProductList = new List<ProductData> { productData_RoundDown }
            };


            /****************************** Create Product quantities on House ******************************/
            // There is no House quantities 4

            productToHouse_Option_RoundDown = new ProductToOptionData(productToOption_RoundDown) { ProductList = new List<ProductData> { productData_RoundDown } };


            houseQuantities_Option_RoundDown = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = ROUND_DOWN_ROUNDING_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_Option_RoundDown }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_Option_RoundDown = new ProductData(productData_RoundDown) { Quantities = "17.00" };

            productToHouseBOM_Option_RoundDown = new ProductToOptionData(productToHouse_Option_RoundDown) { ProductList = new List<ProductData> { productData_HouseBOM_Option_RoundDown } };



            houseQuantities_Option_RoundDown_JobBOM = new HouseQuantitiesData(houseQuantities_Option_RoundUp)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Option_RoundDown }
            };

            //b.2 Always Round-Down
            ProductData productData_HouseBOM_Option_RoundDown_Negative_Value = new ProductData(productData_RoundDown) { Quantities = "17.00" };

            productToHouseBOM_Option_RoundDown_Negative_Value = new ProductToOptionData(productToHouse_Option_RoundDown) { ProductList = new List<ProductData> { productData_HouseBOM_Option_RoundDown_Negative_Value } };

            houseQuantities_Option_RoundDown_JobBOM_Negative_Value = new HouseQuantitiesData(houseQuantities_Option_RoundDown)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Option_RoundDown_Negative_Value }
            };

            //2.Product Waste

            JobQuantitiesWaste = new JobQuantitiesData()
            {
                Option = WASTE_ROUNDING_OPTION_NAME_DEFAULT,
                BuildingPhase = { BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT },
                Source = "Pipeline",
                Products = { "QA_RT_Product18_JobBOM_Automation" },
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantity = { "10.00" }
            };

            _product_Waste = new ProductData()
            {
                Name = "QA_RT_Product18_JobBOM_Automation",
                Manufacture = "QA_Manu_Automation",
                Style = "QA_Style_Automation",
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT,
            };

            productData_Waste = new ProductData()
            {
                Name = "QA_RT_Product18_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "10.00",
                Unit = "NONE",
            };



            productToOption_Waste = new ProductToOptionData()
            {
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT,
                ProductList = new List<ProductData> { productData_Waste }
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field

            // There is no House quantities 4

            productToHouse_Option_Waste = new ProductToOptionData(productToOption_Waste) { ProductList = new List<ProductData> { productData_Waste } };



            houseQuantities_Option_Waste = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = WASTE_ROUNDING_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_Option_Waste }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_Option_Waste = new ProductData(productData_Waste) { Quantities = "11.00" };
            productToHouseBOM_Option_Waste = new ProductToOptionData(productToOption_Waste) { ProductList = new List<ProductData> { productData_HouseBOM_Option_Waste } };



            houseQuantities_Option_Waste_JobBOM = new HouseQuantitiesData(houseQuantities_Option_Waste)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Option_Waste }
            };


            //D. Verify BOM Adjustment functions
            //1.Adjust Quantities
            _AdjustJobBOMData = new JobBOMData()
            {
                Option = CHILD1_OPTION_NAME_DEFAULT + " -" + CHILD1_OPTION_CODE_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PARENT,
                Product = "QA_RT_Product09_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                NewQuantity = "11.00"

            };

            //2.One Time Product
            _OneTimejobBOMData = new JobBOMData()
            {
                Option = CHILD1_OPTION_NAME_DEFAULT + " -" + CHILD1_OPTION_CODE_DEFAULT,
                BuildingPhase = PHASE1_VALUE_PARENT,
                Name = "Test",
                Description = "This is Test",
                Quantity = "100.00",
                UnitCost = "$20.00",
                TaxStatus = "Phase",
                Notes = "One Time"
            };

            //H. Verify the Import Job function
            Option_Specified_expectedData_ExistBuildingPhase = new JobImportQuantitiesData()
            {
                Option = "QA_RT_OPTION-CHILD1_AUTOMATION",
                BuildingPhaseCode = BUILDING_PHASE4_CODE_DEFAULT,
                BuildingPhaseName = BUILDING_PHASE4_NAME_DEFAULT,
                ProductName = IMPORT_PRODUCT_NAME_Option_Specified,
                Quantities = 60
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            Option_Specified_productData = new ProductData()
            {
                Name = "QA_RT_Product19_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "60.00",
                Unit = "NONE",
            };
            Option_Specified_productTo = new ProductToOptionData()
            {
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT,
                ProductList = new List<ProductData> { Option_Specified_productData }
            };

            // There is no House quantities 

            Option_Specified_productToHouse = new ProductToOptionData(Option_Specified_productTo) { ProductList = new List<ProductData> { Option_Specified_productData } };

            Option_Specified_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = "QA_RT_OPTION-CHILD1_AUTOMATION",
                productToOption = new List<ProductToOptionData> { Option_Specified_productToHouse }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_Option_Specified = new ProductData(Option_Specified_productData);
            Option_Specified_productToHouseBOM = new ProductToOptionData(Option_Specified_productToHouse) { ProductList = new List<ProductData> { productData_HouseBOM_Option_Specified } };


            Option_Specified_JobQuantities_JobBOM = new HouseQuantitiesData(Option_Specified_JobQuantities)
            {
                productToOption = new List<ProductToOptionData> { Option_Specified_productToHouseBOM }
            };


            NoOption_Specified_expectedData_ExistBuildingPhase = new JobImportQuantitiesData()
            {
                Option = "Reconciled Products",
                BuildingPhaseCode = BUILDING_PHASE4_CODE_DEFAULT,
                BuildingPhaseName = BUILDING_PHASE4_NAME_DEFAULT,
                ProductName = IMPORT_PRODUCT_NAME_NoOption_Specified,
                Quantities = 40
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            NoOption_Specified_productData = new ProductData()
            {
                Name = "QA_RT_Product19_JobBOM_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "40.00",
                Unit = "NONE",
            };
            NoOption_Specified_productTo = new ProductToOptionData()
            {
                BuildingPhase = BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT,
                ProductList = new List<ProductData> { NoOption_Specified_productData }
            };

            // There is no House quantities 

            NoOption_Specified_productToHouse = new ProductToOptionData(NoOption_Specified_productTo) { ProductList = new List<ProductData> { NoOption_Specified_productData } };

            NoOption_Specified_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = "RECONCILED",
                productToOption = new List<ProductToOptionData> { NoOption_Specified_productToHouse }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_NoOption_Specified = new ProductData(NoOption_Specified_productData);
            NoOption_Specified_productToHouseBOM = new ProductToOptionData(NoOption_Specified_productToHouse) { ProductList = new List<ProductData> { NoOption_Specified_productData } };


            NoOption_Specified_JobQuantities_JobBOM = new HouseQuantitiesData(NoOption_Specified_JobQuantities)
            {
                optionName = "Reconciled Products",
                productToOption = new List<ProductToOptionData> { NoOption_Specified_productToHouseBOM }
            };


            ExtentReportsHelper.LogInformation(null, $"Open setting page, Turn OFF Sage 300 and MS NAV.");
            CommunityPage.Instance.SetSage300AndNAVStatus(false);

            ExtentReportsHelper.LogInformation(null, $"Open Lot page, verify Lot button displays or not.");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Try to open Lot URL of any community and verify Add lot button
            CommonHelper.OpenURL(BaseDashboardUrl + "/Builder/Communities/Lots.aspx?cid=1");
            if (LotPage.Instance.IsAddLotButtonDisplay() is false)
            {
                ExtentReportsHelper.LogWarning(null, $"Add lot button doesn't display to continue testing. Stop this test script.");
                Assert.Ignore("Add lot button doesn't display after set NAV and Sage 300 to Running. Stop this test script");
            }

            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter Item In Grid
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, option.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", option.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"The Option with name { option.Name} is displayed in grid.");
            }
            else
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed.</font>");
                }
                // Create Option - Click 'Save' Button
                OptionPage.Instance.AddOptionModal.AddOption(option);
                string _expectedMessage = $"Option Number is duplicated.";
                string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Option with name { option.Name} and Number {option.Number}.");
                }
                BasePage.PageLoad();
            }

            //Prepare data for Community Data
            ExtentReportsHelper.LogInformation(null, "Prepare data for Community Data.");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter community with name {communityData.Name} and create if it doesn't exist.</b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);

            }
            else
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
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
                ExtentReportsHelper.LogInformation(null, "Open Import page.");
                CommonHelper.SwitchLastestTab();
                LotPage.Instance.ImportExporFromMoreMenu("Import");
                string importFileDir = "Pipeline_Lots_In_Community.csv";
                LotPage.Instance.ImportFile("Lot Import", $"\\DataInputFiles\\Import\\PIPE_31228\\{importFileDir}");
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
            ExtentReportsHelper.LogInformation(null, "Prepare data for House Data.");
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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Manufacturer to import Product.");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers, true, true);
            CommonHelper.SwitchLastestTab();

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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Style to import Product.");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
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
            ExtentReportsHelper.LogInformation(null, "Prepare a new Building Group to import Product.");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "31288",
                Name = "QA_RT_Building_Group_PIPE_31288_Automation"
            };

            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_31228\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(BUILDING_GROUP_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_31228\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(PRODUCT_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();

        }
        [Test]
        [Category("Section_V")]
        public void A5_PipelineBOM_Generate_A_Job_BOM()
        {

            //1.Global option
            //1.Navigation to http://betaautomated.bimpipeline.com/Dashboard/Login.aspx  , select ALL JOBS from JOBS dropdown list on Main menu
            ExtentReportsHelper.LogInformation(null, "Config Setting Page");
            // Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A1: 'Generate All' Job BOM with Options : Global option</b></font>");
            ExtentReportsHelper.LogInformation(null, "Step A1.1: Go to Option page And Create Global Option ");
            // Go to Option page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_global.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_global.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_global);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_global.Name);
            }

            //Navigate to Option Assignments
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A1.2: Navigate to Option Assignments: Global option</b></font>");
            OptionDetailPage.Instance.LeftMenuNavigation("Assignments");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A1.3: Assign House to this Global Option: Global option</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName) is false)
            {
                // Add House
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(HouseData.PlanNumber + "-"+HouseData.HouseName);
                string expectedHouseMsg = "House(s) added to house successfully";
                if (expectedHouseMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
                //AssignmentDetailPage.Instance.CloseAddHouseModal();
                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName))
                {
                    ExtentReportsHelper.LogFail($"House with name {HouseData.HouseName} is NOT add to this Option successfully");
                    //Assert.Fail();
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A1.4: Assign Community to this Global Option</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                //Assign Community to this Global Option
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITY_NAME_DEFAULT);
                string expectedCommunityMsg = "Option(s) added to community successfully";
                if (expectedCommunityMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                }
                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITY_NAME_DEFAULT} is NOT add to this Option successfully");
                }
            }


            //Navigate to Jobs menu > All Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A1.5: Navigate to Jobs menu > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }

            JobPage.Instance.CreateJob(jobData);
            string JobDetail_url = JobDetailPage.Instance.CurrentURL;

            //Check Header in BreadsCrumb 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A1.6: Check Header in BreadsCrumb .</b></font>");
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(jobData.Name) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The Header is present correctly.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Header is present incorrectly.</font>");
            }

            // Step 2: Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A1.7: Switch to Job/ Options page. Add Option '{GLOBAL_OPTION_NAME_DEFAULT}' to job if it doesn't exist.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);
            //Get the url of Job Options
            string JobOptions_url = JobOptionPage.Instance.CurrentURL;
            if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", GLOBAL_OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = GLOBAL_OPTION_CODE_DEFAULT + "-" + GLOBAL_OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", "BASE") is true)
                {
                    JobOptionPage.Instance.DeleteItemInGrid("Option Name", "BASE");
                }
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }

            // Switch to Job/ Quantities page. Apply System Quantities.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A1.8: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(jobQuantities_Global);
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(houseQuantities_OptionGlobal);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A1.9 :Switch to Job/ Job BOM page. Generate Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");

            JobBOMPage.Instance.VerifyJobBomPageIsDisplayed("Generated BOMs");

            //Generate Job BOM With Global Option
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A1.10 :Generate Job BOM With Global Option.</b></font>");
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);

            //Verify Item In Job BOM With Global Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A1.11 : Verify Item In Job BOM With Global Option.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_OptionGlobal_JobBOM);


            //2. Option/phase bid with no products
            ExtentReportsHelper.LogInformation(null, "*********** Go to the Option Default page and Navigate to Option Details Page ***********");

            // Go to Option page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A2.1 : Go to Option page and create option/phase bid with no products</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_phasebid1.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_phasebid1.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_phasebid1);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_phasebid1.Name);
            }

            //Navigate to Option Assignments
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A2.2 : Navigate to Option Assignments</b></font>");
            OptionDetailPage.Instance.LeftMenuNavigation("Assignments");

            //Assign House to this Global Option: Option with no products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A2.3 : Assign House to this Phasebid1 Option: Option with no products</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName) is false)
            {
                // Add House
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(HouseData.PlanNumber + "-" + HouseData.HouseName);
                string expectedHouseMsg = "House(s) added to house successfully";
                if (expectedHouseMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }

                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName))
                {
                    ExtentReportsHelper.LogFail($"House with name {HouseData.HouseName} is NOT add to this Option successfully");
                    //Assert.Fail();
                }
            }

            //Assign Community to this Phasebid1 Option: Option with no products
            ExtentReportsHelper.LogInformation(null, "Step A2.4 : Assign Community to this Phasebid1 Option: Option with no products");
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                //Assign Community to this Global Option
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITY_NAME_DEFAULT);
                string expectedCommunityMsg = "Option(s) added to community successfully";
                if (expectedCommunityMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                }
                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITY_NAME_DEFAULT} is NOT add to this Option successfully");
                }
            }

            //Navigate to Option Bid Costs
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A2.5 :Switch to Option/ Bid Costs page. Add a new Bid costs if it does NOT exist on phase '{PHASEBID1_VALUE}'.</b></font>");
            AssignmentDetailPage.Instance.LeftMenuNavigation("Bid Costs");
            if (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASEBID1_VALUE) is false)
            {
                // Add a new Bid costs if it does NOT exist
                BidCostsToOptionPage.Instance.AddOptionBuildingPhase(optionBuildingPhaseBid1Data);
            }

            //Navigate Job Options Page
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A2.6:/Navigate Job Options Page.</b></font>");
            CommonHelper.OpenURL(JobOptions_url);

            if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", PHASEBID1_OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = PHASEBID1_OPTION_CODE_DEFAULT + "-" + PHASEBID1_OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", "BASE") is true)
                {
                    JobOptionPage.Instance.DeleteItemInGrid("Option Name", "BASE");
                }
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }


            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A2.7 :Switch to Job/ Job BOM page. Generate Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");

            JobBOMPage.Instance.VerifyJobBomPageIsDisplayed("Generated BOMs");
            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A2.8 : Verify Item In Job BOM With Phasbid Option With No Product.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(PhaseBid1OptionHouseQuantities_JobBOM, false);

            //3. Option/phase bid with regular products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A3.1: Go to Option page and create option/phase bid with regular products</b></font>");
            // Go to Option page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_phasebid2.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_phasebid2.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_phasebid2);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_phasebid2.Name);
            }

            //Navigate to Option Assignments
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A3.2: Navigate to Option Assignments.</b></font>");
            OptionDetailPage.Instance.LeftMenuNavigation("Assignments");

            //Assign House to this Phasebid1 Option: Phasebid Option with regular products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A3.3 : Assign House to this Phasebid1 Option: Phasebid Option with regular products</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName) is false)
            {
                // Add House
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(HouseData.PlanNumber + "-" + HouseData.HouseName);
                string expectedHouseMsg = "House(s) added to house successfully";
                if (expectedHouseMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
                //AssignmentDetailPage.Instance.CloseAddHouseModal();
                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName))
                {
                    ExtentReportsHelper.LogFail($"House with name {HouseData.HouseName} is NOT add to this Option successfully");
                    //Assert.Fail();
                }
            }
            //Assign Community to this Phasebid1 Option: Phasebid Option with regular products
            ExtentReportsHelper.LogInformation(null, "Step A3.4: Assign Community to this Phasebid2 Option: Phasebid Option with regular products");
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                //Assign Community to this Global Option
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITY_NAME_DEFAULT);
                string expectedCommunityMsg = "Option(s) added to community successfully";
                if (expectedCommunityMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                }
                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITY_NAME_DEFAULT} is NOT add to this Option successfully");
                }
            }

            //Navigate To Option Product
            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A3.5:  Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASEBID1_VALUE}'.</b></font>");

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE1_VALUE_PHASEBID2) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionPhaseBid2OptionQuantitiesData);
            }

            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Building Phase", PHASE2_VALUE_PHASEBID2) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionPhaseBid2HouseOptionQuantitiesData);
            }

            //Navigate to Option Bid Costs
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A3.6: Switch to Option/ Bid Costs page. Add a new Bid costs if it does NOT exist on phase '{PHASEBID1_VALUE}'.</b></font>");
            ProductsToOptionPage.Instance.LeftMenuNavigation("Bid Costs");
            if (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASE2_VALUE_PHASEBID2) is false)
            {
                // Add a new Bid costs if it does NOT exist
                BidCostsToOptionPage.Instance.AddOptionBuildingPhase(optionBuildingPhaseBid2Data);
            }


            //Navigate Job Options Page
            CommonHelper.OpenURL(JobDetail_url);
            // Step 2: Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A3.7: Switch to Job/ Options page. Add Option '{GLOBAL_OPTION_NAME_DEFAULT}' to job if it doesn't exist.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");

            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", PHASEBID2_OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = PHASEBID2_OPTION_CODE_DEFAULT + "-" + PHASEBID2_OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", "BASE") is true)
                {
                    JobOptionPage.Instance.DeleteItemInGrid("Option Name", "BASE");
                }
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }

            // Switch to Job/ Quantities page. Apply System Quantities.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A3.7: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");
            //Go to Job option and Job quantities
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantities_PhaseBid2);

            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(PhaseBidOption2HouseQuantities_JobBOM);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A3.8: Switch to Job/ Job BOM page. Generate Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");
            JobBOMPage.Instance.VerifyJobBomPageIsDisplayed("Generated BOMs");

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A3.9 : Verify Item In Job BOM With Phasbid Option With Regular Products.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(PhaseBidOption2HouseQuantities_JobBOM);


            //4.Option/phase bid with supplemental products
            ExtentReportsHelper.LogInformation(null, "Step A4.1: Go to Option page and create option/phase bid with supplemental products");
            // Go to Option page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_phasebid3.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_phasebid3.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_phasebid3);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_phasebid3.Name);
            }

            //Navigate to Option Assignments
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A4.2: Navigate to Option Assignments.</b></font>");

            //Add House Name Into Option
            OptionDetailPage.Instance.LeftMenuNavigation("Assignments");
            //Assign House to this Phasebid1 Option: Phasebid Option with supplemental products
            ExtentReportsHelper.LogInformation(null, "Step A4.3 : Assign House to this Phasebid3 Option: Phasebid Option with supplemental products");
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName) is false)
            {
                // Add House
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(HouseData.PlanNumber + "-" + HouseData.HouseName);
                string expectedHouseMsg = "House(s) added to house successfully";
                if (expectedHouseMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
                //AssignmentDetailPage.Instance.CloseAddHouseModal();
                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName))
                {
                    ExtentReportsHelper.LogFail($"House with name {HouseData.HouseName} is NOT add to this Option successfully");
                    //Assert.Fail();
                }
            }
            //Add Community Into Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A4.4 : Assign Community to this Phasebid3 Option: Phasebid Option with supplemental products</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                //Assign Community to this  Option
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITY_NAME_DEFAULT);
                string expectedCommunityMsg = "Option(s) added to community successfully";
                if (expectedCommunityMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                }
                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITY_NAME_DEFAULT} is NOT add to this Option successfully");
                }
            }

            //Navigate To Option Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A4.5 : Assign Product to this Phasebid3 Option: Phasebid Option with supplemental products</b></font>");
            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASEBID1_VALUE}'.</b></font>");

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE1_VALUE_PHASEBID3) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionPhaseBid3OptionQuantitiesData);
            }

            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Building Phase", PHASE1_VALUE_PHASEBID3) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionPhaseBid3HouseOptionQuantitiesData);
            }

            //Navigate to Option Bid Costs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A4.5 : Assign Product to this Phasebid3 Option: Phasebid Option with supplemental products</b></font>");
            ProductsToOptionPage.Instance.LeftMenuNavigation("Bid Costs");
            if (BidCostsToOptionPage.Instance.IsOptionBuildingPhaseInGrid("Building Phase", PHASE1_VALUE_PHASEBID3) is false)
            {
                // Add a new Bid costs if it does NOT exist
                BidCostsToOptionPage.Instance.AddOptionBuildingPhase(optionBuildingPhaseBid3Data);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data for Community Data.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A4.6: Prepare data for Community Data</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Step A4.7: Filter community with name {communityData.Name} and create if it doesn't exist.</b></font>");
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
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Step A4.7: Add Option with supplemental products into Community.</b></font>");
            CommunityDetailPage.Instance.LeftMenuNavigation("Options");
            CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.Contains, PHASEBID3_OPTION_NAME_DEFAULT);
            if (!CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", PHASEBID3_OPTION_NAME_DEFAULT))
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData);
            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A4.7: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASEBID1_VALUE}'.</b></font>");
            if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesPhaseBid3Data.BuildingPhase, communityQuantitiesPhaseBid3Data.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesPhaseBid3Data);
            }

            //Navigate Job Options Page
            CommonHelper.OpenURL(JobDetail_url);
            // Step 2: Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A4.8: Switch to Job/ Options page. Add Option '{PHASEBID3_OPTION_NAME_DEFAULT}' to job if it doesn't exist.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", PHASEBID3_OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = PHASEBID3_OPTION_CODE_DEFAULT + "-" + PHASEBID3_OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", "BASE") is true)
                {
                    JobOptionPage.Instance.DeleteItemInGrid("Option Name", "BASE");
                }
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }

            // Switch to Job/ Quantities page. Apply System Quantities.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A4.9: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantities_PhaseBid3);

            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(PhaseBidOption3HouseQuantities);


            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A4.10: Switch to Job/ Job BOM page. Generate Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");
            JobBOMPage.Instance.VerifyJobBomPageIsDisplayed("Generated BOMs");

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            // Verify Supplemental products on the Job grid view
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A4.10: Verify Supplemental products on the grid view.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>On Job Side Navigation, click the Job BOM to open the Job BOM page.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(PhaseBidOption3HouseQuantities_JobBOM, false);

            JobBOMPage.Instance.VerifySupplementalByPhase(PhaseBidOption3HouseQuantities_JobBOM);

            //5.Parent/child option
            ExtentReportsHelper.LogInformation(null, "Step A5.1: Go to Option page and create Parent/child option");
            //Create New Option Child1
            ExtentReportsHelper.LogInformation(null, " Go to Option page and create child option");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_child1.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_child1.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_child1);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_child1.Name);
            }

            //Navigate to Option Assignments
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.2: Navigate to Option Assignments.</b></font>");
            OptionDetailPage.Instance.LeftMenuNavigation("Assignments");

            //Assign House to this Child1 Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.3 : Assign House to this Child1 Option</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName) is false)
            {
                // Add House
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(HouseData.PlanNumber + "-" + HouseData.HouseName);
                string expectedHouseMsg = "House(s) added to house successfully";
                if (expectedHouseMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }

                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName))
                {
                    ExtentReportsHelper.LogFail($"House with name {HouseData.HouseName} is NOT add to this Option successfully");
                    //Assert.Fail();
                }
            }

            //Add Community Into Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.4: Assign Community to this Child1 Option</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                //Assign Community to this Global Option
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITY_NAME_DEFAULT);
                string expectedCommunityMsg = "Option(s) added to community successfully";
                if (expectedCommunityMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                }
                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITY_NAME_DEFAULT} is NOT add to this Option successfully");
                }
            }


            //Create New Option Child2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.5: Go to Option page and create child2 option</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_child2.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_child2.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_child2);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_child2.Name);
            }


            //Navigate to Option Assignments
            OptionDetailPage.Instance.LeftMenuNavigation("Assignments");
            //Assign House to this Child2 Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.6: Assign House to this Child2 Option</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName) is false)
            {
                // Add House
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(HouseData.PlanNumber + "-" + HouseData.HouseName);
                string expectedHouseMsg = "House(s) added to house successfully";
                if (expectedHouseMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }

                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName))
                {
                    ExtentReportsHelper.LogFail($"House with name {HouseData.HouseName} is NOT add to this Option successfully");
                    //Assert.Fail();
                }
            }

            //Add Community Into Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.6: Assign Community to this Child2 Option</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                //Assign Community to this Global Option
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITY_NAME_DEFAULT);
                string expectedCommunityMsg = "Option(s) added to community successfully";
                if (expectedCommunityMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                }
                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITY_NAME_DEFAULT} is NOT add to this Option successfully");
                }
            }


            // Go to Option page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.5: Go to Option page and create parent option</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_parent.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_parent.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_parent);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_parent.Name);
            }

            //Navigate to Option Assignments
            OptionDetailPage.Instance.LeftMenuNavigation("Assignments");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.7: Assign House to this parent Option</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName) is false)
            {
                // Add House
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(HouseData.PlanNumber + "-" + HouseData.HouseName);
                string expectedHouseMsg = "House(s) added to house successfully";
                if (expectedHouseMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HouseData.HouseName))
                {
                    ExtentReportsHelper.LogFail($"House with name {HouseData.HouseName} is NOT add to this Option successfully");
                }
            }

            //Add Community Into Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.8: Assign Community to this parent Option</b></font>");
            if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                //Assign Community to this Global Option
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITY_NAME_DEFAULT);
                string expectedCommunityMsg = "Option(s) added to community successfully";
                if (expectedCommunityMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                }
                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITY_NAME_DEFAULT))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITY_NAME_DEFAULT} is NOT add to this Option successfully");
                }
            }

            //Add Child1 option Into Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.9: Assign Child1 option to this parent Option</b></font>");

            if (AssignmentDetailPage.Instance.IsItemInProductGrid("Name", CHILD1_OPTION_NAME_DEFAULT) is false)
            {
                // Add Product Quantity Options
                AssignmentDetailPage.Instance.ClickAddProductQuantityOptionToShowModal().AddProductQuantityToOption(CHILD1_OPTION_NAME_DEFAULT);
                string expectedMsg = "Product Quantity Child Option(s) successfully added";
                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Product Quantity Child Option(s) successfully added");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
            }

            //Add Child1 option Into Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.10: Assign Child2 option to this parent Option</b></font>");

            if (AssignmentDetailPage.Instance.IsItemInProductGrid("Name", CHILD2_OPTION_NAME_DEFAULT) is false)
            {
                // Add Product Quantity Options
                AssignmentDetailPage.Instance.ClickAddProductQuantityOptionToShowModal().AddProductQuantityToOption(CHILD2_OPTION_NAME_DEFAULT);
                string expectedMsg = "Product Quantity Child Option(s) successfully added";
                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Product Quantity Child Option(s) successfully added");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
            }

            //Navigate To Option Product
            AssignmentDetailPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A5.11: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE1_VALUE_PARENT}'.</b></font>");

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE1_VALUE_PARENT) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionParentOptionQuantitiesData);
            }

            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Products", "QA_RT_Product10_JobBOM_Automation") is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionParentHouseOptionQuantitiesData);
            }


            //Create New Option Child1
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.12: Create New Option Child1</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_child1.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_child1.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_child1);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_child1.Name);
            }

            //Navigate To Option Product
            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A5.13: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE1_VALUE_PARENT}'.</b></font>");

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", optionChild1Option1QuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionChild1Option1QuantitiesData);
            }

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", optionChild1Option2QuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionChild1Option2QuantitiesData);
            }


            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Products", optionChild1HouseOption1QuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionChild1HouseOption1QuantitiesData);
            }

            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Products", optionChild1HouseOption2QuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionChild1HouseOption2QuantitiesData);
            }


            //Create New Option Child2
            ExtentReportsHelper.LogInformation(null, "Step A5.12: Create New Option Child2");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option_child2.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option_child2.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option_child2);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option_child2.Name);
            }


            //Navigate To Option Product
            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A5.13: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE1_VALUE_PARENT}'.</b></font>");

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", optionChild2Option1QuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionChild2Option1QuantitiesData);
            }
            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", optionChild2Option2QuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(optionChild2Option2QuantitiesData);
            }

            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Products", optionChild2HouseOption1QuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionChild2HouseOption1QuantitiesData);
            }

            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Products", optionChild2HouseOption2QuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionChild2HouseOption2QuantitiesData);
            }
            

            //Navigate Job Options Page
            //Navigate to Jobs menu > All Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.14: Navigate to Jobs menu > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a new Job.</font>");
                JobPage.Instance.CreateJob(jobData);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"The Job {jobData.Name} is exited");
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
            }

            // Step 2: Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A5.15: Switch to Job/ Options page. Add Option '{PARENT_OPTION_NAME_DEFAULT}' , {CHILD1_OPTION_NAME_DEFAULT}, {CHILD1_OPTION_NAME_DEFAULT} to job if it doesn't exist.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);


            if (JobOptionPage.Instance.IsOptionCardDisplayed is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", PARENT_OPTION_NAME_DEFAULT) is false
            && JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", CHILD1_OPTION_NAME_DEFAULT) is false
            && JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", CHILD2_OPTION_NAME_DEFAULT) is false)
            {

                string selectedOptionParent = PARENT_OPTION_CODE_DEFAULT + "-" + PARENT_OPTION_NAME_DEFAULT;
                string selectedOptionChild1 = CHILD1_OPTION_CODE_DEFAULT + "-" + CHILD1_OPTION_NAME_DEFAULT;
                string selectedOptionChild2 = CHILD2_OPTION_CODE_DEFAULT + "-" + CHILD2_OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOptionParent} && {selectedOptionChild1} && {selectedOptionChild2}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOptionParent, selectedOptionChild1, selectedOptionChild2);
                if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", "BASE") is true)
                {
                    JobOptionPage.Instance.DeleteItemInGrid("Option Name", "BASE");
                }
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }


            // Switch to Job/ Quantities page.Apply System Quantities.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A5.16: Switch to Job/ Quantities page. Apply System Quantities.</b></font>");
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");
            //Go to Job option and Job quantities
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantities_Parent);
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.17: Verify Parent Option Job Quantities In Grid.</b></font>");
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(ParentOptionHouseQuantities);
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.18: Verify Child1 Option Job Quantities In Grid.</b></font>");
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Child1OptionHouseQuantities);
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.19: Verify Child2 Option Job Quantities In Grid.</b></font>");
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Child2OptionHouseQuantities);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A5.20: Switch to Job/ Job BOM page. Generate Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");
            JobBOMPage.Instance.VerifyJobBomPageIsDisplayed("Generated BOMs");

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            // Verify Supplemental products on the Job grid view
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A5.21: Switch Job Bom View.</b></font>");

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.22: Verify Parent Option Job BOM In Grid.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Child1OptionHouseQuantities_JobBOM);
            CommonHelper.RefreshPage();
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.23: Verify Parent Option Job BOM In Grid.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Child2OptionHouseQuantities_JobBOM);
            CommonHelper.RefreshPage();
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A5.24: Verify Parent Option Job BOM In Grid.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(ParentOptionHouseQuantities_JobBOM);

            //6.Option with products assigned to a Spec Set (Use Job BOM result at TC 5 to continue)
            //Set up 
            //Navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Products/Products/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _productspecset.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _productspecset.Name) is false)
            {

                //Add button and Populate all values and save new product
                ProductPage.Instance.ClickAddToProductIcon();
                string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
                Assert.That(ProductDetailPage.Instance.IsPageDisplayed(expectedURL), "Product detail page isn't displayed");

                ExtentReportsHelper.LogInformation("Populate all values and save new product");
                // Select the 'Save' button on the modal;
                getNewproductspecset = ProductDetailPage.Instance.CreateAProduct(_productspecset);

                // Verify new Product in header
                Assert.That(ProductDetailPage.Instance.IsCreateSuccessfully(getNewproductspecset), "Create new Product unsuccessfully");
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</Cb></font>");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A6.1: Option with products assigned to a Spec Set (Use Job BOM result at TC 5 to continue)</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A6.1:Back to Spec Set page And Create New SpecSet.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", _specsetGroup1.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", _specsetGroup1.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b> {_specsetGroup1.GroupName} is exited in grid.</b></font>");
                SpecSetPage.Instance.FindItemInGridWithTextContains("Name", _specsetGroup1.GroupName);
                SpecSetPage.Instance.DeleteItemInGrid("Name", _specsetGroup1.GroupName);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Create new Spec Set group.</b></font>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(_specsetGroup1.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", _specsetGroup1.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", _specsetGroup1.GroupName);

            string url = SpecSetDetailPage.Instance.CurrentURL;
            CommonHelper.OpenURL(url);
            // Add the spec set

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.2: Add > Verify the spec set :{_specsetGroup1.SpectSetName} is added successfully</b></font>");
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            Assert.That(SpecSetDetailPage.Instance.IsModalDisplayed(), "The add new spect set modal is not displayed");
            SpecSetDetailPage.Instance.CreateNewSpecSet(_specsetGroup1.SpectSetName);

            // Expand all
            SpecSetDetailPage.Instance.ExpandAndCollapseSpecSet(_specsetGroup1.SpectSetName, "Expand");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.3: Add Product Conversion Into SpecSet :{_specsetGroup1.SpectSetName}</b></font>");
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(_specsetGroup1);
            if ($"Created Spec Set Product ({_specsetGroup1.OriginalProduct}) In Spec Set ({_specsetGroup1.SpectSetName})" != SpecSetDetailPage.Instance.GetLastestToastMessage())
                Console.WriteLine(SpecSetDetailPage.Instance.GetLastestToastMessage());
            ExtentReportsHelper.LogInformation("Created the Product Conversation in Spec Set.");
            //Collapse Spec Set
            SpecSetDetailPage.Instance.ExpandAndCollapseSpecSet(_specsetGroup1.SpectSetName, "Collapse");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.4: Add > Verify the spec set :{_specsetGroup2.SpectSetName} is added successfully</b></font>");
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            Assert.That(SpecSetDetailPage.Instance.IsModalDisplayed(), "The add new spect set modal is not displayed");
            SpecSetDetailPage.Instance.CreateNewSpecSet(_specsetGroup2.SpectSetName);

            // Expand all
            SpecSetDetailPage.Instance.ExpandAndCollapseSpecSet(_specsetGroup2.SpectSetName, "Expand");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.5: Add Product Conversion Into SpecSet :{_specsetGroup2.SpectSetName}</b></font>");
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategoryInSpecSet2(_specsetGroup2);
            if ($"Created Spec Set Product ({_specsetGroup2.OriginalProduct}) In Spec Set ({_specsetGroup2.SpectSetName})" != SpecSetDetailPage.Instance.GetLastestToastMessage())
                Console.WriteLine(SpecSetDetailPage.Instance.GetLastestToastMessage());
            ExtentReportsHelper.LogInformation("Created the Product Conversation in Spec Set.");

            //Collapse Spec Set
            SpecSetDetailPage.Instance.ExpandAndCollapseSpecSet(_specsetGroup2.SpectSetName, "Collapse");

            //Navigate Job Options Page
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.6: Navigate Job Options Page</b></font>");
            CommonHelper.OpenURL(JobDetail_url);
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            //Delete Product Quantities in grid
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.7: Switch to Job/ Quantities page. Delete Product Quantities in grid</b></font>");
            JobQuantitiesPage.Instance.DeleteQuantities("Pipeline");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.8: Switch to Job/ Quantities page. Add Product Quantities in grid</b></font>");
            //Add Product Quantities
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantities_Child1);
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantities_Child2);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.9: Switch to Job/ Quantities page. Apply System Quantities</b></font>");
            //Delete Product Quantities in grid
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            // Verify Product Quantities is displayed in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step A6.10: Verify Specset Option Job Quantities In Grid.</b></font>");
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Child1_SpecSetOptionHouseQuantities_JobBOM);

            CommonHelper.RefreshPage();
            // Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Child2_SpecSetOptionHouseQuantities_JobBOM);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.11: Switch to Job/ Job BOM page. Generate Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");
            JobBOMPage.Instance.VerifyJobBomPageIsDisplayed("Generated BOMs");

            string JobBOM_url = JobBOMPage.Instance.CurrentURL;

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);

            //Verify SpecSet Option Job BOM In Grid
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step A6.12: Verify SpecSet Option Job BOM In Grid.</b></font>");

            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Child1_SpecSetOptionHouseQuantities_JobBOM);

            CommonHelper.RefreshPage();
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Child2_SpecSetOptionHouseQuantities_JobBOM);


            //Show Category on Add Spec Set Product Conversion Modal - TURN OFF
            // Navigate setting/product
            ExtentReportsHelper.LogInformation(null, "<b> Show Category on Add Spec Set Product Conversion Modal - TURN OFF </b>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);

            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(false);

            //Navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Products/Products/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _productsubcomponent.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _productsubcomponent.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _productsubcomponent.Name);
            }
            else
            {

                //Add button and Populate all values and save new product
                ProductPage.Instance.ClickAddToProductIcon();
                string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_PRODUCT_URL;
                Assert.That(ProductDetailPage.Instance.IsPageDisplayed(expectedURL), "Product detail page isn't displayed");

                ExtentReportsHelper.LogInformation("Populate all values and save new product");
                // Select the 'Save' button on the modal;
                getNewproductsubcomponent = ProductDetailPage.Instance.CreateAProduct(_productsubcomponent);

                // Verify new Product in header
                Assert.That(ProductDetailPage.Instance.IsCreateSuccessfully(getNewproductsubcomponent), "Create new Product unsuccessfully");
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Create successful Product.</b></font>");
            }

            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //Navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Products/Products/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to product detail/ Subcomponents > Click on the “Copy Subcomponent> Select the data and check.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a subcomponent inside a product, remember to add dependent Option above, and check result.</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _productspecset.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _productspecset.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _productspecset.Name);
            }

            //Navigate To Subcomponents
            ExtentReportsHelper.LogInformation(null, "<b> Navigate To Subcomponents</b>");
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

            //Create a subcomponent inside a product, remember to add dependent Option above, and check result
            //Add subcomponent with type is Basic 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add subcomponent with type is Basic</b></font color>");

            // Click add subcomponent
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(_productspecset.BuildingPhase)
                                            .SelectStyleOfProduct(_productspecset.Style)
                                            .SelectChildBuildingPhaseOfSubComponent(BUILDINGPHASE_SUBCOMPONENT_DEFAULT)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT_SUBCOMPONENT_NAME_DEFAULT)
                                            .SelectChildStyleOfSubComponent(STYLE_NAME_DEFAULT)
                                            .SelectCalculationSubcomponent("NONE")
                                            .ClickSaveProductSubcomponent();

            //Verify add subcomponent
            string expectedMess = "Successfully added new subcomponent!";
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BUILDINGPHASE_SUBCOMPONENT_DEFAULT);

            //Navigate to JOB BOM And Verify Product Subcomponent 
            CommonHelper.OpenURL(JobBOM_url);
            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();


            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>On Job Side Navigation, click the Job BOM to open the Job BOM page.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Child1_SubcomponentOptionHouseQuantities_JobBOM);
            CommonHelper.RefreshPage();
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Child2_SubcomponentOptionHouseQuantities_JobBOM);

            //B.Verify all view by dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step B: Verify all view by dropdown list.</b></font>");
            // View Phase/Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step B.1:View Phase/Product.</b></font>");
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyViewByDropDownList(HouseQuantitiesForView_PhaseProduct_JobBOM, JOB_BOM_VIEW_PHASE_PRODUCT);


            // View Phase/Product/Use
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step B.2: View Phase/Product/Use.</b></font>");
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_PRODUCT_USE);
            JobBOMPage.Instance.VerifyViewByDropDownList(HouseQuantitiesForView_PhaseProductUse_JobBOM, JOB_BOM_VIEW_PHASE_PRODUCT_USE);

            // View Option/Phase/Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step B.3: View Phase/Product/Use.</b></font>");
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyViewByDropDownList(HouseQuantitiesForView_OptionPhaseProduct_JobBOM, JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);

            // View Option/Phase/Product/Use
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step B.4: View Phase/Product/Use.</b></font>");
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT_USE);
            JobBOMPage.Instance.VerifyViewByDropDownList(HouseQuantitiesForView_OptionPhaseProductUse_JobBOM, JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT_USE);
            //C. Verify the Product Rounding/Waste on the Job BOM page
            //1.Product Rounding
            //With RoundUpNegativeValuesToZero == true(with the new setting)
            //a.Positive values
            //a.1.Standard Rounding

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>With RoundUpNegativeValuesToZero == true(with the new setting).</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.RoundNegativeValuesTowardsZero(false);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>C.a.1.Standard Rounding.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT_NAME_STANDRAD_ROUNDING);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT_NAME_STANDRAD_ROUNDING) is false)
            {
                // Create new Product
                ProductPage.Instance.ClickAddToProductIcon();
                ProductDetailPage.Instance.CreateAProduct(_product_StandradRounding);
            }

            //Navigate Job BOM and verify it
            CommonHelper.OpenURL(JobDetail_url);
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");
            //Go to Job option and Job quantities
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantitiesStandradRounding);


            //Verify Product Quantities is displayed in grid
            ExtentReportsHelper.LogInformation(null, "Verify Product Quantities is displayed in grid");
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.UpdateJobQuantities(houseQuantities_Option_StandradRounding);
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(houseQuantities_Option_StandradRounding);

            // Navigate Job BOM and verify it
            JobOptionPage.Instance.LeftMenuNavigation("Job BOM");

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_Option_StandradRounding_JobBOM);


            //a.2.Always Round-Up
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>C.a.2.Always Round-Up.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT_NAME_ROUND_UP);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT_NAME_ROUND_UP) is false)
            {
                // Create new Product
                ProductPage.Instance.ClickAddToProductIcon();
                ProductDetailPage.Instance.CreateAProduct(_product_RoundUp);
            }

            CommonHelper.OpenURL(JobDetail_url);
            // Navigate Job BOM and verify it
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            //Go to Job option and Job quantities
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantitiesRoundUp);

            //Verify Product Quantities is displayed in grid
            ExtentReportsHelper.LogInformation(null, "Verify Product Quantities is displayed in grid");
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.UpdateJobQuantities(houseQuantities_Option_RoundUp);
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(houseQuantities_Option_RoundUp);

            // Navigate Job BOM and verify it
            JobOptionPage.Instance.LeftMenuNavigation("Job BOM");
            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_Option_RoundUp_JobBOM);


            //a.3.Always Round Down
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>C.a.3.Always Round Down.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, PRODUCT_NAME_ROUND_DOWN);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT_NAME_ROUND_DOWN) is false)
            {
                // Create new Product
                ProductPage.Instance.ClickAddToProductIcon();
                ProductDetailPage.Instance.CreateAProduct(_product_RoundDown);
            }

            // Navigate Job BOM and verify it
            CommonHelper.OpenURL(JobDetail_url);
            // Navigate Job BOM and verify it
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            //Go to Job option and Job quantities
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantitiesRoundDown);

            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.UpdateJobQuantities(houseQuantities_Option_RoundDown);
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(houseQuantities_Option_RoundDown);

            // Switch Job Bom View

            // Navigate 
            JobOptionPage.Instance.LeftMenuNavigation("Job BOM");
            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_Option_RoundDown_JobBOM);


            // Navigate Job BOM and verify it
            // b.Negative value
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step B:Navigate to Settings > Group by Parameters settings is turned on.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.RoundNegativeValuesTowardsZero(true);


            //B1.Product Rounding
            //With RoundUpNegativeValuesToZero == true(with the new setting)
            //b.Negative value
            //b.1.Standard Rounding
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step B.1: Product Rounding With RoundUpNegativeValuesToZero == true(with the new setting).</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>B.b.1.Standard Rounding.</b></font>");
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, PRODUCT_NAME_STANDRAD_ROUNDING);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT_NAME_STANDRAD_ROUNDING) is false)
            {
                // Create new Product
                ProductPage.Instance.ClickAddToProductIcon();
                ProductDetailPage.Instance.CreateAProduct(_product_StandradRounding);
            }

            //Navigate Job BOM and verify it
            CommonHelper.OpenURL(JobDetail_url);

            // Switch Job Bom View
            JobOptionPage.Instance.LeftMenuNavigation("Job BOM");
            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_Option_StandradRounding_JobBOM_Negative_Value);


            //b.2.Always Round-Up
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>B.b.2.Always Round-Up.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, PRODUCT_NAME_ROUND_UP);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT_NAME_ROUND_UP) is false)
            {
                // Create new Product
                ProductPage.Instance.ClickAddToProductIcon();
                ProductDetailPage.Instance.CreateAProduct(_product_RoundUp);
            }

            // Navigate Job BOM and verify it
            CommonHelper.OpenURL(JobDetail_url);
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            // Switch Job Bom View
            JobOptionPage.Instance.LeftMenuNavigation("Job BOM");
            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_Option_StandradRounding_JobBOM_Negative_Value);

            //b.3.Always Round Down
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>B.b.3.Always Round Down.</b></font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);


            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, PRODUCT_NAME_ROUND_DOWN);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT_NAME_ROUND_DOWN) is false)
            {
                // Create new Product
                ProductPage.Instance.ClickAddToProductIcon();
                ProductDetailPage.Instance.CreateAProduct(_product_RoundDown);
            }

            CommonHelper.OpenURL(JobDetail_url);
            // Navigate Job BOM and verify it
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            // Switch Job Bom View
            JobOptionPage.Instance.LeftMenuNavigation("Job BOM");
            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_Option_RoundDown_JobBOM_Negative_Value);


            //2.Product Waste
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Navigate Product And Create New Product Waste.</b></font>");

            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.EqualTo, PRODUCT_NAME_WASTE);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT_NAME_WASTE) is false)
            {
                // Create new Product
                ProductPage.Instance.ClickAddToProductIcon();
                ProductDetailPage.Instance.CreateAProduct(_product_Waste);
            }


            CommonHelper.OpenURL(JobDetail_url);
            // Navigate Job BOM and verify it
            JobOptionPage.Instance.LeftMenuNavigation("Quantities");

            //Go to Job option and Job quantities
            JobQuantitiesPage.Instance.AddQuantitiesInGrid(JobQuantitiesWaste);

            //Verify Product Quantities is displayed in grid
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.UpdateJobQuantities(houseQuantities_Option_Waste);
            CommonHelper.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(houseQuantities_Option_Waste);

            // Switch Job Bom View
            JobOptionPage.Instance.LeftMenuNavigation("Job BOM");

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();

            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_Option_Waste_JobBOM);

            //D.Verify BOM Adjustment functions
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step D:Verify BOM Adjustment functions<b>");
            //D.1 Adjust Quantities

            // Switch Job Bom View
            JobOptionPage.Instance.LeftMenuNavigation("Job BOM");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step D.1: Adjust Quantities <b>");
            JobBOMPage.Instance.SelectBOMAdjustQuantities("AdjustQuantities");
            JobBOMPage.Instance.AddNewQuantityAdjustment(_AdjustJobBOMData);
            string actual_updateQuantities= JobBOMPage.Instance.GetLastestToastMessage();
            string expect_updateQuantities = "Successfully added quantity adjustment.";
            if (actual_updateQuantities.Equals(expect_updateQuantities))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Add Quantities is successfully on Addjust Quantites grid.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Add Quantities is unsuccessfully on Addjust Quantites grid . Actual Result :{actual_updateQuantities}</font>");
            }

            JobBOMPage.Instance.SelectBOMAdjustQuantities("AdjustQuantities");
            if (JobBOMPage.Instance.IsItemInAdjustQuantitiesGrid("Building Phase", BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT) is true)
            {
                JobBOMPage.Instance.DeleteItemInAdjustQuantitiesGrid("Building Phase", BUILDING_PHASE4_CODE_DEFAULT + "-" + BUILDING_PHASE4_NAME_DEFAULT);
                string actual_deleteQuantities = JobBOMPage.Instance.GetLastestToastMessage();
                string expect_deleteQuantities = "Successfully deleted quantity adjustment.";
                if (actual_deleteQuantities.Equals(expect_deleteQuantities))
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b> Delete Quantities is successfully on Addjust Quantites grid.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Delete Quantities is unsuccessfully on Addjust Quantites grid</font>");
                }
            }

            //D.2 One Time Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step D.2: One Time Product <b>");
            JobBOMPage.Instance.SelectBOMAdjustQuantities("OneTimeProduct");
            //Create a new One Time Products
            if (JobBOMPage.Instance.OneTimeItemModal.IsModalDisplayed is true)
            {
                JobBOMPage.Instance.OneTimeItemModal.AddNewOneTimeProducts(_OneTimejobBOMData);
                if (JobBOMPage.Instance.IsItemInOneTimeItemGrid("Option", CHILD1_OPTION_NAME_DEFAULT) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>One Time Item is displayed on the grid.</b></font>");

                    ExtentReportsHelper.LogInformation(null, "Edit One Time Item details is displayed on the grid");
                    JobBOMPage.Instance.EditOneTimeItem(CHILD1_OPTION_NAME_DEFAULT, "20", "20");
                    CommonHelper.CaptureScreen();

                    ExtentReportsHelper.LogInformation(null, "Delete One Time Item details is displayed on the grid");
                    JobBOMPage.Instance.DeleteItemInOneTimeItemGrid("Option", CHILD1_OPTION_NAME_DEFAULT);
                    string actual_delete = JobBOMPage.Instance.GetLastestToastMessage();
                    string expect_delete = "Successfully deleted One Time Item.";
                    if (actual_delete.Equals(expect_delete))
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b> Delete Data is successfully On One Time Item Grid.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>Delete Data is unsuccessfully On One Time Item.</font>");
                    }
                    CommonHelper.CaptureScreen();
                }
            }

            //E. Verify Job BOM Archives:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>E. Verify Job BOM Archives:<b>");
            //E.1 Job Config BOM Archive
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>E.1 Job Config BOM Archive<b>");
            JobBOMPage.Instance.SelectJobBOMArchives("Job Config BOM Archive");

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE);
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_OPTION);
            CommonHelper.SwitchTab(0);
            CommonHelper.CloseAllTabsExcludeCurrentOne();

            //E.2 Job BOM Archive 3.x
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>E.2 Job BOM Archive 3.x<b>");
            JobBOMPage.Instance.SelectJobBOMArchives("Job BOM Archive 3.x");
            JobBOMPage.Instance.SwitchBOMBasedOnView("Job Quantities");
            JobBOMPage.Instance.SwitchBOMBasedOnView("House Quantities");

            // G.Verify all export function in the 'Utilities' dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>G.Verify all export function in the 'Utilities' dropdown list.x<b>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");
            //Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.Job_BOM.ToString(), JOB_NAME_DEFAULT);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step G: Verify the Export/Import functions<b>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step G.1: Verify 'EXPORT CSV' function.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.DownloadBaseLineJobBOMFile(EXPORT_JOBBOM_TO_CSV, exportFileName + "_OptionsView");
            JobBOMPage.Instance.ExportJobBOMFile(EXPORT_JOBBOM_TO_CSV, exportFileName + "_OptionsView", 0, ExportTitleFileConstant.JOBBOM_TITLE);
            //JobBOMPage.Instance.CompareExportFile(exportFileName + "_OptionsView", TableType.CSV);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step G.2: Verify 'EXPORT XML' function.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.DownloadBaseLineJobBOMFile(EXPORT_JOBBOM_TO_XML, exportFileName + "_OptionsView_Config");
            JobBOMPage.Instance.ExportJobBOMFile(EXPORT_JOBBOM_TO_XML, exportFileName + "_OptionsView_Config", 0, ExportTitleFileConstant.JOBBOM_TITLE);
            //JobBOMPage.Instance.CompareExportFile(exportFileName + "_OptionsView_Config", TableType.XML);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step G.3: Verify 'EXPORT Excel' function.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.DownloadBaseLineJobBOMFile(EXPORT_JOBBOM_TO_EXCEL, exportFileName + "_OptionsView");
            JobBOMPage.Instance.ExportJobBOMFile(EXPORT_JOBBOM_TO_EXCEL, exportFileName + "_OptionsView", 0, ExportTitleFileConstant.JOBBOM_TITLE);
            //JobBOMPage.Instance.CompareExportFile(exportFileName + "_OptionsView", TableType.XLSX);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step G.4: Verify 'Export XML w/ Trace' function.</b></font>");
            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            JobBOMPage.Instance.DownloadBaseLineJobBOMFile(EXPORT_JOBBOM_TO_XML_WITH_TRACE, exportFileName + "_OptionsView_Config_WithTrace");
            JobBOMPage.Instance.ExportJobBOMFile(EXPORT_JOBBOM_TO_XML_WITH_TRACE, exportFileName + "_OptionsView_Config_WithTrace", 0, ExportTitleFileConstant.JOBBOM_TITLE);
            //JobBOMPage.Instance.CompareExportFile(exportFileName + "_OptionsView_Config_WithTrace", TableType.XML);

            // H.Verify the Import Job function
            JobDetailPage.Instance.LeftMenuNavigation("Import", false);
            if (JobImportPage.Instance.IsImportPageDisplayed is false)
                ExtentReportsHelper.LogFail("<font color='red'>Import Job Quantities page doesn't display or title is incorrect.</font>");
            ExtentReportsHelper.LogPass(null, "<font color='green'><b>Import Job Quantities page displays correctly.</b></font>");


            //Select Option Specified button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step H: Select Option Specified button.</b></font>");
            JobImportPage.Instance.ClickOptionSpecified();

            // Step 8: Import file and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step H: Import file with EXISTING BUILDING PHASE ON PL and verify it. -------------------------</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step H.1: Upload job quantities file again.</b></font>");
            string importFileName_OptionsSpecified = "QA_JOB_Import_Quantity_OptionsSpecified_PIPE_31228.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName_OptionsSpecified, IS_OPTION_SPECIFIED);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Expand grid view.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify a new panel display with non existing product. Continue create product.</b></font>");
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(Option_Specified_expectedData_ExistBuildingPhase);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step H.3: Click Finish Impor button. Expected: Import successfully</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step H.4: Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");
            JobImportPage.Instance.LeftMenuNavigation("Quantities");
            CommonHelper.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_JobQuantities);

            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>On Job Side Navigation, click the Job BOM to open the Job BOM page.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Option_Specified_JobQuantities_JobBOM);


            JobBOMPage.Instance.LeftMenuNavigation("Import", false);
            if (JobImportPage.Instance.IsImportPageDisplayed is false)
                ExtentReportsHelper.LogFail("<font color='red'>Import Job Quantities page doesn't display or title is incorrect.</font>");
            ExtentReportsHelper.LogPass(null, "<font color='green'><b>Import Job Quantities page displays correctly.</b></font>");

            // Select No Option Specified button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select No Option Specified button.</b></font>");
            JobImportPage.Instance.ClickNoOptionSpecified();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Upload job quantities file again.</b></font>");
            string importFileName_NoOptionsSpecified = "QA_JOB_Import_Quantity_NoOptionsSpecified_PIPE_31228.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName_NoOptionsSpecified, IS_NO_OPTION_SPECIFIED);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify a new panel display with non existing product. Continue create product.</b></font>");
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(NoOption_Specified_expectedData_ExistBuildingPhase);
                
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click Finish Impor button. Expected: Import successfully</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");
            JobImportPage.Instance.LeftMenuNavigation("Quantities");
            CommonHelper.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(NoOption_Specified_JobQuantities);

            JobImportPage.Instance.LeftMenuNavigation("Job BOM");

            //Generate JobBOM
            JobBOMPage.Instance.GenerateJobBOM();
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_PHASE_OPTION_PHASE_PRODUCT);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>On Job Side Navigation, click the Job BOM to open the Job BOM page.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(NoOption_Specified_JobQuantities_JobBOM);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }

        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Back to Spec Set page and delete it.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", _specsetGroup1.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", _specsetGroup1.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b> {_specsetGroup1.GroupName} is exited in grid.</b></font>");
                SpecSetPage.Instance.FindItemInGridWithTextContains("Name", _specsetGroup1.GroupName);
                SpecSetPage.Instance.DeleteItemInGrid("Name", _specsetGroup1.GroupName);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Subcomponent.</font>");
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _productspecset.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _productspecset.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _productspecset.Name);
                //Navigate To Subcomponents
                ExtentReportsHelper.LogInformation(null, "<b> Navigate To Subcomponents</b>");
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                //Add subcomponent with type is Basic 
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add subcomponent with type is Basic</b></font color>");

                //Delete Subcomponent before create new data
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
