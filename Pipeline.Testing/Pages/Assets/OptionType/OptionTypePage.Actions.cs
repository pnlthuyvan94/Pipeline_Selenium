using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Assets.OptionType.AddOptionType;

namespace Pipeline.Testing.Pages.Assets.OptionType
{
    public partial class OptionTypePage
    {
        public void ClickAddOptionTypeButton()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddOptionTypeModal = new AddOptionTypeModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return OptionType_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionType_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionTypes']/div[1]",5000);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            OptionType_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionTypes']/div[1]");
        }

        public void WaitGridLoad()
        {
            OptionType_Grid.WaitGridLoad();
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            OptionType_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }
    }
}
