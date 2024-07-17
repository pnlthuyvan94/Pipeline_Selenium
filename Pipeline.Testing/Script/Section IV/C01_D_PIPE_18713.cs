using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Documents;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.IO;

namespace Pipeline.Testing.Script.Section_IV
{
    class C01_D_PIPE_18713 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private JobData jobData;
        private IList<string> fileList;
        private DocumentData newData;
        [SetUp]
        public void SetUpData()
        {
            Row newRowOfTestData = ExcelFactory.GetRow(DocumentPage.Instance.TestData_RT01207, 1);
            newData = new DocumentData()
            {
                Name = newRowOfTestData["Name"],
                Description = newRowOfTestData["Description"],
                Upload = newRowOfTestData["Uploaded"]
            };
            fileList = CommonHelper.CastValueToList(newRowOfTestData["Name"]);
            jobData = new JobData()
            {
                Name = "QA_RT_Job_Automation",
                Community = "456-QA_RT_Community1_Automation",
                House = "456-QA_RT_House1_Automation",
                Lot = "QA_RT_Lot1_Automation - Available",
                Orientation = "None"
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Open JOBS/ALL JOBS.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Create a new Job.</b></font>");
                JobPage.Instance.CreateJob(jobData);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"The Job {jobData.Name} is exited");
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
            }
        }
        [Test]
        [Category("Section_IV")]
        public void C01_D_Jobs_DetailPages_AllJobs_Documents()
        {
            // Step 1: Navigate to Jobs menu > All Jobs
            Assert.That(JobDetailPage.Instance.IsHeaderBreadcrumbsCorrect(jobData.Name));

            //Step 2: In Job Side Navigation, open the Documents
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: In Job Side Navigation, open the Document.</b></font>");
            JobDetailPage.Instance.LeftMenuNavigation("Documents");

            // Verify opened successfully the Document page
            Assert.That(DocumentPage.Instance.IsDocumentsPageDisplay(), "Opened unsuccessfully the Document page.");
            ExtentReportsHelper.LogPass("<font color='green'><b>Opened successfully the Document page.</b></font>");

            //Step 3: Add Document into Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Add Document into Job.</b></font>");
            // Add all files of Document to upload
            ExtentReportsHelper.LogInformation(null,"<font color='lavender'><b>a. Add all files of Document to upload.<b>");
            DocumentPage.Instance.UploadDocumentsAndVerify(fileList[0], fileList[1], fileList[2], fileList[3], fileList[4], fileList[5], fileList[6], fileList[7], fileList[8], fileList[9]);

            //Verify Drag &drop the Documents. (Can not implement automation)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>b. Verify Drag &drop the Documents. (Can not implement automation).</b>");

            // The filter the newly created item
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>c. The filter the newly created item.<b>");
            DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.Contains, fileList[0]);
            System.Threading.Thread.Sleep(3000);

            // Verify filter successfully
            if (DocumentPage.Instance.IsItemInGridOption("Name", fileList[0]) is true && DocumentPage.Instance.IsNumberItemInGrid(1) is true)
            {
                ExtentReportsHelper.LogPass($"The document <font color='green'><b>{fileList[0]}</b></font> file filtered successfully.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"The document <font color='red'>{fileList[0]}</font> file filtered unsuccessfully.");
            }

            // Step 4: Update Document Information
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Step 4: Update Document Information</b></font>.");
            DocumentPage.Instance.EditAndVerifyDocumentFile(fileList[0], newData.Description);

            // Step 5. Click the Document; verify the Document is downloaded
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Step 5: Click the Document; verify the hyperlink.</b></font>");
            DocumentPage.Instance.DownloadFile(DocumentPage.Instance.IsFileHref(fileList[0]), pathReportFolder + fileList[0]);
            System.Threading.Thread.Sleep(3000);
            if (File.Exists(pathReportFolder + fileList[0]))
            {
                ExtentReportsHelper.LogPass($"The document <font color='green'><b>{fileList[0]}</b></font> file is downloaded.");
            }
            else
                ExtentReportsHelper.LogFail($"The document <font color='red'>{fileList[0]}</font> file download unsuccessfully.");

            // Step 6: Delete item in Grid
            ExtentReportsHelper.LogInformation("<font color='lavender'><b>Step 6: Delete item in Grid.</b></font>");
            DocumentPage.Instance.DeleteDocumentFile(fileList[0]);
        }

        [TearDown]
        public void DeleteData()
        {
            // clear filter
            DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.NotIsEmpty, "");
            System.Threading.Thread.Sleep(5000);

            ExtentReportsHelper.LogInformation("<font color='lavender'><b>============<b>Clear Data</b>============.<b></font>");
            DocumentPage.Instance.DeleteDocumentFile(fileList[1], fileList[2], fileList[3], fileList[4], fileList[5], fileList[6], fileList[8], fileList[9]);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Delete a new Job.<b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData.Name);
            JobPage.Instance.WaitGridLoad();
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name))
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }
        }
    }
}


