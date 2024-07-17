using NUnit.Framework;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System;
using System.IO;
using System.Net;
using System.Reflection;

namespace Pipeline.Testing.Pages.Documents
{
    public partial class DocumentPage : DashboardContentPage<DocumentPage>
    {
        public void ClickUploadDocument(string sitemapFileName)
        {
            Upload_txt.SendKeysWithoutClear(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + sitemapFileName);
            System.Threading.Thread.Sleep(500);
            PageLoad();
        }

        public bool IsItemInGridOption(string columnName, string value)
        {
            return DocumentsTable.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsNumberItemInGrid(int number)
        {
            return DocumentsTable.GetTotalItems == number;
        }

        public void FilterItemInGridOption(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            DocumentsTable.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_DocumentManager1_LoadingPanel1ctl00_CPH_Content_DocumentManager1_pnlDocs']/div[1]");
        }
        public void DeleteItemInGridOption(string columnName, string value)
        {
            DocumentsTable.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            DocumentsTable.WaitGridLoad();
        }
        public void UploadDocumentsAndVerify(params string[] uploadFileName)
        {
            foreach (string item in uploadFileName)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>Select file: {item} to upload.</font>");
                ClickUploadDocument($@"\DataInputFiles\Documents\{item}");
                Label textbox = new Label(FindType.XPath, $"//*/span[@class='ruFileWrap ruStyled']/span[.='{item}']");
                textbox.WaitForElementIsInVisible(10, false);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_DocumentManager1_LoadingPanel1ctl00_CPH_Content_DocumentManager1_pnlDocs']/div[1]");
                System.Threading.Thread.Sleep(7000);
                FilterItemInGridOption("Name", GridFilterOperator.Contains, item);
                // Verify file is uploaded successfully
                if (IsItemInGridOption("Name", item) is true)
                {
                    ExtentReportsHelper.LogPass($"Document <font color='green'><b>{item}</b></font> file is uploaded successfully.");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"Document <font color='red'><b>{item}</b></font> file is uploaded unsuccessfully.");
                }
            }
        }
        public void EditAndVerifyDocumentFile(string nameFile, string contains)
        {
            DocumentsTable.ClickEditItemInGrid("Name", nameFile);
            DocumentsTable.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);

            Textbox textbox = new Textbox(FindType.XPath, $"//table[contains(@id,'_rgDocuments')]/tbody/tr/td/span[text()='{nameFile}']//..//following-sibling::td[1]//textarea");
            textbox.SetText(contains);
            Button buttonAccept = new Button(FindType.XPath, $"//table[contains(@id,'_rgDocuments')]/tbody/tr/td/span[text()='{nameFile}']//..//following-sibling::td[3]//input[contains(@src,'accept')]");
            buttonAccept.Click();
            DocumentsTable.WaitGridLoad();
            System.Threading.Thread.Sleep(5000);

            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = "Record was successfully saved.";
            if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Save document unsuccessfully.");
                Assert.Fail($"Save documennt {nameFile} file <font color='red'><b<unsuccessfully</b></font>. Actual messsage: {_actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass(null, $"Save document <font color='green'><b>{nameFile}</b></font> file successfully.");
                CloseToastMessage();
            }
        }

        public void DeleteDocumentFile(params string[] deletedFileName)
        {
            foreach (string item in deletedFileName)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>Delete document: {item}.</font>");
                DocumentsTable.ClickDeleteItemInGrid("Name", item);
                ConfirmDialog(ConfirmType.OK);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_DocumentManager1_LoadingPanel1ctl00_CPH_Content_DocumentManager1_pnlDocs']/div[1]");

                string _actualMessage = GetLastestToastMessage();
                string _expectedMessage = "The document was successfully deleted.";
                if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail(null, $"Delete document unsuccessfully.");
                    //Assert.Fail($"Delete documennt <font color='red'><b>{item}</b></font> file unsuccessfully. Actual messsage: {_actualMessage}");
                }
                else
                {
                    //CloseToastMessage();
                    ExtentReportsHelper.LogPass(null, $"Delete document <font color='green'><b>{item}</b></font> file successfully.");
                }
            }
        }

        /// <summary>
        /// Down load document base on href
        /// </summary>
        /// <param name="href">hyperlink of file need to download</param>
        /// <param name="folderPath">Location to download</param>
        public void DownloadFile(string href, string folderPath)
        {
            using (WebClient wc = new WebClient())
            {
                string file = Path.GetFileName(href);
                wc.DownloadFile(
                    // Param1 = Link of file
                    new Uri(href),
                   // Param2 = Path to save
                   folderPath
                );
            }
        }
    }
}
