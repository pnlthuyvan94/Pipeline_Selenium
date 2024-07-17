

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules
{
    public class BOMLogicRuleData
    {
        public string RuleName { get; set; }
        public string RuleDescription { get; set; }
        public string SortOrder { get; set; }
        public string Execution { get; set; }

        public BOMLogicRuleData()
        {
            RuleName = string.Empty;
            RuleDescription = string.Empty;
            SortOrder = string.Empty;
            Execution = string.Empty;
        }
    }
}
