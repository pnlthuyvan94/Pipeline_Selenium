using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.JobBOM.OneTimeItem
{
    public partial class OneTimeItemModal
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
                        throw new TimeoutException("The \"Add One Time Item\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add One Time Item");
            }
        }
    }
}
