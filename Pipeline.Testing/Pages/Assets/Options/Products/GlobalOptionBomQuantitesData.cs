using Pipeline.Testing.Pages.Assets.House;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.Options.Products
{
    public class GlobalOptionBomQuantitesData
    {
        public string optionName;
        public string buildingPhase;
        public List<ProductToOptionData> productToOption;

        public GlobalOptionBomQuantitesData(GlobalOptionBomQuantitesData data)
        {
            optionName = data.optionName;
            buildingPhase = data.buildingPhase;
            productToOption = new List<ProductToOptionData>(data.productToOption);
        }
        public GlobalOptionBomQuantitesData()
        {
            buildingPhase = string.Empty;
            optionName = string.Empty;
            productToOption = new List<ProductToOptionData>();
        }
    }
}
