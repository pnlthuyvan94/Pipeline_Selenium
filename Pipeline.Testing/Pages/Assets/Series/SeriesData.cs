namespace Pipeline.Testing.Pages.Assets.Series
{
    public class SeriesData
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string HousePlanNumber { get; set; }
        public string HouseName { get; set; }

        public SeriesData()
        {
            Name = string.Empty;
            Code = string.Empty;
            Description = string.Empty;
            HousePlanNumber = string.Empty;
            HouseName = string.Empty;
        }
    }
}
