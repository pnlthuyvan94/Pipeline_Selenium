using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther;
using Pipeline.Testing.Pages.Assets.Communities.AvailablePlan.AddHouseToCommunity;

namespace Pipeline.Testing.Pages.Assets.Communities.AvailablePlan
{
    public partial class AvailablePlanPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            AvailablePlan_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            AvailablePlan_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return AvailablePlan_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            AvailablePlan_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            AvailablePlan_Grid.WaitGridLoad();
        }

        public void OpenAddHouseToCommunityModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddHouse']").Click();
            CommonHelper.VisibilityOfAllElementsLocatedBy(3, "//*[@id='plans - modal']/section/header/h1");
            AddHouseToCommunityModal = new AddHouseToCommunityModal();
            
            // Wait 10s until the modal display
            CommonHelper.WaitUntilElementVisible(10, "//div[contains(@id, 'ltPlanModalTitlePanel') and text()='Add House To Community']");
        }

        public void WaitGridLoad()
        {
            AvailablePlan_Grid.WaitGridLoad();
        }

        public void OpenAssignPhaseToHouseModal(string houseName)
        {
            string AssignPhaseXpath = $"//*[@id='ctl00_CPH_Content_rgHouses_ctl00']/tbody/tr[td/a[text()='{houseName}']]/td/input[contains(@src,'communityphase')]";
            IWebElement assignPhase = FindElementHelper.FindElement(FindType.XPath, AssignPhaseXpath);
            if (assignPhase != null)
            {
                assignPhase.Click();
                // Open asigned modal
                AssignedModal = new AssignLotOrPhaseOrHouseToEachOtherModal();
            }
        }

        public void OpenAssignLotToHouseModal(string houseName)
        {
            string AssignLotXpath = $"//*[@id='ctl00_CPH_Content_rgHouses_ctl00']/tbody/tr[td/a[text()='{houseName}']]/td/input[contains(@src,'lot')]";
            IWebElement assignLot = FindElementHelper.FindElement(FindType.XPath, AssignLotXpath);
            if (assignLot != null)
            {
                assignLot.Click();
                // Open asigned modal
                AssignedModal = new AssignLotOrPhaseOrHouseToEachOtherModal();
            }
        }

    }

}

