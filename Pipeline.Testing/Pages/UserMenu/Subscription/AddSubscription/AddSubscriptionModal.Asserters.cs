using System;

namespace Pipeline.Testing.Pages.UserMenu.Subscription.AddSubscription
{
    public partial class AddSubscriptionModal
    {
        public bool IsModalDisplayed()
        {
            if (!ModalTitle_lbl.WaitForElementIsVisible(5))
                throw new TimeoutException("The Insert event to Subscribe modal is not displayed.");
            return (ModalTitle_lbl.GetText() == "Choose Events to Subscribe To");
        }

    }
}
