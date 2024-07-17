using System;

namespace Pipeline.Testing.Pages.Pathway.DesignerView.AddDesignerView
{
    public partial class AddDesignerViewModal
    {
        /*
         * Check Adding model is displayed or not
         */
        public bool IsModalDisplayed()
        {
            if (ModalTitle_lbl.WaitForElementIsVisible(10))
                return (ModalTitle_lbl.GetText() == "Add View");
            else
                return false;
        }
    }
}
