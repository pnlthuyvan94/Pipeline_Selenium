using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Purchasing.CostCategory;
using Pipeline.Testing.Pages.Purchasing.CostCategory.AddCostCategory;

namespace Pipeline.Testing.Pages.Sales.Prospect
{
    public partial class ProspectPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Prospect_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            Prospect_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Prospect_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Prospect_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            Prospect_Grid.WaitGridLoad();
        }
    }
}
