using OpenQA.Selenium;
using System;

namespace Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders.ChangeStatus
{
    public partial class ChangeStatusModal
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
                        throw new TimeoutException("The \"Change Status\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Change Status");
            }
        }
    }
}
