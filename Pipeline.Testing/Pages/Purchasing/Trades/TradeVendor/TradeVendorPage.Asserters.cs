using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor
{
    public partial class TradeVendorPage
    {
        public bool IsColumnFoundInGrid(string columnName)
        {
            try
            {
                return Vendor_Grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }

        public bool IsAddVendorTradeBtnDisplayed
        {
            get
            {
                var addBtn = GetItemOnHeader(DashboardContentItems.Add);
                return (addBtn != null);
            }
        }

        public bool IsBulkActionDisplayed
        {
            get
            {
                var bulkAction = GetItemOnHeader(DashboardContentItems.BulkActions);
                return (bulkAction != null);
            }
        }

        public bool IsUtilitiesDisplayed
        {
            get
            {
                var utilities = GetItemOnHeader(DashboardContentItems.Utilities);
                return (utilities != null);
            }
        }
    }
}
