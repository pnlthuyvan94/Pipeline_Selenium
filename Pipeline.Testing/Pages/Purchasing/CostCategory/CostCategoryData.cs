namespace Pipeline.Testing.Pages.Purchasing.CostCategory
{
    public class CostCategoryData
    {
        public CostCategoryData()
        {
            Name = string.Empty;
            Description = string.Empty;
            CostType = string.Empty;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CostType { get; set; }
    }
}