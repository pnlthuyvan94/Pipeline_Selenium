using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Assets.House.Import
{
    public partial class HouseImportQuantitiesPage
    {
        /// <summary>
        /// Upload House Quantities file
        /// </summary>
        /// <param name="importType"></param>
        /// <param name="importFiles"></param>
        public void ImportHouseQuantities(string importType = "CSV", params string[] importFiles)
        {
            string actualMess;

            // Click Upload button and verify the modal
            UploadFile_btn.Click();

            if (Upload_lbl.IsDisplayed() is true)
                ExtentReportsHelper.LogPass("<font color ='green'><b>The Upload House Material Files modal displays correctly.<b></font>");
            else
                ExtentReportsHelper.LogFail("<font color ='red'>The Upload House Material Files modal doesn't display or the title isn't correct." +
                    "<br>Expected title: <b>Upload House Material Files</b></br></font>");

            // Select Import Type
            ImportType_ddl.SelectItem(importType);

            foreach (string importFile in importFiles)
            {
                string fullFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + importFile;
                UploadFile_txt.SendKeysWithoutClear(fullFilePath);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPendingFiles']");

                actualMess = GetLastestToastMessage();

                if (actualMess.ToLower().Contains("fail") is true)
                    ExtentReportsHelper.LogFail($"<font color='red'>The House quantities file was NOT uploaded successful to Upload File field on Upload House Material Files modal." +
                        $"<br>The actual message: {actualMess}</br></font>");
                CloseToastMessage();
            }

            // Select All File and click upload button
            SelectAllFile_ckb.SetCheck(true);
            UploadConfirm_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbUpload']");

            // Verify toast message
            actualMess = GetLastestToastMessage();
            string expectedMess = "Successfully saved";

            if (actualMess.ToLower().Contains(expectedMess.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The House quantities file was NOT uploaded successful on Upload House Material Files modal." +
                    $"<br>The actual message: {actualMess}</br>" +
                    $"<br>The expected message: {expectedMess}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The House quantities file was uploaded successfully on Upload House Material Files modal and the toast message indicated success.</b></font>");

            // Close modal
            CancelConfirm_btn.Click();
        }

        /// <summary>
        /// Generate Report for all upload Files
        /// </summary>
        public void GenerateReportViewAllFiles()
        {
            try
            {
                // Select all current house material file 
                SelectAllHouseMaterialFile_ckb.SetCheck(true);

                GenerateReportView_btn.Click();

                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbGenerateReportView']");

                // Wait to Import Quantities grid display and click Import button
                if (ImportQuantities_lbl.IsDisplayed() is true)
                    ExtentReportsHelper.LogPass("<font color ='green'><b>The Import Quantities grid displays correctly.<b></font>");
                else
                    ExtentReportsHelper.LogFail("<font color ='red'>TheImport Quantities grid doesn't display or the title isn't correct." +
                        "<br>Expected title: <b>Import Quantities</b></br></font>");

                // Click Import button
                Import_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbImport']");

                // Click Finish button
                FinishImport_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbImport']");
            }
            catch (Exception)
            {
                ExtentReportsHelper.LogFail($"<font color='yellow'>Can't generate Report View by internal error.</font>.");
            }
        }

        public void DeleteAllHouseMaterialFiles()
        {
            try
            {
                // Select all current house material file 
                SelectAllHouseMaterialFile_ckb.SetCheck(true);
                //WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbGenerateReportView']");

                // Click Bulk delete/ choose Delete Selected File button
                BulkAction_btn.Click();
                DeleteSelectedFiles_btn.Click();
                ConfirmDialog(ConfirmType.OK);
                WaitingLoadingGifByXpath(loadingIcon);
            }
            catch (Exception)
            {
                ExtentReportsHelper.LogFail($"<font color='yellow'>Can't find Deleted Seleted Files on Bulk Delete.</font>.");
            }
        }

        /// <summary>
        /// Delete House Material Files
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueToFind"></param>
        public void DeleteItemOnGrid(string columnName, string valueToFind)
        {
            HouseMaterialFile_grid.ClickDeleteItemInGrid(columnName, valueToFind);
            WaitingLoadingGifByXpath(loadingIcon);
        }
    }
}
