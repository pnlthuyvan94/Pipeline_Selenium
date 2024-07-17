using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Jobs.Job.JobDetail
{
    public partial class JobDetailPage
    {
        public JobDetailPage EnterJobName(string name)
        {
            Name_txt.SetText(name);
            System.Threading.Thread.Sleep(500);
            return this;
        }

        public string SelectComunity(string commuName)
        {
            if (Community_ddl.IsItemInList(commuName))
            {
                Community_ddl.SelectItem(commuName, true);
                WaitingLoadingGifByXpath(_loadingHouseXpath);
                return commuName;
            }
            else
            {
                Community_ddl.SelectItem(2);
                WaitingLoadingGifByXpath(_loadingHouseXpath);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Community_ddl), $"Select Community {Community_ddl.SelectedItemName}");
                return Community_ddl.SelectedItemName;
            }
        }

        public string SelectHouse(string houseName)
        {
            return House_ddl.SelectItemByValueOrIndex(houseName, 0);
        }

        public string SelectLot(string lot)
        {
            if (Lot_ddl.IsItemInList(lot))
            {
                Lot_ddl.SelectItem(lot, true);
                WaitingLoadingGifByXpath(_loadingLotAddressXpath);
                return lot;
            }
            else
            {
                

                AddLot_Btn.Click();
                System.Threading.Thread.Sleep(5000);
                NewLot_Text.SetText(lot);
                InsertLot_Btn.Click();
                System.Threading.Thread.Sleep(5000);
                //Lot_ddl.SelectItem(1);
                WaitingLoadingGifByXpath(_loadingLotAddressXpath);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Lot_ddl), $"Select Lot { Lot_ddl.SelectedItemName}");
                return Lot_ddl.SelectedItemName;
            }
        }

        public string SelectDrafting(string drafting)
        {
            return Drafting_ddl.SelectItemByValueOrIndex(drafting,0);
        }

        public string SelectOrientation(string orientation)
        {
            return Orientation_ddl.SelectItemByValueOrIndex(orientation,1);
        }

        public void SelectCustomerItem(string customer)
        {
            //*[@id="ctl00_CPH_Content_CustomersDialogModal_myCustomerGrid_rgCustomers_ctl00"]/tbody/tr/td[text()='Alexis']
            string xpathItem = $"//*[@id='ctl00_CPH_Content_CustomersDialogModal_myCustomerGrid_rgCustomers_ctl00']/tbody/tr/td[text()='{customer}']";
            Button customerItems = new Button(FindType.XPath, xpathItem);
            customerItems.Click();
        }

        public string SelectAssignCustomer(string customer)
        {
            AssignCustomer_btn.Click();
            // Filter customer and select it on Grid
            ExtentReportsHelper.LogInformation("Filter customer and select it on Grid");
            AssignCustomer_Grid.FilterByColumn("Email", GridFilterOperator.Contains, customer);
            AssignCustomer_Grid.WaitGridLoad();
            SelectCustomerItem(customer);
            // Click Save after select customer
            ExtentReportsHelper.LogInformation("Click Save after select customer");
            AssignCustomerSave_btn.Click();
            AssignCustomer_Grid.WaitGridLoad();
            // Waiting page load after added customer
            PageLoad();
            return customer;
        }
        public JobData CreateAJob(JobData job)
        {
            EnterJobName(job.Name);
            job.Community = SelectComunity(job.Community);
            job.House = SelectHouse(job.House);
            job.Lot = SelectLot(job.Lot);
            job.Orientation = SelectOrientation(job.Orientation);
            //job.Customer = SelectAssignCustomer(job.Customer);
            Save();
            JobData newjob = new JobData(job)
            {
                Community = job.Community,
                House = job.House,
                Lot = job.Lot,
                Orientation = job.Orientation,
                //Customer = job.Customer
            };
            return newjob;
        }

        public JobDetailPage ViewBomPageBy(string viewBy)
        {
            ViewByOnJobBOM_ddl.SelectItem(viewBy, true, false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlRpt']/div[1]");
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ViewByOnJobBOM_ddl), $"View By {viewBy}.");
            return this;
        }

        public void SyncSerberisWithPhaseName(string phaseName)
        {
            string _xpath = $"//tbody/tr/td[ ./following-sibling::td[contains(text(),'{phaseName}')] ]/input[@type='checkbox']";
            CheckBox item = new CheckBox(FindType.XPath, _xpath);
            item.Check();
            SyncSaberis_btn.Click();
        }

        public void SyncSerberisWithOptionName(string optionName)
        {
            string _xpath = $"//tbody/tr/td[ ./following-sibling::td[contains(text(),'{optionName}')] ]/input[@type='checkbox']";
            CheckBox item = new CheckBox(FindType.XPath, _xpath);
            item.Check();
            SyncSaberis_btn.Click();
        }

        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]");
            PageLoad();
        }

        public void DeleteAllQuantities()
        {
            DeleteQuantities_Btn.Click();
            DeleteOnModal_Btn.WaitForElementIsVisible(5);
            CheckAll_Chk.Check();
            DeleteOnModal_Btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionConfigurationQuantities']/div[1]");
            string actualMessage = GetLastestToastMessage();
            if (!string.IsNullOrEmpty(actualMessage))
            {
                if (actualMessage.Equals("Successfully deleted the quantities."))
                    ExtentReportsHelper.LogPass(actualMessage);
                else
                    ExtentReportsHelper.LogInformation(actualMessage);
                CloseToastMessage();
            }
            CloseModal_Btn.Click();
            DeleteOnModal_Btn.WaitForElementIsInVisible(3, false);
        }

        public void ImportJobQuantities(string sourceFile, bool optionSpecified = true)
        {
            if (optionSpecified)
            {
                if (!OptionSpecified_Chk.IsChecked)
                    OptionSpecified_Chk.JavaScriptClick();
            }
            else
            {
                if (!NoOptionSpecified_Chk.IsChecked)
                    NoOptionSpecified_Chk.JavaScriptClick();
            }
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_ddlFiles']/div[1]");
            ImportFile_Txt.SendKeysWithoutClear(sourceFile);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_ddlFiles']/div[1]");
        }

        public JobDetailPage DeleteAllOldFile()
        {
            if (DeleteSelectedImportFile_Btn.IsExisted(false))
            {
                DeleteSelectedImportFile_Btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_ddlFiles']/div[1]");
                var msg = GetLastestToastMessage(2);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (msg.Equals("File deleted successfully!"))
                        ExtentReportsHelper.LogPass(msg);
                    CloseToastMessage(3, false);
                }
                DeleteSelectedImportFile_Btn.RefreshWrappedControl();
                DeleteAllOldFile();
            }
            return this;
        }

        public JobDetailPage ProcessedImportFile()
        {
            ProcessedImportFile_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_lbProcessFileAndConfiguration']/div[1]");
            return this;
        }

        public void FinishImportFile()
        {
            FinishImportFile_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_lbFinishJobImportProcess']/div[1]");
        }

        public void GenerateBOM()
        {
            GenerateBomAndEstimate_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlRpt']/div[1]");
            PageLoad();
            ExtentReportsHelper.LogPass("Generate BOM Successfully");
        }

        public void GenerateBomAndEstimate()
        {
            GenerateBomAndEstimate_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhasesView']/div[1]");
            PageLoad();
            ExtentReportsHelper.LogPass("Generate BOM And Estimate Successfully");
        }

        public JobDetailPage ExpandCreatePO()
        {
            ExpandAllCreatePO_Btn.Click();
            System.Threading.Thread.Sleep(500);
            return this;
        }

        public JobDetailPage SelectPOByBuildingPhaseAndProduct(string buildingPhaseCode, string productName)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']/tbody/tr[./td/span[starts-with(.,'{buildingPhaseCode}')]]/following-sibling::tr//tbody/tr[./td[3][contains(text(),'{productName}')]]/td[2]/input";
            CheckBox selected_Chk = new CheckBox(FindType.XPath, xpath);
            if (selected_Chk.IsExisted(false))
            {
                selected_Chk.Check();
            }
            else
            {
                ExtentReportsHelper.LogFail($"The PO with building phase code <font color='green'><b>{buildingPhaseCode}</b></font> and product name <font color='green'><b>{productName}</b></font> could not be found.");
                throw new NotFoundException($"The PO with building phase code {buildingPhaseCode} and product name {productName} could not be found.");
            }
            return this;
        }

        public void ProcessCreatePO()
        {
            CreatePO_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhasesView']/div[1]", 300, 0);
        }

        public JobDetailPage ExpandAllPOOnViewPOPage()
        {
            ExpandAllPO_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView']/div[1]");
            ExtentReportsHelper.LogInformation($"After expanded.");
            return this;
        }

        public JobDetailPage SortDateCreatedAsc(string buildingPhaseCode)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']/tbody/tr[./td[3]/span[starts-with(text(),'{buildingPhaseCode}')]]/following-sibling::tr//th[./a[.='Date Created']]";
            Label header_lbl = new Label(FindType.XPath, xpath);
            if (!header_lbl.GetAttribute("class").Contains("rgSorted"))
            {
                header_lbl.UpdateValueToFind($"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']/tbody/tr[./td[3]/span[starts-with(text(),'{buildingPhaseCode}')]]/following-sibling::tr//th/a[.='Date Created']");
                header_lbl.RefreshWrappedControl();
                header_lbl.JavaScriptClick();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView']/div[1]");
            }
            return this;
        }


        public string SelectPOOnViewPOPage(string buildingPhaseCode, string status, string createdDate, string userCreated)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']/tbody/tr[./td[3]/span[starts-with(text(),'{buildingPhaseCode}')]]/following-sibling::tr//tbody/tr[./td[6][.='{status}'] and ./td[7][.='{createdDate}'] and ./td[8][.='{userCreated}']]";
            string selected_chk = xpath + "/td[2]/input";
            string selected_POName = xpath + "/td[4]";
            CheckBox selectedPO_Chk = new CheckBox(FindType.XPath, selected_chk);
            Label selectedPO_lbl = new Label(FindType.XPath, selected_POName);
            if (selectedPO_Chk.IsExisted(false))
            {
                selectedPO_Chk.Check();
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(selectedPO_lbl), $"The PO with building phase code <font color='green'><b>{buildingPhaseCode}</b></font>, status <font color='green'><b>{status}</b></font>, created at <font color='green'><b>{createdDate}</b></font> by <font color='green'><b>{userCreated}</b></font> is selected.");
                return selectedPO_lbl.GetText();
            }
            else
            {
                ExtentReportsHelper.LogFail($"The PO with building phase code <font color='green'><b>{buildingPhaseCode}</b></font>, status <font color='green'><b>{status}</b></font>, created at <font color='green'><b>{createdDate}</b></font> by <font color='green'><b>{userCreated}</b></font> could not be found.");
                throw new NotFoundException($"The PO with building phase code {buildingPhaseCode} and status {status} could not be found.");
            }
        }

        public void SyncPO()
        {
            SyncToBuildPro_Btn.Click();
            if (StartSyncToBuildPro.WaitForElementIsVisible(3))
            {
                StartSyncToBuildPro.Click();
                System.Threading.Thread.Sleep(1000);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_BuildProSyncModal_autoGrid_rgResults']/div[1]", 300, 0);
            }
        }

        public void ChangeVendor(string PONumber, string vendorName)
        {
            VendorChangeRequest_Grid.ClickEditItemInGridWithTextContains("PO #", PONumber);
            DropdownList dropdownList = new DropdownList(FindType.XPath, "//*[contains(@id,'ReplacementVendors')]/select");
            dropdownList.SelectItem(vendorName, true);

            Button accept = new Button(FindType.XPath, "//*[contains(@id,'_UpdateButton')]");
            accept.Click();
            VendorChangeRequest_Grid.WaitGridLoad();
        }

        public void SelectAndProcessVendorChange(string PONumber)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_rgVendorChangesPending_ctl00']/tbody/tr[./td[contains(.,'{PONumber}')]]/td/input[@type='checkbox']";
            CheckBox selected_Chk = new CheckBox(FindType.XPath, xpath);

            if (selected_Chk.IsExisted(false))
                selected_Chk.Check();
            else
            {
                ExtentReportsHelper.LogFail($"The PO # <font color='green'><b>{PONumber}</b></font> could not be found.");
            }
            ProcessPOVenderChange_Btn.Click();
            VendorChangeRequest_Grid.WaitGridLoad();
        }

        public void SyncVendorChangeToBuildPro(string PONumber)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_rgVendorChangesProcessed_ctl00']/tbody/tr[./td[contains(.,'{PONumber}')]]/td/input[@type='checkbox']";
            CheckBox selected_Chk = new CheckBox(FindType.XPath, xpath);
            if (selected_Chk.IsExisted(false))
                selected_Chk.Check();
            else
            {
                ExtentReportsHelper.LogFail($"The PO # <font color='green'><b>{PONumber}</b></font> could not be found.");
            }
            Button sync = new Button(FindType.Id, "ctl00_CPH_Content_lbStartSync");
            sync.Click();

            Button SyncVendorChange_Btn = new Button(FindType.Id, "ctl00_CPH_Content_BuildProSyncModal_lbBuildProIntegrationSync");
            if (SyncVendorChange_Btn.WaitForElementIsVisible(3))
            {
                SyncVendorChange_Btn.Click();
                System.Threading.Thread.Sleep(1000);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_BuildProSyncModal_autoGrid_rgResults']/div[1]");
            }
        }

        /// <summary>
        /// Update job on the detail page
        /// </summary>
        /// <param name="jobData"></param>
        public void UpdateJobOnDetailPage(JobData jobData)
        {
            // Update job on the detail page
            CreateAJob(jobData);

            string actualToastMess = GetLastestToastMessage();
            string expectedToastMess = $"Job {jobData.Name} saved successfully!";

            if (actualToastMess.Equals(expectedToastMess))
                ExtentReportsHelper.LogPass($"<font color='green'><b>Update job {jobData.Name} successfully. Toast message same is as the expectation.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Failed to update job {jobData.Name}. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }

            // Refreshing page and verify the updated item
            CommonHelper.RefreshPage();
            IsCreateSuccessfully(jobData);
        }

        /// <summary>
        /// Switch current job to Open or Close mode and verify it
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="status"></param>
        public void OpenOrCloseJob(string status)
        {
            Button Status_Btn;
            string expectedMess;
            bool isCaptured = false;
            bool isJobClosedPanelDisplay;

            if (status.ToLower() == "open job")
            {
                // Click to Open button to close job. Expected: Job is closed
                Status_Btn = OpenJob_btn;
                expectedMess = "Job Closed";
                isJobClosedPanelDisplay = true;
            }
            else
            {
                // Click to Close button to open job. Expected: Job is opened
                Status_Btn = CloseJob_btn;
                expectedMess = "Job Opened";
                isJobClosedPanelDisplay = false;
            }

            if (Status_Btn.IsDisplayed() is false)
            {
                ExtentReportsHelper.LogInformation($"<font color='Yellow'><b>Current Job status is '{expectedMess}'. Don't need to switch mode.</b></font>");
                return;
            }

            Status_Btn.Click(isCaptured);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']");

            string actualToastMess = GetLastestToastMessage();

            if (actualToastMess.Equals(expectedMess))
                ExtentReportsHelper.LogPass($"<font color='green'><b>Switch the current job to status '{expectedMess}' successfully. Toast message same as the expectation.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Failed to switch the current job to status '{expectedMess}'. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }

            // Verify 'Job is Closed' panel display
            Label JobClosed_lbl = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlClosed']");
            if (JobClosed_lbl.IsDisplayed(false) != isJobClosedPanelDisplay)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Failed to Switch job status to <b>'{expectedMess}'</b>" +
                    $"<br>With Close status, The panel 'Job is Closed' with red color should display." +
                    $"<br>With Open status, The panel 'Job is Closed' with red color should be invisible.</font>");
            } else
                ExtentReportsHelper.LogPass($"<font color='green'><b>Switch job status to '{expectedMess}' successfully.</b>" +
                    $"<br>With Close status, display the panel 'Job is Closed' with red color." +
                    $"<br>With Open status, don't display the panel 'Job is Closed' with red color.</font>");

        }

        /// <summary>
        /// Upload community image
        /// </summary>
        /// <param name="List_Resource"></param>
        public void UploadCommununityAndVerify(params string[] List_Resource)
        {
            foreach (string source in List_Resource)
            {
                if (string.Empty == source)
                    return;

                Textbox Image_txt = new Textbox(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_AsyncUpload1file')]");
                Image_txt.SendKeysWithoutClear(source);

                // On the first time uploading image, there is no action/ loading icon => wait 8s
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_imgJobImage']", 8000);

                // Verify uploaded image
                Label ImageAfterUploading_lbl = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_imgJobImage']");
                if (ImageAfterUploading_lbl.IsDisplayed() is true)
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The uploaded image display correctly on the Job detail page.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='green'>The uploaded image can't display on the Job detail page after 8s.</font>");
            }

        }

        /// <summary>
        /// View First and Last configs on Jon detail page
        /// </summary>
        /// <param name="configNum"></param>
        public void ViewFistAndLastConfigs(string configNum)
        {
            Button FistAndLast_btn = new Button(FindType.XPath, "//*[@id='configs-hide']");

            if (FistAndLast_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogInformation($"<font color = red>Current mode is 'View First and Last Configs'. Don't need to change mode.</font>");
            }
            else
            {
                FistAndLast_btn.Click(false);
                // No loading message, so set waiting time
                System.Threading.Thread.Sleep(1000);
            }

            // Verify First and Last config display only
            int configNumber = int.Parse(configNum);
            ListItem config_lst = new ListItem(FindElementHelper.FindElements(FindType.XPath, "//div[contains(@class, 'configuration-number')]/a"));

            if (configNumber == 1)
            {
                // Display Configuration 1 only
                if (config_lst.GetAllItems().Count == 1)
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Config number '1' display correctly on 'View First and Last configs' mode</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Config number <b>'1'</b> should display on 'View First and Last configs' mode.</font>");
            }
            else
            {
                // Verify the config 1 and last one is displayed
                if (config_lst.GetAllItems()[0].Text != "1" || config_lst.GetAllItems().Last().Text != configNum)
                    ExtentReportsHelper.LogFail($"<font color='red'>Config number <b>'1'</b> and <b>'{configNumber}'</b> should display on 'View First and Last configs' mode.</font>");
                else
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Config number '1' display correctly on 'View First and Last configs' mode</b></font>");
            }
        }

        /// <summary>
        /// View All Config on Job detail page
        /// </summary>
        /// <param name="configNum"></param>
        public void ViewAllConfigs(string configNum)
        {
            Button AllConfigs_btn = new Button(FindType.XPath, "//*[@id='configs-show']");

            if (AllConfigs_btn.IsDisplayed(false) is false)
                ExtentReportsHelper.LogInformation($"<font color = red>Current mode is 'View All Configs'. Don't need to change mode.</font>");
            else
            {
                AllConfigs_btn.Click(false);
                // No loading message, so set waiting time
                System.Threading.Thread.Sleep(1000);
            }

            // Verify all Configs
            ListItem config_lst = new ListItem(FindElementHelper.FindElements(FindType.XPath, "//div[contains(@class, 'configuration-number')]/a"));
            int index = 1;
            int configNumber = int.Parse(configNum);
            while (index <= configNumber)
            {
                if (config_lst.GetAllItems()[index - 1].Text == index.ToString())
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Config number '{index}' display correctly on  'View All Configs' mode</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Config number <b>'{index}'</b> should display on  'View All Configs' mode.</font>");
                index++;
            }
        }
    }
}
