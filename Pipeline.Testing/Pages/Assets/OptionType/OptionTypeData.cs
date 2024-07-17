namespace Pipeline.Testing.Pages.Assets.OptionType
{
    public class OptionTypeData
    {
        public OptionTypeData()
        {
            Name = string.Empty;
            SortOrder = string.Empty;
            DisplayName = string.Empty;
            IsFlexPlan = false;
            IsFlexPlan = false;
        }

        public string Name { get; set; }
        public string SortOrder { get; set; }
        public string DisplayName { get; set; }
        public bool IsPathwayVisible { get; set; }
        public bool IsFlexPlan { get; set; }

    }
}