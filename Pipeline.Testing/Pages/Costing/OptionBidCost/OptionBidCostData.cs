

namespace Pipeline.Testing.Pages.Costing.OptionBidCost
{
    public class OptionBidCostData
    {
        public string Tier1Option { get; set; }
        public string Tier2House { get; set; }
        public string Tier2Option { get; set; }
        public string Tier3Commmunity { get; set; }
        public string Tier3Option { get; set; }
        public string Tier4Community { get; set; }
        public string Tier4House { get; set; }
        public string Tier4Option { get; set; }
        public string Tier1Cost { get; set; }
        public string Tier2Cost { get; set; }
        public string Tier3Cost { get; set; }
        public string Tier4Cost { get; set; }
        public string Tier4Cost2 { get; set; }
        public string Job { get; set; }
        public string JobOption { get; set; }
        public string JobCost { get; set; }
        public string JobCost2 { get; set; }
        public string Vendor { get; set; }
        public string BuildingPhase1 { get; set; }
        public string BuildingPhase2 { get; set; }
        public string BuildingPhase3 { get; set; }
        public string BuildingPhase4 { get; set; }
        public string BuildingPhase5 { get; set; }

        public OptionBidCostData(OptionBidCostData data)
        {
            Tier1Option = data.Tier1Option;
            Tier2House = data.Tier2House;
            Tier2Option = data.Tier2Option;
            Tier3Commmunity = data.Tier3Commmunity;
            Tier3Option = data.Tier3Option;
            Tier4Community = data.Tier4Community;
            Tier4House = data.Tier4House;
            Tier4Option = data.Tier4Option;
            Tier1Cost = data.Tier1Cost;
            Tier2Cost = data.Tier2Cost;
            Tier3Cost = data.Tier3Cost;
            Tier4Cost = data.Tier4Cost;
            Tier4Cost2 = data.Tier4Cost2;
            Job = data.Job;
            JobOption = data.JobOption;
            JobCost = data.JobCost;
            JobCost2 = data.JobCost2;
            Vendor = data.Vendor;
            BuildingPhase1 = data.BuildingPhase1;
            BuildingPhase2 = data.BuildingPhase2;
            BuildingPhase3 = data.BuildingPhase3;
            BuildingPhase4 = data.BuildingPhase4;
            BuildingPhase5 = data.BuildingPhase5;
        }

        public OptionBidCostData()
        {
            Tier1Option = string.Empty;
            Tier2House = string.Empty;
            Tier2Option = string.Empty;
            Tier3Commmunity = string.Empty;
            Tier3Option = string.Empty;
            Tier4Community = string.Empty;
            Tier4House = string.Empty;
            Tier4Option = string.Empty;
            Tier1Cost = string.Empty;
            Tier2Cost = string.Empty;
            Tier3Cost = string.Empty;
            Tier4Cost = string.Empty;
            Tier4Cost2 = string.Empty;
            Job = string.Empty;
            JobOption = string.Empty;
            JobCost = string.Empty;
            JobCost2 = string.Empty;
            Vendor = string.Empty;
            BuildingPhase1 = string.Empty;
            BuildingPhase2 = string.Empty;
            BuildingPhase3 = string.Empty;
            BuildingPhase4 = string.Empty;
            BuildingPhase5 = string.Empty;
        }
    }
}
