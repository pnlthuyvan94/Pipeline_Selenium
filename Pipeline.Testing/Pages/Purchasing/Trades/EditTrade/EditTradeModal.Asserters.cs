using System;

namespace Pipeline.Testing.Pages.Purchasing.Trades.EditTrade
{
    public partial class EditTradeModal
    {
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
                        throw new TimeoutException("The \"Update Trade\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Edit Trade");
            }
        }

    }
}
