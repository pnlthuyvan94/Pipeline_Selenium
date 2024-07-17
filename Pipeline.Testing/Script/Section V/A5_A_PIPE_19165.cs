using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.Products;
using System.Collections.Generic;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Common.Constants;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Jobs.Job.Options;

namespace Pipeline.Testing.Script.Section_V
{
    public partial class A5_A_PIPE_19165 : BaseTestScript
    {
        /// <summary>
        /// Style Conversion class using for spec set swap
        /// </summary>
        public class StyleConversion
        {
            public string SpecSetGroup { get; set; }
            public string SpecSet { get; set; }
            public string OriginalManu { get; set; }
            public string OriginalStyle { get; set; }
            public string OriginalUse { get; set; }
            public string NewManu { get; set; }
            public string NewStyle { get; set; }
            public string NewUse { get; set; }
            public string Calculation { get; set; }
            public string CommunityCode { get; set; }
            public string OptionName { get; set; }

            public StyleConversion()
            {
                SpecSetGroup = string.Empty;
                SpecSet = string.Empty;
                OriginalManu = string.Empty;
                OriginalStyle = string.Empty;
                OriginalUse = string.Empty;
                NewManu = string.Empty;
                NewStyle = string.Empty;
                NewUse = string.Empty;
                Calculation = string.Empty;
                CommunityCode = string.Empty;
                OptionName = string.Empty;
            }
        }
        private readonly string PARAMETER_DEFAULT = "";
        private readonly string COMMUNITY_CODE_DEFAULT = "QA_RT_Community_PIPE_19165_Code";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community_PIPE_19165";
        private readonly string LOT_NUMBER = "QA_RT_Lot_PIPE_19165_01";
        private readonly string OPTION_NAME_DEFAULT = "QA_RT_Option_PIPE_19165";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House_PIPE_19165";
        private readonly string JOB_NUMBER_DEFAULT = "QA_RT_OB_Zero_Quantities_Automation";

        private readonly string MANUFACTURER_NAME_CONCEPT = "CONCEPT";
        private readonly string MANUFACTURER_NAME_GENERIC = "GENERIC";

        private readonly string STYLE_NAME_CONCEPT_BASE= "CONCEPT BASE";
        private readonly string STYLE_NAME_JACOB_LADDER = "JACOBS-LADDER";
        private readonly string STYLE_NAME_GENERIC = "GENERIC";
        private readonly string STYLE_NAME_PANHARD_BAR = "PANHARD-BAR";
        private readonly string STYLE_NAME_Z_LINK = "Z-LINK";
        private readonly string STYLE_THREE_BAR = "THREE-BAR";

        private readonly string BUILDING_GROUP_NAME = "QA_RT_Building_Group_PIPE_19165";
        private readonly string BUILDING_GROUP_CODE = "96385";
        private readonly string USE_NAME = "QA_RT_USE_PIPE_19165";
        private readonly string UNIT_NAME_LF = "LF";
        private readonly string UNIT_NAME_SF_2 = "SF-2";
        private readonly string UNIT_NAME_LF_2 = "LF-2";

        private readonly string SERIES_NAME = "QA_RT_Series_PIPE_19165";

        private readonly string BUILDING_PHASE_CODE_1 = "A_01";
        private readonly string BUILDING_PHASE_NAME_1 = "QA_RT_Building_Phase_PIPE_19165_01";
        private readonly string BUILDING_PHASE_CODE_2 = "A_02";
        private readonly string BUILDING_PHASE_NAME_2 = "QA_RT_Building_Phase_PIPE_19165_02";
        private readonly string BUILDING_PHASE_CODE_3 = "A_03";
        private readonly string BUILDING_PHASE_NAME_3 = "QA_RT_Building_Phase_PIPE_19165_03";
        private readonly string BUILDING_PHASE_CODE_4 = "A_04";
        private readonly string BUILDING_PHASE_NAME_4 = "QA_RT_Building_Phase_PIPE_19165_04";

