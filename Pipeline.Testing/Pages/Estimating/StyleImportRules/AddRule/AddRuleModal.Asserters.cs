using System;

namespace Pipeline.Testing.Pages.Estimating.StyleImportRules.AddRule
{
    public partial class AddRuleModal
    {
        public bool IsModalDisplayed
        {
            get
            {
                return ModalTitle_lbl.WaitForElementIsVisible(5) && ModalTitle_lbl.GetText() == "Add Rule";
            }
        }

    }
}
