namespace Pipeline.Testing.Pages.Assets.Communities.Options
{
    public class CommunityOptionData
    {
        public CommunityOptionData()
        {
            AllHouseOptions = new string[] {};
            OtherMasterOptions = new string[] { };
            SalePrice = string.Empty;
            isStandard = false;
            isAvailableToAllHouse = false;
        }

        public string[] AllHouseOptions { get; set; }
        public string[] OtherMasterOptions { get; set; }
        public string SalePrice { get; set; }
        public bool isStandard { get; set; }
        public bool isAvailableToAllHouse { get; set; }
    }
}