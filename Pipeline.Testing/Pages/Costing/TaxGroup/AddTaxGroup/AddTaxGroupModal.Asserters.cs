using System;

namespace Pipeline.Testing.Pages.Costing.TaxGroup.AddTaxGroup
{
    public partial class AddTaxGroupModal
    {
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Tax Group\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Tax Group");
        }

    }
}
