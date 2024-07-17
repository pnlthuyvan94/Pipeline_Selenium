using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Import;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_VIII
{
    class UAT_HOTFIX_PIPE_35050 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VIII);
        }
        private const string JOB_BOM_VIEW_Phase_Option_Phase_Product = "Option/Phase/Product";

        private readonly string IMPORT_PRODUCT_NAME = "QA_Product_01_Automation";
        private readonly bool IS_OPTION_SPECIFIED = true;
        private readonly bool IS_NO_OPTION_SPECIFIED = false;

        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_01";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";

        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House04_Automation";

        private const string OPTION = "OPTION";
        private static string OPTION_NAME_DEFAULT = "Option_QA_RT";
        private static string OPTION_CODE_DEFAULT = "ORT";

        private static string OPTION1_NAME_DEFAULT = "Option_QA_RT1";
        private static string OPTION1_CODE_DEFAULT = "ORT1";

        private static string OPTION2_NAME_DEFAULT = "Option_QA_RT2";
        private static string OPTION2_CODE_DEFAULT = "ORT2";

        private JobData jobData;

        private OptionQuantitiesData OptionQuantitiesData;

        //I.Import Job with Option Specific. 
        //1.Import product quantities (No sourceid tag - The option has product quantities)
        //Import Job Quantities File
        private JobImportQuantitiesData expectedData_ImportProductQuantities;

        //Create Total Job Quantities 
        private ProductData Option_Specified_ImportProductQuantities_productData;
        private ProductToOptionData Option_Specified_ImportProductQuantities_productTo;
        private ProductToOptionData Option_Specified_ImportProductQuantities_productToHouse;
        private ProductToOptionData Option_Specified_ImportProductQuantities_productToHouseBOM;
        private HouseQuantitiesData Option_Specified_ImportProductQuantities_JobQuantities;
        private HouseQuantitiesData Option_Specified_ImportProductQuantities_JobQuantities_JobBOM;

        //2.Import updated product quantities (Sourceid tag)
        //Import Job Quantities File
        private JobImportQuantitiesData expectedData_ImportUpdatedProductQuantities;

        //Create Total Job Quantities 
        private ProductData Option_Specified_ImportUpdatedProductQuantities_productData;
        private ProductToOptionData Option_Specified_ImportUpdatedProductQuantities_productTo;
        private ProductToOptionData Option_Specified_ImportUpdatedProductQuantities_productToHouse;
        private ProductToOptionData Option_Specified_ImportUpdatedProductQuantities_productToHouseBOM;
        private HouseQuantitiesData Option_Specified_ImportUpdatedProductQuantities_JobQuantities;
        private HouseQuantitiesData Option_Specified_ImportUpdatedProductQuantities_JobQuantities_JobBOM;

        //3.Import product quantities with old value (Sourceid tag)
        //Import Job Quantities File
        private JobImportQuantitiesData expectedData_ImportProductQuantitiesWithOldValue;

        //Create Total Job Quantities 
        private ProductData Option_Specified_ImportProductQuantitiesWithOldValue_productData;
        private ProductToOptionData Option_Specified_ImportProductQuantitiesWithOldValue_productTo;
        private ProductToOptionData Option_Specified_ImportProductQuantitiesWithOldValue_productToHouse;
        private HouseQuantitiesData Option_Specified_ImportProductQuantitiesWithOldValue_JobQuantities;

        //4.Import the file has wrong sourceid value
        //Import Job Quantities File
        private ProductData Option_Specified_ImportTheFileHasWrongSourcedValue_productData;
        //Create Total Job Quantities 

        private ProductToOptionData Option_Specified_ImportTheFileHasWrongSourcedValue_productTo;
        private ProductToOptionData Option_Specified_ImportTheFileHasWrongSourcedValue_productToHouse;
        private HouseQuantitiesData Option_Specified_ImportTheFileHasWrongSourcedValue_JobQuantities;

        //5. Import the file has ommunity code over 128 characters

        //II.Import Job with No Option Specific
        //1.Import file with the option belongs this job and the import file have product quantities (sourceid tag)
        //Import Job Quantities File
        OptionData _option2;
        private static string PHASE_VALUE = "QA_1-QA_BuildingPhase_01_Automation";
        private OptionQuantitiesData optionPhaseOptionQuantitiesData;
        private JobImportQuantitiesData expectedData_ImportJob_No_Option_Specific1;
        private JobImportQuantitiesData expectedData_ImportJob_No_Option_Specific2;

        private JobImportQuantitiesData expectedData_ImportJob_No_Option_Specific3;
        private JobImportQuantitiesData expectedData_ImportJob_No_Option_Specific4;

        OptionData _option3;
        private static string PHASE2_VALUE = "QA_2-QA_BuildingPhase_02_Automation";
        private OptionQuantitiesData option2PhaseOptionQuantitiesData;

        //Create Total Job Quantities 
        private ProductData Option_Specified_ImportJob_No_Option_Specific1_productData;
        private ProductData Option_Specified_ImportJob_No_Option_Specific2_productData;
        private ProductData Option_Specified_ImportJob_No_Option_Specific3_productData;
        private ProductData Option_Specified_ImportJob_No_Option_Specific4_productData;

        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific1_productTo;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific2_productTo;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific3_productTo;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific4_productTo;

        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific1_productToHouse;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific2_productToHouse;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific3_productToHouse;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific4_productToHouse;

        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific1_productToHouseBOM;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific2_productToHouseBOM;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific3_productToHouseBOM;
        private ProductToOptionData Option_Specified_ImportJob_No_Option_Specific4_productToHouseBOM;

        private HouseQuantitiesData Option_Specified_ImportJob_No_Option_Specific1_JobQuantities;
        private HouseQuantitiesData Option_Specified_ImportJob_No_Option_Specific2_JobQuantities;
        private HouseQuantitiesData Option_Specified_ImportJob_No_Option_Specific3_JobQuantities;

        private HouseQuantitiesData Option_Specified_ImportJob_No_Option_Specific1_JobQuantities_JobBOM;
        private HouseQuantitiesData Option_Specified_ImportJob_No_Option_Specific2_JobQuantities_JobBOM;
        private HouseQuantitiesData Option_Specified_ImportJob_No_Option_Specific3_JobQuantities_JobBOM;

        //2. Import file with the option has product quantities but doesn’t belong this job and import file has contain this option (No sourceid tag)
        //Import Job Quantities File
        private JobImportQuantitiesData expectedData_ImportJob_No_Option2_Specific1;
        private JobImportQuantitiesData expectedData_ImportJob_No_Option2_Specific2;
        private JobImportQuantitiesData expectedData_ImportJob_No_Option2_Specific3;

        //Create Total Job Quantities 
        private ProductData Option_Specified_ImportJob_No_Option2_Specific1_productData;
        private ProductData Option_Specified_ImportJob_No_Option2_Specific2_productData;

        private ProductToOptionData Option_Specified_ImportJob_No_Option2_Specific1_productTo;
        private ProductToOptionData Option_Specified_ImportJob_No_Option2_Specific2_productTo;

        private ProductToOptionData Option_Specified_ImportJob_No_Option2_Specific1_productToHouse;
        private ProductToOptionData Option_Specified_ImportJob_No_Option2_Specific2_productToHouse;

        private HouseQuantitiesData Option_Specified_ImportJob_No_Option2_Specific1_JobQuantities;
        private HouseQuantitiesData Option_Specified_ImportJob_No_Option2_Specific2_JobQuantities;


        [SetUp]
        public void GetData()
        {
            jobData = new JobData()
            {
                Name = "RT-QA_JOB_Import_Quantity",
                Community = "Automation_01-QA_RT_Community01_Automation",
                House = "400-QA_RT_House04_Automation",
                Lot = "_111 - Sold",
                Orientation = "Left",
            };


            OptionQuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation",
                ProductName = "QA_Product_02_Automation",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "1.00",
                Source = "Pipeline"
            };

            //1. Import product quantities (No sourceid tag - The option has product quantities)
            expectedData_ImportProductQuantities = new JobImportQuantitiesData()
            {
                Option = "Option_QA_RT1",
                BuildingPhaseCode = "QA_1",
                BuildingPhaseName = "QA_BuildingPhase_01_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Future = "100.00",
                Now = "0.00"
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            Option_Specified_ImportProductQuantities_productData = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "NONE",
                Use = "NONE",
                Quantities = "100.00",
                Unit = "NONE"
            };
            Option_Specified_ImportProductQuantities_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportProductQuantities_productData }
            };

            // There is House quantities 

            Option_Specified_ImportProductQuantities_productToHouse = new ProductToOptionData(Option_Specified_ImportProductQuantities_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportProductQuantities_productData } };

            Option_Specified_ImportProductQuantities_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportProductQuantities_productToHouse }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData Option_Specified_productData_ImportProductQuantities_HouseBOM = new ProductData(Option_Specified_ImportProductQuantities_productData) { Style = "123" };
            Option_Specified_ImportProductQuantities_productToHouseBOM = new ProductToOptionData(Option_Specified_ImportProductQuantities_productTo) { ProductList = new List<ProductData> { Option_Specified_productData_ImportProductQuantities_HouseBOM } };


            Option_Specified_ImportProductQuantities_JobQuantities_JobBOM = new HouseQuantitiesData(Option_Specified_ImportProductQuantities_JobQuantities)
            {
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportProductQuantities_productToHouseBOM }
            };


            // 2. Import updated product quantities (Sourceid tag)
            expectedData_ImportUpdatedProductQuantities = new JobImportQuantitiesData()
            {
                Option = "Option_QA_RT1",
                BuildingPhaseCode = "QA_1",
                BuildingPhaseName = "QA_BuildingPhase_01_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Now = "100.00",
                Future = "200.00"

            };

            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            Option_Specified_ImportUpdatedProductQuantities_productData = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "200.00",
                Unit = "NONE",
            };
            Option_Specified_ImportUpdatedProductQuantities_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportUpdatedProductQuantities_productData }
            };

            // There is House quantities 

            Option_Specified_ImportUpdatedProductQuantities_productToHouse = new ProductToOptionData(Option_Specified_ImportUpdatedProductQuantities_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportUpdatedProductQuantities_productData } };

            Option_Specified_ImportUpdatedProductQuantities_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportUpdatedProductQuantities_productToHouse }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData Option_Specified_productData_ImportUpdatedProductQuantities_HouseBOM = new ProductData(Option_Specified_ImportUpdatedProductQuantities_productData);
            Option_Specified_ImportUpdatedProductQuantities_productToHouseBOM = new ProductToOptionData(Option_Specified_ImportUpdatedProductQuantities_productTo) { ProductList = new List<ProductData> { Option_Specified_productData_ImportUpdatedProductQuantities_HouseBOM } };


            Option_Specified_ImportUpdatedProductQuantities_JobQuantities_JobBOM = new HouseQuantitiesData(Option_Specified_ImportUpdatedProductQuantities_JobQuantities)
            {
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportUpdatedProductQuantities_productToHouseBOM }
            };



            //3.  Import product quantities with old value (Sourceid tag)
            expectedData_ImportProductQuantitiesWithOldValue = new JobImportQuantitiesData()
            {
                Option = "Option_QA_RT1",
                BuildingPhaseCode = "QA_1",
                BuildingPhaseName = "QA_BuildingPhase_01_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Now = "200.00",
                Future = "200.00"
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            Option_Specified_ImportProductQuantitiesWithOldValue_productData = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "200.00",
                Unit = "NONE",
            };
            Option_Specified_ImportProductQuantitiesWithOldValue_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportProductQuantitiesWithOldValue_productData }
            };

            // There is House quantities 

            Option_Specified_ImportProductQuantitiesWithOldValue_productToHouse = new ProductToOptionData(Option_Specified_ImportProductQuantitiesWithOldValue_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportProductQuantitiesWithOldValue_productData } };

            Option_Specified_ImportProductQuantitiesWithOldValue_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportProductQuantitiesWithOldValue_productToHouse }
            };

            //4. Import the file has wrong sourced value

            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            Option_Specified_ImportTheFileHasWrongSourcedValue_productData = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "200.00",
                Unit = "NONE",
            };
            Option_Specified_ImportTheFileHasWrongSourcedValue_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportTheFileHasWrongSourcedValue_productData }
            };

            // There is House quantities 

            Option_Specified_ImportTheFileHasWrongSourcedValue_productToHouse = new ProductToOptionData(Option_Specified_ImportTheFileHasWrongSourcedValue_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportTheFileHasWrongSourcedValue_productData } };

            Option_Specified_ImportTheFileHasWrongSourcedValue_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportTheFileHasWrongSourcedValue_productToHouse }
            };


            //5. Import the file has Community code over 128 characters

            //II.Import Job with No Option Specific
            //1. Import product quantities (No sourceid tag - The option has product quantities)
            _option2 = new OptionData()
            {
                Name = "Option_QA_RT2",
                Number = "",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
            };

            optionPhaseOptionQuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = PHASE_VALUE,
                ProductName = "QA_Product_01_Automation",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "67.00",
                Source = "Pipeline"
            };

            expectedData_ImportJob_No_Option_Specific1 = new JobImportQuantitiesData()
            {
                Option = "Option_QA_RT1",
                BuildingPhaseCode = "QA_2",
                BuildingPhaseName = "QA_BuildingPhase_02_Automation",
                ProductName = "QA_Product_02_Automation",
                Future = "1.00",
                Now = "0.00"
            };
            expectedData_ImportJob_No_Option_Specific2 = new JobImportQuantitiesData()
            {
                Option = "Option_QA_RT2",
                BuildingPhaseCode = "QA_1",
                BuildingPhaseName = "QA_BuildingPhase_01_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Future = "0.00",
                Now = "67.00"
            };

            expectedData_ImportJob_No_Option_Specific3 = new JobImportQuantitiesData()
            {
                Option = "Reconciled Products",
                BuildingPhaseCode = "QA_1",
                BuildingPhaseName = "QA_BuildingPhase_01_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Future = "-57.00",
                Now = "0.00"
            };
            expectedData_ImportJob_No_Option_Specific4 = new JobImportQuantitiesData()
            {
                Option = "Reconciled Products",
                BuildingPhaseCode = "QA_2",
                BuildingPhaseName = "QA_BuildingPhase_02_Automation",
                ProductName = "QA_Product_02_Automation",
                Future = "-1.00",
                Now = "0.00"
            };

            _option3 = new OptionData()
            {
                Name = "Option_QA_RT5",
                Number = "",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
            };

            option2PhaseOptionQuantitiesData = new OptionQuantitiesData()
            {
                BuildingPhase = "QA_3-QA_BuildingPhase_03_Automation",
                ProductName = "QA_Product_03_Automation",
                ProductDescription = "Product Description",
                Style = "DEFAULT",
                Condition = false,
                Use = string.Empty,
                Quantity = "123.00",
                Source = "Pipeline"
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            Option_Specified_ImportJob_No_Option_Specific1_productData = new ProductData()
            {
                Name = "QA_Product_02_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "1.00",
                Unit = "NONE",
            };

            Option_Specified_ImportJob_No_Option_Specific2_productData = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "67.00",
                Unit = "NONE",
            };
            Option_Specified_ImportJob_No_Option_Specific3_productData = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "-57.00",
                Unit = "NONE",
            };

            Option_Specified_ImportJob_No_Option_Specific4_productData = new ProductData()
            {
                Name = "QA_Product_02_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "-1.00",
                Unit = "NONE",
            };

            Option_Specified_ImportJob_No_Option_Specific1_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option_Specific1_productData }
            };

            Option_Specified_ImportJob_No_Option_Specific2_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option_Specific2_productData }
            };

            Option_Specified_ImportJob_No_Option_Specific3_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option_Specific3_productData }
            };

            Option_Specified_ImportJob_No_Option_Specific4_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option_Specific4_productData }
            };

            // There is House quantities 
            Option_Specified_ImportJob_No_Option_Specific1_productToHouse = new ProductToOptionData(Option_Specified_ImportJob_No_Option_Specific1_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option_Specific1_productData } };
            Option_Specified_ImportJob_No_Option_Specific2_productToHouse = new ProductToOptionData(Option_Specified_ImportJob_No_Option_Specific2_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option_Specific2_productData } };
            Option_Specified_ImportJob_No_Option_Specific3_productToHouse = new ProductToOptionData(Option_Specified_ImportJob_No_Option_Specific3_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option_Specific3_productData } };
            Option_Specified_ImportJob_No_Option_Specific4_productToHouse = new ProductToOptionData(Option_Specified_ImportJob_No_Option_Specific4_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option_Specific4_productData } };
            
            Option_Specified_ImportJob_No_Option_Specific1_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportJob_No_Option_Specific1_productToHouse }
            };

            Option_Specified_ImportJob_No_Option_Specific2_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportJob_No_Option_Specific2_productToHouse }
            };

            Option_Specified_ImportJob_No_Option_Specific3_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = "RECONCILED",
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportJob_No_Option_Specific3_productToHouse, Option_Specified_ImportJob_No_Option_Specific4_productToHouse }
            };

            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData Option_Specified_productData_ImportJob_No_Option_Specific1_HouseBOM = new ProductData(Option_Specified_ImportJob_No_Option_Specific1_productData) { Style = "123" };
            Option_Specified_ImportJob_No_Option_Specific1_productToHouseBOM = new ProductToOptionData(Option_Specified_ImportJob_No_Option_Specific1_productTo) { ProductList = new List<ProductData> { Option_Specified_productData_ImportJob_No_Option_Specific1_HouseBOM } };


            Option_Specified_ImportJob_No_Option_Specific1_JobQuantities_JobBOM = new HouseQuantitiesData(Option_Specified_ImportJob_No_Option_Specific1_JobQuantities)
            {
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportJob_No_Option_Specific1_productToHouseBOM }
            };

            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData Option_Specified_productData_ImportJob_No_Option_Specific2_HouseBOM = new ProductData(Option_Specified_ImportJob_No_Option_Specific2_productData) { Style = "123" };
            Option_Specified_ImportJob_No_Option_Specific2_productToHouseBOM = new ProductToOptionData(Option_Specified_ImportJob_No_Option_Specific2_productTo) { ProductList = new List<ProductData> { Option_Specified_productData_ImportJob_No_Option_Specific2_HouseBOM } };


            Option_Specified_ImportJob_No_Option_Specific2_JobQuantities_JobBOM = new HouseQuantitiesData(Option_Specified_ImportJob_No_Option_Specific2_JobQuantities)
            {

                productToOption = new List<ProductToOptionData> { Option_Specified_ImportJob_No_Option_Specific2_productToHouseBOM }
            };



            ProductData Option_Specified_productData_ImportJob_No_Option_Specific3_HouseBOM = new ProductData(Option_Specified_ImportJob_No_Option_Specific3_productData) { Style = "123" };
            ProductData Option_Specified_productData_ImportJob_No_Option_Specific4_HouseBOM = new ProductData(Option_Specified_ImportJob_No_Option_Specific4_productData) { Style = "123" };
            
            Option_Specified_ImportJob_No_Option_Specific3_productToHouseBOM = new ProductToOptionData(Option_Specified_ImportJob_No_Option_Specific3_productTo) { ProductList = new List<ProductData> { Option_Specified_productData_ImportJob_No_Option_Specific3_HouseBOM } };
            Option_Specified_ImportJob_No_Option_Specific4_productToHouseBOM = new ProductToOptionData(Option_Specified_ImportJob_No_Option_Specific4_productTo) { ProductList = new List<ProductData> { Option_Specified_productData_ImportJob_No_Option_Specific4_HouseBOM } };

            Option_Specified_ImportJob_No_Option_Specific3_JobQuantities_JobBOM = new HouseQuantitiesData(Option_Specified_ImportJob_No_Option_Specific3_JobQuantities)
            {
                optionName = "Reconciled Products",
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportJob_No_Option_Specific3_productToHouseBOM, Option_Specified_ImportJob_No_Option_Specific4_productToHouseBOM }
            };

            //2. Import file with the option has product quantities but doesn’t belong this job and import file has contain this option (No sourceid tag)

            expectedData_ImportJob_No_Option2_Specific1 = new JobImportQuantitiesData()
            {
                Option = "Option_QA_RT2",
                BuildingPhaseCode = "QA_1",
                BuildingPhaseName = "QA_BuildingPhase_01_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Future = "67.00",
                Now = "67.00"
            };
            expectedData_ImportJob_No_Option2_Specific2 = new JobImportQuantitiesData()
            {
                Option = "Reconciled Products",
                BuildingPhaseCode = "QA_1",
                BuildingPhaseName = "QA_BuildingPhase_01_Automation",
                ProductName = IMPORT_PRODUCT_NAME,
                Future = "32.00",
                Now = "-57.00"
            };

            expectedData_ImportJob_No_Option2_Specific3 = new JobImportQuantitiesData()
            {
                Option = "Reconciled Products",
                BuildingPhaseCode = "QA_2",
                BuildingPhaseName = "QA_BuildingPhase_02_Automation",
                ProductName = "QA_Product_02_Automation",
                Future = "-1.00",
                Now = "-1.00"
            };

            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            Option_Specified_ImportJob_No_Option2_Specific1_productData = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "67.00",
                Unit = "NONE",
            };

            Option_Specified_ImportJob_No_Option2_Specific2_productData = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "32.00",
                Unit = "NONE",
            };



            Option_Specified_ImportJob_No_Option2_Specific1_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option2_Specific1_productData }
            };

            Option_Specified_ImportJob_No_Option2_Specific2_productTo = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option2_Specific2_productData }
            };


            // There is House quantities 

            Option_Specified_ImportJob_No_Option2_Specific1_productToHouse = new ProductToOptionData(Option_Specified_ImportJob_No_Option2_Specific1_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option2_Specific1_productData } };
            Option_Specified_ImportJob_No_Option2_Specific2_productToHouse = new ProductToOptionData(Option_Specified_ImportJob_No_Option2_Specific2_productTo) { ProductList = new List<ProductData> { Option_Specified_ImportJob_No_Option2_Specific2_productData } };

            Option_Specified_ImportJob_No_Option2_Specific1_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportJob_No_Option2_Specific1_productToHouse }
            };

            Option_Specified_ImportJob_No_Option2_Specific2_JobQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = "RECONCILED",
                productToOption = new List<ProductToOptionData> { Option_Specified_ImportJob_No_Option2_Specific2_productToHouse }
            };

        }

        [Test]
        [Category("Section_VIII")]
        public void UAT_HotFix_Import_JobXMLs_Version_1_0_2_Is_Not_Working_In_Pipeline_2023_0_2()
        {

            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION1_NAME_DEFAULT);
            if (OptionPage.Instance.IsItemInGrid("Name", OPTION1_NAME_DEFAULT) is true)
            {
                // Delete before creating a new one
                OptionPage.Instance.SelectItemInGrid("Name", OPTION1_NAME_DEFAULT);
            }

            //Navigate To Option Product
            OptionDetailPage.Instance.LeftMenuNavigation("Products");

            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE2_VALUE) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                ProductsToOptionPage.Instance.AddOptionQuantities(OptionQuantitiesData);
            }

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();

            // Step 0.1: Navigate to Jobs > All Jobs and filter job 'RT-QA_JOB_Import_Quantity' and delete if it's existing
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Navigate to Jobs > All Jobs and filter job 'RT-QA_JOB_Import_Quantity' and delete if it's existing.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData.Name);
            JobPage.Instance.WaitGridLoad();

            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name))
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }

            // Step 0.2: Create a new job with name 'RT-QA_JOB_Import_Quantity'.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Create a new job with name 'RT-QA_JOB_Import_Quantity'.</b></font>");
            JobPage.Instance.CreateJob(jobData);

            //Check Header in BreadsCrumb 
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(jobData.Name) is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Header is present incorrectly.</font>");
            }

            // Step 0.3: Switch to Job/ Options page. Add Option '{OPTION1_NAME_DEFAULT}' to job if it doesn't exist
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><bStep 0.3: Switch to Job/ Options page. Add Option '{OPTION1_NAME_DEFAULT}' to job if it doesn't exist.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Options", false);

            if (JobOptionPage.Instance.IsOptionCardDisplayed is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            }

            // Step 0.4: Open Option page from left navigation and approve config.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Open Option page from left navigation and approve config.</b></font>");

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION1_NAME_DEFAULT) is false)
            {
                string[] selectedOption = { OPTION1_CODE_DEFAULT + "-" + OPTION1_NAME_DEFAULT, OPTION2_CODE_DEFAULT + "-" + OPTION2_NAME_DEFAULT };

                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddNewConfiguration();
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
                // Approve config
                JobOptionPage.Instance.ClickApproveConfig();
            }


            //Step 1.1a: Open Import page from left navigation.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1a: Open Import page from left navigation.</b></font>");

            JobDetailPage.Instance.LeftMenuNavigation("Import", false);
            if (JobImportPage.Instance.IsImportPageDisplayed is false)
                ExtentReportsHelper.LogFail("<font color='red'>Import Job Quantities page doesn't display or title is incorrect.</font>");
            ExtentReportsHelper.LogPass(null, "<font color='green'><b>Import Job Quantities page displays correctly.</b></font>");

            //Step 1.2a: Don't need to click 'Option Specified' tab, It's default value.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2a: Don't need to click 'Option Specified' tab, It's default value.</b></font>");
            JobImportPage.Instance.ClickOptionSpecified();

            // Step 1.3a: Import product quantities (No sourceid tag - The option has product quantities) and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 1.3a: Import product quantities (No sourceid tag - The option has product quantities) and verify it. -------------------------</b></font>");
            string importFileName = "JobStartWithOptionsSample_1.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_OPTION_SPECIFIED);

            // Step 1.4a: Expand grid view.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4a: Expand grid view.</b></font>");

            // Step 1.5a: Expand Product Quantities And Verify Product Quantities To Import 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5a: Expand Product Quantities And Verify Product Quantities To Import.</b></font>");
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportProductQuantities);


            // Step 1.6a: Click Finish Import button. Expected: Import successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6a: Click Finish Import button. Expected: Import successfully.</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            // Step 1.7a: Open the Job Quantities page on a new tab. All products should be shown correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7a: Open the Job Quantities page on a new tab. All products should be shown correctly.</b></font>");
            var jobImport_URL = JobImportPage.Instance.GetJobQuantitiesURL();
            CommonHelper.OpenLinkInNewTab(jobImport_URL);
            CommonHelper.SwitchTab(1);

            // Step 1.7a: Verify Job Quantities In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7a: Verify Job Quantities In Grid.</b></font>");
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportProductQuantities_JobQuantities);

            // Step 1.8a: Navigate Job BOM 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8a: Navigate Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");

            //Step 1.9a: Generate a Job BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9a: Generate a Job BOM.</b></font>");
            JobBOMPage.Instance.GenerateJobBOM();

            //Step 1.10a: Verify Job BOM In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10a: Verify Job BOM In Grid.</b></font>");
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_Phase_Option_Phase_Product);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Option_Specified_ImportProductQuantities_JobQuantities_JobBOM);


            // Step 1b: Import updated product quantities (Sourceid tag) and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 1b: Import updated product quantities (Sourceid tag) and verify it. -------------------------</b></font>");
            
            //Step 1.1b: Delete File Import Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1b: Delete File Import Job.</b></font>");
            CommonHelper.SwitchTab(0);
            CommonHelper.RefreshPage();
            JobImportPage.Instance.DeleteSelectedFile();

            //Step 1.2b: Import Job Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2b: Import Job Quantities.</b></font>");
            importFileName = "JobStartWithOptionsSample_02.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_OPTION_SPECIFIED);

            //Step 1.3b: Expand grid view And Verify Product Quantities on the import grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3b: Expand grid view And Verify Product Quantities on the import grid.</b></font>");
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportUpdatedProductQuantities);

            //Step 1.4b: Click Finish Impor button. Expected: Import successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4b: Click Finish Impor button. Expected: Import successfully.</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            //Step 1.5b: Switch to Job Quantities page tab. All products should be shown correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5b: Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");
            CommonHelper.SwitchTab(1);
            CommonHelper.RefreshPage();

            // Step 1.6b: Verify Job Quantities In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6b: Verify Job Quantities In Grid.</b></font>");
            JobBOMPage.Instance.LeftMenuNavigation("Quantities");
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportUpdatedProductQuantities_JobQuantities);

            // Step 1c:  Import product quantities with old value (Sourceid tag) and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 1c:  Import product quantities with old value (Sourceid tag) and verify it. -------------------------</b></font>");
            CommonHelper.SwitchTab(0);
            CommonHelper.RefreshPage();
            //Step 1.1c: Delete File Import Job Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1c: Delete File Import Job Quantities.</b></font>");
            JobImportPage.Instance.DeleteSelectedFile();

            //Step 1.2c: Upload job quantities file again
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2c: Upload job quantities file again.</b></font>");
            importFileName = "JobStartWithOptionsSample_03.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_OPTION_SPECIFIED);

            //Step 1.3c: Expand grid view And Verify Product Quantities on the import grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3c: Expand grid view And Verify Product Quantities on the import grid.</b></font>");
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportProductQuantitiesWithOldValue);

            //Step 1.4c: Click Finish Impor button. Expected: Import successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4c: Click Finish Impor button. Expected: Import successfully</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            //Step 1.5c: Switch to Job Quantities page tab. All products should be shown correctl
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5c: Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");
            CommonHelper.SwitchTab(1);
            CommonHelper.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportProductQuantitiesWithOldValue_JobQuantities);

            // Step 1d : Import the file has wrong sourceid value and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 1d : Import the file has wrong sourceid value and verify it. -------------------------</b></font>");
            //Step 1.1d: Delete File Import Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1d: Delete File Import Job.</b></font>");
            CommonHelper.SwitchTab(0);
            CommonHelper.RefreshPage();
            JobImportPage.Instance.DeleteSelectedFile();

            //Step 1.2d: Upload Job Quantities And Process For Error Forma
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2d: Upload job quantities And Process For Error Format.</b></font>");
            importFileName = "JobStartWithOptionsSample_04.xml";
            string expectedToastMess = $"Unable to process {importFileName}. Job code does not exist.";

            JobImportPage.Instance.UploadJobQuantitiesAndProcessForErrorFile(importFileName, expectedToastMess, IS_OPTION_SPECIFIED);
            
            //Switch to Job Quantities page tab. All products should be shown correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3d: Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");
            CommonHelper.SwitchTab(1);
            JobBOMPage.Instance.LeftMenuNavigation("Quantities");
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportTheFileHasWrongSourcedValue_JobQuantities);

            // Step 1e: Import the file has ommunity code over 128 characters and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 1e: Import the file has ommunity code over 128 characters and verify it. -------------------------</b></font>");
            CommonHelper.SwitchTab(0);
            CommonHelper.RefreshPage();
            //Step 1.1e: Upload job quantities And Process For Error File
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1e: Upload job quantities And Process For Error File.</b></font>");
            importFileName = "JobStartWithOptionsSample_05.xml";
            string expectedErrorToastMess = $"Unable to process {importFileName}. The community code should not be over 128 characters.";
            JobImportPage.Instance.UploadJobQuantitiesAndProcessForErrorFile(importFileName, expectedErrorToastMess, IS_OPTION_SPECIFIED);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II. Import Job with No Option Specific.</b></font>");
            CommonHelper.SwitchTab(1);
            JobBOMPage.Instance.LeftMenuNavigation("Quantities");


            //2. Import file with the option belongs to this job has product quantities and and the import file has this option  ( sourceid tag)
            ExtentReportsHelper.LogInformation(null, "Step 2.1a: Go to Option page and create option/phase bid with regular products");
            // Go to Option page
            CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            CommonHelper.SwitchTab(2);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option2.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option2.Name) is true)
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option2.Name);
                //Navigate To Option Product
                ExtentReportsHelper.LogInformation(null, "Step 2.2a: Assign Product to this Option: Phasebid Option with supplemental products");
                OptionDetailPage.Instance.LeftMenuNavigation("Products");

                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.3a: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE_VALUE}'.</b></font>");
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", PHASE_VALUE) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionPhaseOptionQuantitiesData);
                }
            }

            CommonHelper.SwitchTab(0);
            // Step 2.4a: Select No Option Specified button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4a: Select No Option Specified button.</b></font>");
            JobImportPage.Instance.ClickNoOptionSpecified();

            // Step 2.5a: Import file with the option belongs this job and the import file have product quantities (sourceid tag)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 2.5a: Import file with the option belongs this job and the import file have product quantities (sourceid tag). -------------------------</b></font>");
            importFileName = "Import_Job_No_Option_Specific_1.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_NO_OPTION_SPECIFIED);

            // Step 2.6a: Expand Product Quantities And Verify Product Quantities To Import 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.6a: Expand Product Quantities And Verify Product Quantities To Import.</b></font>");
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportJob_No_Option_Specific1);
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportJob_No_Option_Specific2);
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportJob_No_Option_Specific3);
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportJob_No_Option_Specific4);

            // Step 2.7a: Click Finish Import button. Expected: Import successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step 2.7a: Click Finish Import button. Expected: Import successfully.</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            // Step 2.8a: Open the Job Quantities page on a new tab. All products should be shown correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.8a: Open the Job Quantities page on a new tab. All products should be shown correctly.</b></font>");

            CommonHelper.SwitchTab(1);
            JobBOMPage.Instance.LeftMenuNavigation("Quantities");
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportJob_No_Option_Specific1_JobQuantities);
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportJob_No_Option_Specific2_JobQuantities);
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportJob_No_Option_Specific3_JobQuantities);

            //Step 2.9a: Generate a Job BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.9a: Generate a Job BOM.</b></font>");
            JobQuantitiesPage.Instance.LeftMenuNavigation("Job BOM");
            JobBOMPage.Instance.GenerateJobBOM();

            //Step 2.10a: Verify Job BOM In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.10a: Verify Job BOM In Grid.</b></font>");
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_Phase_Option_Phase_Product);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Option_Specified_ImportJob_No_Option_Specific1_JobQuantities_JobBOM);
            CommonHelper.RefreshPage();
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_Phase_Option_Phase_Product);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Option_Specified_ImportJob_No_Option_Specific2_JobQuantities_JobBOM);
            CommonHelper.RefreshPage();
            JobBOMPage.Instance.SwitchJobBomView(JOB_BOM_VIEW_Phase_Option_Phase_Product);
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(Option_Specified_ImportJob_No_Option_Specific3_JobQuantities_JobBOM);

            // Go to Option page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2b: Go to Option page</b></font>");

            CommonHelper.SwitchTab(2);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option3.Name);

            if (OptionPage.Instance.IsItemInGrid("Name", _option3.Name) is true)
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option3.Name);

                //Step 2.1b: Assign Product to this Option
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1b: Assign Product to this Option</b></font>");
                OptionDetailPage.Instance.LeftMenuNavigation("Products");
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.2b: Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{PHASE_VALUE}'.</b></font>");

                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", option2PhaseOptionQuantitiesData.BuildingPhase) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    ProductsToOptionPage.Instance.AddOptionQuantities(option2PhaseOptionQuantitiesData);
                }
            }

            CommonHelper.SwitchTab(0);
            CommonHelper.RefreshPage();

            // Step 2.3b: Select No Option Specified button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3b: Select No Option Specified button.</b></font>");
            JobImportPage.Instance.ClickNoOptionSpecified();

            //Step 2.4b: Delete File Import Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4b:  Delete File Import Job.</b></font>");
            JobImportPage.Instance.DeleteSelectedFile();

            // Step 2.5b:  Import file with the option has product quantities but doesn’t belong this job and import file has contain this option (No sourceid tag)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 2.5b:  Import file with the option has product quantities but doesn’t belong this job and import file has contain this option (No sourceid tag). -------------------------</b></font>");
            importFileName = "Import_Job_No_Option_Specific_2.xml";
            JobImportPage.Instance.UploadJobQuantitiesAndProcess(importFileName, IS_NO_OPTION_SPECIFIED);

            //Step 2.6b: Expand grid view And Verify Product Quantities on the import grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.6b: Expand grid view And Verify Product Quantities on the import grid.</b></font>");
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportJob_No_Option2_Specific1);
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportJob_No_Option2_Specific2);
            JobImportPage.Instance.ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(expectedData_ImportJob_No_Option2_Specific3);

            // Step 2.7b: Click Finish Import button. Expected: Import successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.7b: Click Finish Import button. Expected: Import successfully.</b></font>");
            JobImportPage.Instance.ClickFinishImport();

            //Step 2.8c: Switch to Job Quantities page tab. All products should be shown correctl
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.8c: Switch to Job Quantities page tab. All products should be shown correctly.</b></font>");

            CommonHelper.SwitchTab(1);
            JobBOMPage.Instance.LeftMenuNavigation("Quantities");
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportJob_No_Option2_Specific1_JobQuantities);
            JobQuantitiesPage.Instance.RefreshPage();
            JobQuantitiesPage.Instance.VerifyJobQuantitiesInGrid(Option_Specified_ImportJob_No_Option2_Specific2_JobQuantities);

            // Step 2c: Import the file has ommunity code over 128 characters and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>------------------------- Step 2c: Import the file has ommunity code over 128 characters and verify it. -------------------------</b></font>");
            CommonHelper.SwitchTab(0);
            CommonHelper.RefreshPage();
            //Step 2.1c: Delete File Import Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>2.1c: Delete File Import Job.</b></font>");

            JobImportPage.Instance.ClickNoOptionSpecified();
            JobImportPage.Instance.DeleteSelectedFile();

            //Step 2.2c: Upload job quantities And Process For Error File
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2c: Upload job quantities And Process For Error File.</b></font>");

            importFileName = "Import_Job_No_Option_Specific_3.xml";
            string NoOptionexpectedErrorToastMess = $"Unable to process {importFileName}. The file does not use the latest xml import version. (1.0.2)";
            JobImportPage.Instance.UploadJobQuantitiesAndProcessForErrorFile(importFileName, NoOptionexpectedErrorToastMess, IS_NO_OPTION_SPECIFIED);

            // Close current tab
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }

    }
}
