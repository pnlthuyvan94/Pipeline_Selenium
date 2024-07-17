namespace Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask.AddSchedulingTaskToTrade
{
    public partial class AddSchedulingTaskToTradeModal
    {

        public void SelectSchedulingTasks(string[] list)
        {
            Tasks_list.SetChecked(Common.Enums.GridFilterOperator.EqualTo, list);
        }
        
        public void Save()
        {
            Save_btn.Click();
            System.Threading.Thread.Sleep(1000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlPhase']/div[1]");
        }
        
    }
}
