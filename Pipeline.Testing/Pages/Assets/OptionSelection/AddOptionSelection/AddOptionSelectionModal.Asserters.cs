using System;

namespace Pipeline.Testing.Pages.Assets.OptionSelection.AddOptionSelection
{
    public partial class AddOptionSelectionModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(OptionSelectionName_txt.GetText()))
                    return false;
                if (!"NONE".Equals(OptionSelectionGroup_ddl.SelectedItem.Text))
                    return false;
                if (Customizable_cb.IsChecked)
                    return false;
                return true;
            }
        }

        public bool IsCustomizableChecked()
        {
            return (Customizable_cb.IsChecked);
        }

        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Option Selection\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Option Selection");
        }

        public bool IsModalClosed()
        {
            if (!ModalTitle_lbl.WaitForElementIsInVisible(5))
                return false;
            return true;
        }
    }
}
