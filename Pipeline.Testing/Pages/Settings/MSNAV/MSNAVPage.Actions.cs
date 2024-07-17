using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.MSNAV
{
    public partial class MSNAVPage
    {
        public bool GetRunningStatus()
        {
            if (Running_btn != null)
                return Running_btn.GetAttribute("checked") != null;
            return false;
        }

        /// <summary>
        /// Change NAV status to Running or Pause
        /// </summary>
        /// <param name="isRunning"></param>
        public void SetMsNAVStatus(bool isRunning)
        {
            bool isCaptured = false;
            if (isRunning is true)
            {
                // Set NAV to running
                Running_btn.Click(isCaptured);
            }
            else
            {
                // Set NAV to pause
                Paused_btn.Click(isCaptured);
            }

            Save_btn.Click();

            string actualMess = GetLastestToastMessage(30);
            string expectedMess = "Changes saved!";
            if(actualMess.ToLower().Equals(expectedMess.ToLower()))
                ExtentReportsHelper.LogPass($"<font color ='green'><b>MS NAV Integration Settings saved successfully.</b></font>");

            else
                ExtentReportsHelper.LogInformation($"<font color ='red'>MS NAV Integration Settings failed to save." +
                    $"<br>Expected message: {expectedMess}" +
                    $"<br>Actual message: {actualMess}</br></font>");

        }
    }
}
