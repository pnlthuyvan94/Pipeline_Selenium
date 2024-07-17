using System;

namespace Pipeline.Testing.Pages.Assets.OptionType.AddOptionType
{
    public partial class AddOptionTypeModal
    {

        public bool IsModalDisplayed()
        {
            {
                if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                    return false;
                return (ModalTitle_lbl.GetText() == "Add Option Type");
            }
        }
    }
}
