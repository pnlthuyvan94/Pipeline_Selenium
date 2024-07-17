using System;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity.AddCommunity
{
    public partial class DivisionCommunityModal
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (AddCommunity_lbl == null || AddCommunity_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("Add Community in Division modal is not displayed.");
                    }
                }
                return (AddCommunity_lbl.GetText() == "Add Communities");
            }
        }

    }
}
