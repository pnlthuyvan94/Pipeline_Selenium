namespace Pipeline.Testing.Pages.Estimating.Styles.DetailStyles.SubManufacturerPage
{
    public partial class SubmanufacturerPage
    {
        public SubmanufacturerPage EnterSubManufacturerName(string name)
        {
            SubManufacturerName_txt.SetText(name);
            return this;
        }

        public void Save()
        {
            SubManufacturerSave_btn.Click();
            // Wait the grid load
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_txtMfgInsert']/div[1]");
        }

        public void CloseModal()
        {
            SubManufacturerClose_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
