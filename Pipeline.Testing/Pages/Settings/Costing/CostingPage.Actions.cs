namespace Pipeline.Testing.Pages.Settings.Costing
{
    public partial class CostingPage
    {
        public CostingPage AppendTBDCode(string vendorCode)
        {
            if (!TBDCode_Txt.GetValue().Contains(vendorCode))
                TBDCode_Txt.AppendKeys(";" + vendorCode);
            return this;
        }

        public void Save()
        {
            Save_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]");
        }
    }
}
