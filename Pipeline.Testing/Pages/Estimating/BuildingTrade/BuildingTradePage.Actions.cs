using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BuildingTrade.AddBuildingTrade;

namespace Pipeline.Testing.Pages.Estimating.BuildingTrade
{
    public partial class BuildingTradePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingTrade_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingTrade_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingTrade_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            BuildingTrade_Grid.WaitGridLoad();
        }

        public void ClickAddToOpenCreateBuildingTradeModal()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddBuildingTradeModal = new AddBuildingTradeModal();
            System.Threading.Thread.Sleep(500);
        }

        public void CloseModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='buildingtrades-modal']/section/header/a").Click();
            System.Threading.Thread.Sleep(500);
        }
    }

}
