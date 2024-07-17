using Pipeline.Common.Enums;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Options.Bid_Costs
{
    public partial class BidCostsToOptionPage
    {
        public void FilterOptionBuildingPhaseInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionBuildingPhaseGrid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(optionBuildingPhaseLoadingIcon, 2000);
        }

        public void FilterOptionBuildingPhaseByDropDownInGrid(string columnName, string value)
        {
            string valueToFind_ListItem;
            if ("Building Phase" == columnName)
                valueToFind_ListItem = "//*[contains(@id, 'ddlBuildingPhases_DropDown')]/div/ul";
            else
                // If there are any column with drop down list, display value here
                valueToFind_ListItem = string.Empty;

            OptionBuildingPhaseGrid.FilterByColumnDropDowwn(columnName, valueToFind_ListItem, value);
            WaitingLoadingGifByXpath(optionBuildingPhaseLoadingIcon, 2000);
        }

        public bool IsOptionBuildingPhaseInGrid(string columnName, string value)
        {
            return OptionBuildingPhaseGrid.IsItemOnCurrentPage(columnName, value);
        }

        public void AddOptionBuildingPhase(OptionBuildingPhaseData optionPhaseData)
        {
            Button Add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddBuildingPhase']");
            if (Add_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Can't find Add new Bid Cost button. Stop this step.</font>");
                return;
            }
            Add_btn.Click();

            Label title_lbl = new Label(FindType.XPath, "//h1[text()='Add Building Phase To Option']");
            if (title_lbl.IsDisplayed() is false)
                ExtentReportsHelper.LogFail("<font color='red'>Can't Open Add new Bid Cost modal or the title is incorrect.</font>");

            // Populate the data to modal and save
            PoppulateBuildingPhaseToOptionModal(optionPhaseData);
        }

        public void PoppulateBuildingPhaseToOptionModal(OptionBuildingPhaseData optionPhaseData)
        {
            // Select building phase
            foreach (var item in optionPhaseData.BuildingPhase)
            {
                string selectedBuildingXpath = $"//*[@id='ctl00_CPH_Content_rlbBuildingPhases']//label[./span[text()='{item}']]/input";
                CheckBox temp = new CheckBox(FindType.XPath, selectedBuildingXpath);

                if (temp.IsDisplayed() is true)
                    temp.Check();
                else
                    ExtentReportsHelper.LogWarning($"<font color = 'green'>Can't find phase with value '<b>{item}</b>' to select!</font>");
            }

            // Set Name
            Textbox name_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNames']");
            if (name_txt.IsDisplayed(false) is true && optionPhaseData.Name != string.Empty)
                name_txt.SetText(optionPhaseData.Name);

            // Set Description
            Textbox description_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNotes']");
            if (description_txt.IsDisplayed(false) is true && optionPhaseData.Description != string.Empty)
                description_txt.SetText(optionPhaseData.Description);

            // Set Allowance
            //Textbox allowance_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAllowances']");
            //if (allowance_txt.IsDisplayed(false) is true && optionPhaseData.Allowance > 0)
            //    allowance_txt.SetText(optionPhaseData.Allowance.ToString());

            // Click save button
            Button add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertBuildingPhaseOption']");
            if (add_btn.IsDisplayed() is true)
                add_btn.Click(false);

            // Verify toast message
            string _expectedMessage = $"Building Phase were successfully added.";
            string actualMsg = GetLastestToastMessage();
            if (actualMsg == _expectedMessage)
            {
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Option Bid Cost with phase {optionPhaseData.BuildingPhase} added successfully!</b></font>");
            }
            else
            {
                foreach (var item in optionPhaseData.BuildingPhase)
                {
                    FilterOptionBuildingPhaseByDropDownInGrid("Building Phase", item);
                    if (IsOptionBuildingPhaseInGrid("Building Phase", item) is false)
                        ExtentReportsHelper.LogWarning($"Option  Bid Cost with phase {item} could not be added - Possible constraints preventing additional.");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Option Bid Cost with phase {item} added successfully!</b></font>");
                }
            }
        }

        public void DeleteOptionBuildingPhase(string columnName, string value)
        {
            OptionBuildingPhaseGrid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK, false);
            CommonHelper.WaitUntilElementInvisible(optionBuildingPhaseLoadingIcon, 5, false);
        }
    }

}
