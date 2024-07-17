using LinqToExcel;
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

namespace Pipeline.Testing.Script.Section_III
{
    public partial class C01_RT_01088 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        JobData _jobdata;
        HouseData HouseData;
        CommunityData communityData;
        LotData _lotdata;

        private readonly string COMMUNITY_NAME_DEFAULT = "HN-QA-Community";
        private string importFileDir;
        private readonly int[] indexs = new int[] { };

        [SetUp]
        public void GetTestData()
        {
            _jobdata = new JobData()
            {
                Name = "RT_Job-Automation",
                Community = "_001-HN-QA-Community",
                House = "_001-HN-QA-House-01",
                Lot = "_001 - Sold",
                Orientation = "Left",
                Customer = "lam.tran.ssg@gmail.com"
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

            _lotdata = new LotData()
            {
                Number = "_001",
                Status= "Sold"
            };

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

        }
        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void C01_Jobs_AllJob_AddAJob()
        {

            // Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/Jobs/Jobs/Default.aspx
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            // Step 2 - 3: Populate all values
           
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", _jobdata.Name);
            if(JobPage.Instance.IsItemInGrid("Job Number", _jobdata.Name) is true)
            {
                ExtentReportsHelper.LogInformation($"Delete { _jobdata.Name} is exited in grid");
                JobPage.Instance.DeleteItemInGrid("Job Number", _jobdata.Name);
                JobPage.Instance.CreateJob(_jobdata);
            }
            else
            {
                JobPage.Instance.CreateJob(_jobdata);
            }

            // Step 4 - 5. Create a Duplicate Job. Back to Job default page
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.CreateJob(_jobdata, false);

            // Step 6. Back to list of Job and verify new item in grid view
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            // Insert name to filter and click filter by Contain value
            //JobPage.Instance.EnterJobNameToFilter("Job Number", TestData["Name"]);
            System.Threading.Thread.Sleep(5000);
            bool isFound = JobPage.Instance.IsItemInGrid("Job Number", _jobdata.Name);
            Assert.That(isFound, string.Format("New Job \"{0}\" was not display on grid.", _jobdata.Name));

            // Step 7. Select item and click deletete icon
            JobPage.Instance.DeleteItemInGrid("Job Number", _jobdata.Name);

            string successfulMess = $"Job {_jobdata.Name} deleted successfully!";
            if (successfulMess == JobPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(successfulMess);
                JobPage.Instance.CloseToastMessage();
            }
            else
            {
                if (JobPage.Instance.IsItemInGrid("Job Number", _jobdata.Name))
                    ExtentReportsHelper.LogFail($"Job {_jobdata.Name} could not be deleted!");
                else
                    ExtentReportsHelper.LogPass(successfulMess);
            }

        }
        #endregion

    }
}
