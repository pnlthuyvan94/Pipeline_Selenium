

using System;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules.AddBOMLogicRule
{
    public partial class AddBOMLogicRuleModal
    {
        public bool IsModalDisplayed()
        {
            if (!AddBOMLogicRule_lbl.WaitForElementIsVisible(10))
            {
                // Wait to title visible
                throw new Exception("Not found " + AddBOMLogicRule_lbl.GetText() + " element");
            }
            return (AddBOMLogicRule_lbl.GetText() == "Create Rule");
        }


    }
}
