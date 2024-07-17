
namespace Pipeline.Testing.Pages.Purchasing.CostType
{
    public class CostTypeData
    {
        public CostTypeData()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string TaxGroup { get; set; }
    }
}