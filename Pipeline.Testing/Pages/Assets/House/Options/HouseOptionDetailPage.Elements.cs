using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Assets.House.Options
{
    public partial class HouseOptionDetailPage : DetailsContentPage<HouseOptionDetailPage>
    {
        protected Button AddElevation_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddElevation");
        protected IGrid Elevation_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgElevations_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgElevations']/div[1]");
        protected Button AddOption_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddOption");
        protected IGrid Option_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgOptions_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        protected Label AddElevationModalTitle_Lbl => new Label(FindType.XPath, "//*[@id='elevations-modal']/section/header/h1");
        protected Label AddOptionModalTitle_Lbl => new Label(FindType.XPath, "//*[@id='options-modal']/section/header/h1");
        protected Button CloseElevationModal_btn => new Button(FindType.XPath, "//*[@id='elevations-modal']/section/header/a");
        protected Button CloseOptionModal_btn => new Button(FindType.XPath, "//*[@id='options-modal']/section/header/a");
        protected Button InsertElevationToHouse_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertElevation");
        protected Button InsertOptionToHouse_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertOption");
        protected Button RemoveImg_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbRemoveElevationImage");
        protected Label ImageThumbnail => new Label(FindType.Id, "ctl00_CPH_Content_imgElevation");
        protected IGrid OptionHouse_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgOptions_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");

    }

}
