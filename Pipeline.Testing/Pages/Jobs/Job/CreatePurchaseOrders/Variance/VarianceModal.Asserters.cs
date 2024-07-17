using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders.Variance
{
    public partial class VarianceModal
    {
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Variance\" modal is not displayed."); 
            return (ModalTitle_lbl.GetText() == "Confirm Price Changes to Purchase Orders");
        }
    }
}
