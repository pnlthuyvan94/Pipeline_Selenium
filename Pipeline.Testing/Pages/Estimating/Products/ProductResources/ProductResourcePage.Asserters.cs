using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductResources
{
    public partial class ProductResourcePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Resource_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Resource_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Resource_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            Resource_Grid.WaitGridLoad();
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            Resource_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }
    }
}
