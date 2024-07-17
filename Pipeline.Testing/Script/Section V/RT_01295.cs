using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Pages.Assets.CustomOptions;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionProduct;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Common.Constants;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionVendors;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.CommunityVendor;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Settings.MainSetting;

namespace Pipeline.Testing.Script.Section_V
{
    public partial class A2_RT_01295 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_V);
        }

        private CustomOptionData _customOption;
        private const string MANUFACTUER_NAME = "QA_RT_New_Manu_RT_01295";
        private const string STYLE_NAME = "QA_RT_New_Style_RT_01295";
        private const string VENDOR_NAME = "QA_RT_Vendor_RT_01295";
        private const string DIVISION_NAME = "QA_RT_Div_RT_01295";
        private const string COMMUNITY_NAME = "QA_RT_Community_RT_01295";
        private const string CUSTOM_OPTION_CODE = "RT_QA_CustomOption_RT_01295";

        private const string BUILDING_GROUP_CODE = "1295";
        private const string BUILDING_GROUP_NAME = "QA_RT_Building_Group_RT_01295";
        private const string BUILDING_PHASE_CODE = "1295";
        private const string BUILDING_PHASE_NAME = "QA_RT_Building_Phase_RT_01295";
        private const string PRODUCT_NAME_1 = "QA_RT_New_Product_RT_01295_01";
        private const string PRODUCT_NAME_2 = "QA_RT_New_Product_RT_01295_02";
        private const string PRODUCT_NAME_3 = "QA_RT_New_Product_RT_01295_03";
        private const string CODE_COLUMN = "Code";

        private string importFile;
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\RT_01295";

        [SetUp]
        public void GetData()
        {
            /****************************************** Setting *******************************************/

            // Update setting with : TransferSeparationCharacter: Import with ','
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Make sure current transfer seperation character is ','<b></b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            /*********************************************************************************************/

            _customOption = new CustomOptionData()
            {
                Code = CUSTOM_OPTION_CODE,
                Description = "Test for CustomOption BOM",
                Structural = bool.Parse("true"),
                Price = double.Parse("10.00")
            };

            DivisionData _division = new DivisionData()
            {
                Name = DIVISION_NAME
            };

            ManufacturerData manuData_New = new ManufacturerData()
            {
                Name = MANUFACTUER_NAME
            };

            StyleData styleData_New = new StyleData()
            {
                Name = STYLE_NAME,
                Manufacturer = manuData_New.Name
            };

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = BUILDING_GROUP_CODE,
                Name = BUILDING_GROUP_NAME
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Create and verify new Custom Option.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL);

            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, _customOption.Code);
            if (!CustomOptionPage.Instance.IsItemInGrid("Code", _customOption.Code))
            {
                // Create a new custom option
                CustomOptionPage.Instance.CreateCustomOption(_customOption);
            }

            // Step 0.3: Create a new Manufacturer to import Product.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Create a new Manufacturer to import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData_New.Name);
            if (!ManufacturerPage.Instance.IsItemInGrid("Name", manuData_New.Name))
            {
                // Create new Manu if it's NOT existing
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Create new Manu '{manuData_New.Name}' incase NOT existing.</b></font>");
                ManufacturerPage.Instance.CreateNewManufacturer(manuData_New);
            }

            // Step 0.4: Create a new Style to import Product.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Create a new Style to import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);

            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData_New.Name);
            if (!StylePage.Instance.IsItemInGrid("Name", styleData_New.Name))
            {
                // Create new Style if it's NOT existing
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Create new Style '{styleData_New.Name}' incase NOT existing.</b></font>");
                StylePage.Instance.CreateNewStyle(styleData_New);
            }

            // Step 0.5: Create a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Create a new Building Group to import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);

            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (!BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code))
            {
                // Create a new Building Group if it's NOT existing
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            // Step 0.6: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6: Import Building Phase to import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (!ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.BUILDING_PHASE_IMPORT} grid view to import new products.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            // Step 0.7: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Import Product.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (!ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Step 0.8: Import Custom Option Products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Import Custom Option Products.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDERBOM_IMPORT_URL_CUSTOMOPTION_PRODUCT);
            if (!BuilderBOMImportPage.Instance.IsImportGridDisplay(ImportGridTitle.CUSTOM_OPTION_PRODUCT_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.CUSTOM_OPTION_PRODUCT_IMPORT} grid view to import Custom Option Quantities.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_CustomOptionProducts_RT_QA_CustomOption.csv";
            BuilderBOMImportPage.Instance.ImportValidData(ImportGridTitle.CUSTOM_OPTION_PRODUCT_IMPORT, importFile);

            // Step 0.9: Import Vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.9: Import Vendor.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.COSTING_IMPORT_URL_VIEW_IMPORTVENDORS);
            if (!CostingImportPage.Instance.IsImportLabelDisplay(ImportGridTitle.VENDORS_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.VENDORS_IMPORT} grid view to import Vendor.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Vendors.csv";
            CostingImportPage.Instance.ImportFile(ImportGridTitle.VENDORS_IMPORT, importFile);

            // Step 0.10: Import Building Phases to Vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.10: Import Building Phases to Vendor.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.COSTING_IMPORT_URL);
            if (!CostingImportPage.Instance.IsImportLabelDisplay(ImportGridTitle.VENDORS_TO_PRODUCT_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.VENDORS_TO_PRODUCT_IMPORT} grid view to import Building To Vendor.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_VendorsToBuildingPhases_QA_RT_Vendor_RT_01295.csv";
            CostingImportPage.Instance.ImportFile(ImportGridTitle.VENDORS_TO_PRODUCT_IMPORT, importFile);

            // Step 0.11: Create Division
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>tep 0.11: Create Division.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL);

            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, DIVISION_NAME);
            if (!DivisionPage.Instance.IsItemInGrid("Division", _division.Name))
            {
                // Create a new one if it doesn't exist
                DivisionPage.Instance.CreateDivision(_division);
            }

            // Step 0.12: Import Communities.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12: Import Communities.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_IMPORT} grid view to import new Options.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Communities.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_IMPORT, importFile);

            // Step 0.11 Add community to division
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.11 Add community to division.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, DIVISION_NAME);
            if (DivisionPage.Instance.IsItemInGrid("Division", DIVISION_NAME))
            {
                // Open Division detail page
                DivisionPage.Instance.SelectItemInGrid("Division", DIVISION_NAME);

                // Add Community to Division
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12.1: Add Community to Division.</b></font>");
                DivisionDetailPage.Instance.LeftMenuNavigation("Communities", true);
                string[] communityNames = { COMMUNITY_NAME };
                DivisionCommunityPage.Instance.AssignCommunityToDivision(communityNames);


                // Add Vendor to Division
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12.2: Add Vendor to Division.</b></font>");
                DivisionDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                string[] vendorList = { VENDOR_NAME };
                DivisionVendorPage.Instance.AssignVendorToDivision(vendorList);

                // Add vendor to community > costing> vendor assignments
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.12.3: AAdd vendor to community > costing> vendor assignments.</b></font>");
                DivisionVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDING_PHASE_NAME);
                if (DivisionVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDING_PHASE_NAME))
                {
                    DivisionVendorPage.Instance.EditVendorAssignments(BUILDING_PHASE_NAME, VENDOR_NAME);
                }
                else
                    ExtentReportsHelper.LogWarning($"<font color = 'red'>Could not find Building Phase with name {BUILDING_PHASE_NAME} to assign.</font");
            }
            else
            {
                // Can't find Division
                ExtentReportsHelper.LogFail($"<font color = 'red'>Could not find Community with name {DIVISION_NAME}.</font");
            }

            // Step 0.13: Add vendor to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13: Add vendor to community.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, COMMUNITY_NAME);
            if (CommunityPage.Instance.IsItemInGrid("Name", COMMUNITY_NAME))
            {
                // Open Community detail page
                CommunityPage.Instance.SelectItemInGrid("Name", COMMUNITY_NAME);

                // Navigate to Vendor tab
                CommunityDetailPage.Instance.LeftMenuNavigation("Vendors", true);
                string[] vendorList = { VENDOR_NAME };
                CommunityVendorPage.Instance.AssignVendorToCommunity(vendorList);

                // Add vendor to community > costing> vendor assignments
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.13.1: Add vendor to community > costing> vendor assignments.</b></font>");
                CommunityVendorPage.Instance.FilterItemInVendorAssignmentsGrid("Building Phase", GridFilterOperator.Contains, BUILDING_PHASE_NAME);
                if (CommunityVendorPage.Instance.IsItemInVendorAssignmentsGrid("Building Phase", BUILDING_PHASE_CODE + "-" + BUILDING_PHASE_NAME))
                {
                    CommunityVendorPage.Instance.EditVendorAssignments(BUILDING_PHASE_NAME, VENDOR_NAME);
                }
                else
                    ExtentReportsHelper.LogWarning($"<font color = 'red'>Could not find Building Phase with name {BUILDING_PHASE_NAME} to assign.</font");
            }
            else
            {
                // Community name isn't exist
                ExtentReportsHelper.LogFail($"<font color = 'red'>Could not find Community with name {COMMUNITY_NAME}.</font");
            }
        }

        [Test]
        [Category("Section_V")]
        public void A2_PipelineBOM_GenerateACustomOptionBOM()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1. Create a Custom Option. Refer to step 0.2</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL);

            //2. Filter the new item which we have just created. Verify it is displayed on the grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open Custom Option detail page.</b></font>");

            CustomOptionPage.Instance.FilterItemInGrid(CODE_COLUMN, GridFilterOperator.EqualTo, _customOption.Code);
            if (!CustomOptionPage.Instance.IsItemInGrid(CODE_COLUMN, _customOption.Code))
            {
                // Can't find custom opt to continue BOM testing
                Assert.Fail($"Can't find Custom Option with code '{_customOption.Code}' to continue running this test script.");
            }

            CustomOptionPage.Instance.SelectItemInGrid(CODE_COLUMN, _customOption.Code);
            if (!CustomOptionDetailPage.Instance.IsSaveCustomOptionSuccessful(_customOption.Code))
                ExtentReportsHelper.LogFail($"<font color='red'>Failed to open Custom Option '{_customOption.Code}' detail page.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Open Custom Option '{_customOption.Code}' detail page successfully!</b></font>");

            // Step 3: Open Custom Option Products page from left navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Open Custom Option Products page from left navigation.</b></font>");
            CustomOptionDetailPage.Instance.LeftMenuNavigation("Products");

            // Step 4: Add product to Custom Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Add Product to Custom Option. Refer to step 0.8</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Generate BOM.</b></font>");
            CustomOptionProduct.Instance.SelectCommunityBOM(COMMUNITY_NAME);
            CustomOptionProduct.Instance.Click_GenerateBOMEstimate(true);

            string _expectedGenerate = $"Report generated";
            string actualMsg = CustomOptionPage.Instance.GetLastestToastMessage();
            if (actualMsg == _expectedGenerate)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Report generated</b></font color>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'>Report generate failed.</font>");
            CustomOptionProduct.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1 Assign Building phase to Vendor. Refer to step 0.10</b></font>");
            CommonHelper.OpenLinkInNewTab(BaseDashboardUrl + BaseMenuUrls.VIEW_VENDORS_URL);
            CommonHelper.SwitchLastestTab();

            VendorPage.Instance.EnterVendorNameToFilter("Name", VENDOR_NAME);
            if (!VendorPage.Instance.IsItemInGrid("Name", VENDOR_NAME))
            {
                Assert.Fail($"Can't find Vendor with name '{VENDOR_NAME}' to continue running this test script. Recheck step. ");
            }
            VendorPage.Instance.SelectVendor("Name", VENDOR_NAME);
            VendorBuildingPhasePage.Instance.LeftMenuNavigation("Building Phases");

            if (!VendorBuildingPhasePage.Instance.IsItemExist(BUILDING_PHASE_CODE+"-"+BUILDING_PHASE_NAME))
                ExtentReportsHelper.LogFail($"<font color='red'>Missing building phase {BUILDING_PHASE_NAME} on current Vendor. Please recheck import step again.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Building phases {BUILDING_PHASE_NAME} is assigned on current Vendor</b></font>");


            // Step 6.2: Assign Vendor to Division
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2: Assign Vendor to Division. Please refer to step 0.12.2</b></font>");


            // Step 6.3: Assign Vendor to Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.3: Assign Vendor to Community. Please refer to step 0.13.1.</b></font>");


            // Step 7: Update Cost for the product with vendor assigned community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7. Update Cost for the product with vendor assigned community.</b></font>");
            VendorBuildingPhasePage.Instance.LeftMenuNavigation("Products");

            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Update cost for product</b></font>");
            VendorProductPage.Instance.UpdateCostingforVerdor(BUILDING_PHASE_NAME, PRODUCT_NAME_1, "10.00", "5.00");
            VendorProductPage.Instance.UpdateCostingforVerdor(BUILDING_PHASE_NAME, PRODUCT_NAME_2, "10.00", "10.00");
            VendorProductPage.Instance.UpdateCostingforVerdor(BUILDING_PHASE_NAME, PRODUCT_NAME_3, "5.00", "5.00");

            ExtentReportsHelper.LogPass("<font color ='green'><b>All product costing to be updated and displayed on the grid</b></font color>");


            // Back to first tab and gen Custom Option BOM and Estimate
            CommonHelper.SwitchTab(0);

            // Step 8. Generate BOM and ESTIMATE
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8. Generate BOM and ESTIMATE.</b></font>");
            CustomOptionProduct.Instance.Click_GenerateBOMEstimate(true);
            _expectedGenerate = $"Report generated";
            actualMsg = CustomOptionProduct.Instance.GetLastestToastMessage();
            if (actualMsg == _expectedGenerate)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Report generated</b></font color>");
                CustomOptionProduct.Instance.ClickExplainItemOnGridBOM();
                ExtentReportsHelper.LogPass("<font color ='green'><b>The product costing is correct and displayed on the grid</b></font color>");
            }
            else
                ExtentReportsHelper.LogFail("Report generate failed");
            CustomOptionProduct.Instance.CloseToastMessage();
        }

        [TearDown]
        public void DeleteData()
        {
            // Close all tabs
            CommonHelper.CloseAllTabsExcludeCurrentOne();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete Custom Option '{_customOption.Code}'</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL);

            CustomOptionPage.Instance.FilterItemInGrid(CODE_COLUMN, GridFilterOperator.EqualTo, _customOption.Code);
            if (CustomOptionPage.Instance.IsItemInGrid(CODE_COLUMN, _customOption.Code))
            {
                // Delete a new custom option
                CustomOptionPage.Instance.DeleteCustomOption(_customOption);
            }
        }
    }
}
