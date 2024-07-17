using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Communities.Location
{
    public partial class CommunityLocationPage : DetailsContentPage<CommunityLocationPage>
    {

        public CommunityLocationPage EnterLatitude(string latitude)
        {
            Latitude_txt.SetText(latitude);
            return this;
        }

        public CommunityLocationPage EnterLongitude(string longitude)
        {
            Longitude_txt.SetText(longitude);
            return this;
        }

        public void SaveLocation()
        {
            SaveLocation_btn.Click();
        }

        public void ZoomInLocation()
        {
            ZoomIn_btn.JavaScriptClick();
        }

        public void ZoomOutLocation()
        {
            ZoomOut_btn.JavaScriptClick();
        }

        public void FullSceenLocation()
        {
            FullScreen_btn.JavaScriptClick();
        }

        public void ExitFullSceenLocation()
        {
            ExitFullScreen_btn.JavaScriptClick();
        }

        public CommunityLocationPage UpdateLocation(string latitude, string longitude)
        {
            EnterLatitude(latitude).EnterLongitude(longitude).SaveLocation();
            PageLoad();
            return this;
        }
    }

}
