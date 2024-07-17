using OpenQA.Selenium;

namespace Pipeline.Testing.Pages.Assets.Series.AddSeries
{
    public partial class AddSeriesModal
    {
        public AddSeriesModal EnterSeriesCode(string code)
        {
            if (string.IsNullOrEmpty(code) is false)
                SeriesCode_txt.SetText(code);
            return this;
        }

        public AddSeriesModal EnterSeriesName(string name)
        {
            SeriesName_txt.SetText(name);
            return this;
        }

        public AddSeriesModal EnterSeriesDescription(string description)
        {
            if (string.IsNullOrEmpty(description) is false)
                SeriesDescription_txt.SetText(description);
            return this;
        }

        public string Save()
        {
            SeriesSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlContent']/div[1]");
            return GetLastestToastMessage();
        }

        public void CloseModal()
        {
            if (!IsModalDisplayed())
                return;

            SeriesClose_btn.Click();
            System.Threading.Thread.Sleep(500);
        }

    }
}
