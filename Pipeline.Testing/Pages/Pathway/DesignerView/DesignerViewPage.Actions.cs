using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Pathway.DesignerView.AddDesignerView;

namespace Pipeline.Testing.Pages.Pathway.DesignerView
{
    public partial class DesignerViewPage
    {
        public void ClickAddToShowDesignerViewModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddDesignerViewModal = new AddDesignerViewModal();
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
