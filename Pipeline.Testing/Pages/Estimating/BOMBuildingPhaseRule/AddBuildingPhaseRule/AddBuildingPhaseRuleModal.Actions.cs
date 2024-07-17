using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule.AddBuildingPhaseRule
{
    public partial class AddBuildingPhaseRuleModal
    {
        public string SelectOriginalProductBuildingPhase(string data)
        {
            return OriginalProductBuildingPhase_ddl.SelectItemByValueOrIndex(data, 2);
        }

        public string SelectOriginalSubBuildingPhase(string data)
        {
            return OriginalSubBuildingPhase_ddl.SelectItemByValueOrIndex(data, 2);
        }

        public string SelectNewSubBuildingPhase(string data)
        {
            return NewSubBuildingPhase_ddl.SelectItemByValueOrIndex(data, 2);
        }

        public void Save()
        {
            Save_btn.Click();
        }

        public BOMBuildingPhaseRuleData AddBuildingPhaseRule(BOMBuildingPhaseRuleData data)
        {
            data.OriginalProductBuildingPhase = SelectOriginalProductBuildingPhase(data.OriginalProductBuildingPhase);
            data.OriginalSubcomponentBuildingPhase = SelectOriginalSubBuildingPhase(data.OriginalSubcomponentBuildingPhase);
            data.NewSubcomponentBuildingPhase = SelectNewSubBuildingPhase(data.NewSubcomponentBuildingPhase);
            Save();
            BOMBuildingPhaseRuleData newdata = new BOMBuildingPhaseRuleData(data)
            {
                OriginalProductBuildingPhase= data.OriginalProductBuildingPhase,
                OriginalSubcomponentBuildingPhase= data.OriginalSubcomponentBuildingPhase,
                NewSubcomponentBuildingPhase= data.NewSubcomponentBuildingPhase
            };
            return  newdata;
        }
    }
}