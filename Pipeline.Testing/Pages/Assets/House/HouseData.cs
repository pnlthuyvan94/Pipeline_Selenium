namespace Pipeline.Testing.Pages.Assets.House
{
    public class HouseData
    {
        public string HouseName { get; set; }
        public string SaleHouseName { get; set; }
        public string Series { get; set; }
        public string PlanNumber { get; set; }
        public string BasePrice { get; set; }
        public string SQFTBasement { get; set; }
        public string SQFTFloor1 { get; set; }
        public string SQFTFloor2 { get; set; }
        public string SQFTHeated { get; set; }
        public string SQFTTotal { get; set; }
        public string Style { get; set; }
        public string Stories { get; set; }
        public string Bedrooms { get; set; }
        public string Bathrooms { get; set; }
        public string Garage { get; set; }
        public string Description { get; set; }

        public HouseData()
        {
            HouseName = string.Empty;
            SaleHouseName = string.Empty;
            Series = string.Empty;
            PlanNumber = string.Empty;
            BasePrice = string.Empty;
            SQFTBasement = string.Empty;
            SQFTFloor1 = string.Empty;
            SQFTFloor2 = string.Empty;
            SQFTHeated = string.Empty;
            SQFTTotal = string.Empty;
            Style = string.Empty;
            Stories = string.Empty;
            Bedrooms = string.Empty;
            Bathrooms = string.Empty;
            Garage = string.Empty;
            Description = string.Empty;
        }
        public HouseData(HouseData HouseData) 
        {
            HouseName = HouseData.HouseName;
            SaleHouseName = HouseData.SaleHouseName;
            Series = HouseData.Series;
            PlanNumber = HouseData.PlanNumber;
            BasePrice = HouseData.BasePrice;
            SQFTBasement = HouseData.SQFTBasement;
            SQFTFloor1 = HouseData.SQFTFloor1;
            SQFTFloor2 = HouseData.SQFTFloor2;
            SQFTHeated = HouseData.SQFTHeated;
            SQFTTotal = HouseData.SQFTTotal;
            Style = HouseData.Style;
            Stories = HouseData.Stories;
            Bedrooms = HouseData.Bedrooms;
            Bathrooms = HouseData.Bathrooms;
            Garage = HouseData.Garage;
            Description = HouseData.Description;
         }

    }
}
