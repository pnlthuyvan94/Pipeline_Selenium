using System;

namespace Pipeline.Testing.Pages.Estimating.Category.AddCategory
{
    public partial class AddCategoryPage
    {
        /*
         * Check Adding model is displayed or not
         */
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Category\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Category");
        }

        public bool IsNameEmpty
        {
            get
            {
                return string.IsNullOrEmpty(CategoryName_txt.GetText());
            }
        }
    }
}