        private readonly string PRODUCT_NAME_1 = "QA_RT_New_Product_PIPE_19165_01";
        private readonly string PRODUCT_NAME_2 = "QA_RT_New_Product_PIPE_19165_02";
        private readonly string PRODUCT_NAME_3 = "QA_RT_New_Product_PIPE_19165_03";
        private readonly string PRODUCT_NAME_4 = "QA_RT_New_Product_PIPE_19165_04";
        private readonly string PRODUCT_NAME_5 = "QA_RT_New_Product_PIPE_19165_05";


        private readonly string SPEC_SET_GROUP = "QA_RT_New_Spec_Set_Group_PIPE_19165";
        private readonly string SPEC_SET = "Spec_Set_PIPE_19165";
        private readonly string MANU_NEW = "QA_RT_New_Manu_PIPE_19165_New";
        private readonly string STYLE_NEW = "QA_RT_New_Style_PIPE_19165_New";

        private const string OPTION = "OPTION";

        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\PIPE_19165";

        private StyleConversion specSetDetailInfor;

        private ProductData productData_Option_1;
        private ProductData productData_Option_2;
        private ProductData productData_Option_3;
        private ProductData productData_Option_4;
        private ProductToOptionData productToOption1;
        private ProductToOptionData productToOption2;
        private ProductToOptionData productToOption3;
        private ProductToOptionData productToOption4;

        private ProductData productData_House_1;
        private ProductData productData_House_2;
        private ProductData productData_House_3;

        private ProductToOptionData productToHouse1;
        private ProductToOptionData productToHouse2;
        private ProductToOptionData productToHouse3;

        private ProductToOptionData productToHouseBOM1;
        private ProductToOptionData productToHouseBOM2;
        private ProductToOptionData productToHouseBOM3;
        private ProductToOptionData productToHouseBOM4;

        private HouseQuantitiesData houseQuantities;
        private HouseQuantitiesData houseQuantities_HouseBOM;
        private HouseQuantitiesData houseQuantities_HouseBOM_No_Zero_Quantity;

