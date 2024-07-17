using System;

namespace Pipeline.Testing.Pages.Assets.OptionRooms.AddOptionRoom
{
    public partial class AddOptionRoomModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(OptionRoomName_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(SortOrder_txt.GetText()))
                    return false;
                return true;
            }
        }

        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Create Option Room\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Option Room");
        }
    }

}
