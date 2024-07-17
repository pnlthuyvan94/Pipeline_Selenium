using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.AvailablePlan;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.Communities.Phase;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_C_RT_01200 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CommunityData communityData;
        private AvaiablePlanData houseData;
        private LotData lotData;
        private CommunityPhaseData phaseData;
        private SeriesData seriesData;

        private readonly string SERIES_NAME = "QA_RT_Series_RT_01200";

        string importFile;

        [SetUp] // Pre-condition
        public void GetOldTestData()
        {
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

            /****************************************** Import data *******************************************/
            // Make sure current transfer seperation character is ','
            // Step 0.3: Navigate to Setting page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Navigate to Setting page.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            communityData = new CommunityData()
            {
                Name = "QA_RT_Community_Auto_Assign_Lot_Phase_To_House",
                Code = "QA_RT_Community_Auto_Assign_Lot_Phase_To_House_Code",
                Division = "None",
                Status = "Open",
                Slug = "R-QA-Only-Community-Auto - slug",
            };

            houseData = new AvaiablePlanData()
            {
                Id = "0050",
                Name = "QA_RT_House_RT_01200"
            };

            lotData = new LotData()
            {
                Number = "QA_RT_Lot_1"
            };

            phaseData = new CommunityPhaseData()
            {
                Name = "QA_RT_Phase_1",
                Code = "RT1"
            };

            /******************** Issue related to import community *********************************/

            /*
            // Step 1.1: Navigate to Community page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Community default page.</b></font>");

            // Import to create new or update community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Import Communities.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);

            //CommonHelper.SwitchLastestTab();
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_IMPORT} grid view to import new Community.</font>");

            importFile = "\\DataInputFiles\\Import\\RT_01200\\Pipeline_Communities.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_IMPORT, importFile);
            */

            /*********************** Create a new community instead of importing ************************/
            // Step 1.1: Filter community with name "QA_RT_Community_Auto_Assign_Lot_Phase_To_House" and create if it doesn't exist.
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.1: Filter community with name {communityData.Name} and create if it doesn't exist.<b></b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                // Create a new one
                CommunityPage.Instance.CreateCommunity(communityData);
            }


            // Step 1.2: Import Lots to Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Import Lots to Community.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_LOT);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.LOT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.LOT_IMPORT} grid view to import new Options.</font>");

            importFile = "\\DataInputFiles\\Import\\RT_01200\\Pipeline_Lots.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.LOT_IMPORT, importFile);

            // Step 1.3: Create Series
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Create a new Series.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            seriesData = new SeriesData()
            {
                Name = SERIES_NAME
            };

            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is false)
            {
                // Create a new series
                SeriesPage.Instance.CreateSeries(seriesData);
            }

            // Stưp 1.4: Import House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Import House.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_IMPORT} grid view to import House.</font>");

            importFile = "\\DataInputFiles\\Import\\RT_01200\\Pipeline_Houses.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_IMPORT, importFile);

            /****************************************** Set up data *******************************************/

            // Step 1.5: Filter community with name "QA_RT_Community_Auto_Assign_Lot_Phase_To_House" and create if it doesn't exist.
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.5: Filter community with name {communityData.Name} and create if it doesn't exist.<b></b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                // Can't find community in the import file
                Assert.Ignore($"Can't find community {communityData.Name} in the import file to continue testing. Recheck the import function. Stop this test script");
            }

            // Select filter item to open detail page
            CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);

            // Step 1.6: Add House to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.:6 Add House to community.</b></font>");
            CommunityPage.Instance.AddHouseToCommunity(houseData, communityData.Name);

            // Step 1.5: Add lot to  - This step is implemented by import function on 1.2
            //ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Add lot to community.</b></font>");
            //CommunityPage.Instance.AddLotToCommunity(lotData, false);

            // Step 1.7: Add Phase to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Add Phase to community.</b></font>");
            CommunityPage.Instance.AddPhaseToCommunity(phaseData, false);
        }

        #region"Test Case"
        [Test]
        [Order(2)]
        [Category("Section_IV")]
        public void A02_C_Assets_DetailPage_Community_AvailablePlan_AssignLotAndPhaseToHouse()
        {

            // Step 2: Open Available Plan navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open Available Plan navigation.</b></font>");
            AvailablePlanPage.Instance.LeftMenuNavigation("Available Plans");
            if (AvailablePlanPage.Instance.IsAvailablePlanPageDisplayed is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>Available Plan page displays correctly.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Available Plan page doesn't display or title is incorrect." +
                    $"<br>Expected title: 'Available Plan'</font>");


            // ************************************* Assign All community phase to plan ****************************************************
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Assign All community phase to plan.</b></font>");
            CommunityPage.Instance.AssignOrUnAssignLotPhaseToHouse(houseData.Name, false, true);

            // ************************************* Assign All lot to plan **********************************************************************
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Assign All lot to plan.</b></font>");
            CommunityPage.Instance.AssignOrUnAssignLotPhaseToHouse(houseData.Name, true, true);


            //******************************************************************************************************************************

            // Remove house from community: Expected: can't delete hosue
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3: Remove house from community: Expected: can't delete hosue.</b></font>");
            CommunityPage.Instance.RemoveHouseFromCommunity(communityData.Name, houseData.Name, false);

            // ************************************* Un-Assign All community phase to plan ****************************************************
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4: Un-Assign All community phase to plan.</b></font>");
            CommunityPage.Instance.AssignOrUnAssignLotPhaseToHouse(houseData.Name, false, false);

            // ************************************* Un-Assign All lot to plan **********************************************************************
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.5: Un-Assign All lot to plan.</b></font>");
            CommunityPage.Instance.AssignOrUnAssignLotPhaseToHouse(houseData.Name, true, false);

            //******************************************************************************************************************************

            // Remove house from community: Expectation: Delete hosue successful
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.6: Remove house from community: Expectation: Delete hosue successful.</b></font>");
            CommunityPage.Instance.RemoveHouseFromCommunity(communityData.Name, houseData.Name, true);

        }
        #endregion



        [TearDown]
        public void CleanData()
        {
            // Remove phase from community: Expectation: Delete phase successful
            CommunityPhasePage.Instance.LeftMenuNavigation("Phases");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Remove phase from community: Expectation: Delete phase successful.</b></font>");
            CommunityPhasePage.Instance.RemoveCommunityPhaseFromCommunity(communityData.Name, phaseData.Name, true);

            // Remove lot from community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Remove lot from community.</b></font>");
            LotPage.Instance.LeftMenuNavigation("Lots");
            if (LotPage.Instance.IsLotPageDisplayed is true)
                ExtentReportsHelper.LogPass("<font color='green'><b>Add Lot page displays correctly then remove all plan from community.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color='red'>Add Lot page doesn't display or title is incorrect.<br>Expected title: 'Add Lot'.</font>");


            LotPage.Instance.GetInstance();
            LotPage.Instance.RemoveLotFromCommunity(lotData.Number);

            // Step 5: Remove house from community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Remove house from community.</b></font>");
            AvailablePlanPage.Instance.LeftMenuNavigation("Available Plans");
            if (AvailablePlanPage.Instance.IsAvailablePlanPageDisplayed is true)
                ExtentReportsHelper.LogPass("<font color='green'><b>Available Plan page displays correctly then remove all plan from community.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color='red'>Available Plan page doesn't display or title is incorrect.<br>Expected title: 'Available Plan'.</font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Phase is already deleted from step 3." +
                "<br>On this step, we just delete house from community.</b></font>");
            CommunityPage.Instance.RemoveHouseFromCommunity(communityData.Name, houseData.Name, true);


            // Step 6.1: Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1: Back to Community default page and delete data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);

            // Step 6.2: Back to House default page and delete data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2: Back to House default page and delete data.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Filter community then delete it
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.Name);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.Name) is true)
                HousePage.Instance.DeleteHouse(houseData.Name);

            // Step 6.3: Back to Series default page and delete data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.3: Back to Series default page and delete data.</b></font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);

            // Filter community then delete it
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name) is true)
                SeriesPage.Instance.DeleteSeries(string.Empty, seriesData.Name);

            // Step 7: Open setting page, Turn ON Sage 300 and MS NAV
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 7: Open setting page, Turn ON Sage 300 and MS NAV.<b></b></font>");
            CommunityPage.Instance.SetSage300AndNAVStatus(true);
        }
    }
}
