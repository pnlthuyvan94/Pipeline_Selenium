
namespace Pipeline.Testing.Pages.Assets.House.Quantities
{
    public class HouseQuantityUsesData
    {
        public string UseName { get; set; }
        public string Quantity { get; set; }
        public string Waste { get; set; }
        public string Rounding { get; set; }
        public string TotalQuantity { get; set; }
     
       
        /// <summary>
        /// HouseQuantityUsesData class using in House/HouseBOM/generate BOM/at USE layer (4th layer)
        /// </summary>
        public HouseQuantityUsesData(HouseQuantityUsesData data)
        {
            UseName = data.UseName;
            Quantity = data.Quantity;
            Waste = data.Waste;
            Rounding = data.Rounding;
            TotalQuantity = data.TotalQuantity;
        }

        public HouseQuantityUsesData()
        {
        }
    }
}
