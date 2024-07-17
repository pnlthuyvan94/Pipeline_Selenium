
using System;

namespace Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail.AddOptionsToOptionRoom
{
    public partial class AddOptionToRoomModal
    {
        public bool IsModalDisplayed()
        {
            if (!AddOptionToRoomTitle_lbl.WaitForElementIsVisible(10))
            {
                // Wait to title visible
                throw new Exception("Not found " + AddOptionToRoomTitle_lbl.GetText() + " element");
            }
            return (AddOptionToRoomTitle_lbl.GetText() == "Add Option to Option Room");
        }
    }
}
