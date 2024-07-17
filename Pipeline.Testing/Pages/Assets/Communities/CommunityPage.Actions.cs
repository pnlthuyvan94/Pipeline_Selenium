using NUnit.Framework;
using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.AvailablePlan;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Lot;
using Pipeline.Testing.Pages.Assets.Communities.Phase;
using Pipeline.Testing.Pages.Settings.MSNAV;
using Pipeline.Testing.Pages.Settings.Sage300CRE;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Pages.Assets.Communities
{
    public partial class CommunityPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CommunityPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            System.Threading.Thread.Sleep(2000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CommunityPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsItemWithTextContainsInGrid(string columnName, string value)
        {
            return CommunityPage_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            CommunityPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]", 5000);
        }
        public void WaitGridLoad()
        {
            CommunityPage_Grid.WaitGridLoad();
        }


        public void SelectItemInGrid(string columnName, string value)
        {
            CommunityPage_Grid.ClickItemInGridWithTextContains(columnName, value);
            PageLoad();
        }

        public void ClickItemInGridWithTextContains(string columnName, string value)
        {
            CommunityPage_Grid.ClickItemInGridWithTextContains(columnName, value);
            PageLoad();
        }

        public void SelectItemInGrid(int columIndex, int rowIndex)
        {
            CommunityPage_Grid.ClickItemInGrid(columIndex, rowIndex);
            PageLoad();
        }

        public void SyncCommunityToBuildPro()
        {
            BuildProSync_Btn.Click();
            // Waiting for the model is displayed
            CommonHelper.WaitUntilElementVisible(5, "//*[@id='ctl00_CPH_Content_BuildProSyncModal_lblHeader']");
            SyncToBuildPro_Btn.Click();
            System.Threading.Thread.Sleep(2000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_BuildProSyncModal_autoGrid_rgResults']/div[1]", 600, 2);
        }

        public void AddHouseToCommunity(AvaiablePlanData houseData, string communityName)
        {
            if (CurrentURL.Contains("Dashboard/Builder/Communities/Plans.aspx") is false)
            {
                // Navigate to Avaiable Plan page and add house to community
                AvailablePlanPage.Instance.LeftMenuNavigation("Available Plans");
                Assert.That(AvailablePlanPage.Instance.IsAvailablePlanPageDisplayed, "Available Plan page doesn't display.");
                ExtentReportsHelper.LogPass(null, "Available Plan page displays correctly.");
            }

            // Verify the House does not exist on Available plan
            if (!AvailablePlanPage.Instance.IsItemInGrid("Name", houseData.Name))
            {
                // Click Add (+) house to community button
                AvailablePlanPage.Instance.OpenAddHouseToCommunityModal();
                AvailablePlanPage.Instance.AddHouseToCommunityModal.AddHouseToCommunity(houseData);
                AvailablePlanPage.Instance.WaitGridLoad();

                string _actualMessage = AvailablePlanPage.Instance.GetLastestToastMessage();
                string _expectedMessage = "House added successfully!";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"Could not add house {houseData.Name} to community {communityName}.");
                    Assert.Fail($"Could not add house {houseData.Name} to community {communityName}. Actual messsage: {_actualMessage}");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Add house {houseData.Name} to community {communityName} sucessfully.");
                    AvailablePlanPage.Instance.CloseToastMessage();
                }
                Assert.That(AvailablePlanPage.Instance.AddHouseToCommunityModal.IsAddedHouseToCommunitySuccessful(houseData.Id + "-" + houseData.Name),
                    $"The house {houseData.Name} is added to community unsuccessfully.");


                // Filter house in the grid view

                if (!AvailablePlanPage.Instance.IsItemInGrid("Name", houseData.Name))
                    ExtentReportsHelper.LogFail($"House: {houseData.Name} doesn't display in the grid view");
                else
                    ExtentReportsHelper.LogPass($"House: {houseData.Name} displays correctly in the grid view.");
            }
            else
                ExtentReportsHelper.LogInformation($"House: {houseData.Name} displayed in the grid view.");
        }

        public void AddPhaseToCommunity(CommunityPhaseData phaseData, bool isDuplicate = false)
        {
            if (CurrentURL.Contains("Dashboard/Builder/Communities/Phases/Default.aspx") is false)
            {
                // Navigate to Phases page and add phase to community
                CommunityPhasePage.Instance.LeftMenuNavigation("Phases");
                Assert.That(CommunityPhasePage.Instance.IsPhasePageDisplayed, "Phases page doesn't display.");
                ExtentReportsHelper.LogPass(null, "Phases page displays correctly.");
            }

            // Click Add (+) house to community button
            CommunityPhasePage.Instance.OpenAddPhaseModal();

            CommunityPhasePage.Instance.AddPhaseModal.AddPhase(phaseData);
            CommunityPhasePage.Instance.WaitGridLoad();

            string _actualMessage = CommunityPhasePage.Instance.GetLastestToastMessage();
            string _expectedAddNewPhaseMessage = $"Successfully created Community Phase {phaseData.Name}.";
            string _expectedDuplicatePhaseMessage = $"Unable to create Community Phase {phaseData.Name} at this time.";

            if (!isDuplicate && !string.IsNullOrEmpty(_actualMessage) && _actualMessage != _expectedAddNewPhaseMessage)
            {
                // Create new phase unsuccessful
                ExtentReportsHelper.LogFail($"Could not create new phase {phaseData.Name}.");
                Assert.Fail($"Could not create new phase {phaseData.Name}. Actual messsage: {_actualMessage}");
            }
            else if (isDuplicate && !string.IsNullOrEmpty(_actualMessage) && _actualMessage != _expectedDuplicatePhaseMessage)
            {
                // Don't support creating duplicate phase name
                ExtentReportsHelper.LogFail($"Phase: {phaseData.Name} shouldn't be created with duplicate code.");
                Assert.Fail($"Phase: {phaseData.Name} shouldn't be created with duplicate code.");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Create new phase {phaseData.Name} sucessfully/ Can't create with a duplicate phase code: {phaseData.Code} successfully.");
                //CommunityPhasePage.Instance.CloseToastMessage();
            }

            // Close modal
            //CommunityPhasePage.Instance.AddPhaseModal.Close();
        }

        public void AddLotToCommunity(LotData lotData, bool isDuplicate = false)
        {
            if (CurrentURL.Contains("Dashboard/Builder/Communities/Lots/Default.aspx") is false)
            {
                // Navigate to Lots page and add lot to community
                LotPage.Instance.LeftMenuNavigation("Lots");
                Assert.That(LotPage.Instance.IsLotPageDisplayed, "Lots page doesn't display.");
                ExtentReportsHelper.LogPass(null, "Lots page displays correctly.");
            }

            if (!isDuplicate)
            {
                LotPage.Instance.LeftMenuNavigation("Lots");
                Assert.That(LotPage.Instance.IsLotPageDisplayed, "Lot page doesn't display.");
                ExtentReportsHelper.LogPass("Lot page displays correctly.");
            }

            // Click Add (+) house to community button
            LotPage.Instance.OpenAddLotModal();

            LotPage.Instance.AddLotModal.AddLot(lotData);
            LotPage.Instance.WaitGridLoad();

            string _actualMessage = LotPage.Instance.GetLastestToastMessage();
            string _expectedAddNewLotMessage = "Lot created successfully!";
            string _expectedDuplicateLotMessage = "Lot Number already exists in Community Phase";

            if (!isDuplicate && !string.IsNullOrEmpty(_actualMessage) && _actualMessage != _expectedAddNewLotMessage)
            {
                // Create new lot unsuccessful
                ExtentReportsHelper.LogFail($"Could not create new lot {lotData.Number}.");
                Assert.Fail($"Could not create new lot {lotData.Number}. Actual messsage: {_actualMessage}");
            }
            else if (isDuplicate && !string.IsNullOrEmpty(_actualMessage) && _actualMessage != _expectedDuplicateLotMessage)
            {
                // Don't support creating duplicate lot number
                ExtentReportsHelper.LogFail($"Lot: {lotData.Number} shouldn't be created with duplicate number.");
                Assert.Fail($"Lot: {lotData.Number} shouldn't be created with duplicate number.");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Create new lot {lotData.Number} sucessfully/ Can't create with a duplicate lot number: {lotData.Number} successfully.");
                LotPage.Instance.CloseToastMessage();
            }

            // Close modal
            LotPage.Instance.AddLotModal.Cancel();
        }

        public void AssignOrUnAssignLotPhaseToHouse(string houseName, bool isAssignLotToHouse = true, bool isAssignAll = true)
        {
            bool isAssignedToPlan = true;
            bool isAssignedToPhase = false;
            string assignedItem, status;

            if (isAssignLotToHouse)
            {
                assignedItem = "Lots";
                // Open assign lot to plan modal
                AvailablePlanPage.Instance.OpenAssignLotToHouseModal(houseName);
            }
            else
            {
                assignedItem = "Phases";
                // Open assign phase to plan modal
                AvailablePlanPage.Instance.OpenAssignPhaseToHouseModal(houseName);
            }


            if (isAssignAll)
                status = "Assign all";
            else
                status = "Un-Assign all";

            if (AvailablePlanPage.Instance.AssignedModal.IsModalDisplayed(isAssignedToPlan, isAssignedToPhase, isAssignLotToHouse) is true)
                ExtentReportsHelper.LogPass(null, $"Assign {assignedItem} to Plan modal displays correctly.");
            else
                ExtentReportsHelper.LogFail($"Assign {assignedItem} to Plan modal doesn't display.");

            // Un-Assign all or Assign all
            AvailablePlanPage.Instance.AssignedModal.SelectOrUnSelectAllItems(isAssignAll, isAssignedToPlan, isAssignedToPhase, isAssignLotToHouse);
            if (AvailablePlanPage.Instance.AssignedModal.IsAssignedSuccessful(isAssignedToPlan, isAssignedToPhase, isAssignLotToHouse))
                ExtentReportsHelper.LogPass(null, $"{status} current {assignedItem} to Plan: {houseName} successfully.");
            else
                ExtentReportsHelper.LogFail($"{status} current {assignedItem} to Plan: {houseName} unsuccessfully.");

            // Close modal
            AvailablePlanPage.Instance.AssignedModal.Cancel();
        }

        public void RemoveHouseFromCommunity(string communityName, string houseName, bool isExpectedDeleteSuccessful = true)
        {
            if (AvailablePlanPage.Instance.IsItemInGrid("Name", houseName))
            {
                AvailablePlanPage.Instance.DeleteItemInGrid("Name", houseName);
                AvailablePlanPage.Instance.WaitGridLoad();

                string expectedDeleteSuccessfulMess = "House removed successfully!";
                string expectedDeleteUnSuccessfulMess = $"Failed to delete House {houseName} from Community {communityName} due to association with community phase assignment(s).";

                if (isExpectedDeleteSuccessful)
                {
                    // Expected: delete successful
                    string actualMsg = AvailablePlanPage.Instance.GetLastestToastMessage();
                    if (expectedDeleteSuccessfulMess.Equals(actualMsg))
                    {
                        ExtentReportsHelper.LogPass($"House {houseName} removed successfully from community {communityName}!");
                        CloseToastMessage();
                    }
                    else if (!string.IsNullOrEmpty(actualMsg))
                    {
                        ExtentReportsHelper.LogFail($"House {houseName} could not be deleted from community {communityName}! Actual message: {actualMsg}");
                        CloseToastMessage();
                    }
                }
                else
                {
                    string actualMsg = AvailablePlanPage.Instance.GetLastestToastMessage();
                    // Expected: can't delete house
                    if (expectedDeleteUnSuccessfulMess.Equals(actualMsg))
                    {
                        ExtentReportsHelper.LogPass($"House {houseName} can't be removed from community {communityName}!");
                        CloseToastMessage();
                    }
                    else if (!string.IsNullOrEmpty(actualMsg))
                    {
                        ExtentReportsHelper.LogFail($"House {houseName} deleted from community {communityName}! <br> Expected: can't delete house from community. Actual message {actualMsg}");
                        CloseToastMessage();
                    }
                }
            }
        }

        /// <summary>
        /// Override close toast message function
        /// </summary>
        private void CloseToastMessage()
        {
            Button item = new Button(FindType.XPath, "/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper' and position()=last()]");
            if (item.WaitForElementIsVisible(1000, false))
                item.Click();
        }


        public void CreateCommunity(CommunityData communityData)
        {
            // Step 2: click on "+" Add button
            GetItemOnHeader(DashboardContentItems.Add).Click();
            var _expectedCreateCommunityURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_COMMUNITY_URL;
            Assert.That(CommunityDetailPage.Instance.IsPageDisplayed(_expectedCreateCommunityURL), "Community detail page with id = 0 doesn't displayed.");

            // Create Community - Click 'Save' Button
            CommunityDetailPage.Instance.AddOrUpdateCommunity(communityData);

            string _expectedMessage = $"Could not create Community with name {communityData.Name}.";
            if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                ExtentReportsHelper.LogFail($"<font color = red>Could not create Community with name { communityData.Name}.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Create Community with name {communityData.Name} successfully.</b></font>");

            // Verify community name on the breadscrum
            if (CommunityDetailPage.Instance.IsSaveCommunitySuccessful(communityData.Name) is false)
                ExtentReportsHelper.LogFail($"Community with name '{communityData.Name}' DOESN'T display correctly on the breadscrum." +
                    $"<br>Expected:<font color='red'><b>{communityData.Name}</b></font>.");
            else
                ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Community with name '{communityData.Name}' display correctly on the breadscrum.</b></font>");
        }

        /// <summary>
        /// Delete community by name
        /// </summary>
        /// <param name="communityName"></param>
        public void DeleteCommunity(string communityName)
        {
            DeleteItemInGrid("Name", communityName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");

            string successfulMess = $"Community {communityName} deleted successfully!";
            string actualMsg = GetLastestToastMessage();
            if (successfulMess.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Community deleted successfully!</b></font>");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Name", communityName))
                    ExtentReportsHelper.LogFail("The Community could not be deleted - Possible constraint preventing deletion.");
                else
                    ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Community deleted successfully!</b></font>");
            }
        }
		
		public void DeletAllAssignments()
        {
            LeftMenuNavigation("Assignments");
            Button DelAllAssign = new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbDeleteAssignment']");
            DelAllAssign.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlCommunityAssignments']/div[1]");
            
        }

        /// <summary>
        /// Verify Build Pro and NAV status before running test scripts related to lot button
        /// </summary>
        /// <returns></returns>
        public void SetSage300AndNAVStatus(bool status)
        {
            // Open Setting page
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);

            // Set Nav status
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Open MS NAV on the left navigation and verify it.</b></font>");
            MSNAVPage.Instance.LeftMenuNavigation("MS NAV");
            MSNAVPage.Instance.SetMsNAVStatus(status);

            // Set Sage 300 status
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Open Sage 300 CRE on the left navigation and verify it.</b></font>");
            Sage300CREPage.Instance.LeftMenuNavigation("Sage300CRE");
            Sage300CREPage.Instance.SetSage300Status(status);
        }

        /// <summary>
        /// Get Community Name by column and row
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public string GetCommunityNameByIndexAndColumn(string columnName, int rowIndex)
        {
            IWebElement option = CommunityPage_Grid.GetItemByRowAndColumn(columnName, rowIndex);
            return option != null ? option.Text : string.Empty;
        }

        /// <summary>
        /// Get total number on the grid view
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return CommunityPage_Grid.GetTotalItems;
        }
    }

}
