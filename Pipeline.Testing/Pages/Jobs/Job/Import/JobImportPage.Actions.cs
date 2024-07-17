using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;



namespace Pipeline.Testing.Pages.Jobs.Job.Import
{
    public partial class JobImportPage
    {
        private readonly string loadingIconXpath = "//*[contains(@id,'LoadingPanel1ctl00_CPH_Content')]/div[1]";

        public void ClickNoOptionSpecified()
        {
            NoOptionSpecified_btn.Click(false);
            // Wait loading icon
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_ddlFiles']/div[1]", 5);

            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Switch to the No Option Specified tab.</font>");
        }

        public void ClickOptionSpecified()
        {
            OptionSpecified_btn.Click(false);
            // Wait loading icon
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_ddlFiles']/div[1]", 5);

            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Switch to the Option Specified tab.</font>");
        }

        public void ClickProcessFile()
        {
            if (ProcessFile_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Cant' find button with name 'Process File'.</font>");
                return;
            }

            ProcessFile_btn.Click(false);
            // Wait loading icon
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_lbDelete']/div[1]", 5);
        }

        public void ClickFinishImport()
        {
            if (FinishImport_btn.IsDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Cant' find button with name 'Finish Import'.</font>");
                return;
            }

            FinishImport_btn.Click(false);

            // Get current toast message and verify it
            string actualToastMess = GetLastestToastMessage();
            string expectedToastMess = "The quantities imported successfully from the file!";

            if (actualToastMess.Equals(expectedToastMess))
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Process file and import successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Failed to import job quantities. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }
        }

        /// <summary>
        /// Get URL of job quantity page
        /// </summary>
        /// <returns></returns>
        public string GetJobQuantitiesURL()
        {
            return FindElementHelper.FindElement(FindType.XPath, "//*[text()='Quantities' and contains(@id, 'sidenavinner_rptMainNav')]").GetAttribute("href");
        }

        public void ExpandProductQuantitiesOnImportGrid()
        {
            // Expand Reconciled Product
            Button ExpandReconciledProducts = new Button(FindType.XPath, "//*[contains(@id, 'GECBtnExpandColumn')]");
            ExpandReconciledProducts.WaitForElementIsVisible(5);
            ExpandReconciledProducts.Click(false);

            // Wait loading icon
            WaitingLoadingGifByXpath("//*[contains(@id, 'lp1ctl00_CPH_Content_rgConfigs')]/div[1]", 5);

            // Expand all building phase
            Button ExpandBuildingPhase = new Button(FindType.XPath, "//*[text()='Building Phases CodeName']/../preceding-sibling::th");
            ExpandBuildingPhase.WaitForElementIsVisible(5);
            ExpandBuildingPhase.Click(false);
            // Wait loading icon
            WaitingLoadingGifByXpath("//*[contains(@id, 'lp1ctl00_CPH_Content_rgConfigs')]/div[1]", 5);
        }

        /// <summary>
        /// Verify product quantity after click Process Import button
        /// </summary>
        /// <param name="expectedData"></param>
        /// <returns></returns>
        public bool VerifyProductQuantitiesToImport(JobImportQuantitiesData expectedData)
        {
            bool result = true;

            // Get current building phase to compare
            Label buildingPhase = new Label(FindType.XPath, "//*[text()='Building Phases CodeName']//following::span[contains(@id, 'lblBuildingPhasesCodeName')]");

            if (buildingPhase.IsDisplayed() is true)
            {
                string actualBuildingPhase = buildingPhase.GetText().Trim();
                if (!actualBuildingPhase.Contains(expectedData.BuildingPhaseCode) || !actualBuildingPhase.Contains(expectedData.BuildingPhaseName))
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Building phase with code {expectedData.BuildingPhaseCode} and name {expectedData.BuildingPhaseName} doesn't display on the Product Quantities grid view to import.</font>");
                    result = false;
                }
            }
            else
                result = false;

            Label product = new Label(FindType.XPath, "//*[contains(@rel, 'Products/Details')]");

