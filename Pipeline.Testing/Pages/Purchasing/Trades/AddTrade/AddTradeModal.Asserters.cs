using OpenQA.Selenium;
using System;

namespace Pipeline.Testing.Pages.Purchasing.Trades.AddTrade
{
    public partial class AddTradeModal
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
                        throw new TimeoutException("The \"Create Trade\" modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add Trade");
            }
        }

        public bool IsCompanyVendorDisplayed()
        {
            try
            {
                if (CompanyVendor_ddl != null && CompanyVendor_ddl.IsDisplayed())
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

    }
}
