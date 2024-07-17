namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.AddVendorToPhase
{
    public partial class AddVendorToPhaseModal
    {
        
        public string SelectVendor(string vendor, int index)
        {
            return Vendor_ddl.SelectItemByValueOrIndex(vendor, index);
        }       

        public void Save()
        {
            Save_btn.Click();
            System.Threading.Thread.Sleep(1000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlPhase']/div[1]");
        }

        
    }
}
