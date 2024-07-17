using System.Collections.Generic;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddBuildingPhase;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase
{
    public partial class BuildingPhasePage
    {
        public void ClickAddToBuildingPhaseModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddBuildingPhaseModal = new AddBuildingPhaseModal();
        }

        public void ClickItemInGrid(string columnName, string value)
        {
            BuildingPhase_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingPhase_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhase_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']");
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingPhase_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']");
        }

        public void WaitGridLoad()
        {
            BuildingPhase_Grid.WaitGridLoad();
        }

        public void SyncBuildingPhaseToBuildPro()
        {
            SyncToBuildPro.Click();
            CommonHelper.WaitUntilElementVisible(5, "//*[@id='ctl00_CPH_Content_BuildProSyncModal_lblHeader']");
            StartSyncToBuildPro_Btn.Click();
            System.Threading.Thread.Sleep(2000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_BuildProSyncModal_autoGrid_rgResults']/div[1]", 600, 0);
        }

        public void ClickEditItemInGrid(string columnName,  string value)
        {
            BuildingPhase_Grid.ClickEditItemInGrid(columnName,value);
            PageLoad();
        }

        public void SelectItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhase_Grid.ClickItemInGrid(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']");
        }
        private void SetCheckItemsViaCheckBox(string columnName, List<string> items)
        {
            foreach (var item in items)
            {
                BuildingPhase_Grid.SelectItemOnGridViaCheckbox(columnName, item);
            }
        }
        private void DeleteSelectedItemsViaBulkActions()
        {
            BulkActionsButton.Click();
            DeleteSelectedButton.Click();
            ConfirmDialog(ConfirmType.OK);
            PageLoad();
        }
        public void DeleteItemsViaCheckbox(string columnName, List<string> items)
        {
            SetCheckItemsViaCheckBox(columnName, items);
            DeleteSelectedItemsViaBulkActions();
        }
    }
}
