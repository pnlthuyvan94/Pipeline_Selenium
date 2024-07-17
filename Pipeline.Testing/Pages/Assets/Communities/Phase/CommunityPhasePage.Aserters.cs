using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Phase
{
    public partial class CommunityPhasePage
    {
        public bool IsPhasePageDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (Phase_Grid == null)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("/'Add Phase/' page is not displayed.");
                    }
                }
                return true;
            }
        }
    }
}
