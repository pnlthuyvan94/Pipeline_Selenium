using System;

namespace Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule.AddBuildingPhaseRule
{
    public partial class AddBuildingPhaseRuleModal
    {
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add New BOM Building Phase Rule\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add New BOM Building Phase Rule");
        }

    }
}
