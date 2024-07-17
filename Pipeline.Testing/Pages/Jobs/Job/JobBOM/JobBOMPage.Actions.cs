using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM.OneTimeItem;
using System;
using System.Linq;

namespace Pipeline.Testing.Pages.Jobs.Job.JobBOM
{
    public partial class JobBOMPage
    {
        public void GenerateJobBOM()
        {
            // Refresh page to reload the grid view
            CommonHelper.RefreshPage();
            System.Threading.Thread.Sleep(5000);
            if (GenerateBOM_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color = red>Can't find 'Generate BOM' button on current page.</font>");
                return;
            }
            GenerateBOM_btn.Click(false);

            // Wait loading
            WaitingLoadingGifByXpath(loadingGrid_xpath);

            // Get current toast message and verify it
            string actualToastMess = GetLastestToastMessage(50);
            string expectedToastMess = "BOM generation complete!";

            if (actualToastMess.Equals(expectedToastMess))
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Generate Job BOM successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Failed to Generate Job BOM. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return JobBomPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInBOMGrid(string columnName, GridFilterOperator filterOperator, string value)
        {
            JobBomPage_Grid.FilterByColumn(columnName, filterOperator, value);
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        /// <summary>
        /// Switch View Mode on the BOM grid view
        /// </summary>
        /// <param name="viewMode"></param>
        public void SwitchJobBomView(string viewMode)
        {
            DropdownList View_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlViewTypes']");

            // Current view mode is same as expectation, then don't need to update it
            if (View_ddl.SelectedItemName == viewMode)
                return;

            string[] viewModeList = new String[] { "Phase","Phase/Product",
                "Phase/Product/Use",
                "Option",
                "Option/Phase/Product/Use",
                "Option/Phase/Product" };

            string selectedViewMode = viewModeList.Where(p => (p == viewMode)).FirstOrDefault();

            // If the input View Mode doesn't display on the dropdown list, then selet "Option/Phase/Product" by default.
            if (string.IsNullOrEmpty(selectedViewMode))
                selectedViewMode = "Option/Phase/Product";

            View_ddl.SelectItem(selectedViewMode);

            // Wait loading
            WaitingLoadingGifByXpath(loadingGrid_xpath,3000);

        }

        public int GetTotalNumberItem()
        {
            return JobBomPage_Grid.GetTotalItems;
        }
        public void DownloadBaseLineJobBOMFile(string exportType, string exportName)
        {
            bool isCaptured = false;
            // Download baseline file to report folder
            try
            {
                Utilities_btn.Click();
                string itemXpath = $"//*[@id='ctl00_CPH_Content_hypUtils']/following::a[.='{exportType}']";
                IWebElement itemNeedToClick = driver.FindElement(By.XPath(itemXpath));
                CommonHelper.WaitUntilElementVisible(5, itemXpath, false);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(itemNeedToClick),
                        $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                itemNeedToClick.Click();
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {exportType} on Utilities menu"));
            }
            //Close Utilities 
            Utilities_btn.Click();

            // Verify and move it to baseline folder
            ValidationEngine.DownloadBaseLineFile(exportType, exportName);

        }

        public void ExportJobBOMFile(string exportType, string exportName, int expectedTotalNumber, string expectedExportTitle)
        {
            bool isCaptured = false;
            // Download baseline file to report folder
            try
            {
                Utilities_btn.Click();
                string itemXpath = $"//*[@id='ctl00_CPH_Content_hypUtils']/following::a[.='{exportType}']";
                IWebElement itemNeedToClick = driver.FindElement(By.XPath(itemXpath));
                CommonHelper.WaitUntilElementVisible(5, itemXpath, false);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(itemNeedToClick),
                        $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                itemNeedToClick.Click();
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {exportType} on Utilities menu"));
            }
            //Close Utilities 
            Utilities_btn.Click();

            // Don't verify total number and header if that's xml file
            if (exportType.ToLower().Contains("xml"))
                return;

            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(exportType, exportName, expectedExportTitle, expectedTotalNumber);
        }

        public void OpenOneTimeItemModal()
        {
            BomAdjustments_btn.Click();
            CommonHelper.CaptureScreen();
            OneTimeItem_Btn.Click();
            CommonHelper.CaptureScreen();
            OneTimeItemModal = new OneTimeItemModal();
        }

        public void DeleteItemInOneTimeItemGrid(string columnName, string value)
        {
            OneTimeItem_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnOneTimeProduct']/div[1]");
        }

        public bool IsItemInOneTimeItemGrid(string columnName, string value)
        {
            return OneTimeItem_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInOneTimeItemGrid(string columnName, GridFilterOperator filterOperator, string value)
        {
            OneTimeItem_Grid.FilterByColumn(columnName, filterOperator, value);
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }


        public bool IsItemInPhaseUseGrid(string columnName, string value)
        {
            return PhaseViewJobBomPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInPhaseUseBOMGrid(string columnName, GridFilterOperator filterOperator, string value)
        {
            PhaseViewJobBomPage_Grid.FilterByColumn(columnName, filterOperator, value);
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        public void EditOneTimeItem(string optionName, string unitCost, string quantity)
        {
            OneTimeItem_Grid.ClickEditItemInGrid("Option", optionName);
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();

            //Textbox unitCostTextBox = new Textbox(FindType.Id, "ctl00_CPH_Content_rgOneTimeProduct_ctl00_ctl04_txtUpdateProductCost1Time");
            Textbox unitCostTextBox = new Textbox(FindType.XPath, "//input[@type='text' and contains(@id,'txtUpdateProductCost1Time')]");
            unitCostTextBox.Clear();
            unitCostTextBox.AppendKeys(unitCost);

            Textbox quantityTextBox = new Textbox(FindType.XPath, "//input[@type='text' and contains(@id,'txtUpdateProductQuantity1Time')]");
            quantityTextBox.Clear();
            quantityTextBox.AppendKeys(quantity);

            ExtentReportsHelper.LogPass(null, $"Total Cost” column is no longer an editable field when edit mode is activated.");

            Button accept = new Button(FindType.XPath, "//input[@type='image' and contains(@id,'_UpdateButton')]");
            accept.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgOneTimeProduct_ctl00']/div[1]", 5000);
        }

        public void SelectBOMAdjustQuantities(string adjust)
        {
            BOMAdjustments_btn.Click();
            Button Adjust_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lb{adjust}']");
            switch (adjust)
            {
                case "AdjustQuantities":
                    Adjust_btn.Click();
                    Label NewQuantityAdjustmentTitle_lbl = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments_ctl00']//a[@title='Add New Quantity Adjustment']");
                    NewQuantityAdjustmentTitle_lbl.WaitForElementIsVisible(5);
                    if (NewQuantityAdjustmentTitle_lbl.IsDisplayed() && NewQuantityAdjustmentTitle_lbl.GetText().Equals("Add New Quantity Adjustment"))
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Adjust Quantities Modal is displayed.</b></font>");

                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>Adjust Quantities Modal is not displayed.</font>");
                    }
                    break;
                case "OneTimeProduct":
                    Adjust_btn.Click();
                    OneTimeItemModal = new OneTimeItemModal();
                    Label OneTimeProductTitle_lbl = new Label(FindType.XPath, "//*[@id='one-time-product-modal']//header[@class='card-header']/h1");
                    OneTimeProductTitle_lbl.WaitForElementIsVisible(5);
                    if (OneTimeProductTitle_lbl.IsDisplayed() && OneTimeProductTitle_lbl.GetText().Equals("Add One Time Item"))
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Add One Time Product Modal is displayed.</b></font>");

                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>Add One Time Product Modal is not displayed.</font>");
                    }
                    break;
                default:
                    ExtentReportsHelper.LogInformation(null, $"No adjust type is select.");
                    break;
            }

        }

        public void AddNewQuantityAdjustment(JobBOMData JobBOMData)
        {
            Button AddNewQuantity_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgQuantityAdjustments']//input[@title='Add New Quantity Adjustment']");
            AddNewQuantity_btn.Click();
            System.Threading.Thread.Sleep(5000);
            Option_ddl.SelectItem(JobBOMData.Option);
            Buildingphase_ddl.SelectItem(JobBOMData.BuildingPhase);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgQuantityAdjustments']/div[1]");
            Product_ddl.SelectItem(JobBOMData.Product);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgQuantityAdjustments']/div[1]");
            Style_ddl.SelectItem(JobBOMData.Style);
            Use_ddl.SelectItem(JobBOMData.Use);
            NewQuantity_txt.SetText(JobBOMData.NewQuantity);
            Insert_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgQuantityAdjustments']/div[1]");
        }

        public bool IsItemInAdjustQuantitiesGrid(string colum, string value)
        {
            return AdjustQuantities_Grid.IsItemOnCurrentPage(colum, value);

        }

        public void DeleteItemInAdjustQuantitiesGrid(string colum, string value)
        {
            AdjustQuantities_Grid.ClickDeleteItemInGrid(colum, value);
            ConfirmDialog(ConfirmType.OK);
            System.Threading.Thread.Sleep(2000);
        }

        public void SelectJobBOMArchives(string select)
        {
            JobBOMArchives_btn.Click();
            Label Title_lbl = new Label(FindType.XPath, "//*[@class='card-header clearfix']/h1");
            switch (select)
            {
                case "Job Config BOM Archive":
                    JobConfigBom_btn.Click();
                    PageLoad();
                    CommonHelper.SwitchLastestTab();
                    if (Title_lbl.IsDisplayed() && Title_lbl.GetText().Equals("Configuration"))
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Configuration Page is displayed.</b></font>");

                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>Configuration Page is not displayed.</font>");
                    }
                    break;
                case "Job BOM Archive 3.x":
                    ArchiveJobBom_btn.Click();
                    PageLoad();
                    CommonHelper.SwitchLastestTab();
                    if (Title_lbl.IsDisplayed() && Title_lbl.GetText().Equals("Archived Job BOM"))
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Archived Job BOM Page is displayed.</b></font>");

                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>Archived Job BOM Page is not displayed.</font>");
                    }
                    break;
                default:
                    ExtentReportsHelper.LogInformation(null, $"No JobBom type is select.");
                    break;
            }
        }

        public void SwitchBOMBasedOnView(string viewMode)
        {
            DropdownList ViewBom_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBOMTypes']");
            // Current view mode is same as expectation, then don't need to update it
            if (ViewBom_ddl.SelectedItemName == viewMode)
                return;
            string[] viewBOMModeList = new String[] { "Job Quantities", "House Quantities" };

            string selectedViewbomMode = viewBOMModeList.Where(p => (p == viewMode)).FirstOrDefault();

            // If the input View Mode doesn't display on the dropdown list, then selet "Option/Phase/Product" by default.
            if (string.IsNullOrEmpty(selectedViewbomMode))
                selectedViewbomMode = "House Quantities";
            ViewBom_ddl.SelectItem(selectedViewbomMode);

            // Wait loading
            WaitingLoadingGifByXpath(loadingGrid_xpath);

        }
    }
}
