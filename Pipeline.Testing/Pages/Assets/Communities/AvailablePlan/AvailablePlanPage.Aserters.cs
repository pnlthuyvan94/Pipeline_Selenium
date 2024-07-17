using System;

namespace Pipeline.Testing.Pages.Assets.Communities.AvailablePlan
{
    public partial class AvailablePlanPage
    {
        public bool IsAvailablePlanPageDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (AvailablePlan_Grid == null)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("/'Available Plan/' page is not displayed.");
                    }
                }
                return true;
            }
        }
    }
}
