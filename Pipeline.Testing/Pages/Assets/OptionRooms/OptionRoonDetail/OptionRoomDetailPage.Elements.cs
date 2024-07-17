using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail.AddOptionsToOptionRoom;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail
{
    public partial class OptionRoomDetailPage : DetailsContentPage<OptionRoomDetailPage>
    {
        public AddOptionToRoomModal AddOptionToRoomModal { get; private set; }
        // Option Room detail

        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox SortOrder_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSortOrder']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        string loadingIcon => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbLoadingAnimation']/div[1]";

        // Options
        protected Label OptionTitle_lbl => new Label(FindType.XPath, "//h1[text()='Options']");

        protected Button AddOptionToRoom_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddOption']");

        protected IGrid Options_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelectionInfo_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionInfo']/div[1]");

    }
}
