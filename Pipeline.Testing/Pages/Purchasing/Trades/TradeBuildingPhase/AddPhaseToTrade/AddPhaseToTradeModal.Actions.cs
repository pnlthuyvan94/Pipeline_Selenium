namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase.AddPhaseToTrade
{
    public partial class AddPhaseToTradeModal
    {

        public void SelectBuildingPhases(string[] list)
        {
            BuildingPhase_list.SetChecked(Common.Enums.GridFilterOperator.EqualTo, list);
        }
        
        public void Save()
        {
            Save_btn.Click();
            System.Threading.Thread.Sleep(1000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlPhase']/div[1]");
        }
        
    }
}
