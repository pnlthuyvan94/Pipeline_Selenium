using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Import;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.Vendor.Import
{
    public partial class VendorsImportPage
    {
        /// <summary>
        /// Verify import grid displays or not
        /// </summary>
        /// <param name="gridTitle"></param>
        /// <returns></returns>
        public bool IsImportGridDisplay(string view, string gridTitle)
        {
            DropdownList view_ddl = new DropdownList(FindType.XPath, "//*[@id='ddlViewType']");
            if (view_ddl.IsDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find 'View' drop down list on the current page.</font>");
                return false;
            }

            if (view.Equals(view_ddl.SelectedItemName) is false)
            {
                // Current view is different from expected one, then re-select it
                view_ddl.SelectItem(view, true);
                PageLoad();
            }

            Label import_lbl = new Label(FindType.XPath, $"//h1[text()='{gridTitle}']");
            if (import_lbl.IsDisplayed() is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>{gridTitle} grid view displays successfully.</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find {gridTitle} grid view on the current page.</font>");
                return false;
            }
        }

        /// <summary>
        /// Import file in Product Import page
        /// </summary>
        /// <param name="importTitle"></param>
        /// <param name="importFileDir"></param>
        /// <returns></returns>
        public string ImportFile(string importTitle, string importFileDir)
        {
            string textboxUpload_Xpath, importButtion_Xpath, message_Xpath;
            switch (importTitle)
            {
                case ImportGridTitle.VENDORS_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportVendors']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportVendors']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblVendors']";
                    break;
                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle}.</font>");
                    return string.Empty;
            }

            try
            {
                // Upload file to corect grid
                Textbox Upload_txt = new Textbox(FindType.XPath, textboxUpload_Xpath);
                Button Import_btn = new Button(FindType.XPath, importButtion_Xpath);

                // Get upload file location
                string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + importFileDir;

                // Upload
                Upload_txt.RefreshWrappedControl();
                Upload_txt.SendKeysWithoutClear(fileLocation);
                System.Threading.Thread.Sleep(500);
                Import_btn.Click(false);
                PageLoad();

                // Get message
                Label message = new Label(FindType.XPath, message_Xpath);
                return message.IsDisplayed() ? message.GetText() : string.Empty;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Import with valid data
        /// </summary>
        /// <param name="importGridTitle"></param>
        /// <param name="fullFilePath"></param>
        public void ImportValidData(string importGridTitle, string fullFilePath)
        {
            string actualMessage = ImportFile(importGridTitle, fullFilePath);

            string expectedMessage = "Import complete.";
            if (expectedMessage.ToLower().Contains(actualMessage.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The valid file was NOT imported." +
                    $"<br>The toast message is: {actualMessage}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid file was imported successfully and the toast message indicated success.</b></font>");

        }

        /// <summary>
        /// Import with invalid data
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="expectedFailedData"></param>
        public void ImportInvalidData(string importGridTitlte, string fullFilePath, string expectedFailedData)
        {
            string actualMess = ImportFile(importGridTitlte, fullFilePath);

            if (expectedFailedData.ToLower().Contains(actualMess.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file was failed to import and the toast message indicated failure.</b></font>");
        }
    }
}
