using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;
using System.Text;

namespace Pipeline.Testing.Pages.Costing.Vendor.VendorProduct
{
    public partial class VendorProductPage
    {
        /// <summary>
        /// Update Costing in Vendor
        /// </summary>
        /// <param name="buildingPhase"></param>
        /// <param name="productName"></param>
        /// <param name="material"></param>
        /// <param name="labor"></param>
        public void UpdateCostingforVerdor(string buildingPhase, string productName, string material, string labor)
        {
            Button Edit_btn = new Button(FindType.XPath, $"//span[contains(., '{buildingPhase}')]/ancestor::tr/following-sibling::tr//a[contains(text(), '{productName}')]/../following-sibling::td/input");
            Button Update_btn = new Button(FindType.XPath, $"//span[contains(., '{buildingPhase}')]/ancestor::tr/following-sibling::tr//tr/td/a[contains(text(),'{productName}')]/ancestor::tr[1]/following-sibling::tr[1]/td/descendant::a[contains(@id,'UpdateButton')]");

            // Click edit button
            Edit_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']", 1000);

            // Set value for Base Material Cost 
            string Material_Xpath_01 = $"//span[contains(., '{buildingPhase}')]/ancestor::tr/following-sibling::tr//tr/td/a[contains(text(),'{productName}')]/ancestor::tr[1]/following-sibling::tr[1]/td/descendant::input[contains(@id,'txtBaseMaterial') and contains(@class, 'riTextBox')]";
            string Material_Xpath_02 = $"//span[contains(., '{buildingPhase}')]/ancestor::tr/following-sibling::tr//tr/td/a[contains(text(),'{productName}')]/ancestor::tr[1]/following-sibling::tr[1]/td/descendant::input[contains(@id,'txtBaseMaterial_ClientState')]";

            IWebElement Material_txt_01 = driver.FindElement(By.XPath(Material_Xpath_01));
            IWebElement Material_txt_02 = driver.FindElement(By.XPath(Material_Xpath_02));
            Material_txt_01.Clear();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            js.ExecuteScript("arguments[0].value = '" + material + "';", Material_txt_01);
            //js.ExecuteScript("arguments[0].value = '" + material + "';", Material_txt_02);


            // Set value for Labor Cost
            string Labor_Xpath_01 = $"//span[contains(., '{buildingPhase}')]/ancestor::tr/following-sibling::tr//tr/td/a[contains(text(),'{productName}')]/ancestor::tr[1]/following-sibling::tr[1]/td/descendant::input[contains(@id,'txtBaseLabor') and contains(@class, 'riTextBox')]";
            string Labor_Xpath_02 = $"//span[contains(., '{buildingPhase}')]/ancestor::tr/following-sibling::tr//tr/td/a[contains(text(),'{productName}')]/ancestor::tr[1]/following-sibling::tr[1]/td/descendant::input[contains(@id,'txtBaseLabor_ClientState')]";

            IWebElement Labor_txt_01 = driver.FindElement(By.XPath(Labor_Xpath_01));
            IWebElement Labor_txt_02 = driver.FindElement(By.XPath(Labor_Xpath_02));
            Labor_txt_01.Clear();

            js.ExecuteScript("arguments[0].value = '" + labor + "';", Labor_txt_01);
            //js.ExecuteScript("arguments[0].value = '" + labor + "';", Labor_txt_02);


            // Click update and waiting
            Update_btn.Click();
            System.Threading.Thread.Sleep(5000);
            //WaitVendorProductBuildingPhaseGird();
        }


        public void WaitVendorProductBuildingPhaseGird()
        {
            WaitingLoadingGifByXpath("//*[@id='//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']/div[1]']");
        }
        public void OpenAndVerifyCostComparison(string Vendor, string Building, string product)
        {
            Label ProductCost_lbl = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_tiltle']");
            Button CostComparision_btn = new Button(FindType.XPath, $"//table[contains(@id,'ctl00_CPH_Content_rgBuildingPhases_ctl00_ct')]/tbody/tr/td/a[contains(text(),'{product}')]/../following-sibling::td/a/img");
            CostComparision_btn.Click();
            PageLoad();
            ProductCost_lbl.IsDisplayed(false);
            Textbox BuildingPhase_lbl = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgProductCostComparisonPanel']//a[contains(text(),'{Vendor}')] //ancestor::tr/td/span[contains(text(),'{Building}')]");
            if (ProductCost_lbl.GetText().Equals("Product Cost Comparison") && CurrentURL.Contains("Dashboard/Costing/Products/ProductCosts.aspx?"))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Product Comparison is displayed.</b></font>");
                if (BuildingPhase_lbl.GetText().Equals(Building))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The {BuildingPhase_lbl.GetText()} is displayed in grid.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The {BuildingPhase_lbl.GetText()} is not displayed in grid.</font>");
                }
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Product Comparison is not displayed</font>");
            }
        }
        /// <summary>
        /// Import/ Export Building Phase Phase attributes from Utilities menu
        /// </summary>
        /// <param name="item"></param>
        /// <param name="BuildingPhaseName"></param>
        public void ImportExporFromMoreMenu(string item, string BuildingPhaseName)
        {
            string BuildingPhaseExportName = "Pipeline_VendorProducts_" + BuildingPhaseName;

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
        /// Export Building Phase in Vendor Products to CSV/ Excel file
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
        /// Import Building Phase to Vendor Products
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
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import {importTitle} to Vendor Product successfuly.</b></font>");
            }
        }
    }
}
