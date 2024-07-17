using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.MainSetting
{
    public partial class MainSettingPage
    {
        /// <summary>
        /// Change Transfer Separation Character status to Running or Pause
        /// </summary>
        /// <param name="isRunning"></param>
        public void SetTransferSeparationCharactertatus(string expectedCharacter)
        {
            string currentCharater = TransferSeparation_txt.GetText();
            if (currentCharater.Equals(expectedCharacter))
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b>The current Transfer Sepearation Character is '{expectedCharacter}'." +
                    $"<br>Don't need to update it.</b></br></font>");
                return;
            }

            bool isCaptured = false;
            TransferSeparation_txt.Clear();
            TransferSeparation_txt.SetText(expectedCharacter);
            Save_btn.Click(isCaptured);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']");

            string actualMess = GetLastestToastMessage(30);
            string expectedMess = "Settings Updated";
            if(actualMess.ToLower().Equals(expectedMess.ToLower()))
                ExtentReportsHelper.LogPass($"<font color ='green'><b>Transfer separation Character saved successfully.</b></font>");

            else
                ExtentReportsHelper.LogInformation($"<font color ='red'>Transfer separation Character failed to save." +
                    $"<br>Expected message: {expectedMess}" +
                    $"<br>Actual message: {actualMess}</br></font>");

        }
    }
}
