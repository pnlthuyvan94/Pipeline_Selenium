using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Import
{
    public class BuilderBOMImportPage : DashboardContentPage<BuilderBOMImportPage>
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
                case "Worksheet Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportWorksheets']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportWorksheets']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblWorksheets']";
                    break;
                case "Custom Option Product Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportCustomOptionProducts']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportCustomOptionProducts']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblProducts']";
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
    }

}
