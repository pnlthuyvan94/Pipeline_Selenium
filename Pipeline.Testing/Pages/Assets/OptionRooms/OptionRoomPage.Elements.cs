using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.OptionRooms.AddOptionRoom;


namespace Pipeline.Testing.Pages.Assets.OptionRooms
{
    public partial class OptionRoomPage : DashboardContentPage<OptionRoomPage>
    {
        public AddOptionRoomModal AddOptionRoomModal { get; private set; }

        protected IGrid OptionRoomPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptionRooms_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionRooms']/div[1]");

    }
}
