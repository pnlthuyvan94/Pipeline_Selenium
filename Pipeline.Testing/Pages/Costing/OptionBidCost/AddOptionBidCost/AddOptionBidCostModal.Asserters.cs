using System;


namespace Pipeline.Testing.Pages.Costing.OptionBidCost.AddOptionBidCost
{
    public partial class AddOptionBidCostModal
    {
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The \"Add Bid Cost\" modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Add Bid Cost");
        }
    }
}
