

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
    class B05_B_PIPE_22395 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private BOMBuildingPhaseRuleData _data;
        private BOMBuildingPhaseRuleData _data1;
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";
        private const string EXPORT_CSV_MORE_MENU = "Export CSV";

        private const string BOM_BUILDING_PHASE_NAME1 = "123-QA_RT_BuildingPhase1_Automation";
        private const string BOM_BUILDING_PHASE_NAME2 = "124-QA_RT_BuildingPhase2_Automation";

        private const string NEW_SUBCOMPONENT_BUILDINGPHAS = "123-QA_RT_BuildingPhase1_Automation";
        private const int BOM_BUILDING_PHASE_NAME_TOTAL = 1;
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

            string importFile = "\\DataInputFiles\\Import\\PIPE_22395\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

        }
        [Test]
        [Category("Section_III")]
        public void B03_B_Estimating_BOMBuildingPhaseRules_Export_Import_BOMBuildingPhaseRules()
        {
            //1.Access http://dev.bimpipeline.com using your credentials

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 1.Access http://dev.bimpipeline.com using your credentials.</font>");
            //2. Navigate to Estimating > Building Phases > BOM Building Phase Rules page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 2. Navigate to Estimating > Building Phases > BOM Building Phase Rules page.</font>");
            BOMBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BomPhaseRules);

            //3. Verify if BOM Building Phase Rules page is displayed with the following columns in the table grid:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 3. Verify if BOM Building Phase Rules page is displayed with the following columns in the table grid:.</font>");
            BOMBuildingPhaseRulePage.Instance.IsColumnHeaderIndexByName("Original Product Building Phase");
            BOMBuildingPhaseRulePage.Instance.IsColumnHeaderIndexByName("Original Subcomponent Building Phase");
            BOMBuildingPhaseRulePage.Instance.IsColumnHeaderIndexByName("New Subcomponent Building Phase");

            //4. Click on Add button at the upper right part of the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 4. Click on Add button at the upper right part of the table grid</font>");
            BOMBuildingPhaseRulePage.Instance.IsAddAndUtilitiesButton();
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase) is false)
            {
                //5. Verify if Add New BOM Building Phase Rule modal is displayed
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 5. Verify if Add New BOM Building Phase Rule modal is displayed</font>");
                //6. In these 3 dropdown, select the following values
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 6. In these 3 dropdown, select the following values</font>");
                //7. Click Save button
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 7. Click Save button</font>");
                //8. Verify if correct toast notification message is displayed on the screen: New BOM Phase Rule successfully inserted
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Step 8. Verify if correct toast notification message is displayed on the screen: New BOM Phase Rule successfully inserted</font>");
                BOMBuildingPhaseRulePage.Instance.ClickAddToOpenBuildingPhaseRuleModal();
                if (BOMBuildingPhaseRulePage.Instance.AddBuildingPhaseRuleModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Building Phase Rules modal is not displayed.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>New Building Phase Rules modal is displayed.</b></font>");
                }

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

            //9. Verify if the entry is added successfully on the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 9. Verify if the entry is added successfully on the table grid</font>");
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>BOM Building Phase Rulet {_data.OriginalProductBuildingPhase} is display on grid.<b>T</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>BOM Building Phase Rulet {_data.OriginalProductBuildingPhase} was not display on grid.</font>");
            }


            //10. Click on Utilities add  icon
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 10. Click on Utilities add icon</font>");
            CommonHelper.ScrollToEndOfPage();
            int totalItems = BOMBuildingPhaseRulePage.Instance.GetTotalNumberItem();
            // Scroll up to click utility button

            //11. Verify if these options are available in the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 11. Verify if these options are available in the dropdown list</font>");
            string exportFileName = CommonHelper.GetExportFileName(ExportType.BOMPhaseRules.ToString());

            //12. Select Export CSV
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 12. Select Export CSV</font>");
            //13. Verify if you are able to download a .csv file: Pipeline_BOMPhaseRules.csv
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 13. Verify if you are able to download a .csv file: Pipeline_BOMPhaseRules.csv</font>");

            //14. Verify if the file contains the correct data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 14. Verify if the file contains the correct data</font>");
            // Download baseline files before comparing files
            BOMBuildingPhaseRulePage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            BOMBuildingPhaseRulePage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.BOMPHASERULES_TITLE);

            //15. Select Export Excel
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 15. Select Export Excel</font>");
            //16. Verify if you are able to download a .xlsx file: Pipeline_BOMPhaseRules.xlsx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 16. Verify if you are able to download a .xlsx file: Pipeline_BOMPhaseRules.xlsx</font>");

            //17. Verify if the file contains the correct data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 17. Verify if the file contains the correct data</font>");
            // Download baseline files before comparing files
            BOMBuildingPhaseRulePage.Instance.DownloadBaseLineFile(EXPORT_EXCEL_MORE_MENU, exportFileName);
            BOMBuildingPhaseRulePage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, totalItems, ExportTitleFileConstant.BOMPHASERULES_TITLE);

            //18. Select Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 18. Select Import</font>");
            BOMBuildingPhaseRulePage.Instance.ImportExporFromMoreMenu("Import");

            //19.  Verify if the page redirects to > http://dev.bimpipeline.com/Dashboard/BuilderBom/Transfers/Imports/BuilderBom.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 19.  Verify if the page redirects to > http://dev.bimpipeline.com/Dashboard/BuilderBom/Transfers/Imports/BuilderBom.aspx</font>");
            string expectedURL = BaseDashboardUrl + "/BuilderBom/Transfers/Imports/BuilderBom.aspx";
            if (BOMBuildingPhaseRulePage.Instance.IsPageDisplayed(expectedURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import BOM Building Phase Rule page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Import BOM Building Phase Rule page is displayed</b></font>");
            }

            //20. Verify if BOM Building Phase Rules Import pane is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 20. Verify if BOM Building Phase Rules Import pane is displayed</font>");
            if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BOM_BUILDING_PHASE_RULE_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.BOM_BUILDING_PHASE_RULE_IMPORT} grid view to import new BOM Building Phase Rule.</font>");

            //21. Download the attached import file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 21. Download the attached import file</font>");
            //22. Click on Choose File button in BOM Building Phase Rules Import pane
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 22. Click on Choose File button in BOM Building Phase Rules Import pane</font>");
            //23. Select the downloaded template
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 23. Select the downloaded template</font>");

            //24. Click on Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 24. Click on Import button</font>");
            string importFile = "\\DataInputFiles\\Import\\PIPI_22395\\Pipeline_BOMBuildingPhaseRules_Automation.csv";

            //25. Verify if there is a correct validation message: Import complete.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 25. Verify if there is a correct validation message: Import complete.</font>");
            BOMBuildingPhaseRuleImportPage.Instance.ImportValidData(ImportGridTitle.BOM_BUILDING_PHASE_RULE_IMPORT, importFile);

            //26. Navigate back to this page > http://dev.bimpipeline.com/Dashboard/BuilderBom/BOMPhaseRules/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 26. Navigate back to this page > http://dev.bimpipeline.com/Dashboard/BuilderBom/BOMPhaseRules/Default.aspx.</font>");
            //27. Verify if the imported entries can now be seen in the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 27. Verify if the imported entries can now be seen in the table grid.</font>");
            BOMBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BomPhaseRules);
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _data1.OriginalProductBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>BOM Building Phase Rulet {_data1.OriginalProductBuildingPhase} was not display on grid.<b>T</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>BOM Building Phase Rulet {_data1.OriginalProductBuildingPhase} was not display on grid.</font>");
            }

            //28. Click on Choose File button again BOM Building Phase Rules Import pane
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 28. Click on Choose File button again BOM Building Phase Rules Import pane.</font>");
            BOMBuildingPhaseRulePage.Instance.ImportExporFromMoreMenu("Import");

            //29. Choose the .xlsx file from step #15
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 29. Choose the .xlsx file from step #15.</font>");
            //30. Click Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 30. Click Import button.</font>");

            //31. Verify if there is a correct validation message: Failed to import file due to wrong file format.File must be.csv format.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 31. Verify if there is a correct validation message: Failed to import file due to wrong file format.File must be.csv format.</font>");
            string expectedErrorMess = " An error has occured while trying to read the imported CSV file.";
            string ImportFile = "\\DataInputFiles\\Import\\PIPI_22395\\Pipeline_BOMBuildingPhaseRulesErrorFile_Automation.csv";
            BOMBuildingPhaseRuleImportPage.Instance.ImportInvalidData(ImportGridTitle.BOM_BUILDING_PHASE_RULE_IMPORT, ImportFile, expectedErrorMess);

            //32. Navigate back to http://dev.bimpipeline.com/Dashboard/BuilderBom/BOMPhaseRules/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 32. Navigate back to http://dev.bimpipeline.com/Dashboard/BuilderBom/BOMPhaseRules/Default.aspx.</font>");
            BOMBuildingPhaseRulePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BomPhaseRules);

            //32. Verify if filter functionality throws the correct filter results
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 32. Verify if filter functionality throws the correct filter results.</font>");
            BOMBuildingPhaseRulePage.Instance.FilterItemInGrid("Original Product Building Phase", GridFilterOperator.Contains, _data.OriginalProductBuildingPhase);
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>BOM Building Phase Rulet {_data.OriginalProductBuildingPhase} is display on grid.<b>T</font>");
                //33.  Click on Edit icon on one of the entries in the table grid
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 33.  Click on Edit icon on one of the entries in the table grid.</font>");

                //34. Change the New Subcomponent Building Phase and click the Update  button
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 34. Change the New Subcomponent Building Phase and click the Update  button.</font>");

                //35. Change the New Subcomponent Building Phase and click the Update button
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 35. Change the New Subcomponent Building Phase and click the Update button.</font>");

                //36. Verify if correct toast validation message is displayed on the screen: Successfully updated BOM Building Phase Rule
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 36. Verify if correct toast validation message is displayed on the screen: Successfully updated BOM Building Phase Rule.</font>");
                BOMBuildingPhaseRulePage.Instance.EditItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase);
                BOMBuildingPhaseRulePage.Instance.UpdateNewSubcomponentBuildingPhase(NEW_SUBCOMPONENT_BUILDINGPHAS);
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>BOM Building Phase Rulet {_data.OriginalProductBuildingPhase} was not display on grid.</font>");
            }

            //37. Verify if the updated subcomponent is reflected in the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 37. Verify if the updated subcomponent is reflected in the table grid.</font>");

            //38. Click on the Delete icon besides the Edit icon
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 38. Click on the Delete icon besides the Edit icon.</font>");
            BOMBuildingPhaseRulePage.Instance.DeleteItemInGrid("New Subcomponent Building Phase", NEW_SUBCOMPONENT_BUILDINGPHAS);

            //39. Verify if correct toast notification message is displayed on the screen: Successfully deleted BOM Building Phase Rule
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 39. Verify if correct toast notification message is displayed on the screen: Successfully deleted BOM Building Phase Rule.</font>");

            //40. Verify if the entry is removed in the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 40. Verify if the entry is removed in the table grid.</font>");
            string successfulMess = $"Successfully deleted BOM Building Phase Rule";
            if (successfulMess == BOMBuildingPhaseRulePage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "BOM Building Phase Rule deleted successfully!");
                BOMBuildingPhaseRulePage.Instance.CloseToastMessage();
            }
            else
            {
                if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("New Subcomponent Building Phase", NEW_SUBCOMPONENT_BUILDINGPHAS))
                    ExtentReportsHelper.LogFail("BOM Building Phase Rule could not be deleted!");
                else
                    ExtentReportsHelper.LogPass(null, "BOM Building Phase Rule deleted successfully!");
            }

            //41. Select all the remaining entries in the table grid
            BOMBuildingPhaseRulePage.Instance.FilterItemInGrid("Original Product Building Phase", GridFilterOperator.NoFilter, string.Empty);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 41. Select all the remaining entries in the table grid.</font>");
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", BOM_BUILDING_PHASE_NAME2) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import BOM Building Phase Rule page is successfully and show in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import BOM Building Phase Rule page is unsuccessfully and isn't show in grid.</font>");
            }

            //42. Click Bulk Actions > Remove Selected option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 42. Click Bulk Actions > Remove Selected option.</font>");
            BOMBuildingPhaseRulePage.Instance.DeleteItemSelectedInGrid(0);

            //43. Verify if correct toast validation message is displayed on the page: [x] Building Phase Rule deleted
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 43. Verify if correct toast validation message is displayed on the page: [x] Building Phase Rule deleted.</font>");
            string expectedDeletedMessage = $"{BOM_BUILDING_PHASE_NAME_TOTAL} BOM Building Phase Rule deleted";
            string actualDeletedMessage = BOMBuildingPhaseRulePage.Instance.GetLastestToastMessage();
            if (actualDeletedMessage == expectedDeletedMessage)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Delete BOM Building Phase Rule successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color ='red'>Delete BOM Building Phase Rule unsuccessfully.<br>Actual result :{actualDeletedMessage}<br></font>");
            }

            //44. Verify if all entries are removed in the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 44. Verify if all entries are removed in the table grid.</font>");
            if (BOMBuildingPhaseRulePage.Instance.IsItemInGrid("Original Product Building Phase", BOM_BUILDING_PHASE_NAME2) is false)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Delete BOM Building Phase Rule page is successfully and show in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Delete BOM Building Phase Rule is unsuccessfully and isn't show in grid.</font>");
            }
        }
    }
}
