using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Integrations;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Jobs.Job.Options;
using Pipeline.Testing.Pages.Settings.Specitup;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class C01_B_PIPE_18715 : BaseTestScript
    {

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private JobData jobData;
        HouseData HouseData;
        CommunityData communityData;
        LotData _lotdata;

        string[] selectedOption;
        string[] selectedCustomOption;

        private readonly string COMMUNITY_NAME_DEFAULT = "HN-QA-Community";
        private readonly int[] indexs = new int[] { };
        private string importFileDir;

        private const string OPTION = "OPTION";
        private const string CUSTOM_OPTION = "CUSTOM OPTION";
        private const string VIEW_OPT_FOR_CHOSEN_CONFIG = "View Options Only for Chosen Configuration";
        private const string VIEW_SUM_OF_OPT_UP_TO_CHOOSE_CONFIG = "View Sum of Options up to Chosen Configuration.";

        // Option OPTION_CODE_DEFAULT_1 has name 'QA_RT_Option_Automation_01'
        private const string OPTION_CODE_DEFAULT_1 = "QA_RT_Au_Opt_01";
        // Option OPTION_CODE_DEFAULT_2 has name 'QA_RT_Option_Automation_02'
        private const string OPTION_CODE_DEFAULT_2 = "QA_RT_Au_Opt_02";

        // Custom Option CUSTOM_OPTION_CODE_DEFAULT_1 has name 'QA_RT_Custom_Option_Automation_01'
        private const string CUSTOM_OPTION_CODE_DEFAULT_1 = "QA_RT_Cus_Opt_01";
        // Custom Option CUSTOM_OPTION_CODE_DEFAULT_2 has name 'QA_RT_Custom_Option_Automation_02'
        private const string CUSTOM_OPTION_CODE_DEFAULT_2 = "QA_RT_Cus_Opt_02";

        private const string selectionOption = "TYRR1";
        private const string selectionField = "DoorHardwareSelection";
        private const string selectionValue = "3910SN";
        private const string selectionSku = "S123";
        private const string selectionBrandName = "Brand 123";
        private const string selectionImgUrl = "https://fastly.picsum.photos/id/9/5000/3269.jpg?hmac=cZKbaLeduq7rNB8X-bigYO8bvPIWtT-mh8GRXtU3vPc";
        private const string selectionNotes = "Test Notes";

        #region"Set up data"

        [SetUp]
        public void SetUpData()
        {
            jobData = new JobData()
            {
                Name = "QA_RT_Job_Option_Approve_Config",
                Community = "_001-HN-QA-Community",
                House = "_001-HN-QA-House-01",
                Lot = "_001 - Sold",
                Orientation = "Left",
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
                Status = "Sold"
            };

            selectedOption = new string[] {
                $"{OPTION_CODE_DEFAULT_1}",
                $"{OPTION_CODE_DEFAULT_2}"
            };

            selectedCustomOption = new string[] {
                $"{CUSTOM_OPTION_CODE_DEFAULT_1}",
                $"{CUSTOM_OPTION_CODE_DEFAULT_2}"
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

            // Step 1.1: Navigate to Jobs > All Jobs and filter job 'QA_RT_Job_Option_Approve_Config' and delete if it's existing
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.1: Navigate to Jobs > All Jobs and filter job '{jobData.Name}' and delete if it's existing.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            JobPage.Instance.EnterJobNameToFilter("Job Number", jobData.Name);
            JobPage.Instance.WaitGridLoad();

            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name))
            {
                JobPage.Instance.DeleteJob(jobData.Name);
            }

            // Step 1.2: Create a new job with name 'RT-QA_JOB_Import_Quantity' 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Create a new job with name 'RT-QA_JOB_Import_Quantity'.</b></font>");
            JobPage.Instance.CreateJob(jobData);
        }

        #endregion

        #region"Test case"

        [Test]
        [Category("Section_IV")]
        public void C01_B_Jobs_DetailPages_AllJobs_Options()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Open Option page from left navigation and approved the configuration.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Options", true);

                // Approve current config that contain a option BASE on the grid view adding by DEFAULT
                JobOptionPage.Instance.ClickApproveConfig();

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Go to Specitup Settings and set to RUNNING.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Specitup");
            SpecitupPage.Instance.RunningSpecitup(true);
            SpecitupPage.Instance.SaveSetting();
            CommonHelper.CaptureScreen();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Verify availability of Add Config button.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Options", true);

                if (!JobOptionPage.Instance.IsAddConfigEnabled)
                    ExtentReportsHelper.LogPass("<font color='green'>Add Config button is disabled.</font>");
                else
                    ExtentReportsHelper.LogFail("<font color='red'>Add Config button is enabled.</font>");
                CommonHelper.CaptureScreen();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Go to Specitup Settings and set to PAUSED.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Specitup");
            SpecitupPage.Instance.RunningSpecitup(false);
            SpecitupPage.Instance.SaveSetting();
            CommonHelper.CaptureScreen();

            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Options", true);

                if (JobOptionPage.Instance.IsAddConfigEnabled)
                    ExtentReportsHelper.LogPass("<font color='green'>Add Config button is enabled.</font>");
                else
                    ExtentReportsHelper.LogFail("<font color='red'>Add Config button is disabled.</font>");

            }

            // Step 2: Open Option page from left navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open Option page from left navigation.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);
                JobDetailPage.Instance.LeftMenuNavigation("Options", true);

                if (JobOptionPage.Instance.IsOptionCardDisplayed is false)
                    ExtentReportsHelper.LogFail("<font color='red'>Job > Option page doesn't display or title is incorrect.</font>");
                ExtentReportsHelper.LogPass($"<font color='green'>Job > Option page displays correctly with URL: <b>{JobOptionPage.Instance.CurrentURL}.</b></font>");

                // Step 3: Add a new configuation
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Add a new configuation.</b></font>");
                JobOptionPage.Instance.AddNewConfiguration();

                // Step 4.1: Add 2 Options to job.
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.1: Add 2 Options to job.</b></font>");
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(OPTION, selectedOption);

                // Step 4.2: Add 2 Custom Options to job.
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.2: Add 2 Custom Options to job.</b></font>");
                JobOptionPage.Instance.AddOptionOrCustomOptionToJob(CUSTOM_OPTION, selectedCustomOption);

                // Step 4.3: Filter Option on the grid view and delete the first one.
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.3: Filter Option on the grid view and delete the first one.</b></font>");

                foreach (string optNumber in selectedOption)
                {
                    JobOptionPage.Instance.FilterItemInGrid(OPTION, "Option Number", GridFilterOperator.EqualTo, optNumber);
                    if (JobOptionPage.Instance.IsItemInGrid(OPTION, "Option Number", optNumber) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>Option with code: <b>{optNumber}</b> added to job successfully and displayed on the grid view.</font>");
                    else
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with code: <b>{optNumber}</b> on the grid view.</font>");
                }
                // Clear filter
                JobOptionPage.Instance.FilterItemInGrid(OPTION, "Option Number", GridFilterOperator.NoFilter, string.Empty);
                JobOptionPage.Instance.RemoveOptionOrCustomOptionFromJob(OPTION, "Option Number", selectedOption[0]);

                // Step 4.4: Filter Custom Option on the grid view and delete the first one.
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.4: Filter Custom Option on the grid view and delete the first one.</b></font>");
                foreach (string optNumber in selectedCustomOption)
                {
                    JobOptionPage.Instance.FilterItemInGrid(CUSTOM_OPTION, "Code", GridFilterOperator.EqualTo, optNumber);
                    if (JobOptionPage.Instance.IsItemInGrid(CUSTOM_OPTION, "Code", optNumber) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>Custom Option with code: <b>{optNumber}</b> added to job successfully and displayed on the grid view.</font>");
                    else
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find Custom Option with code: <b>{optNumber}</b> on the grid view.</font>");
                }
                // Clear filter
                JobOptionPage.Instance.FilterItemInGrid(CUSTOM_OPTION, "Code", GridFilterOperator.NoFilter, string.Empty);
                JobOptionPage.Instance.RemoveOptionOrCustomOptionFromJob(CUSTOM_OPTION, "Code", selectedCustomOption[0]);

                // Step 5.1: Approve config.
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1: Approve config with 1 Option and 1 Custom Opt.</b></font>");
                JobOptionPage.Instance.ClickApproveConfig();
                string expectedConfig = JobOptionPage.Instance.GetCurrentConfigurationNumber();
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                var userDateTime = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZone);
                string expectedDateTime = userDateTime.ToString("MM/dd/yy");

                // Step 5.2: Back to Job detail page and verify new config is displayed
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2: Back to Job detail page and verify new config is displayed.</b></font>");
                JobPage.Instance.LeftMenuNavigation("Details", false);
                JobPage.Instance.VerifyNewConfigOnJobDetailPage(expectedConfig, expectedDateTime);

                // Back to Job Option page
                JobOptionPage.Instance.LeftMenuNavigation("Options", false);

                /*
                 * On Options grid, after creating a new job with selected house and community, Options 'BASE' is added by DEFAULT.
                 * At config 1: Approve config. Current Opt: 1, current Cus Opt: 0
                 * At config 2: 2 Options and 2 Custom Options added to current grid at step 4.1. Current Opt: 2, current Cus Opt: 2
                 * At config 2: 1 Option and 1 Custom Opt are removed from current grid at step 4.3. Current Opt: 1, current Cus Opt: 1
                 */

                // Step 5.3: Verify 'View Option Only for Chosen Configuration' view. Expectation: Display 2 Options and 1 Custom Option on the grid view
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3: Verify 'View Option Only for Chosen Configuration' view. Expectation: Display 2 Options and 1 Custom Option on the grid view.</b></font>");
                // It means, on the UI, it will show data of 'View Sum of Options to Chosen Configuration'
                int expectedOptionNum = 2; // Contain opt BASE and QA_RT_Au_Opt_02
                int expectedCusOptNum = 1; // Contain cus opt QA_RT_Cus_Opt_02
                JobOptionPage.Instance.SwitchOptionView(VIEW_OPT_FOR_CHOSEN_CONFIG, expectedOptionNum, expectedCusOptNum);

                // Step 5.4: Verify 'View Sum of Options to Chosen Configuration' view. Expectation: Display 2 Options and 2 Custom Options on the grid view
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.4: Verify 'View Sum of Options to Chosen Configuration' view. Expectation: Display 2 Options and 2 Custom Options on the grid view.</b></font>");
                // It means, on the UI, it will show data of 'View Options Only for Chosen Configuration'
                expectedOptionNum = 2; // contain opt QA_RT_Au_Opt_01 and QA_RT_Au_Opt_02
                expectedCusOptNum = 2; // Contain cus QA_RT_Au_Opt_01 and opt QA_RT_Cus_Opt_02
                JobOptionPage.Instance.SwitchOptionView(VIEW_SUM_OF_OPT_UP_TO_CHOOSE_CONFIG, expectedOptionNum, expectedCusOptNum);

                // Step 5.5: Select first config and verify it
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.5: Select first config and verify it.</b></font>");
                expectedOptionNum = 1; // Contain Base opt only
                expectedCusOptNum = 0; // Still not added cus opt
                JobOptionPage.Instance.UpdateCurrentConfigurationNumber(1, expectedOptionNum, expectedCusOptNum);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.6: Setup Notes and Selections data via Host API used by SIU.</b></font>");
                string apiMethod = "rest/" + BasePage.BasePath + "/jobs/AddUpdateJobConfiguration/" + jobData.Name + "/1?Signature=6E21C7CB-A7B2-4777-8D9A-4990062134C3";

                var JobConfigOptions = new[]
                {
                        new
                        {
                            StructuralOption = new
                            {
                                Number = selectionOption
                            },
                            Quantity = 1,
                            Notes = selectionNotes,
                            Selections = new [] {
                                new
                                {
                                    Field = selectionField,
                                    Value= selectionValue,
                                    SKU=selectionSku,
                                    BrandName=selectionBrandName,
                                    ImageUrl=selectionImgUrl
                                }
                            }
                        }
                    };

                ApiConnection.AddUpdateJobConfigOptions(BasePage.BaseApiUrl, apiMethod, JobConfigOptions);
                CommonHelper.RefreshPage();
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.7: Verify notes and selections data are displayed in the drawer component.</b></font>");

                JobOptionPage.Instance.ViewNotesAndSelections(selectionNotes, selectionField, selectionValue, selectionSku, selectionBrandName, selectionImgUrl);
            }


        }

        #endregion

        [TearDown]
        public void DeleteData()
        {
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: Back to Job default page and delete current job.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", jobData.Name);
            }
        }
    }
}

