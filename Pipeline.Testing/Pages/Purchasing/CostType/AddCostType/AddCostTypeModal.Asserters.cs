using System;

namespace Pipeline.Testing.Pages.Purchasing.CostType.AddCostType
{
    public partial class AddCostTypeModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(Name_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(Description_txt.GetText()))
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
                        throw new TimeoutException("The \"Create Cost Type\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Cost Type");
            }
        }

    }
}
