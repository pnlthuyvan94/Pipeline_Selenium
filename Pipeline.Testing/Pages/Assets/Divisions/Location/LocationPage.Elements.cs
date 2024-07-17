using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Divisions.Location
{
    public partial class LocationPage : DetailsContentPage<LocationPage>
    {
        protected Label LocationModelTilte_lbl
            => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlGoogleMap']/div/article/section/article/header/div/h1");

        protected Textbox Latitude_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtGLat']");

        protected Textbox Longitude_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtGLng']");

        protected Button SaveLocation_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveDivisionLocation']");

        protected Button ZoomIn_btn
            => new Button(FindType.XPath, "//button[@Title='Zoom in'] | //*[@id='map']/div[1]/div/div[13]/div/div[3]/div/button[1]");

        protected Button ZoomOut_btn
            => new Button(FindType.XPath, "//button[@Title='Zoom out'] | //*[@id='map']/div[1]/div/div[13]/div/div[3]/div/button[2]");

        protected Button FullScreen_btn
            => new Button(FindType.XPath, "//*[@class='gm-control-active gm-fullscreen-control']");

        protected Button ExitFullScreen_btn
            => new Button(FindType.XPath, "//*[@class='gm-control-active gm-fullscreen-control']");
    }
}
