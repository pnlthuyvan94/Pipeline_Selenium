using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;
using NUnit.Framework;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule;
using Pipeline.Testing.Script.TestData;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_Q_PIPE_35652 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        HouseImportQuantitiesData _houseImportQuantitiesData1;
        HouseImportQuantitiesData _houseImportQuantitiesData2;
        HouseImportQuantitiesData _houseImportQuantitiesData3;

        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData1_i;
        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData2_i;
        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData3_i;

        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData1_ii;
        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData2_ii;
        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData3_ii;


        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData1_iii;
        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData2_iii;
        QuantityBuildingPhaseRuleData _quantityBuildingPhaseRuleData3_iii;


        private const string COMMUNITY_CODE_DEFAULT = TestDataCommon.COMMUNITY_CODE_DEFAULT;
        private const string COMMUNITY_NAME_DEFAULT = TestDataCommon.COMMUNITY_NAME_DEFAULT;

        private const string HOUSE_NAME_DEFAULT = TestDataCommon.HOUSE_NAME_DEFAULT;

        private const string OPTION1_NAME_DEFAULT = TestDataCommon.OPTION_NAME_DEFAULT;
        private const string OPTION1_CODE_DEFAULT = TestDataCommon.OPTION_CODE_DEFAULT;

        private const string OPTION2_NAME_DEFAULT = "BASE, QA_RT_Option_Automation";

        private const string OPTION3_NAME_DEFAULT = TestDataCommon.OPTION_NAME_DEFAULT;

        private const string PARAMETER_DEFAULT = "LEVEL";

        private readonly string PRODUCT1_DEFAULT = "QA_RT_New_Prod_301";
        private readonly string PRODUCT2_DEFAULT = "QA_RT_New_Prod_302";
        private readonly string PRODUCT3_DEFAULT = "QA_RT_New_Prod_303";
        private readonly string PRODUCT4_DEFAULT = "QA_RT_New_Prod_304";
        private readonly string PRODUCT5_DEFAULT = "QA_RT_New_Prod_305";


        private readonly string BUILDINGPHASE1_DEFAULT = "301-QA_RT_Building_Phase_01_Automation";
        private readonly string BUILDINGPHASE2_DEFAULT = "302-QA_RT_Building_Phase_02_Automation";
        private readonly string BUILDINGPHASE3_DEFAULT = "303-QA_RT_Building_Phase_03_Automation";

        private readonly string BUILDINGPHASE4_DEFAULT = "304-QA_RT_Building_Phase_04_Automation";
        private readonly string BUILDINGPHASE5_DEFAULT = "305-QA_RT_Building_Phase_05_Automation";

        private readonly string PRODUCT1_DESCRIPTION_DEFAULT = "Product Description 301";
        private readonly string PRODUCT2_DESCRIPTION_DEFAULT = "Product Description 302";
        private readonly string PRODUCT3_DESCRIPTION_DEFAULT = "Product Description 303";
        private readonly string PRODUCT4_DESCRIPTION_DEFAULT = "Product Description 304_New";
        private readonly string PRODUCT5_DESCRIPTION_DEFAULT = "";


        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";
        private const string ImportType_1 = "Pre-Import Modification";
        private const string ImportType_2 = "Delta (As-Built)";
        private const string ImportType_3 = "CSV";

        private ProductData productData_Option_1;
        private ProductData productData_Option_2;
        private ProductData productData_Option_3;
        private ProductData productData_Option_4;
        private ProductData productData_Option_5;

        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToOption3;
        private ProductToOptionData productToOption4;
        private ProductToOptionData productToOption5;

        private ProductData productData_House_1;
        private ProductData productData_House_2;
        private ProductData productData_House_3;
        private ProductData productData_House_4;
        private ProductData productData_House_5;

        private ProductData productData_House_1_ii;
        private ProductData productData_House_2_ii;
        private ProductData productData_House_3_ii;
        private ProductData productData_House_4_ii;
        private ProductData productData_House_5_ii;

        private ProductData productData_House_1_iii;
        private ProductData productData_House_2_iii;
        private ProductData productData_House_3_iii;
        private ProductData productData_House_4_iii;
        private ProductData productData_House_5_iii;

        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToHouse3;
        private ProductToOptionData productToHouse4;
        private ProductToOptionData productToHouse5;

        private ProductToOptionData productToHouse1_ii;
        private ProductToOptionData productToHouse2_ii;
        private ProductToOptionData productToHouse3_ii;
        private ProductToOptionData productToHouse4_ii;
        private ProductToOptionData productToHouse5_ii;

        private ProductToOptionData productToHouse1_iii;
        private ProductToOptionData productToHouse2_iii;
        private ProductToOptionData productToHouse3_iii;
        private ProductToOptionData productToHouse4_iii;
        private ProductToOptionData productToHouse5_iii;


        private HouseQuantitiesData houseQuantities_ImportType_1;
        private HouseQuantitiesData houseQuantities_ImportType_2;
        private HouseQuantitiesData houseQuantities_ImportType_3;

        [SetUp]
        public void GetTestData()
        {

            _quantityBuildingPhaseRuleData1_i = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "1ST_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE1_DEFAULT,
                NewBuildingPhase = "307-QA_RT_Building_Phase_07_Automation",
            };

            _quantityBuildingPhaseRuleData2_i = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "2ND_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE2_DEFAULT,
                NewBuildingPhase = "308-QA_RT_Building_Phase_08_Automation",
            };

            _quantityBuildingPhaseRuleData3_i = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "3ND_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE3_DEFAULT,
                NewBuildingPhase = "309-QA_RT_Building_Phase_09_Automation",
            };


            _quantityBuildingPhaseRuleData1_ii = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "1ST_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE1_DEFAULT,
                NewBuildingPhase = "310-QA_RT_Building_Phase_10_Automation",
            };

            _quantityBuildingPhaseRuleData2_ii = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "2ND_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE2_DEFAULT,
                NewBuildingPhase = "310-QA_RT_Building_Phase_10_Automation",
            };

            _quantityBuildingPhaseRuleData3_ii = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "3ND_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE3_DEFAULT,
                NewBuildingPhase = "310-QA_RT_Building_Phase_10_Automation",
            };


            _quantityBuildingPhaseRuleData1_iii = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "1ST_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE1_DEFAULT,
                NewBuildingPhase = "311-QA_RT_Building_Phase_11_Automation",
            };

            _quantityBuildingPhaseRuleData2_iii = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "2ND_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE2_DEFAULT,
                NewBuildingPhase = "311-QA_RT_Building_Phase_11_Automation",
            };

            _quantityBuildingPhaseRuleData3_iii = new QuantityBuildingPhaseRuleData()
            {
                Priority = "0",
                LevelCondition = "3ND_FLOOR",
                OriginalBuildingPhase = BUILDINGPHASE3_DEFAULT,
                NewBuildingPhase = "311-QA_RT_Building_Phase_11_Automation",
            };

            _houseImportQuantitiesData1 = new HouseImportQuantitiesData()
            {
                Products = new List<string>() { PRODUCT4_DEFAULT, PRODUCT5_DEFAULT },
                BuildingPhases = new List<string>() { "307 - QA_RT_Building_Phase_07_Automation", "308 - QA_RT_Building_Phase_08_Automation", "309 - QA_RT_Building_Phase_09_Automation" },
                Description = new List<string>() { PRODUCT1_DESCRIPTION_DEFAULT, PRODUCT2_DESCRIPTION_DEFAULT, PRODUCT3_DESCRIPTION_DEFAULT },
            };

            _houseImportQuantitiesData2 = new HouseImportQuantitiesData()
            {
                BuildingPhases = new List<string>() { "310 - QA_RT_Building_Phase_10_Automation", "310 - QA_RT_Building_Phase_10_Automation", "310 - QA_RT_Building_Phase_10_Automation" },
                Description = new List<string>() { PRODUCT1_DESCRIPTION_DEFAULT, PRODUCT2_DESCRIPTION_DEFAULT, PRODUCT3_DESCRIPTION_DEFAULT },
            };

            _houseImportQuantitiesData3 = new HouseImportQuantitiesData()
            {
                BuildingPhases = new List<string>() { "311 - QA_RT_Building_Phase_11_Automation", "311 - QA_RT_Building_Phase_11_Automation", "311 - QA_RT_Building_Phase_11_Automation" },
                Description = new List<string>() { PRODUCT1_DESCRIPTION_DEFAULT, PRODUCT2_DESCRIPTION_DEFAULT, PRODUCT3_DESCRIPTION_DEFAULT },
            };

            productData_Option_1 = new ProductData()
            {
                Name = "QA_RT_New_Prod_301",
                Description = "Product Description 301",
                Use = "NONE",
                Unit = "",
                Manufacture = "QA_RT_Manufacturer_Automation",
                Style = "QA_RT_Style_Automation",
                RoundingRule = "Standard Rounding",
                BuildingPhase = "301-QA_RT_Building_Phase_01_Automation"
            };

            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_New_Prod_302",
                Description = "Product Description 302",
                Use = "NONE",
                Unit = "",
                Manufacture = "QA_RT_Manufacturer_Automation",
                Style = "QA_RT_Style_Automation",
                RoundingRule = "Standard Rounding",
                BuildingPhase = "302-QA_RT_Building_Phase_02_Automation"
            };

            productData_Option_3 = new ProductData()
            {
                Name = "QA_RT_New_Prod_303",
                Description = "Product Description 303",
                Use = "NONE",
                Unit = "",
                Manufacture = "QA_RT_Manufacturer_Automation",
                Style = "QA_RT_Style_Automation",
                RoundingRule = "Standard Rounding",
                BuildingPhase = "303-QA_RT_Building_Phase_03_Automation"
            };

            productData_Option_4 = new ProductData()
            {
                Name = "QA_RT_New_Prod_304",
                Use = "NONE",
                Manufacture = "QA_RT_Manufacturer_Automation",
                Style = "QA_RT_Style_Automation",
                Description = "Product Description 304_New"
            };

            productData_Option_5 = new ProductData()
            {
                Name = "QA_RT_New_Prod_305",
                Use = "NONE",
                Manufacture = "QA_RT_Manufacturer_Automation",
                Style = "QA_RT_Style_Automation",
                Description = string.Empty
            };


            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "307-QA_RT_Building_Phase_07_Automation",
                ProductList = new List<ProductData> { productData_Option_1 }
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "308-QA_RT_Building_Phase_08_Automation",
                ProductList = new List<ProductData> { productData_Option_2 }
            };

            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "309-QA_RT_Building_Phase_09_Automation",
                ProductList = new List<ProductData> { productData_Option_3 }
            };

            productToOption4 = new ProductToOptionData()
            {
                BuildingPhase = BUILDINGPHASE4_DEFAULT,
                ProductList = new List<ProductData> { productData_Option_4 }
            };

            productToOption5 = new ProductToOptionData()
            {
                BuildingPhase = BUILDINGPHASE5_DEFAULT,
                ProductList = new List<ProductData> { productData_Option_5 }
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2);

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_House_3 = new ProductData(productData_Option_3);

            // House quantities 4 will be same as option quantities 4 but diffirent 'Style' and 'Quantities' fields
            productData_House_4 = new ProductData(productData_Option_4);

            // House quantities 4 will be same as option quantities 5 but diffirent 'Style' and 'Quantities' fields
            productData_House_5 = new ProductData(productData_Option_5);

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };
            productToHouse3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3 } };
            productToHouse4 = new ProductToOptionData(productToOption4) { ProductList = new List<ProductData> { productData_House_4 } };
            productToHouse5 = new ProductToOptionData(productToOption5) { ProductList = new List<ProductData> { productData_House_5 } };


            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1_ii = new ProductData(productData_Option_1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2_ii = new ProductData(productData_Option_2);

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_House_3_ii = new ProductData(productData_Option_3);

            // House quantities 4 will be same as option quantities 4 but diffirent 'Style' and 'Quantities' fields
            productData_House_4_ii = new ProductData(productData_Option_4) { Description = string.Empty };

            // House quantities 4 will be same as option quantities 5 but diffirent 'Style' and 'Quantities' fields
            productData_House_5_ii = new ProductData(productData_Option_5);

            // House quantities 2 will be same as option quantities 1 but diffirent 'Quantities' field
            productToHouse1_ii = new ProductToOptionData(productToOption1) { BuildingPhase = "310-QA_RT_Building_Phase_10_Automation", ProductList = new List<ProductData> { productData_House_1_ii } };
            productToHouse2_ii = new ProductToOptionData(productToOption2) { BuildingPhase = "310-QA_RT_Building_Phase_10_Automation", ProductList = new List<ProductData> { productData_House_2_ii } };
            productToHouse3_ii = new ProductToOptionData(productToOption3) { BuildingPhase = "310-QA_RT_Building_Phase_10_Automation", ProductList = new List<ProductData> { productData_House_3_ii } };
            productToHouse4_ii = new ProductToOptionData(productToOption4) { ProductList = new List<ProductData> { productData_House_4_ii } };
            productToHouse5_ii = new ProductToOptionData(productToOption5) { ProductList = new List<ProductData> { productData_House_5_ii } };


            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1_iii = new ProductData(productData_Option_1);

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2_iii = new ProductData(productData_Option_2);

            // House quantities 3 will be same as option quantities 3 but diffirent 'Style' and 'Quantities' fields
            productData_House_3_iii = new ProductData(productData_Option_3);

            // House quantities 4 will be same as option quantities 4 but diffirent 'Style' and 'Quantities' fields
            productData_House_4_iii = new ProductData(productData_Option_4) { Description = string.Empty };

            // House quantities 4 will be same as option quantities 5 but diffirent 'Style' and 'Quantities' fields
            productData_House_5_iii = new ProductData(productData_Option_5);

            // House quantities 3 will be same as option quantities 1 but diffirent 'Quantities' field
            productToHouse1_iii = new ProductToOptionData(productToOption1) { BuildingPhase = "311-QA_RT_Building_Phase_11_Automation", ProductList = new List<ProductData> { productData_House_1_iii } };
            productToHouse2_iii = new ProductToOptionData(productToOption2) { BuildingPhase = "311-QA_RT_Building_Phase_11_Automation", ProductList = new List<ProductData> { productData_House_2_iii } };
            productToHouse3_iii = new ProductToOptionData(productToOption3) { BuildingPhase = "311-QA_RT_Building_Phase_11_Automation", ProductList = new List<ProductData> { productData_House_3_iii } };
            productToHouse4_iii = new ProductToOptionData(productToOption4) { ProductList = new List<ProductData> { productData_House_4_iii } };
            productToHouse5_iii = new ProductToOptionData(productToOption5) { ProductList = new List<ProductData> { productData_House_5_iii } };

            // There is no House quantities 
            houseQuantities_ImportType_1 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2, productToHouse3, productToHouse4, productToHouse5 }
            };

            houseQuantities_ImportType_2 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1_ii, productToHouse2_ii, productToHouse3_ii, productToHouse4_ii, productToHouse5_ii }
            };
            houseQuantities_ImportType_3 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1_iii, productToHouse2_iii, productToHouse3_iii, productToHouse4_iii, productToHouse5_iii }
            };

        }
        [Test]
        [Category("Section_IV")]
        public void A04_Q_Assets_DetailPage_Houses_ImportPage_HouseQuantitiesImport_Descriptions_of_the_key_measures_and_phases_on_house_import_process()
        {

            //Group by Parameters settings is turned false
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Group by Parameters settings is turned false.</font><b>");
            //Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            string settingBOM_url = SettingPage.Instance.CurrentURL;
            BOMSettingPage.Instance.SelectGroupByParameter(false, PARAMETER_DEFAULT);

            //1.Verify user the descriptions of the key measures (Products) show when import with the pre-import file (generate report view +Specific community)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.Verify user the descriptions of the key measures (Products) show when import with the pre-import file (generate report view +Specific community).</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_1.Name);

                // Verify new Product in header
                if (ProductDetailPage.Instance.IsCreateSuccessfully(productData_Option_1) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The new successful Product {productData_Option_1.Name} is not show correctly all data.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>The new successful Product {productData_Option_1.Name}is show correctly all data.</b></font>");
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_2.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_2.Name);
                // Verify new Product in header
                if (ProductDetailPage.Instance.IsCreateSuccessfully(productData_Option_2) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The new successful Product {productData_Option_2.Name} is not show correctly all data.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>The new successful Product {productData_Option_2.Name}is show correctly all data.</b></font>");
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_3.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_3.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_3.Name);
                // Verify new Product in header
                if (ProductDetailPage.Instance.IsCreateSuccessfully(productData_Option_3) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The new successful Product {productData_Option_3.Name} is not show correctly all data.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>The new successful Product {productData_Option_3.Name}is show correctly all data.</b></font>");
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_4.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_4.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_4.Name} is not present in grid.</b>");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_4.Name} is present in grid.</b>");
                ProductPage.Instance.DeleteProduct(productData_Option_4.Name);
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_5.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_5.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_5.Name} is not present in grid.</b>");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_5.Name} is present in grid.</b>");
                ProductPage.Instance.DeleteProduct(productData_Option_5.Name);
            }

            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.Check_Prompt_Building_Phase_Add_to_Products(true);

            //Go to the building phase rule and setup data
            QuantityBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.QuanttityPhaseRules);

            // Insert name to filter and click filter by Contain value
            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData1_i.NewBuildingPhase);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData1_i.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_i.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData1_i.OriginalBuildingPhase} is display on grid.</font>");
            }
            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();
                if (QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase Rules modal is not displayed.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>New Building Phase Rules modal is displayed</b></font>");
                }
                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData1_i);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData1_i.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData1_i.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData1_i.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData1_i.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData1_i.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData1_i.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData1_i.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }

            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData2_i.NewBuildingPhase);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData2_i.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_i.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData2_i.OriginalBuildingPhase} is display on grid.</font>");
            }
            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();

                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData2_i);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData2_i.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData2_i.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData2_i.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData2_i.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData2_i.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData2_i.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData2_i.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }

            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData3_i.NewBuildingPhase);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData3_i.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_i.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData3_i.OriginalBuildingPhase} is display on grid.</font>");
            }

            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();

                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData3_i);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData3_i.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData3_i.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData3_i.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData3_i.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData3_i.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData3_i.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData3_i.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }



            //1.1 Go to House detail/Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House default page.</font><b>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            // Hover over Assets  > click Houses then click a House that will be used for testing.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Hover over Assets  > click Houses then click a House that will be used for testing..</font><b>");
            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }

            string HouseDetail_url = HouseDetailPage.Instance.CurrentURL;

            //Go to Assets/Houses/House detail/Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.1: Go to Assets/Houses/House detail/Import.font>");
            //Once navigated to House Details page click House BOM tab in the left nav panel.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Once navigated to House Details page click House BOM tab in the left nav panel.</b></font>");
            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION1_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION1_NAME_DEFAULT + " - " + OPTION1_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Community.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            //1.2 Upload file with mode pre-import type
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.2 Upload file with mode pre-import type.</font>");
            //1.3 Import file with a specific community and generate report view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.3 Import file with a specific community and generate report view.</font>");
            //1.4 Check the description on the import page.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.4 Check the description on the import page..</font>");
            //1.4.1 Import Quantities: On this page will get description form the import file if the file is empty value then get from the product detail page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.4.1 Import Quantities: On this page will get description form the import file if the file is empty value then get from the product detail page.</font>");
            //1.4.2 Validate Products: New product so it will get the description field form the import file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.4.2 Validate Products: New product so it will get the description field form the import file.</font>");
            //1.4.3 Building phase Assignment Not Found: The Description get from the product detail
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.4.3 Building phase Assignment Not Found: The Description get from the product detail.</font>");

            //Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Processing the import with specific community.</font>");
            //Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", "BASE" + "," + OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", "BASE" + "," + OPTION1_NAME_DEFAULT);
            }


            // Import House Quantities
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(ImportType_1, string.Empty, OPTION1_NAME_DEFAULT, "ImportHouseQuantities_Pre_ImportFile_PIPE_35652.xml");

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT1_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT1_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT1_DEFAULT} is imported successfully with Description {PRODUCT1_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT1_DEFAULT} is imported unsuccessfully with Description {PRODUCT1_DESCRIPTION_DEFAULT}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT2_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT2_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT2_DEFAULT} is imported successfully with Description {PRODUCT2_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT2_DEFAULT} is imported unsuccessfully with Description {PRODUCT2_DESCRIPTION_DEFAULT}.</font>");
            }


            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT3_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Description {PRODUCT3_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT3_DEFAULT} is imported unsuccessfully with Description {PRODUCT3_DESCRIPTION_DEFAULT}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT4_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT4_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT4_DEFAULT} is imported successfully with Description {PRODUCT4_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT4_DEFAULT} is imported unsuccessfully with Description {PRODUCT4_DESCRIPTION_DEFAULT}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT5_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT5_DEFAULT} is imported successfully with Empty Description .</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT5_DEFAULT} is imported unsuccessfully with Description .</font>");
            }


            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithNoProduct(_houseImportQuantitiesData1);


            //1.5 Import finish and go to the quantities page: check data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.5 Import finish and go to the quantities page: check data</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");

            foreach (ProductToOptionData housequantity in houseQuantities_ImportType_1.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_ImportType_1.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Description", item.Description) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_ImportType_1.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Description: {item.Description}</br></font>");
                }
            }


            //2. Verify user the descriptions of the key measures(Products) show when import with the As-built file(Default community with Start comparison setup)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2. Verify user the descriptions of the key measures(Products) show when import with the As-built file(Default community with Start comparison setup)</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_1.Name);

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_07_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase is not displayed in grid.</font>");
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_2.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_2.Name);
                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_08_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase is not displayed in grid.</font>");
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_3.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_3.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_3.Name);

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_09_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase is not displayed in grid.</font>");
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_4.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_4.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_4.Name} is not present in grid.</b>");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_4.Name} is present in grid.</b>");
                ProductPage.Instance.DeleteProduct(productData_Option_4.Name);
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_5.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_5.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_5.Name} is not present in grid.</b>");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_5.Name} is present in grid.</b>");
                ProductPage.Instance.DeleteProduct(productData_Option_5.Name);
            }

            //Go to the building phase rule and setup data
            QuantityBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.QuanttityPhaseRules);
            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData1_i.OriginalBuildingPhase) is true
            && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_i.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_i.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData2_i.OriginalBuildingPhase) is true
            && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_i.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_i.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData3_i.OriginalBuildingPhase) is true
            && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_i.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_i.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }

            // Insert name to filter and click filter by Contain value
            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData1_ii.NewBuildingPhase);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData1_ii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_ii.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData1_ii.OriginalBuildingPhase} is display on grid.</font>");
            }
            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();

                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData1_ii);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData1_ii.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData1_ii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData1_ii.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData1_ii.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData1_ii.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData1_ii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData1_ii.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }

            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData2_ii.OriginalBuildingPhase);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData2_ii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_ii.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData2_ii.OriginalBuildingPhase} is display on grid.</font>");
            }
            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();

                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData2_ii);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData2_ii.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData2_ii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData2_ii.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData2_ii.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData2_ii.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData2_ii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData2_ii.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }

            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData3_ii.NewBuildingPhase);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData3_ii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_ii.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData3_ii.OriginalBuildingPhase} is display on grid.</font>");
            }
            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();

                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData3_ii);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData3_ii.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData3_ii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData3_ii.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData3_ii.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData3_ii.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData3_ii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData2_ii.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }

            //2.1 Go to House detail/Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.1 Go to House detail/Import page</font>");
            CommonHelper.OpenURL(HouseDetail_url);
            //Delete All House Quantities In Default Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Community.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            //2.2 Upload file with mode pre-import type
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.2 Upload file with mode pre-import type.</font>");
            //Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            //Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }


            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION2_NAME_DEFAULT);
            }

            // Import House Quantities
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(ImportType_2, string.Empty, OPTION2_NAME_DEFAULT, "ImportHouseQuantities_As_built_ImportFile_PIPE_35652.xml");


            //2.3 Import file with a default community and generate a report view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.3 Import file with a default community and generate a report view.</font>");
            //2.4 Check the description on the import page.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4 Check the description on the import page.</font>");
            //2.4.1 Import Quantities: On this page will get description form the import file if the file is empty value then get from the product detail page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.1 Import Quantities: On this page will get description form the import file if the file is empty value then get from the product detail page.</font>");
            //2.4.2 Validate Products: New product so it will get the description field form the import file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.2 Validate Products: New product so it will get the description field form the import file.</font>");
            //2.4.3 Building phase Assignment Not Found: The Description get from the product detail
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.3 Building phase Assignment Not Found: The Description get from the product detail.</font>");
            //2.4.4 Import quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.4 Import quantities.</font>");
            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT1_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT1_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT1_DEFAULT} is imported successfully with Description {PRODUCT1_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT1_DEFAULT} is imported unsuccessfully with Description {PRODUCT1_DESCRIPTION_DEFAULT}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT2_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT2_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT2_DEFAULT} is imported successfully with Description {PRODUCT2_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT2_DEFAULT} is imported unsuccessfully with Description {PRODUCT2_DESCRIPTION_DEFAULT}.</font>");
            }


            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT3_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Description {PRODUCT3_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT3_DEFAULT} is imported unsuccessfully with Description {PRODUCT3_DESCRIPTION_DEFAULT}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT4_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT4_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT4_DEFAULT} is imported successfully with Description {PRODUCT4_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT4_DEFAULT} is imported unsuccessfully with Description {PRODUCT4_DESCRIPTION_DEFAULT}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT5_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT5_DEFAULT} is imported successfully with Empty Description .</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT5_DEFAULT} is imported unsuccessfully with Description .</font>");
            }

            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithNoProduct(_houseImportQuantitiesData2);

            //2.5 Import finish and go to the quantities page: check data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.5 Import finish and go to the quantities page: check data</font>");

            HouseCommunities.Instance.LeftMenuNavigation("Quantities");

            foreach (ProductToOptionData housequantity in houseQuantities_ImportType_2.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Description", item.Description) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Description: {item.Description}</br></font>");
                }
            }


            //3. Verify user the descriptions of the key measures(Products) show when import with the CSV(Default community with Generate report view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3. Verify user the descriptions of the key measures(Products) show when import with the CSV(Default community with Generate report view</font>");

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_1.Name);

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_01_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Building Phase With Name {productData_Option_1.BuildingPhase}is not displayed in grid.</font>");
                }

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_07_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase With Name {_quantityBuildingPhaseRuleData1_i.NewBuildingPhase} is not displayed in grid.</font>");
                }

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase With Name {_quantityBuildingPhaseRuleData3_i.NewBuildingPhase} is not displayed in grid.</font>");
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_2.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_2.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_2.Name);

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_02_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Building Phase is not displayed in grid.</font>");
                }

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_08_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase With Name {_quantityBuildingPhaseRuleData2_i.NewBuildingPhase} is not displayed in grid.</font>");
                }


                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase With Name {_quantityBuildingPhaseRuleData2_i.NewBuildingPhase} is not displayed in grid.</font>");
                }
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_3.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_3.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_3.Name);

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_03_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase is not displayed in grid.</font>");
                }

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_09_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase With Name {_quantityBuildingPhaseRuleData3_i.NewBuildingPhase} is not displayed in grid.</font>");
                }

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase With Name {_quantityBuildingPhaseRuleData3_i.NewBuildingPhase} is not displayed in grid.</font>");
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_4.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_4.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_4.Name} is not present in grid.</b>");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_4.Name} is present in grid.</b>");
                ProductPage.Instance.DeleteProduct(productData_Option_4.Name);
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_5.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_5.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_5.Name} is not present in grid.</b>");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<b>New Product {productData_Option_5.Name} is present in grid.</b>");
                ProductPage.Instance.DeleteProduct(productData_Option_5.Name);
            }

            //Go to the building phase rule and setup data
            QuantityBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.QuanttityPhaseRules);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData1_ii.OriginalBuildingPhase) is true
            && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_ii.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_ii.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData2_ii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_ii.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_ii.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData3_ii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_ii.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_ii.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }

            // Insert name to filter and click filter by Contain value
            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData1_iii.NewBuildingPhase);
            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData1_iii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_iii.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData1_iii.OriginalBuildingPhase} is display on grid.</font>");
            }
            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();

                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData1_iii);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData1_iii.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData1_iii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData1_iii.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData1_iii.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData1_iii.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData1_iii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData1_iii.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }

            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData2_iii.NewBuildingPhase);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData2_iii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_iii.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData2_iii.OriginalBuildingPhase} is display on grid.</font>");
            }
            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();

                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData2_iii);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData2_iii.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData2_iii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData2_iii.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData2_iii.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData2_iii.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData2_iii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData2_iii.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }

            QuantityBuildingPhaseRulePage.Instance.FilterItemInGrid("New Phase", GridFilterOperator.Contains, _quantityBuildingPhaseRuleData3_iii.NewBuildingPhase);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData3_iii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_iii.NewBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Building Phase Rule {_quantityBuildingPhaseRuleData3_iii.OriginalBuildingPhase} is display on grid.</font>");
            }
            else
            {
                QuantityBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();

                // Create Building Phase Rule - Click 'Save' Button
                QuantityBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_quantityBuildingPhaseRuleData3_iii);

                string _actualMessage = QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "Building Phase Rule created successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData3_iii.Priority} and Original Building Phase {_quantityBuildingPhaseRuleData3_iii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData3_iii.NewBuildingPhase}.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create Building Phase Rule with Priority { _quantityBuildingPhaseRuleData3_iii.Priority} and LEVEL condition {_quantityBuildingPhaseRuleData3_iii.LevelCondition} and Original Building Phase {_quantityBuildingPhaseRuleData3_iii.OriginalBuildingPhase} and New Building Phase {_quantityBuildingPhaseRuleData3_iii.NewBuildingPhase} successfully.");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
            }

            //3.1 Go to House detail/Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.1 Go to House detail/Import page</font>");
            CommonHelper.OpenURL(HouseDetail_url);

            //Delete All House Quantities In Default Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Community.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            //3.2 Upload file with mode pre-import type
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.2 Upload file with mode pre-import type</font>");

            //Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");

            //Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION2_NAME_DEFAULT);
            }

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION3_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION3_NAME_DEFAULT);
            }
            // Import House Quantities
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(ImportType_3, string.Empty, OPTION3_NAME_DEFAULT, "ImportHouseQuantities_CSV_ImportFile_PIPE_35652.csv");

            //3.3 Import file with a default community and generate a report view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.3 Import file with a default community and generate a report view.</font>");
            //3.4 Check the description on the import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 Check the description on the import page.</font>");
            //3.4.1 Import Quantities: On this page will get a description form the import file if the file is empty value then get from the product detail page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4.1 Import Quantities: On this page will get a description form the import file if the file is empty value then get from the product detail page.</font>");
            //3.4.2 Validate Products: New product so it will get the description field form the import file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4.2 Validate Products: New product so it will get the description field form the import file.</font>");
            //3.4.3 Building phase Assignment Not Found: The Description get from the product detail
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4.3 Building phase Assignment Not Found: The Description get from the product detail.</font>");
            //3.4.4 Import quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4.4 Import quantities.</font>");

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT1_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT1_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT1_DEFAULT} is imported successfully with Description {PRODUCT1_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT1_DEFAULT} is imported unsuccessfully with Description {PRODUCT1_DESCRIPTION_DEFAULT}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT2_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT2_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT2_DEFAULT} is imported successfully with Description {PRODUCT2_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT2_DEFAULT} is imported unsuccessfully with Description {PRODUCT2_DESCRIPTION_DEFAULT}.</font>");
            }


            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT3_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Description", PRODUCT3_DESCRIPTION_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT3_DEFAULT} is imported successfully with Description {PRODUCT3_DESCRIPTION_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT3_DEFAULT} is imported unsuccessfully with Description {PRODUCT3_DESCRIPTION_DEFAULT}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT4_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT4_DEFAULT} is imported successfully with Empty Description</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT4_DEFAULT} is imported unsuccessfully with Empty Description</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT5_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT5_DEFAULT} is imported successfully with Empty Description.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT5_DEFAULT} is imported unsuccessfully with Description.</font>");
            }

            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithNoProduct(_houseImportQuantitiesData3);

            //3.5 Import finish and go to the quantities page: check data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.5 Import finish and go to the quantities page: check data</font>");

            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            foreach (ProductToOptionData housequantity in houseQuantities_ImportType_3.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_ImportType_3.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Description", item.Description) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_ImportType_3.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Description: {item.Description}</br></font>");
                }
            }
        }

        [TearDown]
        public void ClearData()
        {
            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION2_NAME_DEFAULT);
            }
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION3_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION3_NAME_DEFAULT);
            }
            //Delete All House Quantities In Default Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Community.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            QuantityBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.QuanttityPhaseRules);

            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData1_iii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_iii.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData1_iii.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }


            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData2_iii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_iii.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData2_iii.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }


            if (QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("Original Phase", _quantityBuildingPhaseRuleData3_iii.OriginalBuildingPhase) is true
                && QuantityBuildingPhaseRulePage.Instance.IsItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_iii.NewBuildingPhase) is true)
            {
                QuantityBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Phase", _quantityBuildingPhaseRuleData3_iii.NewBuildingPhase);
                string successfulMess = $"Building Phase Rule deleted successfully!";
                if (successfulMess == QuantityBuildingPhaseRulePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Building Phase Rule deleted successfully!");
                    QuantityBuildingPhaseRulePage.Instance.CloseToastMessage();
                }
                CommonHelper.RefreshPage();
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_1.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_1.Name) is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_1.Name);

                if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_07_Automation") is true)
                {
                    ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_07_Automation");

                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                    else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_07_Automation") == true)

                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                    else
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                    if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") is true)
                    {
                        ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation");

                        if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                        else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") == true)

                            ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                        else
                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                    }
                    if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation") is true)
                    {
                        ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation");

                        if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                        else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation") == true)

                            ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                        else
                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                    }
                }

                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
                ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_2.Name);
                if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_2.Name) is true)
                {
                    ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_2.Name);

                    if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_08_Automation") is true)
                    {
                        ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_08_Automation");
                        if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                        else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_08_Automation") == true)

                            ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                        else
                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                    }

                    if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") is true)
                    {
                        ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation");

                        if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                        else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") == true)

                            ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                        else
                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                    }
                    if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation") is true)
                    {
                        ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation");

                        if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                        else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation") == true)

                            ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                        else
                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                    }
                }

                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
                ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData_Option_3.Name);
                if (ProductPage.Instance.IsItemInGrid("Product Name", productData_Option_3.Name) is true)
                {
                    ProductPage.Instance.SelectItemInGrid("Product Name", productData_Option_3.Name);

                    if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_09_Automation") is true)
                    {
                        ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_09_Automation");
                        if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                        else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_09_Automation") == true)

                            ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                        else
                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                    }

                    if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") is true)
                    {
                        ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation");

                        if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                        else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_10_Automation") == true)

                            ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                        else
                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                    }
                    if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation") is true)
                    {
                        ProductDetailPage.Instance.DeleteItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation");

                        if (ProductDetailPage.Instance.GetLastestToastMessage() == "Building Phase deleted successfully!")

                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");

                        else if (ProductDetailPage.Instance.IsItemOnBuildPhaseGrid("Name", "QA_RT_Building_Phase_11_Automation") == true)

                            ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Building Phase failed; Don't received a message delete successfully</b></font>");
                        else
                            ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Building Phase successfully; Received a message delete successfully</b></font>");
                    }
                }
            }
        }
    }
}
