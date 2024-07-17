using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail.AddOptionsToOptionRoom
{
    public partial class AddOptionToRoomModal : DetailsContentPage<AddOptionToRoomModal>
    {
        // Add Building Phase to Building Group
        protected Label AddOptionToRoomTitle_lbl => new Label(FindType.XPath, "//h1[text()='Add Option to Option Room']");

        protected Button AddOptionToRoom_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSO']");

        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");

        string OptionListXpath => "//*[@id='ctl00_CPH_Content_rlbSOs']/div/ul/li";
        protected ListItem OptionList_lst => new ListItem(FindElementHelper.FindElements(FindType.XPath, OptionListXpath).ToList());

        string loadingIcon => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rlbSOs']/div[1]";
    }
}
