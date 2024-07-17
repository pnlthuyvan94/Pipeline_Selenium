using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Phase.AddPhase
{
    public partial class AddCommunityPhaseModal
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (AddPhaseTitle_lbl == null || AddPhaseTitle_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("/'Add Community Phase To Community/' modal is not displayed.");
                    }
                }
                return (AddPhaseTitle_lbl.GetText() == "Add Community Phase To Community");
            }
        }

        public bool IsAddedPhaseToCommunitySuccessful(CommunityPhaseData data)
        {
            // Successful in case all value is clear
            if (string.Empty != Name_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: Name field should be empty.");
                return false;
            }
            if (string.Empty != Code_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: Code field should be empty.");
                return false;
            }
            if (string.Empty != SortOrder_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: Sort Order field should be empty.");
                return false;
            }
            if (string.Empty != Description_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: Description field should be empty.");
                return false;
            }
            return true;
        }
    }
}
