using Pipeline.Testing.Pages.Estimating.Products;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.House
{
    public class ProductToOptionData
    {
        public string BuildingPhase { get; set; }
        public string PhaseBid { get; set; }
        public IList<ProductData> ProductList { get; set; }
        public IList<ProductData> ParameterList { get; set; }

        /// <summary>
        /// Product Quantities class using in Option/ product page
        /// </summary>
        public ProductToOptionData(ProductToOptionData data)
        {
            BuildingPhase = data.BuildingPhase;
            PhaseBid = data.PhaseBid;
            ProductList = new List<ProductData>(data.ProductList);
            ParameterList = new List<ProductData>(data.ParameterList);
        }

        public ProductToOptionData()
        {
            BuildingPhase = string.Empty;
            PhaseBid = string.Empty;
            ProductList = new List<ProductData>();
            ParameterList = new List<ProductData>();
        }
    }
}
