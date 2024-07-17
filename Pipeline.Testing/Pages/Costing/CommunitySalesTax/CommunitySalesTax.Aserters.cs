namespace Pipeline.Testing.Pages.Costing.CommunitySalesTax {
    public partial class CommunitySalesTaxPage {
        public bool IsItemInGrid (string columnName, string value) {
            return CommunitySalesTaxPage_Grid.IsItemOnCurrentPage (columnName, value);
        }

    }
}