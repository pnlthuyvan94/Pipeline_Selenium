using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Import
{
    public class BuilderImportPage : DashboardContentPage<BuilderImportPage>
    {
        /// <summary>
        /// Verify import grid displays or not
        /// </summary>
        /// <param name="gridTitle"></param>
        /// <returns></returns>
        public bool IsImportGridDisplay(string gridTitle)
        {
            Label CommunityImport_lbl = new Label(FindType.XPath, $"//h1[text()='{gridTitle}']");
            if (CommunityImport_lbl.IsDisplayed() is true)
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
                case "Community Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportCommunities']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportCommunity']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblCommunities']";
                    break;
                case "House Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportHouses']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportHouses']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblHouses']";
                    break;
                case "Lot Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportLots']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportLots']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblStatusMessageLots']";
                    break;
                case "Option Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptions']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportOptions']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblOptions']";
                    break;
                case ImportGridTitle.HOUSE_OPTION_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportHouseOptions']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportHouseOptions']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblHouseOptions']/span";
                    break;
                case ImportGridTitle.HOUSE_COMMUNITY_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportHouseCommunities']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportHouseCommunities']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblHouseCommunities']";
                    break;
                case ImportGridTitle.COMMUNITY_OPTION_IMPORT:
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportCommunityOptions']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportCommunityOptions']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblCommunityObtions']";
                    break;
                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle}.</font>");
                    return string.Empty;
            }

            // Upload file to corect grid
            Textbox Upload_txt = new Textbox(FindType.XPath, textboxUpload_Xpath);
            Button CommunityImport_btn = new Button(FindType.XPath, importButtion_Xpath);

            // Get upload file location
            string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + importFileDir;

            // Upload
            Upload_txt.RefreshWrappedControl();
            Upload_txt.SendKeysWithoutClear(fileLocation);
            System.Threading.Thread.Sleep(500);
            CommunityImport_btn.Click(false);
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

            string expectedMess;
            if (importGridTitlte.Equals(ImportGridTitle.LOT_IMPORT))
                expectedMess = "Lots successfully imported";
            else if (importGridTitlte.Equals(ImportGridTitle.HOUSE_OPTION_IMPORT))
                expectedMess = "Successfully imported";
            else
                expectedMess = "Import complete";

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
            string actualMess = BuilderImportPage.Instance.ImportFile(importGridTitlte, fullFilePath);

            if (actualMess.ToLower().Contains(expectedFailedData.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file was failed to import and the toast message indicated failure as expected.</b></font>");

        }
    }

}
