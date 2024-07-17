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
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_E_RT_01202 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CommunityData communityData;
        private LotData lotData;
        public static AvaiablePlanData houseData;
        private SeriesData seriesData;

        private readonly string SERIES_NAME = "QA_RT_Series_RT_01202";

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
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.ADMIN_SETTINGS_URL);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            communityData = new CommunityData()
            {
                Name = "QA_RT_Community_Auto_Assign_House_To_Lot",
                Code = "QA_RT_Community_Auto_Assign_House_To_Lot_Code",
                Division = "None",
                Status = "Open",
                Slug = "R-QA-Only-Community-Auto - slug",
            };


            lotData = new LotData()
            {
                Number = "QA_RT_Lot_1"
            };


            houseData = new AvaiablePlanData()
            {
                Id = "0051",
                Name = "QA_RT_House_RT_01202"
            };


            // Step 1.1: Filter community with name "QA_RT_Community_Auto_Assign_Lot_Phase_To_House" and create if it doesn't exist.
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
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

            string importFile = "\\DataInputFiles\\Import\\RT_01202\\Pipeline_Lots.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.LOT_IMPORT, importFile);

            // Step 1.3: Create Series
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Create a new Series.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

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

            importFile = "\\DataInputFiles\\Import\\RT_01202\\Pipeline_Houses.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_IMPORT, importFile);

            /****************************************** Set up data *******************************************/

            // Step 1.5: Filter community with name "QA_RT_Community_Auto_Assign_Lot_Phase_To_House" and create if it doesn't exist.
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
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
        }


        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A02_E_Assets_DetailPage_Community_Lot_AssigHouseToLot()
        {
            // Step 2: Open Lot navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open Lot navigation.</b></font>");
            LotPage.Instance.LeftMenuNavigation("Lots");
            if (LotPage.Instance.IsLotPageDisplayed is true)
                ExtentReportsHelper.LogPass("<font color='green'><b>Lot page displays correctly.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The title on Lot page is incorrect.<br>Expected title: 'Add Lot'.</font>");


            // ************************************* Assign All plan to lot ****************************************************
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3: Assign All plan to lot.</b></font>");
            // Open assign lot to plan modal
            LotPage.Instance.OpenAssignPlanToLotModal(lotData.Number);
            if(LotPage.Instance.AssignedModal == null)
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Assign all Plan to Lot button.</font>");

            bool isAssignedToPlan = false;
            bool isAssignedToPhase = false;
            bool isAssignedLot = false;

            if (LotPage.Instance.AssignedModal.IsModalDisplayed(isAssignedToPlan, isAssignedToPhase, isAssignedLot) is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>Assign all Plan to Lot modal displays correctly.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Assign all Plan to Lot modal doesn't display.</font>");


            // Assign all
            LotPage.Instance.AssignedModal.SelectOrUnSelectAllItems(true, isAssignedToPlan, isAssignedToPhase, isAssignedLot);
            if (LotPage.Instance.AssignedModal.IsAssignedSuccessful(isAssignedToPlan, isAssignedToPhase, isAssignedLot))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Assigned all Plan to Lot: {lotData.Number} successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Assigned all Plan to Lot: {lotData.Number} unsuccessfully.</font>");
            }

            // Close modal
            LotPage.Instance.AssignedModal.Cancel();

            // Step 4: Delete lot
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4: Delete lot.</b></font>");
            if (LotPage.Instance.RemoveLotFromCommunity(lotData.Number))
                ExtentReportsHelper.LogInformation($"Lot: {lotData.Number} deleted successfully!");
        }

        #endregion

        [TearDown]
        public void CleanData()
        {
            // Remove house from community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Remove house from community.</b></font>");
            AvailablePlanPage.Instance.LeftMenuNavigation("Available Plans");
            if (AvailablePlanPage.Instance.IsAvailablePlanPageDisplayed is true)
                ExtentReportsHelper.LogPass("<font color='green'><b>Available Plan page displays correctly then remove all plan from community.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color='red'>Available Plan page doesn't display or title is incorrect.<br>Expected title: 'Available Plan'.</font>");

            // Step 6: Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: Back to Community default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);

            // Step 7: Open setting page, Turn ON Sage 300 and MS NAV
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 7: Open setting page, Turn ON Sage 300 and MS NAV.<b></b></font>");
            CommunityPage.Instance.SetSage300AndNAVStatus(true);
        }
    }
}