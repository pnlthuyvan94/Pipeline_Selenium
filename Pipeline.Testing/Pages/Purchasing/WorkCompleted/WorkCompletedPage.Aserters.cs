using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.WorkCompleted
{
    public partial class WorkCompletedPage
    {
        public bool VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage()
        {
            bool isDisplayed = MarkApprovedForPayment.WaitForElementIsVisible(5, false);
            if (isDisplayed)
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(MarkApprovedForPayment), "Mark Approvd for Paymento button is displayed on page");
            else
                ExtentReportsHelper.LogFail("Mark Approvd for Payment button is <font color='green'><b>NOT</b></font> displayed on page");
            return isDisplayed;
        }

        public bool VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage()
        {
            bool isDisplayed = MarkApprovedForPayment.WaitForElementIsVisible(5, false);
            if (!isDisplayed)
                ExtentReportsHelper.LogPass("Mark Approvd for Paymento button is NOT displayed on page as expected");
            else
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(MarkApprovedForPayment), "Mark Approvd for Payment button is <font color='green'><b>displayed</b></font> on page");
            return !isDisplayed;
        }
    }
}
