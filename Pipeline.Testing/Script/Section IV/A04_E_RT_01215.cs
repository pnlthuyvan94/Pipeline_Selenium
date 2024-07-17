using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.Phase;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Phase;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Dashboard;
using Pipeline.Testing.Pages.Resources;
using System.Collections.Generic;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Assets.Series;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A04_E_RT_01215 : BaseTestScript
    {
        private HouseData houseData;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        readonly string communityForCommunityPhase = "QA_RT_Auto_Community_RT_01215";
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\RT_01215";

        private readonly int[] indexs = new int[2] { 17, 18 };
        CommunityData communityData;
        private CommunityPhaseData phaseData;
        private int testCaseIndex;
        SeriesData series;
        [SetUp]
        public void GetData()
        {
            series = new SeriesData()
            {
                Name = "RT_Series_DoNot_Delete",
                Code = "5456",
                Description= "	Please do not delete this series, use for the automation purpose"
            };

            houseData = new HouseData()
            {
                HouseName = "QA_RT_Auto_House_RT_01215",
                SaleHouseName = "RegressionTest_House_Sales_Name",
                Series = "RT_Series_DoNot_Delete",
                PlanNumber = "1215",
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
                Name = "QA_RT_Auto_Community_RT_01215",
                Division = "CG Visions",
                Code = "RT_01215",
                City = "Ho Chi Minh",
                CityLink = "https://hcm.com",
                Township = "Ho Chi Minh",
                County = "",
                State = "",
                Zip = "",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Community's used for automation testing. Please don't edit/ delete it",
                Slug = "Community_RT_Auto_Dont_Edit/Delete",
            };

            phaseData = new CommunityPhaseData()
            {
                Name = "Community_Phase_RegressionTest_Auto",
                Code = "P_001",
                Status="Open"
            };

            /****************************************** Setting *******************************************/

            // Update setting with : TransferSeparationCharacter, SetSage300AndNAV, Group by Parameter
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Make sure current transfer seperation character is ','<b></b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);
            // Filter the created series 
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, series.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", series.Name) is false)
            {
                // Create a new series
                SeriesPage.Instance.CreateSeries(series);
            }

            /****************************************** Import data *******************************************/

            // Step 0.4: Import Communities.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Import Communities.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_IMPORT} grid view to import new Communities.</font>");

            string importFile = $@"{IMPORT_FOLDER}\Pipeline_Communities.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_IMPORT, importFile);


            // Step 0.4: Import House.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Import House.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
            if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_IMPORT} grid view to import new House.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Houses.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_IMPORT, importFile);


            //Prepare data for Community Data
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.5: Filter community with name {communityData.Name} and create if it doesn't exist.<b></b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                // Can't find community in the import file
                Assert.Ignore($"Can't find community {communityData.Name} in the import file to continue testing. Recheck the import function. Stop this test script");
            }
            else
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
            }

            //Add Phase to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Add Phase to community.</b></font>");
            CommunityDetailPage.Instance.LeftMenuNavigation("Phases");
            if (!CommunityPhasePage.Instance.IsItemInGrid("Name", phaseData.Name))
                CommunityPage.Instance.AddPhaseToCommunity(phaseData, false);
            else
                ExtentReportsHelper.LogInformation($"{phaseData.Name} is displaying in grid.");
        }

        [Test]
        [Order(1)]
        [Category("Section_IV")]
        public void A04_E_Assets_DetailPage_House_Communities()
        {
            testCaseIndex = 1;
            // Step 1: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to House default page</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Filter house with name {houseData.HouseName} and create if it doesn't exist.</b></font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
            {
                // Can't find community in the import file
                Assert.Ignore($"Can't find House {houseData.HouseName} in the import file to continue testing. Recheck the import function. Stop this test script.");
            }
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);

            // Step 2.1: In House Side Navigation, click the Communities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: In House Side Navigation, click the Communities.</b></font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Communities");

            // Verify opened successfully the House Communities page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Verify opened successfully the House Communities page.</b></font>");

            string expectedCommunityPage = "/Dashboard/Builder/Houses/Communities.aspx?hid";
            if (ResourcePage.Instance.CurrentURL.Contains(expectedCommunityPage) is true)
            {
                ExtentReportsHelper.LogPass($"House Communities page is displayed");
            }
            else
            {
                ExtentReportsHelper.LogFail($"House Communities page isn't displayed");
            }

            ExtentReportsHelper.LogPass($"Opened successfully the House Communities page");

            // Step 3: Click the '+' button to add Community into House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Click the '+' button to add Community into House.</b></font>");
            HouseCommunities.Instance.AddButtonCommunities();

            // Get community with index [4] and [5] to select
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Get community with index [4] and [5] to select.</b></font>");
            IList<string> Communities = HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(communityForCommunityPhase, indexs);

            // Step 4. Click the Community; verify the hyperlink 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Click the Community. Verify the hyperlink.</b></font>");
            string communityLink = HouseCommunities.Instance.GetAndCheckHyperLink(Communities[0]);
            string URLCommunity = "Communities/Community.aspx?cid=";
            if (communityLink.ToLower().Contains(URLCommunity.ToLower()) is true)
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Opened successfully Community Details page.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Opened unsuccessfully Community Details page</font><br>" +
                    $"Actualy URL: <b>{HouseCommunities.Instance.GetAndCheckHyperLink(Communities[0])}</b>");
            }

            // Step 5. Verify filter the Communities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Verify filter the Communities.</b></font>");
            HouseCommunities.Instance.VerifyFilterCommunity("Name", Communities[0]);

            // Step 6.The ability to delete the unassigned newly created item from the UI
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: The ability to delete the unassigned newly created item from the UI.</b></font>");
            Communities.Remove(communityForCommunityPhase);
            foreach (var item in Communities)
                HouseCommunities.Instance.DeleteAndVerifyDeleteCommunity("Name", item);

            // Don't delete house at this step and continue with second test script
        }

        [Test]
        [Order(2)]
        [Category("Section_IV")]
        public void A04_E_Assets_DetailPage_House_CommunitiePhases()
        {
            testCaseIndex = 2;
            string communityPhaseName = "Community_Phase_RegressionTest_Auto";

            // Step 1: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to House default page</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Close all tab exclude the current one
            CommonHelper.CloseAllTabsExcludeCurrentOne();

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Filter house with name {houseData.HouseName} and create if it doesn't exist.</b></font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
            {
                // Can't find community in the import file
                Assert.Ignore($"Can't find House {houseData.HouseName} in the import file to continue testing. Recheck the import function. Stop this test script.");
            }

            // Step 2: Open House detail page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open House detail page on a new tab.</b></font>");
            HousePage.Instance.OpenHouseDetailPageOnNewTab(houseData.HouseName);
            CommonHelper.SwitchTab(1);

            // Step 3: Don't select community phase and click save button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Don't select community phase and click save button.</b></font>");
            HouseDetailPage.Instance.ClickAddToShowCommunityPhaseModal().InsertCommunityPhaseToThisHouse();

            // Verify mesage
            string warningMsg = "There is no Phase selected.";
            string actualMessage = HouseDetailPage.Instance.GetLastestToastMessage();
            if (warningMsg.Equals(actualMessage))
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Can't save phase to house. The message is displayed correctly.</b></font>");
                //System.Threading.Thread.Sleep(5000);
                HouseDetailPage.Instance.CloseToastMessage();
            }
            else
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The message is displayed incorrectly.</font<Expected: <font color='green'><b>{warningMsg}</b></font>.<br>Actual: <font color='green'><b>{actualMessage}</b></font>");

            // Step 4: Add community to house without price
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Add community to house without price.</b></font>");
            HouseDetailPage.Instance.SelectCommunityPhase(communityPhaseName).InsertCommunityPhaseToThisHouse();
            string errorMsg = "Price must be a number.";
            actualMessage = HouseDetailPage.Instance.GetLastestToastMessage(5);
            if (errorMsg.Equals(actualMessage))
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Can't save phase to house. The message is displayed correctly.</b></font>");
                HouseDetailPage.Instance.CloseToastMessage();
            }
            else
                ExtentReportsHelper.LogFail($"The message is displayed incorrectly." +
                    $"<br>Expected: <font color='red'>{errorMsg}</font>." +
                    $"<br>Actual: <font color='red'>{actualMessage}</font>");

            // Step 5.1: Add community to house - Happy case
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Add community to house - happy case.</b></font>");
            HouseDetailPage.Instance.SelectCommunityPhase(communityPhaseName).EnterCommunityPhasePrice("999").InsertCommunityPhaseToThisHouse();
            string successfulMsg = "1 Community Phases were added successfully.";
            actualMessage = HouseDetailPage.Instance.GetLastestToastMessage();

            if (successfulMsg.Equals(actualMessage))
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Add phase to house successfully. The message is displayed correctly.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color='red'>The message is displayed incorrectly.</font>" +
                    $"<br>Expected: <font color='green'><b>{successfulMsg}</b></font>" +
                    $"<br>Actual: <font color='red'><b>{actualMessage}</b></font>");

            // Step 5.2: Verify new community phase on the grid view
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2: Verify new community phase on the grid view.</b></font>");
            if (HouseDetailPage.Instance.IsCommunityPhaseInGrid("Community Phase Name", communityPhaseName) is true)
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>The message is displayed correctly.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The message is displayed incorrectly.</font>" +
                    $"<br>Expected: <font color='red'>{successfulMsg}</font>" +
                    $"<br>Actual: <font color='red'>{actualMessage}</font>");


            // Step 6: Back to House page and try to delete house that has a community phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: Back to House page and try to delete house that has a community phase.</b></font>");
            CommonHelper.SwitchTab(0);

            // Attempt to delete this house
            HousePage.Instance.DeleteItemInGrid("Name", houseData.HouseName);
            actualMessage = $"Not able to delete House {houseData.PlanNumber} {houseData.HouseName}";
            if (actualMessage == HousePage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>House deleted unsuccessfully as expected!</b></font>");
                //System.Threading.Thread.Sleep(5000);
                HousePage.Instance.CloseToastMessage();
            }
            else
            {
                if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
                    ExtentReportsHelper.LogPass(null, "<font color='green'><b>The House could not be deleted as expectation.</b></font>");
                else
                    ExtentReportsHelper.LogFail("<font color='red'>House deleted successfully!</font>");
            }
            CommonHelper.CloseCurrentTab();

            // Step 7: Delete community phase from house
            CommonHelper.SwitchTab(0);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7: Delete community phase from house.</b></font>");
            HouseDetailPage.Instance.DeleteCommunityPhaseInGrid("Community Phase Name", communityPhaseName);

            string deleteMsg = "Community Phase to House was successfully deleted.";
            actualMessage = HouseDetailPage.Instance.GetLastestToastMessage();
            if (deleteMsg.Equals(actualMessage))
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>The Community Phase is deleted successfully.</b></font>");
                //System.Threading.Thread.Sleep(5000);
                HouseDetailPage.Instance.CloseToastMessage();
            }
            if (HouseDetailPage.Instance.IsCommunityPhaseInGrid("Community Phase Name", communityPhaseName))
                ExtentReportsHelper.LogFail(null, "<font color='red'>The Community Phase is NOT delete successfully.</font>");

            // Step 8: Open Community tab and remove community from current house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8: Open Community tab and remove community from current house.</b></font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Communities");
            HouseCommunities.Instance.DeleteAndVerifyDeleteCommunity("Name", communityForCommunityPhase);

            // Step 9: Back to House default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 9: Back to House default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Filter old and new house then delete it
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
                HousePage.Instance.DeleteHouse(houseData.HouseName);
        }

        [TearDown]
        public void DeleteData()
        {
            if (testCaseIndex == 1)
            {
                // Don't delete house om test case 1
                return;
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 9: Back to House default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            // Filter old and new house then delete it
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
                HousePage.Instance.DeleteHouse(houseData.HouseName);
        }
    }
}
