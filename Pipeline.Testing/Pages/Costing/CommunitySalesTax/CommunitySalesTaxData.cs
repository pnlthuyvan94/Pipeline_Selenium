

namespace Pipeline.Testing.Pages.Costing.CommunitySalesTax
{
    public class CommunitySalesTaxData
    {
        public string Community { get; set; }
        public string TaxGroup { get; set; }
        public string BuildingPhase { get; set; }
        public string TaxGroupOverride { get; set; }
        public CommunitySalesTaxData()
        {
            Community = string.Empty;
            TaxGroup = string.Empty;
            BuildingPhase = string.Empty;
            TaxGroupOverride = string.Empty;
        }
    }
}
