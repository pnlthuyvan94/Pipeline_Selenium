namespace Pipeline.Testing.Pages.Assets.Communities.Phase
{
    public class CommunityPhaseData
    {
        public CommunityPhaseData()
        {
            Name = string.Empty;
            Code = string.Empty;
            Status = string.Empty;
            SortOrder = string.Empty;
            Description = string.Empty;
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string SortOrder { get; set; }
        public string Description { get; set; }
    }
}