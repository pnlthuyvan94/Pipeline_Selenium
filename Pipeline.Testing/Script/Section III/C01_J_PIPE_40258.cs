using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Jobs.Job;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_III
{
    public class C01_J_PIPE_40258 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private double acceptableLoadTime = 10;
        private string jobNumberFilter = "QA";
        private string addressFilter = "Indiana";
        private string communityFilter = "0000-Master Community";
        private string seriesFilter = "Visions";
        private string houseFilter = "1530-QA_HOUSE_01";

        [Test]
        public void C01_J_Jobs_Landing_Page()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Go to Jobs Landing Page.</b></font>");
            var pageLoadTime = CommonHelper.MeasureLoadTime(() => JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs));
            if(pageLoadTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Jobs Landing Page load time is " + pageLoadTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Jobs Landing Page load time is " + pageLoadTime + " seconds.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Filter Jobs Grid using Job Number column.</b></font>");
            var filterJobNoTime = CommonHelper.MeasureLoadTime(() => JobPage.Instance.FilterItemInGrid("Job Number", GridFilterOperator.Contains, jobNumberFilter));
            CommonHelper.CaptureScreen();
            if (filterJobNoTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Job Number column load time is " + filterJobNoTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Job Number column load time is " + filterJobNoTime + " seconds.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Filter Jobs Grid using Address column.</b></font>");
            var filterAddressTime = CommonHelper.MeasureLoadTime(() => JobPage.Instance.FilterItemInGrid("Address", GridFilterOperator.Contains, addressFilter));
            CommonHelper.CaptureScreen();
            if(filterAddressTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Address column load time is " + filterAddressTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Address column load time is " + filterAddressTime + " seconds.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Filter Jobs Grid using Community column.</b></font>");
            var filterCommunityTime = CommonHelper.MeasureLoadTime(() => JobPage.Instance.FilterByColumnDropDown("Community", communityFilter));
            CommonHelper.CaptureScreen();
            if(filterCommunityTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Community column load time is " + filterCommunityTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Community column load time is " + filterCommunityTime + " seconds.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Filter Jobs Grid using Series column.</b></font>");
            var filterSeriesTime = CommonHelper.MeasureLoadTime(() => JobPage.Instance.FilterByColumnDropDown("Series", seriesFilter)); 
            CommonHelper.CaptureScreen();
            if(filterSeriesTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using Series column load time is " + filterSeriesTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using Series column load time is " + filterSeriesTime + " seconds.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Filter Jobs Grid using House column.</b></font>");
            var filterHouseTime = CommonHelper.MeasureLoadTime(() => JobPage.Instance.FilterByColumnDropDown("House", houseFilter));
            CommonHelper.CaptureScreen();
            if(filterHouseTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Filtering using House column load time is " + filterHouseTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Filtering using House column load time is " + filterHouseTime + " seconds.</b></font>");

            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Go to Job Grid page 2.</b></font>");
            var goToPage2Time = CommonHelper.MeasureLoadTime(() => JobPage.Instance.NavigateToPage(2));
            if(goToPage2Time <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Go to page 2 load time is " + goToPage2Time + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Go to page 2 load time is " + goToPage2Time + " seconds.</b></font>");
                      
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Go to Job Grid last page.</b></font>");
            var goToLastPageTime = CommonHelper.MeasureLoadTime(() => JobPage.Instance.NavigateToPage(JobPage.Instance.GetJobGridPageCount()));
            if (goToLastPageTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Go to last page load time is " + goToLastPageTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Go to last page load time is " + goToLastPageTime + " seconds.</b></font>");


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Go to Job Grid first page.</b></font>");
            var goToFirstPageTime = CommonHelper.MeasureLoadTime(() => JobPage.Instance.NavigateToPage(1));
            if (goToLastPageTime <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Go to first page load time is " + goToLastPageTime + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Go to first page load time is " + goToLastPageTime + " seconds.</b></font>");

            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Change page size to 500.</b></font>");
            var changePageSize500Time = CommonHelper.MeasureLoadTime(() => JobPage.Instance.ChangePageSize(500));
            if (changePageSize500Time <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Change page size to 500 load time is " + changePageSize500Time + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Change page size to 500 load time is " + changePageSize500Time + " seconds.</b></font>");
           
            CommonHelper.RefreshPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.11: Change page size to 100.</b></font>");
            var changePageSize100Time = CommonHelper.MeasureLoadTime(() => JobPage.Instance.ChangePageSize(100));
            if (changePageSize100Time <= acceptableLoadTime)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Change page size to 100 load time is " + changePageSize100Time + " seconds.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Change page size to 100 load time is " + changePageSize100Time + " seconds.</b></font>");

           
        }
    }
}
