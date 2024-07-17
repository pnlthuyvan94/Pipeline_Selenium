using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.OptionBidCost.AddJobOptionBidCost
{
    public partial class AddJobOptionBidCostModal
    {
        public bool IsModalDisplayed()
        {
            if (!JobModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Bid Cost\" modal is not displayed.");
            return (JobModalTitle_lbl.GetText() == "Add Bid Cost");
        }
    }
}
