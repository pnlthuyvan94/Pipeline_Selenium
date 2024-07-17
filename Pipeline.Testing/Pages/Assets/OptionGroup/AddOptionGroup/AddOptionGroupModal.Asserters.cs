using System;

namespace Pipeline.Testing.Pages.Assets.OptionGroup.AddOptionGroup
{
    public partial class AddOptionGroupModal
    {

        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Option Selection group\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Option Group");
        }
    }
}
