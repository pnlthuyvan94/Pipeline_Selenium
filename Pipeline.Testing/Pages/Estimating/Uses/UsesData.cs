
namespace Pipeline.Testing.Pages.Estimating.Uses.AddUses
{
    public class UsesData
    {
        public UsesData()
        {
            Name = string.Empty;
            Description = string.Empty;
            SortOrder = string.Empty;
        }
        public string Name { get; set; }
        public string SortOrder { get; set; }

        public string Description { get; set; }
    }
}