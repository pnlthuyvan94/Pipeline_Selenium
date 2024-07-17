using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail
{
    public partial class CustomOptionDetailPage : DetailsContentPage<CustomOptionDetailPage>
    {
        public CustomOptionDetailPage EnterCustomOptionCode(string name)
        {
            if (!string.IsNullOrEmpty(name))
                COCode_txt.SetText(name);
            return this;
        }

        public CustomOptionDetailPage EnterCustomOptionDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                CODescription_txt.SetText(description);
            return this;
        }

        public CustomOptionDetailPage EnterStructural(bool description)
        {
            Structural_chk.SetCheck(description);
            return this;
        }

        public CustomOptionDetailPage EnterCustomOptionPrice(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Price_txt.SetText(description);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlContent']/div[1]");
        }

        public void AddCustomOption(CustomOptionData CustomOption)
        {
            EnterCustomOptionCode(CustomOption.Code)
                .EnterCustomOptionDescription(CustomOption.Description)
                .EnterStructural(CustomOption.Structural)
                .EnterCustomOptionPrice(CustomOption.Price.ToString())
                .Save();
            PageLoad();
        }
    }

}
