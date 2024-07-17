using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Import
{
    public class ProductsImportPage : DashboardContentPage<ProductsImportPage>
    {

        /******************************* View = Product Attribute *********************************/

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
                case ImportGridTitle.USE_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportUses']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportUses']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblUses']";
                    break;
                case ImportGridTitle.OPTION_CATEGORY_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportCategories']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportCategories']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblImportCategories']";
                    break;
                case ImportGridTitle.PRODUCT_TO_CATEGORIES_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportProductsToCategories']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportProductsToCategories']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblImportProductsToCategories']";
                    break;
                case ImportGridTitle.PRODUCT_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportProducts']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportProducts']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblProduct']";
                    break;
                case ImportGridTitle.BUILDING_PHASE_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportGroupsPhases']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportGroupsPhases']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblPhases']";
                    break;
                case ImportGridTitle.UNIT_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportUnits']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportUnits']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lbImportUnits']";
                    break;
                case ImportGridTitle.OPTION_PRODUCT_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionProducts']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportOptionProducts']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblProducts']";
                    break;
                case ImportGridTitle.SPEC_SET_GROUP_AND_SPEC_SET_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportSpecSetGroupsToSpecSetCreation']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportSpecSetGroupsToSpecSetCreation']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblSpecSetGroupsToSpecSetCreation']";
                    break;
                case ImportGridTitle.COMMUNITY_TO_SPEC_SET_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportCommunityToSpecSets']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportCommunityToSpecSets']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblCommunityToSpecSets']";
                    break;
                case ImportGridTitle.OPTION_TO_SPEC_SET_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionToSpecSets']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportOptionToSpecSets']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblOptionToSpecSets']";
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
        /// Import with valid file
        /// </summary>
        /// <param name="fullFilePath"></param>
        public void ImportValidData(string importGridTitle, string fullFilePath)
        {
            string actualMessage = ImportFile(importGridTitle, fullFilePath);
            string expectedMessage = ExpectedImportGridTitle(importGridTitle);
            if (expectedMessage.ToLower().Contains(actualMessage.ToLower()) is false)
            {
                if (actualMessage.Contains("Import Completed, however"))
                {
                    ExtentReportsHelper.LogPass(
                        "<font color='green'><b>The valid file was imported successfully except invalid item(s)</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail("<font color='red'>The valid file was NOT imported." +
                    $"<br>The toast message is: {actualMessage}</br></font>");
                }
            }
            else
                ExtentReportsHelper.LogPass(
                    "<font color='green'><b>The valid file was imported successfully and the toast message indicated success.</b></font>");
        }
        public string ExpectedImportGridTitle(string importGridTitle)
        {
            string expectedMessage;
            if (importGridTitle.Equals(ImportGridTitle.OPTION_PRODUCT_IMPORT))
                expectedMessage = "Import successful.";
            else
                expectedMessage = "Import complete.";
            return expectedMessage;
        }
        /// <summary>
        /// Import with invalid file
        /// </summary>
        /// <param name="fullFilePath"></param>
        /// <param name="expectedFailedData"></param>
        public void ImportInvalidData(string importGridTitlte, string fullFilePath, string expectedFailedData)
        {
            string actualMess = ImportFile(importGridTitlte, fullFilePath);

            if (actualMess.ToLower().Contains(expectedFailedData.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file was failed to import and the toast message indicated failure.</b></font>");

        }

        /// <summary>
        /// Import with invalid format
        /// </summary>
        /// <param name="importGridTitlte"></param>
        /// <param name="fullFilePath"></param>
        /// <param name="ListErrorTypeMsg"></param>
        public void ImportInvalidFormat(string importGridTitlte, string fullFilePath, List<string> ListErrorTypeMsg)
        {
            ImportFile(importGridTitlte, fullFilePath);
            string ErrorLog_Xpath = "//*[@class='card-header clearfix']/h1[contains(text(),'Error Log')]";
            Label ErrorLog = new Label(FindType.XPath, ErrorLog_Xpath);
            if (ErrorLog.IsDisplayed() is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Error Log Is Displayed On UI.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Error Log Is Not Displayed On UI.</br></font>");
            }

            foreach (string message in ListErrorTypeMsg)
            {
                var rows = driver.FindElements(By.XPath("//table[@id = 'ctl00_CPH_Content_rgErrorLog_ctl00']/tbody/tr"));
                bool messageFound = false;
                for (int idx = 1; idx <= rows.Count; idx++)
                {
                    SpecificControls control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgErrorLog_ctl00']/tbody/tr[" + idx + "]/td[2]");
                    string errorMessage = control.GetAttribute("innerHTML");
                    
                    if (errorMessage.ToLower().Contains(message.ToLower()))
                    {
                        messageFound = true;
                    }                    
                }
                if (messageFound)
                    ExtentReportsHelper.LogPass($"The Error Type message With Text: <font color='green'><b>{message}</b></font> is displayed on Error Log");
                else
                    ExtentReportsHelper.LogFail($"The Error Type message With Text: <font color='red'>{message}.</font> is not displayed on Error Log");


                //Textbox ErrorType = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgErrorLogPanel']//tbody//td[contains(text(),'{message}')]");
                //SpecificControls control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgErrorLog_ctl00']/tbody/tr[1]/td[2]");
                //string errorMessage = control.GetAttribute("innerHTML");


                //if (message.Contains("Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings."))
                //{
                //    if (errorMessage.Contains(message))
                //    {
                //        ExtentReportsHelper.LogPass($"The Error Type message With Text: <font color='green'><b>{message}</b></font> is displayed on Error Log");
                //    }
                //    else
                //    {
                //        ExtentReportsHelper.LogFail($"The Error Type message With Text: <font color='red'>{message}.</font> is not displayed on Error Log");
                //    }
                //}
                //else
                //{
                //    if (errorMessage.Equals(message))
                //    {
                //        ExtentReportsHelper.LogPass($"The Error Type message With Text: <font color='green'><b>{message}</b></font> is displayed on Error Log");
                //    }
                //    else
                //    {
                //        ExtentReportsHelper.LogFail($"The Error Type message With Text: <font color='red'>{message}.</font> is not displayed on Error Log");
                //    }
                //}

            }

            Button Close_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbClose']");
            Close_btn.Click();
        }

        public void CloseErrorTable()
        {
            string ErrorLog_Xpath = "//*[@class='card-header clearfix']/h1[contains(text(),'Error Log')]";
            Label ErrorLog = new Label(FindType.XPath, ErrorLog_Xpath);
            if (!ErrorLog.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Error Log Is Not Displayed After Close Table.</b></font>");
            }
        }
    }
}
