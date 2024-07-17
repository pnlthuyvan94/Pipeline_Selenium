using System;

namespace Pipeline.Testing.Pages.Estimating.Styles.DetailStyles.SubManufacturerPage
{
    public partial class SubmanufacturerPage
    {

        public bool IsSubManufacturerDisplayed()
        {
            if (!SubManufacturerTitle_lbl.WaitForElementIsVisible(10))
            {
                throw new Exception("Not found " + SubManufacturerTitle_lbl.GetText() + " element");
            }
            return true;
        }

    }
}
