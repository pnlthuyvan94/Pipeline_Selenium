
using System;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail.AddOptionSelectionToGroup
{
    public partial class AddOptionSelectionToGroupModal
    {
        public bool IsModalDisplayed()
        {
            if (!AddOptionSelectionToGroupTitle_lbl.WaitForElementIsVisible(10))
            {
                // Wait to title visible
                throw new Exception("Not found " + AddOptionSelectionToGroupTitle_lbl.GetText() + " element");
            }
            return (AddOptionSelectionToGroupTitle_lbl.GetText() == "Add an Option Selection");
        }

    }
}
