using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Estimating.Manufactures.ManufacturerDetail
{
    public partial class ManufacturerDetailPage
    {
        public ManufacturerDetailPage EnterManufaturerName(string name)
        {
            if (string.IsNullOrEmpty(name) is false)
                Name_txt.SetText(name);
            return this;
        }

        public ManufacturerDetailPage EnterManufaturerUrl(string url)
        {
            if (string.IsNullOrEmpty(url) is false)
                Url_txt.SetText(url);
            return this;
        }

        public ManufacturerDetailPage EnterManufaturerDescription(string description)
        {
            if (string.IsNullOrEmpty(description) is false)
                Description_txt.SetText(description);
            return this;
        }

        public void Save()
        {
            ManufacturerSave_btn.Click();

            // Wait loading hide
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlContent']");
            PageLoad();
        }

        public void CloseModal()
        {
            ManufacturerAdd_btn.Click();
            System.Threading.Thread.Sleep(500);
        }

        public void UpdateManufacturer(ManufacturerData data)
        {
            EnterManufaturerDescription(data.Description).EnterManufaturerUrl(data.Url).Save();
        }
    }
}
