
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask.AddSchedulingTaskToTrade;

namespace Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask
{
    public partial class TradeSchedulingTaskPage
    {
        public void DeleteItemInGrid(string columnName, string value)
        {
            SchedulingTask_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            PageLoad();
        }

        public void WaitBuildingPhaseGird()
        {
            SchedulingTask_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return SchedulingTask_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            SchedulingTask_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public void Save()
        {
            ShowModal_btn.Click();
            System.Threading.Thread.Sleep(1000);
        }

        public void ShowAddSchedulingTasksToTradeModal()
        {
            ShowModal_btn.Click();
            AddSchedulingTaskToTradeModal = new AddSchedulingTaskToTradeModal();
            System.Threading.Thread.Sleep(1000);
        }

    }
}
