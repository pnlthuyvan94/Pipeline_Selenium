using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.CostCode.CostCodeDetail
{
    public partial class CostCodeDetailPage
    {
        /******************* Cost Type detail page *******************/

        private CostCodeDetailPage EnterCostCodeName(string name)
        {
            if (name != null)
                Name_txt.SetText(name);
            return this;
        }

        private CostCodeDetailPage EnterCostCodeDescription(string description)
        {
            if (description != null)
                Description_txt.SetText(description);
            return this;
        }

        private void ClickSaveCostCode()
        {
            SaveCostCode_Btn.Click(false);
        }

        public void UpdateCostCode(CostCodeData data)
        {
            EnterCostCodeName(data.Name).EnterCostCodeDescription(data.Description)
                .ClickSaveCostCode();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbSaveContinue']");

            // Verify toast message
            string expectedMessage = $"Cost Code Successfully Saved";
            string actualMsg = GetLastestToastMessage();
            if (actualMsg == expectedMessage)
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Cost Code with name '{data.Name}' updated successfully!</b></font>");
            else
                ExtentReportsHelper.LogInformation($"<font color = 'yellow'>Can't get toast message - Possible constraints preventing updational.</font>");

            // Refresh page and verify the updated item
            ExtentReportsHelper.LogInformation(null, $"<b>Refresh page and verify the updated item.</b>");
            CommonHelper.RefreshPage();

            if (IsCostTypeDisplayCorrect(data) is true)
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>The updated Cost Code displays correctly after refreshing page.</b></font>");
        }

        /******************* Building Phase *******************/
        public void ClickAddBuildingPhase()
        {
            AddBuildingPhase_btn.Click(false);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhase_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(loading_Xpath, 500);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingPhase_Grid.IsItemOnCurrentPage(columnName, value);
        }

        /// <summary>
        /// Remove Category from Cost Type by name
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="buildingPhaseName"></param>
        public void RemoveBuildingPhaseFromCostCode(string columnName, string buildingPhaseName)
        {
            BuildingPhase_Grid.ClickDeleteItemInGrid(columnName, buildingPhaseName);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(loading_Xpath);

            // Verify toast message
            string actualMessage = GetLastestToastMessage();
            string expectedMessage = "Building Phase successfully removed.";
            if (!string.IsNullOrEmpty(actualMessage) && actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Building Phase '{buildingPhaseName}' is removed from current Cost Code successfuly.</b></font>");
            }
            else
            {
                if (IsItemInGrid(columnName, buildingPhaseName) is true)
                    ExtentReportsHelper.LogFail($"<font color='red'>Building Phase '{buildingPhaseName}' is NOT removed from current Cost Code." +
                        "<br>Failed to remove Cost Category from Cost Type.</font>");
                else
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Building Phase'{buildingPhaseName}' is removed from current Cost Code successfuly.</b></font>");
            }
        }

        /// <summary>
        /// Remove all Categories from Cost Type
        /// </summary>
        public void RemoveAllBuildingPhaseFromCostCode()
        {
            // Don't capture many items
            bool isCaptured = false;

            // Select all
            CheckBox selectAll_ckb = new CheckBox(FindType.XPath, "//*[@class='rgHeader rgCheck']/*[contains(@id, 'ClientSelectColumnSelectCheckBox')]");
            if (selectAll_ckb == null || selectAll_ckb.IsDisplayed(isCaptured) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Select All button to click.</font>");
                return;
            }
            selectAll_ckb.SetCheck(true, isCaptured);

            // Click Bulk action
            Button bulkAction_btn = new Button(FindType.XPath, "//*[@id='bulk-actions']");
            if (bulkAction_btn == null || bulkAction_btn.IsDisplayed(isCaptured) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Bulk Action button to click.</font>");
                return;
            }
            bulkAction_btn.Click(isCaptured);

            // Select Delete selected
            Button deleteSelected_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbDeleteSelectedBuildingPhases']");
            if (deleteSelected_btn == null || deleteSelected_btn.IsDisplayed(isCaptured) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find 'Delele Selected' button to click.</font>");
                return;
            }
            deleteSelected_btn.Click(isCaptured);
            ConfirmDialog(ConfirmType.OK);
            PageLoad();

            // Verify the number of item after deleting all
            if (BuildingPhase_Grid.GetTotalItems != 0)
                ExtentReportsHelper.LogFail($"<font color='red'>There are some Building Phase on the grid view." +
                    $"<br>Failed to remove all Categories from current Cost Type.</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>All Categories on the grid view are removed from current Cost Type.</b></font>");

        }

        /// <summary>
        /// Select Building Phase to assign to current Cost Type
        /// </summary>
        /// <param name="buildingPhaseList">List of selected Building Phase</param>
        public void SelectBuildingPhaseByName(params string[] buildingPhaseList)
        {
            IWebElement buildingPhase;
            Actions action = new Actions(driver);
            action.KeyDown(Keys.Control).Build().Perform();
            foreach (var item in buildingPhaseList)
            {
                buildingPhase = FindElementHelper.FindElement(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbBuildingPhases']/div/ul/li/span[contains(text(),'{item}')]");  
                if (buildingPhase == null)
                {
                    ExtentReportsHelper.LogInformation($"<font color='yellow'>The Building Phase with name '{item}' doesn't display on the modal.</font>");
                    break;
                }
                else
                {
                    buildingPhase.Click();
                }
            }
            action.KeyUp(Keys.Control).Build().Perform();

            // Click Save button and verify toast message
            SaveBuildingPhase_btn.Click(false);

            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = "Building Phase(s) were successfully added.";
            if (!string.IsNullOrEmpty(_actualMessage) && _actualMessage != _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Could not assign Cost Categories to current Cost Type.</font>" +
                    $"<br>The expected message: {_expectedMessage}" +
                    $"<br>The actual message: {_actualMessage}</br>");
                CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assign Building Phases to current Cost Code successfuly.</b></font>");
            }
        }

    }
}
