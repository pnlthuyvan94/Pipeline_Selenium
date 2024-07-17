using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Pathway.AssetGroup.AddAssetGroup;

namespace Pipeline.Testing.Pages.Pathway.AssetGroup
{
    public partial class AssetGroupPage
    {
        public void ClickAddToShowAssetGroupModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddAssetGroupModal = new AddAssetGroupModal();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CostCategory_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CostCategory_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            CostCategory_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitGridLoad();
        }

        public void WaitGridLoad()
        {
            CostCategory_Grid.WaitGridLoad();
        }
    }



}