            if (product.IsDisplayed() is false || !expectedData.ProductName.Equals(product.GetText().Trim()))
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Product with name {expectedData.ProductName} doesn't display on the Product Quantities grid view to import.</font>");
                result = false;
            }

            if (result is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Building phase '{expectedData.BuildingPhaseName}' and Product '{expectedData.ProductName}' display correctly on the Product Quantities grid view to import.</b></font>");
            return result;
        }

        /// <summary>
        /// Upload job quantities by sending file to textbox
        /// </summary>
        /// <param name="jobQuantitiesFileName">upload filename</param>
        private void UploadJobQuantities(string jobQuantitiesFileName)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + jobQuantitiesFileName;
            Select_txt.SendKeysWithoutClear(path);
            System.Threading.Thread.Sleep(500);
            PageLoad();
        }

        /// <summary>
        /// Upload file and process it
        /// </summary>
        /// <param name="uploadFileName"></param>
        public void UploadJobQuantitiesAndProcess(string uploadFileName, bool isOptionSpecified = true)
        {
            string uploadFileLocation;

            if (isOptionSpecified is true)
                uploadFileLocation = "JobQuantities_OptionsSpecified\\" + uploadFileName;
            else
                uploadFileLocation = "JobQuantities_NoOptionsSpecified\\" + uploadFileName;

            ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>Select file: {uploadFileName} to upload.</font>");
            UploadJobQuantities($@"\DataInputFiles\Jobs\{uploadFileLocation}");
            Label textbox = new Label(FindType.XPath, $"//span[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlFilesPanel']//option[.='{uploadFileName}']");

            // Verify the upload file name on Job Quantities File text box
            if (textbox.WaitForElementIsInVisible(10, false) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Job Quantities file {uploadFileName} is uploaded successfully.</b></font>");

            // Verify the toast message
            string actualMessage = GetLastestToastMessage();
            string expectedToastMess = $"{uploadFileName} parsed successfully";
            if (expectedToastMess.Equals(actualMessage) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The toast message is same as the expectation.</b></font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='red'><b>The toast message is NOT same as the expectation.</b></font>" +
                    $"<br>Expected: {expectedToastMess}" +
                    $"<br>Actual: {actualMessage}");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click Process File button. Verify Product Quantities on the import grid.</b></font>");
            ClickProcessFile();
        }

        /// <summary>
        /// Verify product quantity after click Finish Import button
        /// </summary>
        /// <param name="expectedData"></param>
        /// <returns></returns>
        public void VerifyQuantitiesOnJobGrid(JobImportQuantitiesData expectedData)
        {
            // Verify Expand Option
            Button expandOption = new Button(FindType.XPath, $"//*[normalize-space(text())='{expectedData.Option}']/ancestor::tr/td[@class='rgExpandCol']/input");
            string buildingPhase_Xpath = $"//td[contains(text(), '{expectedData.BuildingPhaseCode+"-"+ expectedData.BuildingPhaseName}')]/../td[@class='rgExpandCol']";
            string product_Xpath = $"/../following-sibling::tr//td/a[@title='Details' and contains(text(),'{expectedData.ProductName}')]";
            string quantity_Xpath = $"/../following-sibling::td/span[contains(@id,'lblTotalQty') and contains(text(),'{expectedData.Quantities}')]";
            string deleteQuantity_Xpath = "/../following-sibling::td/input[contains(@id,'btnDelete')]";

            // Expand option and verify building phase
            if (expandOption.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option Expand All button to click.</font>");
                return;
            }
            expandOption.Click();
            WaitingLoadingGifByXpath(loadingIconXpath);        

            IWebElement buildingPhase = FindElementHelper.FindElement(FindType.XPath, buildingPhase_Xpath);
            if (buildingPhase == null || buildingPhase.Displayed == false)
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with code {expectedData.BuildingPhaseCode} and name {expectedData.BuildingPhaseName}.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Building Phase with code {expectedData.BuildingPhaseCode} and name {expectedData.BuildingPhaseName} displays correctly on the grid view.</b></font>");


            // Expand building phase and verify product + quantities
            ExtentReportsHelper.LogInformation(null, "Expand building phase and verify product and quantities");
            Button ExpandBuildingPhase = new Button(FindType.XPath, "//th[a[text()= 'Building Phases']]/preceding-sibling::th");
            if (ExpandBuildingPhase == null || ExpandBuildingPhase.IsDisplayed() == false)
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase Expand All button to click.</font>");

            ExpandBuildingPhase.Click(false);
            WaitingLoadingGifByXpath(loadingIconXpath);


            // Verify product
            IWebElement product = FindElementHelper.FindElement(FindType.XPath, buildingPhase_Xpath + product_Xpath);
            if (product == null || product.Displayed == false)
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name {expectedData.ProductName}.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with  name {expectedData.ProductName} displays correctly on the grid view.</b></font>");

            // Verify Quantities
            IWebElement quantities = FindElementHelper.FindElement(FindType.XPath, buildingPhase_Xpath + product_Xpath + quantity_Xpath);
            if (quantities == null || quantities.Displayed == false)
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product {expectedData.ProductName} with quantities {expectedData.Quantities}.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product {expectedData.ProductName} with quantities {expectedData.Quantities} " +
                    $"on building phase {expectedData.BuildingPhaseName} displays correctly on the grid view.</b></font>");


            // Delete current quantity
            Button deleteQuantity = new Button(FindType.XPath, buildingPhase_Xpath + product_Xpath + deleteQuantity_Xpath);
            if (deleteQuantity.IsDisplayed() is false)
                return;
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(deleteQuantity), "Delete current quantity.");
            deleteQuantity.Click(false);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(loadingIconXpath);

            string actualMessage = GetLastestToastMessage();
            string expectedToastMess = "Successfully deleted quantity.";
            if (expectedToastMess.Equals(actualMessage) is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>Delete job quantity successfully. The toast message is same as the expectation.</b></font>");
            else
                if (deleteQuantity.IsDisplayed() is true)
                ExtentReportsHelper.LogFail($"<font color='red'>The toast message is NOT same as the expectation." +
                    $"<br>Expected: {expectedToastMess}" +
                    $"<br>Actual: {actualMessage}</font>");
        }

        /// <summary>
        /// Process import file with Product not exist on system
        /// </summary>
        /// <param name="expectedData"></param>
        public void ProcessNonExistProductQuantities(JobImportQuantitiesData expectedData)
        {
            string nonProduct_lbl = "//*[contains(@id, 'ImportStatusInfo') and @class= 'alert-notify']";
            CommonHelper.WaitUntilElementVisible(5, nonProduct_lbl, false);

            Label nonExistProduct_lbl = new Label(FindType.XPath, nonProduct_lbl);
            if (nonExistProduct_lbl == null || nonExistProduct_lbl.IsDisplayed() == false)
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Should display a panel with title 'These Products do not currently exist in Pipeline.'</font>");

            // Turn on the checkbox on the end of new product row
            CheckBox createProduct_ckb = new CheckBox(FindType.XPath, $"//*[text() = '{expectedData.ProductName}']/following::input[contains(@id,'ckbCreate')]");
            createProduct_ckb.SetCheck(true, false);

            // Click continue button
            Button continue_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbProductsAdd']");
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(continue_btn), "<font color='lavender'><b>Click continue button.</b></font>");
            continue_btn.Click(false);

            // Wait loading icon
            //WaitingLoadingGifByXpath("//*[contains(@id, 'lp1ctl00_CPH_Content_rgConfigs')]/div[1]", 5);
            WaitingLoadingGifByXpath("//*[contains(@id, 'Content_pnlImportStatusInfo') and contains(@class, 'RadAjax_Default')]/div[1]", 5);

            // Verify the new product on the import grid view
            VerifyProductQuantitiesToImport(expectedData);
        }

        /// <summary>
        /// Expand Product Quantities In Import Grid
        /// </summary>
        /// <param name="expectedData"></param>
        /// <returns></returns>
        public bool ExpandProductQuantitiesAndVerifyProductQuantitiesToImport(JobImportQuantitiesData expectedData, bool IsCaptured = true)
        {
            bool result = true;
            // Expand Option
            Button ExpandOption_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgConfigs_ctl00']//span[contains(text(),'{expectedData.Option}')]//ancestor::tr/td[@class='rgExpandCol']");

            ExpandOption_btn.WaitForElementIsVisible(5);

            ExpandOption_btn.Click(IsCaptured);

            // Wait loading icon
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgConfigs']/div[1]");

            // Expand all building phase
            Button ExpandBuildingPhase = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgConfigs_ctl00']//tr//td/span[contains(text(),'{expectedData.BuildingPhaseCode + "-" + expectedData.BuildingPhaseName}')]/../preceding-sibling::td");
            Label BuildingPhase_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgConfigs_ctl00']//tr//td/span[contains(text(),'{expectedData.BuildingPhaseCode + "-" + expectedData.BuildingPhaseName}')]");

            if (BuildingPhase_lbl.IsDisplayed() is true)
            {
                if (BuildingPhase_lbl.GetText().Equals(expectedData.BuildingPhaseCode + "-" + expectedData.BuildingPhaseName) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Building phase with code {expectedData.BuildingPhaseCode} and name {expectedData.BuildingPhaseName} doesn't display on the Product Quantities grid view to import.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"<font color='green'>Building phase with code {expectedData.BuildingPhaseCode} and name {expectedData.BuildingPhaseName} is displayed on the Product Quantities grid view to import.</font>");
                }
                ExpandBuildingPhase.WaitForElementIsVisible(5);
                ExpandBuildingPhase.Click(IsCaptured);
                // Wait loading icon
                WaitingLoadingGifByXpath("//*[contains(@id, 'ctl00_CPH_Content_lp1ctl00_CPH_Content_rgConfigs')]/div[1]");
            }
            else
                result = false;

            Label product = new Label(FindType.XPath, $"//*[contains(@rel, 'Products/Details') and contains(text(), '{expectedData.ProductName}')]");

            if (product.IsDisplayed() is false || !expectedData.ProductName.Equals(product.GetText().Trim()))
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Product with name {expectedData.ProductName} doesn't display on the Product Quantities grid view to import.</font>");
                result = false;
            }

            Label Now = new Label(FindType.XPath, $"//*[contains(@rel, 'Products/Details') and contains(text(), '{expectedData.ProductName}')]/../following-sibling::td[contains(text(),'{expectedData.Now}')]");
            if(expectedData.Now != string.Empty)
            {
                if (Now.IsDisplayed() is false || !expectedData.Now.Equals(Now.GetText().Trim()))
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Now with Value: {expectedData.Now} doesn't display on the Now Quantities grid view to import.</font>");
                    result = false;
                }
            }

            Label Future = new Label(FindType.XPath, $"//*[contains(@rel, 'Products/Details') and contains(text(), '{expectedData.ProductName}')]/../following-sibling::td[contains(text(),'{expectedData.Future}')]");
            if (expectedData.Future != string.Empty)
            {
                if (Future.IsDisplayed() is false || !expectedData.Future.Equals(Future.GetText().Trim()))
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Future with Value: {expectedData.Future} doesn't display on the Future Quantities grid view to import.</font>");
                    result = false;
                }
            }
            if (result is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Building phase '{expectedData.BuildingPhaseName}' and Product '{expectedData.ProductName}' display correctly on the Product Quantities grid view to import.</b></font>");

            //Collapse Option
            ExpandOption_btn.RefreshWrappedControl();
            ExpandOption_btn.Click();

            // Wait loading icon
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgConfigs']/div[1]");
            return result;
        }

        /// <summary>
        /// Delete File Import Product Quantities
        /// </summary>
        public void DeleteSelectedFile()
        {
            DeleteSelectedFile_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_ddlFiles']/div[1]");
            string actualMsg = GetLastestToastMessage();
            string expectMsg = "File deleted successfully!";
            if (actualMsg.Equals(expectMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Delete file deleted successfully!.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The toast message is NOT same as the expectation.</font>" +
                                        $"<br>Expected: {expectMsg}" +
                                        $"<br>Actual: {actualMsg}");
            }
        }

        /// <summary>
        /// Upload Job Quantities From Error File 
        /// </summary>
        /// <param name="uploadFileName"></param>
        /// <param name="expectToastMess"></param>
        /// <param name="isOptionSpecified"></param>
        public void UploadJobQuantitiesAndProcessForErrorFile(string uploadFileName, string expectToastMess, bool isOptionSpecified = true)
        {
            string uploadFileLocation;
            if (isOptionSpecified is true)
                uploadFileLocation = "JobQuantities_OptionsSpecified\\" + uploadFileName;
            else
                uploadFileLocation = "JobQuantities_NoOptionsSpecified\\" + uploadFileName;
            ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>Select file: {uploadFileName} to upload.</font>");
            UploadJobQuantities($@"\DataInputFiles\Jobs\{uploadFileLocation}");

            Label textbox = new Label(FindType.XPath, $"//span[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlFilesPanel']//option[.='{uploadFileName}']");

            // Verify the upload file name on Job Quantities File text box
            if (textbox.WaitForElementIsInVisible(10, false) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Job Quantities file {uploadFileName} is uploaded successfully.</b></font>");

            // Verify the toast message
            string actualMessage = GetLastestToastMessage();
            string expectedToastMess = $"{expectToastMess}";
            if (expectedToastMess.Equals(actualMessage) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The toast error message is same as the expectation.</b></font>");
            else
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>The toast message is NOT same as the expectation.</b></font>" +
                                        $"<br>Expected: {expectedToastMess}" +
                                        $"<br>Actual: {actualMessage}");

            //Close Toase Message
            CloseToastMessage(3);
            CommonHelper.RefreshPage();
        }

    }
    }
