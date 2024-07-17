using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders;
using Pipeline.Testing.Pages.Settings.Purchasing;
using Pipeline.Testing.Pages.UserMenu.Setting;


namespace Pipeline.Testing.Script.Section_VIII
{
    public partial class UAT_HOTFIX_PIPE_30850 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VIII);
        }
        JobData jobData;
        private const string BuildingPhase = "4700-INSULATION";
        private const string Po = "_AJob004";
        private const string PurchaseOrderUrl = "Purchasing/PurchaseOrder/PurchaseOrderByOption.aspx?poid=6006&ponum=_AJob004";

        [SetUp]
        public void GetData()
        {
            jobData = new JobData()
            {
                Name = "_AJob"
            };
        }

        [Test]
        [Category("Section_VIII")]
        public void UAT_HotFix_Hide_ZeroCostItems_From_PurchaseOrder()
        {
            //Step 1: Navigate to Settings > Purchase and enable Hide Zero Cost 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Settings > Purchase and enable Hide Zero Cost.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Purchasing");

            string url = SettingPage.Instance.CurrentURL;
            PurchasingPage.Instance.Check_HideZeroCostItems(true);

            //Step 2: Navigate to a JOBS > All Jobs then select a Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Navigate to a JOBS > All Jobs then select a Job.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
            }
            // Log page detail has been opened
            if (JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(jobData.Name) is true)
            {
                ExtentReportsHelper.LogInformation("<font color='green'><b> Jobs page detail has been opened.</b></font>");
            }
            else
                ExtentReportsHelper.LogInformation("<font color='red'><b> Cannot open Jobs page detail.</b></font>");

            //Step 3: Navigate to Create Purchase Orders on Job and select an item and click Create POs button.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Navigate to Create Purchase Orders on Job and select an item and click Create POs button.</b></font>");         
            JobDetailPage.Instance.LeftMenuNavigation("Create Purchase Orders");

            if (CreatePurchaseOrdersPage.Instance.IsItemInGrid("Building Phase", BuildingPhase) is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Create PO page is loaded.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Create PO page failed to load.</b></font>");

            CreatePurchaseOrdersPage.Instance.CheckStatusOfPOHasBeenCreatedInBuldingPhase(BuildingPhase);

            //Step 4: Navigate to View Purchase Orders page on Job.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Navigate to View Purchase Orders page on Job.</b></font>");
            CreatePurchaseOrdersPage.Instance.LeftMenuNavigation("View Purchase Orders");
            ViewPurchaseOrdersPage.Instance.VerifyViewPuchaseOrdersIsDisplayed("/Purchasing/PurchaseOrder/JobPurchaseOrders.aspx?jid=2429");

            //Step 5: View Purchase Order Icon and Observe.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: View Purchase Order Icon and Observe.</b></font>");
            ViewPurchaseOrdersPage.Instance.ViewPurchaseOrderInGridAndVerify(BuildingPhase, PurchaseOrderUrl, Po);
            CommonHelper.SwitchToDefaultContent();

            //Step 6: Navigate to Settings > Purchase and disable Hide Zero Cost.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: Navigate to Settings > Purchase and disable Hide Zero Cost.</b></font>");
            CommonHelper.OpenURL(url);
            CommonHelper.SwitchTab(1);
            PurchasingPage.Instance.Check_HideZeroCostItems(false);
            CommonHelper.SwitchTab(0);

            //Step 7: Navigate back to View Purchase Orders page on Job.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7: Navigate back to View Purchase Orders page on Job.</b></font>");

            // navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Jobs/Jobs/Default.aspx
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
            }

            CreatePurchaseOrdersPage.Instance.LeftMenuNavigation("View Purchase Orders");
            ViewPurchaseOrdersPage.Instance.VerifyViewPuchaseOrdersIsDisplayed("/Purchasing/PurchaseOrder/JobPurchaseOrders.aspx?jid=2429");

            //Step 8:View Purchase Order Icon and Observe.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8:View Purchase Order Icon and Observe.</b></font>");
            ViewPurchaseOrdersPage.Instance.ViewPurchaseOrderInGridAndVerify(BuildingPhase, PurchaseOrderUrl, Po);
        }
    }
}
