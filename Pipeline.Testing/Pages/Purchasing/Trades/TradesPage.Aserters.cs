using Pipeline.Common.Enums;
using System;

namespace Pipeline.Testing.Pages.Purchasing.Trades
{
    public partial class TradesPage
    {
        public bool IsColumnFoundInGrid(string columnName)
        {
            try
            {
                return Trades_Grid.IsColumnFoundInGrid(columnName);
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public bool IsVendorAssignmentBtnDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (VendorAssignments_btn == null || VendorAssignments_btn.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("The Vendor Assignments button is not displayed.");
                    }
                }
                return (VendorAssignments_btn.GetText() == "Vendor Assignments");
            }
        }

        public bool IsAddTradeBtnDisplayed
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
