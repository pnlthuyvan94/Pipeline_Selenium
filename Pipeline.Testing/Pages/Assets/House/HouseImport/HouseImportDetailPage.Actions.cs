using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace Pipeline.Testing.Pages.Assets.House.Import
{
    public partial class HouseImportDetailPage
    {
        private const int DOWNLOAD_WAITING_TIME = 500;
        private const string PENDING_FILE_LOADING_IN_GRID = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPendingFiles']/div[1]";
        private const string FILE_LOADING_GRID = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgFiles']/div[1]";
        private const string UPLOAD_FILE_LOADING_IN_GRID = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbUpload']/div[1]";
        private const string GENERATE_REPORT_VIEW_FILE_LOADING_IN_GRID = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbGenerateReportView']/div[1]";
        private const string REPORT_VIEW_FILE_LOADING_IN_GRID = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlReportView']/div[1]";
        public void ClickUploadDocument(string sitemapFileName)
        {
            SelectFileTxt.SendKeysWithoutClear(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + sitemapFileName);
            System.Threading.Thread.Sleep(DOWNLOAD_WAITING_TIME);
            PageLoad();
        }
        public void UploadHouseQuantities()
        {
            UploadHouseQuantitiesBtn.Click();
            WaitingLoadingGifByXpath(UPLOAD_FILE_LOADING_IN_GRID);
        }

        public void GenerateReportView()
        {
            GenerateReportViewBtn.Click();
            WaitingLoadingGifByXpath(GENERATE_REPORT_VIEW_FILE_LOADING_IN_GRID);
        }

        public bool IsItemInGridHouseUpload(string columnName, string value)
        {
            return UploadHouseGrid.IsItemOnCurrentPage(columnName, value);
        }
        private void CheckAllFileUpload(bool isCheckFile)
        {
            FileCheckAllChk.SetCheck(isCheckFile);
        }
        public void ClickOnFinishImport()
        {
            FinishImportBtn.Click();
            WaitingLoadingGifByXpath(REPORT_VIEW_FILE_LOADING_IN_GRID);
        }

        public void ClickOnFinishReviewImport()
        {
            Button FinishImportBtn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblFinishImportReview']");
            FinishImportBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlImportEndProcess']/div[1]");
        }

        public void ClickOnImportFile()
        {
            ImportBtn.WaitForElementIsVisible(5);
            ImportBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlReportView']/div[1]");
        }

        public void OpenUploadFile()
        {
            UploadBtn.WaitForElementIsVisible(5);
            UploadBtn.Click();
        }
        public void UploadAllFileImport()
        {
            CheckAllFileUpload(true);
            UploadHouseQuantities();
            CloseToastMessage();
            CloseModal();
        }
        public void ScrollLeftToOffSetWidth()
        {
            CommonHelper.ScrollLeftToOffSetWidth("//*[@id='ctl00_CPH_Content_rgReportView_Frozen']");
        }

        public void FilterItemInGridHouseUpload(string columnName, string value)
        {
            Textbox FilenameTxt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00_ctl02_ctl02_FilterTextBox_FileName']");
            FilenameTxt.SetText(value);
            Button OpenFilterBtn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00_ctl02_ctl02_Filter_FileName']");
            Button FilterTypeBtn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPendingFiles_rfltMenu_detached']" +
                $"//a/span[contains(text(),'{columnName}')]");
            OpenFilterBtn.Click();
            FilterTypeBtn.Click();
            WaitingLoadingGifByXpath(PENDING_FILE_LOADING_IN_GRID);
        }

        public void DeleteItemInGridHouseUpload(string columnName, string value)
        {
            UploadHouseGrid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(PENDING_FILE_LOADING_IN_GRID);
            string expected = "Successfully deleted file.";
            string actual = GetLastestToastMessage();
            if (actual.Equals(expected))
            {
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>File deleted successfully!</b></font>");
                return;
            }
            if (IsItemInGridHouseUpload("FileName", value))
                ExtentReportsHelper.LogWarning("The File could not be deleted - Possible constraint preventing deletion.");
            else
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>File deleted successfully!</b></font>");
        }

        public void ChangeSpecificCommunityInHouseUpload(string Community, string item)
        {
            if (Community != string.Empty)
            {
                DropdownList communityDdl = new DropdownList(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00']" +
                    $"//tr//td[contains(text(),'{item}')]//ancestor::tr//select");
                communityDdl.SelectItem(Community);
                WaitingLoadingGifByXpath(PENDING_FILE_LOADING_IN_GRID);
                ExtentReportsHelper.LogInformation($"Select {Community} on grid.");
            }
        }

        public void CheckSpecificFileInHouseUpload(string item, bool isChecked)
        {
            CheckBox fileNameChk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00']" +
                $"//td[contains(text(),'{item}')]/preceding-sibling::td/input[@type='checkbox']");
            fileNameChk.SetCheck(isChecked);
        }

        public void DeleteFileImportQuantities(string columnName, string value)
        {
            UploadHouseMaterialFilesGrid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(FILE_LOADING_GRID);
        }
        public void CloseModal()
        {
            UploadHouseMaterialFilesCloseBtn.JavaScriptClick();
            System.Threading.Thread.Sleep(DOWNLOAD_WAITING_TIME);
        }
        public bool IsItemInGridHouseMaterialFiles(string columnName, string value)
        {
            return UploadHouseMaterialFilesGrid.IsItemOnCurrentPage(columnName, value);
        }
        public void FilterItemInGridHouseMaterialFiles(string columnName, GridFilterOperator GridFilterOperator, string value)
        {
            UploadHouseMaterialFilesGrid.FilterByColumn(columnName, GridFilterOperator, value);
            WaitingLoadingGifByXpath(FILE_LOADING_GRID);
        }

        private void ClickOnUpload()
        {
            UploadBtn.WaitForElementIsVisible(5);
            UploadBtn.Click();
        }
        private void CheckFileNameIsUpload(string item)
        {
            if (IsItemInGridHouseUpload("FileName", item) is false)
            {
                ExtentReportsHelper.LogFail($"HouseQuantities <font color='red'><b>{item}</b></font> file is uploaded unsuccessfully.");
            }
            ExtentReportsHelper.LogPass($"HouseQuantities <font color='green'><b>{item}</b></font> file is uploaded successfully.");
        }

        private void CheckFileNameIsExistInGrid(string community, string option, string fileName)
        {
            string name = community.Substring(community.IndexOf("-") + 1);
            if (community != string.Empty)
            {
                if (community != string.Empty)
                {
                    if (IsItemInGridHouseMaterialFiles("Option(s)", option) && IsItemInGridHouseMaterialFiles("Community", name) is false)
                    {
                        ExtentReportsHelper.LogFail($"House Material Files <font color='red'><b>{fileName}</b></font> file is displayed in grid unsuccessfully.");
                    }
                    ExtentReportsHelper.LogPass($"House Material Files <font color='green'><b>{fileName}</b></font> file is displayed in grid successfully.");
                }
                else
                {
                    if (IsItemInGridHouseMaterialFiles("Option(s)", option) && IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is false)
                    {
                        ExtentReportsHelper.LogFail($"House Material Files <font color='red'><b>{fileName}</b></font> file is displayed in grid unsuccessfully.");
                    }
                    ExtentReportsHelper.LogPass($"House Material Files <font color='green'><b>{fileName}</b></font> file is displayed in grid successfully.");
                }
            }
        }

        private void CheckFileNameIsImportSuccess(string filename)
        {
            if (ImportCompletedTxt.IsDisplayed() is false || ImportCompletedTxt.GetText().Equals("Import Completed") is false)
            {
                ExtentReportsHelper.LogFail($"Import {filename} File is  unsuccessfully.");
            }
            ExtentReportsHelper.LogPass($"Import {filename} File is successfully.");
        }

        /// <summary>
        /// UploadFile And Import Quantities File To House Quantites
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="Community"></param>
        /// <param name="NameCommunity"></param>
        /// <param name="uploadFileName"></param>
        public void UploadFileAndImportHouseQuantities(string exportType, string community, string option, params string[] uploadFileName)
        {
            string name = community.Substring(community.IndexOf("-") + 1);
            if ((!IsItemInGridHouseMaterialFiles("Option(s)", option) && (!IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") || !IsItemInGridHouseMaterialFiles("Community", name))))
            {
                ExtentReportsHelper.LogInformation($"The file with Community: {name} was Imported To HouseQuantities.");
            }
            ClickOnUpload();
            if (ExportTypeDdl.IsDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Export File Modal is not display.</font>");
            }
            ExportTypeDdl.SelectItem(exportType);
            foreach (string item in uploadFileName)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>Select file: {item} to upload.</font>");
                ClickUploadDocument($@"\DataInputFiles\Assets\House\{item}");
                WaitingLoadingGifByXpath(FILE_LOADING_GRID);
                ChangeSpecificCommunityInHouseUpload(community, item);
                CheckFileNameIsUpload(item);
                CheckAllFileUpload(true);
                UploadHouseQuantities();
                CloseModal();
                CheckFileNameIsExistInGrid(community, option, item);
                CheckALLFileImport();
                GenerateReportView();
                ClickOnImportFile();
                CheckFileNameIsImportSuccess(item);
                ClickOnFinishImport();
            }
        }

        /// <summary>
        /// UploadFile And Import Quantities File To House Quantites
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="Community"></param>
        /// <param name="NameCommunity"></param>
        /// <param name="uploadFileName"></param>
        public void ImportHouseQuantities(string ExportType, string community, string option, params string[] uploadFileName)
        {
            string name = community.Substring(community.IndexOf("-") + 1);
            if ((IsItemInGridHouseMaterialFiles("Option(s)", option) && (IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") || IsItemInGridHouseMaterialFiles("Community", name))) is true)
            {
                ExtentReportsHelper.LogInformation($"The file with Community: {name} was Imported To HouseQuantities.");
            }
            ClickOnUpload();
            if (ExportTypeDdl.IsDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Export File Modal is not display.</font>");
            }
            ExportTypeDdl.SelectItem(ExportType);
            foreach (string item in uploadFileName)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>Select file: {item} to upload.</font>");
                ClickUploadDocument($@"\DataInputFiles\Assets\House\{item}");
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPendingFiles']/div[1]");
                ChangeSpecificCommunityInHouseUpload(community, item);
                CheckFileNameIsUpload(item);
            }
        }

        /// <summary>
        /// UploadFile And Import Quantities File To House Quantites
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="Community"></param>
        /// <param name="NameCommunity"></param>
        /// <param name="uploadFileName"></param>
        public void ImportHouseQuantitiesAndGenerationReportView(string ExportType, string community, string option, params string[] uploadFileName)
        {
            string name = community.Substring(community.IndexOf("-") + 1);
            ImportHouseQuantities(ExportType, community, option, uploadFileName);
            CheckAllFileUpload(true);
            UploadHouseQuantities();
            CloseToastMessage();
            CloseModal();
            foreach (string fileName in uploadFileName)
            {
                CheckFileNameIsExistInGrid(community, option, fileName);
            }
            CheckALLFileImport();
            GenerateReportView();
        }

        public bool IsItemInImportQuantitiesGrid(string columnName, string value)
        {
            return ImportQuantitiesGrid.IsItemOnCurrentPage(columnName, value);
        }

        public void ImportFileIntoHouseQuantitiesAfterUploadFileInGrid(params string[] uploadFileName)
        {
            foreach (string item in uploadFileName)
            {
                ClickOnImportFile();
                CheckFileNameIsImportSuccess(item);
                ClickOnFinishImport();
            }
        }
        public void ImportFileWithBuildingPhaseIsNotAssigned()
        {
            ClickOnImportFile();
            VerifyProductDoesNotHaveNotAssignedBuldingPhaseMessage();
            ClickOnFinishImport();
        }
        public void ClickOnContinueImport()
        {
            Button continueImportBtn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbReviewFinalizeImport']");
            continueImportBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlReportView']/div[1]");
        }
        /// <summary>
        /// Import House Quantities With No Product In System
        /// </summary>
        /// <param name="HouseImportQuantitiesData"></param>
        public void ImportHouseQuantitiesWithNoProduct(HouseImportQuantitiesData HouseImportQuantitiesData)
        {
            ClickOnImportFile();
            ValidateProducts(HouseImportQuantitiesData);
            //After Import Product is not exited in system
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Building phase Assignment Not Found: The Description get from the product detail.</font>");
            if (BuildingPhaseNotFoundLbl.IsDisplayed() && BuildingPhaseNotFoundLbl.GetText().Equals("Building Phase Assignment Not Found"))
            {
                if (CheckAllBuildingPhaseChk.IsDisplayed() is true)
                {
                    ExtentReportsHelper.LogInformation($"Building Phase is not exited in system");
                    CheckAllBuildingPhaseChk.Check(true);
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddProductToBuildingPhase']/div[1]");
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Building Phase is exited in system");
                }
                foreach (string item in HouseImportQuantitiesData.Description)
                {
                    if (BuildingPhaseNotFoundGrid.IsItemOnCurrentPage("Description", item) is true)
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'><b>The Description column with name {item} is displayed in Building Phase Assignment Not Found</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogInformation($"<font color='yellow'>The Description column with name {item} is displayed in Building Phase Assignment Not Found</font>");
                    }
                }
                foreach (string item in HouseImportQuantitiesData.BuildingPhases)
                {
                    if (BuildingPhaseNotFoundGrid.IsItemOnCurrentPage("Building Phase", item) is true)
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'><b>The Building Phase column with {item} is displayed in Building Phase Assignment Not Found</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogInformation($"<font color='yellow'>The Building Phase column with {item} is displayed in Building Phase Assignment Not Found</font>");
                    }
                }
                foreach (string item in HouseImportQuantitiesData.Products)
                {
                    if (BuildingPhaseNotFoundGrid.IsItemOnCurrentPage("Product Name", item) is true)
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'><b>The Product Name column with {item} is displayed in Building Phase Assignment Not Found</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogInformation($"<font color='yellow'>The Product Name column with {item} is displayed in Building Phase Assignment Not Found</font>");
                    }
                }
            }

            ContinueProductToBuildingPhaseBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddProductToBuildingPhase']/div[1]");


            if (ImportCompletedTxt.IsDisplayed() && ImportCompletedTxt.GetText().Equals("Import Completed"))
            {
                ExtentReportsHelper.LogPass($"Import File No Product In System is successfully.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Import File No Product In System is unsuccessfully.");
            }
            ClickOnContinueImport();
            ClickOnFinishReviewImport();
        }

        public void ValidateProducts(HouseImportQuantitiesData HouseImportQuantitiesData)
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Validate Products: New product so it will get the description field form the import file.</font>");
            if (ValidateProductLbl.IsDisplayed() && ValidateProductLbl.GetText().Equals("Validate Products"))
            {
                if (CheckAllProductChk.IsDisplayed() is true)
                {
                    ExtentReportsHelper.LogInformation($"Product is not exited in system");
                    CheckAllProductChk.Check(true);
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Product is exited in system");
                }

                foreach (string item in HouseImportQuantitiesData.Products)
                {
                    if (ValidateProductsGrid.IsItemOnCurrentPage("Product Name", item))
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'><b>The New Product Name column with {item} is displayed in Validate Products.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogInformation($"<font color='yellow'>The New Product Name column with {item} is displayed in Validate Products</font>");
                    }
                }

                ContinueProductsImportBtn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbContinueProducts']/div[1]");
                string actual_result = GetLastestToastMessage();
                string expect_result = "Successfully created new product(s).";
                if (actual_result.Equals(expect_result))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Create New Products is successfully sush as Toast Message</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'><b>Create New Products is not successfully sush as Toast Message</b></font>");
                }
            }
        }
        public void ValidateProductStyles(HouseImportQuantitiesData HouseImportQuantitiesData)
        {
            if (ValidateProductStylesLbl.IsDisplayed() && ValidateProductStylesLbl.GetText().Equals("Validate Product Styles"))
            {
                if (CheckAllCreateChk.IsDisplayed() is false)
                {
                    ExtentReportsHelper.LogInformation($"Product Style is exited in system");

                }
                ExtentReportsHelper.LogInformation($"Product Style is not exited in system");
                CheckAllCreateChk.Check(true);

                foreach (string item in HouseImportQuantitiesData.Manufacturers)
                {
                    if (ValidateProductStylesGrid.IsItemOnCurrentPage("Manufacturer", item) is true)
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'><b>The New Manufacturer Name column with {item} is displayed in Validate Product Styles.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogInformation($"<font color='yellow'>The New Manufacturer Name column with {item} is displayed in Validate Product Styles</font>");
                    }
                }

                foreach (string item in HouseImportQuantitiesData.Styles)
                {
                    if (ValidateProductStylesGrid.IsItemOnCurrentPage("Style", item) is true)
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'><b>The New Style Name column with {item} is displayed in Validate Product Styles.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogInformation($"<font color='yellow'>The New Style Name column with {item} is displayed in Validate Product Styles</font>");
                    }
                }

                ContinueStylesImportBtn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddStyles']/div[1]");

                string actual_result = GetLastestToastMessage();
                string expect_result = "Successfully created new style(s).";
                if (actual_result.Equals(expect_result))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Create New Style is successfully sush as Toast Message</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'><b>Create New Style is not successfully sush as Toast Message</b></font>");
                }

                ClickOnFinishImport();
            }
        }
        /// <summary>
        /// Import House Quantities With No Product In System
        /// </summary>
        /// <param name="HouseImportQuantitiesData"></param>
        public void ImportHouseQuantitiesWithNoManufacturerOrStyle(HouseImportQuantitiesData HouseImportQuantitiesData)
        {
            ClickOnImportFile();
            ValidateProductStyles(HouseImportQuantitiesData);
        }


        public void DeleteAllHouseMaterialFiles()
        {
            CheckBox CheckAll = new CheckBox(FindType.XPath, "//h1[contains(.,'House Material Files')]/parent::header/following-sibling::div[3]//thead//th[1]//input");
            CheckAll.Check();
            Button BulkAc = new Button(FindType.XPath, "//h1[contains(.,'House Material Files')]/preceding::div[1]//a[@id = 'bulk-actions']");
            BulkAc.Click();
            Button DeleteSelectedFiles = new Button(FindType.XPath, "//h1[contains(.,'House Material Files')]/preceding-sibling::div//li/a");
            DeleteSelectedFiles.Click();
            bool alertAppear = CommonHelper.WaitUntilAlertAppears(driver, TimeSpan.FromSeconds(10));
            if (alertAppear)
            {
                driver.SwitchTo().Alert().Accept();
            }
            else
            {
                return;
            }
        }

        public void DeleteAllOptionQuantities()
        {
            CheckBox checkAllChk = new CheckBox(FindType.XPath, "//h1[contains(.,'Quantities')]/ancestor::article[3]/div[6]//thead/tr[2]/th[1]/input");
            checkAllChk.Check();
            Button BulkAcBtn = new Button(FindType.XPath, "//h1[contains(.,'Quantities')]/preceding::div[1]//a[@id = 'bulk-actions']");
            BulkAcBtn.Click();
            Button DeleteSelectedBtn = new Button(FindType.XPath, "//h1[contains(.,'Quantities')]/preceding-sibling::div/ul/li[1]");
            DeleteSelectedBtn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(FILE_LOADING_GRID);
        }

        public void OpenGenerateComparison()
        {
            GenerateComparisonBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbGenerateComparisons']/div[1]");
        }
        private void CheckAndAddBaseOptionInComparison(string option)
        {
            if (AddBaseOptionBtn.IsDisplayed() is true)
            {
                Un_ModifiedOptionsChk.Check();
                AddBaseOptionBtn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUnmodifiedOptions']/div[1]");

                //Find option in Comparison Groups
                if (ComparisonGroupsGrid.IsItemWithTextContainsOnCurrentPage("Option", option) is false)
                    ExtentReportsHelper.LogFail($"<font color='red'>Option with Name {option} is not displayed in Comparison Groups</font>");
            }
        }
        private void CheckItemInImportQuantitiesGrid(HouseImportQuantitiesData HouseImportQuantitiesData)
        {
            //Check Data In Grid
            Label OptionLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgReportView_ctl00']//tbody/tr/td[contains(text(),'{HouseImportQuantitiesData.OptionName}')]");

            if (OptionLbl.IsDisplayed() is true && OptionLbl.GetText().Equals(HouseImportQuantitiesData.OptionName) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Option Name column with {HouseImportQuantitiesData.OptionName} is displayed in Import Quantities</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogInformation($"<font color='yellow'>The Option Name column with {HouseImportQuantitiesData.OptionName} is displayed in Import Quantities</font>");
            }

            foreach (string itemData in HouseImportQuantitiesData.BuildingPhases)
            {
                Label BuildingPhaseLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgReportView_ctl00']//tbody/tr/td/a[contains(text(),'{itemData}')]");
                if (BuildingPhaseLbl.IsDisplayed() && BuildingPhaseLbl.GetText().Equals(itemData) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Building Phase column with {itemData} is displayed in Import Quantities</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"<font color='yellow'>The Building Phase column with {itemData} is displayed in Import Quantities</font>");
                }
            }

            foreach (string itemData in HouseImportQuantitiesData.Products)
            {
                Label ProductLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgReportView_ctl00']//tbody/tr/td/a[contains(text(),'{itemData}')]");
                if (ProductLbl.IsDisplayed() is true && ProductLbl.GetText().Equals(itemData))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Product Name column with {itemData} is displayed in Import Quantities</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"<font color='yellow'>The Product Name column with {itemData} is displayed in Import Quantities</font>");
                }
            }
        }
        private void VerifyProductDoesNotHaveNotAssignedBuldingPhaseMessage()
        {
            if (StatusLbl.IsDisplayed() is false || StatusLbl.GetText().Contains("this Product does not have the following Building Phase assigned.") is false)
            {
                ExtentReportsHelper.LogInformation($"<font color='yellow'>Status is not display as expected</font>");
            }
            ExtentReportsHelper.LogPass($"<font color='green'><b>Status is displayed as expected</b></font>");
        }

        /// <summary>
        /// UploadFile And Import Quantities File To House Quantites
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="Community"></param>
        /// <param name="NameCommunity"></param>
        /// <param name="uploadFileName"></param>
        public void UploadFileAndComparisonSetup(string ExportType, HouseImportQuantitiesData HouseImportQuantitiesData, bool isCheckFile, params string[] uploadFileName)
        {
            string name = HouseImportQuantitiesData.Community.Substring(HouseImportQuantitiesData.Community.IndexOf("-") + 1);
            if ((!IsItemInGridHouseMaterialFiles("Option(s)", HouseImportQuantitiesData.OptionName) && (!IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") || !IsItemInGridHouseMaterialFiles("Community", name))))
            {
                ExtentReportsHelper.LogInformation($"The file with Community: {name} was Imported To HouseQuantities.");
            }
            ClickOnUpload();
            if (ExportTypeDdl.IsDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Export File Modal is not display.</font>");
            }
            ExportTypeDdl.SelectItem(ExportType);
            foreach (string item in uploadFileName)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>Select file: {item} to upload.</font>");
                ClickUploadDocument($@"\DataInputFiles\Assets\House\{item}");
                WaitingLoadingGifByXpath(FILE_LOADING_GRID);
                ChangeSpecificCommunityInHouseUpload(HouseImportQuantitiesData.Community, item);
                CheckFileNameIsUpload(item);
                CheckAllFileUpload(isCheckFile);
                UploadHouseQuantities();
                CloseToastMessage();
                CloseModal();
                CheckAllFileUpload(isCheckFile);
                OpenStartComparisonSetup();
                //If Option is assigned then we don't need to add.
                CheckAndAddBaseOptionInComparison(HouseImportQuantitiesData.OptionName);
                OpenGenerateComparison();
                CheckItemInImportQuantitiesGrid(HouseImportQuantitiesData);
                ImportFileWithBuildingPhaseIsNotAssigned();
            }
        }

        /// <summary>
        /// UploadFile And Import Quantities File To House Quantites
        /// </summary>
        /// <param name="ExportType"></param>
        /// <param name="Community"></param>
        /// <param name="NameCommunity"></param>
        /// <param name="uploadFileName"></param>
        public void ImportHouseQuantitiesWithFiles(string ExportType, string Community, string Option, string source, params string[] uploadFileName)
        {
            if (ExportTypeDdl.IsDisplayed())
            {
                ExportTypeDdl.SelectItem(ExportType);
                foreach (string item in uploadFileName)
                {
                    ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>Select file: {item} to upload.</font>");
                    ClickUploadDocument($@"\DataInputFiles\Assets\House\{item}");
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPendingFiles']/div[1]");
                    // Verify file is uploaded successfully
                    FilterItemInGridHouseUpload("Contains", item);
                    ChangeSpecificCommunityInHouseUpload(Community, item);
                    Label OptionLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00']//td[contains(text(),'{item}')]" +
                        $"/following-sibling::td[contains(text(),'{Option}')]");
                    Label CommunityLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00']//td[contains(text(),'{item}')]" +
                        $"/following-sibling::td/select/option[@selected]");
                    Label SourceLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00']//td[contains(text(),'{item}')]" +
                        $"/following-sibling::td[contains(text(),'{source}')]");
                    if (IsItemInGridHouseUpload("FileName", item) is true)
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'>HouseQuantities <font color='green'><b>{item}</b></font> file is uploaded successfully.</font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>HouseQuantities <font color='red'><b>{item}</b></font> file is uploaded unsuccessfully.</font>");
                    }

                    if (OptionLbl.GetText().Equals(Option))
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'>Import File With Option {Option} is displayed correctly</font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Import File With Option {Option} is display incorrectly</font>");
                    }

                    if (CommunityLbl.GetText().Equals("Default House Quantities"))
                    {
                        ExtentReportsHelper.LogInformation($"<font color='green'>Import File With Defaulut House Quantities is displayed correctly</font>");
                    }
                    else if (CommunityLbl.GetText().Equals(Community))
                    {
                        ExtentReportsHelper.LogInformation($"<font color='green'>Import File With Specific Community {CommunityLbl.GetText()} is displayed correctly</font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail("<font color='red'>Import File is displayed incorrectly as expected</font>");
                    }

                    if (SourceLbl.GetText().Equals(source))
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'>Import File With Souce: {source} is displayed correctly</font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Import File With Souce: {source} is display incorrectly</font>");
                    }
                }
            }
        }

        public void OpenStartComparisonSetup()
        {
            StartComparisonSetupBtn.Click();
            StartComparisonBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbStartComparison']/div[1]");
        }

        public void ViewComparison()
        {
            StartComparisonSetupBtn.Click();
            ViewComparisonBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbViewComparison']/div[1]");
            PageLoad();
            CommonHelper.SwitchLastestTab();
        }

        public void AddComparisonGroups()
        {
            AddComparisonGroupsBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
        }

        public void ExpandComparisonGroups(string Option)
        {
            Button ExpandOption_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]//ancestor::tr/td[@class='rtlL rtlL2']/input[@class='rtlExpand']");
            ExpandOption_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
        }

        public void CheckALLFileImport()
        {
            CheckAllChk.SetCheck(true);
        }

        public void VerifyComparisonGroups(List<string> ListOption)
        {
            if (NoImportComparionLbl.IsDisplayed() && NoImportComparionLbl.GetText().Equals("No Import Comparison Groups to display.") is true)
            {

                ExtentReportsHelper.LogPass($"<font color='green'><b>Comparison Groups is displayed With No Import Comparison</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning($"<font color='yellow'>Comparison Groups is not display With No Import Comparison</font>");
            }

            //If Option is assigned then we don't need to add.

            foreach (string OptionName in ListOption)
            {
                Label OptionLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgUnmodifiedOptions_ctl00']//td[contains(text(),'{OptionName}')]");
                if (OptionName != string.Empty && OptionLbl.IsDisplayed() is true && OptionLbl.GetText().Equals(OptionName) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Option Name column with {OptionName} is displayed in Un-Modifed Options</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogWarning($"<font color='yellow'>The Option Name column with {OptionName} is displayed in Un-Modifed Options</font>");
                }
            }
            Un_ModifiedOptionsChk.Check();
            AddBaseOptionBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUnmodifiedOptions']/div[1]");

            //Check Option In Comparison Groups Grid
            foreach (string Option in ListOption)
            {
                Label OptionLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]");
                if (Option != string.Empty && OptionLbl.IsDisplayed() is true && OptionLbl.GetText().Equals(Option) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Option Name column with {Option} is displayed in Comparison Groups</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The Option Name column with {Option} is displayed in Comparison Groups</font>");
                }
            }
        }

        public void StartComparion()
        {
            CheckAllFileUpload(false);
            UploadHouseQuantities();
            CloseToastMessage();
            CloseModal();
            CheckALLFileImport();
            OpenStartComparisonSetup();

        }
        public void OpenInsertItemComparison(string Option)
        {
            Button openOptionBtn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]//ancestor::td/following-sibling::td/input[contains(@id,'InsertCommandColumn')]");
            openOptionBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
        }


        public void InsertItemComparsion(string Option, string Condition, string InlineConditon, params string[] ListIncludedOption)
        {
            OptionDdl.SelectItem(Option);
            ConditionBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");

            InlineOptionDdl.SelectItem(Option);
            if (Condition != string.Empty && InlineConditon != string.Empty)
            {
                InlineConditonDdl.SelectItem(Condition);
                InlineConditonTxt.SetText(InlineConditon);
            }
            foreach (string IncludedOption in ListIncludedOption)
            {
                CheckBox IncludedOptionChk = new CheckBox(FindType.XPath, $"//*[contains(@id,'IncludedOption')]//ul/li//span[contains(text(),'{IncludedOption}')]/preceding-sibling::input");
                IncludedOptionChk.Check();
            }
            InsertBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
            string actual_result = GetLastestToastMessage();
            string expect_result = "Comparison Group created successfully!";
            if (actual_result.Equals(expect_result) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>Create Comparison Group Message is not display as expect with Otion: {Option} </b></font>");
            }
            ExtentReportsHelper.LogPass($"<font color='green'><b>Create Comparison Group Message is displayed as expect with Otion: {Option} </b></font>");
        }


        public void UpdateItemComparsion(string Option, string Condition, string InlineConditon, params string[] ListIncludedOption)
        {
            Button EditOptionBtn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]//ancestor::td/following-sibling::td/input[contains(@id,'EditButton')]");
            EditOptionBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
            OptionDdl.SelectItem(Option);
            ConditionBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
            InlineOptionDdl.SelectItem(Option);
            InlineConditonDdl.SelectItem(Condition);
            InlineConditonTxt.SetText(InlineConditon);
            foreach (string IncludedOption in ListIncludedOption)
            {
                CheckBox IncludedOptionChk = new CheckBox(FindType.XPath, $"//*[contains(@id,'IncludedOption')]//ul/li//span[contains(text(),'{IncludedOption}')]/preceding-sibling::input ");
                IncludedOptionChk.Check();
            }
            UpdateBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
            string actual_result = GetLastestToastMessage();
            string expect_result = "Comparison Group saved successfully!";
            if (actual_result.Equals(expect_result) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>Edit Comparison Group Message is not display as expect with Otion: {Option} </b></font>");
            }
            ExtentReportsHelper.LogPass($"<font color='green'><b>Edit Comparison Group Message is displayed as expect with Otion:{Option} </b></font>");
        }

        public void CopyItemComparsionGroups(string Option)
        {
            Button CopyGroupBtn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']" +
                $"//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]//ancestor::td/following-sibling::td/input[@title='Copy Group']");
            CopyGroupBtn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
            string actual_result = GetLastestToastMessage();
            string expect_result = "Successfully copied Import Comparison Group";
            if (actual_result.Equals(expect_result) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>Copy Comparison Group Message is not display as expect</b></font>");
            }
            ExtentReportsHelper.LogPass($"<font color='green'><b>Copy Comparison Group Message is displayed as expect</b></font>");
        }

        public void DeleteItemComparsion(string Option)
        {
            Button DeleteComparisonGroupBtn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']" +
                $"//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]//ancestor::td/following-sibling::td/input[@title='Delete Import Comparison Group']");
            DeleteComparisonGroupBtn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
            string actual_result = GetLastestToastMessage();
            string expect_result = "Comparison Group deleted successfully!";
            if (actual_result.Equals(expect_result) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>Delete Comparison Group Message is not display as expect With {Option}</b></font>");
            }
            ExtentReportsHelper.LogPass($"<font color='green'><b>Delete {Option} Comparison Group Message is displayed as expect With {Option}</b></font>");
        }
    }
}
