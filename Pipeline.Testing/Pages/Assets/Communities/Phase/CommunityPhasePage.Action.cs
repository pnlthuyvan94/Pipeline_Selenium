using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther;
using Pipeline.Testing.Pages.Assets.Communities.Phase.AddPhase;

namespace Pipeline.Testing.Pages.Assets.Communities.Phase
{
    public partial class CommunityPhasePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Phase_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            Phase_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Phase_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            //Phase_Grid.ClickDeleteItemInGrid(columnName, value);
            string delete = $"//tbody/tr/td/a[contains(text(),{value})]/../following-sibling::td/input[contains(@src,'Images/delete')]";
            IWebElement deletebtn = FindElementHelper.FindElement(FindType.XPath, delete);
            deletebtn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunityPhases']/div[1]");
        }

        public void OpenAddPhaseModal()
        {
            Add_btn.Click();
            CommonHelper.VisibilityOfAllElementsLocatedBy(3, "//*[@id='lotinfomodal']/section/header/h1");
            AddPhaseModal = new AddCommunityPhaseModal();
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunityPhases']/div[1]");
        }

        public void OpenAssignLotToPhaseModal(string phaseName)
        {
           // string AssignLotXpath = $"//*[@id='ctl00_CPH_Content_rgCommunityPhases_ctl00']/tbody/tr[td/a[text()='{phaseName}']]/td/input[contains(@src,'Images/lots')]";
            string AssignLotXpath = $"//tbody/tr/td/a[contains(text(),{phaseName})]/../following-sibling::td/input[contains(@src,'Images/lots')]";
            IWebElement assignLot = FindElementHelper.FindElement(FindType.XPath, AssignLotXpath);
            if (assignLot != null)
            {
                assignLot.Click();
                // Open asigned modal
                AssignedModal = new AssignLotOrPhaseOrHouseToEachOtherModal();
            }
        }

        public void OpenAssignPlanToPhaseModal(string phaseName)
        {
            // string AssignPlanXpath = $"//*[@id='ctl00_CPH_Content_rgCommunityPhases_ctl00']/tbody/tr[td/a[text()='{phaseName}']]/td/input[contains(@src,'Images/home')]";
            string AssignPlanXpath = $"//tbody/tr/td/a[contains(text(),{phaseName})]/../following-sibling::td/input[contains(@src,'Images/home')]";
            IWebElement assignPlan = FindElementHelper.FindElement(FindType.XPath, AssignPlanXpath);
            if (assignPlan != null)
            {
                assignPlan.Click();
                // Open asigned modal
                AssignedModal = new AssignLotOrPhaseOrHouseToEachOtherModal();
            }
        }

        /// <summary>
        /// Assign or unassign lot/ House to Community Phase
        /// </summary>
        /// <param name="phaseName"></param>
        /// <param name="isAssignLotToPhase"></param>
        /// <param name="isSssignAll"></param>
        public void AssignOrUnAssignLotOrHouseToPhase(string phaseName, bool isAssignLotToPhase, bool isSssignAll)
        {
            bool isAssignedToPlan = false;
            bool isAssignedToPhase = true;
            string assignedItem, status;

            if (isAssignLotToPhase)
            {
                assignedItem = "Lots";

                // Open assign lot to plan modal
                OpenAssignLotToPhaseModal(phaseName);
            }
            else
            {
                assignedItem = "Plans";

                // Open assign phase to plan modal
                OpenAssignPlanToPhaseModal(phaseName);
            }

            if (isSssignAll)
                status = "Assign all";
            else
                status = "Un-Assign all";

            if (AssignedModal.IsModalDisplayed(isAssignedToPlan, isAssignedToPhase, isAssignLotToPhase) is true)
                ExtentReportsHelper.LogPass(null, $"Assign {assignedItem} to Community Phase modal displays correctly.");
            else
                ExtentReportsHelper.LogFail($"Assign {assignedItem} to Community Phase modal doesn't display.");


            // Un-Assign all or Assign all
            AssignedModal.SelectOrUnSelectAllItems(isSssignAll, isAssignedToPlan, isAssignedToPhase, isAssignLotToPhase);
            if (AssignedModal.IsAssignedSuccessful(isAssignedToPlan, isAssignedToPhase, isAssignLotToPhase))
            {
                ExtentReportsHelper.LogPass($"{status} current {assignedItem} to Community Phase: {phaseName} successfully.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"{status} current {assignedItem} to Community Phase: {phaseName} unsuccessfully.");
            }

            // Close modal
            AssignedModal.Cancel();
        }

        /// <summary>
        /// Remove phase from community by phase name
        /// </summary>
        /// <param name="communityName"></param>
        /// <param name="phaseName"></param>
        /// <param name="isExpectedDeleteSuccessful"></param>
        public void RemoveCommunityPhaseFromCommunity(string communityName, string phaseName, bool isExpectedDeleteSuccessful = true)
        {
            DeleteItemInGrid("Name", phaseName);
            WaitGridLoad();

            string expectedDeleteSuccessfulMess = "Successfully deleted Community Phase.";
            string expectedDeleteUnSuccessfulMess = "Unable to delete Community Phase at this time.";

            if (isExpectedDeleteSuccessful)
            {
                // Expected: delete successful
                if (expectedDeleteSuccessfulMess == GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass($"Community Phase {phaseName} removed successfully from community {communityName}!");
                }
                else
                {
                    if (IsItemInGrid("Name", phaseName))
                        ExtentReportsHelper.LogWarning($"Community Phase {phaseName} could not be deleted from community {communityName}!");
                    else
                        ExtentReportsHelper.LogPass($"Community Phase {phaseName} deleted successfully from community {communityName}!");
                }
            }
            else
            {
                // Expected: can't delete Community Phase
                if (expectedDeleteUnSuccessfulMess == GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass($"Community Phase {phaseName} can't be removed from community {communityName}!");
                }
                else
                {
                    if (IsItemInGrid("Name", phaseName))
                        ExtentReportsHelper.LogPass($"Community Phase {phaseName} could not be deleted from community {communityName}!");
                    else
                        ExtentReportsHelper.LogFail($"Community Phase {phaseName} deleted from community {communityName}! <br> Expected: can't delete house from community.");
                }
            }
            CloseToastMessage();
        }

    }

}
