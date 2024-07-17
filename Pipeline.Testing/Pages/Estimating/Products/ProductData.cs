using Pipeline.Testing.Pages.Assets.House.Quantities;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Products
{
    public class ProductData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public string SKU { get; set; }
        public string RoundingUnit { get; set; }
        public string RoundingRule { get; set; }
        public string Waste { get; set; }
        public bool Supplemental { get; set; }
        public string Manufacture { get; set; }
        public string Style { get; set; }
        public string Category { get; set; }
        public string BuildingPhase { get; set; }
        public string Use { get; set; }
        public string Quantities { get; set; }
        public string Parameter { get; set; }

        public ProductData(ProductData data)
        {
            Name = data.Name;
            Description = data.Description;
            Notes = data.Notes;
            Code = data.Code;
            Unit = data.Unit;
            SKU = data.SKU;
            RoundingUnit = data.RoundingUnit;
            RoundingRule = data.RoundingRule;
            Waste = data.Waste;
            Supplemental = data.Supplemental;
            Manufacture = data.Manufacture;
            Style = data.Style;
            Category = data.Category;
            Use = data.Use;
            Quantities = data.Quantities;
            Unit = data.Unit;
            BuildingPhase = data.BuildingPhase;
            Parameter = data.Parameter;
        }

        public ProductData()
        {
            Name = string.Empty;
            Description = string.Empty;
            Notes = string.Empty;
            Code = string.Empty;
            Unit = string.Empty;
            SKU = string.Empty;
            RoundingUnit = string.Empty;
            RoundingRule = string.Empty;
            Waste = string.Empty;
            Supplemental = false;
            Manufacture = string.Empty;
            Style = string.Empty;
            Category = string.Empty;
            BuildingPhase = string.Empty;
            Use = string.Empty;
            Quantities = string.Empty;
            Parameter = string.Empty;
        }
    }
}
