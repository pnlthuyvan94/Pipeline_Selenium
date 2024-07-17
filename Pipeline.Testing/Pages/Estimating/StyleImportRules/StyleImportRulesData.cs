

namespace Pipeline.Testing.Pages.Estimating.StyleImportRules
{
    public class StyleImportRulesData
    {
        public bool Active { get; set; }
        public string BuildingPhase { get; set; }
        public string Styles { get; set; }
        public string DefaultStyle { get; set; }
        public StyleImportRulesData()
        {
            Active =  true;
            BuildingPhase = string.Empty;
            Styles = string.Empty;
            DefaultStyle = string.Empty;
        }
        public StyleImportRulesData(StyleImportRulesData data)
        {
            Active = true;
            BuildingPhase = data.BuildingPhase;
            Styles = data.Styles;
            DefaultStyle = data.DefaultStyle;
        }
    }
}
