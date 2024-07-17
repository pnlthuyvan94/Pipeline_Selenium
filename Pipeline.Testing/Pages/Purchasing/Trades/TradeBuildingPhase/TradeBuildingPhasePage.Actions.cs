
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase.AddPhaseToTrade;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase
{
    public partial class TradeBuildingPhasePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhase_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(_gridLoading, 500);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingPhase_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            PageLoad();
        }

        public void WaitBuildingPhaseGird()
        {
            BuildingPhase_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingPhase_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void ShowAddPhaseToTradeModal()
        {
            ShowModal_btn.Click();
            AddPhaseToTradeModal = new AddPhaseToTradeModal();
            System.Threading.Thread.Sleep(1000);
        }

        public void DeleteAllBuildingPhases()
        {
            SelectAll_chk.Check();
            BulkActions_btn.Click();
            RemoveSelectedPhases_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");
        }
    }
}
