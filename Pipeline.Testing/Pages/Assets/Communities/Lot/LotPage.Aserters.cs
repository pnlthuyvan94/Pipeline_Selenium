using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Lot
{
    public partial class LotPage
    {
        public bool IsLotPageDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (Lot_Grid == null)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("/'Add Lot/' page is not displayed.");
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Verify lot buttion status is displayed or not
        /// </summary>
        /// <returns></returns>
        public bool IsAddLotButtonDisplay()
        {
            return Add_btn.IsDisplayed();
        }
    }
}
