
namespace Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule
{
    public class QuantityBuildingPhaseRuleData
    {
        public QuantityBuildingPhaseRuleData()
        {
            Priority = string.Empty;
            LevelCondition = string.Empty;
            OriginalBuildingPhase = string.Empty;
            NewBuildingPhase = string.Empty;
        }
        public QuantityBuildingPhaseRuleData(QuantityBuildingPhaseRuleData data)
        {
            Priority = data.Priority;
            LevelCondition = data.LevelCondition;
            OriginalBuildingPhase = data.OriginalBuildingPhase;
            NewBuildingPhase = data.NewBuildingPhase;
        }
        public string Priority { get; set; }
        public string LevelCondition { get; set; }
        public string OriginalBuildingPhase { get; set; }
        public string NewBuildingPhase { get; set; }
    }
}