using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Jobs.JobDocuments.Upload;
using Pipeline.Testing.Pages.Purchasing.Trades.AddTrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.JobDocuments
{
    public partial class JobDocuments
    {
        public void ClickDocumentTypesButton()
        {
            DocumentTypes_btn.Click();
        }

        public void ClickUploadButton()
        {
            Upload_btn.Click();
            UploadDocumentsModal = new UploadDocumentsModal();
        }

        public bool CheckModalDisplayedInPage()
        {
            if (ModalTitle_lbl.IsDisplayed() == true)
            {
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Upload Document Modal is displayed on the page.</b></font>");
                return true;
            }
            return false;
        }

        public JobDocuments SetSearchFilter(string data)
        {
            if (!string.IsNullOrEmpty(data))
                JobSearch_Txt.SetText(data);
            return this;
        }

        public JobDocuments ViewByFilter(bool viewByAddress, bool displayOnlyClosedJobs)
        {
            ViewByFilter_btn.Click();
            System.Threading.Thread.Sleep(2000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ViewByFilter_btn), 
                $"Click <font color='green'><b><i>View</i></b></font> button.");
            if ((viewByAddress == false) && (displayOnlyClosedJobs == false))
            {
                Reset_btn.Click();
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>It does not have any checkbox enabled. No view filter was selected.</b></font>");
            }
            else if ((viewByAddress == false) && (displayOnlyClosedJobs == true))
            {
                ViewByAddress_chk.UnCheck();
                DisplayOnlyCloseJobs_chk.Check();
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>View by Address is not selected and Display only Closed Jobs is selected.</b></font>");
            }
            else if ((viewByAddress == true) && (displayOnlyClosedJobs == false))
            {
                ViewByAddress_chk.Check();
                DisplayOnlyCloseJobs_chk.UnCheck();
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>View by Address is selected and Display only Closed Jobs is not selected.</b></font>");
            }
            else
            {
                ViewByAddress_chk.Check();
                DisplayOnlyCloseJobs_chk.Check();
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>View by Address and Display only Closed Jobs are both selected.</b></font>");
            }
            CommonHelper.CaptureScreen();
            Filter_btn.Click();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Filter_btn),
                $"Click <font color='green'><b><i>View</i></b></font> button.");
            System.Threading.Thread.Sleep(4000);
            return this;
        }

        public JobDocuments CheckJobInformationTooltip()
        {
            JobInformationTooltip.HoverMouse();
            CommonHelper.CaptureScreen();
            if (JobInformationTooltip.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Tooltip displays the information of the job</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Tooltip did not display the information of the job</font color>");
            }
            CommonHelper.CaptureScreen();
            return this;
        }

        public JobDocuments ClickFilterButton()
        {
            Filter_btn.Click();
            System.Threading.Thread.Sleep(4000);
            return this;
        }
        
        public JobDocuments ClickSearchButton()
        {
            JobSearch_btn.Click();
            System.Threading.Thread.Sleep(4000);
            return this;
        }

        public JobDocuments SelectJob(string jobNumber)
        {
            Link jobLink = new Link(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgJobDocumentsLinks_ctl00'" +
             "]/tbody/tr/td/div/a[contains(@title,'" + jobNumber + "')]");
            jobLink.Click();
            System.Threading.Thread.Sleep(4000);
            return this;
        }

        public void ClickAddDocumentsButton()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
        }
        
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Documents_grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobDocument']");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Documents_grid.IsItemOnCurrentPage(columnName, value);
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            Documents_grid.ClickItemInGrid(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Documents_grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public string DeleteAllDocuments()
        {
            CloseToastMessage();
            SelectAll_chk.Check();
            System.Threading.Thread.Sleep(2000);
            CommonHelper.CaptureScreen();
            BulkAction_Btn.Click();
            DeleteAll_lnk.Click();
            ConfirmDialog(ConfirmType.OK);
            CommonHelper.CaptureScreen();
            PageLoad();
            return GetLastestToastMessage();
        }

        public bool IsFileNameInGrid(string columnName, string valueToFind)
        {
            return Documents_grid.IsItemOnCurrentPageV2(columnName, valueToFind);
        }

        public string EditDocument(string oldName, string newName, string oldDoctype, string newDocumentType)
        {
            IWebElement webElement = driver.FindElement(By.XPath("//table[@id='ctl00_CPH_Content_rgJobDocument_ctl00']/thead/tr[2]/th[2]"));

            Documents_grid.SelectGridRowV2("Name", oldName);
            Textbox name_txt = new Textbox(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgJobDocument_ctl00']/tbody/tr/td/div/input[contains(@id,'Documents_Name_Display_txtUpdateJobDocumentName')]");
            name_txt.Clear();
            name_txt.SetText(newName);
            CommonHelper.CaptureScreen();

            Documents_grid.SelectGridRowV2("Document Type", oldDoctype);
            DropdownList documentType_dll = new DropdownList(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgJobDocument_ctl00']/tbody/tr/td/div/div[contains(@id,'JobDocumentType_ddlDocumentTypes')]/select");
            documentType_dll.SelectItem(newDocumentType);
            CommonHelper.CaptureScreen();

            Button saveBtn = new Button(FindType.Xpath, "//table[@id='ctl00_CPH_Content_rgJobDocument_ctl00'" +
                "]/thead/tr[1]/td/table/tbody/tr/td/a[contains(@id,'SaveChangesButton')]");
            saveBtn.Click();
            WaitingLoadingGifByXpath(_gridLoading, 1000);
            CommonHelper.CaptureScreen();

            return GetLastestToastMessage();
        }

        public bool CheckAddDocumentButtonStatus()
        {
            string btnClass = AddDocument_btn.GetAttribute("class");
            if (btnClass.Contains("aspNetDisabled"))
            {
                return false;
            }
            return true;
        }


    }
}
