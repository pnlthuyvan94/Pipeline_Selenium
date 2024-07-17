
using System;

namespace Pipeline.Testing.Pages.Estimating.Units.UnitDetail.AddProductToUnit
{
    public partial class AddProductToUnitModal
    {
        public bool IsModalDisplayed()
        {
            if (!AddProductToUnitTitle_lbl.WaitForElementIsVisible(10))
            {
                // Wait to title visible
                throw new Exception("Not found " + AddProductToUnitTitle_lbl.GetText() + " element");
            }
            return (AddProductToUnitTitle_lbl.GetText() == "Add Product to Unit");
        }

    }
}
