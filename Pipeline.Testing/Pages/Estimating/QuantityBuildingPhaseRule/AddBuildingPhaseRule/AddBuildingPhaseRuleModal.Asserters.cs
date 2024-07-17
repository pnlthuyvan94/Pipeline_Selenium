using System;

namespace Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule.AddBuildingPhaseRule
{
    public partial class AddBuildingPhaseRuleModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(Priority_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(LevelCondition_txt.GetText()))
                    return false;
                return true;
            }
        }

        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Create Building Trade\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "New Building Phase Rules");
        }

    }
}
