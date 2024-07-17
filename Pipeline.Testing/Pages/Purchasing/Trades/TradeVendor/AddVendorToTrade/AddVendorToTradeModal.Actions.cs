namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor.AddVendorToTrade
{
    public partial class AddVendorToTradeModal
    {

        public void SelectVendors(string[] list)
        {
            Vendor_list.SetChecked(Common.Enums.GridFilterOperator.EqualTo, list);
        }
        
        public void Save()
        {
            Save_btn.Click();
            System.Threading.Thread.Sleep(1000);
            //WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlPhase']/div[1]");
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rlbVendors']/div[1]");
        }
        
    }
}
