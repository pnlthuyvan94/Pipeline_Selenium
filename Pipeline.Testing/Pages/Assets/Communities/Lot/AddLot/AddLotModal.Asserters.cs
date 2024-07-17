using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Lot.AddLot
{
    public partial class AddLotModal
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (AddLotTitle_lbl == null || AddLotTitle_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("/'Add House To Community modal/' is not displayed.");
                    }
                }
                return (AddLotTitle_lbl.GetText() == "Add House To Community");
            }
        }

        public bool IsAddedHouseToCommunitySuccessful(string house)
        {
            // Successful in case house doesn't display in the drop downlist anymore
            if (house == Status_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {house} house doesn't display in the drop downlist anymore.");
                return false;
            }
            return true;
        }
    }
}
