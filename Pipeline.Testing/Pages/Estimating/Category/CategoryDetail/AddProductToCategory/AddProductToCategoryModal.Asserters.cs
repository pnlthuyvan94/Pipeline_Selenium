namespace Pipeline.Testing.Pages.Estimating.Category.CategoryDetail.AddProductToCategory
{
    public partial class AddProductToCategoryModal
    {
        public bool IsModalDisplayed()
        {
            return AddProductToCategoryTitle_lbl.WaitForElementIsVisible(5);
        }
    }
}
