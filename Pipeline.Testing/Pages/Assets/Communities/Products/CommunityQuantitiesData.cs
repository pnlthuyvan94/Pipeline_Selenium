

namespace Pipeline.Testing.Pages.Assets.Communities.Products
{
    public class CommunityQuantitiesData
    {
        public CommunityQuantitiesData()
        {
            OptionName = string.Empty;
            BuildingPhase = string.Empty;
            ProductName = string.Empty;
            ProductDescription = string.Empty;
            Style = string.Empty;
            Condition = false;
            Use = string.Empty;
            Quantity = string.Empty;
            Source = string.Empty;
            Option = string.Empty;
        }
        public string OptionName { get; set; }
        public string BuildingPhase { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Style { get; set; }
        public bool Condition { get; set; }
        public string Use { get; set; }
        public string Quantity { get; set; }
        public string Source { get; set; }
        public string Option { get; set; }
    }
}
