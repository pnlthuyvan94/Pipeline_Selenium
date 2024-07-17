
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Jobs.Job.Quantities
{
    public class JobQuantitiesData
    {
        public string Option;
        public string Source;
        public List<string> BuildingPhase;
        public List<string> Products;
        public string Style;
        public string Code;
        public string Use;
        public List<string> Quantity;
        public JobQuantitiesData()
        {
            Option = string.Empty;
            Source = string.Empty;
            BuildingPhase = new List<string>();
            Products = new List<string>();
            Style = string.Empty;
            Code = string.Empty;
            Use = string.Empty;
            Quantity = new List<string>();
        }

    }
}
