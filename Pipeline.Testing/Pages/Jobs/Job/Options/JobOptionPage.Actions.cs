using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Jobs.Job.Options
{
    public partial class JobOptionPage
    {
        /// <summary>
        ///  Approve current configuation
        /// </summary>
        public void ClickApproveConfig()
        {
            if (AprroveConfig_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogInformation($"<font color = yellow>Can't find or 'Approve Config' button isn't enable to click on Option page.</font>");
                return;
            }
            AprroveConfig_btn.Click(false);
            ConfirmDialog(ConfirmType.OK, false);

            // Get current toast message and verify it
            string actualToastMess = GetLastestToastMessage();
            string expectedToastMess = "The record was processed and saved successfully.";

            if (actualToastMess.Equals(expectedToastMess))
                ExtentReportsHelper.LogPass($"<font color='green'><b>Approve config successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Failed to Approve config. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }
            CloseToastMessage();
        }

        /// <summary>
        /// Verify Is item on Custom Option/ Option grid view
        /// </summary>
        /// <param name="optionType"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsItemInGrid(string optionType, string columnName, string value)
        {
            IGrid Grid;
            if (optionType.ToLower() == "option")
                Grid = OptionPage_Grid;
            else
                // CUSTOM OPTION
                Grid = CustomOptionPage_Grid;

            return Grid.IsItemOnCurrentPage(columnName, value);
        }

        /// <summary>
        /// Filter Custom Option/ Option on grid view
        /// </summary>
        /// <param name="optionType"></param>
        /// <param name="columnName"></param>
        /// <param name="gridFilterOperator"></param>
        /// <param name="value"></param>
        public void FilterItemInGrid(string optionType, string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            IGrid Grid;
            string loadingXpath;
            if (optionType.ToLower() == "option")
            {
                Grid = OptionPage_Grid;
                loadingXpath = optionLoadingGrid_xpath;
            }
            else
            {
                // CUSTOM OPTION
                Grid = CustomOptionPage_Grid;
                loadingXpath = customOptLoadingGrid_xpath;
            }

            Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(loadingXpath, 2000);
        }

        /// <summary>
        /// Add a new configuation on Job Option page
        /// </summary>
        public void AddNewConfiguration()
        {
            if (AddConfig_btn.GetAttribute("href") != null)
            {
                int currentConfig = int.Parse(Configuration_ddl.SelectedValue);

                // Add config button is ENABLE then creating a new config to add more option
                AddConfig_btn.Click(false);
                WaitingLoadingGifByXpath(optionLoadingGrid_xpath);

                string actualToastMess = GetLastestToastMessage();
                string expectedToastMess = "Configuration successfully added.";

                if (actualToastMess.Equals(expectedToastMess))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Add a new Configuration successfully.</b></font>");

                    // Verify a new Configuration added to the drop down list
                    Configuration_ddl.RefreshWrappedControl();
                    int newConfig = int.Parse(Configuration_ddl.SelectedValue);
                    if (newConfig != currentConfig + 1)
                        ExtentReportsHelper.LogFail("<font color='red'>The number of configuration isn't same as the expectation." +
                        $"<br>The expected config number: {currentConfig}" +
                        $"<br>The actual config number: {newConfig}</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail("<font color='red'>Failed to add a new Configuration. The toast message isn't same as the expectation." +
                        $"<br>The expected: {expectedToastMess}" +
                        $"<br>The actual: {actualToastMess}</font>");
                }
                CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogInformation("Add configuration isn't enable. New config was added.");
            }
        }

        /// <summary>
        /// Add Option/ Custom Option to Job by option code
        /// </summary>
        /// <param name="optionType"></param>
        /// <param name="options"></param>
        public void AddOptionOrCustomOptionToJob(string optionType, params string[] options)
        {
            Button AddOpt_btn;
            Button SaveOpt_btn;
            Button CloseOpt_btn;
            string loading_Xpath;
            string optionList_Xpath;
            bool isCaptured = false;

            if (optionType.ToLower() == "option")
            {
                AddOpt_btn = AddOption_btn;
                SaveOpt_btn = SaveOption_btn;
                CloseOpt_btn = CloseOption_btn;
                loading_Xpath = optionLoadingGrid_xpath;
                optionList_Xpath = "//*[@id='ctl00_CPH_Content_lstBxOptions']";
            }
            else
            {
                // CUSTOM OPTION
                AddOpt_btn = AddCustomOption_btn;
                SaveOpt_btn = SaveCustomOption_btn;
                CloseOpt_btn = CloseCustomOption_btn;
                loading_Xpath = customOptLoadingGrid_xpath;
                optionList_Xpath = "//*[@id='ctl00_CPH_Content_lstBxCustomOptions']";
            }

            // Add config button is DISABLE. It means the current config isn't approved, so we can add option to job
            if (AddOpt_btn.GetAttribute("href") == null)
            {
                // The Add Option to Job button is DISABLE. Can't click add button then stop this step
                ExtentReportsHelper.LogFail($"<font color='red'>The Add {optionType} to Job button doesn't display to click." +
                    $"<br>Failed to add {optionType} to Job.</font>");
                return;

            }

            // The Add Option/Custom Option to Job button is ENABLE then click it.
            AddOpt_btn.Click(isCaptured);
            WaitingLoadingGifByXpath(loading_Xpath);

            DropdownList optionList = new DropdownList(FindType.XPath, optionList_Xpath);
            // A new Option/Custom Option table will display
            if (optionList.IsNull() || optionList.GetTotalItems == 0)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find any {optionType} to select after clicking 'Add' button." +
                $"<br>Failed to add {optionType} to Job.</font>");
                return;
            }

            // Seclect Option/ Custom Option on the list
            optionList.SelectItems(options);

            SaveOpt_btn.Click(isCaptured);
            WaitingLoadingGifByXpath(loading_Xpath);

            string actualToastMess = GetLastestToastMessage();
            string expectedToastMess = $"{optionType.ToLower()}s added.";

            if (actualToastMess.EndsWith(expectedToastMess))
                ExtentReportsHelper.LogPass($"<font color='green'><b>Add {optionType} to Job successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Failed to add {optionType} to Job. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }

            CloseOpt_btn.Click(isCaptured);
            WaitingLoadingGifByXpath(loading_Xpath);
        }

        /// <summary>
        /// Remove Option/ Custom Opt from Job
        /// </summary>
        /// <param name="optionType"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void RemoveOptionOrCustomOptionFromJob(string optionType, string columnName, string value)
        {
            // Get correct grid to delete option
            IGrid Grid;
            string LoadingGrid_Xpath;
            if (optionType.ToLower() == "option")
            {
                Grid = OptionPage_Grid;
                LoadingGrid_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']";
            }             
            else
            {
                // CUSTOM OPTION
                Grid = CustomOptionPage_Grid;
                LoadingGrid_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomOptions']";
            }

            Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(LoadingGrid_Xpath,1000);

            // Verify toast messaeg
            string actualToastMess = GetLastestToastMessage();
            string expectedToastMess = "The record was processed and saved successfully.";

            if (actualToastMess.Equals(expectedToastMess))
                ExtentReportsHelper.LogPass($"<font color='green'><b>Remove {optionType} with name '{value}' from Job successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Failed to Remove {optionType} with name '{value}' from Job. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }
        }

        /// <summary>
        /// Get current configuration on the Job Option page
        /// </summary>
        /// <returns></returns>
        public string GetCurrentConfigurationNumber()
        {
            return Configuration_ddl.SelectedItemName;
        }

        /// <summary>
        /// Update current config with new value
        /// </summary>
        /// <param name="selectedConfigNum"></param>
        /// <param name="expectedOptionNum"></param>
        /// <param name="expectedCusOptNum"></param>
        public void UpdateCurrentConfigurationNumber(int selectedConfigNum, int expectedOptionNum, int expectedCusOptNum)
        {
            Configuration_ddl.SelectItem(selectedConfigNum.ToString());
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lblConfigDate']");

            // Verify Option grid view
            int currentOptionNum = OptionPage_Grid.GetTotalItems;
            if (currentOptionNum == expectedOptionNum)
                ExtentReportsHelper.LogPass($"<font color='green'><b>The total Options on grid view after switching to configuration '{selectedConfigNum}' is same as the expected value: '{currentOptionNum}'.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The total Options on grid view after switching to configuration '{selectedConfigNum}' is NOT same as the expected value." +
                    $"<br>The expected Option: {expectedOptionNum}" +
                    $"<br>The actual Option: {currentOptionNum}</font>");


            // Verify Custom Option grid view
            int currentCusOptionNum = CustomOptionPage_Grid.GetTotalItems;
            if (currentCusOptionNum == expectedCusOptNum)
                ExtentReportsHelper.LogPass($"<font color='green'><b>The total Custom Options on grid view after switching to configuration '{selectedConfigNum}' is same as the expected value: '{currentCusOptionNum}'.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The total Custom Options on grid view after switching to configuration '{selectedConfigNum}' is NOT same as the expected value." +
                    $"<br>The expected Custom Option: {expectedCusOptNum}" +
                    $"<br>The actual Custom Option: {currentCusOptionNum}</font>");
        }

        /// <summary>
        /// Switch Option view and verify the number item on the grid
        /// </summary>
        /// <param name="optionView"></param>
        /// <param name="expectedOptionNum"></param>
        /// <param name="expectedCusOptNum"></param>
        public void SwitchOptionView(string optionView, int expectedOptionNum, int expectedCusOptNum)
        {
            string loadingXpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lblConfigDate']";
            string expectedView_Xpath;
            string oppositeView_Xpath;
            bool isCaptured = false;

            if (optionView == "View Options Only for Chosen Configuration")
            {
                expectedView_Xpath = "//*[contains(text(), 'View Options Only for Chosen Configuration')]";

                // To open 'View Options Only for Chosen Configuration', we have to click on 'View Sum of Options up to Chosen Configuration.' view
                oppositeView_Xpath = "//*[contains(text(), 'View Sum of Options up to Chosen Configuration.')]";
            }
            else
            {
                // View Sum of Options up to Chosen Configuration.
                expectedView_Xpath = "//*[contains(text(), 'View Sum of Options up to Chosen Configuration.')]";

                // To open 'View Sum of Options up to Chosen Configuration.', we have to click on 'View Options Only for Chosen Configuration' view
                oppositeView_Xpath = "//*[contains(text(), 'View Options Only for Chosen Configuration')]";
            }

            Button ExpectedViewbtn = new Button(FindType.XPath, expectedView_Xpath);
            if (ExpectedViewbtn.IsDisplayed(isCaptured) is false)
            {
                // if the expected view button doesn't display, then clicking to opposite view to switch to the expected view button.
                Button OppositeViewbtn = new Button(FindType.XPath, oppositeView_Xpath);
                if (OppositeViewbtn.IsDisplayed(isCaptured) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>{optionView}</b> button to click.</font>");
                    return;
                }
                OppositeViewbtn.Click(isCaptured);
                WaitingLoadingGifByXpath(loadingXpath);
            }

            // The expected view is displaying, then veriry the item on grid without click it

            // Verify Option grid view
            int currentOptionNum = OptionPage_Grid.GetTotalItems;
            if (currentOptionNum == expectedOptionNum)
                ExtentReportsHelper.LogPass($"<font color='green'><b>The total Options on grid view after clicking '{optionView}' button is same as the expected value: '{currentOptionNum}'.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The total Options on grid view after clicking '{optionView}' button is NOT same as the expected value." +
                    $"<br>The expected value: {expectedOptionNum}" +
                    $"<br>The actual value: {currentOptionNum}</font>");


            // Verify Custom Option grid view
            int currentCusOptionNum = CustomOptionPage_Grid.GetTotalItems;
            if (currentCusOptionNum == expectedCusOptNum)
                ExtentReportsHelper.LogPass($"<font color='green'><b>The total Custom Options on grid view after clicking '{optionView}' button is same as the expected value: '{currentCusOptionNum}'.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The total Custom Options on grid view after clicking '{optionView}' button is NOT same as the expected value." +
                    $"<br>The expected value: {expectedCusOptNum}" +
                    $"<br>The actual value: {currentCusOptionNum}</font>");
        }
        public void DeleteItemInGrid(string columnName, string value)
        {
            OptionPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        }

        public void ViewNotesAndSelections(string selectionNotes, string selectionField, string selectionValue, string selectionSku, string selectionBrandName, string selectionImgUrl)
        {
            OptionPage_Grid.ClickItemInGridWithTextContains("Notes & Selections", "View");
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();

            Button Notes_btn = new Button(FindType.Id, "btnNotes");
            Notes_btn.Click();

            Button Selections_btn = new Button(FindType.Id, "btnSelections");
            Selections_btn.Click();
            CommonHelper.CaptureScreen();

            Label Notes_lbl = new Label(FindType.Id, "lblNotes");
            if(Notes_lbl != null)
            {
                if (Notes_lbl.GetText() == selectionNotes)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Notes value in the drawer component is the expected value.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Notes value in the drawer component is the expected value.</b></font>");

            }

            Label Field_lbl = new Label(FindType.XPath, "//div[@id='selectionsContainer']/div[1]/span[1]");
            if(Field_lbl != null)
            {
                if (Field_lbl.GetText() == selectionField)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Field value in the drawer component is the expected value.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Field value in the drawer component is the expected value.</b></font>");

            }

            Label SKUBrand_lbl = new Label(FindType.XPath, "//div[@id='selectionsContainer']/div[1]/span[3]");
            if (SKUBrand_lbl != null)
            {
                if (SKUBrand_lbl.GetText() == selectionSku + " " + selectionBrandName)
                    ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>SKU and Brand Name values in the drawer component is the expected values.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>SKU and Brand Name values in the drawer component is the expected values.</b></font>");

            }

        }
    }
}
