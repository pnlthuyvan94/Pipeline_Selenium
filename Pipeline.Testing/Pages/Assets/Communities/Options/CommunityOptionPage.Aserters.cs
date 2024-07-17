using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Options
{
    public partial class CommunityOptionPage
    {
        public bool IsCommunityOptionPageDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (CommunityOption_Grid == null || CommunityHouseOption_Grid == null)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("/'Community Option/' page is not displayed.");
                    }
                }
                return true;
            }
        }
    }
}
