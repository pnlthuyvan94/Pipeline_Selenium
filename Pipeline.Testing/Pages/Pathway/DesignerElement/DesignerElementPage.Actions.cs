using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Pathway.DesignerElement.AddElement;
using Pipeline.Testing.Pages.Pathway.DesignerElement.AddElementType;

namespace Pipeline.Testing.Pages.Pathway.DesignerElement
{
    public partial class DesignerElementPage
    {
        public void ClickAddToShowModal(DesignerElementOption modal)
        {
            switch (modal)
            {
                case DesignerElementOption.ElementType:
                    FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_lbAddElementType").Click();
                    System.Threading.Thread.Sleep(1000);
                    AddElementTypeModal = new AddElementTypeModal();
                    break;
                default:
                    FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_lbAddElement").Click();
                    System.Threading.Thread.Sleep(1000);
                    AddElementModal = new AddElementModal();
                    break;
            }
        }

        public void FilterItemInGrid(DesignerElementOption grid, string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            switch (grid)
            {
                case DesignerElementOption.ElementType:
                    ElementType_grid.FilterByColumn(columnName, gridFilterOperator, value);
                    ElementType_grid.WaitGridLoad();
                    break;
                default:
                    DesignerElement_grid.FilterByColumn(columnName, gridFilterOperator, value);
                    DesignerElement_grid.WaitGridLoad();
                    break;
            }
        }


        public void DeleteItemInGrid(DesignerElementOption grid, string columnName, string value)
        {
            switch (grid)
            {
                case DesignerElementOption.ElementType:
                    ElementType_grid.ClickDeleteItemInGrid(columnName, value);
                    ConfirmDialog(ConfirmType.OK);
                    ElementType_grid.WaitGridLoad();
                    break;
                default:
                    DesignerElement_grid.ClickDeleteItemInGrid(columnName, value);
                    ConfirmDialog(ConfirmType.OK);
                    DesignerElement_grid.WaitGridLoad();
                    break;
            }
        }

        public enum DesignerElementOption
        {
            ElementType,
            DesignerElement
        }
    }



}
