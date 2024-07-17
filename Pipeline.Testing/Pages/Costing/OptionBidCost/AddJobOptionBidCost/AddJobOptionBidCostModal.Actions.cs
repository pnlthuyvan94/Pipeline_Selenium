

namespace Pipeline.Testing.Pages.Costing.OptionBidCost.AddJobOptionBidCost
{
    public partial class AddJobOptionBidCostModal
    {
        public void WaitForModalLoadingGif()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddJobModal']", 1000);
        }

        public AddJobOptionBidCostModal SelectJob(string jobName)
        {
            ModalJob_Dropdown.SelectItem(jobName);
            System.Threading.Thread.Sleep(1000);
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal SelectOption(string optionName)
        {
            JobOption_Dropdown.SelectItem(optionName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal ClickToggleCondition()
        {
            JobToggleConditionFields_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal SelectConditionOption(int index)
        {
            JobConditionOption_Dropdown.SelectItem(index);
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal ClickAddConditionOption()
        {
            JobAddOptionToFinalCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal SelectConditionOperator(int index)
        {
            JobOperator_Dropdown.SelectItem(index);
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal ClickAddConditionOperator()
        {
            JobAddOperatorToFinalCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal EnterFinalConditionText(string text)
        {
            JobFinalCondition_Textbox.Clear();
            JobFinalCondition_Textbox.AppendKeys(text);
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal ClickSaveFinalCondition()
        {
            JobSaveCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }
        public AddJobOptionBidCostModal ClickClearFinalCondition()
        {
            JobClearCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal ClickCancelFinalCondition()
        {
            JobCancelCondition_Btn.Click();
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal SelectBuildingPhase(string phaseName)
        {
            JobBuildingPhase_Dropdown.SelectItem(phaseName);
            WaitForModalLoadingGif();
            return this;
        }

        public AddJobOptionBidCostModal EnterBidCostAmount(double amount)
        {
            JobBidCost_Textbox.Clear();
            JobBidCost_Textbox.AppendKeys(amount.ToString("N2"));
            WaitForModalLoadingGif();
            return this;
        }

        public string Save()
        {
            JobSaveBidCost_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveJobBidCost']/div[1]");
            return GetLastestToastMessage();
        }

        public void CloseModal()
        {
            JobClose_Btn.Click();
            JobModalTitle_lbl.WaitForElementIsInVisible(5);
        }
    }
}
