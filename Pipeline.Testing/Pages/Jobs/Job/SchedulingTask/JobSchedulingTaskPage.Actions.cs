using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Jobs.Job.SchedulingTask
{
    public partial class JobSchedulingTaskPage
    {
        public void CollapseAllGrid()
        {
            CommonHelper.CaptureScreen();
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgJobScheduling_ctl00']/thead/tr/th/input";
            Button collapseAllBtn = new Button(FindType.XPath, xpath);
            collapseAllBtn.Click();
            CommonHelper.CaptureScreen();
        }

        public bool IsItemInMilestoneGrid(string columnName, string value)
        {
            return Milestones_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public void ValidateScheduledTask(string columnName, string expectedValue, int rowNumber, int columnNumber)
        {
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgJobScheduling_ctl00_ctl05_Detail10']/tbody/tr[" + rowNumber + "]/td[" + columnNumber + "]/span";
            SpecificControls control = new SpecificControls(FindType.XPath, xpath);
            string actualValue = control.GetAttribute("innerHTML").Replace(" ", "").Replace("\r","").Replace("\n", "");
            if (expectedValue.Replace(" ", "").Equals(actualValue))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected " + columnName +  " value is displayed on the grid.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='green'><b>The expected " + columnName + " value is not displayed on the grid.</b></font>");
            }
        }



        public bool IsColumnFoundInMilestoneGrid(string columnName)
        {
            try
            {
                return Milestones_Grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }

        public bool IsColumnFoundInDetailGrid(string columnName)
        {
            try
            {
                return Detail_Grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }
    }
}
