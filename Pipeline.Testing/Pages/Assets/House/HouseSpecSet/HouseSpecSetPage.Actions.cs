

using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.House.HouseSpecSet
{
    public partial class HouseSpecSetPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            HouseSpecSet_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return HouseSpecSet_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public void DeleteItemInGrid(string columnName, string value)
        {
            HouseSpecSet_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
        }

        public void ChangeHouseSpecSetPageSize(int size)
        {
            HouseSpecSet_Grid.ChangePageSize(size);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            System.Threading.Thread.Sleep(2000);
        }

        public void Navigatepage(int i)
        {
            HouseSpecSet_Grid.NavigateToPage(i);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecSetGroups']/div[1]");
        }
    }
}
