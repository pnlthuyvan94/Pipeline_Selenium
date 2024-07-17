using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase
{
    public partial class CutoffPhasePage
    {
        public bool IsHaveBuildingPhaseSection
        {
            get
            {
                if (!BuildingPhases_lbl.IsNull())
                {
                    CommonHelper.HighLightElement(BuildingPhases_lbl);
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
