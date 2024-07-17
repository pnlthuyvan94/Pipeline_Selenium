using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.Location
{
    public partial class CommunityLocationPage : DetailsContentPage<CommunityLocationPage>
    {
        protected Textbox Latitude_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtGLat']");

        protected Textbox Longitude_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtGLng']");

        protected Button SaveLocation_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveLocation']");

        protected Button ZoomIn_btn
            => new Button(FindType.XPath, "//button[@Title='Zoom in']");

        protected Button ZoomOut_btn
            => new Button(FindType.XPath, "//button[@Title='Zoom out']");

        protected Button FullScreen_btn
            => new Button(FindType.XPath, "//*[@class='gm-control-active gm-fullscreen-control']");

        protected Button ExitFullScreen_btn
            => new Button(FindType.XPath, "//*[@class='gm-control-active gm-fullscreen-control']");

        protected Label Map_lbl
            => new Label(FindType.XPath, "//*[@id='map']");
    }
}
