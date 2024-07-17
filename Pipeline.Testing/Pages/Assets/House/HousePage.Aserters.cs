namespace Pipeline.Testing.Pages.Assets.House
{
    public partial class HousePage
    {
        public bool IsItemInGrid(string columnName, string value)
        {
            HousePage_Grid.WaitGridLoad(true);
            return HousePage_Grid.IsItemOnCurrentPage(columnName, value);
        }
                
        public bool IsIncludeAllQuantitiesChecked()
        {
            return includeAllCommunitiesInCopyModal.IsChecked;
        }
        public bool IsIncludeAllHousesOptionsChecked()
        {
            return includeAllHouseOptionsInCopyModal.IsChecked;
        }
        public bool IsIncludeAllSalesOptionsLogicChecked()
        {
            return includeAllQuantitiesInCopyModal.IsChecked;
        }
        public bool IsIncludeAllCommunitiesChecked()
        {
            return includeAllSalesOptionsInCopyModal.IsChecked;
        }
        public bool IsIncludeAllSalesOptionsChecked()
        {
            return includeAllSalesOptionsLogicInCopyModal.IsChecked;
        }

    }
}
