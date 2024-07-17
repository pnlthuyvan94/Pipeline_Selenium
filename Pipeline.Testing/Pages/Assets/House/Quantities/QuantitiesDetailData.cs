using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Assets.House.Quantities
{
    public class QuantitiesDetailData
    {
        public string Option { get; set; }
        public string DependentCondition { get; set; }
        public string BuildingPhase { get; set; }
        public string Products { get; set; }
        public string Description { get; set; }
        public string Style { get; set; }
        public string Use { get; set; }
        public string Parameters { get; set; }
        public string Quantity { get; set; }
        public string Source { get; set; }

        public QuantitiesDetailData() { }

        public QuantitiesDetailData(QuantitiesDetailData data)
        {
            Option = data.Option;
            DependentCondition = data.DependentCondition;
            BuildingPhase = data.BuildingPhase;
            Products = data.BuildingPhase;
            Description = data.Description;
            Style = data.Style;
            Use = data.Use;
            Parameters = data.Parameters;
            Quantity = data.Quantity;
            Source = data.Source;
        }
    }
  
}
