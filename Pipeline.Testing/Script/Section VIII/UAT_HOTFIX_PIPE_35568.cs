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
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_VIII
{
    class UAT_HOTFIX_PIPE_35568 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VIII);
        }

        CommunityData _community;
        HouseData _housedata;
        ProductData _product1;
        ProductData _product2;

        private int totalItems;
        private string exportFileName;

        private readonly int[] indexs = new int[] { };
        private const string ImportType_1 = "Pre-Import Modification";
        private const string ImportType_2 = "Delta (As-Built)";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteSelectedQtys";

        private readonly string PARAMETER_DEFAULT_1 = "SWG";
        private readonly string PARAMETER_DEFAULT_1_VALUE_1 = "L";
        private readonly string PARAMETER_DEFAULT_1_VALUE_2 = "R";

        private readonly string PARAMETER_DEFAULT_2 = "LEVEL";

        private readonly string PARAMETER_DEFAULT_2_VALUE_1 = "5th_Floor";
        private readonly string PARAMETER_DEFAULT_2_VALUE_2 = "QQQ";
        private readonly string PARAMETER_DEFAULT_2_VALUE_3 = "3rd_Floor";


        private readonly string PARAMETER_DEFAULT_3 = "GO";
        private readonly string PARAMETER_DEFAULT_3_VALUE = "DEF";

        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_01";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House06_Automation";
        private readonly string OPTION_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private readonly string OPTION_CODE_DEFAULT = "0100";

        private readonly string PRODUCT1_DEFAULT = "QA_RT_Product1_Automation";
        private readonly string BUILDINGPHASE1_DEFAULT = "123-QA_RT_BuildingPhase1_Automation";

        private readonly string PRODUCT2_DEFAULT = "QA_Product_01_Automation";
        private readonly string BUILDINGPHASE2_DEFAULT = "QA_1-QA_BuildingPhase_01_Automation";

        private readonly string PRODUCT3_DEFAULT = "QA_Product_01_Automation";
        private readonly string BUILDINGPHASE3_DEFAULT = "QA_2-QA_BuildingPhase_02_Automation";


        //Default House Quantities
        private ProductData productData_Option_withParameter1;
        private ProductData productData_Option_withParameter2;
        private ProductData productData_Option_withParameter3;

        private ProductToOptionData productToOption_withParameter1;
        private ProductToOptionData productToOption_withParameter2;
        private ProductToOptionData productToOption_withParameter3;

        private ProductData productData_House_withParameter1;
        private ProductData productData_House_withParameter2;
        private ProductData productData_House_withParameter3;

        private ProductToOptionData productToHouse_withParameter1;
        private ProductToOptionData productToHouse_withParameter2;
        private ProductToOptionData productToHouse_withParameter3;

        private ProductToOptionData productToHouseBOM_with_SWGParameter1;
        private ProductToOptionData productToHouseBOM_with_SWGParameter2;
        private ProductToOptionData productToHouseBOM_with_SWGParameter3;

        private ProductToOptionData productToHouseBOM_with_LEVELParameter1;
        private ProductToOptionData productToHouseBOM_with_LEVELParameter2;
        private ProductToOptionData productToHouseBOM_with_LEVELParameter3;

        private ProductToOptionData productToHouseBOM_OFFParameter1;
        private ProductToOptionData productToHouseBOM_OFFParameter2;
        private ProductToOptionData productToHouseBOM_OFFParameter3;

        private HouseQuantitiesData houseQuantities_withParameter;

        private HouseQuantitiesData houseQuantities_HouseBOM_with_SWGParameter;

        private HouseQuantitiesData houseQuantities_HouseBOM_with_LEVELParameter;

        private HouseQuantitiesData houseQuantities_HouseBOM_with_OFFParameter;

        //Specific Community House Quantities
        private ProductData productData_withParameter_SpecificCommunity1;
        private ProductData productData_withParameter_SpecificCommunity2;
        private ProductData productData_withParameter_SpecificCommunity3;

        private ProductToOptionData productToOption_withParameter_SpecificCommunity1;
        private ProductToOptionData productToOption_withParameter_SpecificCommunity2;
        private ProductToOptionData productToOption_withParameter_SpecificCommunity3;

        private ProductData productData_House_withParameter_SpecificCommunity1;
        private ProductData productData_House_withParameter_SpecificCommunity2;
        private ProductData productData_House_withParameter_SpecificCommunity3;

        private ProductToOptionData productToHouse_withParameter_SpecificCommunity1;
        private ProductToOptionData productToHouse_withParameter_SpecificCommunity2;
        private ProductToOptionData productToHouse_withParameter_SpecificCommunity3;


        private ProductToOptionData productToHouseBOM_with_SWGParameter_SpecificCommunity1;
        private ProductToOptionData productToHouseBOM_with_SWGParameter_SpecificCommunity2;
        private ProductToOptionData productToHouseBOM_with_SWGParameter_SpecificCommunity3;

        private ProductToOptionData productToHouseBOM_with_LEVELParameter_SpecificCommunity1;
        private ProductToOptionData productToHouseBOM_with_LEVELParameter_SpecificCommunity2;
        private ProductToOptionData productToHouseBOM_with_LEVELParameter_SpecificCommunity3;

        private ProductToOptionData productToHouseBOM_OFFParameter_SpecificCommunity1;
        private ProductToOptionData productToHouseBOM_OFFParameter_SpecificCommunity2;
        private ProductToOptionData productToHouseBOM_OFFParameter_SpecificCommunity3;


        private HouseQuantitiesData houseQuantities_withParameter_SpecificCommunity;

        private HouseQuantitiesData houseQuantities_HouseBOM_with_SWGParameter_SpecificCommunity;

        private HouseQuantitiesData houseQuantities_HouseBOM_with_LEVELParameter_SpecificCommunity;

        private HouseQuantitiesData houseQuantities_HouseBOM_with_OFFParameter_SpecificCommunity;

        [SetUp]
        public void GetData()
        {

            _community = new CommunityData()
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
                Slug = "R-QA-Only-Community-Auto",
            };

            _housedata = new HouseData()
            {
                HouseName = "QA_RT_House06_Automation",
                SaleHouseName = "QA_RT_House06_Sales_Name",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "6000",
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

            _product1 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                BuildingPhase = BUILDINGPHASE2_DEFAULT

            };

            _product2 = new ProductData()
            {
                Name = "QA_RT_Product1_Automation"
            };

            productData_Option_withParameter1 = new ProductData()
            {
                Name = "QA_RT_Product1_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "500.00",
                Parameter = "LEVEL=QQQ"
            };

            productData_Option_withParameter2 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "500.00",
                Parameter = "LEVEL=5th_Floor|SWG=L"
            };
            productData_Option_withParameter3 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "100.00",
                Parameter = "GO=ABC|GO=DEF|LEVEL=3rd_Floor|SWG=R"
            };


            productToOption_withParameter1 = new ProductToOptionData()
            {
                BuildingPhase = "123-QA_RT_BuildingPhase1_Automation",
                ProductList = new List<ProductData> { productData_Option_withParameter1 },
                ParameterList = new List<ProductData> { productData_Option_withParameter1 }
            };

            productToOption_withParameter2 = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_withParameter2 },
                ParameterList = new List<ProductData> { productData_Option_withParameter2 }
            };

            productToOption_withParameter3 = new ProductToOptionData()
            {
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation",
                ProductList = new List<ProductData> { productData_Option_withParameter3 },
                ParameterList = new List<ProductData> { productData_Option_withParameter3 }
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_withParameter1 = new ProductData(productData_Option_withParameter1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_withParameter2 = new ProductData(productData_Option_withParameter2);

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_House_withParameter3 = new ProductData(productData_Option_withParameter3);



            productToHouse_withParameter1 = new ProductToOptionData(productToOption_withParameter1) { ProductList = new List<ProductData> { productData_House_withParameter1 } };
            productToHouse_withParameter2 = new ProductToOptionData(productToOption_withParameter2) { ProductList = new List<ProductData> { productData_House_withParameter2 } };
            productToHouse_withParameter3 = new ProductToOptionData(productToOption_withParameter3) { ProductList = new List<ProductData> { productData_House_withParameter3 } };


            // There is no House quantities 
            houseQuantities_withParameter = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_withParameter1, productToHouse_withParameter2, productToHouse_withParameter3 }
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_with_SWGParameter1 = new ProductData(productData_Option_withParameter1) { Quantities = "500.00", Parameter = "No ( SWG ) assignment" };
            ProductData productData_HouseBOM_with_SWGParameter2 = new ProductData(productData_Option_withParameter2) { Quantities = "500.00", Parameter = "SWG=L" };
            ProductData productData_HouseBOM_with_SWGParameter3 = new ProductData(productData_Option_withParameter3) { Quantities = "100.00", Parameter = "SWG=R" };

            productToHouseBOM_with_SWGParameter1 = new ProductToOptionData(productToHouse_withParameter1) { ProductList = new List<ProductData> { productData_HouseBOM_with_SWGParameter1 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_SWGParameter1 } };

            productToHouseBOM_with_SWGParameter2 = new ProductToOptionData(productToHouse_withParameter2) { ProductList = new List<ProductData> { productData_HouseBOM_with_SWGParameter2 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_SWGParameter2 } };

            productToHouseBOM_with_SWGParameter3 = new ProductToOptionData(productToHouse_withParameter3) { ProductList = new List<ProductData> { productData_HouseBOM_with_SWGParameter3 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_SWGParameter3 } };

            houseQuantities_HouseBOM_with_SWGParameter = new HouseQuantitiesData(houseQuantities_withParameter)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_with_SWGParameter1, productToHouseBOM_with_SWGParameter2, productToHouseBOM_with_SWGParameter3 },
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_with_LEVELParameter1 = new ProductData(productData_Option_withParameter1) { Quantities = "500.00", Parameter = "LEVEL=QQQ" };
            ProductData productData_HouseBOM_with_LEVELParameter2 = new ProductData(productData_Option_withParameter2) { Quantities = "500.00", Parameter = "LEVEL=5th_Floor" };
            ProductData productData_HouseBOM_with_LEVELParameter3 = new ProductData(productData_Option_withParameter3) { Quantities = "100.00", Parameter = "LEVEL=3rd_Floor" };

            productToHouseBOM_with_LEVELParameter1 = new ProductToOptionData(productToHouse_withParameter1) { ProductList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter1 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter1 } };

            productToHouseBOM_with_LEVELParameter2 = new ProductToOptionData(productToHouse_withParameter2) { ProductList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter2 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter2 } };

            productToHouseBOM_with_LEVELParameter3 = new ProductToOptionData(productToHouse_withParameter3) { ProductList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter3 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter3 } };

            houseQuantities_HouseBOM_with_LEVELParameter = new HouseQuantitiesData(houseQuantities_withParameter)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_with_LEVELParameter1, productToHouseBOM_with_LEVELParameter2, productToHouseBOM_with_LEVELParameter3 },
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_with_OFFParameter1 = new ProductData(productData_Option_withParameter1) { Quantities = "500.00", Parameter = string.Empty };
            ProductData productData_HouseBOM_with_OFFParameter2 = new ProductData(productData_Option_withParameter2) { Quantities = "500.00", Parameter = string.Empty };
            ProductData productData_HouseBOM_with_OFFParameter3 = new ProductData(productData_Option_withParameter3) { Quantities = "100.00", Parameter = string.Empty };

            productToHouseBOM_OFFParameter1 = new ProductToOptionData(productToHouse_withParameter1) { ProductList = new List<ProductData> { productData_HouseBOM_with_OFFParameter1 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_OFFParameter1 } };

            productToHouseBOM_OFFParameter2 = new ProductToOptionData(productToHouse_withParameter2) { ProductList = new List<ProductData> { productData_HouseBOM_with_OFFParameter2 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_OFFParameter2 } };

            productToHouseBOM_OFFParameter3 = new ProductToOptionData(productToHouse_withParameter3) { ProductList = new List<ProductData> { productData_HouseBOM_with_OFFParameter3 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_OFFParameter3 } };

            houseQuantities_HouseBOM_with_OFFParameter = new HouseQuantitiesData(houseQuantities_withParameter)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_OFFParameter1, productToHouseBOM_OFFParameter2, productToHouseBOM_OFFParameter3 },
            };

            //Specific Community 
            productData_withParameter_SpecificCommunity1 = new ProductData()
            {
                Name = "QA_RT_Product1_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "500.00",
                Parameter = "LEVEL=QQQ"
            };

            productData_withParameter_SpecificCommunity2 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "500.00",
                Parameter = "LEVEL=5th_Floor|SWG=L"
            };

            productData_withParameter_SpecificCommunity3 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Use = "NONE",
                Quantities = "100.00",
                Parameter = "GO=ABC|GO=DEF|LEVEL=3rd_Floor|SWG=R"
            };

            productToOption_withParameter_SpecificCommunity1 = new ProductToOptionData()
            {
                BuildingPhase = "123-QA_RT_BuildingPhase1_Automation",
                ProductList = new List<ProductData> { productData_Option_withParameter1 },
                ParameterList = new List<ProductData> { productData_Option_withParameter1 }
            };

            productToOption_withParameter_SpecificCommunity2 = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { productData_withParameter_SpecificCommunity2 },
                ParameterList = new List<ProductData> { productData_withParameter_SpecificCommunity2 }
            };

            productToOption_withParameter_SpecificCommunity3 = new ProductToOptionData()
            {
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation",
                ProductList = new List<ProductData> { productData_withParameter_SpecificCommunity3 },
                ParameterList = new List<ProductData> { productData_withParameter_SpecificCommunity3 }
            };

            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_withParameter_SpecificCommunity1 = new ProductData(productData_withParameter_SpecificCommunity1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_withParameter_SpecificCommunity2 = new ProductData(productData_withParameter_SpecificCommunity2);

            // House quantities 3 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_withParameter_SpecificCommunity3 = new ProductData(productData_withParameter_SpecificCommunity3);

            productToHouse_withParameter_SpecificCommunity1 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity1) { ProductList = new List<ProductData> { productData_House_withParameter_SpecificCommunity1 } };
            productToHouse_withParameter_SpecificCommunity2 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity2) { ProductList = new List<ProductData> { productData_House_withParameter_SpecificCommunity2 } };
            productToHouse_withParameter_SpecificCommunity3 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity3) { ProductList = new List<ProductData> { productData_House_withParameter_SpecificCommunity3 } };

            // There is no House quantities 
            houseQuantities_withParameter_SpecificCommunity = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_withParameter_SpecificCommunity1, productToHouse_withParameter_SpecificCommunity2, productToHouse_withParameter_SpecificCommunity3 }
            };



            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_with_SWGParameter_SpecificCommunit1 = new ProductData(productData_withParameter_SpecificCommunity1) { Quantities = "500.00", Parameter = "No ( SWG ) assignment" };
            ProductData productData_HouseBOM_with_SWGParameter_SpecificCommunit2 = new ProductData(productData_withParameter_SpecificCommunity2) { Quantities = "500.00", Parameter = "SWG=L" };
            ProductData productData_HouseBOM_with_SWGParameter_SpecificCommunit3 = new ProductData(productData_withParameter_SpecificCommunity3) { Quantities = "100.00", Parameter = "SWG=R" };

            productToHouseBOM_with_SWGParameter_SpecificCommunity1 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity1) { ProductList = new List<ProductData> { productData_HouseBOM_with_SWGParameter_SpecificCommunit1 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_SWGParameter_SpecificCommunit1 } };

            productToHouseBOM_with_SWGParameter_SpecificCommunity2 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity2) { ProductList = new List<ProductData> { productData_HouseBOM_with_SWGParameter_SpecificCommunit2 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_SWGParameter_SpecificCommunit2 } };

            productToHouseBOM_with_SWGParameter_SpecificCommunity3 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity3) { ProductList = new List<ProductData> { productData_HouseBOM_with_SWGParameter_SpecificCommunit3 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_SWGParameter_SpecificCommunit3 } };

            houseQuantities_HouseBOM_with_SWGParameter_SpecificCommunity = new HouseQuantitiesData(houseQuantities_withParameter_SpecificCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_with_SWGParameter_SpecificCommunity1, productToHouseBOM_with_SWGParameter_SpecificCommunity2, productToHouseBOM_with_SWGParameter_SpecificCommunity3 },
            };

            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_with_LEVELParameter_SpecificCommunit1 = new ProductData(productData_withParameter_SpecificCommunity1) { Quantities = "500.00", Parameter = "LEVEL=QQQ" };
            ProductData productData_HouseBOM_with_LEVELParameter_SpecificCommunit2 = new ProductData(productData_withParameter_SpecificCommunity2) { Quantities = "500.00", Parameter = "LEVEL=5th_Floor" };
            ProductData productData_HouseBOM_with_LEVELParameter_SpecificCommunit3 = new ProductData(productData_withParameter_SpecificCommunity3) { Quantities = "100.00", Parameter = "LEVEL=3rd_Floor" };

            productToHouseBOM_with_LEVELParameter_SpecificCommunity1 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity1) { ProductList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter_SpecificCommunit1 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter_SpecificCommunit1 } };

            productToHouseBOM_with_LEVELParameter_SpecificCommunity2 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity2) { ProductList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter_SpecificCommunit2 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter_SpecificCommunit2 } };

            productToHouseBOM_with_LEVELParameter_SpecificCommunity3 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity3) { ProductList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter_SpecificCommunit3 }, ParameterList = new List<ProductData> { productData_HouseBOM_with_LEVELParameter_SpecificCommunit3 } };

            houseQuantities_HouseBOM_with_LEVELParameter_SpecificCommunity = new HouseQuantitiesData(houseQuantities_withParameter_SpecificCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_with_LEVELParameter_SpecificCommunity1, productToHouseBOM_with_LEVELParameter_SpecificCommunity2, productToHouseBOM_with_LEVELParameter_SpecificCommunity3 },
            };

            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_OFFParameter_SpecificCommunit1 = new ProductData(productData_withParameter_SpecificCommunity1) { Quantities = "500.00", Parameter = string.Empty };
            ProductData productData_HouseBOM_OFFParameter_SpecificCommunit2 = new ProductData(productData_withParameter_SpecificCommunity2) { Quantities = "500.00", Parameter = string.Empty };
            ProductData productData_HouseBOM_OFFParameter_SpecificCommunit3 = new ProductData(productData_withParameter_SpecificCommunity3) { Quantities = "100.00", Parameter = string.Empty };

            productToHouseBOM_OFFParameter_SpecificCommunity1 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity1) { ProductList = new List<ProductData> { productData_HouseBOM_OFFParameter_SpecificCommunit1 }, ParameterList = new List<ProductData> { productData_HouseBOM_OFFParameter_SpecificCommunit1 } };

            productToHouseBOM_OFFParameter_SpecificCommunity2 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity2) { ProductList = new List<ProductData> { productData_HouseBOM_OFFParameter_SpecificCommunit2 }, ParameterList = new List<ProductData> { productData_HouseBOM_OFFParameter_SpecificCommunit2 } };

            productToHouseBOM_OFFParameter_SpecificCommunity3 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity3) { ProductList = new List<ProductData> { productData_HouseBOM_OFFParameter_SpecificCommunit3 }, ParameterList = new List<ProductData> { productData_HouseBOM_OFFParameter_SpecificCommunit3 } };

            houseQuantities_HouseBOM_with_OFFParameter_SpecificCommunity = new HouseQuantitiesData(houseQuantities_withParameter_SpecificCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_OFFParameter_SpecificCommunity1, productToHouseBOM_OFFParameter_SpecificCommunity2, productToHouseBOM_OFFParameter_SpecificCommunity3 },
            };


        }

        [Test]
        [Category("Section_VIII")]
        public void UAT_HotFix_HouseQuanQty_Import_Custom_Parameter_Feature_Is_Broken_For_HouseQty_Imports()
        {
            // 	Verify the value parameter show correctly with pre-import file with generate report view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Check setting.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Navigate to Settings > Group by Parameters settings is turned on.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();

            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            string settingBOM_url = SettingPage.Instance.CurrentURL;
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change House BOM Product Orientation is turned false.</b></font>");
            BOMSettingPage.Instance.Check_House_BOM_Product_Orientation(false);
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT_1);
            
            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();

            //1.Prepair file import (house qty has multiple custom parameters inside, separated by | character)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.Prepair file import (house qty has multiple custom parameters inside, separated by | character).</b></font>");
            //Go to Product Detail >> Check Default BuildingPhase In Product Detail
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Check Default BuildingPhase In Product Detail.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product1.Name);
                // b.Manufacturers and Style
                ExtentReportsHelper.LogInformation("<b>Create Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_BuildingPhase_01_Automation") is false)
                {
                    ProductDetailPage.Instance.AddBuildingPhases(BUILDINGPHASE2_DEFAULT, true, "Phase");
                }
                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_BuildingPhase_02_Automation") is false)
                {
                    ProductDetailPage.Instance.AddBuildingPhases(BUILDINGPHASE3_DEFAULT, false, "Phase");
                }
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, _product2.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", _product2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", _product2.Name);
                // b.Manufacturers and Style
                ExtentReportsHelper.LogInformation("<b>Create Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_BuildingPhase1_Automation") is false)
                {
                    ProductDetailPage.Instance.AddBuildingPhases(BUILDINGPHASE1_DEFAULT, true, "Phase");
                }
            }


            //Navigate to House default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House default page.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {_housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _housedata.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", _housedata.HouseName) is true)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(_housedata);
                string updateMsg = $"House {_housedata.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            else
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", _housedata.HouseName);

            }

            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", _community.Name) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(_community.Name, indexs);
            }

            //Step I.2. Import file with generate report view and default community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2. Import file with generate report view and default community.</b></font>");
            HouseCommunities.Instance.LeftMenuNavigation("Import");

            //Import House Quantities
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(ImportType_1, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_35568.xml");

            //I.3. On the Import quantities: Check value parameter
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.3. On the Import quantities: Check value parameter.</b></font>");

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT2_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_2, PARAMETER_DEFAULT_2_VALUE_1) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT2_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_2} And Value {PARAMETER_DEFAULT_2_VALUE_1}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT2_DEFAULT} is imported unsuccessfully with Parameter {PARAMETER_DEFAULT_2}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_2, PARAMETER_DEFAULT_2_VALUE_2) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_2} And Value {PARAMETER_DEFAULT_2_VALUE_2}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT3_DEFAULT} is imported unsuccessfully with Parameter {PARAMETER_DEFAULT_2}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT1_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_2, PARAMETER_DEFAULT_2_VALUE_3) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT1_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_2} And Value {PARAMETER_DEFAULT_2_VALUE_3}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT1_DEFAULT} is imported unsuccessfully with Parameter {PARAMETER_DEFAULT_2}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_3, PARAMETER_DEFAULT_3_VALUE) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_3} And Value {PARAMETER_DEFAULT_3_VALUE}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_3}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT2_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.ScrollLeftToOffSetWidth();
                if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_1, PARAMETER_DEFAULT_1_VALUE_1) is true
                    && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_1, PARAMETER_DEFAULT_1_VALUE_2) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT2_DEFAULT} and {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_1} Has Value {PARAMETER_DEFAULT_1_VALUE_1} and {PARAMETER_DEFAULT_1_VALUE_2}.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT2_DEFAULT} and {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_1} Has Value {PARAMETER_DEFAULT_1_VALUE_1} and {PARAMETER_DEFAULT_1_VALUE_2}.</font>");
                }
            }

            HouseImportDetailPage.Instance.ImportFileIntoHouseQuantitiesAfterUploadFileInGrid("ImportHouseQuantities_DefaultCommunity_PIPE_35568.xml");

            //I.4. Import success and go to the Quantities page: check data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.4. Import success and go to the Quantities page: check data</font>");
            //Verify the set up data for product quantities on House

            HouseCommunities.Instance.LeftMenuNavigation("Quantities");

            foreach (ProductToOptionData housequantity in houseQuantities_withParameter.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_withParameter.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", item.Parameter) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_withParameter.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }


            //Navigate To House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Navigate To House BOM.</b></font>");
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the url of House BOM
            string HouseBOM_url = HouseBOMDetailPage.Instance.CurrentURL;

            //Step I.5: Generate BOM: setting “Group by Parameter:”= ON with value is SWG”.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step I.5: Generate BOM: setting “Group by Parameter:”= ON with value is SWG”.</b></font>");
            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT_1);
            CommonHelper.OpenURL(HouseBOM_url);

            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_with_SWGParameter.communityCode + "-" + houseQuantities_HouseBOM_with_SWGParameter.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_with_SWGParameter.communityCode + "-" + houseQuantities_HouseBOM_with_SWGParameter.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGridWithParameter(houseQuantities_HouseBOM_with_SWGParameter);

            //Step I.6: Change setting “Group by Parameter:”= ON with value is LEVEL”.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.6: Change setting “Group by Parameter:”= ON with value is LEVEL”.</b></font>");

            //Navigate To Setting Page Set Parameter 'LEVEL'
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate To Setting Page Set Parameter 'LEVEL'.</b></font>");
            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT_2);

            //Back to House BOM
            CommonHelper.OpenURL(HouseBOM_url);
            //Generate House BOM and verify it

            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_with_LEVELParameter.communityCode + "-" + houseQuantities_HouseBOM_with_LEVELParameter.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_with_LEVELParameter.communityCode + "-" + houseQuantities_HouseBOM_with_LEVELParameter.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGridWithParameter(houseQuantities_HouseBOM_with_LEVELParameter);

            //Step I.7: Change setting “Group by Parameter:”= OFF  and generate BOM.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step I.7: Change setting “Group by Parameter:”= OFF  and generate BOM.</b></font>");

            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);

            CommonHelper.OpenURL(HouseBOM_url);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_with_OFFParameter.communityCode + "-" + houseQuantities_HouseBOM_with_OFFParameter.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_with_OFFParameter.communityCode + "-" + houseQuantities_HouseBOM_with_OFFParameter.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_with_OFFParameter);

            //II.1. Prepair file import (house qty has multiple custom parameters inside, separated by | character)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step II.1. Prepair file import (house qty has multiple custom parameters inside, separated by | character).</b></font>");
            // Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //II.2. Import file with generate report view and Specific community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step II.2.Import file with generate report view and Specific community.</b></font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(ImportType_1, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_35568.xml");

            //II.3. On the Import quantities: Check value parameter
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step II.3. On the Import quantities: Check value parameter.</b></font>");

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT2_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_2, PARAMETER_DEFAULT_2_VALUE_1) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT2_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_2} And Value {PARAMETER_DEFAULT_2_VALUE_1}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT2_DEFAULT} is imported unsuccessfully with Parameter {PARAMETER_DEFAULT_2}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_2, PARAMETER_DEFAULT_2_VALUE_2) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_2} And Value {PARAMETER_DEFAULT_2_VALUE_2}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT3_DEFAULT} is imported unsuccessfully with Parameter {PARAMETER_DEFAULT_2}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT1_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_2, PARAMETER_DEFAULT_2_VALUE_3) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT1_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_2} And Value {PARAMETER_DEFAULT_2_VALUE_3}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT1_DEFAULT} is imported unsuccessfully with Parameter {PARAMETER_DEFAULT_2}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_3, PARAMETER_DEFAULT_3_VALUE) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_3} And Value {PARAMETER_DEFAULT_3_VALUE}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_3}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT2_DEFAULT) is true
            && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.ScrollLeftToOffSetWidth();
                if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_1, PARAMETER_DEFAULT_1_VALUE_1) is true
                    && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT_1, PARAMETER_DEFAULT_1_VALUE_2) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT2_DEFAULT} and {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_1} Has Value {PARAMETER_DEFAULT_1_VALUE_1} and {PARAMETER_DEFAULT_1_VALUE_2}.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT2_DEFAULT} and {PRODUCT3_DEFAULT} is imported successfully with Parameter {PARAMETER_DEFAULT_1} Has Value {PARAMETER_DEFAULT_1_VALUE_1} and {PARAMETER_DEFAULT_1_VALUE_2}.</font>");
                }
            }

            HouseImportDetailPage.Instance.ImportFileIntoHouseQuantitiesAfterUploadFileInGrid("ImportHouseQuantities_SpecificCommunity_PIPE_35568.xml");

            //II.4. Import success and go to the Quantities page: check data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II.4. Import success and go to the Quantities page: check data</font>");

            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the set up data for product quantities on House</font>");

            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");

            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_withParameter_SpecificCommunity.communityCode + "-" + houseQuantities_withParameter_SpecificCommunity.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities_withParameter_SpecificCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_withParameter_SpecificCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", item.Parameter) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_withParameter_SpecificCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }

            //Navigate To House BOM
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("House BOM");

            //II.5: Generate BOM: setting “Group by Parameter:”= ON with value is SWG”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step II.5. Generate BOM: setting “Group by Parameter:”= ON with value is SWG”.</b></font>");
            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT_1);

            CommonHelper.OpenURL(HouseBOM_url);
            //5.Generate BOM: setting “Group by Parameter:”= ON with value is SWG”
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_with_SWGParameter_SpecificCommunity.communityCode + "-" + houseQuantities_HouseBOM_with_SWGParameter_SpecificCommunity.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_with_SWGParameter_SpecificCommunity.communityCode + "-" + houseQuantities_HouseBOM_with_SWGParameter_SpecificCommunity.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGridWithParameter(houseQuantities_HouseBOM_with_SWGParameter_SpecificCommunity);

            //II.6 Change setting “Group by Parameter:”= ON with value is LEVEL”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step II.6 Change setting “Group by Parameter:”= ON with value is LEVEL”.</b></font>");

            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT_2);

            CommonHelper.OpenURL(HouseBOM_url);
            HouseBOMDetailPage.Instance.LeftMenuNavigation("Quantities");

            HouseQuantitiesDetailPage.Instance.DeleteSelectedHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
            
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("House BOM");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_with_LEVELParameter_SpecificCommunity.communityCode + "-" + houseQuantities_HouseBOM_with_LEVELParameter_SpecificCommunity.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_with_LEVELParameter_SpecificCommunity.communityCode + "-" + houseQuantities_HouseBOM_with_LEVELParameter_SpecificCommunity.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGridWithParameter(houseQuantities_HouseBOM_with_LEVELParameter_SpecificCommunity);

            //II.7. Change setting “Group by Parameter:”= OFF  and generate BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step II.7. Change setting “Group by Parameter:”= OFF  and generate BOM.</b></font>");

            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);

            CommonHelper.OpenURL(HouseBOM_url);
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_with_OFFParameter_SpecificCommunity.communityCode + "-" + houseQuantities_HouseBOM_with_OFFParameter_SpecificCommunity.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_with_OFFParameter_SpecificCommunity.communityCode + "-" + houseQuantities_HouseBOM_with_OFFParameter_SpecificCommunity.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_with_OFFParameter_SpecificCommunity);

        }


        [TearDown]
        public void DeleteData()
        {

            //Delete File House Quantities
            HouseBOMDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteSelectedHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_withParameter_SpecificCommunity.communityCode + '-' + houseQuantities_withParameter_SpecificCommunity.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteSelectedHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

        }

    }
}
