using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.House
{
    public class HouseQuantitiesData
    {
        public string communityCode;
        public string communityName;
        public string houseName;
        public string optionName;
        public string dependentCondition;
        public List<ProductToOptionData> productToOption;

        /// <summary>
        /// Product Quantities class using in House/ Quantities page
        /// </summary>
        public HouseQuantitiesData(HouseQuantitiesData data)
        {
            communityCode = data.communityCode;
            communityName = data.communityName;
            houseName = data.houseName;
            optionName = data.optionName;
            dependentCondition = data.dependentCondition;
            productToOption = new List<ProductToOptionData>(data.productToOption);
        }

        public HouseQuantitiesData()
        {
            communityCode = string.Empty;
            communityName = string.Empty;
            houseName = string.Empty;
            optionName = string.Empty;
            dependentCondition = string.Empty;
            productToOption = new List<ProductToOptionData>();
        }
    }
}
