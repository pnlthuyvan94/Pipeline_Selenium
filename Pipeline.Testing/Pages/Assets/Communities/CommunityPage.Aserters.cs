using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities
{
    public partial class CommunityPage
    {
        public bool VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage()
        {
            bool isDisplayed = BuildProSync_Btn.WaitForElementIsVisible(5, false);
            if (isDisplayed)
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(BuildProSync_Btn), "The Sync Communities to BuildPro button is displayed on page");
            else
                ExtentReportsHelper.LogFail("The Sync Communities to BuildPro button is <font color='green'><b>NOT</b></font> displayed on page");
            return isDisplayed;
        }

        public bool VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage()
        {
            bool isDisplayed = BuildProSync_Btn.WaitForElementIsVisible(5, false);
            if (!isDisplayed)
                ExtentReportsHelper.LogPass("The Sync Communities to BuildPro button is NOT displayed on page as expected");
            else
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(BuildProSync_Btn), "The Sync Communities to BuildPro button is <font color='green'><b>displayed</b></font> on page");
            return !isDisplayed;
        }

    }
}
