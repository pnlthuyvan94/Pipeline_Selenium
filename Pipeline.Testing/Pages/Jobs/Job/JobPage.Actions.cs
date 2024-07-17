using NUnit.Framework;
using NUnit.Framework.Legacy;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;

namespace Pipeline.Testing.Pages.Jobs.Job
{
    public partial class JobPage
    {
        public void ClickAddToJobIcon()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            PageLoad();
        }

        public void FilterItemInGrid(string columnName, string value)
        {
            JobPage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 2000);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            JobPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 2000);
        }

        public void FilterByColumnDropDown(string columnName, string value)
        {
            string valueToFind_ListItem;
            if (columnName == "Community")
                valueToFind_ListItem = "//*[contains(@id, 'ddlCommunities_DropDown')]/div/ul";
            else if (columnName == "Series")
                valueToFind_ListItem = "//*[contains(@id, 'ddlSeries_DropDown')]/div/ul";
            else if (columnName == "House")
                valueToFind_ListItem = "//*[contains(@id, 'ddlHouses_DropDown')]/div/ul";
            else
                valueToFind_ListItem = string.Empty;

            JobPage_Grid.FilterByColumnDropDowwn(columnName, valueToFind_ListItem, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 2000);
        }

        public JobPage NavigateToPage(int pageNumber)
        {
            JobPage_Grid.NavigateToPage(pageNumber);
            return this;
        }
        public int GetJobGridPageCount()
        {
            return JobPage_Grid.GetTotalPages;
        }
        public JobPage ChangePageSize(int size)
        {
            JobPage_Grid.ChangePageSize(size);
            return this;
        }
        public bool IsItemInGrid(string columnName, string value)
        {
            return JobPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public JobPage DeleteItemInGrid(string columnName, string value)
        {
            JobPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]");
            return this;
        }

        public JobDetailPage OpenJobDetailPage(string columnName, string jobName)
        {
            JobPage_Grid.ClickItemInGrid(columnName, jobName);
            return this.JobDetailPage;
        }
        public void WaitGridLoad()
        {
            string loading_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]";
            CommonHelper.WaitUntilElementInvisible(loading_Xpath, 10, false);
            //JobPage_Grid.WaitGridLoad();
        }

        public JobPage EnterJobNameToFilter(string columnName, string name)
        {
            JobPage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, name);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 1000);
            return this;
        }

        public void GoToDetailPageOfThe1stJob()
        {
            JobPage_Grid.ClickItemInGrid(0, 1);
            PageLoad();
        }

        public void CreateJob(JobData jobData, bool isExpectCreateSuccessfully = true)
        {

            // Step 2: click on "+" Add button
            JobPage.Instance.ClickAddToJobIcon();
            string expectedURL = "/jobs/job.aspx?jid=0";
            Assert.That(CommonHelper.GetCurrentDriverURL.ToLower().EndsWith(expectedURL), "Job detail page isn't displayed");

            // 4. Select the 'Save' button on the modal;
            JobData getnewjobData = JobDetailPage.Instance.CreateAJob(jobData);

            if (isExpectCreateSuccessfully)
            {
                // Expectation: Create job successfully
                Assert.That(JobDetailPage.Instance.IsCreateSuccessfully(getnewjobData), "Create new Job unsuccessfully");
                ExtentReportsHelper.LogPass("Create successful Job");
            }
            else
            {
                // Expectation: Failed to create job
                ClassicAssert.AreEqual("Job Name Already Exists.", JobPage.Instance.GetLastestToastMessage(), "The delete message is not as expected.");
                ExtentReportsHelper.LogPass("Cannot create a duplicate job name!");
                JobPage.Instance.CloseToastMessage();
            }
        }
        
        public void DeleteJob(string jobNumber)
        {
            DeleteItemInGrid("Job Number", jobNumber);

            string successfulMess = $"Job {jobNumber} deleted successfully!";
            if (successfulMess == JobPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(successfulMess);
                JobPage.Instance.CloseToastMessage();
            }
            else
            {
                if (JobPage.Instance.IsItemInGrid("Job Number", jobNumber))
                    ExtentReportsHelper.LogFail($"Job {jobNumber} could not be deleted!");
                else
                    ExtentReportsHelper.LogPass(successfulMess);
            }
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            JobPage_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }
    }
}
