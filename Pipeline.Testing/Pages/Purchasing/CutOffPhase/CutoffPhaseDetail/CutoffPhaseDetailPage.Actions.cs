using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.IO;
using System.Reflection;
using System.Text;

namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase.CutoffPhaseDetail
{
    public partial class CutoffPhaseDetailPage
    {
        /******************* Cutoff Phase detail page *******************/

        private CutoffPhaseDetailPage EnterCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
                Code_txt.SetText(code);
            return this;
        }

        private CutoffPhaseDetailPage EnterName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        private CutoffPhaseDetailPage EnterSortOrder(string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
                SortOrder_txt.SetText(sortOrder);
            return this;
        }

        private void ClickSaveCutoffPhase()
        {
            SaveCutoffPhase_Btn.Click(false);
        }

        /// <summary>
        /// Update Cutoff Phase with a new value
        /// </summary>
        /// <param name="data"></param>
        public void UpdateCutoffPhase(CutoffPhaseData data)
        {
            EnterCode(data.Code).EnterName(data.Name)
                .EnterSortOrder(data.SortOrder).ClickSaveCutoffPhase();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbSaveContinue']");

            // Verify toast message
            string expectedMsg = $"Cutoff Phase {data.Name} updated successfully!";
            string actualMsg = GetLastestToastMessage();
            if (actualMsg == expectedMsg)
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Cutoff Phase with name '{data.Name}' updated successfully!</b></font>");
            else
                ExtentReportsHelper.LogInformation($"<font color = 'yellow'>Can't get toast message - Possible constraints preventing updational.</font>");

            // Refresh page and verify the updated item
            ExtentReportsHelper.LogInformation(null, $"<b>Refresh page and verify the updated item.</b>");
            CommonHelper.RefreshPage();

            if (IsCutoffPhaseDisplayedCorrect(data) is true)
                ExtentReportsHelper.LogPass($"<font color = 'green'><b>The updated Cutoff Phase displays correctly after refreshing page.</b></font>");
        }

        /******************* Attribute assignment *******************/

        public void ClickAddAttributeButton(string attributeName)
        {
            Button add_btn;
            switch (attributeName)
            {
                case "Building Phases":
                    add_btn = AddBuildingPhase_btn;
                    break;

                case "Option Groups":
                    add_btn = AddOptionGroup_btn;
                    break;
                default:
                    // Default is "Options"
                    add_btn = AddOption_btn;
                    break;
            }

            add_btn.Click(false);
        }

        public void FilterItemInGrid(string attributeName, string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            IGrid attribute_grid;
            string loading_Xpath;
            switch (attributeName)
            {
                case "Building Phases":
                    attribute_grid = BuildingPhase_Grid;
                    loading_Xpath = buildingPhaseLoading_Xpath;
                    break;

                case "Option Groups":
                    attribute_grid = OptionGroup_Grid;
                    loading_Xpath = optionGroupLoading_Xpath;
                    break;
                default:
                    // Default is "Options"
                    attribute_grid = Option_Grid;
                    loading_Xpath = optionLoading_Xpath;
                    break;
            }

            attribute_grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(loading_Xpath, 500);
        }

        public bool IsItemInGrid(string attributeName, string columnName, string value)
        {
            IGrid attribute_grid;
            switch (attributeName)
            {
                case "Building Phases":
                    attribute_grid = BuildingPhase_Grid;
                    break;

                case "Option Groups":
                    attribute_grid = OptionGroup_Grid;
                    break;
                default:
                    // Default is "Options"
                    attribute_grid = Option_Grid;
                    break;
            }

            return attribute_grid.IsItemOnCurrentPage(columnName, value);
        }

        /// <summary>
        /// Remove attribute from Cutoff Phase by name. Attribute can be Building Phases/ Option Groups/ Options
        /// </summary>
        /// <param name="cutoffPhaseName"></param>
        /// <param name="attributeName"></param>
        /// <param name="columnName"></param>
        /// <param name="removeItem"></param>
        public void RemoveAttributeByName(string cutoffPhaseName, string attributeName, string columnName, string removeItem)
        {
            string expectedToasMsg;
            string loading_Xpath;
            IGrid currentGrid;

            switch (attributeName)
            {
                case "Building Phases":
                    expectedToasMsg = $"Building phase {removeItem} removed from {cutoffPhaseName}.";
                    loading_Xpath = buildingPhaseLoading_Xpath;
                    currentGrid = BuildingPhase_Grid;
                    break;

                case "Option Groups":
                    expectedToasMsg = $"Option Groups deleted from Cutoff Phase {cutoffPhaseName} successfully!";
                    loading_Xpath = optionGroupLoading_Xpath;
                    currentGrid = OptionGroup_Grid;
                    break;

                default:
                    // Default is "Options"
                    expectedToasMsg = $"Options deleted from Cutoff Phase {cutoffPhaseName} successfully!";
                    loading_Xpath = optionLoading_Xpath;
                    currentGrid = Option_Grid;
                    break;
            }

            currentGrid.ClickDeleteItemInGrid(columnName, removeItem);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(loading_Xpath);

            // Verify toast message
            string actualMessage = GetLastestToastMessage();
            if (!string.IsNullOrEmpty(actualMessage) && actualMessage == expectedToasMsg)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>{attributeName} '{removeItem}' is removed from current Cutoff Phase successfuly.</b></font>");
            }
            else
            {
                FilterItemInGrid(attributeName, columnName, GridFilterOperator.EqualTo, attributeName);
                if (IsItemInGrid(attributeName, columnName, attributeName) is true)
                    ExtentReportsHelper.LogFail($"<font color='red'>{attributeName} '{removeItem}' is NOT removed from current Cutoff Phase." +
                        $"<br>Failed to remove {attributeName} '{removeItem}' from Cutoff Phase.</font>");
                else
                    ExtentReportsHelper.LogPass($"<font color='green'><b>{attributeName} '{removeItem}' is removed from current Cutoff Phase successfuly.</b></font>");
            }
        }

        /// <summary>
        /// Remove all attributes from Cutoff Phase. Attribute can be Building Phases/ Option Groups/ Options
        /// </summary>
        /// <param name="cutoffPhaseName"></param>
        /// <param name="attributeName">Building Phases/ Option Groups/ Options</param>
        public void RemoveAllAttributesFromCutoffPhase(string cutoffPhaseName, string attributeName)
        {
            // Don't capture many images
            bool isCaptured = false;

            string expectedToasMsg;
            string selectAll_Xpath;
            string bulkAction_btn_Xpath;
            string deleteSelected_Xpath;
            string loading_Xpath;
            IGrid currentGrid;

            switch (attributeName)
            {
                case "Building Phases":
                    expectedToasMsg = $"building phases removed from {cutoffPhaseName}.";
                    selectAll_Xpath = "//*[@id='ctl00_CPH_Content_rgBuildingPhasesInfo_ctl00']//th[contains(@class, 'rgHeader rgCheck')]/input";
                    bulkAction_btn_Xpath = "//*[@id='bulk-actions-buildingPhases']";
                    deleteSelected_Xpath = "//*[@id='ctl00_CPH_Content_lbDeleteSelectedBuildingPhases']";
                    loading_Xpath = buildingPhaseLoading_Xpath;
                    currentGrid = BuildingPhase_Grid;
                    break;

                case "Option Groups":
                    expectedToasMsg = "Option Groups removed from Cutoff Phase.";
                    selectAll_Xpath = "//*[@id='ctl00_CPH_Content_rgOptionGroupsInfo_ctl00']//th[contains(@class, 'rgHeader rgCheck')]/input";
                    bulkAction_btn_Xpath = "//h1[text()='Option Groups']/preceding-sibling::div/a";
                    deleteSelected_Xpath = "//*[@id='ctl00_CPH_Content_lbDeleteSelectedOptionGroups']";
                    loading_Xpath = optionGroupLoading_Xpath;
                    currentGrid = OptionGroup_Grid;
                    break;

                default:
                    // Default is "Options"
                    expectedToasMsg = "Options removed from Cutoff Phase.";
                    selectAll_Xpath = "//*[@id='ctl00_CPH_Content_rgOptionsInfo_ctl00']//th[contains(@class, 'rgHeader rgCheck')]/input";
                    bulkAction_btn_Xpath = "//*[@id='bulk-actions']";
                    deleteSelected_Xpath = "//*[@id='ctl00_CPH_Content_lbDeleteSelectedOptions']";
                    loading_Xpath = optionLoading_Xpath;
                    currentGrid = Option_Grid;
                    break;
            }

            // Select all
            CheckBox selectAll_ckb = new CheckBox(FindType.XPath, selectAll_Xpath);
            if (selectAll_ckb.IsDisplayed(isCaptured) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Select All button to click on {attributeName} grid.</font>");
                return;
            }
            selectAll_ckb.SetCheck(true, isCaptured);

            // Click Bulk action
            Button bulkAction_btn = new Button(FindType.XPath, bulkAction_btn_Xpath);
            if (bulkAction_btn.IsDisplayed(isCaptured) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Bulk Action button to click on {attributeName} grid.</font>");
                return;
            }
            bulkAction_btn.Click(isCaptured);

            // Select Delete selected
            Button deleteSelected_btn = new Button(FindType.XPath, deleteSelected_Xpath);
            if (deleteSelected_btn.IsDisplayed(isCaptured) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find 'Delele Selected' button to click on {attributeName} grid.</font>");
                return;
            }
            deleteSelected_btn.Click(isCaptured);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(loading_Xpath);


            // Verify toast message
            string actualMessage = GetLastestToastMessage();
            if (!string.IsNullOrEmpty(actualMessage) && actualMessage.Contains(expectedToasMsg) is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>All {attributeName} are removed from current Cutoff Phase successfuly.</b></font>");
            else
                ExtentReportsHelper.LogInformation($"<Can't get toast message after deleting {attributeName} from current Cutoff Phase.");


            // Verify the number of item after deleting all
            if (currentGrid.GetTotalItems != 0)
                ExtentReportsHelper.LogFail($"<font color='red'>There are some {attributeName} on the grid view." +
                    $"<br>Failed to remove all {attributeName} from current Cutoff Phase.</br></font>");
        }

        /// <summary>
        /// Select attribute to assign to current Cutoff Phase. Attribute can be Building Phases/ Option Groups/ Options
        /// </summary>
        /// <param name="cutoffPhaseName"></param>
        /// <param name="attributeName">Building Phases/ Option Groups/ Options</param>
        public void SelectAttributeByName(string cutoffPhaseName, string attributeName, params string[] attributeList)
        {
            CheckBox selectedItem;
            Button SaveAttribute_btn = SaveOption_btn;
            string expectedToasMsg = string.Empty;
            bool isCaptured = false;

            foreach (var item in attributeList)
            {
                string selectedItem_Xpath;

                switch (attributeName)
                {
                    case "Building Phases":
                        selectedItem_Xpath = $"//*[@id='ctl00_CPH_Content_rlbBuildingPhases']/div/ul/li/label/span[contains(text(),'{item}')]/../input";
                        expectedToasMsg = $"building phases added to cutoff phase {cutoffPhaseName}.";
                        SaveAttribute_btn = SaveBuildingPhase_btn;
                        break;

                    case "Option Groups":
                        selectedItem_Xpath = $"//*[@id='ctl00_CPH_Content_rlbOptionGroups']/div/ul/li/label/span[contains(text(),'{item}')]/../input";
                        expectedToasMsg = $"Option Groups added to Cutoff Phase {cutoffPhaseName} successfully!";
                        SaveAttribute_btn = SaveOptionGroup_btn;
                        break;

                    default:
                        // Default is "Options"
                        selectedItem_Xpath = $"//*[@id='ctl00_CPH_Content_rlbOptions']/div/ul/li/label/span[contains(text(),'{item}')]/../input";
                        expectedToasMsg = $"Options added to Cutoff Phase {cutoffPhaseName} successfully!";
                        SaveAttribute_btn = SaveOption_btn;
                        break;
                }

                selectedItem = new CheckBox(FindType.XPath, selectedItem_Xpath);
                if (selectedItem.IsDisplayed(isCaptured) is false)
                {
                    ExtentReportsHelper.LogInformation($"<font color='yellow'>The {attributeName} with name '{item}' doesn't display on the 'Add {attributeName} To Cutoff Phase' modal.</font>");
                    break;
                }
                else
                {
                    selectedItem.SetCheck(true, isCaptured);
                }
            }

            // Click Save button and verify toast message
            SaveAttribute_btn.Click(isCaptured);

            string _actualMessage = GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage) && !_actualMessage.EndsWith(expectedToasMsg))
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Could not assign {attributeName} to current Cutoff Phase.</font>" +
                    $"<br>The expected message: {expectedToasMsg}" +
                    $"<br>The actual message: {_actualMessage}</br>");
                CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Assign {attributeName} to current Cutoff Phase successfuly.</b></font>");
            }
        }

        /// <summary>
        /// Verify attribute is existed on the grid view or not. Attribute can be Building Phases/ Option Groups/ Options
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="isAssign"></param>
        /// <param name="attributeList"></param>
        public void VerifyAttributeOnGrid(string attributeName, bool isAssign, params string[] attributeList)
        {
            string function;
            if (isAssign is true)
                function = "Assigned";
            else
                function = "Imported";

            // Verify all items on list
            foreach (string item in attributeList)
            {
                if (IsItemInGrid(attributeName, "Name", item) is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>{function} {attributeName} with name '{item}' to current Cutoff Phase successfully.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find {attributeName} with name {item} on the grid view." +
                        $"<br>Failed to {function} {attributeName} '{item}' to current Cutoff Phase.</font>");
            }
        }

        /// <summary>
        /// Import/ Export Cutoff Phase attributes from Utilities menu
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cutoffPhaseName"></param>
        public void ImportExporFromMoreMenu(string item, string cutoffPhaseName)
        {
            string optionExportName = "Pipeline_OptionsToCutOffPhase_" + cutoffPhaseName;
            string optionGroupExportName = "Pipeline_OptionGroupsToCutOffPhase_" + cutoffPhaseName;

            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();

            switch (item)
            {
                case "Export CSV (Option Groups to Cut Off Phase)":
                    SelectItemInUtiliestButton(item, true);
                    ExportFile("CSV", optionGroupExportName);
                    break;

                case "Export Excel (Option Groups to Cut Off Phase)":
                    SelectItemInUtiliestButton(item, true);
                    ExportFile("Excel", optionGroupExportName);
                    break;

                case "Export CSV (Options to Cut Off Phase)":
                    SelectItemInUtiliestButton(item, true);
                    ExportFile("CSV", optionExportName);
                    break;

                case "Export Excel (Options to Cut Off Phase)":
                    SelectItemInUtiliestButton(item, true);
                    ExportFile("Excel", optionExportName);
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
        /// Export Option/ Option Groups in Cutoff Phase to CSV/ Excel file
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
        /// Import Option/ Option Groups to Cutoff Phase 
        /// </summary>
        /// <param name="importTitle"></param>
        /// <param name="importFileDir"></param>
        public void ImportFile(string importTitle, string importFileDir)
        {
            string textboxUpload_Xpath, importButtion_Xpath, message_Xpath;
            switch (importTitle)
            {
                case "Option Groups To Cutoff Phases Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionGroupsToCutOffPhase']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportOptionGroupsToCutOffPhase']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblOptionGroupsToCutOffPhase']";
                    break;

                case "Options To Cutoff Phases Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportOptionsToCutOffPhase']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportOptionsToCutOffPhase']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblOptionGroupsToCutOffPhase']";
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
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import {importTitle} to current Cutoff Phase successfuly.</b></font>");
            }

        }
    }
}
