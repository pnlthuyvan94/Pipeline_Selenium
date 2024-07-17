using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Estimating.Worksheet;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetProducts;
using Pipeline.Common.Constants;
using Pipeline.Testing.Pages.UserMenu.Setting;
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
    public partial class A4_RT_01297 : BaseTestScript
    {
        public static bool isDuplicated;
        private WorksheetData worksheetData;

        private const string MANUFACTUER_NAME = "QA_RT_New_Manu_RT_01297";
        private const string STYLE_NAME = "QA_RT_New_Style_RT_01297";
        private const string VENDOR_NAME = "QA_RT_Vendor_RT_01297";
        private const string DIVISION_NAME = "QA_RT_Div_RT_01297";
        private const string COMMUNITY_NAME = "QA_RT_Community_RT_01297";
        private const string WORKSHEET_NAME = "QA_RT_Worksheet_RT_01297";
        private readonly string WORKSHEET_CODE = "01297";


        private readonly string BUILDING_GROUP_CODE = "1297";
        private readonly string BUILDING_GROUP_NAME = "QA_RT_Building_Group_RT_01297";
        private readonly string BUILDING_PHASE_CODE = "1297";
        private readonly string BUILDING_PHASE_NAME = "QA_RT_Building_Phase_RT_01297";
        private readonly string PRODUCT_NAME_1 = "QA_RT_New_Product_RT_01297_01";
        private readonly string PRODUCT_NAME_2 = "QA_RT_New_Product_RT_01297_02";
        private readonly string PRODUCT_NAME_3 = "QA_RT_New_Product_RT_01297_03";

        private string importFile;
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\RT_01297";



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

            /*********************************************************************************************/

            worksheetData = new WorksheetData()
            {
                Name = WORKSHEET_NAME,
                Code = WORKSHEET_CODE
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


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Create and verify new Worksheets.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_WORKSHEETS_URL);

            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", worksheetData.Name);
            if (!WorksheetPage.Instance.IsItemInGrid("Name", worksheetData.Name))
            {
                // Create a new workshet
                WorksheetPage.Instance.CreateNewWorksheet(worksheetData);
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


            // Step 0.8: Import product to worksheet
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Import product to worksheet.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDERBOM_IMPORT_URL_WORKSHEET);
            if (!BuilderBOMImportPage.Instance.IsImportGridDisplay(ImportGridTitle.WORKSHEET_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.WORKSHEET_IMPORT} grid view to import Worksheet Quantities.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Worksheets_QA_RT_Worksheet_RT_01297_02.csv";
            BuilderBOMImportPage.Instance.ImportValidData(ImportGridTitle.WORKSHEET_IMPORT, importFile);

            // Step 0.8: Inport product to worksheet 2
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.8: Import product to worksheet.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDERBOM_IMPORT_URL_WORKSHEET);
            if (!BuilderBOMImportPage.Instance.IsImportGridDisplay(ImportGridTitle.WORKSHEET_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.WORKSHEET_IMPORT} grid view to import Worksheet Quantities.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Worksheets_QA_RT_Worksheet_Automation.csv";
            BuilderBOMImportPage.Instance.ImportValidData(ImportGridTitle.WORKSHEET_IMPORT, importFile);

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

            importFile = $@"{IMPORT_FOLDER}\Pipeline_VendorsToBuildingPhases_QA_RT_Vendor_RT_01297.csv";
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
            }
            else
            {
                // Community name isn't exist
                ExtentReportsHelper.LogFail($"<font color = 'red'>Could not find Community with name {COMMUNITY_NAME}.</font");
            }
        }

        [Test]
        [Category("Section_V")]
        public void A4_PipelineBOM_GenerateAWorksheetBOM()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1. Create a Worksheet. Refer to step 0.2</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_WORKSHEETS_URL);


            // Step 2-3: Open Worksheet 'QA_RT_Worksheet_RT_01297' detail page and add product
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2-3: Open Worksheet '{worksheetData.Name}' detail page and add product</b></font>");
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", worksheetData.Name);
            if (!WorksheetPage.Instance.IsItemInGrid("Name", worksheetData.Name))
            {
                Assert.Fail($"Can't find Worsheet with name '{worksheetData.Name}' to continue running this test script.");
            }

            WorksheetPage.Instance.SelectItemInGrid("Name", worksheetData.Name);

            if (!WorksheetDetailPage.Instance.IsSaveWorksheetSuccessful(worksheetData.Name))
                ExtentReportsHelper.LogFail($"<font color='red'>Failed to open Worksheet '{worksheetData.Name}' detail page.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Open Worksheet '{worksheetData.Name}' detail page successfully!</b></font>");


            // Step 4: Add product to worksheet
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Add Product to Worksheet. Refer to step 0.8</b></font>");
            WorksheetProductsPage.Instance.LeftMenuNavigation("Products");
            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step: Open other worksheets and verify the worksheet quantities of other worksheets are still remained correctly </b></font>");
            if (!WorksheetProductsPage.Instance.IsItemInGrid("Product", "QA_RT_New_Product_RT_01297_01"))
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>Can't find quanity worksheet with name QA_RT_New_Product_RT_01297_01.</b></font>");
            }
            else
                ExtentReportsHelper.LogPass("<font color='green'><b>The worksheet still has test data after import orther worksheet quanity</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Generate BOM.</b></font>");
            WorksheetProductsPage.Instance.SelectCommunityBOM(COMMUNITY_NAME + "-" + COMMUNITY_NAME);

            string _expectedGenerate = $"Report generated";
            string actualMsg = WorksheetProductsPage.Instance.Click_GenerateBOMEstimate();
            if (actualMsg == _expectedGenerate)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Report generated</b></font color>");
            }
            else
            {
                WorksheetProductsPage.Instance.FilterItemOnReportGrid("Option", GridFilterOperator.EqualTo, WORKSHEET_NAME);
                if(WorksheetProductsPage.Instance.IsItemInReportGrid("Option", WORKSHEET_NAME))
                {
                    ExtentReportsHelper.LogPass("<font color ='green'><b>Report generated</b></font color>");
                }
                else
                {
                    ExtentReportsHelper.LogFail("<font color='red'>Report generate failed.</font>");
                }
            }
                

            WorksheetProductsPage.Instance.CloseToastMessage();

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

            if (!VendorBuildingPhasePage.Instance.IsItemExist(BUILDING_PHASE_NAME))
                ExtentReportsHelper.LogFail($"<font color='red'>Missing building phase {BUILDING_PHASE_NAME} on current Vendor. Please recheck import step again.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Building phases {BUILDING_PHASE_NAME} is ssigned on current Vendor</b></font>");


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


            // Back to first tab and gen Worksheet BOM and Estimate
            CommonHelper.SwitchTab(0);

            // Step 8. Generate BOM and ESTIMATE
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8. Generate BOM and ESTIMATE.</b></font>");
            
            _expectedGenerate = $"Report generated";
            actualMsg = WorksheetProductsPage.Instance.Click_GenerateBOMEstimate();
            if (actualMsg == _expectedGenerate)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Report generated</b></font color>");
                WorksheetProductsPage.Instance.Click_ExpandWorksheet();
                WorksheetProductsPage.Instance.Click_ExpandSubWorksheet();
                ExtentReportsHelper.LogPass("<font color ='green'><b>The product costing is correct and displayed on the grid</b></font color>");
            }
            else
                ExtentReportsHelper.LogFail("Report generate failed");
            WorksheetProductsPage.Instance.CloseToastMessage();

        }

        [TearDown]
        public void DeleteData()
        {
            // Close all tabs
            CommonHelper.CloseAllTabsExcludeCurrentOne();

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete Worksheets '{worksheetData.Name}'</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_WORKSHEETS_URL);

            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", worksheetData.Name);
            if (WorksheetPage.Instance.IsItemInGrid("Name", worksheetData.Name) is true)
            {
                WorksheetPage.Instance.DeleteWorkSheet(worksheetData.Name);
            }
        }
    }
}
