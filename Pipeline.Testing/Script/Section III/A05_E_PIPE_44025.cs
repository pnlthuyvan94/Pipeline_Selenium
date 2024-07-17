using NUnit.Framework;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.OptionGroup;
using Pipeline.Testing.Pages.Assets.OptionGroupDetail;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Dashboard;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Pathway.Assets;
using Pipeline.Testing.Pages.Purchasing.CostCode;
using Pipeline.Testing.Pages.Settings.Purchasing;
using Pipeline.Testing.Pages.UserMenu.Role;
using Pipeline.Testing.Pages.UserMenu.Role.RolePermission;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_III
{
    public class A05_E_PIPE_44025 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private const string CutoffPhasesImportImport = "Cutoff Phases Import";
        private const string OptionGroupsToCutoffPhasesImportImport = "Option Groups To Cutoff Phases Import";
        private JobData jobData;
        private OptionData optionData;
        private OptionGroupData optionGroupData;

        [SetUp]
        public void SetupTest()
        {
            jobData = new JobData()
            {
                Name = "RT-QA_JOB_Import_Quantity",
            };

            optionData = new OptionData()
            {
                Name = "RT_QA_Automation_Option_36148",
            };

            optionGroupData = new OptionGroupData()
            {
                Name = "QA_RT_OPTION GROUP_AUTOMATION",
            };
        }


        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void A05_E_PIPE_44025_Hide_Cutoff_Phase_In_Purchasing_Module()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Purchasing menu and verify if CutOff Phases is no longer visible from the list of purchasing options.</b></font>");
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING, true);
            CommonHelper.CaptureScreen();
            IList<string> actual = DashboardPage.Instance.GetListPURCHASING;
            if (!actual.Contains("CutOff Phases"))
                ExtentReportsHelper.LogPass("Able to navigate to Purchasing menu and verify if CutOff Phases is no longer visible from the list of purchasing options");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Select Building Phases from Purchasing menu.</b></font>");
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Verify if CutOff Phases is no longer displayed from the left navigation.</b></font>");
            if (!actual.Contains("CutOff Phases"))
                ExtentReportsHelper.LogPass("CutOff Phases is no longer displayed from the left navigation");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Navigate to Cost Codes from the left navigation.</b></font>");
            PurchasingPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Click the ⚙️ icon from the upper right of the page.</b></font>");

            CostCodePage.Instance.OpenImportPage(BaseMenuUrls.PURCHASING_IMPORT_URL);
            CommonHelper.CaptureScreen();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Select Import and verify if these 2 panes are no longer appearing on the page: Option Groups To CutOff Phases Import and Options To CutOff Phases Import.</b></font>");
            if ((PurchasingImportPage.Instance.IsImportLabelDisplay(CutoffPhasesImportImport) is false))
                ExtentReportsHelper.LogPass("Able to select Import and Options To CutOff Phases Import pane sis no longer appearing on the page");

            if ((PurchasingImportPage.Instance.IsImportLabelDisplay(OptionGroupsToCutoffPhasesImportImport) is false))
                ExtentReportsHelper.LogPass("Able to navigate to Jobs > All Jobs.");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Navigate to Jobs > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            ExtentReportsHelper.LogPass("Able to navigate to Assets > Options.");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Select any jobs from the list.</b></font>");
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Verify if CutOff Phases is no longer appearing under Purchasing section from the left navigation pane.</b></font>");

                bool isPresent = JobDetailPage.Instance.IsPresentInLeftMenuNavigation("CutOff Phases");
                if (isPresent == false)
                    ExtentReportsHelper.LogPass("CutOff Phases is no longer appearing under Purchasing section from the left navigation pane.");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Navigate to Assets > Options.</b></font>");
            AssetsPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            ExtentReportsHelper.LogPass("Able to navigate to Assets > Options.");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.11: Select any options from the list.</b></font>");
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", optionData.Name);
                ExtentReportsHelper.LogPass("Able to select any options from the list.");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.12: In Options details, verify if CutOff Phase field is no longer available.</b></font>");
                bool isPresent = OptionDetailPage.Instance.IsCutOffPhasePanelDisplayed();
                if (isPresent == false)
                    ExtentReportsHelper.LogPass("CutOff Phase field is no longer available in Option details page.");

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.13: Navigate to Assets > Option Groups.</b></font>");
            AssetsPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionGroups);
            ExtentReportsHelper.LogPass("Able to select any option group from the list.");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.14: Select any option group from the list.</b></font>");
            OptionGroupPage.Instance.FilterItemInGrid("Option Group Name", GridFilterOperator.Contains, optionGroupData.Name);
            if (OptionGroupPage.Instance.IsItemInGrid("Option Group Name", optionGroupData.Name) is true)
            {
                OptionGroupPage.Instance.SelectItemInGrid("Option Group Name", optionGroupData.Name);
                ExtentReportsHelper.LogPass("Able to select any option group from the list.");

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.15: In Option Groups page, verify if CutOff Phase field is no longer available.</b></font>");
                bool isPresent = OptionGroupDetailPage.Instance.IsCutOffPhasePanelDisplayed();
                if (isPresent == false)
                    ExtentReportsHelper.LogPass("CutOff Phase field is no longer available in Option Groups page.");
            }
        }

        #endregion

        [TearDown]
        public void TearDownTest()
        {

        }


    }
}
