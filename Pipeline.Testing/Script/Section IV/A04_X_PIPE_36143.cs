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
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A04_X_PIPE_36143 : BaseTestScript
    {
        private HouseData houseData;
        // Set up the Test Section Name for each Test case
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\RT_01215";
        CommunityData communityData;
        private CommunityPhaseData phaseData;

        [SetUp]
        public void GetData()
        {
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
                Code = "P_001"
            };

            /****************************************** Setting *******************************************/

            // Update setting with : TransferSeparationCharacter, SetSage300AndNAV, Group by Parameter
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Prepare test data: Open setting page, Make sure current transfer seperation character is ',' <b></b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
        }

        [Test]
        [Category("Section_IV")]
        public void A04_X_Assets_DetailPage_House_HouseDetails_UserCannotNavigateToCommunicatesPhase()
        {
            string communityPhaseName = "Community_Phase_RegressionTest_Auto";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.SETUP TEST DATA</b></font>");
            // Step: Create house and community and phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.1 Create the house by import house</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_IMPORT} grid view to import new Communities.</font>");
            string importFile = $@"{IMPORT_FOLDER}\Pipeline_Communities.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_IMPORT, importFile);
            //Step: Import House.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare test data: Import House.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
            if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_IMPORT} grid view to import new House.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Houses.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_IMPORT, importFile);
            //Prepare data for Community Data
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter community with name {communityData.Name} and create if it doesn't exist.</font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                //Can't find community in the import file
                Assert.Ignore($"Can't find community {communityData.Name} in the import file to continue testing. Recheck the import function. Stop this test script");
            }
            else
            {
                //Select Community with Name
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
            }
            //Add Phase to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step: Add Phase to Community.</font>");
            CommunityDetailPage.Instance.LeftMenuNavigation("Phases");
            if (!CommunityPhasePage.Instance.IsItemInGrid("Name", phaseData.Name))
                CommunityPage.Instance.AddPhaseToCommunity(phaseData, false);
            else
                ExtentReportsHelper.LogInformation($"{phaseData.Name} is displaying in grid.");

            // 2. Go to the community/Phase: Add phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I.2: Go to the community/Phase: Add phase.</b></font>");
            // Navigate to house detail page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Navigate to House default page </font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            CommonHelper.CloseAllTabsExcludeCurrentOne();

            //Filter and check house exist
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {houseData.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
            {
                // Can't find community in the import file
                Assert.Ignore($"Can't find House {houseData.HouseName} in the import file to continue testing. Recheck the import function. Stop this test script.");
            }
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
            string url_HouseDetailPage = CommonHelper.GetCurrentDriverURL;

            //II. Verify user can go to the Community Phase detail when click on the community phase name 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II. Verify user can go to the Community Phase detail when click on the community phase name</b></font>");
            if (HouseDetailPage.Instance.IsItemNameCommunityPhaseInGrid() is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare test data: Import House to Communites.</font>");
                CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
                if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_IMPORT))
                    ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_IMPORT} grid view to import new House.</font>");

                string importFileHousetoCommunity = $@"{IMPORT_FOLDER}\Pipeline_House_Communities_QA_RT_Auto_House_RT_01215.csv";
                BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_COMMUNITY_IMPORT, importFileHousetoCommunity);

                // Navigate house detail page
                CommonHelper.OpenURL(url_HouseDetailPage);
                CommunityDetailPage.Instance.LeftMenuNavigation("House Details");


                HouseDetailPage.Instance.ClickAddToShowCommunityPhaseModal()
                                        .SelectCommunityPhase(communityPhaseName)
                                        .EnterCommunityPhasePrice("500")
                                        .InsertCommunityPhaseToThisHouse();
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.1/Click on the “Community phase” name</b></font>");
            HouseDetailPage.Instance.SelectItemNameCommunityPhaseInGrid();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.2/Opening on another tab</b></font>");
            if (CommunityPhasePage.Instance.IsPhasePageDisplayed is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Display the community phase detail in the another tag.</b></font>");
            }
            else
                ExtentReportsHelper.LogFail("<font color='red'><b>Not Display the community phase detail in the another tag</b></font>");

        }
    }
}
