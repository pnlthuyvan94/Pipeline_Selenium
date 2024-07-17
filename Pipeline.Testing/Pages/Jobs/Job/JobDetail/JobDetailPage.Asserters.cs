using AventStack.ExtentReports.Utils;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Xml.Linq;

namespace Pipeline.Testing.Pages.Jobs.Job.JobDetail
{
    public partial class JobDetailPage
    {
        /*
         * Verify head title and id of new Job
         */
        public bool IsCreateSuccessfully(JobData job)
        {
            bool isPassed = true;
            if (!IsHeaderBreadcrumbsCorrect(job.Name))
            {
                SpecificControls breadscrumb = new SpecificControls(FindElementHelper.FindElement(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[1]/ul/li[2]"));
                CommonHelper.HighLightElement(breadscrumb);
                isPassed = false;
            }
            if (job.Name != Name_txt.GetValue())
            {
                CommonHelper.HighLightElement(Name_txt);
                isPassed = false;
            }
            if (job.Community != Community_ddl.SelectedItemName)
            {
                CommonHelper.HighLightElement(Community_ddl);
                isPassed = false;
            }
            if (job.House != House_ddl.SelectedItemName)
            {
                CommonHelper.HighLightElement(House_ddl);
                isPassed = false;
            }
            Console.WriteLine(Lot_ddl.SelectedItemName);
            if (job.Lot != Lot_ddl.SelectedItemName && (!Lot_ddl.SelectedItemName.Contains("0") || !Lot_ddl.SelectedItemName.Contains("None")))
            {
                CommonHelper.HighLightElement(Lot_ddl);
                isPassed = false;
            }

            if (job.Orientation != Orientation_ddl.SelectedItemName)
            {
                CommonHelper.HighLightElement(Orientation_ddl);
                isPassed = false;
            }
            if (!isPassed)
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), "The Job is created with invalid data.");

            VerifyCutOffPhaseIsNotVisibleOnCurrentPage();
            return isPassed;
        }

        public bool IsHeaderBreadcrumbsCorrect(string _name)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedName = $"{_name}";
            return SubHeadTitle().Equals(expectedName) && !CurrentURL.EndsWith("jid=0");
        }

        public bool IsSaberisDisplayed
        {
            get
            {
                if (SyncSaberis_btn.IsNull())
                    return false;
                return SyncSaberis_btn.IsDisplayed();
            }
        }

        public void VerifyCutOffPhaseIsNotVisibleOnCurrentPage()
        {
            string xpath = "//*[@id=\"ctl00_CPH_Content_urlCutOffPhases\"]";
            var el = FindElementHelper.FindElement(FindType.XPath, xpath);
            if (el == null)
                ExtentReportsHelper.LogPass("Latest Cutoff Phase label is no longer visible in “New Job” page.");
        }

