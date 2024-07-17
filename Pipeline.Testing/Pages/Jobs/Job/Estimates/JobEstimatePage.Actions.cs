using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.Estimates
{
    public partial class JobEstimatePage
    {
        public bool HasImgDifferentPhaseValuesDisplay()
        {
            if (imgDifferentPhaseValues_img.IsDisplayed(false) is true)
            {
                return true;
            }
            return false;
        }

        public void GenerateBomAndEstimates()
        {
            GenerateBomAndEstimates_btn.Click(true);
            WaitingLoadingGifByXpath(loadingGrid_xpath);

            // Get current toast message and verify it
            string actualToastMess = GetLastestToastMessage(50);
            string expectedToastMess = "BOM & Estimate generation complete!";

            if (actualToastMess.Equals(expectedToastMess))
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Generate Job BOM and Estimate successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Failed to Generate Job BOM and Estimate. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return JobEstimatesPhase_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            JobEstimatesPhase_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public bool CheckVendorForPhase(string buildingPhase, string expectedVendor)
        {
            bool found = false;
            SelectView("Phase");
            System.Threading.Thread.Sleep(5000);
            FilterItemInGrid("Phase Name", GridFilterOperator.EqualTo, buildingPhase);
            System.Threading.Thread.Sleep(5000);
            if(IsItemInGrid("Phase Name", buildingPhase) is true)
            {
                IWebElement webElement = driver.FindElement(By.XPath("//table[@id='ctl00_CPH_Content_rgPhasesView_ctl00']/tbody/tr/td[3]"));
                string actualVendor = webElement.Text;
                if (expectedVendor == actualVendor)
                    found = true;
            }

            return found;
        }
        public void SelectView(string view)
        {
            ViewBy_ddl.SelectItem(view, false);
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }
    }
}
