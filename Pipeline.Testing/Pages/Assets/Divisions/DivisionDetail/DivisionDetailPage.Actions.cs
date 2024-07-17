using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail
{
    public partial class DivisionDetailPage : DetailsContentPage<DivisionDetailPage>
    {
        public DivisionDetailPage EnterDivisionName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                DivisionName_txt.SetText(name);
            return this;
        }

        public DivisionDetailPage EnterDivisionAddress(string description)
        {
            if (!string.IsNullOrEmpty(description))
                DivisionAddress_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterCity(string description)
        {
            if (!string.IsNullOrEmpty(description))
                City_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterDivisionState(string description)
        {
            if (!string.IsNullOrEmpty(description))
                State_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterDivisionZip(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Zip_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterPhone(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Phone_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterFax(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Fax_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterMainEmail(string description)
        {
            if (!string.IsNullOrEmpty(description))
                MainEmail_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterServiceEmail(string description)
        {
            if (!string.IsNullOrEmpty(description))
                ServiceEmail_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterSlug(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Slug_txt.SetText(description);
            return this;
        }

        public DivisionDetailPage EnterDivisionDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Description_txt.SetText(description);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveDivision']/div[1]");
        }

        public void AddDivision(DivisionData division)
        {
            EnterDivisionName(division.Name)
                .EnterDivisionAddress(division.Address)
                .EnterCity(division.City)
                .EnterDivisionState(division.State)
                .EnterDivisionZip(division.Zip)
                .EnterPhone(division.Phone)
                .EnterFax(division.Fax)
                .EnterMainEmail(division.MainEmail)
                .EnterServiceEmail(division.ServicesEmail)
                .EnterSlug(division.Slug)
                .EnterDivisionDescription(division.Description)
                .Save();
            

            Label loadingGif = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveDivision']/div[1]", 1);
            loadingGif.WaitUntilExist(1);
            loadingGif.WaitForElementIsInVisible(5);
            PageLoad();
        }

        public void UpdateDivision(DivisionData division)
        {
            EnterDivisionName(division.Name)
                .EnterDivisionAddress(division.Address)
                .EnterCity(division.City)
                .EnterDivisionState(division.State)
                .EnterDivisionZip(division.Zip)
                .EnterPhone(division.Phone)
                .EnterFax(division.Fax)
                .EnterMainEmail(division.MainEmail)
                .EnterServiceEmail(division.ServicesEmail)
                .EnterSlug(division.Slug)
                .EnterDivisionDescription(division.Description)
                .Save();
        }
    }

}
