using System;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.AddBuildingGroup
{
    public partial class AddBuildingGroupModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(BuildingGroupCode_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(BuildingGroupName_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(BuildingGroupDescription_txt.GetText()))
                    return false;
                return true;
            }
        }

        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Building Group\" modal is not displayed after 10s.");
            return (ModalTitle_lbl.GetText() == "Add Building Group");
        }
    }
}
