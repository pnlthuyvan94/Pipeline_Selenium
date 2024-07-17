using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail.AddOptionSelectionToGroup;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail
{
    public partial class OptionSelectionGroupDetailPage
    {
        private OptionSelectionGroupDetailPage EnterOptionSelectionGroupName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                OptionSelectionGroupName_txt.SetText(name);
            return this;
        }

        private OptionSelectionGroupDetailPage EnterSortOrder(string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
                SortOrder_txt.SetText(sortOrder);
            return this;
        }

        private void Save()
        {
            Save_btn.Click();
        }

        public OptionSelectionGroupDetailPage UpdateOptionSelectionGroup(OptionSelectionGroupData data)
        {
            EnterOptionSelectionGroupName(data.Name).EnterSortOrder(data.SortOrder);
            Save();
            return this;
        }

        public OptionSelectionGroupDetailPage AddOptionSelectionToGroup()
        {
            AddOptionSelection_btn.Click();
            AddOptionSelectionModal = new AddOptionSelectionToGroupModal();
            return this;
        }

        public void WaitGridLoad()
        {
            OptionSelection_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return OptionSelection_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionSelection_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            OptionSelection_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

    }
}
