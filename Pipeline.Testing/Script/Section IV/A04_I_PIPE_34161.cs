using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;
using NUnit.Framework;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Script.TestData;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Common.Export;
using Pipeline.Testing.Pages.Assets.Communities.Products;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_I_PIPE_34161 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private SpecSetData specSetData;
        private SpecSetData productConversions;

        private OptionQuantitiesData optionPhaseQuantitiesData1;
        private OptionQuantitiesData optionPhaseQuantitiesData2;
        private OptionQuantitiesData optionPhaseQuantitiesData3;
        private CommunityQuantitiesData communityQuantitiesData;

        private int totalItems;
        private string exportFileName;

        private const string COMMUNITY_CODE_DEFAULT = TestDataCommon.COMMUNITY_CODE_DEFAULT;
        private const string COMMUNITY_NAME_DEFAULT = TestDataCommon.COMMUNITY_NAME_DEFAULT;

        private const string HOUSE_NAME_DEFAULT = TestDataCommon.HOUSE_NAME_DEFAULT;
        private const string HOUSE_CODE_DEFAULT = TestDataCommon.HOUSE_CODE_DEFAULT;
        private const string OPTION_NAME_DEFAULT = TestDataCommon.OPTION_NAME_DEFAULT;
        private const string OPTION_CODE_DEFAULT = TestDataCommon.OPTION_CODE_DEFAULT;

        private const string BUILDINGPHASE1_DEFAULT = "123-QA_RT_BuildingPhase1_Automation";
        private const string BUILDINGPHASE2_DEFAULT = "124-QA_RT_BuildingPhase2_Automation";
        private const string BUILDINGPHASE3_DEFAULT = "125-QA_RT_BuildingPhase5_Automation";
        private const string ImportType = "Pre-Import Modification";

        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";
        private const string EXPORT_EXCEL_MORE_MENU = "Excel";
        readonly string OPTION_NAME = "Options";

        private ProductData productData_Option_ZeroQuantities1;
        private ProductData productData_Option_ZeroQuantities2;
        private ProductData productData_Option_ZeroQuantities3;
        private ProductData productData_Option_1;
        private ProductData productData_Option_2;


        private ProductToOptionData productToOption_ZeroQuantities1;
        private ProductToOptionData productToOption_ZeroQuantities2;
        private ProductToOptionData productToOption_ZeroQuantities3;
        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToOption3;

        private ProductData productData_House_ZeroQuantities3;
        private ProductData productData_House_2;
        private ProductData productData_House_3;


        private ProductToOptionData productToHouse_ZeroQuantities3;
        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;


        private ProductToOptionData productToHouseBOM_ZeroQuantities1;
        private ProductToOptionData productToHouseBOM_ZeroQuantities2;
        private ProductToOptionData productToHouseBOM_ZeroQuantities3;
        private ProductToOptionData productToHouseBOM1;
        private HouseQuantitiesData houseQuantities_SpecificCommunity;
        private HouseQuantitiesData houseQuantities_HouseBOM;
        private HouseQuantitiesData houseQuantities3_HouseBOM;
        private HouseQuantitiesData houseZeroQuantities_HouseBOM1;
        private HouseQuantitiesData houseZeroQuantities_HouseBOM2;
        private HouseQuantitiesData houseZeroQuantities_HouseBOM3;
        private HouseQuantitiesData houseQuantities_SpecificCommunity_HouseBOM;

        [SetUp]
        public void GetTestData()
        {
            specSetData = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_Automation",
                UseDefault = "TRUE",
                SpectSetName = "QA_RT_SpecSet_Automation",
            };
            productConversions = new SpecSetData()
            {
                OriginalPhase = "123-QA_RT_BuildingPhase1_Automation",
                OriginalCategory = "QA_RT_Category1_Automation",
                OriginalProduct = "QA_RT_Product1_Automation",
                OriginalProductStyle = "QA_RT_Style1_Automation",
                OriginalProductUse = "ALL",
                NewPhase = "124-QA_RT_BuildingPhase2_Automation",
                NewCategory = "QA_RT_Category2_Automation",
                NewProduct = "QA_RT_Product2_Automation",
                NewProductStyle = "QA_RT_Style2_Automation",
                NewProductUse = "NONE",
                ProductCalculation = "NONE"

            };

            optionPhaseQuantitiesData1 = new OptionQuantitiesData()
            {
                BuildingPhase = "123-QA_RT_BuildingPhase1_Automation",
                ProductName = "QA_RT_Product1_Automation",
                Category = "QA_RT_Category1_Automation",
                Style = "QA_RT_Style1_Automation",
                Condition = false,
                Use = "NONE",
                Quantity = "14.00",
                Source = "Pipeline"
            };


            communityQuantitiesData = new CommunityQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                BuildingPhase = "126-QA_RT_BuildingPhase4_Automation",
                ProductName = "QA_RT_Product4_Automation",
                Style = "QA_RT_Style4_Automation",
                Condition = false,
                Use = "NONE",
                Quantity = "10.00",
                Source = "Pipeline"
            };

            optionPhaseQuantitiesData2 = new OptionQuantitiesData()
            {
                BuildingPhase = "126-QA_RT_BuildingPhase4_Automation",
                ProductName = "QA_RT_Product4_Automation",
                Category = "QA_RT_Category4_Automation",
                Style = "QA_RT_Style4_Automation",
                Condition = false,
                Use = "NONE",
                Quantity = "-6.00",
                Source = "Pipeline"
            };

            optionPhaseQuantitiesData3 = new OptionQuantitiesData()
            {
                BuildingPhase = "127-QA_RT_BuildingPhase5_Automation",
                ProductName = "QA_RT_Product5_Automation",
                Category = "QA_RT_Category5_Automation",
                Style = "QA_RT_Style5_Automation",
                Condition = false,
                Use = "NONE",
                Quantity = "0.00",
                Source = "Pipeline"
            };

            productData_Option_ZeroQuantities1 = new ProductData()
            {
                Name = "QA_RT_Product1_Automation",
                Style = "QA_RT_Style1_Automation",
                Quantities = "0.00",
                Unit = "NONE",
            };

            productData_Option_ZeroQuantities2 = new ProductData()
            {
                Name = "QA_RT_Product4_Automation",
                Style = "QA_RT_Style4_Automation",
                Quantities = "0.00",
                Unit = "NONE",
            };

            productData_Option_ZeroQuantities3 = new ProductData()
            {
                Name = "QA_RT_Product5_Automation",
                Style = "QA_RT_Style5_Automation",
                Quantities = "0.00",
                Unit = "NONE",
            };

            productData_Option_1 = new ProductData()
            {
                Name = "QA_RT_Product2_Automation",
                Style = "QA_RT_Style2_Automation",
                Quantities = "14.00",
                Unit = "NONE",
            };

            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_Product4_Automation",
                Style = "QA_RT_Style4_Automation",
                Quantities = "-4.00",
                Unit = "NONE",
            };


            productToOption_ZeroQuantities1 = new ProductToOptionData()
            {
                BuildingPhase = "123-QA_RT_BuildingPhase1_Automation",
                ProductList = new List<ProductData> { productData_Option_ZeroQuantities1 },
            };

            productToOption_ZeroQuantities2 = new ProductToOptionData()
            {
                BuildingPhase = "126-QA_RT_BuildingPhase4_Automation",
                ProductList = new List<ProductData> { productData_Option_ZeroQuantities2 },
            };
            productToOption_ZeroQuantities3 = new ProductToOptionData()
            {
                BuildingPhase = "127-QA_RT_BuildingPhase5_Automation",
                ProductList = new List<ProductData> { productData_Option_ZeroQuantities3 },
            };

            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "124-QA_RT_BuildingPhase2_Automation",
                ProductList = new List<ProductData> { productData_Option_1 },
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "126-QA_RT_BuildingPhase4_Automation",
                ProductList = new List<ProductData> { productData_Option_2 },
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2);

            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };


            // There is no House quantities 

            houseQuantities_SpecificCommunity = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> {productToHouse2 }
            };

            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_ZeroQuantities1 = new ProductData(productData_Option_ZeroQuantities1);
            ProductData productData_HouseBOM_ZeroQuantities2 = new ProductData(productData_Option_ZeroQuantities2);
            ProductData productData_HouseBOM_ZeroQuantities3 = new ProductData(productData_Option_ZeroQuantities3);


            productToHouseBOM_ZeroQuantities1 = new ProductToOptionData(productToOption_ZeroQuantities1) { ProductList = new List<ProductData> { productData_HouseBOM_ZeroQuantities1 } };
            productToHouseBOM_ZeroQuantities2 = new ProductToOptionData(productToOption_ZeroQuantities2) { ProductList = new List<ProductData> { productData_HouseBOM_ZeroQuantities2 } };
            productToHouseBOM_ZeroQuantities3 = new ProductToOptionData(productToOption_ZeroQuantities3) { ProductList = new List<ProductData> { productData_HouseBOM_ZeroQuantities3 } };

            ProductData productData_HouseBOM_1 = new ProductData(productData_Option_1);

            productToHouseBOM1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };

            houseZeroQuantities_HouseBOM1 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM_ZeroQuantities1 }
            };
            houseZeroQuantities_HouseBOM2 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM_ZeroQuantities1, productToHouseBOM_ZeroQuantities2 }
            };


            houseZeroQuantities_HouseBOM3 = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM_ZeroQuantities3 }
            };


            houseQuantities_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM1 }
            };

            houseQuantities_SpecificCommunity_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM1 }
            };

            houseQuantities3_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouseBOM1 }
            };
        }

        [Test]
        [Category("Section_IV")]
        public void A04_I_Assets_DetailPage_Houses_HouseBOMPage_Zero_Quantities_Showing_on_house_BOM()
        {
            //I. Verify House BOM with quantities = 0 when using Spec Set - Product Conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I. Verify House BOM with quantities = 0 when using Spec Set - Product Conversion.</font>");
            //1.1 Settings “House BOM Show Zero Quantities:” is True
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.1.1 Settings “House BOM Show Zero Quantities:” is True.</font>");
            //Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);

            string seperationCharacter = ','.ToString();
            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("Estimating");
            EstimatingPage.Instance.Check_Show_Category_On_Product_Conversion(false);

            SettingPage.Instance.LeftMenuNavigation("BOM");
            string BOMSetting_url = BOMSettingPage.Instance.CurrentURL;
            BOMSettingPage.Instance.SelectHouseBOMShowZeroQuantities(true);
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);

            //1.2 Using Spec Set -Product Conversion to convert product A->B
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.1.2 Using Spec Set -Product Conversion to convert product A->B.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData.GroupName);
            }

            SpecSetPage.Instance.CreateNewSpecSetGroup(specSetData.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", specSetData.GroupName);
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(specSetData.SpectSetName);
            // Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            //Add Product and Style to Spec Set
            ExtentReportsHelper.LogInformation(null, "Add Product and Style to Spec Set.");
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(productConversions);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(productConversions);


            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(OPTION_NAME, OPTION_NAME_DEFAULT, specSetData.SpectSetName, string.Empty);
 
            //1.3 Add Option and House Qty with same qty is product A
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.1.3 Add Option and House Qty with same qty is product A.</font>");
            // Go to Option default page
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, OPTION_CODE_DEFAULT);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);

            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME_DEFAULT))
            {
                OptionPage.Instance.SelectItemInGrid("Name", OPTION_NAME_DEFAULT);

                OptionDetailPage.Instance.LeftMenuNavigation("Products");
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Switch to Option/ Product page. Add a new option quantity if it does NOT exist on phase '{BUILDINGPHASE1_DEFAULT}'.</b></font>");

                //ProductsToOptionPage.Instance.DeleteAllOptionQuantities();

                ProductsToOptionPage.Instance.CloseToastMessage();
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", BUILDINGPHASE1_DEFAULT) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionPhaseQuantitiesData1);
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, COMMUNITY_NAME_DEFAULT);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY_NAME_DEFAULT))
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY_NAME_DEFAULT);

            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {

                ExtentReportsHelper.LogInformation($"House with Name {HOUSE_NAME_DEFAULT} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }

            //1.4 Generate House BOM -> Qty A will still not display, only qty B
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1.4 Generate House BOM -> Qty A will still not display, only qty B.</font>");
            //Navigate To House BOM
            HouseDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the url of House BOM
            string HouseBOM_url = HouseBOMDetailPage.Instance.CurrentURL;

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberItem();

            //Generate House BOM and verify it
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseZeroQuantities_HouseBOM1.communityCode + "-" + houseZeroQuantities_HouseBOM1.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseZeroQuantities_HouseBOM1.communityCode + "-" + houseZeroQuantities_HouseBOM1.communityName);
            
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseZeroQuantities_HouseBOM1);

            // Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.House_BOM.ToString(), COMMUNITY_NAME_DEFAULT, HOUSE_NAME_DEFAULT);

            //Export CSV file and make sure the export file existed.
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName, string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);

            //1.5 Turn the settings to False -> Qty A will still not display, only qty B
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I.1.5 Turn the settings to False -> Qty A will still not display, only qty B.</font>");
            CommonHelper.OpenURL(BOMSetting_url);
            BOMSettingPage.Instance.SelectHouseBOMShowZeroQuantities(false);
            // Check exported House BOM, make sure data is correct(no 0 qty)
            CommonHelper.OpenURL(HouseBOM_url);

            CommonHelper.ScrollToEndOfPage();

            //Generate House BOM and verify it
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM.communityCode + "-" + houseQuantities_HouseBOM.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM.communityCode + "-" + houseQuantities_HouseBOM.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM);

            //Export CSV file and make sure the export file existed.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4 : Export House BOM, with all BP and 1 specific BP with Group by Parameters settings is turned on.</b></font>");
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (1)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (1)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);



            //II. Verify House BOM with quantity sum = 0 when using Community, Option & House Qty
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II. Verify House BOM with quantity sum = 0 when using Community, Option & House Qty.</font>");
            // 1.Settings “House BOM Show Zero Quantities:” is True
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II.1.Settings “House BOM Show Zero Quantities:” is True.</font>");
            CommonHelper.OpenURL(BOMSetting_url);
            BOMSettingPage.Instance.SelectHouseBOMShowZeroQuantities(true);

            // 2.Product A is added as many Community, Option & House Quantities, with sum = 0
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II.2.Product A is added as many Community, Option & House Quantities, with sum = 0.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, COMMUNITY_NAME_DEFAULT);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY_NAME_DEFAULT))
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY_NAME_DEFAULT);

            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            if (CommunityProductsPage.Instance.IsItemInCommunityQuantitiesGrid(communityQuantitiesData.BuildingPhase, communityQuantitiesData.ProductName) is false)
            {
                // Add a new option quantitiy if it doesn't exist
                CommunityProductsPage.Instance.AddCommunityQuantities(communityQuantitiesData);
            }

            // Go to Option default page
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);

            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME_DEFAULT))
            {
                OptionPage.Instance.SelectItemInGrid("Name", OPTION_NAME_DEFAULT);

                OptionDetailPage.Instance.LeftMenuNavigation("Products");

                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", BUILDINGPHASE2_DEFAULT) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionPhaseQuantitiesData2);
                }

            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {

                ExtentReportsHelper.LogInformation($"House with Name {HOUSE_NAME_DEFAULT} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }

            // Navigate To Import House Quantities
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Processing the import with default community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Processing the import with default community.</font>");
            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_34161.xml");
            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Go to House quantities and check data.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + "-" + houseQuantities_SpecificCommunity.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities_SpecificCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_SpecificCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_SpecificCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            // 3.Generate House BOM -> Qty A displays with 0
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II.3.Generate House BOM -> Qty A displays with 0.</font>");
            //Navigate To House BOM
            HouseDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the url of House BOM
             HouseBOM_url = HouseBOMDetailPage.Instance.CurrentURL;

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberItem();

            //Generate House BOM and verify it
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseZeroQuantities_HouseBOM2.communityCode + "-" + houseZeroQuantities_HouseBOM2.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseZeroQuantities_HouseBOM2.communityCode + "-" + houseZeroQuantities_HouseBOM2.communityName);

            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseZeroQuantities_HouseBOM2);

            //Export EXCEL file and make sure the export file existed.
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (2)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (2)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);
            // 4.Turn the settings to False -> Qty A will not display
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II.4.Turn the settings to False -> Qty A will not display.</font>");
            CommonHelper.OpenURL(BOMSetting_url);
            BOMSettingPage.Instance.SelectHouseBOMShowZeroQuantities(false);

            // Check exported House BOM, make sure data is correct(no 0 qty)
            CommonHelper.OpenURL(HouseBOM_url);
            CommonHelper.ScrollToEndOfPage();

            //Generate House BOM and verify it
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_SpecificCommunity_HouseBOM.communityCode + "-" + houseQuantities_SpecificCommunity_HouseBOM.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_SpecificCommunity_HouseBOM.communityCode + "-" + houseQuantities_SpecificCommunity_HouseBOM.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_SpecificCommunity_HouseBOM);

            //Export CSV file and make sure the export file existed.
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (3)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (3)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);


            //III. Verify House BOM with original quantities = 0 when using Option Qty
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III. Verify House BOM with original quantities = 0 when using Option Qty.</font>");
            // 1.Settings “House BOM Show Zero Quantities:” is True
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.1.Settings “House BOM Show Zero Quantities:” is True.</font>");
            CommonHelper.OpenURL(BOMSetting_url);
            BOMSettingPage.Instance.SelectHouseBOMShowZeroQuantities(true);
            // 2.Product A is added as many Community, Option & House Quantities, with sum = 0
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.2.Product A is added as many Community, Option & House Quantities, with sum = 0.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, COMMUNITY_NAME_DEFAULT);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY_NAME_DEFAULT))
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY_NAME_DEFAULT);

            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {

                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }

            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + "-" + houseQuantities_SpecificCommunity.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
            // Go to Option default page
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);

            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME_DEFAULT))
            {
                OptionPage.Instance.SelectItemInGrid("Name", OPTION_NAME_DEFAULT);

                OptionDetailPage.Instance.LeftMenuNavigation("Products");

                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", BUILDINGPHASE3_DEFAULT) is false)
                {
                    // Add a new option quantitiy if it doesn't exist
                    ProductsToOptionPage.Instance.AddOptionQuantities(optionPhaseQuantitiesData3);
                }

            }

            // 3.Generate House BOM -> Qty A displays with 0
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.3. Generate House BOM -> Qty A displays with 0.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {

                ExtentReportsHelper.LogInformation($"House with Name {HOUSE_NAME_DEFAULT} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }

            HouseDetailPage.Instance.LeftMenuNavigation("House BOM");

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            totalItems = HouseBOMDetailPage.Instance.GetTotalNumberItem();

            //Generate House BOM and verify it
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseZeroQuantities_HouseBOM3.communityCode + "-" + houseZeroQuantities_HouseBOM3.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseZeroQuantities_HouseBOM3.communityCode + "-" + houseZeroQuantities_HouseBOM3.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseZeroQuantities_HouseBOM3);

            //Export EXCEL file and make sure the export file existed.
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (4)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (4)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);
            // 4.Turn the settings to False -> Qty A will not display
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.4.Turn the settings to False -> Qty A will not display.</font>");
            CommonHelper.OpenURL(BOMSetting_url);
            BOMSettingPage.Instance.SelectHouseBOMShowZeroQuantities(false);


            // Check exported House BOM, make sure data is correct(no 0 qty)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>III. Check exported House BOM, make sure data is correct(no 0 qty).</font>");
            CommonHelper.OpenURL(HouseBOM_url);
            CommonHelper.ScrollToEndOfPage();

            //Generate House BOM and verify it
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities3_HouseBOM.communityCode + "-" + houseQuantities3_HouseBOM.communityName);

            CommonHelper.RefreshPage();

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities3_HouseBOM.communityCode + "-" + houseQuantities3_HouseBOM.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities3_HouseBOM);

            //Export EXCEL file and make sure the export file existed.
            HouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (5)", string.Empty);
            HouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_EXCEL_MORE_MENU, $"{exportFileName} (5)", totalItems, ExportTitleFileConstant.HOUSEBOMPRODUCT_TITLE, string.Empty);
            //HouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);
        }
        [TearDown]
        public void DeleteData()
        {
            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

        }
        }
    }
