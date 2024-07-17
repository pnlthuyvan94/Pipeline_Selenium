using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.Vendor.BaseCosts.AddBaseCost
{
    public partial class AddBaseCostModal
    {
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Cost\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Cost");
        }
    }
}
