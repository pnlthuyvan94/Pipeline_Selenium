using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.CostType.CostTypeDetail
{
    public partial class CostTypeDetailPage
    {
        /******************* Cost Type detail page *******************/

        private CostTypeDetailPage EnterCostTypeName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        private CostTypeDetailPage EnterCostTypeDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Description_txt.SetText(description);
            return this;
        }

        private CostTypeDetailPage SelectTaxGroup(string taxGroup)
        {
            if (!string.IsNullOrEmpty(taxGroup))
                TaxGroup_ddl.SelectItem(taxGroup);
            return this;
        }

        private void ClickSaveCostType()
        {
            SaveCostType_Btn.Click(false);
        }

        public void UpdateCostType(CostTypeData data)
        {
            EnterCostTypeName(data.Name).EnterCostTypeDescription(data.Description)
                .SelectTaxGroup(data.TaxGroup).ClickSaveCostType();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbSaveContinue']");

            // Verify toast message
            string expectedMessage = $"Cost Code Type Saved";
            string actualMsg = GetLastestToastMessage();
            if (actualMsg == expectedMessage)
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Cost Type with name '{data.Name}' updated successfully!</b></font>");
            else
                ExtentReportsHelper.LogInformation($"<font color = 'yellow'>Can't get toast message - Possible constraints preventing updational.</font>");

            // Refresh page and verify the updated item
            ExtentReportsHelper.LogInformation(null, $"Refresh page and verify the updated item.");
            CommonHelper.RefreshPage();

            if (IsCostTypeDisplayCorrect(data) is true)
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>The updated Cost Type displays correctly after refreshing page.</b></font>");
        }

        /******************* Cost Categories *******************/
        public void ClickAddCostCategory()
        {
            AddCostCategory_btn.Click();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CostCategory_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(loading_Xpath, 500);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CostCategory_Grid.IsItemOnCurrentPage(columnName, value);
        }

        /// <summary>
        /// Remove Category from Cost Type by name
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="categoryName"></param>
        public void RemoveCostCategoryFromCostType(string columnName, string categoryName)
        {
            CostCategory_Grid.ClickDeleteItemInGrid(columnName, categoryName);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(loading_Xpath);

            // Verify toast message
            string actualMessage = GetLastestToastMessage();
            string expectedMessage = "Building Phase successfully removed.";
            if (!string.IsNullOrEmpty(actualMessage) && actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Cost Categories '{categoryName}' is removed from current Cost Type successfuly.</b></font>");
            }
            else
            {
                if (IsItemInGrid(columnName, categoryName) is true)
                    ExtentReportsHelper.LogFail($"<font color='red'>Cost Categories '{categoryName}' is NOT removed from current Cost Type." +
                        "<br>Failed to remove Cost Category from Cost Type.</font>");
                else
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Cost Categories '{categoryName}' is removed from current Cost Type successfuly.</b></font>");
            }
        }

        /// <summary>
        /// Remove all Categories from Cost Type
        /// </summary>
        public void RemoveAllCostCategoryFromCostType()
        {
            // Select all
            CheckBox selectAll_ckb = new CheckBox(FindType.XPath, "//*[contains(@id, 'ClientSelectColumnSelectCheckBox')]");
            if (selectAll_ckb == null || selectAll_ckb.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"font color='red'>Can't find Select All button to click.</font>");
                return;
            }
            selectAll_ckb.SetCheck(true, false);

            // Click Bulk action
            Button bulkAction_btn = new Button(FindType.XPath, "//*[@id='bulk-actions']");
            if (bulkAction_btn == null || bulkAction_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Bulk Action button to click.</font>");
                return;
            }
            bulkAction_btn.Click(false);

            // Select Delete selected
            Button deleteSelected_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbDeleteSelectedCostCategories']");
            if (deleteSelected_btn == null || deleteSelected_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find 'Delele Selected' button to click.</font>");
                return;
            }
            deleteSelected_btn.Click(false);
            ConfirmDialog(ConfirmType.OK);
            PageLoad();

            // Verify the number of item after deleting all
            if (CostCategory_Grid.GetTotalItems != 0)
                ExtentReportsHelper.LogFail($"<font color='red'>There are some categories on the grid view." +
                    $"<br>Failed to remove all Categories from current Cost Type.</br></font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>All Categories on the grid view are removed from current Cost Type.</b></font>");

        }

        /// <summary>
        /// Select categories to assign to current Cost Type
        /// </summary>
        /// <param name="categoryList">List of selected categories</param>
        public void SelectCostCategoryByName(params string[] categoryList)
        {
            IWebElement costCategory;
            Actions action = new Actions(driver);
            action.KeyDown(Keys.Control).Build().Perform();
            foreach (var item in categoryList)
            {
                costCategory = FindElementHelper.FindElement(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbCostCategories']/div/ul/li/span[contains(text(),'{item}')]");
                if (costCategory == null)
                {
                    ExtentReportsHelper.LogInformation($"<font color='yellow'>The Cost Category with name '{item}' doesn't display on the modal.</font>");
                    break;
                }
                else
                {
                    // Select multiple item
                    costCategory.Click();
                }
            }
            action.KeyUp(Keys.Control).Build().Perform();

            SaveCostCategory_btn.Click(false);
            WaitingLoadingGifByXpath(loading_Xpath);

            // Click Save button and verify toast message
            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = "Cost Code Categories were successfully added.";
            if (!string.IsNullOrEmpty(_actualMessage) && _actualMessage != _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Could not assign Cost Categories to current Cost Type.</font>" +
                    $"<br>The expected message: {_expectedMessage}" +
                    $"<br>The actual message: {_actualMessage}</br>");
                CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assign Cost Categories to current Cost Type successfuly.</b></font>");
            }
        }

    }
}
