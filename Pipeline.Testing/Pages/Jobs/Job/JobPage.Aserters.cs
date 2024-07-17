using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Jobs.Job
{
    public partial class JobPage
    {
        /// <summary>
        /// Verify the current job configuration number and created date time
        /// </summary>
        /// <param name="configNum"></param>
        /// <param name="createdDate"></param>
        public void VerifyNewConfigOnJobDetailPage(string configNum, string createdDate)
        {
            bool isCaptured = false;

            // Get Current Config
            Label currentConfig = new Label(FindType.XPath, "//section[@class='sub-information active']//*[contains(@class, 'job-header')]");
            if (currentConfig.IsDisplayed(isCaptured) is true)
            {
                // Compare current config on the job detail page and the 'configNum'
                string currentConfigValue = currentConfig.GetText();
                if (currentConfigValue.Equals(configNum))
                    ExtentReportsHelper.LogPass($"<font color='green'>Current Config Number displays correctly with value <b>'{currentConfigValue}'</b> on the Job detail page.</font>");
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Current config number displays incorrecly on the Job detail page." +
                           $"<br>The expected: {configNum}" +
                           $"<br>The actual: {currentConfigValue}</font>");
                }
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find current config on the Job detail page.</font>");


            // Get Current Date Time
            Label currentDateTime = new Label(FindType.XPath, "//section[@class='sub-information active']//time");
            if (currentDateTime.IsDisplayed(isCaptured) is true)
            {
                // Compare current Date Time on the job detail page and the 'createdDate'
                string currentDateTimeValue = currentDateTime.GetText();
                if (currentDateTimeValue.Equals(createdDate))
                    ExtentReportsHelper.LogPass($"<font color='green'>Current Date Time displays correctly with value <b>'{currentDateTimeValue}'</b> on the Job detail page.</font>");
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Current Date Time displays incorrecly on the Job detail page." +
                           $"<br>The expected: {createdDate}" +
                           $"<br>The actual: {currentDateTimeValue}</font>");
                }
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find current Date Time on current config panel of the Job detail page.</font>");
        }
    }
}
