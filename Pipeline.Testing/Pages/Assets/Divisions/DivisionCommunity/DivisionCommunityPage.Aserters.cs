using System;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity
{
    public partial class DivisionCommunityPage
    {
        public bool IsDivisionPageDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (DivisionCommunity_lbl == null || DivisionCommunity_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("Communities in Division page is not displayed.");
                    }
                }
                return ("Communities in Division".Equals(DivisionCommunity_lbl.GetText()));
            }
        }
    }
}
