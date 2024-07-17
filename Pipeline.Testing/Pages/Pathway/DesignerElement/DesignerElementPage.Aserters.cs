namespace Pipeline.Testing.Pages.Pathway.DesignerElement
{
    public partial class DesignerElementPage
    {
        public bool IsItemInGrid(DesignerElementOption grid, string columnName, string value)
        {
            switch (grid)
            {
                case DesignerElementOption.ElementType:
                    return ElementType_grid.IsItemOnCurrentPage(columnName, value);
                default:
                    return DesignerElement_grid.IsItemOnCurrentPage(columnName, value);
            }
        }
    }

}

