

using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Import
{
    class BOMBuildingPhaseRuleImportPage : DashboardContentPage<BOMBuildingPhaseRuleImportPage>
    {
        /// <summary>
        /// Verify import grid displays or not
        /// </summary>
        /// <param name="gridTitle"></param>
        /// <returns></returns>
        public bool IsImportGridDisplay(string gridTitle)
        {
            Label Import_lbl = new Label(FindType.XPath, $"//h1[text()='{gridTitle}']");
            if (Import_lbl.IsDisplayed())
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
        /// Import file in Community Import page
        /// </summary>
        /// <param name="importTitle"></param>
        /// <param name="importFileDir"></param>
        /// <returns></returns>
        public string ImportFile(string importTitle, string importFileDir)
        {
            string textboxUpload_Xpath, importButtion_Xpath, message_Xpath;
            switch (importTitle)
            {
                case "BOM Building Phase Rules Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportBOMPhaseRules']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportBOMPhaseRules']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblBOMPhaseRules']";
                    break;

                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle}.</font>");
                    return string.Empty;
            }

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
            IWebElement message = FindElementHelper.FindElement(FindType.XPath, message_Xpath);
            return message.Displayed ? message.Text : string.Empty;

        }
        /// <summary>
        /// Import with valid file
        /// </summary>
        /// <param name="fullFilePath"></param>
        public void ImportValidData(string importGridTitlte, string fullFilePath)
        {
            string actualMess = ImportFile(importGridTitlte, fullFilePath);

            string expectedMess = "Import complete.";

            if (actualMess.ToLower().Contains(expectedMess.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The valid file was NOT imported to '{importGridTitlte}'." +
                    $"<br>The toast message is: {actualMess}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid file was imported successfully to '{importGridTitlte}' and the toast message indicated success.</b></font>");
        }

        /// <summary>
        /// Import with invalid file
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
