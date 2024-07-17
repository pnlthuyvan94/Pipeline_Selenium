using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.JobSpecSets;


namespace Pipeline.Testing.Script.Section_IV
{
    public partial class C01_C_PIPE_18714 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private JobData jobData;
        private JobSpecSetsData jobSpecSetsData;

        private readonly string COMMUNITY_CODE_DEFAULT = "456";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community1_Automation";

        private readonly string HOUSE_CODE_DEFAULT = "456";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House1_Automation";

        private readonly string JOB_NAME_DEFAULT = "QA_RT_Job1_Automation";

        readonly string JOBS_NAME = "Jobs";

        [SetUp]
        public void SetUpData()
        {

            jobData = new JobData()
            {
                Name = "QA_RT_Job_SpecSet_PIPE_PIPI_18714_Automation",
                Community = "456-QA_RT_Community1_Automation",
                House = "456-QA_RT_House1_Automation",
                Lot = "QA_RT_Lot1_Automation - Available",
                Orientation = "None"
            };
            jobSpecSetsData = new JobSpecSetsData()
            {
                JobSpecSetName = "QA_RT_SpecSetGroup1_Automation",
                JobSetOverride = "QA_RT_SpecSet1_Automation",
                PageSize = 20,
                PageNumber = 3
            };

            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.ChangeSpecSetPageSize(20);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", jobSpecSetsData.JobSpecSetName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", jobSpecSetsData.JobSpecSetName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {jobSpecSetsData.JobSpecSetName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", jobSpecSetsData.JobSpecSetName);
            }
            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(jobSpecSetsData.JobSpecSetName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", jobSpecSetsData.JobSpecSetName);
            SpecSetPage.Instance.SelectItemInGrid("Name", jobSpecSetsData.JobSpecSetName);

            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(jobSpecSetsData.JobSetOverride);

            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(JOBS_NAME, JOB_NAME_DEFAULT, jobSpecSetsData.JobSetOverride, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT));
        }

        [Test]
        [Category("Section_IV")]
        public void C01_C_Jobs_DetailPages_AllJobs_SpecSets()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Jobs menu > All Jobs.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Create a new Job.<b>");
                JobPage.Instance.CreateJob(jobData);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"The Job {jobData.Name} is exited");
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
            }

            // Step 1: Navigate to Jobs menu > All Jobs
            Assert.That(JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(jobData.Name));

            // Step 2:  In the Side Navigation, open the Spec Sets data page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: In the Side Navigation, open the Spec Sets data page.</b></font>");
            JobDetailPage.Instance.LeftMenuNavigation("Spec Sets");
            JobSpecSetsPage.Instance.VerifyJobSpecSetsPageIsDisplayed();

            // Step 3:  Select a Spec Set and edit Job Set Override column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Select a Spec Set and edit Job Set Override column.</b></font>");
            JobSpecSetsPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, jobSpecSetsData.JobSpecSetName);
            if(JobSpecSetsPage.Instance.IsItemInGrid("Name", jobSpecSetsData.JobSpecSetName) is true)
            {
                JobSpecSetsPage.Instance.ClickEditItemInGrid("Name", jobSpecSetsData.JobSpecSetName);
                string actualMsg = JobSpecSetsPage.Instance.UpdateJobOverrideItemInGrid(jobSpecSetsData.JobSetOverride);
                string expectedMsg = "Record was successfully updated.";
                if (actualMsg.Equals(expectedMsg))
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Record was successfully updated.</b></font>");
                    JobSpecSetsPage.Instance.GetLastestToastMessage();
                }
                else 
                {
                    ExtentReportsHelper.LogFail("<font color='red'>Record was unsuccessfully updated. Actual Result : </font>");
                }
                JobSpecSetsPage.Instance.RefreshPage();
                JobSpecSetsPage.Instance.VerifyJobSetOverrideIsDisplayedCorrectly(jobSpecSetsData.JobSetOverride);
            }
           
            // 4.Filter the Spec Sets on page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Filter the Spec Sets on page.</b></font>");
            JobSpecSetsPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, jobSpecSetsData.JobSpecSetName);
            JobSpecSetsPage.Instance.VerifyFilterItemInGridIsDisplayedCorrectly(jobSpecSetsData.JobSpecSetName);
            JobSpecSetsPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NotIsEmpty, "");
            JobSpecSetsPage.Instance.ChangeJobSpecSetPageSize(jobSpecSetsData.PageSize);
            JobSpecSetsPage.Instance.NavigateToPage(jobSpecSetsData.PageNumber);
        }
        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Delete a new Job.<b>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name))
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }
        }

    }
}

