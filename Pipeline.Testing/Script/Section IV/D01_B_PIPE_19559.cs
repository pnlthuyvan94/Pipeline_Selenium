

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
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_IV
{
    class D01_B_PIPE_19559 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private VendorData vendor;
        private VendorData vendor2;
        string dataCreatedURL;

        private const string BUILDINGPHASE1_NAME_DEFAULT = "QA_RT_New_Building_Phase_01_Automation";
        private const string BUILDINGPHASE1_CODE_DEFAULT = "Au01";

        private const string BUILDINGPHASE2_NAME_DEFAULT = "QA_RT_New_Building_Phase_02_Automation";
        private const string BUILDINGPHASE2_CODE_DEFAULT = "Au02";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        private const string EXPORT_BUILDINGPHASE_TO_CSV = "Export CSV";
        private const string EXPORT_BUILDINGPHASE_TO_EXCEL = "Export Excel";
        private const string IMPORT_BUILDINGPHASE_TO_VENDOR_BUILDINGPHASE = "Vendors To Building Phases Import";
        private const string IMPORT = "Import";

        [SetUp]
        public void SetUpData()
        {
            vendor = new VendorData()
            {
                Name = "QA_RT_Vendor01_Automation",
                Code = "0001",
                Email = "qa_automation3@email.com",
                Address1 = "200 SR 38 E",
                City = "Lafayette",
                State = "IN",
                Zip = "456789"
            };

            vendor2 = new VendorData()
            {
                Name = "QA_RT_Vendor2_Automation",
                Code = "102",
                Email = "qa_automation3@email.com",
                Address1 = "200 SR 38 E",
                City = "Lafayette",
                State = "IN",
                Zip = "456789"
            };


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

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            // Create new vendor           
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, vendor2.Name);
            if (VendorPage.Instance.IsItemInGrid("Name", vendor2.Name) is false)
            {

                CreateTheVendor(vendor2);
            }

        }

        [Test]
        [Category("Section_IV")]
        public void D01_B_Costing_DetailPages_Vendors_BuildingPhases()
        {
            //1. On the Vendors  data page, click the Vendor to which you would like to select
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1. On the Vendors  data page, click the Vendor to which you would like to select</b></font>");
            // Create new vendor           
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, vendor.Name);
            if (VendorPage.Instance.IsItemInGrid("Name", vendor.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "Vendor is exited on page");
                VendorPage.Instance.SelectVendor("Name",vendor.Name);
            }
            else
            {
                // Create new vendor
                ExtentReportsHelper.LogInformation(null, "************************************Create Vendor and Sync to BuildPro************************************");

                CreateTheVendor(vendor);
            }


            //2. On the Vendor Side Navigation menu, click the 'Building Phases' to open the Building Phases data page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2. On the Vendor Side Navigation menu, click the 'Building Phases' to open the Building Phases data page</b></font>");
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases");

            //3. Click the '+' button to add a Building Phase to Vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3. Click the '+' button to add a Building Phase to Vendor</b></font>");
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

            //4. Filter/delete Building Phase out of Vendor
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4. Filter/delete Building Phase out of Vendor</b></font>");
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE1_NAME_DEFAULT);

            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Filtered successfully<b>");
                VendorBuildingPhasePage.Instance.DeleteItemInGrid("Building Phase", BUILDINGPHASE1_CODE_DEFAULT + "-" + BUILDINGPHASE1_NAME_DEFAULT);
                string actualdeleteMsg = VendorBuildingPhasePage.Instance.GetLastestToastMessage();
                string expecteddeleteMsg = "Vendor to Building Phase was successfully deleted.";
                if (actualdeleteMsg.Equals(expecteddeleteMsg))
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Vendor to Building Phase was successfully deleted.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail("<font color='red'>Vendor to Building Phase was unsuccessfully deleted.</font>");
                }
            }
            //5. Click the 'Utilities' button; verify the Export/Import functions
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Click the 'Utilities' button; verify the Export/Import functions</b></font>");
            // Add More Building Phase To Export File
            ExtentReportsHelper.LogInformation(null, "Add More Building Phase To Export File");
            VendorBuildingPhasePage.Instance.AddBuildingPhase(BUILDINGPHASE1_CODE_DEFAULT);
            VendorBuildingPhasePage.Instance.Close();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: verify the Export/Import functions</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1: Verify 'EXPORT CSV' function.</b></font>");
            VendorBuildingPhasePage.Instance.ImportExporFromMoreMenu(EXPORT_BUILDINGPHASE_TO_CSV, vendor.Name);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2: Verify 'EXPORT Excel' function.</b></font>");
            VendorBuildingPhasePage.Instance.ImportExporFromMoreMenu(EXPORT_BUILDINGPHASE_TO_EXCEL, vendor.Name);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3: Verify IMPORT Building Phase function.</b></font>");
            string importFileDir;
            // Open Import page
            importFileDir = $"\\DataInputFiles\\Import\\PIPE_19559\\Pipeline_VendorsToBuildingPhases.csv";

            // Click import from Utilities button
            ExtentReportsHelper.LogInformation(null, "Click import from Utilities button");
            VendorBuildingPhasePage.Instance.ImportExporFromMoreMenu(IMPORT, BUILDINGPHASE1_NAME_DEFAULT);
            VendorBuildingPhasePage.Instance.ImportFile(IMPORT_BUILDINGPHASE_TO_VENDOR_BUILDINGPHASE, importFileDir);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify Building Phase is displayed in grid after import file</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, vendor2.Name);

            if (VendorPage.Instance.IsItemInGrid("Name", vendor2.Name))
            {
                VendorPage.Instance.SelectVendor("Name", vendor2.Name);
            }
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases");
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, BUILDINGPHASE2_NAME_DEFAULT);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", BUILDINGPHASE2_CODE_DEFAULT + "-" + BUILDINGPHASE2_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Import file is successfully with BuildingPhase Name {BUILDINGPHASE2_NAME_DEFAULT}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Import file is unsuccessfully.</font>");
            }
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
