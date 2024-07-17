using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Integrations;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.Budget;
using Pipeline.Testing.Pages.Jobs.Job.Estimates;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Jobs.Job.Quantities;
using Pipeline.Testing.Pages.Jobs.Job.SchedulingTask;
using Pipeline.Testing.Pages.Purchasing.Trades;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor;
using Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments;
using Pipeline.Testing.Pages.Settings.Purchasing;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_X
{
    public class UAT_HOTFIX_PIPE_48018 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_X);
        }

        private JobData jobdata;
        private SeriesData seriesData;
        private HouseData houseData;
        private CommunityData communityData;
        private DivisionData newDivision;
  
        private const string newDivisionName = "RT_QA_New_Division_48018";


        [SetUp]
        public void SetupTestData()
        {

            newDivision = new DivisionData()
            {
                Name = newDivisionName
            };

            //create new series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Series_48018",
                Code = "48018",
                Description = "RT_QA_Series_48018"
            };
            //create new house
            houseData = new HouseData()
            {
                PlanNumber = "8018",
                HouseName = "RT_QA_House_48018",
                SaleHouseName = "RT_QA_House_48018",
                Series = "RT_QA_Series_48018"
            };
            //create new community
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_48018",
                Code = "RT_QA_Community_48018"
            };
        
            jobdata = new JobData()
            {
                Name = "RT_QA_Job_48018",
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Lot = "RT_QA_Lot_48018"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is false)
            {
                DivisionPage.Instance.CreateDivision(newDivision);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Series test data.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, seriesData.Name);
            System.Threading.Thread.Sleep(5000);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                SeriesPage.Instance.CreateSeries(seriesData);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new House test data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is false)
            {
                HousePage.Instance.CreateHouse(houseData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(communityData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5: Add new community to new division.</b></font>");
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.EqualTo, newDivisionName);
            System.Threading.Thread.Sleep(2000);
            if (DivisionPage.Instance.IsItemInGrid("Division", newDivisionName) is true)
            {
                DivisionPage.Instance.SelectItemInGrid("Division", newDivisionName);
                DivisionDetailPage.Instance.LeftMenuNavigation("Communities", true);

                DivisionCommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
                System.Threading.Thread.Sleep(2000);
                if (DivisionCommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
                {
                    string[] communities = { communityData.Name };
                    DivisionCommunityPage.Instance.OpenDivisionCommunityModal();
                    DivisionCommunityPage.Instance.DivisionCommunityModal.SelectDivisionCommunity(communities);
                    DivisionCommunityPage.Instance.DivisionCommunityModal.Save();
                }
            }

            //add house to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6 Add new House to Community.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, houseData.HouseName);
            System.Threading.Thread.Sleep(5000);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Communities");
                HouseCommunities.Instance.FillterOnGrid("Name", communityData.Name);
                System.Threading.Thread.Sleep(5000);
                if (HouseCommunities.Instance.IsValueOnGrid("Name", communityData.Name) is false)
                {
                    HouseCommunities.Instance.AddButtonCommunities();
                    HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(communityData.Name, 0);
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Community with Name {communityData.Name} is displayed in grid");
                }
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.7: Add new Job test data.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is false)
            {
                JobPage.Instance.CreateJob(jobdata);
            }


        }
        [Test]
        public void UAT_HOTFIX_Jobs_Scheduling_Task()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to the Job Scheduling Page.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobdata.Name);
                System.Threading.Thread.Sleep(5000);
                try
                {
                    JobDetailPage.Instance.LeftMenuNavigation("Scheduling Task", true);
                    var param1 = new
                    {
                        JobScheduleItems_ActivityId = 622,
                        JobScheduleItems_ActivityCode = "1",
                        JobScheduleItems_ActivityName = "Start Up",
                        JobScheduleItems_ActivitiesDescription = "Start Up",
                        JobScheduleItems_Activities_Sequence = "1",
                        JobScheduleItems_ScheduledStartDate = "/Date(-2208988800000)/",
                        JobScheduleItems_ScheduledEndDate = "/Date(-2208988800000)/",
                        JobScheduleItems_ActualStartDate = "/Date(-2208988800000)/",
                        JobScheduleItems_ActualEndDate = "/Date(-2208988800000)/",
                        JobScheduleItems_IsComplete = false
                    };

                    var param2 = new
                    {
                        JobScheduleItems_ActivityId = 660,
                        JobScheduleItems_ActivityCode = "39",
                        JobScheduleItems_ActivityName = "PO Release #1",
                        JobScheduleItems_ActivitiesDescription = "PO Release #1",
                        JobScheduleItems_Activities_ParentCode = "1",
                        JobScheduleItems_Activities_Sequence = "1",
                        JobScheduleItems_ScheduledStartDate = "/Date(1708409759000)/",
                        JobScheduleItems_ScheduledEndDate = "/Date(1708409759000)/",
                        JobScheduleItems_ActualStartDate = "/Date(1708409759000)/",
                        JobScheduleItems_ActualEndDate = "/Date(1708409759000)/",
                        JobScheduleItems_IsComplete = true
                    };

                    var param3 = new
                    {
                        JobScheduleItems_ActivityId = 659,
                        JobScheduleItems_ActivityCode = "38",
                        JobScheduleItems_ActivityName = "Start of Job",
                        JobScheduleItems_ActivitiesDescription = "Start of Job",
                        JobScheduleItems_Activities_ParentCode = "1",
                        JobScheduleItems_Activities_Sequence = "1",
                        JobScheduleItems_ScheduledStartDate = "/Date(1708409759000)/",
                        JobScheduleItems_ScheduledEndDate = "/Date(1708409759000)/",
                        JobScheduleItems_ActualStartDate = "/Date(-2208988800000)/",
                        JobScheduleItems_ActualEndDate = "/Date(-2208988800000)/",
                        JobScheduleItems_IsComplete = false
                    };


                    List<object> parameters = new List<object>();
                    parameters.Add(param1);
                    parameters.Add(param2);
                    parameters.Add(param3);

                    string apiMethodCreateTasks = "rest/" + BasePage.BasePath + "/Scheduling/CreateTaskForScheduleByJobNumberMultiple/" + jobdata.Name + "?Signature=6E21C7CB-A7B2-4777-8D9A-4990062134C3";
                    string apiMethodCreateSchedule = "rest/" + BasePage.BasePath + "/Scheduling/CreateSchedule?Signature=6E21C7CB-A7B2-4777-8D9A-4990062134C3";
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Setup Scheduled Task test data via Host API Call.</b></font>");

                    var schedule = new
                    {
                        JobNumber = jobdata.Name,
                        JobSchedule_CreatedDate = "/Date(1708409759000)/",
                        JobSchedule_StartedDate = "/Date(-2208988800000)/",
                        JobSchedule_CompletedDate = "/Date(-2208988800000)/"
                    };

                    ApiConnection.CreateSchedule(BasePage.BaseApiUrl, apiMethodCreateSchedule, schedule);
                    ApiConnection.CreateTaskForScheduleByJobNumberMultiple(BasePage.BaseApiUrl, apiMethodCreateTasks, parameters);
                    CommonHelper.RefreshPage();
                    CommonHelper.CaptureScreen();

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Click the collapse all button to show all the data in the grid.</b></font>");
                    JobSchedulingTaskPage.Instance.CollapseAllGrid();

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Verify the expected columns are displayed.</b></font>");
                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInMilestoneGrid("Task ID"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Task ID column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Task ID column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInMilestoneGrid("Milestones"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Milestones column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Milestones column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInDetailGrid("Task Code"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Task Code column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Task Code column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInDetailGrid("Scheduling Task Name"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Scheduling Task Name column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Scheduling Task Name column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInDetailGrid("Building Phases"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Building Phases column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Building Phases column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInDetailGrid("Scheduled Start Date"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Scheduled Start Date column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Scheduled Start Date column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInDetailGrid("Scheduled End Date"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Scheduled End Date column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Scheduled End Date column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInDetailGrid("Actual Start Date"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Actual Start Date column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Actual Start Date column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInDetailGrid("Actual End Date"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Actual End Date column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Actual End Date column is Notfound on the grid.</b></font>");

                    if (JobSchedulingTaskPage.Instance.IsColumnFoundInDetailGrid("Completed"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Completed column is found on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Completed column is Notfound on the grid.</b></font>");


                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Verify the scheduling tasks displayed in the grid.</b></font>");
                    if (JobSchedulingTaskPage.Instance.IsItemInMilestoneGrid("Milestones", "Start Up"))
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>The expected milestone is displayed on the grid.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>The expected milestone is not displayed on the grid.</b></font>");

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Verify if the scheduling tasks data are expected values.</b></font>");

                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Scheduling Task Name", "PO Release #1", 1, 2);
                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Scheduling Task Name", "Start of Job", 2, 2);

                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Scheduled Start Date", "2/20/2024", 1, 4);
                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Scheduled Start Date", "2/20/2024", 2, 4);

                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Scheduled End Date", "2/20/2024", 1, 5);
                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Scheduled End Date", "2/20/2024", 2, 5);

                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Actual Start Date", "2/20/2024", 1, 6);
                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Actual Start Date", "--", 2, 6);

                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Actual End Date", "2/20/2024", 1, 7);
                    JobSchedulingTaskPage.Instance.ValidateScheduledTask("Actual End Date", "--", 2, 7);
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    Assert.Inconclusive($"Schedule Task not found for Job- make sure the Scheduling module is turned on. Marking test inconclusive.");
                }
            }
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Delete Job test data.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobdata.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobdata.Name) is true)
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", jobdata.Name);
            }
        }
    }
}
