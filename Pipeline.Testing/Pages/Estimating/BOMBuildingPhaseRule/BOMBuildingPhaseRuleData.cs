
namespace Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule
{
    public class BOMBuildingPhaseRuleData
    {
        public string OriginalProductBuildingPhase { get; set; }
        public string OriginalSubcomponentBuildingPhase { get; set; }
        public string NewSubcomponentBuildingPhase { get; set; }
        public BOMBuildingPhaseRuleData()
        {
            OriginalProductBuildingPhase = string.Empty;
            OriginalSubcomponentBuildingPhase = string.Empty;
            NewSubcomponentBuildingPhase = string.Empty;
        }
        public BOMBuildingPhaseRuleData(BOMBuildingPhaseRuleData data)
        {
            OriginalProductBuildingPhase = data.OriginalProductBuildingPhase;
            OriginalSubcomponentBuildingPhase = data.OriginalSubcomponentBuildingPhase;
            NewSubcomponentBuildingPhase = data.NewSubcomponentBuildingPhase;
        }
    }
}