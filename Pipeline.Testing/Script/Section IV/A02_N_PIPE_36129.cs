
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.Communities.Spec_Sets;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class A02_N_PIPE_36129 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private JobData jobData;
        HouseData HouseData;
        CommunityData communityData;
        LotData _lotdata;
        private SpecSetData SpecSetData;

        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_36129";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community_36129_Automation";

        private const string HOUSE_NAME_DEFAULT = "QA_RT_House_36129_Automation";
        private const string HOUSE_CODE_DEFAULT = "3612";
        readonly string ATTRIBUTE_NAME = "Houses";
        private const string OPTION = "OPTION";
        private static string OPTION_NAME_DEFAULT = "Option_PIPE_36129";
        private static string OPTION_CODE_DEFAULT = "36129";

        private const string JOB_OPEN = "OPEN JOB";

        private readonly string SPECSETGROUP_NAME_DEFAULT = "QA_RT_SpecSetGroup_36129_Automation";


        string[] OptionData = { OPTION_NAME_DEFAULT };

        private readonly int[] indexs = new int[] { };

        private List<string> ListHouse = new List<string> { HOUSE_CODE_DEFAULT+"/"+HOUSE_NAME_DEFAULT };


        private string importFileDir;

        [SetUp]
        public void GetTestData()
        {
            jobData = new JobData()
            {
                Name = "QA_RT_Job10_Automation",
                Community = "Automation_36129-QA_RT_Community_36129_Automation",
                House = "3612-QA_RT_House_36129_Automation",
                Lot = "_111 - Sold",
                Orientation = "Left",
            };


            HouseData = new HouseData()
            {
                HouseName = "QA_RT_House_36129_Automation",
                SaleHouseName = "QA_RT_House_36129_Sales_Name",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "3612",
                BasePrice = "1000000",
                SQFTBasement = "1",
                SQFTFloor1 = "1",
                SQFTFloor2 = "2",
                SQFTHeated = "3",
                SQFTTotal = "7",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "Test"
            };

            communityData = new CommunityData()
            {
                Name = "QA_RT_Community_36129_Automation",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "Automation_36129",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Nothing to say v1",
                DrivingDirections = "Nothing to say v2",
                Slug = "R-QA-Only-Community-Auto"
            };

            _lotdata = new LotData()
            {
                Number = "_111",
                Status = "Sold"
            };

            SpecSetData = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_36129_Automation",
                SpectSetName= "QA_RT_SpecSet_36129_Automation"

            };

        }

        [Test]
        [Category("Section_IV")]
        public void A04_N_Assets_DetailPage_Communities_AvailablePlans_Community_Override_HouseName_does_not_display_in_some_scenarios_instead_it_displays_the_original_HouseName()
        {
            ExtentReportsHelper.LogInformation(null, $"<b>Open setting page, Turn OFF Sage 300 and MS NAV.</b>");
            CommunityPage.Instance.SetSage300AndNAVStatus(false);

            ExtentReportsHelper.LogInformation(null, $"<b>Open Lot page, verify Lot button displays or not.</b>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            //Try to open Lot URL of any community and verify Add lot button
            ExtentReportsHelper.LogInformation(null, $"<b> Try to open Lot URL of any community and verify Add lot button.</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + "/Builder/Communities/Lots.aspx?cid=1");
            if (LotPage.Instance.IsAddLotButtonDisplay() is false)
            {
                ExtentReportsHelper.LogWarning(null, $"<b>Add lot button doesn't display to continue testing. Stop this test script.</b>");
                Assert.Ignore("Add lot button doesn't display after set NAV and Sage 300 to Running. Stop this test script");
            }

            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            //Import Option
            ExtentReportsHelper.LogInformation(null, "<b>Import Option.</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_OPTION);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_IMPORT} grid view to import new Options.</font>");

            string importOptionFile = "\\DataInputFiles\\Import\\PIPE_36129\\ImportOption\\Pipeline_Options.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importOptionFile);

            //Navigate To Community Page
            ExtentReportsHelper.LogInformation(null, "<b>Navigate to Community default page.</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            ExtentReportsHelper.LogInformation(null, $"<b>Filter community with name {communityData.Name} and create if it doesn't exist.</b>");
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

            string CommunityDetail_Url = CommunityDetailPage.Instance.CurrentURL;

            //Add Option into Community
            ExtentReportsHelper.LogInformation(null, "<b>Add Option into Community.</b>");
            CommunityDetailPage.Instance.LeftMenuNavigation("Options");
            CommunityOptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, OPTION_CODE_DEFAULT);
            if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Number", OPTION_CODE_DEFAULT) is false)
            {
                CommunityOptionPage.Instance.AddCommunityOption(OptionData);
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
                ExtentReportsHelper.LogInformation(null, "<b>Import Lot in Community.</b>");
                CommonHelper.SwitchLastestTab();
                LotPage.Instance.ImportExporFromMoreMenu("Import");
                importFileDir = "Pipeline_Lots_In_Community.csv";
                LotPage.Instance.ImportFile("Lot Import", $@"\DataInputFiles\Import\PIPE_36129\ImportLot\{importFileDir}");
                CommonHelper.OpenURL(LotPageUrl);

                //Check Lot Numbet in grid 
                if (LotPage.Instance.IsItemInGrid("Number", _lotdata.Number) && LotPage.Instance.IsItemInGrid("Status", _lotdata.Status))
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Import Lot File is successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail("<font color='red'>Import Lot File is unsuccessfully</font>");
                }
            }

            ExtentReportsHelper.LogInformation(null, "<b>Create a new Series.</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);
            SeriesData seriesData = new SeriesData()
            {
                Name = "QA_RT_Serie3_Automation",
                Code = "",
                Description = "Please no not remove or modify"
            };

            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                // Create a new series
                SeriesPage.Instance.CreateSeries(seriesData);
            }

            //I. Setup Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I. Setup Data.</font></b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            //Step 1.1: Go to the House detail > Add community and change the “Name” field.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Step 1.1: Go to the House detail > Add community and change the “Name” field.</font>");
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

            //Navigate House Option And Add Option into House
            ExtentReportsHelper.LogInformation(null, $"<b>Switch to House/ Options page. Add option '{OPTION_NAME_DEFAULT}' to house '{HOUSE_NAME_DEFAULT}' if it doesn't exist.</b>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            //Add community and change the “Name” field.
            ExtentReportsHelper.LogInformation(null, "Add community and change the “Name” field.");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");
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
            //II. Check the Override Name on the specset

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step II: Check the Override Name on the specset.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.1: Go to specset detail: Click “ +” on the House grid: Searching the Community and House.</font>");
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData.GroupName);
            }
            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData.GroupName);

            ExtentReportsHelper.LogInformation(null, "<b>Add a Spec Set> Verify the spec set is added successfully</b>");
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            Assert.That(SpecSetDetailPage.Instance.IsModalDisplayed(), "The add new spect set modal is not displayed");
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData.SpectSetName);


            //Saving and checking the Override Name.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.2: Saving and checking the Override Name.</font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(ATTRIBUTE_NAME, HOUSE_CODE_DEFAULT +"-"+ HOUSE_NAME_DEFAULT, SpecSetData.SpectSetName, COMMUNITY_CODE_DEFAULT +"-"+ COMMUNITY_NAME_DEFAULT);

            //III. Check the Override Name on Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step III. Check the Override Name on Community.</b></font>");
            CommonHelper.OpenURL(CommunityDetail_Url);
            CommunityDetailPage.Instance.LeftMenuNavigation("Spec Sets");
            CommunitySpecSetsPage.Instance.FindItemInGridWithTextContains("Spec Set Groups Name", SPECSETGROUP_NAME_DEFAULT);
            CommunitySpecSetsPage.Instance.VerifyHouseOverridesInSpecSetGroup(SPECSETGROUP_NAME_DEFAULT, ListHouse);
            
            //IV. Check the Override Name on Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step IV. Check the Override Name on Job.</b></font>");
            //Create Job
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4.1: Create Job.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_JOB_URL);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is false)
            {
                JobPage.Instance.CreateJob(jobData);
            }
            else
            {
                JobPage.Instance.DeleteJob(jobData.Name);
                JobPage.Instance.CreateJob(jobData);
            }

            //Job list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4.2: Job list.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_JOB_URL);
            ExtentReportsHelper.LogInformation(null, "<b>Verify House Overries in All Job.</b>");
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("House", jobData.House) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>House Overrides with Name {jobData.House} is displayed In All Job.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>House Overrides with Name {jobData.House} is not displayed In All Job.</font>");
            }
            //Active Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4.3: Active Jobs.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_ACTIVE_JOB_URL);
            ExtentReportsHelper.LogInformation(null, "<b>Verify House Overries in Active Job.</b>");
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("House", jobData.House) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>House Overrides with Name {jobData.House} is displayed In Active Job.</b></font>");
                JobPage.Instance.SelectItemInGrid("Job Number", jobData.Name);

                ExtentReportsHelper.LogInformation(null, $"<b>Switch current job to status 'Close'.</b>");
                JobDetailPage.Instance.OpenOrCloseJob(JOB_OPEN);
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>House Overrides with Name {jobData.House} is not displayed In Active Job.</font>");
            }

            //Completed Jobs
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4.5: Completed Jobs.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMPLETED_JOB_URL);
            ExtentReportsHelper.LogInformation(null, "<b>Verify House Overries in Complete Job.</b>");
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("House", jobData.House) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>House Overrides with Name {jobData.House} is displayed In Completed Job.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>House Overrides with Name {jobData.House} is not displayed In Completed Job.</font>");
            }
        }

        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Delete Data.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<b> Back to Spec Set page and delete it.</b>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData.GroupName);

            if (SpecSetPage.Instance.GetLastestToastMessage().ToLower().Contains("successful"))
                ExtentReportsHelper.LogPass("<font color = 'green'><b>Spec Set Group deleted successfully!</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color = 'red'>Spec Set Group failed to delete!</font>");

            ExtentReportsHelper.LogInformation(null, "<b>Delete Completed Job.</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMPLETED_JOB_URL);
            JobPage.Instance.FilterItemInGrid("Job Number", jobData.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", jobData.Name) is true)
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", jobData.Name);
            }
        }
    }
}
