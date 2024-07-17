

using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_IV
{
    class D01_C_PIPE_19560 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private VendorData vendor;

        private const string EXPORT_BUILDINGPHASE_TO_CSV = "Export CSV";
        private const string EXPORT_BUILDINGPHASE_TO_EXCEL = "Export Excel";
        private const string IMPORT_BUILDINGPHASE_TO_VENDOR_PRODUCTS = "Vendors To Products Import";
        private const string IMPORT = "Import";

        private const string BUILDINGPHASE1_NAME_DEFAULT = "QA_RT_New_Building_Phase_01_Automation";
        private const string BUILDINGPHASE1_CODE_DEFAULT = "Au01";

        private const string PRODUCT_NAME_DEFAULT = "QA_RT_New_Product_Automation_01";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        [SetUp]
        public void SetUpData()
        {
            vendor = new VendorData()
            {
                Name = "QA_RT_Vendor4_Automation",
                Code = "104",
                Email = "qa_automation4@email.com",
                Address1 = "200 SR 38 E",
                City = "Lafayette",
                State = "IN",
                Zip = "456789"
            };

            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_RT_New_Manu_Auto"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            // Prepare a new Style to import Product.
            // Can't import new Style then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "QA_RT_New_Style_Auto",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "Prepare a new Building Group to import Product.");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "12111111",
                Name = "QA_RT_New_Building_Group_Auto_01"
            };

            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "Import Building Phase to import Product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_19559\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_19559\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }

        [Test]
        [Category("Section_IV")]
        public void D01_C_Costing_DetailPages_Vendors_Products()
        {
            //1. On the Vendors  data page, click the Vendor to which you would like to select
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: On the Vendors  data page, click the Vendor to which you would like to select</b></font>");
            // Create new vendor           
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, vendor.Name);
            if (VendorPage.Instance.IsItemInGrid("Name", vendor.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "Vendor is exited on page");
                VendorPage.Instance.SelectVendor("Name", vendor.Name);
            }
            else
            {
                // Create new vendor
                ExtentReportsHelper.LogInformation(null, "************************************Create Vendor and Sync to BuildPro************************************");

                CreateTheVendor(vendor);
            }
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Add a Building Phase to Vendor</b></font>");
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE1_NAME_DEFAULT);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT) is false)
            {
                VendorBuildingPhasePage.Instance.AddBuildingPhase(BUILDINGPHASE1_CODE_DEFAULT);
                VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();

                if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT) is true)
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Add Building Phase was successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail("<font color='red'>Add Building Phase was unsuccessfully.</font>");
                }
            }


            //2. On the Vendor Side Navigation menu, click the 'Products' to open the Product data page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2. On the Vendor Side Navigation menu, click the 'Products' to open the Product data page.</b></font>");
            VendorDetailPage.Instance.LeftMenuNavigation("Products");
            VendorProductPage.Instance.VerifyVendorProductIsDisplayed("Vendor Products with Base Costs and Building Phase Overrides");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Verify all Building Phase of Vendor  is displayed</b></font>");

            //3. Click the Collapse all button; verify all Building Phase of Vendor is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3. Click the Collapse all button; verify all Building Phase of Vendor is displayed.</b></font>");
            VendorProductPage.Instance.VerifyBuildingPhaseAddedInProductGrid(BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT);
            
            // Navigate to BuildingPhase
            ExtentReportsHelper.LogInformation(null, "Navigate to the BuildingPhase data page");
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases");
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE1_NAME_DEFAULT);

            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Filtered successfully<b>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='green'><b>The {BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT} is not displayed in grid</b>");
            }
            //4. Click the 'Edit' button to update the Base Cost
            //Navigate To Vendor Product
            VendorDetailPage.Instance.LeftMenuNavigation("Products");

            //Update cost for product and verify Product in grid
            ExtentReportsHelper.LogInformation("Update cost for product");
            VendorProductPage.Instance.UpdateCostingforVerdor(BUILDINGPHASE1_CODE_DEFAULT, PRODUCT_NAME_DEFAULT, "200.00", "5.00");
            VendorProductPage.Instance.VerifyBaseMateriaAndLaborCostIsDisplayedCorrectly(PRODUCT_NAME_DEFAULT, "$" + "200.00", "$" + "5.00");


            //5. Click the 'Cost Comparison' button; verify the Product Cost Comparison is opened
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5. Click the 'Cost Comparison' button; verify the Product Cost Comparison is opened.</b></font>");
            //Open Product CostComparsion Page and verify product in grid 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Open Product CostComparsion Page and verify product in grid .</b></font>");
            ExtentReportsHelper.LogPass("<font color ='green'><b>All product costing to be updated and displayed on the grid</b></font color>");
            VendorProductPage.Instance.OpenAndVerifyCostComparison(vendor.Name, BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT, "QA_RT_New_Product_Automation_01");

            //6. Click the 'Utilities' button; verify the Export/Import functions
            ExtentReportsHelper.LogInformation(null, "Step 6. Click the 'Utilities' button; verify the Export/Import functions");
            VendorProductPage.Instance.BackToPreviousPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify 'EXPORT CSV' function.</b></font>");
            VendorProductPage.Instance.ImportExporFromMoreMenu(EXPORT_BUILDINGPHASE_TO_CSV, vendor.Name);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify 'EXPORT Excel' function.</b></font>");
            VendorProductPage.Instance.ImportExporFromMoreMenu(EXPORT_BUILDINGPHASE_TO_EXCEL, vendor.Name);
            VendorProductPage.Instance.LeftMenuNavigation("Products");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3: Verify IMPORT Building Phase function.</b></font>");
            string importFileDir;
            // Open Import page
            importFileDir = $"\\DataInputFiles\\Import\\PIPE_19560\\Pipeline_VendorsToProducts.csv";

            // Click import from Utilities button
            VendorProductPage.Instance.ImportExporFromMoreMenu(IMPORT, vendor.Name);
            VendorProductPage.Instance.ImportFile(IMPORT_BUILDINGPHASE_TO_VENDOR_PRODUCTS, importFileDir);
        }

        // Create the vendor
        private void CreateTheVendor(VendorData data)
        {
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);

            //click on "+" Add button
            VendorPage.Instance.ClickAddToVendorIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_VENDOR_URL;
            Assert.That(VendorDetailPage.Instance.IsPageDisplayed(expectedURL), "Vendor detail page isn't displayed");

            //Select the 'Save' button on the modal;
            VendorDetailPage.Instance.CreateOrUpdateAVendor(data);

            //Verify new Vendor in header
            ExtentReportsHelper.LogPass("Create successful Vendor");
        }
    }
}
