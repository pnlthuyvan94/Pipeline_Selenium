using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_B_PIPE_49912 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string IMPORTTYPE_1 = "Pre-Import Modification";
        private const string IMPORTTYPE_2 = "Delta (As-Built)";
        private const string IMPORTTYPE_3 = "CSV";

        private static string HOUSE_NAME_DEFAULT = "QA_RT_House_49912_Automation";

        private static string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";
        private static string COMMUNITY_CODE_DEFAULT = "Automation_01";

        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteSelectedQtys";

        private readonly int[] indexs = new int[] { };
        HouseData housedata;
        private static string OPTION1_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private static string OPTION1_CODE_DEFAULT = "0100";

        private static string OPTION2_NAME_DEFAULT = "QA_RT_Option02_Automation";
        private static string OPTION2_CODE_DEFAULT = "0200";
        List<string> ListOptions1 = new List<string>() { string.Empty };
        List<string> ListOptions2 = new List<string>() { OPTION2_NAME_DEFAULT };
        private const string EXPORT_XML_MORE_MENU = "Export XML";
        private const string EXPORT_CSV_MORE_MENU = "Export CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";

        private readonly string PARAMETER_DEFAULT1 = "SWG";
        private readonly string PARAMETER_DEFAULT1_VALUE1 = "swg01";
        private readonly string PARAMETER_DEFAULT1_VALUE2 = "swg02";

        private readonly string PARAMETER_DEFAULT2 = "LEVEL";

        private readonly string PARAMETER_DEFAULT2_VALUE1 = "level01";

        private readonly string PRODUCT1_DEFAULT = "QA_Product_01_Automation";
        private readonly string PRODUCT2_DEFAULT = "QA_Product_02_Automation";

        private static string INCLUDED_IMPORT_OPTION_NAME_DEFAULT = "ELEVATION 1A, OPT. SIDE ENTRY GARAGE";

        //Pre-Import Modifications + Generate report view
        private ProductData productDataOptionPreImport1;
        private ProductData productDataOptionPreImport2;
        private ProductData productDataOptionPreImport3;

        private ProductToOptionData productToOptionPreImport1;
        private ProductToOptionData productToOptionPreImport2;
        private ProductToOptionData productToOptionPreImport3;

        private ProductData productDataHousePreImport1;
        private ProductData productDataHousePreImport2;
        private ProductData productDataHousePreImport3;

        private ProductToOptionData productToHousePreImport1;
        private ProductToOptionData productToHousePreImport2;
        private ProductToOptionData productToHousePreImport3;

        private HouseQuantitiesData houseQuantitiesPreImport1;
        private HouseQuantitiesData houseQuantitiesPreImport2;

        //As-built + Comparison setup
        private ProductData productDataOptionAsbuilt1;
        private ProductData productDataOptionAsbuilt2;
        private ProductData productDataOptionAsbuilt3;

        private ProductToOptionData productToOptionAsbuilt1;
        private ProductToOptionData productToOptionAsbuilt2;
        private ProductToOptionData productToOptionAsbuilt3;

        private ProductData productDataHouseAsbuilt1;
        private ProductData productDataHouseAsbuilt2;
        private ProductData productDataHouseAsbuilt3;

        private ProductToOptionData productToHouseAsbuilt1;
        private ProductToOptionData productToHouseAsbuilt2;
        private ProductToOptionData productToHouseAsbuilt3;

        private HouseQuantitiesData houseQuantitiesAsbuilt;

        // CSV + Comparison setup
        private ProductData productDataOptionCsv1;
        private ProductData productDataOptionCsv2;
        private ProductData productDataOptionCsv3;

        private ProductToOptionData productToOptionCsv1;
        private ProductToOptionData productToOptionCsv2;
        private ProductToOptionData productToOptionCsv3;

        private ProductData productDataHouseCsv1;
        private ProductData productDataHouseCsv2;
        private ProductData productDataHouseCsv3;

        private ProductToOptionData productToHouseCsv1;
        private ProductToOptionData productToHouseCsv2;
        private ProductToOptionData productToHouseCsv3;


        private HouseQuantitiesData houseQuantities_CSV;
        [SetUp]
        public void TestData()
        {
            housedata = new HouseData()
            {
                HouseName = "QA_RT_House_49912_Automation",
                SaleHouseName = "QA_RT_House_49912_Automation",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "4991",
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

            //Pre-Import Modifications + Generate report view

            productDataOptionPreImport1 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "60.00",
                Parameter = "LEVEL=level01|SWG=swg01"
            };

            productDataOptionPreImport2 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "60.00",
                Parameter = "LEVEL=level01|SWG=swg01|SWG=swg02"
            };
            productDataOptionPreImport3 = new ProductData()
            {
                Name = "QA_Product_02_Automation",
                Style = "QA_Style_Automation",
                Use = "NONE",
                Quantities = "60.00"
            };

            productToOptionPreImport1 = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { productDataOptionPreImport1 },
                ParameterList = new List<ProductData> { productDataOptionPreImport1 }
            };

            productToOptionPreImport2 = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { productDataOptionPreImport2 },
                ParameterList = new List<ProductData> { productDataOptionPreImport2 }
            };

            productToOptionPreImport3 = new ProductToOptionData()
            {
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation",
                ProductList = new List<ProductData> { productDataOptionPreImport3 }
            };

            productDataHousePreImport1 = new ProductData(productDataOptionPreImport1);

            productDataHousePreImport2 = new ProductData(productDataOptionPreImport2);

            productDataHousePreImport3 = new ProductData(productDataOptionPreImport3);

            productToHousePreImport1 = new ProductToOptionData(productToOptionPreImport1) { ProductList = new List<ProductData> { productDataHousePreImport1 } };
            productToHousePreImport2 = new ProductToOptionData(productToOptionPreImport2) { ProductList = new List<ProductData> { productDataHousePreImport2 } };
            productToHousePreImport3 = new ProductToOptionData(productToOptionPreImport3) { ProductList = new List<ProductData> { productDataHousePreImport3 } };


            houseQuantitiesPreImport1 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHousePreImport1 }
            };

            houseQuantitiesPreImport2 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHousePreImport2, productToHousePreImport3 }
            };

            //As-built + Comparison setup
            productDataOptionAsbuilt1 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "QA_Style_Automation",
                Quantities = "20.00",
                Parameter = "LEVEL=level01|SWG=swg01"
            };

            productDataOptionAsbuilt2 = new ProductData()
            {
                Name = "QA_Product_02_Automation",
                Style = "QA_Style_Automation",
                Quantities = "26.00",
                Parameter = "LEVEL=level01|SWG=swg01|SWG=swg02"
            };
            productDataOptionAsbuilt3 = new ProductData()
            {
                Name = "QA_Product_03_Automation",
                Style = "QA_Style_Automation",
                Quantities = "60.00"
            };
            productToOptionAsbuilt1 = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { productDataOptionAsbuilt1 },
                ParameterList = new List<ProductData> { productDataOptionAsbuilt1 }
            };

            productToOptionAsbuilt2 = new ProductToOptionData()
            {
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation",
                ProductList = new List<ProductData> { productDataOptionAsbuilt2 },
                ParameterList = new List<ProductData> { productDataOptionAsbuilt2 }
            };

            productToOptionAsbuilt3 = new ProductToOptionData()
            {
                BuildingPhase = "QA_3-QA_BuildingPhase_03_Automation",
                ProductList = new List<ProductData> { productDataOptionAsbuilt3 }
            };

            productDataHouseAsbuilt1 = new ProductData(productDataOptionAsbuilt1);

            productDataHouseAsbuilt2 = new ProductData(productDataOptionAsbuilt2);

            productDataHouseAsbuilt3 = new ProductData(productDataOptionAsbuilt3);

            productToHouseAsbuilt1 = new ProductToOptionData(productToOptionAsbuilt1) { ProductList = new List<ProductData> { productDataHouseAsbuilt1 } };
            productToHouseAsbuilt2 = new ProductToOptionData(productToOptionAsbuilt2) { ProductList = new List<ProductData> { productDataHouseAsbuilt2 } };
            productToHouseAsbuilt3 = new ProductToOptionData(productToOptionAsbuilt3) { ProductList = new List<ProductData> { productDataHouseAsbuilt3 } };


            houseQuantitiesAsbuilt = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION1_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseAsbuilt1, productToHouseAsbuilt2, productToHouseAsbuilt3 }
            };

            //CSV + Comparison setup

            productDataOptionCsv1 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Quantities = "60.00",
                Parameter = "LEVEL=level01|SWG=swg01"
            };

            productDataOptionCsv2 = new ProductData()
            {
                Name = "QA_Product_01_Automation",
                Style = "DEFAULT",
                Quantities = "80.00",
                Parameter = "LEVEL=level01|SWG=swg01|SWG=swg02"
            };
            productDataOptionCsv3 = new ProductData()
            {
                Name = "QA_Product_02_Automation",
                Style = "DEFAULT",
                Quantities = "70.00"
            };
            productToOptionCsv1 = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { productDataOptionCsv1 },
                ParameterList = new List<ProductData> { productDataOptionCsv1 }
            };

            productToOptionCsv2 = new ProductToOptionData()
            {
                BuildingPhase = "QA_1-QA_BuildingPhase_01_Automation",
                ProductList = new List<ProductData> { productDataOptionCsv2 },
                ParameterList = new List<ProductData> { productDataOptionCsv2 }
            };

            productToOptionCsv3 = new ProductToOptionData()
            {
                BuildingPhase = "QA_2-QA_BuildingPhase_02_Automation",
                ProductList = new List<ProductData> { productDataOptionCsv3 }
            };

            productDataHouseCsv1 = new ProductData(productDataOptionCsv1);

            productDataHouseCsv2 = new ProductData(productDataOptionCsv2);

            productDataHouseCsv3 = new ProductData(productDataOptionCsv3);

            productToHouseCsv1 = new ProductToOptionData(productToOptionCsv1) { ProductList = new List<ProductData> { productDataHouseCsv1 } };
            productToHouseCsv2 = new ProductToOptionData(productToOptionCsv2) { ProductList = new List<ProductData> { productDataHouseCsv2 } };
            productToHouseCsv3 = new ProductToOptionData(productToOptionCsv3) { ProductList = new List<ProductData> { productDataHouseCsv3 } };

            houseQuantities_CSV = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION2_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseCsv1, productToHouseCsv2, productToHouseCsv3 }
            };

        }
        [Test]
        [Category("Section_IV")]
        public void A04_B_Assets_DetailPage_House_HouseQuantities_Parameter_field_is_truncated_incorrectly_when_user_exports_House_Quantities_to_XML_and_qty_has_2_parameters()
        {
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {housedata.HouseName} and create if it doesn't exist.</font>");
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
            //Add Option Into House
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION1_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION1_NAME_DEFAULT + " - " + OPTION1_CODE_DEFAULT);
            }

            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION2_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION2_NAME_DEFAULT + " - " + OPTION2_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Communities");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT, indexs);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>1. Upload this Pre-Import Modifications file: File uploaded successfully.</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>2. Tick Skip/Ignore Modification and click Upload</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>3. Choose the uploaded file and click Generate Report View</font>");

            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT);
            }
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(IMPORTTYPE_1, string.Empty,
                OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT, "ImportHouseQuantities_Pre_ImportFile_PIPE_49912.xml");
            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT1_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT1, PARAMETER_DEFAULT1_VALUE1) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT2, PARAMETER_DEFAULT2_VALUE1) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT1_DEFAULT} is imported successfully " +
                    $"with Parameter {PARAMETER_DEFAULT1} And Value: {PARAMETER_DEFAULT1_VALUE1}" +
                    $"Parameter {PARAMETER_DEFAULT2} And Value: {PARAMETER_DEFAULT2_VALUE1}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT1_DEFAULT} is imported unsuccessfully " +
                    $"with Parameter {PARAMETER_DEFAULT1}.</font>");
            }

            if (HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid("Product", PRODUCT2_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT1, PARAMETER_DEFAULT1_VALUE1) is true
                && HouseImportDetailPage.Instance.IsItemInImportQuantitiesGrid(PARAMETER_DEFAULT2, PARAMETER_DEFAULT2_VALUE1) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Product With Name {PRODUCT2_DEFAULT} is imported successfully " +
                    $"with Parameter {PARAMETER_DEFAULT1} And Value: {PARAMETER_DEFAULT1_VALUE1}" +
                    $"Parameter {PARAMETER_DEFAULT2} And Value: {PARAMETER_DEFAULT2_VALUE1}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Product With Name {PRODUCT2_DEFAULT} is imported unsuccessfully " +
                    $"with Parameter {PARAMETER_DEFAULT1}.</font>");
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>4. Click Import</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>5. Click Finish Import</font>");

            HouseImportDetailPage.Instance.ImportFileIntoHouseQuantitiesAfterUploadFileInGrid("ImportHouseQuantities_Pre_ImportFile_PIPE_49912.xml");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>6. Check House Quantities page to see if it displays data correctly," +
                $" especially the Parameter column</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            int totalItems = HouseQuantitiesDetailPage.Instance.GetTotalNumberItem();
            foreach (ProductToOptionData housequantity in houseQuantitiesPreImport1.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantitiesPreImport1.optionName) is true
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
                            $"<br>The expected Option: {houseQuantitiesPreImport1.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }

            foreach (ProductToOptionData housequantity in houseQuantitiesPreImport2.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantitiesPreImport2.optionName) is true
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
                            $"<br>The expected Option: {houseQuantitiesPreImport2.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Check the exported XML file to see if it displays data correctly, especially the Parameters.</font>");
            CommonHelper.ScrollToBeginOfPage();
            string exportFileName = CommonHelper.GetExportFileName(ExportType.DefaultHouseQuantities.ToString(), HOUSE_NAME_DEFAULT);

            //At Present.If Export House Quantities By XML File then sprint Zip File. Cannot Handdle Zip File By Automation 
            //HouseQuantitiesDetailPage.Instance.DownloadBaseLineFile(EXPORT_XML_MORE_MENU, exportFileName);
            //HouseQuantitiesDetailPage.Instance.ExportFile(EXPORT_XML_MORE_MENU, exportFileName, 0, string.Empty);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>8. Click Export CSV</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Check the exported CSV file, if it displays correct data, especially Parameter column.</font>");
            HouseQuantitiesDetailPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            HouseQuantitiesDetailPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.HOUSEQUANTITESWITHPARAMETER_TITLE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>9. Delete all House Quantities.</font>");
            HouseQuantitiesDetailPage.Instance.DeleteSelectedHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II. As - built + Comparison setup</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1. Upload this As-built file: File uploaded successfully</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.2. Click Upload</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.3 Choose uploaded file and Start Comparison Setup</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Import");

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT);
            }

            HouseImportDetailPage.Instance.ImportHouseQuantities(IMPORTTYPE_2, string.Empty, OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT, "ImportHouseQuantities_As_built_ImportFile_PIPE_49912.xml");

            HouseImportDetailPage.Instance.StartComparion();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.4. Click Add Comparison Group</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Choose option and click Insert</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>A new comparison group added successfully</font>");
            HouseImportDetailPage.Instance.AddComparisonGroups();
            HouseImportDetailPage.Instance.InsertItemComparsion(OPTION1_NAME_DEFAULT, string.Empty, string.Empty, INCLUDED_IMPORT_OPTION_NAME_DEFAULT);
            HouseImportDetailPage.Instance.VerifyItemComparisonGroups(OPTION1_NAME_DEFAULT, string.Empty, string.Empty, INCLUDED_IMPORT_OPTION_NAME_DEFAULT);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>5. Click Generate Comparisons</font>");
            HouseImportDetailPage.Instance.OpenGenerateComparison();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>6. Click Import</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>7. Click Finish Import</font>");

            HouseImportDetailPage.Instance.ImportFileIntoHouseQuantitiesAfterUploadFileInGrid("ImportHouseQuantities_As_built_ImportFile_PIPE_49912.xml");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>8 .Go to House Quantities and check if it displays data correctly, especially Parameter values.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            totalItems = HouseQuantitiesDetailPage.Instance.GetTotalNumberItem();
            foreach (ProductToOptionData housequantity in houseQuantitiesAsbuilt.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantitiesAsbuilt.optionName) is true
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
                            $"<br>The expected Option: {houseQuantitiesAsbuilt.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>9 .Click Export XML</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Open the exported XML file to see if it displays data correctly, especially Parameters.</font>");

            //At Present.If Export House Quantities By XML File then sprint Zip File. Cannot Handdle Zip File By Automation 
            //HouseQuantitiesDetailPage.Instance.DownloadBaseLineFile(EXPORT_XML_MORE_MENU, exportFileName);
            //HouseQuantitiesDetailPage.Instance.ExportFile(EXPORT_XML_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.HOUSEQUANTITESWITHPARAMETER_TITLE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>10 .Click Export Excel</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Open the exported Excel file to see if it displays data correctly, especially Parameters.</font>");

            HouseQuantitiesDetailPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
            HouseQuantitiesDetailPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.HOUSEQUANTITESWITHPARAMETER_TITLE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>11. Delete all House Quantities.</font>");
            HouseQuantitiesDetailPage.Instance.DeleteSelectedHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.CSV + Comparison setup</font>");


            HouseDetailPage.Instance.LeftMenuNavigation("Import");

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION1_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION1_NAME_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>1. Upload this CSV file: File uploaded successfully</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>2. Click Upload</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantities(IMPORTTYPE_3, string.Empty, OPTION2_NAME_DEFAULT, "ImportHouseQuantities_CSV_ImportFile_PIPE_49912.csv");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>3. Choose uploaded file and click Start Comparison Setup</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>4. Choose un-modified Option and click Add.</font>");
            HouseImportDetailPage.Instance.StartComparion();
            HouseImportDetailPage.Instance.VerifyComparisonGroups(ListOptions2);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>5 .Click Generate Comparison.</font>");
            HouseImportDetailPage.Instance.VerifyItemComparisonGroups(OPTION1_NAME_DEFAULT, string.Empty, string.Empty, INCLUDED_IMPORT_OPTION_NAME_DEFAULT);
            HouseImportDetailPage.Instance.VerifyItemComparisonGroups(OPTION2_NAME_DEFAULT, string.Empty, string.Empty, OPTION2_NAME_DEFAULT);
            HouseImportDetailPage.Instance.OpenGenerateComparison();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>6 .Click Import.</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>7. Click Finish Import.</font>");
            HouseImportDetailPage.Instance.ImportFileIntoHouseQuantitiesAfterUploadFileInGrid("ImportHouseQuantities_CSV_ImportFile_PIPE_49912.csv");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>8. Go to House Quantities to check if it displays data correctly, especially Parameter values.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            totalItems = HouseQuantitiesDetailPage.Instance.GetTotalNumberItem();
            foreach (ProductToOptionData housequantity in houseQuantities_CSV.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_CSV.optionName) is true
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
                            $"<br>The expected Option: {houseQuantities_CSV.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>9. Click Export XML.</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Open exported XML file to see if it displays data correctly, especially Parameters.</font>");

            //At Present.If Export House Quantities By XML File then sprint Zip File. Cannot Handdle Zip File By Automation 
            //HouseQuantitiesDetailPage.Instance.DownloadBaseLineFile(EXPORT_XML_MORE_MENU, exportFileName);
            //HouseQuantitiesDetailPage.Instance.ExportFile(EXPORT_XML_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.HOUSEQUANTITESWITHPARAMETER_TITLE);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>10 .Click Export Excel.</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Open exported Excel file to see if it displays data correctly, especially Parameters..</font>");
            HouseQuantitiesDetailPage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
            HouseQuantitiesDetailPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.HOUSEQUANTITESWITHPARAMETER_TITLE);
            HouseQuantitiesDetailPage.Instance.DeleteSelectedHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
        }

        [TearDown]
        public void ClearData()
        {
            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION2_NAME_DEFAULT);
            }

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION2_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION2_NAME_DEFAULT);
            }
        }
    }
}
