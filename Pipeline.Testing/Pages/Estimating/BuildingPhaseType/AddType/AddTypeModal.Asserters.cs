using System;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhaseType.AddType
{
    public partial class AddTypeModal
    {
        public bool IsDefaultValues
        {
            get
            {
                return string.IsNullOrEmpty(BuildingPhaseTypeName_txt.GetText());
            }
        }
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(10))
                throw new TimeoutException("The \"Add Building Phase Type\" modal is not displayed after 10s.");
            return (ModalTitle_lbl.GetText() == "Add Type");
        }
    }

}
