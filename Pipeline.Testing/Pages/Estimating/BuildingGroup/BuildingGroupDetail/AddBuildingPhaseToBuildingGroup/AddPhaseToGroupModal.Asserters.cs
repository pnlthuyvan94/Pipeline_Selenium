
using System;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail.AddBuildingPhaseToBuildingGroup
{
    public partial class AddPhaseToGroupModal
    {
        public bool IsModalDisplayed()
        {
            if (!AddPhaseToGroupTitle_lbl.WaitForElementIsVisible(10))
            {
                // Wait to title visible
                throw new Exception("Not found " + AddPhaseToGroupTitle_lbl.GetText() + " element");
            }
            return (AddPhaseToGroupTitle_lbl.GetText() == "Add Building Phases to Building Group");
        }

    }
}
