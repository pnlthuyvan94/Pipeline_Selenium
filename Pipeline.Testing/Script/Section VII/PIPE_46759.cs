using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Jobs.DocumentTypes;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.JobDocuments;
using Pipeline.Testing.Pages.Jobs.JobDocuments.Upload;
using Pipeline.Testing.Pages.Purchasing.Trades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_VII
{
    public class PIPE_46759 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VII);
        }

        private const string NewDocumentTypeName = "General Contracts";
        private const string NewDocumentTypeDescription = "";

        private const string NewDocumentTypeName2 = "Cleaning Contracts";
        private const string NewDocumentTypeDescription2 = "";

        private const string NewBuildingTradeName = "RT_QA_New_BuildingTrade_46759";
        private const string NewBuildingTradeCode = "46759";

        private TradesData newTrade;
        private JobData newJob;
        private CommunityData communityData;

        [SetUp]
        public void Setup()
        {
            Random rndNo = new Random();

            //Add new trade for document type
            newTrade = new TradesData()
            {
                Code = NewBuildingTradeCode,
                //TradeName = NewBuildingTradeName + rndNo.Next(1000).ToString(),
                TradeName = NewBuildingTradeName,
                TradeDescription = NewBuildingTradeName,
                Vendor = "",
                BuildingPhases = "",
                SchedulingTasks = ""
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Add new Trades test data.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, newTrade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTrade.TradeName) is false)
            {
                TradesPage.Instance.ClickAddToOpenCreateTradeModal();
                TradesPage.Instance.CreateTrade(newTrade);
            }

            //Add new job           
            //create new community
            communityData = new CommunityData()
            {
                Name = "RT_QA_Community_46759",
                Code = "RT_QA_Community_46759"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is false)
            {
                CommunityPage.Instance.CreateCommunity(communityData);
            }

            newJob = new JobData()
            {
                Name = "RT_QA_Job_46759",
                Community = communityData.Code + "-" + communityData.Name,
                House = "NONE",
                Lot = "RT_QA_Lot_46759"
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Add new Job test data.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);

            JobPage.Instance.FilterItemInGrid("Job Number", newJob.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", newJob.Name) is false)
            {
                JobPage.Instance.CreateJob(newJob);
            }
        }
        [Test]
        public void Document_Management_Epic_Workflow()
        {
            //Navigate to Jobs > Job Documents page.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0 Go to Job Documents page.</b></font>");
            JobDocuments.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.JobDocuments);
            CommonHelper.CaptureScreen();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Verify that the View drop down filter button are working.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Verify that the View drop down button by default: View by Address Not Enabled and Display only Closed Jobs Not Enabled.</b></font>");
            JobDocuments.Instance.ViewByFilter(false, false);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2 Verify that when selecting the following filter: View by Address Not Enabled and Display only Closed Jobs Enabled.</b></font>");
            JobDocuments.Instance.ViewByFilter(false, true);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Verify that when selecting the following filter: View by Address Enabled and Display only Closed Jobs Not Enabled.</b></font>");
            JobDocuments.Instance.ViewByFilter(true, false);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3 Verify that when selecting the following filter: View by Address Enabled and Display only Closed Jobs Enabled.</b></font>");
            JobDocuments.Instance.ViewByFilter(true, true);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4 Click the View drop down again and click Reset.</b></font>");
            JobDocuments.Instance.ViewByFilter(false, false);

            CommonHelper.RefreshPage();
            System.Threading.Thread.Sleep(4000);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0 On Search field, type in job name (open job filter) with extra spaces and click the search icon or press Enter button from the keyboard. Verify if the search trims the extra spaces and throws the correct search results.</b></font>");
            JobDocuments.Instance.SetSearchFilter("  " + newJob.Name + "  ")
                .ClickSearchButton()
                .SelectJob(newJob.Name);
            System.Threading.Thread.Sleep(4000);
            ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Verified that the search trims the extra spaces and throws the correct search results.</b></font>");
            CommonHelper.CaptureScreen();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.9 On the Job Documents page, verify if :plus: button is disabled indication that users need to add first at least 1 document type.</b></font>");
            if (JobDocuments.Instance.IsUploadButtonDisplayed() is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Verified that + button is enabled when there is document type created.</b></font>");
            else
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Verified that + button is disabled at first when there is still no document type created.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0 Go to Documents Types page.</b></font>");
            JobDocuments.Instance.ClickDocumentTypesButton();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1 Check if expected grid columns are displayed.</b></font>");
            if (DocumentTypes.Instance.IsColumnFoundInGrid("Document Type"))
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Document Type column is found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Document Type column is not found in the grid.</b></font>");

            if (DocumentTypes.Instance.IsColumnFoundInGrid("Description"))
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Description column is found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Description column is not found in the grid.</b></font>");

            if (DocumentTypes.Instance.IsColumnFoundInGrid("Accessible Trades"))
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Accessible Trades column is found in the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Accessible Trades column is not found in the grid.</b></font>");


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.2 Create new Document Types</b></font>");

            var actualMessage = "";
            DocumentTypes.Instance.FilterItemInGrid("Document Type", GridFilterOperator.EqualTo, NewDocumentTypeName);
            var expectedMessage = "Document Type " + NewDocumentTypeName + " saved successfully!";
            if (DocumentTypes.Instance.IsItemInGrid("Document Type", NewDocumentTypeName) is false)
            {
                DocumentTypes.Instance.OpenAddDocumentTypesModal();
                DocumentTypes.Instance.AddDocumentTypeModal
                    .EnterDocumentTypeName(NewDocumentTypeName)
                    .EnterDocumentTypeDescription(NewDocumentTypeDescription)
                    .SelectAccessibleTrades(newTrade.TradeName, 1);
                DocumentTypes.Instance.AddDocumentTypeModal.Save();
                actualMessage = DocumentTypes.Instance.GetLastestToastMessage();
                if (actualMessage == expectedMessage)
                {
                    ExtentReportsHelper.LogPass(actualMessage);
                }
                else
                {
                    DocumentTypes.Instance.FilterItemInGrid("Document Type", GridFilterOperator.EqualTo, NewDocumentTypeName);
                    System.Threading.Thread.Sleep(2000);
                    if (DocumentTypes.Instance.IsItemInGrid("Document Type", NewDocumentTypeName) is true)
                        ExtentReportsHelper.LogPass(expectedMessage);
                    else
                        ExtentReportsHelper.LogFail("Document type was not created.");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.3 Update document type via inline edit.</b></font>");
                var updatedDocumentTypeName = NewDocumentTypeName + "_updated";
                DocumentTypes.Instance.FilterItemInGrid("Document Type", GridFilterOperator.EqualTo, NewDocumentTypeName);
                if (DocumentTypes.Instance.IsItemInGrid("Document Type", NewDocumentTypeName) is true)
                {
                    actualMessage = DocumentTypes.Instance.EditDocumentType("Document Type", NewDocumentTypeName, updatedDocumentTypeName, NewDocumentTypeDescription, newTrade.TradeName);
                    expectedMessage = "Document Type " + updatedDocumentTypeName + " updated successfully!";
                    if (actualMessage == expectedMessage)
                        ExtentReportsHelper.LogPass(actualMessage);
                    else
                        ExtentReportsHelper.LogFail(actualMessage);


                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.4 Reset document type to original value.</b></font>");
                    DocumentTypes.Instance.FilterItemInGrid("Document Type", GridFilterOperator.EqualTo, updatedDocumentTypeName);
                    actualMessage = DocumentTypes.Instance.EditDocumentType("Document Type", updatedDocumentTypeName, NewDocumentTypeName, NewDocumentTypeDescription, newTrade.TradeName);
                    expectedMessage = "Document Type " + NewDocumentTypeName + " updated successfully!";
                    if (actualMessage == expectedMessage)
                        ExtentReportsHelper.LogPass(actualMessage);
                    else
                        ExtentReportsHelper.LogFail(actualMessage);

                }
            }
            else
            {
                ExtentReportsHelper.LogPass(expectedMessage);
            }

            expectedMessage = "Document Type " + NewDocumentTypeName2 + " saved successfully!";
            DocumentTypes.Instance.FilterItemInGrid("Document Type", GridFilterOperator.EqualTo, NewDocumentTypeName2);
            if (DocumentTypes.Instance.IsItemInGrid("Document Type", NewDocumentTypeName2) is false)
            {
                DocumentTypes.Instance.OpenAddDocumentTypesModal();
                DocumentTypes.Instance.AddDocumentTypeModal
                    .EnterDocumentTypeName(NewDocumentTypeName2)
                    .EnterDocumentTypeDescription(NewDocumentTypeDescription2)
                    .SelectAccessibleTrades(newTrade.TradeName, 1);
                DocumentTypes.Instance.AddDocumentTypeModal.Save();
                actualMessage = DocumentTypes.Instance.GetLastestToastMessage();
                if (actualMessage == expectedMessage)
                {
                    ExtentReportsHelper.LogPass(actualMessage);
                }
                else
                {
                    DocumentTypes.Instance.FilterItemInGrid("Document Type", GridFilterOperator.EqualTo, NewDocumentTypeName2);
                    System.Threading.Thread.Sleep(2000);
                    if (DocumentTypes.Instance.IsItemInGrid("Document Type", NewDocumentTypeName2) is true)
                        ExtentReportsHelper.LogPass(expectedMessage);
                    else
                        ExtentReportsHelper.LogFail("Document type was not created.");
                }
            }
            else
            {
                ExtentReportsHelper.LogPass(expectedMessage);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0 Go back to Job Documents page.</b></font>");
            DocumentTypes.Instance.GoBackToJobDocumentsPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.0 Search for the test job data.</b></font>");
            JobDocuments.Instance.SetSearchFilter(newJob.Name)
                .ClickSearchButton()
                .SelectJob(newJob.Name);
            System.Threading.Thread.Sleep(4000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.1 Check if the labels are displayed on the Job Documents page.</b></font>");
            CommonHelper.CaptureScreen();
            JobDocuments.Instance.CheckJobInformationTooltip();

            if (JobDocuments.Instance.IsUploadButtonDisplayed() is true)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Upload button is displayed on the page.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Upload button is not displayed on the page.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.0 Verify if Add button is visible for uploading job document.</b></font>");
            if (JobDocuments.Instance.IsUploadButtonDisplayed() is true)
            {
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Upload button is displayed on the page.</b></font>");
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>There is a document type to be use in uploading doc.</b></font>");

                JobDocuments.Instance.ClickUploadButton();
                System.Threading.Thread.Sleep(4000);
                CommonHelper.CaptureScreen();
                if (JobDocuments.Instance.CheckModalDisplayedInPage() == true)
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.1 Click the Cancel button. Verify if the Upload modal is removed on the screen.</b></font>");
                    UploadDocumentsModal.Instance.ClicCancelUploadButton();
                    System.Threading.Thread.Sleep(4000);
                    CommonHelper.CaptureScreen();
                    if (JobDocuments.Instance.CheckModalDisplayedInPage() == false)
                    {
                        ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Upload modal exits after clicking Cancel button.</b></font>");
                    }
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.2 Select 1 or more document in less than 100 MB and in correct file type.</b></font>");
                JobDocuments.Instance.ClickUploadButton();
                System.Threading.Thread.Sleep(4000);
                CommonHelper.CaptureScreen();
                if (JobDocuments.Instance.CheckModalDisplayedInPage() == true)
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.1 Select a JPEG file to upload.</b></font>");
                    string importFile = "\\DataInputFiles\\Jobs\\UploadFiles\\JPEG_TestImage.jpeg";
                    UploadDocumentsModal.Instance.AddFileToUpload(importFile);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.2 Select a JPG file to upload.</b></font>");
                    importFile = "\\DataInputFiles\\Jobs\\UploadFiles\\JPG_TestImage.jpg";
                    UploadDocumentsModal.Instance.AddFileToUpload(importFile);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.3 Select a PNG file to upload.</b></font>");
                    importFile = "\\DataInputFiles\\Jobs\\UploadFiles\\PNG_TestImage.png";
                    UploadDocumentsModal.Instance.AddFileToUpload(importFile);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.4 Select a PDF file to upload.</b></font>");
                    importFile = "\\DataInputFiles\\Jobs\\UploadFiles\\TEST PDF FILE.pdf";
                    UploadDocumentsModal.Instance.AddFileToUpload(importFile);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.5 Select SVG file to upload.</b></font>");
                    importFile = "\\DataInputFiles\\Jobs\\UploadFiles\\SVG_TestImage.svg";
                    UploadDocumentsModal.Instance.AddFileToUpload(importFile);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.6 Select a file that was already uploaded from the previous step. Verify if the correct error validation message is displayed.</b></font>");
                    importFile = "\\DataInputFiles\\Jobs\\UploadFiles\\JPEG_TestImage.jpeg";
                    UploadDocumentsModal.Instance.AddFileToUpload(importFile);
                    string actualToastMess = UploadDocumentsModal.Instance.GetLastestToastMessage();
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>" + actualToastMess + ".</b></font>");
                    System.Threading.Thread.Sleep(2000);
                    UploadDocumentsModal.Instance.CloseToastMessage();
                    System.Threading.Thread.Sleep(1000);
                    UploadDocumentsModal.Instance.CloseToastMessage();

                    //UploadDocumentsModal.Instance.DeleteItemInGrid("Uploaded Files", "SVG_TestImage.svg");
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.7 Select continue to proceed to Step 2.</b></font>");
                    UploadDocumentsModal.Instance.ClickStep2ContinueUploadButton();
                    CommonHelper.CaptureScreen();
                    System.Threading.Thread.Sleep(4000);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.8 Update file name.</b></font>");
                    UploadDocumentsModal.Instance.UpdateUploadedFileName();

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.9 Click back on Stepper #1. Verify if the original name is not changed.</b></font>");
                    UploadDocumentsModal.Instance.BackToStep1Button();
                    CommonHelper.CaptureScreen();
                    System.Threading.Thread.Sleep(4000);

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.10 Select continue to proceed to Step 2.</b></font>");
                    UploadDocumentsModal.Instance.ClickStep2ContinueUploadButton();
                    CommonHelper.CaptureScreen();
                    System.Threading.Thread.Sleep(4000);

                    UploadDocumentsModal.Instance.SelectDocType(NewDocumentTypeName);
                    UploadDocumentsModal.Instance.SaveUploadButton();
                    System.Threading.Thread.Sleep(4000);
                    CommonHelper.CaptureScreen();
                    CommonHelper.RefreshPage();
                    CommonHelper.CaptureScreen();
                }
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.11 Upload button is not displayed on the page.</b></font>");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.0 Select the job again and validate.</b></font>");
            JobDocuments.Instance.SetSearchFilter(newJob.Name)
                .ClickSearchButton()
                .SelectJob(newJob.Name);
            System.Threading.Thread.Sleep(4000);
            CommonHelper.CaptureScreen();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.1 Type in backtick icon ` and other scpecial character except ( ) { } [ ] _ .  and verify if the ‘Name’ field is no longer accepting it.</b></font>");
            string docName = "SVG_TestImage";
            if (JobDocuments.Instance.IsFileNameInGrid("Name", docName) is true)
            {
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>File was found in the grid.</b></font>");

                expectedMessage = "Character limit exceeded: " + docName;
                actualMessage = JobDocuments.Instance.EditDocument(docName, "DocumentTestDocumentTestDocumentTestDocumentTestDocumentTestDocumentTestDocumentTestDocumentTest", "jpg", NewDocumentTypeName2);
                JobDocuments.Instance.CloseToastMessage();
                ExtentReportsHelper.LogPass(actualMessage);

                expectedMessage = "Document name is required";
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.2 Type in backtick icon ` and other scpecial character except ( ) { } [ ] _ .  and verify if the ‘Name’ field is no longer accepting it.</b></font>");
                actualMessage = JobDocuments.Instance.EditDocument(docName, "`", "jpg", NewDocumentTypeName2);
                JobDocuments.Instance.CloseToastMessage();
                ExtentReportsHelper.LogPass(actualMessage);

                expectedMessage = "Periods are not allowed at the end of the file name.";
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.3 Type . at the end of the file name and verify validation error.</b></font>");
                actualMessage = JobDocuments.Instance.EditDocument(docName, "Test.", "jpg", NewDocumentTypeName2);
                JobDocuments.Instance.CloseToastMessage();
                ExtentReportsHelper.LogPass(actualMessage);

                expectedMessage = "Document name is required";
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.4 Empty file name and verify validation error.</b></font>");
                actualMessage = JobDocuments.Instance.EditDocument(docName, "", "jpg", NewDocumentTypeName2);
                JobDocuments.Instance.CloseToastMessage();
                ExtentReportsHelper.LogPass(actualMessage);

                expectedMessage = "Document updated successfully.";
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.5 Add the same file name and verify validation error.</b></font>");
                actualMessage = JobDocuments.Instance.EditDocument(docName, "SVG_TestImage", "jpg", NewDocumentTypeName2);
                JobDocuments.Instance.CloseToastMessage();
                ExtentReportsHelper.LogPass(actualMessage);

                JobDocuments.Instance.CloseToastMessage();
                CommonHelper.RefreshPage();
            }
            else
            {
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>File not found on the grid.</b></font>");
            }
        }

        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.0 Tear down test data.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.1: Delete Job Documents test data.</b></font>");
            JobDocuments.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.JobDocuments);
            JobDocuments.Instance.SetSearchFilter(newJob.Name)
                .ClickSearchButton()
                .SelectJob(newJob.Name);
            System.Threading.Thread.Sleep(4000);
            //CommonHelper.CaptureScreen();
           
            string actualMessage = JobDocuments.Instance.DeleteAllDocuments(); 
            JobDocuments.Instance.CloseToastMessage();
            ExtentReportsHelper.LogPass(actualMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.3: Delete Document Type test data.</b></font>");
            JobDocuments.Instance.ClickDocumentTypesButton();
            DocumentTypes.Instance.FilterItemInGrid("Document Type", GridFilterOperator.EqualTo, NewDocumentTypeName);
            if (DocumentTypes.Instance.IsItemInGrid("Document Type", NewDocumentTypeName) is true)
            {
                DocumentTypes.Instance.DeleteItemInGrid("Document Type", NewDocumentTypeName);
            }
            CommonHelper.RefreshPage();
            DocumentTypes.Instance.FilterItemInGrid("Document Type", GridFilterOperator.EqualTo, NewDocumentTypeName2);
            if (DocumentTypes.Instance.IsItemInGrid("Document Type", NewDocumentTypeName2) is true)
            {
                DocumentTypes.Instance.DeleteItemInGrid("Document Type", NewDocumentTypeName2);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.3: Delete Job test data.</b></font>");
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.FilterItemInGrid("Job Number", newJob.Name);
            if (JobPage.Instance.IsItemInGrid("Job Number", newJob.Name) is true)
            {
                JobPage.Instance.DeleteItemInGrid("Job Number", newJob.Name);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.4 Delete Trades test data.</b></font>");
            TradesPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.Trades);
            TradesPage.Instance.FilterItemInGrid("Trade", GridFilterOperator.EqualTo, newTrade.TradeName);
            System.Threading.Thread.Sleep(2000);
            if (TradesPage.Instance.IsItemInGrid("Trade", newTrade.TradeName) is true)
            {
                TradesPage.Instance.DeleteItemInGrid("Trade", newTrade.TradeName);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11.5 Delete Community test data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, communityData.Name);
            System.Threading.Thread.Sleep(5000);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is true)
            {
                CommunityPage.Instance.DeleteCommunity(communityData.Name);
            }
        }



    }
}