        public bool VerifyTheScheduleTemplateIsDisplayedOnCurrentPage()
        {
            bool isDisplayed = ScheduleTemplate_div.WaitForElementIsVisible(5, false);
            if (isDisplayed)
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(ScheduleTemplate_div), "The Scheduling Template button is displayed on page");
            else
                ExtentReportsHelper.LogFail("The Scheduling Template button is <font color='green'><b>NOT</b></font> displayed on page");
            return isDisplayed;
        }

        public bool VerifyTheScheduleTemplateIsNOTDisplayedOnCurrentPage()
        {
            bool isDisplayed = ScheduleTemplate_div.WaitForElementIsVisible(5, false);
            if (!isDisplayed)
                ExtentReportsHelper.LogPass("The Scheduling Template button is NOT displayed on page as expected");
            else
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(ScheduleTemplate_div), "The Scheduling Template button is <font color='green'><b>displayed</b></font> on page");
            return !isDisplayed;
        }

        public bool VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage()
        {
            bool isDisplayed = SyncToBuildPro_Btn.WaitForElementIsVisible(5, false);
            if (isDisplayed)
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(SyncToBuildPro_Btn), "The Sync PO to BuildPro button is displayed on page");
            else
                ExtentReportsHelper.LogFail("The Sync PO to BuildPro button is <font color='green'><b>NOT</b></font> displayed on page");
            return isDisplayed;
        }

        public bool VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage()
        {
            bool isDisplayed = SyncToBuildPro_Btn.WaitForElementIsVisible(5, false);
            if (!isDisplayed)
                ExtentReportsHelper.LogPass("The Sync PO to BuildPro button is NOT displayed on page as expected");
            else
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SyncToBuildPro_Btn), "The Sync PO to BuildPro button is <font color='green'><b>displayed</b></font> on page");
            return !isDisplayed;
        }

        public bool VerifyTheViewBuildProEPOButtonIsDisplayedOnCurrentPage()
        {
            bool isDisplayed = ViewBuildProEPO_Btn.WaitForElementIsVisible(5, false);
            if (isDisplayed)
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(ViewBuildProEPO_Btn), "The View BuildPro EPO to BuildPro button is displayed on page");
            else
                ExtentReportsHelper.LogFail("The View BuildPro EPO button is <font color='green'><b>NOT</b></font> displayed on page");
            return isDisplayed;
        }

        public bool VerifyTheViewBuildProEPOButtonIsNOTDisplayedOnCurrentPage()
        {
            bool isDisplayed = ViewBuildProEPO_Btn.WaitForElementIsVisible(5, false);
            if (!isDisplayed)
                ExtentReportsHelper.LogPass("The View BuildPro EPO to BuildPro button is NOT displayed on page as expected");
            else
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(ViewBuildProEPO_Btn), "The View BuildPro EPO button is <font color='green'><b>displayed</b></font> on page");
            return !isDisplayed;
        }

        public bool VerifyPOOnViewPOPageWithStatus(string POName, string status, string createdDate)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']//tr[./td[4][.='{POName}'] and ./td[6][.='{status}'] and ./td[7][.='{createdDate}']]";
            Label pO_lbl = new Label(FindType.XPath, xpath);
            if (pO_lbl.IsExisted(false))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(pO_lbl), $"The PO <font color='green'><b>{POName}</b></font> and Status <font color='green'><b>{status}</b></font> created on <font color='green'><b>{createdDate}</b></font> is displayed.");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"The PO <font color='green'><b>{POName}</b></font> and Status <font color='green'><b>{status}</b></font> created on <font color='green'><b>{createdDate}</b></font> could not be found.");
                return true;
            }
        }

        public bool IsPOOnViewPOPage(string poName, string status, string createdDate, string userCreated)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']/tbody/tr/following-sibling::tr//tbody/tr[./td[6][.='{status}'] and ./td[7][.='{createdDate}'] and ./td[8][.='{userCreated}'] and ./td[4][contains(.,'{poName}')]]";
            Label POFound_Lbl = new Label(FindType.XPath, xpath);
            if (POFound_Lbl.WaitForElementIsVisible(3, false))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(POFound_Lbl), $"The PO <font color='green'><b>{poName}</b></font> and Status <font color='green'><b>{status}</b></font> created on <font color='green'><b>{createdDate}</b></font> is displayed.");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"The PO <font color='green'><b>{poName}</b></font> and Status <font color='green'><b>{status}</b></font> created on <font color='green'><b>{createdDate}</b></font> is displayed.");
                return false;
            }
        }

        public bool IsPOSyncToBuildProSuccessfully(string poName, string buildingPhaseCode)
        {
            Label result_Lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_BuildProSyncModal_autoGrid_rgResults_ctl00']/tbody/tr[./td[.='{buildingPhaseCode}'] and ./td[.='The purchase order \"{poName}\"  has been received by BuildPro.']]");
            if (result_Lbl.IsExisted(false))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(result_Lbl), $"The purchase order <font color='green'><b>\"{poName}\"</b></font> has been received by BuildPro.");
                return true;
            }
            ExtentReportsHelper.LogFail($"The PO with name {poName} with Building Phase Code {buildingPhaseCode} is not sync to BuildPro successfully.");
            return false;
        }

    }
}
