using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.UserMenu.Subscription.AddSubscription;

namespace Pipeline.Testing.Pages.UserMenu.Subscription
{
    public partial class SubscriptionPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Subscription_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            Subscription_Grid.WaitGridLoad();
            System.Threading.Thread.Sleep(2000);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Subscription_Grid.IsItemWithTextContainsOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Subscription_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void OpenSubcriptionModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddSubscriptionModal = new AddSubscriptionModal();
        }

    }

}