        private JobData jobData;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_V);
        }

        [SetUp]
        public void GetData()
        {
            /****************************************** Setting *******************************************/

            // Update setting with : TransferSeparationCharacter, SetSage300AndNAV, Group by Parameter
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Make sure current transfer seperation character is ','<b></b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.2: Turn OFF Sage 300 and MS NAV.<b></b></font>");
            CommunityPage.Instance.SetSage300AndNAVStatus(false);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Update setting base with 'Group by Parameter' and Job BOM Show Zero Quantities are false (turn it off).</b></font>");
            BOMSettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.SelectGroupByParameter(false, PARAMETER_DEFAULT);
            BOMSettingPage.Instance.SelectJobBOMShowZeroQuantities(false);


            /****************************************** Import data *******************************************/

            // Step 0.4: Import Communities.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Import Communities.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_IMPORT} grid view to import new Options.</font>");

            string importFile = $@"{IMPORT_FOLDER}\Pipeline_Communities.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_IMPORT, importFile);

            // Step 0.5: Import Lots to Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Import Lots to Community.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_LOT);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.LOT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.LOT_IMPORT} grid view to import new Options.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Lots.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.LOT_IMPORT, importFile);

            // Step 0.6: Import Option.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Import Option.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_OPTION);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_IMPORT} grid view to import new Options.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Options.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);

            // Step 0.7: Add Option to Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Add Option to Community.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_OPTION_IMPORT} grid view to import Options to Community.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_CommunityOptions.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_OPTION_IMPORT, importFile);

            // Step 0.8: Create a new Manufacturer to import Product.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Create a new Manufacturer to import Product.</b></font>");
            ManufacturerData manuData_New = new ManufacturerData()
            {
                Name = MANU_NEW
            };

            ManufacturerData manuData_Concept = new ManufacturerData()
            {
                Name = MANUFACTURER_NAME_CONCEPT
            };

            ManufacturerData manuData_Generic = new ManufacturerData()
            {
                Name = MANUFACTURER_NAME_GENERIC
            };

            ManufacturerData[] manuList = { manuData_New, manuData_Generic, manuData_Concept };

            // Create new Manu if it's NOT existing
            foreach (ManufacturerData data in manuList)
            {
                ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

                ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (ManufacturerPage.Instance.IsItemInGrid("Name", data.Name) is false)
                {
                    ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Create new Manu '{data.Name}' incase NOT existing.</b></font>");
                    ManufacturerPage.Instance.CreateNewManufacturer(data);
                }
            }


            // Step 0.9: Create a new Style to import Product.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9: Create a new Style to import Product.</b></font>");

            StyleData styleData_New = new StyleData()
            {
                Name = STYLE_NEW,
                Manufacturer = manuData_New.Name
            };

            StyleData styleData_ConcepBase = new StyleData()
            {
                Name = STYLE_NAME_CONCEPT_BASE,
                Manufacturer = manuData_Concept.Name
            };

            StyleData styleData_JacobLadder = new StyleData()
            {
                Name = STYLE_NAME_JACOB_LADDER,
                Manufacturer = manuData_Concept.Name
            };

            StyleData styleData_Generic = new StyleData()
            {
                Name = STYLE_NAME_GENERIC,
                Manufacturer = manuData_Generic.Name
            };

            StyleData styleData_PanhardBar = new StyleData()
            {
                Name = STYLE_NAME_PANHARD_BAR,
                Manufacturer = manuData_Concept.Name
            };
            StyleData styleData_ZLink = new StyleData()
            {
                Name = STYLE_NAME_Z_LINK,
                Manufacturer = manuData_Concept.Name
            };
            StyleData styleData_ThreeBar = new StyleData()
            {
                Name = STYLE_THREE_BAR,
                Manufacturer = manuData_Concept.Name
            };

            StyleData[] styleList = { styleData_New, styleData_ConcepBase, styleData_JacobLadder, styleData_Generic, styleData_PanhardBar, styleData_ZLink, styleData_ThreeBar };

            // Create new Manu if it's NOT existing
            foreach (StyleData data in styleList)
            {
                StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
                StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (StylePage.Instance.IsItemInGrid("Name", data.Name) is false)
                {
                    ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Create new Style '{data.Name}' incase NOT existing.</b></font>");
                    StylePage.Instance.CreateNewStyle(data);
                }
            }


            // Step 0.10: Create a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10: Create a new Building Group to import Product.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = BUILDING_GROUP_CODE,
                Name = BUILDING_GROUP_NAME
            };

            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Building Group
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            // Step 0.11: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11: Import Building Phase to import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.BUILDING_PHASE_IMPORT} grid view to import new products..</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            // Step 0.12: Import Unit
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12: Import Unit.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_ATTRIBUTES_VIEW, ImportGridTitle.UNIT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.UNIT_IMPORT} grid view to import new Unit..</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Units.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.UNIT_IMPORT, importFile);

            // Step 0.13: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Step 0.14: Import Use
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.14: Import Use.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_ATTRIBUTES);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_ATTRIBUTES_VIEW, ImportGridTitle.USE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.USE_IMPORT} grid view to import Use.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_ProductUses.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.USE_IMPORT, importFile);

            // Step 0.15: Import Option Product Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.15: Import Option Product Quantities.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.OPTION_PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_PRODUCT_IMPORT} grid view to import Option Product Quantities.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_OptionProducts.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_PRODUCT_IMPORT, importFile);

            // Step 0.16: Create Series
            SeriesData SeriesData = new SeriesData()
            {
                Name = SERIES_NAME
            };
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.16: Create a new Series '{SeriesData.Name}' if it doesn't exist.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);

            SeriesPage.Instance.FilterItemInGrid("Title", GridFilterOperator.Contains, SeriesData.Name);
            if (SeriesPage.Instance.IsItemInGrid("Title", SeriesData.Name) is false)
            {
                // Create a new series
                SeriesPage.Instance.CreateSeries(SeriesData);
            }

            // Step 0.17: Import House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.17: Import House.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_IMPORT} grid view to import new Options.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Houses.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_IMPORT, importFile);


            // Step 0.18: Import Option to House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.18: Import Option to House.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_OPTION_IMPORT} grid view to import Options to House.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Options_QA_RT_House_PIPE_19165.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_OPTION_IMPORT, importFile);

            // Step 0.19: Import Community to House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.19: Import Community to House.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_COMMUNITY_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_COMMUNITY_IMPORT} grid view to import Community to House.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_House_Communities_QA_RT_House_PIPE_19165.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_COMMUNITY_IMPORT, importFile);

            // Step 0.20: Delete Spec Set Group before importing a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.20: Delete Spec Set Group before importing a new one.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPEC_SET_GROUP);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SPEC_SET_GROUP) is true)
            {
                // Delete that spec set group
                SpecSetPage.Instance.DeleteSpecSet(SPEC_SET_GROUP);
            }


            // Step 0.21: Import Spec Set Group and Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.21: Import Spec Set Group and Spec Set.</b></font>");
            CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_SPEC_SET);
            CommonHelper.SwitchLastestTab();
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.SPEC_SET_GROUP_AND_SPEC_SET_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.SPEC_SET_GROUP_AND_SPEC_SET_IMPORT} grid view to import Spec Set Group and Spec Set.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_SpecSetGroups.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.SPEC_SET_GROUP_AND_SPEC_SET_IMPORT, importFile);


            // Step 0.22: Import Community to Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.22: Import Community to Spec Set.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_SPEC_SET);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_TO_SPEC_SET_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_TO_SPEC_SET_IMPORT} grid view to import Community to Spec Set.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_SpecSet_Community.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_TO_SPEC_SET_IMPORT, importFile);


            // Step 0.23: Import Option to Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.23: Import Option to Spec Set.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_SPEC_SET);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_TO_SPEC_SET_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_TO_SPEC_SET_IMPORT} grid view to import Option to Spec Set.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_SpecSet_Option.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_TO_SPEC_SET_IMPORT, importFile);

            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
            CommonHelper.RefreshPage();


            /****************************************** Verify data *******************************************/

            // Step 1.1: Open Spec Set detail page, add Style Conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Open Spec Set detail page, add Style Conversion.</b></font>");
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPEC_SET_GROUP);

            if (SpecSetPage.Instance.IsItemInGrid("Name", SPEC_SET_GROUP) is false)
            {
                // Stop test script in case there is no option with name "QA_OPT_06"
                Assert.Fail($"Can't find Spec Set with name {SPEC_SET_GROUP} to continue running this test script.");
            }
            SpecSetPage.Instance.SelectItemInGrid("Name", SPEC_SET_GROUP);

            // Click Expand all button and verify product conversion item in grid
            SpecSetDetailPage.Instance.ExpandAllSpecSet();
            specSetDetailInfor = new StyleConversion()
            {
                SpecSetGroup = SPEC_SET_GROUP,
                SpecSet = SPEC_SET,
                OriginalManu = MANUFACTURER_NAME_CONCEPT,
                OriginalStyle = STYLE_NAME_PANHARD_BAR,
                OriginalUse = "ALL",
                NewManu = MANUFACTURER_NAME_CONCEPT,
                NewStyle = STYLE_THREE_BAR,
                NewUse = "NONE",
                Calculation = "NONE",
                CommunityCode = COMMUNITY_CODE_DEFAULT,
                OptionName = OPTION_NAME_DEFAULT
            };

            // Add Style Conversion 
            SpecSetDetailPage.Instance.AddStyleConversion(specSetDetailInfor);

            // Step 1.2: Verify the set up data for spec set - style conversions
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Verify the set up data for spec set - style conversions.</b></font>");

            if (SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSetDetailInfor.SpecSet, "OriginalMfg_Name", specSetDetailInfor.OriginalManu) is true
                //&& SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSetDetailInfor.SpecSet, "OriginalStyle_Name", specSetDetailInfor.OriginalStyle) is true
                && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSetDetailInfor.SpecSet, "OriginalUse", specSetDetailInfor.OriginalUse) is true
                && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSetDetailInfor.SpecSet, "NewMfg_Name", specSetDetailInfor.NewManu) is true
                && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSetDetailInfor.SpecSet, "NewStyle_Name", specSetDetailInfor.NewStyle) is true
                && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSetDetailInfor.SpecSet, "NewUse", specSetDetailInfor.NewUse) is true
                && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(specSetDetailInfor.SpecSet, "Calculation", specSetDetailInfor.Calculation) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up Style Conversion on Spec Set Group <b>'{specSetDetailInfor.SpecSetGroup}'</b> is correct.</font>");

            else
                ExtentReportsHelper.LogWarning("<font color='yellow'>The set up Style Conversion on this page is NOT same as expectation. " +
                            "The next step and the  result after generating a BOM can be incorrect." +
                            $"<br>The expected Original Manufacturer/Style: {specSetDetailInfor.OriginalManu}" +
                            $"<br>The expected Original Use: {specSetDetailInfor.OriginalUse}" +
                            $"<br>The expected New Manufacturer/Style: {specSetDetailInfor.NewManu}" +
                            $"<br>The expected New Use: {specSetDetailInfor.NewUse}" +
                            $"<br>The expected Calculation: {specSetDetailInfor.Calculation}</br></font>");



            // Step 1.3: Verify the set up data for spec set - Communities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Verify the set up data for spec set - Communities.</b></font>");
            if (SpecSetDetailPage.Instance.IsItemOnCommunityGrid(specSetDetailInfor.SpecSet, specSetDetailInfor.CommunityCode) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up Spec set group detail - Communities with code <b>'{specSetDetailInfor.CommunityCode}'</b> is correct.</font>");
            else
                ExtentReportsHelper.LogWarning("<font color='yellow'>The set up Spec set group detail - Communities on this page is NOT same as expectation. " +
                            "The next step and the  result after generating a BOM can be incorrect." +
                            $"<br>The expected Spec Set: {specSetDetailInfor.SpecSet}" +
                            $"<br>The expected Community code: {specSetDetailInfor.CommunityCode}</br></font>");

            // Step 1.4: Verify the set up data for spec set - Options
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Verify the set up data for spec set - Options.</b></font>");
            if (SpecSetDetailPage.Instance.IsItemOnOptionGrid(specSetDetailInfor.SpecSet, specSetDetailInfor.OptionName) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The set up Spec set group detail - Options with name '{specSetDetailInfor.OptionName}' is correct.</b></font>");
            else
                ExtentReportsHelper.LogWarning("<font color='yellow'>The set up Spec set group detail - Options on this page is NOT same as expectation. " +
                            "The next step and the result after generating a BOM can be incorrect." +
                            $"<br>The expected Spec Set: {specSetDetailInfor.SpecSet}" +
                            $"<br>The expected Option name: {specSetDetailInfor.OptionName}</br></font>");



            // Step 1.5: Verify the set up list of products in option 'QA_RT_Option_PIPE_19165'
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.5: Verify the set up list of products in option '{OPTION_NAME_DEFAULT}'.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Clear filter on Number column form the last test script
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);

            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                // Stop test script in case there is no option with name "QA_RT_Option_PIPE_19165"
                Assert.Fail($"Can't find option with name {OPTION_NAME_DEFAULT} to continue running this test script.");
            }

            /****************************** Create Product quantities on Option ******************************/

            productData_Option_1 = new ProductData()
            {
                Name = PRODUCT_NAME_1,
                Style = STYLE_NAME_CONCEPT_BASE,
                Use = USE_NAME,
                Quantities = "12.00",
                Unit = UNIT_NAME_LF
            };

            productData_Option_2 = new ProductData()
            {
                Name = PRODUCT_NAME_2,
                Style = STYLE_NAME_JACOB_LADDER,
                Use = USE_NAME,
                Quantities = "10.00",
                Unit = UNIT_NAME_SF_2
            };

            productData_Option_3 = new ProductData()
            {
                Name = PRODUCT_NAME_3,
                Style = STYLE_NAME_GENERIC,
                Use = "NONE",
                Quantities = "14.00",
                Unit = UNIT_NAME_LF
            };

            productData_Option_4 = new ProductData()
            {
                Name = PRODUCT_NAME_4,
                Style = STYLE_NAME_PANHARD_BAR,
                Use = "NONE",
                Quantities = "1.00",
                Unit = UNIT_NAME_LF_2
            };

            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = $"{BUILDING_PHASE_CODE_1}-{BUILDING_PHASE_NAME_1}",
                ProductList = new List<ProductData> { productData_Option_1 }
            };

            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = $"{BUILDING_PHASE_CODE_2}-{BUILDING_PHASE_NAME_2}",
                ProductList = new List<ProductData> { productData_Option_2 }
            };

            productToOption3 = new ProductToOptionData()
            {
                BuildingPhase = $"{BUILDING_PHASE_CODE_3}-{BUILDING_PHASE_NAME_3}",
                ProductList = new List<ProductData> { productData_Option_3 }
            };

            productToOption4 = new ProductToOptionData()
            {
                BuildingPhase = $"{BUILDING_PHASE_CODE_4}-{BUILDING_PHASE_NAME_4}",
                ProductList = new List<ProductData> { productData_Option_4 }
            };

            IList<ProductToOptionData> productToOptQuantityList = new List<ProductToOptionData> { productToOption1, productToOption2, productToOption3, productToOption4 };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_1 = new ProductData(productData_Option_1) { Quantities = "-12.00" };

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_2 = new ProductData(productData_Option_2) { Style = STYLE_NAME_Z_LINK, Quantities = "-10.00" };

            // House quantities 3 will be different from option quantities 3
            productData_House_3 = new ProductData()
            {
                Name = PRODUCT_NAME_5,
                Style = STYLE_NAME_GENERIC,
                Use = "NONE",
                Quantities = "-14.00",
                Unit = UNIT_NAME_LF
            };

            // There is no House quantities 4

            productToHouse1 = new ProductToOptionData(productToOption1) { ProductList = new List<ProductData> { productData_House_1 } };
            productToHouse2 = new ProductToOptionData(productToOption2) { ProductList = new List<ProductData> { productData_House_2 } };
            productToHouse3 = new ProductToOptionData(productToOption3) { ProductList = new List<ProductData> { productData_House_3 } };

            houseQuantities = new HouseQuantitiesData()
            {
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                houseName = HOUSE_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse1, productToHouse2, productToHouse3 }
            };


            /****************************** The expected data when verifing House BOM and Job Bom - Show zero quantities ******************************/
            // After generating BOM, the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            // The total quantity of House BOM 1 will be 0 (-12 + 12)
            ProductData productData_HouseBOM_1 = new ProductData(productData_Option_1) { Quantities = "0.00" };
            productToHouseBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { productData_HouseBOM_1 } };

            productToHouseBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_Option_2, productData_House_2 } };
            productToHouseBOM3 = new ProductToOptionData(productToHouse3) { ProductList = new List<ProductData> { productData_Option_3, productData_House_3 } };
            productToHouseBOM4 = new ProductToOptionData(productToOption4);

            houseQuantities_HouseBOM = new HouseQuantitiesData(houseQuantities)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM2, productToHouseBOM3, productToHouseBOM4 }
            };


            /****************************** The expected data when verifing Job Bom - Don't show zero quantities ******************************/

            // Don't show product that has quantity is zero
            productToHouseBOM1 = new ProductToOptionData(productToHouse1) { ProductList = new List<ProductData> { } };
            productToHouseBOM2 = new ProductToOptionData(productToHouse2) { ProductList = new List<ProductData> { productData_Option_2, productData_House_2 } };
            productToHouseBOM3 = new ProductToOptionData(productToHouse3) { ProductList = new List<ProductData> { productData_Option_3, productData_House_3 } };
            productToHouseBOM4 = new ProductToOptionData(productToOption4);

            houseQuantities_HouseBOM_No_Zero_Quantity = new HouseQuantitiesData(houseQuantities)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM1, productToHouseBOM2, productToHouseBOM3, productToHouseBOM4 }
            };


            /****************************** Check input data condition before testing ******************************/

            // Option option detail page and open Product from left navigation
            OptionPage.Instance.SelectItemInGrid("Name", OPTION_NAME_DEFAULT);
            OptionPage.Instance.LeftMenuNavigation("Products");

            foreach (ProductToOptionData productToOptQuantity in productToOptQuantityList)
            {
                foreach (ProductData item in productToOptQuantity.ProductList)
                {
                    // Filter product and verify item on the grid
                    ProductsToOptionPage.Instance.FilterOptionQuantitiesInGrid("Product", GridFilterOperator.EqualTo, item.Name);

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", item.Name) is true
                        && ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", productToOptQuantity.BuildingPhase) is true
                        && ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Style", item.Style) is true
                        && ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Use", item.Use) is true
                        && ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Quantity", item.Quantities.ToString()) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for Option quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Building phase: {productToOptQuantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }



            // Step 1.6: Verify the set up data for option in house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Verify the set up data for option in house.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseQuantities.houseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseQuantities.houseName) is false)
            {
                // Stop test script in case there is no option with name "QA_RT_House_PIPE_19165"
                Assert.Fail($"Can't find House with name {houseQuantities.houseName} to continue running this test script.");
            }

            // Go to detail page
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseQuantities.houseName);
            HousePage.Instance.LeftMenuNavigation("Options");

            // A limitation at this step, can't filter at option grid.
            // HouseOptionDetailPage.Instance.FilterItemInOptionnGrid("Name", GridFilterOperator.EqualTo, houseQuantities.optionName);
            // HouseOptionDetailPage.Instance.ChangePageSizeInOptionnGrid(50);
            if (HouseOptionDetailPage.Instance.IsItemInOptionGridWithTextContains("Name", houseQuantities.optionName) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>Option <b>'{houseQuantities.optionName}'</b> added to House <b>'{houseQuantities.houseName}'</b> correctly.</font>");
            else
                ExtentReportsHelper.LogWarning($"<font color='yellow'>Option <b>'{houseQuantities.optionName}'</b> should be added to House <b>'{houseQuantities.houseName}'</b>." +
                    "<br>The next step and the result after generating a BOM can be incorrect.</font>");


            // Step 1.7: Import House Option quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Import House Option quantities.</b></font>");
            HouseImportQuantitiesPage.Instance.LeftMenuNavigation("Import");
            string importType = "CSV";
            string[] importFiles =
            {
                //$@"{IMPORT_FOLDER}\Pipeline_Quantities_QA_RT_House_PIPE_19165_DefaultCommunity.csv",
                $@"{IMPORT_FOLDER}\Pipeline_Quantities_QA_RT_House_PIPE_19165_SpecifiedCommunity.csv"
            };
            HouseImportQuantitiesPage.Instance.ImportHouseQuantities(importType, importFiles);
            HouseImportQuantitiesPage.Instance.GenerateReportViewAllFiles();
            HouseImportQuantitiesPage.Instance.DeleteAllHouseMaterialFiles();

            // Step 1.8: Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Verify the set up data for product quantities on House.</b></font>");
            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities.productToOption)
            {
                HouseQuantitiesDetailPage.Instance.FilterByDropDownColumn("Option", houseQuantities.optionName);

                foreach (ProductData item in housequantity.ProductList)
                {
                    // There is a bug with filter function by building phase (Missing building phase on drop down list)
                    //HouseQuantitiesDetailPage.Instance.FilterByDropDownColumn("Building Phase", housequantity.BuildingPhase);

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            // Step 1.9: Generate House BOM and verify it
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Generate House BOM and verify it.</b></font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");
            HouseBOMDetailPage.Instance.GenerateHouseBOM(houseQuantities.communityCode + '-' + houseQuantities.communityName);

            // Verify BOM
            HouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM);


            // Step 1.10: Open Job page and Create a new Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Open Job page and apply system quantities.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            jobData = new JobData()
            {
                Name = JOB_NUMBER_DEFAULT,
                Community = COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT,
                House = HOUSE_NAME_DEFAULT,
                Lot = LOT_NUMBER
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Jobs menu > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Create a new Job.</font>");
                JobPage.Instance.CreateJob(jobData);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"The Job {jobData.Name} is exited");
                JobPage.Instance.DeleteJob(jobData.Name);
                JobPage.Instance.CreateJob(jobData);
            }

            // Add Option to Job and Approve Config
            JobOptionPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed is false)
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            ExtentReportsHelper.LogPass(null, "<font color='green'><b>Job > Option page displays correctly.</b></font>");

            if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Name", OPTION_NAME_DEFAULT) is false)
            {
                string selectedOption = OPTION_NAME_DEFAULT;
                ExtentReportsHelper.LogInformation(null, $"<font color='green'>Add option <b>{selectedOption}</b> to current job.</font>");
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);
            }
            JobOptionPage.Instance.ClickApproveConfig();

            JobQuantitiesPage.Instance.LeftMenuNavigation("Quantities");
            JobQuantitiesPage.Instance.ApplySystemQuantities("Pipeline");

            // Step 1.11: Move to Job BOM page
            JobBOMPage.Instance.LeftMenuNavigation("Job BOM");
        }

        [Test]
        [Category("Section_V")]
        [Ignore("This test scripts will be ignored at this time and be fixed in the future.")]
        public void A5_A_PipelineBOM_JobBOM_JobBOMShowZeroQuantities()
        {
            // Turn OFF "Job BOM Show Zero Quantities" ,on Job BOM will NOT display '0' quantities, it's different from House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Navigate to setting page. Turn OFF 'Job BOM Show Zero Quantities'.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings, true, true);
            CommonHelper.SwitchTab(1);
            BOMSettingPage.Instance.LeftMenuNavigation("BOM");
            BOMSettingPage.Instance.SelectJobBOMShowZeroQuantities(false);

            // Generate BOM and verify it
            CommonHelper.SwitchTab(0);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.1: Generate BOM.</b></font>");
            JobBOMPage.Instance.GenerateJobBOM();

            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView("Option/Phase/Product");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.2: Verify Job BOM with product quantities on the grid view.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_HouseBOM_No_Zero_Quantity, true);


            // Turn ON "Job BOM Show Zero Quantities:", the quantites on Job BOM will be the same with House BOM's
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Navigate to setting page. Turn ON 'Job BOM Show Zero Quantities'.</b></font>");
            CommonHelper.SwitchTab(1);
            BOMSettingPage.Instance.SelectJobBOMShowZeroQuantities(true);

            // Generate BOM and verify it
            CommonHelper.SwitchTab(0);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.1: Generate BOM again.</b></font>");
            JobBOMPage.Instance.GenerateJobBOM();


            // Switch Job Bom View
            JobBOMPage.Instance.SwitchJobBomView("Option/Phase/Product");

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.2: Verify Job BOM with product quantities on the grid view.</b></font>");
            JobBOMPage.Instance.VerifyItemOnJobBOMGrid(houseQuantities_HouseBOM, true);
        }

        [TearDown]
        public void DeleteData()
        {
            // Delete Spec Set Group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.16: Delete Spec Set Group before importing a new one.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SPEC_SET_GROUP);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SPEC_SET_GROUP) is true)
            {
                // Delete that spec set group
                SpecSetPage.Instance.DeleteSpecSet(SPEC_SET_GROUP);
            }

            // Delete House

            // Delete Job
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }

            // Close all tab exclude the current one
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }

    }
}
