namespace Pipeline.Testing.Pages.Assets.Options.Bid_Costs
{
    public class OptionBuildingPhaseData
    {
        public OptionBuildingPhaseData()
        {
            OptionName = string.Empty;
            BuildingPhase = new string[0];
            Name = string.Empty;
            Description = string.Empty;
            Allowance = 0.00;
        }
        public string OptionName { get; set; }
        public string[] BuildingPhase { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Allowance { get; set; }
    }
}