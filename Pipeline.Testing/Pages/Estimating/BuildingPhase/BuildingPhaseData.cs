namespace Pipeline.Testing.Pages.Estimating.BuildingPhase
{
    public class BuildingPhaseData
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string AbbName { get; set; }
        public string Description { get; set; }
        public string BuildingGroupName { get; set; }
        public string BuildingGroup { get { return BuildingGroupCode + "-" + BuildingGroupName; } }
        public string BuildingGroupCode { get; set; }
        public string Type { get; set; }
        public string Parent { get; set; }
        public string PercentBilled { get; set; }
        public bool Taxable { get; set; }

        public BuildingPhaseData()
        {
            Code = string.Empty;
            Name = string.Empty;
            AbbName = string.Empty;
            Description = string.Empty;
            BuildingGroupName = string.Empty;
            BuildingGroupCode = string.Empty;
            Type = "NONE";
            Parent = "NONE";
            PercentBilled = string.Empty;
            Taxable = false;
        }
        public BuildingPhaseData(BuildingPhaseData data)
        {
            Code = data.Code;
            Name = data.Name;
            AbbName = data.AbbName;
            Description = data.Description;
            BuildingGroupName = data.BuildingGroupName;
            BuildingGroupCode = data.BuildingGroupCode;
            Type = data.Type;
            Parent = data.Parent;
            PercentBilled = data.PercentBilled;
            Taxable = data.Taxable;
        }
    }
}
