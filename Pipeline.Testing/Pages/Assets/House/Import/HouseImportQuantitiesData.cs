

using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.House.Import
{
    public class HouseImportQuantitiesData
    {
        public string HouseName { get; set; }
        public string Community { get; set; }
        public string OptionName { get; set; }

        public List<string> BuildingPhases { get; set; }

        public List<string> Products { get; set; }
        public List<string> Description { get; set; }

        public List<string> Styles { get; set; }
        public List<string> Manufacturers { get; set; }

        public List<string> Parameter { get; set; }

        public HouseImportQuantitiesData()
        {
            HouseName = string.Empty;
            Community = string.Empty;
            OptionName = string.Empty;
            BuildingPhases = new List<string>();
            Products = new List<string>();
            Description = new List<string>();
            Styles = new List<string>();
            Manufacturers = new List<string>();
            Parameter = new List<string>();
        }
    }
}
