
using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;
using System.Text;

namespace Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase
{
    public partial class VendorBuildingPhasePage
    {
       
        public void AddBuildingPhase(string buildPhaseCode)
        {
            Add.Click();
            if (SaveBuildingPhase.WaitForElementIsVisible(10) == true)
            {
                CheckBox BuildPhase = new CheckBox(FindType.XPath, $"//*[contains(@id,'ctl00_CPH_Content_rlbBuildingPhase_i')]/label/span[contains(text(),'" + buildPhaseCode + "')]/preceding-sibling::input");
                BuildPhase.Check();
                SaveBuildingPhase.Click();
            }
            WaitBuildingPhaseGird();
        }

        public void WaitBuildingPhaseGird()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");
        }

        public bool IsItemExist(string buildingPhaseCode)
        {
            return BuildingPhaseTable.IsItemWithTextContainsOnCurrentPage("Building Phase", buildingPhaseCode);

        }
        public bool IsItemInGrid(string columnName, string valueToFind)
        {
            return BuildingPhaseTable.IsItemOnCurrentPage(columnName, valueToFind);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator GridFilterOperator, string value)
        {
            BuildingPhaseTable.FilterByColumn(columnName, GridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");        }

        public int TotalItemBuildingPhaseGird()
        {
            return BuildingPhaseTable.GetTotalItems;
             
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingPhaseTable.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");
        }

        /// <summary>
        /// Import/ Export Building Phase Phase attributes from Utilities menu
        /// </summary>
        /// <param name="item"></param>
        /// <param name="BuildingPhaseName"></param>
        public void ImportExporFromMoreMenu(string item, string Vendor)
        {
            string BuildingPhaseExportName = "Pipeline_VendorsToBuildingPhases_" + Vendor;

            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();
            switch (item)
            {
                case "Export CSV":
                    SelectItemInUtiliestButton(item, true);
                    ExportFile("CSV", BuildingPhaseExportName);
                    break;

                case "Export Excel":
                    SelectItemInUtiliestButton(item, true);
                    ExportFile("XLSX", BuildingPhaseExportName);
                    break;


                case "Import":
                    SelectItemInUtiliestButton(item, true);
                    break;

                default:
                    ExtentReportsHelper.LogInformation("Not found Import/Export items");
                    return;
            }
        }

        /// <summary>
        /// Export Building Phase in Vendor BuildingPhase to CSV/ Excel file
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportAttributeName"></param>
        public void ExportFile(string exportType, string exportAttributeName)
        {
            string extention;
            //Resolve file extension before filesystem check
            if (exportType == "CSV")
                extention = "CSV";
            else
                extention = "XLSX";

            string fileName = $"{exportAttributeName}.{extention.ToLower()}";
            string fullFilePath = CommonHelper.GetFullFilePath("Download\\" + fileName);

            System.Threading.Thread.Sleep(3000);

            // Verify the download file exists on the default saved file location or not
            if (File.Exists(fullFilePath))
            {
                ExtentReportsHelper.LogPass(null, $"The export file <font color='green'><b>'{fileName}'</b></font> file downloaded successfully and existed on the file system.");
                string content = File.ReadAllText(fullFilePath, Encoding.UTF8);
                if (string.IsNullOrEmpty(content) is true)
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Can't read the <b>'{fileName}'</b> file on location: <b>{CommonHelper.GetFullFilePath("Download")}</b>" +
                   $"<br>The export file is empty.</font>");
            }
            else
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Can't find the '{fileName}' file on location: <b>{CommonHelper.GetFullFilePath("Download")}</b>" +
                    $"<br>Failed to export <font color='red'><b>'{fileName}'</b></font> file.");

            // Remove focus from Utilities panel to continue following export steps
            Utilities_btn.Click(false);
        }

        /// <summary>
        /// Import Building Phase to Vendor Building Phase
        /// </summary>
        /// <param name="importTitle"></param>
        /// <param name="importFileDir"></param>
        public void ImportFile(string importTitle, string importFileDir)
        {
            string textboxUpload_Xpath, importButtion_Xpath, message_Xpath;
            switch (importTitle)
            {
                case "Vendors To Building Phases Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportVendorsToBuildingPhases']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbUploadVendorsToBuildingPhases']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblVendorsToBuildingPhases']";
                    break;

                case "Vendors To Products Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportVendorsToProducts']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbUploadVendorsToProducts']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblVendorsToProducts']";
                    break;

                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle}.</font>");
                    return;
            }

            // Upload file to corect grid
            Textbox Upload_txt = new Textbox(FindType.XPath, textboxUpload_Xpath);
            Button ProductImport_btn = new Button(FindType.XPath, importButtion_Xpath);

            // Get upload file location
            string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + importFileDir;

            // Upload
            Upload_txt.SendKeysWithoutClear(fileLocation);
            System.Threading.Thread.Sleep(500);
            ProductImport_btn.Click(false);
            PageLoad();

            // Verify message
            IWebElement message = FindElementHelper.FindElement(FindType.XPath, message_Xpath);
            string expectedMess = "Import complete.";
            if (message.Displayed is false || message.GetAttribute("value") == expectedMess)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import message isn't same as the expectation.</font>" +
                    $"<br>The expected message: {expectedMess}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import {importTitle} to current Vendor Building Phase successfuly.</b></font>");
            }

        }
        public void Close()
        {
            Close_btn.Click();
        }
    }
}
