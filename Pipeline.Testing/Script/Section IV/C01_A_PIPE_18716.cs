using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class C01_A_PIPE_18716 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private JobData jobData;
        private JobData jobData_Update;
        HouseData HouseData;
        CommunityData communityData;
        LotData _lotdata;

        private readonly string COMMUNITY_NAME_DEFAULT = "HN-QA-Community";
        private readonly int[] indexs = new int[] { };
        private string importFileDir;

        private const string JOB_CLOSE = "CLOSE JOB";
        private const string JOB_OPEN = "OPEN JOB";

        private IList<string> dirArrayImage = new List<string>();
        readonly string sourcePathImage = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\DataInputFiles\Resources\";
        private readonly IList<string> listNameImage = new List<string>();

        #region"Set up data"

        [SetUp]
        public void SetUpData()
        {
            jobData = new JobData()
            {
                Name = "QA_RT_Job_Details",
                Community = "_001-HN-QA-Community",
                House = "_001-HN-QA-House-01",
                Lot = "_001 - Sold",
                Draft = "NONE",
                Orientation = "Left",
            };

            jobData_Update = new JobData()
            {
                Name = "QA_RT_Job_Details_Update",
                Community = "_001-HN-QA-Community",
                House = "_001-HN-QA-House-01",
                Lot = "_001 - Sold",
                Draft = "Other",
                Orientation = "Right",
            };

            communityData = new CommunityData()
            {
                Name = "HN-QA-Community",
                Division = "None",
                Code = "_001",
                City = "Ho Chi Minh",
                CityLink = "https://hcm.com",
                Township = "Ho Chi Minh",
                County = "Texas",
                State = "TX",
                Zip = "70000",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Community from automation test v1",
                Slug = "HN-QA-Community",
            };

            HouseData = new HouseData()
            {
                HouseName = "HN-QA-House-01",
                SaleHouseName = "The house which created by Hai Nguyen",
                Series = "Hai Nguyen Series",
                PlanNumber = "_001",
                BasePrice = "1000000.00",
                SQFTBasement = "200",
                SQFTFloor1 = "200",
                SQFTFloor2 = "200",
                SQFTHeated = "0",
                SQFTTotal = "0",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "Hai Nguyen create house - testing"
            };

            _lotdata = new LotData()
            {
                Number = "_001",
                Status = "Sold"
            };

            // Get list images
            dirArrayImage = Directory.GetFiles(sourcePathImage);
            foreach (var nameDoc in dirArrayImage)
            {
                // Accept GIF, JPG, JPEG, JFIF, SVG, or PNG only.
                if (nameDoc.ToLower().EndsWith("gif")
                    || nameDoc.ToLower().EndsWith(".jpg")
                    || nameDoc.ToLower().EndsWith(".jpeg")
                    || nameDoc.ToLower().EndsWith(".jfif")
                    || nameDoc.ToLower().EndsWith(".png"))
                    listNameImage.Add(nameDoc);
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Turn OFF Sage 300 and MS NAV.<b></b></font>");
            CommunityPage.Instance.SetSage300AndNAVStatus(false);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.2: Open Lot page, verify Lot button displays or not.<b></b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Try to open Lot URL of any community and verify Add lot button
            CommonHelper.OpenURL(BaseDashboardUrl + "/Builder/Communities/Lots.aspx?cid=1");
            if (LotPage.Instance.IsAddLotButtonDisplay() is false)
            {
                ExtentReportsHelper.LogWarning(null, $"<font color='lavender'><b>Add lot button doesn't display to continue testing. Stop this test script.</b></font>");
                Assert.Ignore("Add lot button doesn't display after set NAV and Sage 300 to Running. Stop this test script");
            }

            //Navigate To Community Page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Community default page.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter community with name {communityData.Name} and create if it doesn't exist.</b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (!CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);
            }
            else
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
            }

            //Naviage To Community Lot
            CommunityDetailPage.Instance.LeftMenuNavigation("Lots");
            string LotPageUrl = LotPage.Instance.CurrentURL;
            if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
            {
                ExtentReportsHelper.LogInformation($"Lot with Number {_lotdata.Number} and Status {_lotdata.Status} is displayed in grid");
            }
            else
            {
                //Import Lot in Community
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Open Import page.</b></font>");
                CommonHelper.SwitchLastestTab();
                LotPage.Instance.ImportExporFromMoreMenu("Import");
                importFileDir = "Pipeline_Lots_In_Community.csv";
                LotPage.Instance.ImportFile("Lot Import", $@"\DataInputFiles\Assets\Community\{importFileDir}");
                CommonHelper.OpenURL(LotPageUrl);

                //Check Lot Numbet in grid 
                if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
                {
                    ExtentReportsHelper.LogPass("Import Lot File is successful");
                }
                else
                {
                    ExtentReportsHelper.LogFail("Import Lot File is unsuccessful");
                }
            }

            //Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House default page.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {HouseData.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HouseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", HouseData.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(HouseData);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"House with Name {HouseData.HouseName} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HouseData.HouseName);

            }
            //Navigate House Communities And Check Community on grid
            HouseDetailPage.Instance.LeftMenuNavigation("Communities");
            HouseCommunities.Instance.FillterOnGrid("Name", COMMUNITY_NAME_DEFAULT);
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT, indexs);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Community with Name {COMMUNITY_NAME_DEFAULT} is displayed in grid");
            }

            // Step 1.1: Navigate to Jobs > All Jobs and filter job 'QA_RT_Job_Option_Approve_Config' and delete if it's existing
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.1 Navigate to Jobs > All Jobs, filter job '{jobData.Name}' and create if it's NOT existing.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData_Update.Name);
            JobPage.Instance.WaitGridLoad();

            if (JobPage.Instance.IsItemInGrid("Job Number", jobData_Update.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete this job before updating to the same one.</b></font>");
                JobPage.Instance.DeleteJob(jobData_Update.Name);
            }

            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData.Name);
            JobPage.Instance.WaitGridLoad();

            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Create a new job with name '{jobData.Name}'.</b></font>");
                JobPage.Instance.CreateJob(jobData);
            }
            else
            {
                // Step 1.2: Open Job detail page
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Open Job detail page.</b></font>");
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
            }
        }

        #endregion

        #region"Test case"

        [Test]
        [Category("Section_IV")]
        public void C01_A_Jobs_DetailPages_AllJobs_Details()
        {
            // Step 2: Update job on the detail page
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2: Edit job on the detail page.</b></font>");
            JobDetailPage.Instance.UpdateJobOnDetailPage(jobData_Update);

            // Step 3.1: Switch current job to status 'Close'
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.1: Switch current job to status 'Close'.</b></font>");
            JobDetailPage.Instance.OpenOrCloseJob(JOB_OPEN);

            // Step 3.2: Switch current job to status 'Open'
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3.2: Switch current job to status 'Open'.</b></font>");
            JobDetailPage.Instance.OpenOrCloseJob(JOB_CLOSE);

            // Step 4: Upload Job image
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4: Upload Job image.</b></font>");
            JobDetailPage.Instance.UploadCommununityAndVerify(listNameImage.ToArray());

            // Step 5.1: Navigate to Option page, approve config 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.1: Navigate to Option page, approve config.</b></font>");
            JobOptionPage.Instance.LeftMenuNavigation("Options", false);
            if (JobOptionPage.Instance.IsOptionCardDisplayed is false)
                ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
            ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");

            // Approve current config that contain a option BASE on the grid view adding by DEFAULT
            JobOptionPage.Instance.ClickApproveConfig();

            string expectedConfig = JobOptionPage.Instance.GetCurrentConfigurationNumber();
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var userDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZone);
            string expectedDateTime = userDateTime.ToString("MM/dd/yy");

            // Step 5.2: Back to Job detail page and verify new config
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5.2: Back to Job detail page and verify new config.</b></font>");
            JobPage.Instance.LeftMenuNavigation("Details", false);
            JobPage.Instance.VerifyNewConfigOnJobDetailPage(expectedConfig, expectedDateTime);

            // Before testing step 6, back to Job Option page, add and approve 3 configs
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Before testing step 6, back to Job Option page, add and approve 3 configs.</b></font>");

            JobOptionPage.Instance.LeftMenuNavigation("Options", false);
            // Config 2
            JobOptionPage.Instance.AddNewConfiguration();
            JobOptionPage.Instance.ClickApproveConfig();

            // Config 3
            JobOptionPage.Instance.AddNewConfiguration();
            JobOptionPage.Instance.ClickApproveConfig();

            // Update the current number of config
            expectedConfig = JobOptionPage.Instance.GetCurrentConfigurationNumber();

            // Back to Job Detail page, and continue testing step 6
            JobPage.Instance.LeftMenuNavigation("Details", false);

            // Step 6.1: On Job detail page, click View First and Last Configs button (Current is View All Configs mode)
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.1: On Job detail page, click View First and Last Configs button.</b></font>");
            JobDetailPage.Instance.ViewFistAndLastConfigs(expectedConfig);

            // Step 6.2: On Job detail page, click View All Configs button (Current is View First and Last Configs mode)
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 6.2: On Job detail page, click View All Configs button.</b></font>");
            JobDetailPage.Instance.ViewAllConfigs(expectedConfig);
        }

        #endregion

        [TearDown]
        public void DeleteData()
        {
            // Back to Job default page and delete it
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData_Update.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData_Update.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 7: Delete  job '{jobData_Update.Name}'.</b></font>");
                JobPage.Instance.DeleteJob(jobData_Update.Name);
            }

            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Delete this job '{jobData.Name}'.</b></font>");
                JobPage.Instance.DeleteJob(jobData.Name);
            }
        }
    }
}

