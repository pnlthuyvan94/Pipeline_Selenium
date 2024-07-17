using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_W_PIPE_47899 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string IMPORTTYPE_1 = "Pre-Import Modification";
        private const string IMPORTTYPE_2 = "Delta (As-Built)";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";
        private const string OPTION_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private const string OPTION_CODE_DEFAULT = "0100";
        private const string COMMUNITY_CODE_DEFAULT = "Automation_01";
        private const string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";
        private const string HOUSE_NAME_DEFAULT = "QA_RT_House_47899_Automation";
        private const string HOUSE_CODE_DEFAULT = "7899";
        private const string INCLUDED_IMPORT_OPTION_NAME_DEFAULT = "ELEVATION 1A, OPT. SIDE ENTRY GARAGE";
        private const string SIGN = "-";
        private const string MANUFACTURER1_DEFAULT = "QA_RT_New_Manu01";
        private const string MANUFACTURER2_DEFAULT = "QA_RT_New_Manu02";
        private const string MANUFACTURER3_DEFAULT = "QA_RT_New_Manu03";
        private const string MANUFACTURER4_DEFAULT = "QA_RT_New_Manu04";
        private const string MANUFACTURER5_DEFAULT = "QA_RT_New_Manu05";
        private const string MANUFACTURER6_DEFAULT = "QA_RT_New_Manu06";
        private const string MANUFACTURER1_NEW_DEFAULT = "QA_RT_New_Manu_47899_01";
        private const string MANUFACTURER2_NEW_DEFAULT = "QA_RT_New_Manu_47899_02";

        private const string STYLE1_DEFAULT = "QA_RT_New_Style01";
        private const string STYLE2_DEFAULT = "QA_RT_New_Style02";
        private const string STYLE3_DEFAULT = "QA_RT_New_Style03";
        private const string STYLE4_DEFAULT = "QA_RT_New_Style04";
        private const string STYLE5_DEFAULT = "QA_RT_New_Style05";
        private const string STYLE6_DEFAULT = "QA_RT_New_Style06";
        private const string STYLE1_NEW_DEFAULT = "QA_RT_New_Style_47899_01";
        private const string STYLE2_NEW_DEFAULT = "QA_RT_New_Style_47899_02";

        private List<string> ListStyle = new List<string>() { STYLE1_NEW_DEFAULT, STYLE2_NEW_DEFAULT };

        private const string PRODUCT1_DEFAULT = "QA_RT_New_Product_Automation_01";
        private const string PRODUCT2_DEFAULT = "QA_RT_New_Product_Automation_02";
        private const string PRODUCT3_DEFAULT = "QA_RT_New_Product_Automation_03";
        private const string PRODUCT4_DEFAULT = "QA_RT_New_Product_Automation_04";
        private const string PRODUCT5_DEFAULT = "QA_RT_New_Product_Automation_05";
        private const string PRODUCT6_DEFAULT = "QA_RT_New_Product_Automation_06";
        private const string PRODUCT1_NEW_DEFAULT = "QA_RT_New_Product_47899_01";
        private const string PRODUCT2_NEW_DEFAULT = "QA_RT_New_Product_47899_02";
        private const string PRODUCT3_NEW_DEFAULT = "QA_RT_New_Product_47899_03";
        private const string PRODUCT4_NEW_DEFAULT = "QA_RT_New_Product_47899_04";

        private List<string> ListProduct = new List<string>() { PRODUCT1_NEW_DEFAULT, PRODUCT2_NEW_DEFAULT, PRODUCT3_NEW_DEFAULT, PRODUCT4_NEW_DEFAULT };
        private readonly int[] indexs = new int[] { };
        private HouseData housedata;

        HouseImportQuantitiesData houseImportQuantitiesData1;
        HouseImportQuantitiesData houseImportQuantitiesData2;
        HouseImportQuantitiesData houseImportQuantitiesData3;
        HouseImportQuantitiesData houseImportQuantitiesData4;

        private ProductData productDataOption1;
        private ProductData productDataOption2;
        private ProductData productDataOption3;
        private ProductData productDataOption4;
        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToOption3;
        private ProductToOptionData productToOption4;
        private ProductData productDataHouse1;
        private ProductData productDataHouse2;
        private ProductData productDataHouse3;
        private ProductData productDataHouse4;
        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToHouse3;
        private ProductToOptionData productToHouse4;
        private ProductToOptionData productToHouseBOM1;
        private ProductToOptionData productToHouseBOM2;
        private ProductToOptionData productToHouseBOM3;
        private ProductToOptionData productToHouseBOM4;
        private HouseQuantitiesData houseQuantities;
        private HouseQuantitiesData houseQuantities_HouseBOM;

        private ProductData newProductDataOption1;
        private ProductData newProductDataOption2;
        private ProductData newProductDataOption3;
        private ProductData newProductDataOption4;
        private ProductToOptionData newProductToOption1;
        private ProductToOptionData newProductToOption2;
        private ProductToOptionData newProductToOption3;
        private ProductToOptionData newProductToOption4;
        private ProductData newProductDataHouse1;
        private ProductData newProductDataHouse2;
        private ProductData newProductDataHouse3;
        private ProductData newProductDataHouse4;
        private ProductToOptionData newProductToHouse1;
        private ProductToOptionData newProductToHouse2;
        private ProductToOptionData newProductToHouse3;
        private ProductToOptionData newProductToHouse4;
        private ProductToOptionData newProductToHouseBOM1;
        private ProductToOptionData newProductToHouseBOM2;
        private ProductToOptionData newProductToHouseBOM3;
        private ProductToOptionData newProductToHouseBOM4;
        private HouseQuantitiesData houseQuantities3;
        private HouseQuantitiesData houseQuantities_HouseBOM3;
        private HouseQuantitiesData houseQuantities4;
        private HouseQuantitiesData houseQuantities_HouseBOM4;

        [SetUp]
        public void GetData()
        {
            housedata = new HouseData()
            {
                HouseName = "QA_RT_House_47899_Automation",
                SaleHouseName = "QA_RT_House_47899_Automation",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "7899",
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

            houseImportQuantitiesData1 = new HouseImportQuantitiesData()
            {
                Manufacturers = new List<string>() { MANUFACTURER1_DEFAULT, MANUFACTURER2_DEFAULT, MANUFACTURER3_DEFAULT, MANUFACTURER4_DEFAULT },
                Styles = new List<string>() { STYLE1_DEFAULT, STYLE2_DEFAULT, STYLE3_DEFAULT, STYLE4_DEFAULT },
            };

            houseImportQuantitiesData2 = new HouseImportQuantitiesData()
            {
                Products = new List<string>() { PRODUCT1_DEFAULT, PRODUCT2_DEFAULT, PRODUCT3_DEFAULT, PRODUCT4_DEFAULT },
                Manufacturers = new List<string>() { MANUFACTURER5_DEFAULT, MANUFACTURER6_DEFAULT },
                Styles = new List<string>() { STYLE5_DEFAULT, STYLE6_DEFAULT },
            };

            houseImportQuantitiesData3 = new HouseImportQuantitiesData()
            {
                Manufacturers = new List<string>() { MANUFACTURER1_NEW_DEFAULT, MANUFACTURER2_NEW_DEFAULT },
                Styles = new List<string>() { STYLE1_NEW_DEFAULT, STYLE2_NEW_DEFAULT },
            };

            houseImportQuantitiesData4 = new HouseImportQuantitiesData()
            {
                Manufacturers = new List<string>() { MANUFACTURER1_NEW_DEFAULT, MANUFACTURER2_NEW_DEFAULT },
                Styles = new List<string>() { STYLE1_NEW_DEFAULT, STYLE2_NEW_DEFAULT },
            };

            productDataOption1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style01",
                Quantities = "1.00"
            };

            productDataOption2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_02",
                Style = "QA_RT_New_Style02",
                Quantities = "2.00"
            };
            productDataOption3 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_03",
                Style = "QA_RT_New_Style03",
                Quantities = "3.00"
            };

            productDataOption4 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_04",
                Style = "QA_RT_New_Style04",
                Quantities = "4.00"
            };

            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productDataOption1 }
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productDataOption2 }
            };

            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productDataOption3 }
            };

            productToOption4 = new ProductToOptionData()
            {
                BuildingPhase = "Au02-QA_RT_New_Building_Phase_02_Automation",
                ProductList = new List<ProductData> { productDataOption4 }
            };

            productDataHouse1 = new ProductData(productDataOption1);
            productDataHouse2 = new ProductData(productDataOption2);
            productDataHouse3 = new ProductData(productDataOption3);
            productDataHouse4 = new ProductData(productDataOption4);

            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productDataHouse1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productDataHouse2 } };
            productToHouse3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productDataHouse3 } };
            productToHouse4 = new ProductToOptionData(productToOption4) { ProductList = new List<ProductData> { productDataHouse4 } };

            houseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2, productToHouse3, productToHouse4 }
            };

            ProductData productData_HouseBOM_1 = new ProductData(productDataOption1);
            ProductData productData_HouseBOM_2 = new ProductData(productDataOption2);
            ProductData productData_HouseBOM_3 = new ProductData(productDataOption3);
            ProductData productData_HouseBOM_4 = new ProductData(productDataOption4);

            productToHouseBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };
            productToHouseBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_HouseBOM_2 } };
            productToHouseBOM3 = new ProductToOptionData(productToHouse3) { ProductList = new List<ProductData> { productData_HouseBOM_3 } };
            productToHouseBOM4 = new ProductToOptionData(productToHouse4) { ProductList = new List<ProductData> { productData_HouseBOM_4 } };

            houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM2, productToHouseBOM3, productToHouseBOM4 }
            };

            newProductDataOption1 = new ProductData()
            {
                Name = "QA_RT_New_Product_47899_01",
                Style = "QA_RT_New_Style_47899_01",
                Quantities = "1.00"
            };

            newProductDataOption2 = new ProductData()
            {
                Name = "QA_RT_New_Product_47899_02",
                Style = "QA_RT_New_Style_47899_02",
                Quantities = "2.00"
            };

            newProductDataOption3 = new ProductData()
            {
                Name = "QA_RT_New_Product_47899_03",
                Style = "QA_RT_New_Style_47899_01",
                Quantities = "3.00"
            };

            newProductDataOption4 = new ProductData()
            {
                Name = "QA_RT_New_Product_47899_04",
                Style = "QA_RT_New_Style_47899_02",
                Quantities = "4.00"
            };


            newProductToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { newProductDataOption1 }
            };

            newProductToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { newProductDataOption2 }
            };

            newProductToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { newProductDataOption3 }
            };

            newProductToOption4 = new ProductToOptionData()
            {
                BuildingPhase = "Au02-QA_RT_New_Building_Phase_02_Automation",
                ProductList = new List<ProductData> { newProductDataOption4 }
            };

            newProductDataHouse1 = new ProductData(newProductDataOption1);
            newProductDataHouse2 = new ProductData(newProductDataOption2);
            newProductDataHouse3 = new ProductData(newProductDataOption3);
            newProductDataHouse4 = new ProductData(newProductDataOption4);

            newProductToHouse1 = new ProductToOptionData(newProductToOption1) { ProductList = new List<ProductData> { newProductDataHouse1 } };
            newProductToHouse2 = new ProductToOptionData(newProductToOption2) { ProductList = new List<ProductData> { newProductDataHouse2 } };
            newProductToHouse3 = new ProductToOptionData(newProductToOption3) { ProductList = new List<ProductData> { newProductDataHouse3 } };
            newProductToHouse4 = new ProductToOptionData(newProductToOption4) { ProductList = new List<ProductData> { newProductDataHouse4 } };

            houseQuantities3 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { newProductToHouse1, newProductToHouse2 }
            };

            houseQuantities4 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { newProductToHouse3, newProductToHouse4 }
            };

            ProductData newProductData_HouseBOM_1 = new ProductData(newProductDataOption1);
            ProductData newProductData_HouseBOM_2 = new ProductData(newProductDataOption2);

            ProductData newProductData_HouseBOM_3 = new ProductData(newProductDataOption3);
            ProductData newProductData_HouseBOM_4 = new ProductData(newProductDataOption4);


            newProductToHouseBOM1 = new ProductToOptionData(newProductToHouse1) { ProductList = new List<ProductData> { newProductData_HouseBOM_1 } };

            newProductToHouseBOM2 = new ProductToOptionData(newProductToHouse2) { ProductList = new List<ProductData> { newProductData_HouseBOM_2 } };
            newProductToHouseBOM3 = new ProductToOptionData(newProductToHouse3) { ProductList = new List<ProductData> { newProductData_HouseBOM_3 } };
            newProductToHouseBOM4 = new ProductToOptionData(newProductToHouse4) { ProductList = new List<ProductData> { newProductData_HouseBOM_4 } };

            houseQuantities_HouseBOM3 = new HouseQuantitiesData(houseQuantities3)
            {
                productToOption = new List<ProductToOptionData> { newProductToHouseBOM1, newProductToHouseBOM2 }
            };
            houseQuantities_HouseBOM4 = new HouseQuantitiesData(houseQuantities4)
            {
                productToOption = new List<ProductToOptionData> { newProductToHouseBOM3, newProductToHouseBOM4 }
            };

        }

        [Test]
        [Category("Section_IV")]
        public void A04_W_Assets_DetailPage_House_ImportPage_Could_not_import_new_Style_Manufacturer_into_House_Quantities_if_there_are_2_new_Style_Manufacturer_or_above()
        {
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, housedata.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", housedata.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(housedata);
                string updateMsg = $"House {housedata.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            else
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", housedata.HouseName);

            }
            string houseDetailUrl = HouseDetailPage.Instance.CurrentURL;
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            HouseDetailPage.Instance.LeftMenuNavigation("Communities");

            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT, indexs);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.1 Prepare XML file with existing product + new style/manufacturer.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(IMPORTTYPE_1, COMMUNITY_CODE_DEFAULT + SIGN + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_Pre_ImportFile_PIPE_47899.xml");

            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithNoManufacturerOrStyle(houseImportQuantitiesData1);
            HouseImportDetailPage.Instance.ClickOnFinishImport();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.2 Import to House Quantities with Pre-Import + Start Comparison Setup +Specific Community.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.3 Verify the Quantities after importing.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(COMMUNITY_CODE_DEFAULT + SIGN + COMMUNITY_NAME_DEFAULT);
            foreach (ProductToOptionData housequantity in houseQuantities.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities.optionName)
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase)
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name)
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style))
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}</br></font>");
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.4 Generate House BOM.</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM.communityCode + SIGN + houseQuantities_HouseBOM.communityName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify quantities In House BOM.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>2.1 Prepare XML file with existing product +existing style / manufacturer, but they are not assigned to the product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData1 = new ManufacturerData()
            {
                Name = MANUFACTURER5_DEFAULT
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData1.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData1.Name) is false)
            {
                ManufacturerPage.Instance.CreateNewManufacturer(manuData1);
            }

            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData2 = new ManufacturerData()
            {
                Name = MANUFACTURER6_DEFAULT
            };
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData2.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData2.Name) is false)
            {
                ManufacturerPage.Instance.CreateNewManufacturer(manuData2);
            }

            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData1 = new StyleData()
            {
                Name = STYLE5_DEFAULT,
                Manufacturer = manuData1.Name
            };

            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData1.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData1.Name) is false)
            {
                StylePage.Instance.CreateNewStyle(styleData1);
            }

            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData2 = new StyleData()
            {
                Name = STYLE6_DEFAULT,
                Manufacturer = manuData2.Name
            };
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData2.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData2.Name) is false)
            {
                StylePage.Instance.CreateNewStyle(styleData2);
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT5_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT5_DEFAULT) is false)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='red'>Product With Name {PRODUCT5_DEFAULT} is not display in grid.</font>");
            }

            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, PRODUCT6_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", PRODUCT6_DEFAULT) is false)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='red'>Product With Name {PRODUCT6_DEFAULT} is not display in grid.</font>");
            }

            CommonHelper.OpenURL(houseDetailUrl);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>2.2 Import to House Quantities with Delta +Generate Report View +Specific Community.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(IMPORTTYPE_2, COMMUNITY_CODE_DEFAULT + SIGN + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_As_built_ImportFile_PIPE_47899_1.xml");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithNoManufacturerOrStyle(houseImportQuantitiesData2);
            HouseImportDetailPage.Instance.CheckProductWithStyleNotImportAndNoExistInTheSystem(houseImportQuantitiesData2);
            HouseImportDetailPage.Instance.ClickOnFinishImport();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>2.3 Verify the Quantities after importing.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT);
            if (HouseQuantitiesDetailPage.Instance.VerifyHouseQuantitiesIsNotDisplay())
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The option(s) in the {HOUSE_NAME_DEFAULT} is blank</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The option(s) in the {HOUSE_NAME_DEFAULT} is not blank</font>");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>3.1 1.Prepare XML file with new product + new style/manufacturer.</font>");
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            HouseImportDetailPage.Instance.ImportHouseQuantities(IMPORTTYPE_2, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_As_built_ImportFile_PIPE_47899_2.xml");
            HouseImportDetailPage.Instance.StartComparion();
            HouseImportDetailPage.Instance.AddComparisonGroups();
            HouseImportDetailPage.Instance.InsertItemComparsion(OPTION_NAME_DEFAULT, string.Empty, string.Empty, INCLUDED_IMPORT_OPTION_NAME_DEFAULT);
            HouseImportDetailPage.Instance.VerifyItemComparisonGroups(OPTION_NAME_DEFAULT, string.Empty, string.Empty, INCLUDED_IMPORT_OPTION_NAME_DEFAULT);
            HouseImportDetailPage.Instance.OpenGenerateComparison();
            HouseImportDetailPage.Instance.ClickOnImportFile();
            HouseImportDetailPage.Instance.ValidateProductStyles(houseImportQuantitiesData3);
            HouseImportDetailPage.Instance.ValidateProducts(houseImportQuantitiesData3);
            HouseImportDetailPage.Instance.ClickOnContinueImport();
            HouseImportDetailPage.Instance.ClickOnFinishReviewImport();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>3.2. Import to House Quantities with Delta +Start Comparison Setup +(switch to) Default Community.</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>3.3. Verify the Quantities after importing.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            foreach (ProductToOptionData housequantity in houseQuantities3.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities3.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities3.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}</br></font>");
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>3.4 Generate House BOM.</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM3.communityCode + SIGN + houseQuantities_HouseBOM3.communityName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify quantities In House BOM.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM3);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>4.1.Prepare XML file with new product + existing style/manufacturer.</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT))
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT))
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>4.2. Import to House Quantities with Pre-Import + Generate Report View +(switch to) Specific Community.</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(IMPORTTYPE_1, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_Pre_ImportFile_PIPE_47899_2.xml");
            HouseImportDetailPage.Instance.ClickOnImportFile();
            HouseImportDetailPage.Instance.ValidateProducts(houseImportQuantitiesData4);
            HouseImportDetailPage.Instance.ClickOnContinueImport();
            HouseImportDetailPage.Instance.ClickOnFinishReviewImport(); ;
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>4.3. Verify the Quantities after importing.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(COMMUNITY_CODE_DEFAULT + SIGN + COMMUNITY_NAME_DEFAULT);
            foreach (ProductToOptionData housequantity in houseQuantities4.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities4.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities4.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}</br></font>");
                }
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>4.4. Generate House BOM.</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM4.communityCode + SIGN + houseQuantities_HouseBOM4.communityName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify quantities In House BOM.</b></font>");
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM4);
        }

        [TearDown]
        public void ClearData()
        {
            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT))
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_HouseBOM.communityCode + SIGN + houseQuantities_HouseBOM.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Product Style .</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productDataOption1.Name);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.Contains, STYLE1_DEFAULT);

            if (ProductPage.Instance.IsItemInGrid("Product Name", productDataOption1.Name))
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productDataOption1.Name);
                ExtentReportsHelper.LogInformation("<b>Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE1_DEFAULT) == true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", STYLE1_DEFAULT);
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productDataOption2.Name);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.Contains, STYLE2_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productDataOption2.Name))
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productDataOption2.Name);
                ExtentReportsHelper.LogInformation("<b>Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE2_DEFAULT) == true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", STYLE2_DEFAULT);
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productDataOption3.Name);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.Contains, STYLE3_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productDataOption3.Name))
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productDataOption3.Name);
                ExtentReportsHelper.LogInformation("<b>Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE3_DEFAULT) == true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", STYLE3_DEFAULT);
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productDataOption4.Name);
            ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.Contains, STYLE4_DEFAULT);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productDataOption4.Name))
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productDataOption4.Name);
                ExtentReportsHelper.LogInformation("<b>Delete Manufacturers and Style</b>");
                if (ProductDetailPage.Instance.IsItemOnManufacturerGrid("Style", STYLE4_DEFAULT) == true)
                {
                    ProductDetailPage.Instance.DeleteItemOnManufacturersGrid("Style", STYLE4_DEFAULT);
                    if (ProductDetailPage.Instance.GetLastestToastMessage() == "Style deleted successfully!")
                    {
                        ExtentReportsHelper.LogPass("<font color ='yellow'><b>Deleted Style successfully; Received a message delete successfully</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color ='red'>< b>Deleted Style failed; Don't received a message delete successfully</b></font>");
                    }
                }
            }
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);
            foreach (string product in ListProduct)
            {
                ProductPage.Instance.FilterItemInGrid("Style", GridFilterOperator.NoFilter, string.Empty);
                ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, product);
                if (ProductPage.Instance.IsItemInGrid("Product Name", product))
                {
                    ProductPage.Instance.DeleteProduct(product);
                }
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
            foreach (string style in houseImportQuantitiesData1.Styles)
            {
                StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, style);
                if (StylePage.Instance.IsItemInGrid("Name", style))
                {
                    // If Style doesn't exist then create a new one
                    StylePage.Instance.DeleteItemInGrid("Name", style);
                    if ("Style was deleted!" == StylePage.Instance.GetLastestToastMessage())
                    {
                        ExtentReportsHelper.LogPass($"Product Style {style} deleted successfully.");
                    }
                }
            }

            foreach (string style in ListStyle)
            {
                StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, style);
                if (StylePage.Instance.IsItemInGrid("Name", style) is true)
                {
                    // If Style doesn't exist then create a new one
                    StylePage.Instance.DeleteItemInGrid("Name", style);
                    if ("Style was deleted!" == StylePage.Instance.GetLastestToastMessage())
                    {
                        ExtentReportsHelper.LogPass($"Product Style {style} deleted successfully.");
                    }
                }
            }
        }
    }
}
