using System;

namespace Pipeline.Testing.Pages.Estimating.SpecSet.AddSpecSetGroup
{
    public partial class AddSpecSetGroupModal
    {
        /*
         * Check Adding model is displayed or not
         */
        public bool IsModalDisplayed()
        {
            ModalTitle_lbl.WaitForElementIsVisible(5);
            return (ModalTitle_lbl.GetText() == "Add SpecSet Group");
        }

    }
}
