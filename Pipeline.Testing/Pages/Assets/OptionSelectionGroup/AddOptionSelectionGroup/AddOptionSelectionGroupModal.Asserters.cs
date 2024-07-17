using System;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.AddOptionSelectionGroup
{
    public partial class AddOptionSelectionGroupModal
    {
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Option Selection group\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Option Selection Group");
        }

        public bool IsModalClosed()
        {
            if (!ModalTitle_lbl.WaitForElementIsInVisible(5))
                return false;
            return true;
        }
    }
}
