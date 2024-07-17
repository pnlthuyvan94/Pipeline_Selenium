namespace Pipeline.Testing.Pages.Resources
{
    public class ResourceData
    {
        public ResourceData()
        {
            Type = string.Empty;
            Title = string.Empty;
            Link = string.Empty;
            Source = string.Empty;
            UpdatedTitle = string.Empty;
        }

        public string Type { get; set; }
        public string Title { get; set; }
        public string UpdatedTitle { get; set; }
        public string Link { get; set; }
        public string Source { get; set; }
    }
}