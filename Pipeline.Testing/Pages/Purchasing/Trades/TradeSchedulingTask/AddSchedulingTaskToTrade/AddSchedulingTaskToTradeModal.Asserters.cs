using System;

namespace Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask.AddSchedulingTaskToTrade
{
    public partial class AddSchedulingTaskToTradeModal
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
                        throw new TimeoutException("The \"Add Scheduling Tasks To Trade\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Scheduling Task(s)");
            }
        }

    }
}
