using System;

namespace Pipeline.Testing.Pages.Estimating.Uses.AddUses
{
    public partial class AddUsesModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(UsesDescription_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(UsesName_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(UsesSortOrder_txt.GetText()))
                    return false;
                return true;
            }
        }

        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Uses\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Use");
        }
    }
}
