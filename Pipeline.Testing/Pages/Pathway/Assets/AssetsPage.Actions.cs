using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Pathway.Assets.AssetsDetail;

namespace Pipeline.Testing.Pages.Pathway.Assets
{
    public partial class AssetsPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Assets_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            Assets_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Assets_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Assets_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            Assets_Grid.WaitGridLoad();
        }

        public void ClickAddButton()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            // Open Detail User page
            AssetsDetailPage = new AssetsDetailPage();
        }

        public void CreateAnAsset(AssetsData _data)
        {
            AssetsDetailPage.CreateAnAsset(_data);
        }
    }

}
