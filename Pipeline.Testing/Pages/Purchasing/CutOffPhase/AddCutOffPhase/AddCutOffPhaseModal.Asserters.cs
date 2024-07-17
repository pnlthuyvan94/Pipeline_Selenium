using System;

namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase.AddCutoffPhase
{
    public partial class AddCutoffPhaseModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(Code_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(Name_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(SortOrder_txt.GetText()))
                    return false;
                return true;
            }
        }

        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (ModalTitle_lbl == null || ModalTitle_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("The \"Create Cutoff Phase\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Cutoff Phase");
            }
        }
    }
}
