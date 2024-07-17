using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Communities.AvailablePlan.AddHouseToCommunity
{
    public partial class AddHouseToCommunityModal
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (AddHouseToCommunity_lbl == null || AddHouseToCommunity_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("/'Add House To Community modal/' is not displayed.");
                    }
                }
                return (AddHouseToCommunity_lbl.GetText() == "Add House To Community");
            }
        }

        public bool IsAddedHouseToCommunitySuccessful(string house)
        {
            // Successful in case house doesn't display in the drop downlist anymore
            if (house == House_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {house} house doesn't display in the drop downlist anymore.");
                return false;
            }
            return true;
        }
    }
}
