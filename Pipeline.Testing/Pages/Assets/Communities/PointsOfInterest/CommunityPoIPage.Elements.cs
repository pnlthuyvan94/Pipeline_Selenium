using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Communities.PointsOfInterest
{
    public partial class CommunityPoIPage : DetailsContentPage<CommunityPoIPage>
    {
        private string _loadingGifXpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPOI']/div[1]";
        protected IGrid POI_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPOI_ctl00']", _loadingGifXpath);
        protected Textbox Title_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtPTitle");
        protected Textbox Description_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDescription");
        protected Textbox Lat_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtLat");
        protected Textbox Long_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtLong");
        protected CheckBox Published_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_chkPublished");
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSave");
        protected Label HeaderNameOfPage_Lbl => new Label(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[2]/div/article/section[2]/div[1]/article/section/article/header/h1");
        protected Label SubHead_Lbl => new Label(FindType.Id, "ctl00_CPH_Content_lblTitle2");
        protected Button Add_Btn => new Button(FindType.Id, "ctl00_CPH_Content_hypAddPOI");
    }
}
