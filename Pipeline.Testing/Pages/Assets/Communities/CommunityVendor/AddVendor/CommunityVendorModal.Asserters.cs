using System;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityVendor.AddVendor
{
    public partial class CommunityVendorModal
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (Modal_Lbl == null || Modal_Lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("Add Allowed Vendors modal is not displayed.");
                    }
                }
                return (Modal_Lbl.GetText() == "Add Allowed Vendors");
            }
        }

    }
}
