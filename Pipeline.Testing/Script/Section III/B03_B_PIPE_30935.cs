using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_III
{
    class B03_B_PIPE_30935 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private BOMBuildingPhaseRuleData _data;
        private BOMBuildingPhaseRuleData _data1;
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";
        private const string EXPORT_CSV_MORE_MENU = "Export CSV";
        [SetUp]
        public void GetTestData()
        {
            _data = new BOMBuildingPhaseRuleData()
            {
                OriginalProductBuildingPhase = "125-QA_RT_BuildingPhase3_Automation",
                OriginalSubcomponentBuildingPhase = "126-QA_RT_BuildingPhase4_Automation",
                NewSubcomponentBuildingPhase = "127-QA_RT_BuildingPhase5_Automation",
            };

            _data1 = new BOMBuildingPhaseRuleData()
            {
                OriginalProductBuildingPhase = "124-QA_RT_BuildingPhase2_Automation",
                OriginalSubcomponentBuildingPhase = "126-QA_RT_BuildingPhase4_Automation",
                NewSubcomponentBuildingPhase = "127-QA_RT_BuildingPhase5_Automation",
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "123",
                Name = "QA_RT_BuildingGroup1_Automation"
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
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_30935\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

        }
            [Test]
        [Category("Section_III")]
        public void B03_B_Estimating_BuildingPhases_Export_Import_BuildingPhases()
        {
            //1.Access Pipeline using your credentials
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1. Access Pipeline using your credentials.</font>");
            //2. Navigate to Estimating> Building Phases page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2. Navigate to Estimating> Building Phases page.</font>");
            BOMBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BomPhaseRules);
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase) is false)
            {
                BOMBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();
                BOMBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.AddBuildingPhaseRule(_data);

                string _actualMessage = BOMBuildingPhaseRulePage.Instance.GetLastestToastMessage();
                string _expectedMessage = "New BOM Building Phase Rule successfully inserted";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not create Building Phase Rule with Original Product Building Phase { _data.OriginalProductBuildingPhase} .");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Create BOM Building Phase Rule with Original Product Building Phase { _data.OriginalProductBuildingPhase} .");
                    BOMBuildingPhaseRulePage.Instance.CloseToastMessage();
                }

            }
            CommonHelper.ScrollToEndOfPage();
            int totalItems = BOMBuildingPhaseRulePage.Instance.GetTotalNumberItem();
            // Scroll up to click utility button
            // Get export file name
            string exportFileName = CommonHelper.GetExportFileName(ExportType.BOMPhaseRules.ToString());
            //3. In the Building Phases page, click the BOM Building Phase Rules
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3. In the Building Phases page, click the BOM Building Phase Rules.</font>");
            //4. In BOM Building Phase Rules page click the Utilities/Gear
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4. In BOM Building Phase Rules page click the Utilities/Gear.</font>");
            //5. Select Export CSV in selected BOM building phase rule option and verify if you are able to export a csv file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5. Select Export CSV in selected BOM building phase rule option and verify if you are able to export a csv file.</font>");
            // Download baseline files before comparing files
            BOMBuildingPhaseRulePage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            BOMBuildingPhaseRulePage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.BOMPHASERULES_TITLE);

            //6. Select Export excel in selected BOM building phase rule option and verify if you are able to export a excel file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 6. Select Export excel in selected BOM building phase rule option and verify if you are able to export a excel file.</font>");
            // Download baseline files before comparing files
            BOMBuildingPhaseRulePage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
            BOMBuildingPhaseRulePage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.BOMPHASERULES_TITLE);

            //7. Go back to default page of BOM building phase rules /Dashboard/BuilderBom/BOMPhaseRules/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 7. Go back to default page of BOM building phase rules /Dashboard/BuilderBom/BOMPhaseRules/Default.aspx.</font>");

            //8. Select all product of building phase and select delete icon
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 8. Select all product of building phase and select delete icon.</font>");
            //9. Verify that there will be a confirmation pop up message on are you sure you want to delete the items and click ok / yes
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 9. Verify that there will be a confirmation pop up message on are you sure you want to delete the items and click ok / yes.</font>");
            //10. Verify that there will be a successful toast message in deleting the building phase item
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 10. Verify that there will be a successful toast message in deleting the building phase item.</font>");
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase) is true)
            {
                BOMBuildingPhaseRulePage.Instance.DeleteBOMBuildingPhaseRule(_data);
            }
            //11. Navigate back to the BOM building phase rules and verify the deleted items are gone
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 11. Navigate back to the BOM building phase rules and verify the deleted items are gone.</font>");
            //12. Select Import option and verify if BOM building phase rules pane is available for /Dashboard/BuilderBom/Transfers/Imports/BuilderBom.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 12. Select Import option and verify if BOM building phase rules pane is available for /Dashboard/BuilderBom/Transfers/Imports/BuilderBom.aspx.</font>");
            BOMBuildingPhaseRulePage.Instance.ImportExporFromMoreMenu("Import");
            string expectedURL = BaseDashboardUrl + "/BuilderBom/Transfers/Imports/BuilderBom.aspx";
            if (BOMBuildingPhaseRulePage.Instance.IsPageDisplayed(expectedURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import BOM Building Phase Rule page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Import BOM Building Phase Rule page is displayed</b></font>");
            }
            if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BOM_BUILDING_PHASE_RULE_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.BOM_BUILDING_PHASE_RULE_IMPORT} grid view to import new BOM Building Phase Rule.</font>");


            //13. Click Choose File button and select csv file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 13. Click Choose File button and select csv file</font>");
            //14. Click Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 14. Click Import button</font>");
            //15. Verify if the import is completed with the correct verbiage in green font: Import complete.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 15. Verify if the import is completed with the correct verbiage in green font: Import complete.</font>");
            string importFile = "\\DataInputFiles\\Import\\PIPE_30935\\Pipeline_BOMBuildingPhaseRules_Automation.csv";
            BOMBuildingPhaseRuleImportPage.Instance.ImportValidData(ImportGridTitle.BOM_BUILDING_PHASE_RULE_IMPORT, importFile);
            //16. Navigate back to / Dashboard / BuilderBom / BOMPhaseRules / Default.aspx and verify if the newly imported file is added in the list of BOM Building Phase Rules.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 16. Navigate back to / Dashboard / BuilderBom / BOMPhaseRules / Default.aspx and verify if the newly imported file is added in the list of BOM Building Phase Rules.</font>");
            BOMBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BomPhaseRules);
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _data1.OriginalProductBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>BOM Building Phase Rulet {_data1.OriginalProductBuildingPhase} is display on grid.<b>T</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>BOM Building Phase Rulet {_data1.OriginalProductBuildingPhase} was not display on grid.</font>");
            }

            //17. Go back to step# 12, select import option again
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 17. Go back to step# 12, select import option again.</font>");
            BOMBuildingPhaseRulePage.Instance.ImportExporFromMoreMenu("Import");

            //18. Click Choose File button and this time, select any file from your local machine to check the negative path text error.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 18. Click Choose File button and this time, select any file from your local machine to check the negative path text error.</font>");
            //19. Click Import button
            //20. Verify if the import is not successful with the correct error text in red font: Failed to import file due to wrong file format.File must be.csv format.
            string expectedErrorMess = " An error has occured while trying to read the imported CSV file.";
            string ImportFile = "\\DataInputFiles\\Import\\PIPE_30935\\Pipeline_BOMBuildingPhaseRulesErrorFile_Automation.csv";
            BOMBuildingPhaseRuleImportPage.Instance.ImportInvalidData(ImportGridTitle.BOM_BUILDING_PHASE_RULE_IMPORT, ImportFile, expectedErrorMess);
        }
    }
}
