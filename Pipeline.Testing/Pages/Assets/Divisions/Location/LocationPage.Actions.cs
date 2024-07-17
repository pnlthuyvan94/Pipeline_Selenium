using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Divisions.Location
{
    public partial class LocationPage : DetailsContentPage<LocationPage>
    {

        public LocationPage EnterLatitude(string latitude)
        {
            Latitude_txt.SetText(latitude);
            return this;
        }

        public LocationPage EnterLongitude(string longitude)
        {
            Longitude_txt.SetText(longitude);
            return this;
        }

        public void SaveLocation()
        {
            SaveLocation_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveDivisionLocation']/div[1]");
        }

        public void ZoomInLocation()
        {
            ZoomIn_btn.Click();
        }

        public void ZoomOutLocation()
        {
            ZoomOut_btn.Click();
        }

        public void FullSceenLocation()
        {
            FullScreen_btn.Click();
        }

        public void ExitFullSceenLocation()
        {
            ExitFullScreen_btn.JavaScriptClick();
        }

        public LocationPage UpdateLocation(string latitude, string longitude)
        {
            EnterLatitude(latitude).EnterLongitude(longitude).SaveLocation();
            return this;
        }
    }

}
