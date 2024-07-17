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
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Script.TestData;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class B09_C_PIPE_39505 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private SpecSetData SpecSetData1;
        private SpecSetData SpecSetData2;
        private SpecSetData SpecSetData3;
        private SpecSetData SpecSetData4;
        private SpecSetData SpecSetData5;

        private const string Manufacture2_NAME_DEFAULT = "QA_RT_New_Manu_Auto";
        private const string Style2_NAME_DEFAULT_DEFAULT = "QA_RT_New_Style_Auto";

        private const string Manufacture2_NAME_NEW = "QA_RT_New_Manu_Auto";
        private const string Style2_NAME_DEFAULT_NEW = "QA_RT_New_Style_Auto1";

        private const string Manufacture3_NAME_DEFAULT = "QA_RT_New_Manu_Auto";
        private const string Style3_NAME_DEFAULT_DEFAULT = "QA_RT_New_Style_Auto1";

        private const string Manufacture3_NAME_NEW = "QA_RT_New_Manu_Auto1";
        private const string Style3_NAME_NEW = "QA_RT_New_Style_Auto1";

        private const string Manufacture4_NAME_DEFAULT = "QA_RT_New_Manu_Auto";
        private const string Style4_NAME_DEFAULT_DEFAULT = "QA_RT_New_Style_Auto1";

        private const string Manufacture4_NAME_NEW = "QA_RT_New_Manu_Auto3";
        private const string Style4_NAME_NEW = "QA_RT_New_Style_Auto3";

        private const string Manufacture5_NAME_DEFAULT = "QA_RT_New_Manu_Auto";
        private const string Style5_NAME_DEFAULT_DEFAULT = "QA_RT_New_Style_Auto1";

        private const string Manufacture5_NAME_NEW = "QA_RT_New_Manu_Auto";
        private const string Style5_NAME_NEW = "QA_RT_New_Style_Auto1";


        readonly string ATTRIBUTE_NAME = "Houses";
        private const string COMMUNITY_CODE_DEFAULT = TestDataCommon.COMMUNITY_CODE_DEFAULT;
        private const string COMMUNITY_NAME_DEFAULT = TestDataCommon.COMMUNITY_NAME_DEFAULT;

        private const string HOUSE_NAME_DEFAULT = TestDataCommon.HOUSE_NAME_DEFAULT;
        private const string HOUSE_CODE_DEFAULT = TestDataCommon.HOUSE_CODE_DEFAULT;

        private const string OPTION_NAME_DEFAULT = TestDataCommon.OPTION_NAME_DEFAULT;
        private const string OPTION_CODE_DEFAULT = TestDataCommon.OPTION_CODE_DEFAULT;

        private readonly int[] indexs = new int[] { };
        private const string ImportType = "Pre-Import Modification";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";



        private ProductData productData_Option_1;
        private ProductToOptionData productToOption1;
        private ProductData productData_House_1_Style;
        private ProductToOptionData productToHouse1_Style;
        private HouseQuantitiesData houseQuantities_DefaultCommunity1_Style;
        private ProductToOptionData productToHouseBOM_Style1;
        private HouseQuantitiesData houseQuantities_HouseBOM_DefaultCommunity1_Style;


        private ProductData productData_Option_2;
        private ProductToOptionData productToOption2;
        private ProductData productData_House_2_Style;
        private ProductToOptionData productToHouse2_Style;
        private HouseQuantitiesData houseQuantities_DefaultCommunity2_Style;
        private ProductToOptionData productToHouseBOM_Style2;
        private HouseQuantitiesData houseQuantities_HouseBOM_DefaultCommunity2_Style;

        private ProductData productData_Option_3;
        private ProductToOptionData productToOption3;
        private ProductData productData_House_3_Style;
        private ProductToOptionData productToHouse3_Style;
        private HouseQuantitiesData houseQuantities_DefaultCommunity3_Style;
        private ProductToOptionData productToHouseBOM_Style3;
        private HouseQuantitiesData houseQuantities_HouseBOM_DefaultCommunity3_Style;

        [SetUp]
        public void SetUpData()
        {
            SpecSetData1 = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_39505_Automation",
                SpectSetName = "QA_RT_SpecSet_39505_Automation",
                OriginalManufacture = "QA_RT_New_Manu_Auto",
                OriginalStyle = "QA_RT_New_Style_Auto",
                OriginalUse = "ALL",
                NewManufacture = "QA_RT_New_Manu_Auto",
                NewStyle = "QA_RT_New_Style_Auto",
                NewUse = "NONE",
                StyleCalculation = "NONE"
            };

            SpecSetData2 = new SpecSetData(SpecSetData1)
            {
                OriginalManufacture = Manufacture2_NAME_DEFAULT,
                OriginalStyle = Style2_NAME_DEFAULT_DEFAULT,
                NewManufacture = Manufacture2_NAME_NEW,
                NewStyle = Style2_NAME_DEFAULT_NEW,
            };

            SpecSetData3 = new SpecSetData(SpecSetData1)
            {
                OriginalManufacture = Manufacture3_NAME_DEFAULT,
                OriginalStyle = Style3_NAME_DEFAULT_DEFAULT,
                NewManufacture = Manufacture3_NAME_NEW,
                NewStyle = Style3_NAME_NEW,

            };

            SpecSetData4 = new SpecSetData(SpecSetData1)
            {
                OriginalManufacture = Manufacture4_NAME_DEFAULT,
                OriginalStyle = Style4_NAME_DEFAULT_DEFAULT,
                NewManufacture = Manufacture4_NAME_NEW,
                NewStyle = Style4_NAME_NEW,
            };

            SpecSetData5 = new SpecSetData(SpecSetData1)
            {
                OriginalManufacture = Manufacture5_NAME_DEFAULT,
                OriginalStyle = Style5_NAME_DEFAULT_DEFAULT,
                NewManufacture = Manufacture5_NAME_NEW,
                NewStyle = Style5_NAME_NEW,
            };


            productData_Option_1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Style_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "10.00",
                Unit = "NONE"
            };


            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_1 }
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1_Style = new ProductData(productData_Option_1);

            productToHouse1_Style = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1_Style } };
            // There is no House quantities 
            houseQuantities_DefaultCommunity1_Style = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1_Style }
            };

            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_Style1 = new ProductData(productData_Option_1) { Style = "QA_RT_New_Style_Auto1" };

            productToHouseBOM_Style1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_HouseBOM_Style1 } };

            houseQuantities_HouseBOM_DefaultCommunity1_Style = new HouseQuantitiesData(houseQuantities_DefaultCommunity1_Style)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Style1 }
            };


            productData_Option_2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Style_Automation_02",
                Style = "QA_RT_New_Style_Auto1",
                Use = "NONE",
                Quantities = "20.00",
                Unit = "NONE"
            };


            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_2 }
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_2_Style = new ProductData(productData_Option_2);

            productToHouse2_Style = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2_Style } };
            // There is no House quantities 
            houseQuantities_DefaultCommunity2_Style = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse2_Style }
            };

            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_Style2 = new ProductData(productData_Option_2);

            productToHouseBOM_Style2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_HouseBOM_Style2 } };

            houseQuantities_HouseBOM_DefaultCommunity2_Style = new HouseQuantitiesData(houseQuantities_DefaultCommunity2_Style)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Style2 }
            };


            productData_Option_3 = new ProductData()
            {
                Name = "QA_RT_New_Product_Style_Automation_03",
                Style = "QA_RT_New_Style_Auto1",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE"
            };


            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_Option_3 }
            };


            /****************************** Create Product quantities on House ******************************/
            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_3_Style = new ProductData(productData_Option_3);

            productToHouse3_Style = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3_Style } };
            // There is no House quantities 
            houseQuantities_DefaultCommunity3_Style = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse3_Style }
            };

            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_Style3 = new ProductData(productData_Option_3) { Style = "QA_RT_New_Style_Auto3" };

            productToHouseBOM_Style3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_HouseBOM_Style3 } };

            houseQuantities_HouseBOM_DefaultCommunity3_Style = new HouseQuantitiesData(houseQuantities_DefaultCommunity3_Style)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_Style3 }
            };


        }
        [Test]
        [Category("Section_IV")]
        public void B09_C_Jobs_Infinitive_loop_generate_HouseBOM_in_case_configure_the_original_style_and_new_style_are_the_same()
        {
            //I. Same Manufacturer, same Style
            //I.1 Go to Spec Set, add new Style conversions. Manufacturer / Style original and the new are the same
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step I: Same Manufacturer, same Style.</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step I.1: Go to Spec Set, add new Style conversions. Manufacturer / Style original and the new are the same</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.ChangeSpecSetPageSize(20);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData1.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData1.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData1.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData1.GroupName);
            }

            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData1.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData1.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData1.GroupName);

            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            Assert.That(SpecSetDetailPage.Instance.IsModalDisplayed(), "<font color='red'>The add new spect set modal is not displayed. </font>");
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData1.SpectSetName);

            //Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            //I.2 Click Save icon
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step I.2: Click Save icon.</b></font>");
            //Add New Conversation Style
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Add New Conversation Style.</font>");
            SpecSetDetailPage.Instance.ClickAddNewConversationStyle();
            SpecSetDetailPage.Instance.SelectOriginalManufacture(SpecSetData1.OriginalManufacture);
            SpecSetDetailPage.Instance.SelectOriginalStyle(SpecSetData1.OriginalStyle);
            SpecSetDetailPage.Instance.SelectOriginalUse(SpecSetData1.OriginalUse);
            SpecSetDetailPage.Instance.SelectNewManufacture(SpecSetData1.NewManufacture);
            SpecSetDetailPage.Instance.SelectNewStyle(SpecSetData1.NewStyle);
            SpecSetDetailPage.Instance.SelectNewUse(SpecSetData1.NewUse);
            SpecSetDetailPage.Instance.SelectStyleCalculation(SpecSetData1.StyleCalculation);
            SpecSetDetailPage.Instance.PerformInsertStyle();

            string actualErrorMsg = SpecSetDetailPage.Instance.GetLastestToastMessage();
            string expectedErrorMsg = "An error occurred while trying to add the Style Conversion.";
            if (actualErrorMsg.Equals(expectedErrorMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The error message is displayed as expected.</b></font>");
                SpecSetDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The error message is not display as expected.</font> Actual result: {actualErrorMsg}");
                SpecSetDetailPage.Instance.CloseToastMessage();
            }

            //II. Same Manufacturer, different Style
            //1.Go to Spec Set
            //Add new Style conversions which have same manufacturer and different style
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step II: Same Manufacturer, different Style.</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step II.1: Go to Spec Set . Add new Style conversions which have same manufacturer and different style.</b></font>");
            SpecSetDetailPage.Instance.ClickAddNewConversationStyle();
            SpecSetDetailPage.Instance.SelectOriginalManufacture(SpecSetData2.OriginalManufacture);
            SpecSetDetailPage.Instance.SelectOriginalStyle(SpecSetData2.OriginalStyle);
            SpecSetDetailPage.Instance.SelectOriginalUse(SpecSetData2.OriginalUse);
            SpecSetDetailPage.Instance.SelectNewManufacture(SpecSetData2.NewManufacture);
            SpecSetDetailPage.Instance.SelectNewStyle(SpecSetData2.NewStyle);
            SpecSetDetailPage.Instance.SelectNewUse(SpecSetData2.NewUse);
            SpecSetDetailPage.Instance.SelectStyleCalculation(SpecSetData2.StyleCalculation);
            SpecSetDetailPage.Instance.PerformInsertStyle();

            //Select new Use, new Calculation
            ExtentReportsHelper.LogInformation(null, "Select new Use, new Calculation");
            ExtentReportsHelper.LogInformation(null, "Click Save icon");
            //Click Save icon
            string actualMsg = SpecSetDetailPage.Instance.GetLastestToastMessage();
            string expectedMsg = "Style Conversion Created";
            if (actualMsg.Equals(expectedMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Style Conversion created successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Style Conversion is create unsuccessfully.</font>");
            }

            if (SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData2.SpectSetName, "OriginalMfg_Name", SpecSetData2.OriginalManufacture) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData2.SpectSetName, "OriginalStyle_Name", SpecSetData2.OriginalStyle) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData2.SpectSetName, "OriginalUse", SpecSetData2.OriginalUse) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData2.SpectSetName, "NewMfg_Name", SpecSetData2.NewManufacture) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData2.SpectSetName, "NewStyle_Name", SpecSetData2.NewStyle) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData2.SpectSetName, "NewUse", SpecSetData2.NewUse) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData2.SpectSetName, "Calculation", SpecSetData2.StyleCalculation) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up Style Conversion on Spec Set Group <b>'{SpecSetData2.SpectSetName}'</b> is correct.</font>");

            else
                ExtentReportsHelper.LogWarning("<font color='yellow'>The set up Style Conversion on this page is NOT same as expectation. " +
                            "The next step and the  result after generating a BOM can be incorrect." +
                            $"<br>The expected Original Manufacturer/Style: {SpecSetData2.OriginalManufacture}" +
                            $"<br>The expected Original Use: { SpecSetData2.OriginalStyle}" +
                            $"<br>The expected New Manufacturer/Style: {SpecSetData2.NewManufacture}" +
                            $"<br>The expected New Use: {SpecSetData2.NewUse}" +
                            $"<br>The expected Calculation: {SpecSetData2.StyleCalculation}</br></font>");


            //2.Assign Spec Set to House
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step II.2: Assign Spec Set to House.</b></font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(ATTRIBUTE_NAME, HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT, SpecSetData1.SpectSetName, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT);
            CommonHelper.RefreshPage();

            //Make sure current transfer seperation character is ','
            ExtentReportsHelper.LogInformation(null, "Navigate to Settings > Group by Parameters settings is turned on.");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();

            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);
            ExtentReportsHelper.LogInformation(null, "Select Default House BOM View is Basic.");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);
            ExtentReportsHelper.LogInformation(null, "Back to Setting Page to change House BOM Product Orientation is turned false.");
            BOMSettingPage.Instance.Check_House_BOM_Product_Orientation(false);

            //Navigate to House default page
            ExtentReportsHelper.LogInformation(null, "Navigate to House default page.");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }

            //3.Go to House Quantities, add Product has original manufacturer / style above
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step II.3: Go to House Quantities, add Product has original manufacturer / style above</b></font>");

            HouseCommunities.Instance.LeftMenuNavigation("Import");
            string HouseImport_url = HouseImportDetailPage.Instance.CurrentURL;
            //Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_39505_1.xml");

            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "Verify the set up data for product quantities on House.");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            foreach (ProductToOptionData housequantity in houseQuantities_DefaultCommunity1_Style.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_DefaultCommunity1_Style.optionName) is true
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
                            $"<br>The expected Option: {houseQuantities_DefaultCommunity1_Style.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }
            //4.Go to House BOM, select community
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step II.4: Go to House BOM, select community</b></font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");

            //5.Generate House BOM
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step II.5: Generate House BOM</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_DefaultCommunity1_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity1_Style.communityName);
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_DefaultCommunity1_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity1_Style.communityName);           
            HouseBOMDetailPage.Instance.VerifyItemWithStyleOnHouseBOMGrid(houseQuantities_HouseBOM_DefaultCommunity1_Style);

            CommonHelper.RefreshPage();
            // Verify Quantities And Product In BOM Trace 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify Quantities And Product In BOM Trace .</font>");
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_DefaultCommunity1_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity1_Style.communityName);
            HouseBOMDetailPage.Instance.ViewBOMtrace(houseQuantities_HouseBOM_DefaultCommunity1_Style);

            //III.Different Manufacturer, same Style
            //1.Go to Spec Set. Add new Style conversions which have different manufacturer and same style
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step III: Different Manufacturer, same Style</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step III.1: Go to Spec Set. Add new Style conversions which have different manufacturer and same style</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData3.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData3.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData3.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData3.GroupName);
            }
            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData3.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData3.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData3.GroupName);


            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData3.SpectSetName);
            //Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            SpecSetDetailPage.Instance.ClickAddNewConversationStyle();
            SpecSetDetailPage.Instance.SelectOriginalManufacture(SpecSetData3.OriginalManufacture);
            SpecSetDetailPage.Instance.SelectOriginalStyle(SpecSetData3.OriginalStyle);
            SpecSetDetailPage.Instance.SelectOriginalUse(SpecSetData3.OriginalUse);
            SpecSetDetailPage.Instance.SelectNewManufacture(SpecSetData3.NewManufacture);
            SpecSetDetailPage.Instance.SelectNewStyle(SpecSetData3.NewStyle);
            SpecSetDetailPage.Instance.SelectNewUse(SpecSetData3.NewUse);
            SpecSetDetailPage.Instance.SelectStyleCalculation(SpecSetData3.StyleCalculation);
            SpecSetDetailPage.Instance.PerformInsertStyle();

            //Select new Use, new Calculation
            ExtentReportsHelper.LogInformation(null, $"Select new Use, new Calculation");
            //Click Save icon
            ExtentReportsHelper.LogInformation(null, $"Click Save icon");
            actualMsg = SpecSetDetailPage.Instance.GetLastestToastMessage();
            expectedMsg = "Style Conversion Created";
            if (actualMsg.Equals(expectedMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Style Conversion created successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Style Conversion is create unsuccessfully.</font>");
            }

            if (SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData3.SpectSetName, "OriginalMfg_Name", SpecSetData3.OriginalManufacture) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData3.SpectSetName, "OriginalStyle_Name", SpecSetData3.OriginalStyle) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData3.SpectSetName, "OriginalUse", SpecSetData3.OriginalUse) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData3.SpectSetName, "NewMfg_Name", SpecSetData3.NewManufacture) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData3.SpectSetName, "NewStyle_Name", SpecSetData3.NewStyle) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData3.SpectSetName, "NewUse", SpecSetData3.NewUse) is true
            && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData3.SpectSetName, "Calculation", SpecSetData3.StyleCalculation) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up Style Conversion on Spec Set Group <b>'{SpecSetData3.SpectSetName}'</b> is correct.</font>");

            else
                ExtentReportsHelper.LogWarning("<font color='yellow'>The set up Style Conversion on this page is NOT same as expectation. " +
                            "The next step and the  result after generating a BOM can be incorrect." +
                            $"<br>The expected Original Manufacturer/Style: {SpecSetData3.OriginalManufacture}" +
                            $"<br>The expected Original Use: { SpecSetData3.OriginalStyle}" +
                            $"<br>The expected New Manufacturer/Style: {SpecSetData3.NewManufacture}" +
                            $"<br>The expected New Use: {SpecSetData3.NewUse}" +
                            $"<br>The expected Calculation: {SpecSetData3.StyleCalculation}</br></font>");

            //2.Assign Spec Set to House
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step III.2: Assign Spec Set to House</b></font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(ATTRIBUTE_NAME, HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT, SpecSetData3.SpectSetName, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT);

            //3.Go to House Quantities, add Product has original manufacturer / style above
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step III.3: Go to House Quantities, add Product has original manufacturer / style above</b></font>");
            CommonHelper.OpenURL(HouseImport_url);


            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_39505_2.xml");


            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "Verify the set up data for product quantities on House.");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            foreach (ProductToOptionData housequantity in houseQuantities_DefaultCommunity2_Style.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_DefaultCommunity2_Style.optionName) is true
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
                            $"<br>The expected Option: {houseQuantities_DefaultCommunity2_Style.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }

            //4.Go to House BOM, select community
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step III.4: Go to House BOM, select community</b></font>");
            //Navigate To House BOM
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");

            //5.Generate House BOM
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step III.5: Generate House BOM</b></font>");

            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_DefaultCommunity2_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity2_Style.communityName);

            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_DefaultCommunity2_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity2_Style.communityName);

            //Verify quantities are grouped by parameters with setting on House BOM is working fine, with qty separated by parameters
            HouseBOMDetailPage.Instance.VerifyItemWithStyleOnHouseBOMGrid(houseQuantities_HouseBOM_DefaultCommunity2_Style);


            CommonHelper.RefreshPage();
            // Verify Quantities And Product In BOM Trace 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify Quantities And Product In BOM Trace .</font>");
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_DefaultCommunity2_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity2_Style.communityName);
            HouseBOMDetailPage.Instance.ViewBOMtrace(houseQuantities_HouseBOM_DefaultCommunity2_Style);


            //VI.Different Manufacturer, different Style
            //1.Go to Spec Set, add new Style conversions which have different manufacturer and different style
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step VI: Different Manufacturer, different Style</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step VI.1: Go to Spec Set, add new Style conversions which have different manufacturer and different style</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData4.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData4.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData4.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData4.GroupName);
            }
            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData4.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData4.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData4.GroupName);

            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData4.SpectSetName);
            //Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            //Add new Style conversions which have different manufacturer and same style
            SpecSetDetailPage.Instance.ClickAddNewConversationStyle();
            SpecSetDetailPage.Instance.SelectOriginalManufacture(SpecSetData4.OriginalManufacture);
            SpecSetDetailPage.Instance.SelectOriginalStyle(SpecSetData4.OriginalStyle);
            SpecSetDetailPage.Instance.SelectOriginalUse(SpecSetData4.OriginalUse);
            SpecSetDetailPage.Instance.SelectNewManufacture(SpecSetData4.NewManufacture);
            SpecSetDetailPage.Instance.SelectNewStyle(SpecSetData4.NewStyle);
            SpecSetDetailPage.Instance.SelectNewUse(SpecSetData4.NewUse);
            SpecSetDetailPage.Instance.SelectStyleCalculation(SpecSetData4.StyleCalculation);
            SpecSetDetailPage.Instance.PerformInsertStyle();

            //Select new Use, new Calculation

            //Click Save icon
            actualMsg = SpecSetDetailPage.Instance.GetLastestToastMessage();
            expectedMsg = "Style Conversion Created";
            if (actualMsg.Equals(expectedMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Style Conversion created successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Style Conversion is create unsuccessfully.</font>");
            }

            //2.Assign Spec Set to House
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step VI.2: Assign Spec Set to House</b></font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(ATTRIBUTE_NAME, HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT, SpecSetData4.SpectSetName, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT);

            //3.Go to House Quantities, add Product has original manufacturer / style above
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step VI.3: Go to House Quantities, add Product has original manufacturer / style above</b></font>");
            CommonHelper.OpenURL(HouseImport_url);


            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_39505_3.xml");

            ExtentReportsHelper.LogInformation(null, "Verify the set up data for product quantities on House.");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            foreach (ProductToOptionData housequantity in houseQuantities_DefaultCommunity3_Style.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_DefaultCommunity3_Style.optionName) is true
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
                            $"<br>The expected Option: {houseQuantities_DefaultCommunity3_Style.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }

            //4.Go to House BOM, select community
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step VI.4: Go to House BOM, select community</b></font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");
            //5.Generate House BOM
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step VI.5: Generate House BOM</b></font>");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities_HouseBOM_DefaultCommunity3_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity3_Style.communityName);
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_DefaultCommunity3_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity3_Style.communityName);
            HouseBOMDetailPage.Instance.VerifyItemWithStyleOnHouseBOMGrid(houseQuantities_HouseBOM_DefaultCommunity3_Style);

            CommonHelper.RefreshPage();
            // Verify Quantities And Product In BOM Trace 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify Quantities And Product In BOM Trace .</font>");
            HouseBOMDetailPage.Instance.SelectCommunity(houseQuantities_HouseBOM_DefaultCommunity3_Style.communityCode + "-" + houseQuantities_HouseBOM_DefaultCommunity3_Style.communityName);
            HouseBOMDetailPage.Instance.ViewBOMtrace(houseQuantities_HouseBOM_DefaultCommunity3_Style);

            //V.Update existing Spec Set
            //Go to any Spec Set detail to edit existing Style conversion.
            //1.Change New Manufacturer/ Style column to same value with original
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step V: Update existing Spec Set</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step V.1: Change New Manufacturer/Style column to same value with original</b></font>");

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData5.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData5.GroupName) is true)
            {
                SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData5.GroupName);
            }

            //Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();
            //2.Click update icon
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step V.2: Click update icon</b></font>");
            SpecSetDetailPage.Instance.EditItemOnStyleConversionsInGrid(SpecSetData5.OriginalStyle);
            SpecSetDetailPage.Instance.UpdateStyleConversion(SpecSetData5);
            string expectedMsgUpdatedStyle = "An error occurred while trying to add the Style Conversion.";
            string actualMsgUpdatedStyle = SpecSetDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsgUpdatedStyle.Equals(actualMsgUpdatedStyle))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The error message is displayed as expected.</b></font>");
                SpecSetDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The error message is not display as expected. Actual result: {actualMsgUpdatedStyle}</i></font>");
            }
        }

        [TearDown]
        public void DeleteData()
        {
            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "Delete File House Quantities.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }

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
