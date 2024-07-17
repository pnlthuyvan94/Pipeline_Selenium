using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Purchasing.CutoffPhase;
using Pipeline.Testing.Pages.Purchasing.CutoffPhase.CutoffPhaseDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class E06_A_PIPE_18677 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CutoffPhaseData cutoffPhaseData;
        private CutoffPhaseData cutoffPhaseData_Update;
        private string[] buildingPhaseList;
        private string[] optionGroupList;
        private string[] optionList;
        string cutoffPhaseDetailtUrl = string.Empty;

        private const string BUILDING_PHASE = "Building Phases";
        private const string OPTION_GROUP = "Option Groups";
        private const string OPTION = "Options";

        private const string EXPORT_OPTION_GROUPS_TO_CSV = "Export CSV (Option Groups to Cut Off Phase)";
        private const string EXPORT_OPTION_GROUPS_TO_EXCEL = "Export Excel (Option Groups to Cut Off Phase)";
        private const string EXPORT_OPTIONS_TO_CSV = "Export CSV (Options to Cut Off Phase)";
        private const string EXPORT_OPTIONS_TO_EXCEL = "Export Excel (Options to Cut Off Phase)";
        private const string IMPORT_OPTION_GROUPS_TO_CUTOFF_PHASE = "Option Groups To Cutoff Phases Import";
        private const string IMPORT_OPTIONS_TO_CUTOFF_PHASE = "Options To Cutoff Phases Import";
        private const string IMPORT = "Import";

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            cutoffPhaseData = new CutoffPhaseData()
            {
                Name = "QA_RT_Cutoff_Phase",
                Code = "RT_Auto",
                SortOrder = "0"
            };

            cutoffPhaseData_Update = new CutoffPhaseData()
            {
                Name = "QA_RT_Cutoff_Phase_Update",
                Code = "RT_Auto_Update",
                SortOrder = "1"
            };

            buildingPhaseList = new string[] { "QA_RT_Building_Phase_Automation_01", "QA_RT_Building_Phase_Automation_02", "QA_RT_Building_Phase_Automation_03" };
            optionGroupList = new string[] { "QA_RT_Option_Group_Automation_01", "QA_RT_Option_Group_Automation_02", "QA_RT_Option_Group_Automation_03" };
            optionList = new string[] { "QA_RT_Option_Automation_01", "QA_RT_Option_Automation_02", "QA_RT_Option_Automation_03" };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate Purchasing > Cutoff Phase and open Cutoff Phase Detail page.</b></font>");
            CutoffPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CutoffPhases);

            CutoffPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, cutoffPhaseData_Update.Code);
            if (CutoffPhasePage.Instance.IsItemInGrid("Code", cutoffPhaseData_Update.Code) is true)
            {
                // Delete the data before updating to the same name
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete Cutoff Phase with code '{cutoffPhaseData_Update.Code}' before updating to the same one.</b></font>");
                CutoffPhasePage.Instance.DeleteCutOffPhase(cutoffPhaseData_Update);
            }

            CutoffPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, cutoffPhaseData.Code);
            if (CutoffPhasePage.Instance.IsItemInGrid("Code", cutoffPhaseData.Code) is false)
            {
                // Create a new Cutoff Phase incase it doesn't exist
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Create a new Cutoff Phase with code '{cutoffPhaseData.Code}' incase it doesn't exist.</b></font>");
                CutoffPhasePage.Instance.CreateNewCutoffPhase(cutoffPhaseData);
            }

            // Select item to open detail page
            CutoffPhasePage.Instance.SelectItemInGrid("Name", cutoffPhaseData.Name);
            cutoffPhaseDetailtUrl = CutoffPhaseDetailPage.Instance.CurrentURL;

            // Verify open Cutoff Phase detail page display correcly
            if (CutoffPhaseDetailPage.Instance.IsCutoffPhaseTitleDisplayed() is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The Cutoff Phase detail page <b>'{cutoffPhaseData.Name}'</b> displays sucessfully with URL: <b>{cutoffPhaseDetailtUrl}</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The Cutoff Phase '{cutoffPhaseData.Name}' displays with incorrect sub header.</font>");
        }

        #region"Test case"

        [Test]
        [Category("Section_IV")]
        [Ignore("The Cutoff Phase was removed from Purchasing menu - PIPE-21663, so this test sript will be ignored.")]
        public void E06_A_Purchasing_DetailPage_CutoffPhases_CutoffPhase()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Update Cutoff Phase with new value.</b></font>");
            CutoffPhaseDetailPage.Instance.UpdateCutoffPhase(cutoffPhaseData_Update);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Assign Building Phases to current Cutoff Phase.</b></font>");
            CutoffPhaseDetailPage.Instance.ClickAddAttributeButton(BUILDING_PHASE);
            if (CutoffPhaseDetailPage.Instance.IsAddAttributeModalDisplayed(BUILDING_PHASE) is true)
                ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Add Building Phases modal displays correctly.<b></font>");
            else
                ExtentReportsHelper.LogFail("<font color = 'red'>Add Building Phases modal doesn't display or the title isn't correct.</font>");

            CutoffPhaseDetailPage.Instance.SelectAttributeByName(cutoffPhaseData_Update.Name, BUILDING_PHASE, buildingPhaseList);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Assign Option Groups to current Cutoff Phase.</b></font>");
            CutoffPhaseDetailPage.Instance.ClickAddAttributeButton(OPTION_GROUP);
            if (CutoffPhaseDetailPage.Instance.IsAddAttributeModalDisplayed(OPTION_GROUP) is true)
                ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Add Option Groups modal displays correctly.<b></font>");
            else
                ExtentReportsHelper.LogFail("<font color = 'red'>Add Option Groups modal doesn't display or the title isn't correct.</font>");

            CutoffPhaseDetailPage.Instance.SelectAttributeByName(cutoffPhaseData_Update.Name, OPTION_GROUP, optionGroupList);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Assign Options to current Cutoff Phase.</b></font>");
            CutoffPhaseDetailPage.Instance.ClickAddAttributeButton(OPTION);
            if (CutoffPhaseDetailPage.Instance.IsAddAttributeModalDisplayed(OPTION) is true)
                ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Add Option modal displays correctly.<b></font>");
            else
                ExtentReportsHelper.LogFail("<font color = 'red'>Add Option modal doesn't display or the title isn't correct.</font>");

            CutoffPhaseDetailPage.Instance.SelectAttributeByName(cutoffPhaseData_Update.Name, OPTION, optionList);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1: Filter these assigned Building Phases.</b></font>");
            CutoffPhaseDetailPage.Instance.VerifyAttributeOnGrid(BUILDING_PHASE, true, buildingPhaseList);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Remove Building Phase '{buildingPhaseList[0]}' from Cutoff Phase.</b></font>");
            CutoffPhaseDetailPage.Instance.RemoveAttributeByName(cutoffPhaseData_Update.Name, BUILDING_PHASE, "Name", buildingPhaseList[0]);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2: Filter these assigned Option Groups.</b></font>");
            CutoffPhaseDetailPage.Instance.VerifyAttributeOnGrid(OPTION_GROUP, true, optionGroupList);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Remove Option Group '{optionGroupList[0]}' from Cutoff Phase.</b></font>");
            CutoffPhaseDetailPage.Instance.RemoveAttributeByName(cutoffPhaseData_Update.Name, OPTION_GROUP, "Name", optionGroupList[0]);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.3: Filter these assigned Options.</b></font>");
            CutoffPhaseDetailPage.Instance.VerifyAttributeOnGrid(OPTION, true, optionList);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Remove Option '{optionList[0]}' from Cutoff Phase.</b></font>");
            CutoffPhaseDetailPage.Instance.RemoveAttributeByName(cutoffPhaseData_Update.Name, OPTION, "Name", optionList[0]);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.1.1: Verify 'EXPORT CSV (Option Groups to Cut Off Phase)' function.</b></font>");
            CutoffPhaseDetailPage.Instance.ImportExporFromMoreMenu(EXPORT_OPTION_GROUPS_TO_CSV, cutoffPhaseData_Update.Name);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.1.2: Verify 'EXPORT Excel (Option Groups to Cut Off Phase') function.</b></font>");
            CutoffPhaseDetailPage.Instance.ImportExporFromMoreMenu(EXPORT_OPTION_GROUPS_TO_EXCEL, cutoffPhaseData_Update.Name);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.1.3: Verify 'EXPORT CSV (Option to Cut Off Phase)' function.</b></font>");
            CutoffPhaseDetailPage.Instance.ImportExporFromMoreMenu(EXPORT_OPTIONS_TO_CSV, cutoffPhaseData_Update.Name);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.1.4: Verify 'EXPORT Excel (Option to Cut Off Phase)' function.</b></font>");
            CutoffPhaseDetailPage.Instance.ImportExporFromMoreMenu(EXPORT_OPTIONS_TO_EXCEL, cutoffPhaseData_Update.Name);

            // Delete all Building Phase, Option Group and Option before importing new ones
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Remove all Building Phases, Option Groups and Options before importing new ones.</b></font>");
            CutoffPhaseDetailPage.Instance.RemoveAllAttributesFromCutoffPhase(cutoffPhaseData_Update.Name, BUILDING_PHASE);
            CutoffPhaseDetailPage.Instance.RemoveAllAttributesFromCutoffPhase(cutoffPhaseData_Update.Name, OPTION_GROUP);
            CutoffPhaseDetailPage.Instance.RemoveAllAttributesFromCutoffPhase(cutoffPhaseData_Update.Name, OPTION);

            string currentUrl = CutoffPhaseDetailPage.Instance.CurrentURL;
            string importFileDir;
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.2.1: Verify IMPORT Option Groups To Cutoff function.</b></font>");

            // Open Import page
            importFileDir = "\\DataInputFiles\\Purchasing\\Cutoff Phase\\Pipeline_OptionGroupsToCutOffPhase.csv";
            // Click import from Utilities button
            CutoffPhaseDetailPage.Instance.ImportExporFromMoreMenu(IMPORT, cutoffPhaseData_Update.Name);
            CutoffPhaseDetailPage.Instance.ImportFile(IMPORT_OPTION_GROUPS_TO_CUTOFF_PHASE, importFileDir);

            // Open cutoff phase detail page on a new tab to verify these impoted items
            CommonHelper.OpenLinkInNewTab(currentUrl);
            CommonHelper.SwitchTab(1);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify these imported Option Groups on the grid view.</b></font>");
            CutoffPhaseDetailPage.Instance.VerifyAttributeOnGrid(OPTION_GROUP, false, optionGroupList);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.2.2: Verify IMPORT Options To Cutoff function.</b></font>");
            // Back to import tab
            CommonHelper.SwitchTab(0);

            importFileDir = "\\DataInputFiles\\Purchasing\\Cutoff Phase\\Pipeline_OptionsToCutOffPhase.csv";
            CutoffPhaseDetailPage.Instance.ImportFile(IMPORT_OPTIONS_TO_CUTOFF_PHASE, importFileDir);

            // Switch to cuttoff phase detail page on tab 1  to verify these impoted items
            CommonHelper.SwitchTab(1);
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Verify these imported Options on the grid view.</b></font>");
            CutoffPhaseDetailPage.Instance.VerifyAttributeOnGrid(OPTION, false, optionList);
        }

        #endregion

        [TearDown]
        public void DeleteData()
        {
            // Close all tab exclude the current one
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            CommonHelper.OpenURL(cutoffPhaseDetailtUrl);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.1: Remove ALL Option Groups before deleting the current Cutoff Phase.</b></font>");
            CutoffPhaseDetailPage.Instance.RemoveAllAttributesFromCutoffPhase(cutoffPhaseData_Update.Name, OPTION_GROUP);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.2: Remove ALL Options before deleting the current Cutoff Phase.</b></font>");
            CutoffPhaseDetailPage.Instance.RemoveAllAttributesFromCutoffPhase(cutoffPhaseData_Update.Name, OPTION);

            // Delete Cutoff Phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9: Back to Cutoff Phase default page and delete it.</b></font>");
            CutoffPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CutoffPhases);

            // Delete the updated data
            CutoffPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, cutoffPhaseData_Update.Code);
            if (CutoffPhasePage.Instance.IsItemInGrid("Code", cutoffPhaseData_Update.Code) is true)
            {
                // Delete Cutoff Phase if it exists
                CutoffPhasePage.Instance.DeleteCutOffPhase(cutoffPhaseData_Update);
            }

            // Delete the set up data
            CutoffPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, cutoffPhaseData.Code);
            if (CutoffPhasePage.Instance.IsItemInGrid("Code", cutoffPhaseData.Code) is true)
            {
                // Delete Cutoff Phase if it exists
                CutoffPhasePage.Instance.DeleteCutOffPhase(cutoffPhaseData);
            }

        }
    }
}
