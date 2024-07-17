
using System;

namespace Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail.AddOptionToOptionType
{
    public partial class AddOptionToOptionTypeModal
    {
        public bool IsModalDisplayed()
        {
            if (!AddOptionToOptionTypeTitle_lbl.WaitForElementIsVisible(10))
            {
                // Wait to title visible
                throw new Exception("Not found " + AddOptionToOptionTypeTitle_lbl.GetText() + " element");
            }
            return (AddOptionToOptionTypeTitle_lbl.GetText() == "Add Option to Option Type");
        }

    }
}
