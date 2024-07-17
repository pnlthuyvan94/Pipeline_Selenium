using Pipeline.Common.Utils;


namespace Pipeline.Testing.Pages.Costing.OptionBidCost.AddOptionBidCost
{
    public partial class AddOptionBidCostModal
    {
        public void WaitForModalLoadingGif()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddModal']", 1000);
            
        }
         public void WaitSaveButton()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveBidCost']/div[1]");
        }

        public AddOptionBidCostModal SelectCostTier(string tierName)
        {
            CostTier_Dropdown.SelectItem(tierName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal SelectTier1Option(string optionName)
        {
            Tier1Option_Dropdown.SelectItem(optionName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal SelectTier2House(string houseName)
        {
            Tier2House_Dropdown.SelectItem(houseName);
            WaitForModalLoadingGif();
            return this;
        }
        public AddOptionBidCostModal SelectTier2Option(string optionName)
        {
            Tier2Option_Dropdown.SelectItem(optionName);
            WaitForModalLoadingGif();
            return this;
        }
        public AddOptionBidCostModal SelectTier3Community(string communityName)
        {
            Tier3Community_Dropdown.SelectItem(communityName);
            WaitForModalLoadingGif();
            return this;
        }
        public AddOptionBidCostModal SelectTier3Option(string optionName)
        {
            Tier3Option_Dropdown.SelectItem(optionName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal SelectTier4Community(string communityName)
        {
            Tier4Community_Dropdown.SelectItem(communityName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal SelectTier4House(string houseName)
        {
            Tier4House_Dropdown.SelectItem(houseName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal SelectTier4Option(string optionName)
        {
            Tier4Option_Dropdown.SelectItem(optionName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal ClickToggleCondition()
        {
            ToggleConditionFields_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal SelectConditionOption(int index)
        {
            Option_Dropdown.SelectItem(index);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal ClickAddConditionOption()
        {
            AddOptionToFinalCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal SelectConditionOperator(int index)
        {
            Operator_Dropdown.SelectItem(index);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal ClickAddConditionOperator()
        {
            AddOperatorToFinalCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal EnterFinalConditionText(string text)
        {
            FinalCondition_Textbox.Clear();
            FinalCondition_Textbox.AppendKeys(text);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal ClickSaveFinalCondition()
        {
            SaveCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }
        public AddOptionBidCostModal ClickClearFinalCondition()
        {
            ClearCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal ClickCancelFinalCondition()
        {
            CancelCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal SelectBuildingPhase(string phaseName)
        {
            BuildingPhase_Dropdown.SelectItem(phaseName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal EnterBidCostAmount(double amount)
        {
            BidCost_Textbox.Clear();
            BidCost_Textbox.AppendKeys(amount.ToString("N2"));
            WaitForModalLoadingGif();
            return this;
        }

        public string Save()
        {
            SaveBidCost_Btn.Click();
            WaitSaveButton();
            var message = CommonHelper.VisibilityOfAllElementsLocatedBy(20, "/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper' and position()=last()]");
            return message.Text;
        }

        public AddOptionBidCostModal Cancel()
        {
            Cancel_Btn.Click();
            return this;
        }

        public void CloseModal()
        {
            Close_Btn.Click();
            ModalTitle_lbl.WaitForElementIsInVisible(5);
        }

        public AddOptionBidCostModal ClickAddAllowance()
        {
            AddAllowance_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal EnterAllowanceText(double allowance)
        {
            Allowance_Textbox.Clear();
            Allowance_Textbox.AppendKeys(allowance.ToString("N2"));
            WaitForModalLoadingGif();
            return this;
        }

        public AddOptionBidCostModal EnterName(string name)
        {
            Allowance_Textbox.Clear();
            Allowance_Textbox.AppendKeys(name);
            return this;
        }

        public AddOptionBidCostModal EnterDescription(string description)
        {
            Allowance_Textbox.Clear();
            Allowance_Textbox.AppendKeys(description);
            return this;
        }

        public AddOptionBidCostModal ClickSaveAllowance()
        {
            SaveAllowance_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public void CheckPreviousDataRetained(string expectedTier, string expectedOption, string expectedPhase)
        {
            if(CostTier_Dropdown.SelectedItemName == expectedTier)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Previous Cost Tier value is retained.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Previous Cost Tier value is not retained.</b></font>");

            if (Tier1Option_Dropdown.SelectedItemName == expectedOption)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Previous Option value is retained.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Previous Option value is not retained.</b></font>");

            if (BuildingPhase_Dropdown.SelectedItemName == expectedPhase)
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Previous Building Phase value is retained.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Previous Building Phase value is not retained.</b></font>");
        }
    }
}
