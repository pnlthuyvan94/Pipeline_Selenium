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
using Pipeline.Testing.Pages.Assets.Communities.CommunityHouseBOM;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Communities.Products;
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
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_X
{
    class UAT_HOTFIX_PIPE_48407 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_X);
        }

        OptionData option;
        CommunityData community;
        HouseData housedata;
        OptionQuantitiesData optionQuantitiesData;
        CommunityQuantitiesData communityQuantitiesData;
        private int totalItems;
        private string exportFileName;

        private readonly int[] indexs = new int[] { };
        private const string ImportType = "Pre-Import Modification";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";

        private static string COMMUNITY_CODE_DEFAULT = "Automation_48407";
        private static string COMMUNITY_NAME_DEFAULT = "QA_RT_Community_48407_Automation";

        private static string HOUSE_NAME_DEFAULT = "QA_RT_House_48407_Automation";

        private static string GLOBAL_OPTION_NAME_DEFAULT = "QA_RT_Global_Option_Automation";
        private static string GLOBAL_OPTION_CODE_DEFAULT = "8407";

        private static string OPTION1_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private static string OPTION1_CODE_DEFAULT = "0100";

        private static string OPTION2_NAME_DEFAULT = "QA_RT_Option02_Automation";
        private static string OPTION2_CODE_DEFAULT = "0200";

        private static string OPTION3_NAME_DEFAULT = "QA_RT_Option03_Automation";
        private static string OPTION3_CODE_DEFAULT = "0300";

        string[] OptionData1 = { GLOBAL_OPTION_NAME_DEFAULT };
        string[] OptionData2 = { OPTION1_NAME_DEFAULT };
        string[] OptionData3 = { OPTION2_NAME_DEFAULT };
        string[] OptionData4 = { OPTION3_NAME_DEFAULT };

        private const string EXPORT_XML_MORE_MENU = "XML";
        private const string EXPORT_CSV_MORE_MENU = "CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Excel";

        List<string> Options1 = new List<string>() { GLOBAL_OPTION_NAME_DEFAULT, OPTION1_NAME_DEFAULT, OPTION2_NAME_DEFAULT };
        List<string> Options2 = new List<string>() { GLOBAL_OPTION_NAME_DEFAULT, OPTION1_NAME_DEFAULT, OPTION2_NAME_DEFAULT };
        private const string EXPORT_SELECTED_HOUSES = "Export Selected Houses";

        private static string PRODUCT_GLOBAL_OPTION_NAME_DEFAULT = "QA_RT_New_Product_Automation_04";

        private static string PRODUCT1_NAME_DEFAULT = "QA_RT_New_Product_Automation_01";

        private static string PRODUCT2_NAME_DEFAULT = "QA_RT_New_Product_Automation_02";

        private static string PRODUCT3_NAME_DEFAULT = "QA_RT_New_Product_Automation_03";
        private static string PRODUCT4_NAME_DEFAULT = "QA_RT_New_Product_Automation_04";
        private static string PRODUCT5_NAME_DEFAULT = "QA_RT_New_Product_Automation_05";

        private static string StyleOfProduct = "QA_RT_New_Style_Auto";

        private static string BuildingPhaseOfProduct = "Au01-QA_RT_New_Building_Phase_01_Automation";
        private static string BuildingPhaseOfSubcomponent = "Au02-QA_RT_New_Building_Phase_02_Automation";
        private static string BuildingPhaseOfSubcomponent_II = "Au03-QA_RT_New_Building_Phase_03_Automation";
        private ProductData productData_Option_1;
        private ProductData productData_Option_2;
        private ProductData productData_GlobalOption;

        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToGlobalOption;

        private ProductData productData_House_1;
        private ProductData productData_House_2;
        private ProductData productData_GlobalOption_House;


        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToGlobalOption_House;

        private ProductToOptionData productToHouseBOM1;
        private ProductToOptionData productToHouseBOM2;
        private ProductToOptionData productToGlobalOption_HouseBOM;

        private HouseQuantitiesData houseQuantities1;
        private HouseQuantitiesData houseQuantities2;


        private HouseQuantitiesData houseQuantities1_HouseBOM;
        private HouseQuantitiesData houseQuantities2_HouseBOM;
        private HouseQuantitiesData houseQuantities_GlobalOption_HouseBOM;


        //Specific House Quantities
        private ProductData productData_SpecificCommunityI_1;
        private ProductData productData_SpecificCommunityI_2;
        private ProductData productData_SpecificCommunityI_3;

        private ProductToOptionData productToOption_SpecificCommunityI_1;
        private ProductToOptionData productToOption_SpecificCommunityI_2;
        private ProductToOptionData productToOption_SpecificCommunityI_3;

        private ProductData productData_House_SpecificCommunityI_1;
        private ProductData productData_House_SpecificCommunityI_2;
        private ProductData productData_House_SpecificCommunityI_3;

        private ProductToOptionData productToHouse_SpecificCommunityI_1;
        private ProductToOptionData productToHouse_SpecificCommunityI_2;
        private ProductToOptionData productToHouse_SpecificCommunityI_3;

        private ProductToOptionData productToHouseBOM_SpecificCommunityI_1;
        private ProductToOptionData productToHouseBOM_SpecificCommunityI_2;
        private ProductToOptionData productToHouseBOM_SpecificCommunityI_3;

        private HouseQuantitiesData houseQuantities_HouseBOM_SpecificCommunityI_1;
        private HouseQuantitiesData houseQuantities_HouseBOM_SpecificCommunityI_2;
        private HouseQuantitiesData houseQuantities_HouseBOM_SpecificCommunityI_3;


        //II. Verify the Condition option shown on the export BOM

        private ProductData productData_OptionII_1;
        private ProductData productData_OptionII_2;

        private ProductToOptionData productToOptionII_1;
        private ProductToOptionData productToOptionII_2;

        private ProductData productData_HouseII_1;
        private ProductData productData_HouseII_2;


        private ProductToOptionData productToHouseII_1;
        private ProductToOptionData productToHouseII_2;

        private ProductToOptionData productToHouseBOMII_1;
        private ProductToOptionData productToHouseBOMII_2;

        private HouseQuantitiesData houseQuantitiesII_1;
        private HouseQuantitiesData houseQuantitiesII_2;


        private HouseQuantitiesData houseQuantitiesII_1_HouseBOM;
        private HouseQuantitiesData houseQuantitiesII_2_HouseBOM;

        //Specific House Quantities
        private ProductData productData_SpecificCommunityII_1;
        private ProductData productData_SpecificCommunityII_2;

        private ProductToOptionData productToOption_SpecificCommunityII_1;
        private ProductToOptionData productToOption_SpecificCommunityII_2;

        private ProductData productData_House_SpecificCommunityII_1;
        private ProductData productData_House_SpecificCommunityII_2;

        private ProductToOptionData productToHouse_SpecificCommunityII_1;
        private ProductToOptionData productToHouse_SpecificCommunityII_2;

        private ProductToOptionData productToHouseBOM_SpecificCommunityII_1;
        private ProductToOptionData productToHouseBOM_SpecificCommunityII_2;

        private HouseQuantitiesData houseQuantities_HouseBOM_SpecificCommunityII_1;
        private HouseQuantitiesData houseQuantities_HouseBOM_SpecificCommunityII_2;


        [SetUp]
        public void GetData()
        {
            optionQuantitiesData = new OptionQuantitiesData()
            {
                OptionName = GLOBAL_OPTION_NAME_DEFAULT,
                BuildingPhase = "Au02" + "-" + "QA_RT_New_Building_Phase_02_Automation",
                ProductName = PRODUCT_GLOBAL_OPTION_NAME_DEFAULT,
                Style = "QA_RT_New_Style_Auto",
                Condition = true,
                Operator = "and",
                FinalCondition = GLOBAL_OPTION_NAME_DEFAULT,
                Use = string.Empty,
                Quantity = "3.00",
                Source = "Pipeline"
            };


            communityQuantitiesData = new CommunityQuantitiesData()
            {
                OptionName = OPTION1_NAME_DEFAULT,
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductName = PRODUCT1_NAME_DEFAULT,
                Style = "QA_RT_New_Style_Auto",
                Condition = true,
                Use = "NONE",
                Quantity = "1.00",
                Source = "Pipeline"
            };

            var optionType = new List<bool>()
            {
                false, false, true
            };

            option = new OptionData()
            {
                Name = "QA_RT_Global_Option_Automation",
                Number = "8407",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };

            community = new CommunityData()
            {
                Name = "QA_RT_Community_48407_Automation",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "Automation_48407",
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
                Slug = "R-QA-Only-Community-Auto",
            };

            housedata = new HouseData()
            {
                HouseName = "QA_RT_House_48407_Automation",
                SaleHouseName = "QA_RT_House_48407_Automation",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "8407",
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


            //I. Verify the conditions option of themselves doesn't show on the export BOM
            productData_Option_1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "1.00",
                Unit = "NONE",
            };

            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_02",
                Style = "QA_RT_New_Style_Auto",
                Quantities = "2.00",
                Unit = "NONE",
            };

            productData_GlobalOption = new ProductData()
            {
                Name = PRODUCT4_NAME_DEFAULT,
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "3.00",
                Unit = "NONE",
            };

            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_1 },
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_2 },
            };

            productToGlobalOption = new ProductToOptionData()
            {
                BuildingPhase = "Au02-QA_RT_New_Building_Phase_02_Automation",
                ProductList = new List<ProductData> { productData_GlobalOption },
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2) ;

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_GlobalOption_House = new ProductData(productData_GlobalOption) ;

 

            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };
            productToGlobalOption_House = new ProductToOptionData(productToGlobalOption) { ProductList = new List<ProductData> { productData_GlobalOption_House } };
            

            // There is no House quantities 
            houseQuantities1 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                dependentCondition= OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1  }
            };

            houseQuantities2 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse2 }
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_1 = new ProductData(productData_Option_1) { Quantities = "2.00" };
            ProductData productData_HouseBOM_2 = new ProductData(productData_Option_2) { Name = "QA_RT_New_Product_Automation_04", };
            ProductData productData_GlobalOption_HouseBOM_3 = new ProductData(productData_GlobalOption);

            productToHouseBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };

            productToHouseBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_HouseBOM_2 }, BuildingPhase = "Au02-QA_RT_New_Building_Phase_02_Automation" };

            productToGlobalOption_HouseBOM = new ProductToOptionData(productToGlobalOption_House) { ProductList = new List<ProductData> { productData_GlobalOption_HouseBOM_3 } };

            houseQuantities1_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM1 }
            };
            houseQuantities2_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> {productToHouseBOM2 }
            };
            houseQuantities_GlobalOption_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = GLOBAL_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToGlobalOption_HouseBOM }
            };

            //Community Specific
            productData_SpecificCommunityI_1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_04",
                Style = "QA_RT_New_Style_Auto",
                Quantities = "3.00",
            };

            productData_SpecificCommunityI_2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Quantities = "2.00",
            };

            productData_SpecificCommunityI_3 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_04",
                Style = "QA_RT_New_Style_Auto",
                Quantities = "2.00",
            };


            productToOption_SpecificCommunityI_1 = new ProductToOptionData()
            {
                BuildingPhase = "Au02-QA_RT_New_Building_Phase_02_Automation",
                ProductList = new List<ProductData> { productData_SpecificCommunityI_1 },
            };

            productToOption_SpecificCommunityI_2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_SpecificCommunityI_2 },
            };

            productToOption_SpecificCommunityI_3 = new ProductToOptionData()
            {
                BuildingPhase = "Au02-QA_RT_New_Building_Phase_02_Automation",
                ProductList = new List<ProductData> { productData_SpecificCommunityI_3 },
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_SpecificCommunityI_1 = new ProductData(productData_SpecificCommunityI_1);
            productData_House_SpecificCommunityI_2 = new ProductData(productData_SpecificCommunityI_2);
            productData_House_SpecificCommunityI_3 = new ProductData(productData_SpecificCommunityI_3);

            productToHouse_SpecificCommunityI_1 = new ProductToOptionData(productToOption_SpecificCommunityI_1) { ProductList = new List<ProductData> { productData_House_SpecificCommunityI_1 } };
            productToHouse_SpecificCommunityI_2 = new ProductToOptionData(productToOption_SpecificCommunityI_2) { ProductList = new List<ProductData> { productData_House_SpecificCommunityI_2 } };
            productToHouse_SpecificCommunityI_3 = new ProductToOptionData(productToOption_SpecificCommunityI_3) { ProductList = new List<ProductData> { productData_House_SpecificCommunityI_3 } };

            houseQuantities_HouseBOM_SpecificCommunityI_1 = new HouseQuantitiesData()
            {
                houseName = HOUSE_NAME_DEFAULT,
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = GLOBAL_OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_SpecificCommunityI_1},
            };

            houseQuantities_HouseBOM_SpecificCommunityI_2 = new HouseQuantitiesData()
            {
                houseName = HOUSE_NAME_DEFAULT,
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> {productToHouse_SpecificCommunityI_2 },
            };

            houseQuantities_HouseBOM_SpecificCommunityI_3 = new HouseQuantitiesData()
            {
                houseName = HOUSE_NAME_DEFAULT,
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_SpecificCommunityI_3 },
            };

            //II. Verify the Condition option shown on the export BOM
            productData_OptionII_1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Quantities = "1.00",
            };

            productData_OptionII_2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_02",
                Style = "QA_RT_New_Style_Auto",
                Quantities = "2.00",
            };


            productToOptionII_1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_OptionII_1 },
            };

            productToOptionII_2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_OptionII_2 },
            };

            /****************************** Create Product quantities on House ******************************/

            productData_HouseII_1 = new ProductData(productData_OptionII_1);

            productData_HouseII_2 = new ProductData(productData_OptionII_2);



            productToHouseII_1 = new ProductToOptionData(productToOptionII_1) { ProductList = new List<ProductData> { productData_HouseII_1 } };
            productToHouseII_2 = new ProductToOptionData(productToOptionII_2) { ProductList = new List<ProductData> { productData_HouseII_2 } };


            // There is no House quantities 
            houseQuantitiesII_1 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                dependentCondition = OPTION3_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseII_1 }
            };

            houseQuantitiesII_2 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseII_2 }
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOMII_1 = new ProductData(productData_OptionII_1) ;
            ProductData productData_HouseBOMII_2 = new ProductData(productData_OptionII_2) { Name = "QA_RT_New_Product_Automation_05", };

            productToHouseBOMII_1 = new ProductToOptionData(productToHouseII_1) { ProductList = new List<ProductData> { productData_HouseBOMII_1 } };

            productToHouseBOMII_2 = new ProductToOptionData(productToHouseII_2) { ProductList = new List<ProductData> { productData_HouseBOMII_2 }, BuildingPhase = "Au03-QA_RT_New_Building_Phase_03_Automation" };


            houseQuantitiesII_1_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                dependentCondition = OPTION3_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOMII_1 }
            };
            houseQuantitiesII_2_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION3_NAME_DEFAULT,
                dependentCondition = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOMII_2 }
            };

            //Community Specific

            productData_SpecificCommunityII_1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Quantities = "1.00",
            };

            productData_SpecificCommunityII_2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_05",
                Style = "QA_RT_New_Style_Auto",
                Quantities = "2.00",
            };



            productToOption_SpecificCommunityII_1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_SpecificCommunityII_1 },
            };

            productToOption_SpecificCommunityII_2 = new ProductToOptionData()
            {
                BuildingPhase = "Au03-QA_RT_New_Building_Phase_03_Automation",
                ProductList = new List<ProductData> { productData_SpecificCommunityII_2 },
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_SpecificCommunityII_1 = new ProductData(productData_SpecificCommunityII_1);
            productData_House_SpecificCommunityII_2 = new ProductData(productData_SpecificCommunityII_2);

            productToHouse_SpecificCommunityII_1 = new ProductToOptionData(productToOption_SpecificCommunityII_1) { ProductList = new List<ProductData> { productData_House_SpecificCommunityII_1 } };
            productToHouse_SpecificCommunityII_2 = new ProductToOptionData(productToOption_SpecificCommunityII_2) { ProductList = new List<ProductData> { productData_House_SpecificCommunityII_2 } };

            houseQuantities_HouseBOM_SpecificCommunityII_1 = new HouseQuantitiesData()
            {
                houseName = HOUSE_NAME_DEFAULT,
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                dependentCondition= OPTION3_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_SpecificCommunityII_1 },
            };

            houseQuantities_HouseBOM_SpecificCommunityII_2 = new HouseQuantitiesData()
            {
                houseName = HOUSE_NAME_DEFAULT,
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION3_NAME_DEFAULT,
                dependentCondition = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_SpecificCommunityII_2 },
            };

            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {community.Name} is displayed in grid.</font>");
                CommunityPage.Instance.SelectItemInGrid("Name", community.Name);
            }
            else
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(community);
                string _expectedMessage = $"Could not create Community with name {community.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { community.Name}.");
                }

            }

            //Add Option into Community
            ExtentReportsHelper.LogInformation(null, "Add Option into Community.");
            CommunityDetailPage.Instance.LeftMenuNavigation("Options");

            if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION1_NAME_DEFAULT) is false)
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData2);
            }

            if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION2_NAME_DEFAULT) is false)
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData3);
            }

            if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", OPTION3_NAME_DEFAULT) is false)
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData4);
            }

            CommunityOptionPage.Instance.LeftMenuNavigation("Products");
            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            
        }
        [Test]
        [Category("Section_X")]
        public void UAT_HotFix_HouseBom_Export_DoesNot_Match_HouseBOM_displayed_in_Pipeline_Estimating_Specifically_Options_With_Conditions()
        {
            //I. Verify the conditions option of themselves doesn't show on the export BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I. Verify the conditions option of themselves doesn't show on the export BOM.</font>");
            //1. Prepare the data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.1. Prepare the data.</font>");
            //Make sure current transfer seperation character is ','
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Settings > Group by Parameters settings is turned on.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();

            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            string settingBOM_url = SettingPage.Instance.CurrentURL;
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change House BOM Product Orientation is turned false.</b></font>");
            BOMSettingPage.Instance.Check_House_BOM_Product_Orientation(false);

            //Navigate to House default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House default page.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, housedata.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", housedata.HouseName) is false)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(housedata);
                string updateMsg = $"House {housedata.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            else
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", housedata.HouseName);

            }
            string HouseDetail_url = HouseDetailPage.Instance.CurrentURL;
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", community.Name) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(community.Name, indexs);
            }

            //Add 3 options in the house option and community option page:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.1.2 Add 3 options in the house option and community option page</font>");
            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseCommunities.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", GLOBAL_OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(GLOBAL_OPTION_NAME_DEFAULT + " - " + GLOBAL_OPTION_CODE_DEFAULT);
            }

            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION1_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION1_NAME_DEFAULT + " - " + OPTION1_CODE_DEFAULT);
            }

            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION2_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION2_NAME_DEFAULT + " - " + OPTION2_CODE_DEFAULT);
            }

            //Add a condition option for the product quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.1.3 Add a condition option for the product quantities.</b></font>");
            // +[Option detail] Global option with the product quantities has conditions option of itself
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.1.4 [Option detail] Global option with the product quantities has conditions option of itself.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, option.Name);

            if (!OptionPage.Instance.IsItemInGrid("Name", option.Name))
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
                    Assert.Fail($"Could not create Option.");
                }
                BasePage.PageLoad();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option with name { option.Name} is displayed in grid.</font>");
            }
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, GLOBAL_OPTION_NAME_DEFAULT);
            if (OptionPage.Instance.IsItemInGrid("Name", GLOBAL_OPTION_NAME_DEFAULT) is true)
            {

                OptionPage.Instance.SelectItemInGrid("Name", GLOBAL_OPTION_NAME_DEFAULT);
                OptionDetailPage.Instance.LeftMenuNavigation("Products");
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", PRODUCT_GLOBAL_OPTION_NAME_DEFAULT) is false && ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Condition", GLOBAL_OPTION_NAME_DEFAULT) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionQuantitiesData);
                }
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {community.Name} is displayed in grid.</font>");
                CommunityPage.Instance.SelectItemInGrid("Name", community.Name);
            }
            CommunityDetailPage.Instance.LeftMenuNavigation("Options");
            if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", GLOBAL_OPTION_NAME_DEFAULT) is false)
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData1);
            }

            //[House quantities, Community product] Normal option with product quantities has conditions option of itself
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.1.5 [House quantities, Community product] Normal option with product quantities has conditions option of itself.</b></font>");
            CommonHelper.OpenURL(HouseDetail_url);
            HouseDetailPage.Instance.LeftMenuNavigation("Import");

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION1_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_48407_1.xml");
            
            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.1.6 Verify the set up data for product quantities on House.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities1.communityCode + "-" + houseQuantities1.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities1.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities1.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Dependent Condition", houseQuantities1.dependentCondition) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true                    
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)

                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities1.optionName}" +
                            $"<br>The expected Dependent Condition: {houseQuantities1.dependentCondition}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {community.Name} is displayed in grid.</font>");
                CommunityPage.Instance.SelectItemInGrid("Name", community.Name);
            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesData.BuildingPhase, communityQuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesData);
            }

            CommonHelper.OpenURL(HouseDetail_url);
            HouseDetailPage.Instance.LeftMenuNavigation("Import");

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }


            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION2_NAME_DEFAULT);
            }

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION2_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_48407_2.xml");
            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the set up data for product quantities on House.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities2.communityCode + "-" + houseQuantities2.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities2.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities2.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Dependent Condition", houseQuantities2.dependentCondition) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)

                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities2.optionName}" +
                            $"<br>The expected Dependent Condition: {houseQuantities2.dependentCondition}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            //[Subcomponent] Normal option with product quantities has conditions option of itself AA_NormalOption2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.1.7 [Subcomponent] Normal option with product quantities has conditions option of itself .</b></font>");
            CommonHelper.OpenURL(settingBOM_url);
            SettingPage.Instance.LeftMenuNavigation("Estimating");
            SettingPage.Instance.ShowCategoryonAddProductSubcomponentModalCheckbox_Action(false);
            string expectedMess = "Successfully added new subcomponent!";

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT2_NAME_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT2_NAME_DEFAULT))
                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT2_NAME_DEFAULT);
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");
            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(BuildingPhaseOfProduct)
                                            .SelectStyleOfProduct(StyleOfProduct)
                                            .SelectChildBuildingPhaseOfSubComponent(BuildingPhaseOfSubcomponent)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT4_NAME_DEFAULT)
                                            .SelectChildStyleOfSubComponent(StyleOfProduct)
                                            .SelectOptionSubcomponent(OPTION2_NAME_DEFAULT + " - " + OPTION2_CODE_DEFAULT)
                                            .ClickSaveProductSubcomponent();
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BuildingPhaseOfSubcomponent);
            //2/ Generate BOM: Show valid data on house BOM
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, housedata.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", housedata.HouseName) is true)
            {
                //Create a new house
                HousePage.Instance.SelectItemInGridWithTextContains("Name", housedata.HouseName);

            }
            //Navigate To House BOM
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberItem();


            //Generate House BOM and verify it
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities1_HouseBOM.communityCode + "-" + houseQuantities1_HouseBOM.communityName);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities1_HouseBOM.communityCode + "-" + houseQuantities1_HouseBOM.communityName);

            // Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.House_BOM.ToString(), COMMUNITY_NAME_DEFAULT, HOUSE_NAME_DEFAULT);

            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities1_HouseBOM);
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities2_HouseBOM);
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_GlobalOption_HouseBOM);

            //3.Export data on the House BOM, Community House BOm pages
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.1.9 Export data on the House BOM, Community House BOm pages.</b></font>");
            //House BOM Basic
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>House BOM Basic.</b></font>");
            //+ CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Export House BOM CSV.</b></font>");
            //+ Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Export House BOM Excel.</b></font>");
            //+ XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Export House BOM XML.</b></font>");

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities1_HouseBOM.communityCode + "-" + houseQuantities1_HouseBOM.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", TableType.XML);

            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName, string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName, string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);

            //House BOM Advanced
            //+ CSV
            //+ Excel
            //+ XML
            // Go to Advanced House BOM, select Community
            HouseBOMDetailPage.Instance.ClickOnAdvancedHouseBOMView();

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectAdvanceCommunity(houseQuantities1_HouseBOM.communityCode + "-" + houseQuantities1_HouseBOM.communityName);
            HouseBOMDetailPage.Instance.CheckAllOptions();

            //Get Total Number Advance HouseBOM Item
            ExtentReportsHelper.LogInformation(null, $"Get Total Number Advance HouseBOM Item.</font>");
            int totalAdvanceHouseBOMItems = HouseBOMDetailPage.Instance.GetTotalNumberAdvanceHouseBOMItem();
            HouseBOMDetailPage.Instance.GenerateAdvancedHouseBOM();
            HouseBOMDetailPage.Instance.LoadHouseAdvanceQuantities();
            //Filter Option In Grid
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities1_HouseBOM.optionName);
            foreach (ProductToOptionData housequantity in houseQuantities1_HouseBOM.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities1_HouseBOM.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities1_HouseBOM.optionName}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }

            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities2_HouseBOM.optionName);
            foreach (ProductToOptionData housequantity in houseQuantities2_HouseBOM.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities2_HouseBOM.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities2_HouseBOM.optionName}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }

            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantities_GlobalOption_HouseBOM.optionName);
            foreach (ProductToOptionData housequantity in houseQuantities_GlobalOption_HouseBOM.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantities_GlobalOption_HouseBOM.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_GlobalOption_HouseBOM.optionName}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }

            //Export XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export XML House Advanced BOM.</font>");
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (5)", TableType.XML);

            //Export CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV House Advanced BOM.</font>");

            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", totalAdvanceHouseBOMItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", TableType.CSV);

            //Export Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel House Advanced BOM.</font>");
            GenerateAdvanceHouseBOM(houseQuantities1_HouseBOM.communityCode + "-" + houseQuantities1_HouseBOM.communityName, Options1);
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", totalAdvanceHouseBOMItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", TableType.XLSX);

            //Community House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Community House BOM.</font>");
            //Community House BOM:
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", community.Name);
            }
            CommunityDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();

            totalItems = CommunityHouseBOMDetailPage.Instance.GetTotalNumberItem();

            CommunityHouseBOMDetailPage.Instance.GenerateHouseBOM();

            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_SpecificCommunityI_1);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_SpecificCommunityI_2);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_SpecificCommunityI_3);

            // Get export file name
            string exportComunityFileName = CommonHelper.GetExportFileName(ExportType.Community_HouseBOM.ToString(), COMMUNITY_NAME_DEFAULT);
            //+ CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV Community House BOM.</font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportComunityFileName}", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportComunityFileName}", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //+ Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel Community House BOM.</font>");
            //Export EXCEL House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Export EXCEL House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportComunityFileName}", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportComunityFileName}", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);


            //II. Verify the Condition option shown on the export BOM
            ExtentReportsHelper.LogInformation("<font color='lavender'>II. Verify the Condition option shown on the export BOM.</font>");
            //1. Prepare the data:
            ExtentReportsHelper.LogInformation("<font color='lavender'>II.1. Prepare the data:.</font>");
            //Add the options in the house option and community option page
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step II.1.1 Add the options in the house option and community option page</font>");
            CommonHelper.OpenURL(HouseDetail_url);
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION3_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION3_NAME_DEFAULT + " - " + OPTION3_CODE_DEFAULT);
            }


            //Add the condition option for the product quantities. ( Using the data in the I/ and just change condition to another option
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step II.1.2 Add the condition option for the product quantities. ( Using the data in the I/ and just change condition to another option)</font>");

            //[House quantities] Normal option with product quantities has conditions option : AA_NormalOption3
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step II.1.3 [House quantities] Normal option with product quantities has conditions option </font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, housedata.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", housedata.HouseName) is true)
            {
                //Create a new house
                HousePage.Instance.SelectItemInGridWithTextContains("Name", housedata.HouseName);

            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities1.communityCode + '-' + houseQuantities1.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION2_NAME_DEFAULT);
            }

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT);
            }

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_48407_3.xml");

            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II.1.4 Verify the set up data for product quantities on House.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantitiesII_1.communityCode + "-" + houseQuantitiesII_1.communityName);

            foreach (ProductToOptionData housequantity in houseQuantitiesII_1.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantitiesII_1.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Dependent Condition", OPTION3_NAME_DEFAULT) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)

                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantitiesII_1.optionName}" +
                            $"<br>The expected ependent Condition: {houseQuantitiesII_1.dependentCondition}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            foreach (ProductToOptionData housequantity in houseQuantitiesII_2.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantitiesII_2.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)

                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantitiesII_2.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            //[Subcomponent] Normal option with product quantities has conditions option 
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step II.1.5 [Subcomponent] Normal option with product quantities has conditions option</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT2_NAME_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT2_NAME_DEFAULT))
                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT2_NAME_DEFAULT);
            ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

            ProductSubcomponentPage.Instance.ClickDeleteInGird(BuildingPhaseOfSubcomponent);
            string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
            if (act_mess == "Successfully deleted subcomponent")
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BuildingPhaseOfSubcomponent} subcomponent </b></font color>");
            }
            else
                ExtentReportsHelper.LogFail($"<b> Cannot delete {BuildingPhaseOfSubcomponent} subcomponent </b>");
            ProductSubcomponentPage.Instance.CloseToastMessage();

            ProductSubcomponentPage.Instance.ClickAdd_btn();
            ProductSubcomponentPage.Instance.SelectBasicORAdvanced("Basic")
                                            .SelectBuildingPhaseOfProduct(BuildingPhaseOfProduct)
                                            .SelectStyleOfProduct(StyleOfProduct)
                                            .SelectChildBuildingPhaseOfSubComponent(BuildingPhaseOfSubcomponent_II)
                                            .InputProductSubcomponentWithoutCategory(PRODUCT5_NAME_DEFAULT)
                                            .SelectChildStyleOfSubComponent(StyleOfProduct)
                                            .SelectOptionSubcomponent(OPTION3_NAME_DEFAULT + " - " + OPTION3_CODE_DEFAULT)
                                            .ClickSaveProductSubcomponent();
            VerifyToastMessage(expectedMess, "ChildBuildingPhase", BuildingPhaseOfSubcomponent);

            //2. Generate BOM: Show valid data on house BOM
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step II.1.6 Generate BOM: Show valid data on house BOM</font>");
            // +House BOM Basic

            CommonHelper.OpenURL(HouseDetail_url);
            //Navigate To House BOM
            HouseDetailPage.Instance.LeftMenuNavigation("House BOM");
            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberItem();

            //Generate House BOM and verify it
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantitiesII_1_HouseBOM.communityCode + "-" + houseQuantitiesII_1_HouseBOM.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantitiesII_1_HouseBOM.communityCode + "-" + houseQuantitiesII_1_HouseBOM.communityName);
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantitiesII_1_HouseBOM);
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantitiesII_2_HouseBOM);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Export House BOM</b></font>");

            //House BOM Basic
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step II.1.7 House BOM Basic</font>");

            // + XML
            ExtentReportsHelper.LogInformation("<font color='lavender'>Export XML HouseBOM</font>");

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantitiesII_1_HouseBOM.communityCode + "-" + houseQuantitiesII_1_HouseBOM.communityName);
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT}", TableType.XML);

            // +CSV
            ExtentReportsHelper.LogInformation("<font color='lavender'>Export CSV HouseBOM</font>");
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (1)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            // + Excel
            ExtentReportsHelper.LogInformation("<font color='lavender'>Export Excel HouseBOM</font>");
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (1)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);

            //3. Export data on the House BOM, Community House BOm pages
            ExtentReportsHelper.LogInformation("<font color='lavender'>3. Export data on the House BOM, Community House BOm pages</font>");                        
            //House BOM Advanced
            ExtentReportsHelper.LogInformation("<font color='lavender'>Export CSV House BOM Advanced</font>");
            // + CSV
            // + Excel
            // + XML:
            // Go to Advanced House BOM, select Community
            HouseBOMDetailPage.Instance.ClickOnAdvancedHouseBOMView();

            //Select Community Name
            HouseBOMDetailPage.Instance.SelectAdvanceCommunity(houseQuantitiesII_1_HouseBOM.communityCode + "-" + houseQuantitiesII_1_HouseBOM.communityName);
            HouseBOMDetailPage.Instance.CheckAllOptions();

            //Get Total Number Advance HouseBOM Item
            ExtentReportsHelper.LogInformation(null, $"Get Total Number Advance HouseBOM Item.</font>");
            totalAdvanceHouseBOMItems = HouseBOMDetailPage.Instance.GetTotalNumberAdvanceHouseBOMItem();
            ExtentReportsHelper.LogInformation(null, $"Generate Advance Job BOM.</font>");
            HouseBOMDetailPage.Instance.GenerateAdvancedHouseBOM();
            HouseBOMDetailPage.Instance.LoadHouseAdvanceQuantities();
            //Filter Option In Grid
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", houseQuantitiesII_1_HouseBOM.optionName);
            foreach (ProductToOptionData housequantity in houseQuantitiesII_1_HouseBOM.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantitiesII_1_HouseBOM.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Dependent Condition", OPTION3_NAME_DEFAULT) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantitiesII_1_HouseBOM.optionName}" +
                            $"<br>The expected Dependent Condition: {OPTION3_NAME_DEFAULT}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }
            HouseBOMDetailPage.Instance.FilterItemInAdvanceQuantitiesGrid("Option", OPTION3_NAME_DEFAULT);           
            foreach (ProductToOptionData housequantity in houseQuantitiesII_2_HouseBOM.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Option", houseQuantitiesII_2_HouseBOM.optionName) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Dependent Condition", OPTION2_NAME_DEFAULT) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Total Qty", item.Quantities) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Product", item.Name) is true
                        && HouseBOMDetailPage.Instance.IsItemInAdvanceQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Advanced House BOM on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantitiesII_2_HouseBOM.optionName}" +
                            $"<br>The expected Dependent Condition: {OPTION2_NAME_DEFAULT}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }

            }

            //Export XML
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export XML House BOM.</font>");
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_XML_MORE_MENU, $"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (1)", 0, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_Bom_{HOUSE_NAME_DEFAULT} (5)", TableType.XML);

            //Export CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export CSV House BOM.</font>");

            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_CSV_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", totalAdvanceHouseBOMItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", TableType.CSV);

            //Export Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Export Excel House BOM.</font>");
            GenerateAdvanceHouseBOM(houseQuantitiesII_2_HouseBOM.communityCode + "-" + houseQuantitiesII_2_HouseBOM.communityName, Options2);
            HouseBOMDetailPage.Instance.DownloadBaseLineAdvanceHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportAdvanceHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT} (1)", totalAdvanceHouseBOMItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile($"Pipeline_HouseBomAdvance_{COMMUNITY_NAME_DEFAULT}_{HOUSE_NAME_DEFAULT}", TableType.XLSX);

            //Community House BOM
            ExtentReportsHelper.LogInformation("<font color='lavender'>Step II.1.8 Export Community House BOM.</font>");
            //+ CSV
            //+ Excel

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", community.Name);
            }
            CommunityDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();

            totalItems = CommunityHouseBOMDetailPage.Instance.GetTotalNumberItem();

            CommunityHouseBOMDetailPage.Instance.GenerateHouseBOM();
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_SpecificCommunityII_1);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_SpecificCommunityII_2);

            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportComunityFileName} (1)", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportComunityFileName} (1)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);


            //Export EXCEL House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Export EXCEL House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportComunityFileName} (1)", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportComunityFileName} (1)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);

        }
        [TearDown]
        public void DeleteData()
        {
            //Delete File House Quantities
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, housedata.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", housedata.HouseName) is true)
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", housedata.HouseName);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT);
            }


            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities1.communityCode + '-' + houseQuantities1.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
            //Delete SubComponent 
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT2_NAME_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT2_NAME_DEFAULT) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", PRODUCT2_NAME_DEFAULT);
                //Navigate To Subcomponents
                ProductDetailPage.Instance.LeftMenuNavigation("Subcomponents");

                //Create a subcomponent inside a product, remember to add dependent Option above, and check result
                ProductSubcomponentPage.Instance.ClickDeleteInGird(BuildingPhaseOfSubcomponent_II);
                string act_mess = ProductSubcomponentPage.Instance.GetLastestToastMessage();
                if (act_mess == "Successfully deleted subcomponent")
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'><b> Successfully delete {BuildingPhaseOfSubcomponent_II} subcomponent </b></font color>");
                }
                else
                    ExtentReportsHelper.LogFail($"<b> Cannot delete {BuildingPhaseOfSubcomponent_II} subcomponent </b>");
                ProductSubcomponentPage.Instance.CloseToastMessage();
            }
        }

        private void GenerateAdvanceHouseBOM(string selectedCommunity, IList<string> Options)
        {
            // Go to Advanced House BOM, select Community
            HouseBOMDetailPage.Instance.ClickOnAdvancedHouseBOMView();
            //Select Community Name
            HouseBOMDetailPage.Instance.SelectAdvanceCommunity(selectedCommunity);

            //Select Options
            HouseBOMDetailPage.Instance.SelectOptions(Options);
            HouseBOMDetailPage.Instance.GenerateAdvancedHouseBOM();
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
