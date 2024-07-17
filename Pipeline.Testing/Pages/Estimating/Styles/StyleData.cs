namespace Pipeline.Testing.Pages.Estimating.Styles
{
    public class StyleData
    {
        public StyleData()
        {
            Name = string.Empty;
            Manufacturer = string.Empty;
            Url = string.Empty;
            Description = string.Empty;
        }

        public StyleData(StyleData data)
        {
            Name = data.Name;
            Manufacturer = data.Manufacturer;
            Url = data.Url;
            Description = data.Description;
        }

        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }

    }
}