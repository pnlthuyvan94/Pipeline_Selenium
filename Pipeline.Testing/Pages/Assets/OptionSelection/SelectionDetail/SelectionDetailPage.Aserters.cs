namespace Pipeline.Testing.Pages.Assets.OptionSelection.SelectionDetail
{
    public partial class SelectionDetailPage
    {
        public bool IsItemDisplayOnResourceGrid(string columnName, string valueToFind)
        {
            return Resources_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }

        public bool IsItemWithTextContainsDisplayOnResourceGrid(string columnName, string valueToFind)
        {
            return Resources_Grid.IsItemWithTextContainsOnCurrentPage(columnName, valueToFind);
        }

        public bool IsItemInOptionGrid(string columnName, string valueToFind)
        {
            return Option_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
    }
}
