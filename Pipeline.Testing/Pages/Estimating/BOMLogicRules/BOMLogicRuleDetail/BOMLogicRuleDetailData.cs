

using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules.BOMLogicRuleDetail
{
    public class BOMLogicRuleDetailData
    {
        public string ConditionKey { get; set; }
        public string ActionKey { get; set; }
        public string ConditionKeyAttribute { get; set; }
        public string Operator { get; set; }
        public List<string> ConditionValue { get; set; }


        public BOMLogicRuleDetailData()
        {
            ConditionKey = string.Empty;
            ConditionKeyAttribute = string.Empty;
            Operator = string.Empty;
            ConditionValue = new List<string>();
            ActionKey = string.Empty;
        }
    }
}
