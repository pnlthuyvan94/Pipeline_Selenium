using System;

namespace Pipeline.Testing.Pages.Estimating.Units.AddUnit
{
    public partial class AddUnitPage
    {
        /*
         * Check Adding model is displayed or not
         */
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Unit\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Unit");
        }

    }
}
