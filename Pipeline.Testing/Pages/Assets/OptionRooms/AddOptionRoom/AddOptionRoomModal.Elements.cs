using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionRooms.AddOptionRoom
{
    public partial class AddOptionRoomModal : OptionRoomPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='optionRooms-modal']/section/header/h1");

        protected Textbox OptionRoomName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox SortOrder_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSortOrder']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertOptionRoom']");

    }

}
