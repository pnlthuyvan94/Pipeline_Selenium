using System;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductResources.AddProductResource
{
    public partial class AddProductResourceModal
    {
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Resource\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Resource");
        }
    }
}
