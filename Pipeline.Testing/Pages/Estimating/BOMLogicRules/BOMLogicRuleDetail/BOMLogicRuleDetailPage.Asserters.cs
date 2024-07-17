using System;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules.BOMLogicRuleDetail
{
    public partial class BOMLogicRuleDetailPage
    {
        public bool IsModalDisplayed()
        {
            if (!AddCondition_lbl.WaitForElementIsVisible(10))
            {
                // Wait to title visible
                throw new Exception("Not found " + AddCondition_lbl.GetText() + " element");
            }
            return (AddCondition_lbl.GetText() == "Create a Condition");
        }

        public bool IsActionModalDisplayed()
        {
            if (!AddAction_lbl.WaitForElementIsVisible(10))
            {
                // Wait to title visible
                throw new Exception("Not found " + AddAction_lbl.GetText() + " element");
            }
            return (AddAction_lbl.GetText() == "Create an Action");
        }
    }
}
