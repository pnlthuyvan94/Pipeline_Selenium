using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Assets.OptionSelection.AddOptionSelection;

namespace Pipeline.Testing.Pages.Assets.OptionSelection
{
    public partial class OptionSelectionPage
    {
        public void ClickAddToOptionSelectionModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddOptionSelectionModal = new AddOptionSelectionModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            WaitGridLoad();
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

        public void WaitGridLoad()
        {
            OptionSelection_Grid.WaitGridLoad();
        }

        public void SelectItemInGridWithTextContains(string columName, string value)
        {
            OptionSelection_Grid.ClickItemInGridWithTextContains(columName, value);
            JQueryLoad();
        }
    }



}
