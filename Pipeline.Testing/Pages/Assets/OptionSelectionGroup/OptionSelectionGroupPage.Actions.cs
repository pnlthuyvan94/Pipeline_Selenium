using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Assets.OptionSelectionGroup.AddOptionSelectionGroup;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup
{
    public partial class OptionSelectionGroupPage
    {
        public void ClickAddToOptionSelectionGroupModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddOptionSelectionGroup = new AddOptionSelectionGroupModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            WaitGridLoad();
            return OptionSelectionGroup_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionSelectionGroup_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            OptionSelectionGroup_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            OptionSelectionGroup_Grid.WaitGridLoad();
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            OptionSelectionGroup_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }
    }



}
