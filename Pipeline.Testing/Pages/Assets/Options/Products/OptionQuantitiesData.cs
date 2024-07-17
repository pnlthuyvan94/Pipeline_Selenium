namespace Pipeline.Testing.Pages.Assets.Options.Products
{
    public class OptionQuantitiesData
    {
        public OptionQuantitiesData()
        {
            Community = string.Empty;
            House = string.Empty;
            OptionName = string.Empty;
            BuildingPhase = string.Empty;
            Category = string.Empty;
            ProductName = string.Empty;
            ProductDescription = string.Empty;
            Style = string.Empty;
            Condition = false;
            Operator = string.Empty;
            FinalCondition = string.Empty;
            Use = string.Empty;
            Quantity = string.Empty;
            Source = string.Empty;
            Dependent_Condition = string.Empty;
            Parameters = string.Empty;
        }
        public string Community { get; set; }
        public string House { get; set; }
        public string OptionName { get; set; }
        public string BuildingPhase { get; set; }
        public string Category { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Style { get; set; }
        public bool Condition { get; set; }
        public string Operator { get; set; }
        public string FinalCondition { get; set; }
        public string Use { get; set; }
        public string Quantity { get; set; }
        public string Source { get; set; }
        public string Dependent_Condition { get; set; }
        public string Parameters { get; set; }

    }
}