using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule.AddBuildingPhaseRule
{
    public partial class AddBuildingPhaseRuleModal
    {
        public AddBuildingPhaseRuleModal EnterPriority(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Priority_txt.SetText(data);
            return this;
        }

        public AddBuildingPhaseRuleModal EnterLevel(string data)
        {
            if (!string.IsNullOrEmpty(data))
                LevelCondition_txt.SetText(data);
            return this;
        }

        public string SelectOriginalBuildingPhase(string data)
        {
            return OriginalBuildingPhase_ddl.SelectItemByValueOrIndex(data, 1);
        }

        public string SelectNewBuildingPhase(string data)
        {
            return NewBuildingPhase_ddl.SelectItemByValueOrIndex(data, 1);
        }

        public void Save()
        {
            Save_btn.Click();
            WaitGridLoad();
        }

        public QuantityBuildingPhaseRuleData AddBuildingPhaseRule(QuantityBuildingPhaseRuleData data)
        {
            EnterPriority(data.Priority)
                .EnterLevel(data.LevelCondition);
            data.OriginalBuildingPhase = SelectOriginalBuildingPhase(data.OriginalBuildingPhase);
            data.NewBuildingPhase = SelectNewBuildingPhase(data.NewBuildingPhase);
            Save();
            QuantityBuildingPhaseRuleData newdata = new QuantityBuildingPhaseRuleData(data)
            {
                OriginalBuildingPhase = data.OriginalBuildingPhase,
                NewBuildingPhase = data.NewBuildingPhase
            };
            return newdata;
        }
    }
}