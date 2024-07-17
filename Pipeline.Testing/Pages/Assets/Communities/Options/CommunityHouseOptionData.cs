namespace Pipeline.Testing.Pages.Assets.Communities.Options
{
    public class CommunityHouseOptionData
    {
        public CommunityHouseOptionData()
        {
            OptionGroup = string.Empty;
            CostGroup = string.Empty;
            AllHouseOptions = new string[] { };
            OtherMasterOptions = new string[] { };
            SalePrice = string.Empty;
            IsStandard = false;
            IsAvailableToAllHouse = false;
        }

        public string OptionGroup { get; set; }
        public string CostGroup { get; set; }
        public string[] AllHouseOptions { get; set; }
        public string[] OtherMasterOptions { get; set; }
        public string SalePrice { get; set; }
        public bool IsStandard { get; set; }
        public bool IsAvailableToAllHouse { get; set; }
    }
}