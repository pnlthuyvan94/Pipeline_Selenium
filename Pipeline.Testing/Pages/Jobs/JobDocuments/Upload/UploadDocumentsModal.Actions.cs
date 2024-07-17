using NUnit.Framework.Internal;
using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.Trades.AddTrade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pipeline.Testing.Pages.Jobs.JobDocuments.Upload
{
    public partial class UploadDocumentsModal
    {
        public void AddFileToUpload(string fileLocation)
        {
            uploadLink.Click();
            fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + fileLocation;
            driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(fileLocation);
            CommonHelper.CaptureScreen();
        }

        public void ClickUploadButton()
        {
            Upload_btn.Click();
        }

        public void BackToStep1Button()
        {
            BackToStep1_btn.Click();
        }

        public void SaveUploadButton()
        {
            SaveUpload_btn.Click();
            System.Threading.Thread.Sleep(4000);
            string actualToastMess = GetLastestToastMessage();
            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>" + actualToastMess + ".</b></font>");
            System.Threading.Thread.Sleep(4000);
        }

        public void ClickStep2ContinueUploadButton()
        {
            Step2ContinueUpload_btn.Click();
        }

        public void ClicCancelUploadButton()
        {
            CancelUpload_btn.Click();
            ConfirmDialog(ConfirmType.OK);
        }

        public void ClickBackToJobDocuments()
        {
            GetItemOnHeader(DashboardContentItems.Back).Click();
            //BackToJobDocuments_btn.Click();
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            UploadedJobDocumentPreview_grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            System.Threading.Thread.Sleep(4000);
        }

        public void SelectDocType(string newDocumentType)
        {
            if (!string.IsNullOrEmpty(newDocumentType))
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select document type.</b></font>");
                int ddlDocTypeCount = FindElementHelper.FindElements(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_pnl_UploadedJobDocument\"]//div[@class='rpt-values']", 5).Count();
                for (int i=0; i<ddlDocTypeCount; i++) 
                {
                    DropdownList documentType_dll = new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_rptUploadedJobDocument_ctl0" + i + "_ddlDocumentTypes\"]");
                    documentType_dll.SelectItem(newDocumentType);
                }
                System.Threading.Thread.Sleep(4000);
                CommonHelper.CaptureScreen();
            }
        }

        public void UpdateUploadedFileName()
        {
            Textbox file_Name = new Textbox(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_rptUploadedJobDocument_ctl00_txtUpdateJobDocumentName\"]");
            file_Name.SetText("JPEG_TestImageTest");
            System.Threading.Thread.Sleep(4000);
            CommonHelper.CaptureScreen();
        }


    }
}
