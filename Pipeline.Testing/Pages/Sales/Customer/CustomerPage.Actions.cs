using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Purchasing.CostCategory;
using Pipeline.Testing.Pages.Purchasing.CostCategory.AddCostCategory;

namespace Pipeline.Testing.Pages.Sales.Customer
{
    public partial class CustomerPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Customer_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            Customer_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Customer_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Customer_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            Customer_Grid.WaitGridLoad();
        }
    }
}
