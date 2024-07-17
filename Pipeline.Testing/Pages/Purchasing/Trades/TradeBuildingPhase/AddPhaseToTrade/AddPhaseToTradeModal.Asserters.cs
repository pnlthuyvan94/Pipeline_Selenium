using System;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase.AddPhaseToTrade
{
    public partial class AddPhaseToTradeModal
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
                        throw new TimeoutException("The \"Add Building Phases\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Building Phase(s)");
            }
        }

    }
}
