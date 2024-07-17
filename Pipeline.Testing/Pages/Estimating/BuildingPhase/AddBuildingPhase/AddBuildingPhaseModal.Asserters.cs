namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.AddBuildingPhase
{
    public partial class AddBuildingPhaseModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(PhaseCode_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(PhaseName_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(PhaseAbbName_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(PhaseDescription_txt.GetText()))
                    return false;
                if (PhaseBuildingGroup_ddl.SelectedIndex != 0)
                    return false;
                if (PhaseParents_ddl.SelectedIndex != 0)
                    return false;
                if (PhaseType_ddl.SelectedIndex != 0)
                    return false;
                if (!string.IsNullOrEmpty(PhaseCode_txt.GetText()))
                    return false;
                //if (!string.IsNullOrEmpty(PhasePercentBill_txt.GetText()))
                    //return false;
                //if (PhaseTaxable_chk.IsChecked)
                    //return false;
                return true;
            }
        }

        public bool IsModalDisplayed()
        {
            if (ModalTitle_lbl.WaitForElementIsVisible(5))
                return (ModalTitle_lbl.GetText() == "Add Building Phase");
            else
                return false;
        }
    }
}
