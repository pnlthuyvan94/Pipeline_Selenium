using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Settings.Users
{
    public partial class UserPage
    {
        public void ClickAddToUserIcon()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            PageLoad();
        }

        public void SelectItemInGrid(string colum, string value)
        {
            UserPage_Grid.ClickItemInGrid(colum, value);
            JQueryLoad();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            UserPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUsers']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return UserPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            UserPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUsers']/div[1]");
        }
    }
}
