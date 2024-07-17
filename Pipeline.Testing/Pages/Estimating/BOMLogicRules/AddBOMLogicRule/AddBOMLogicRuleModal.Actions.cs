

using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules.AddBOMLogicRule
{
    public partial class AddBOMLogicRuleModal
    {
        public AddBOMLogicRuleModal EnterRuleName(string rulename)
        {
            if (!string.IsNullOrEmpty(rulename))
                RuleName_txt.SetText(rulename);
            return this;
        }
        public AddBOMLogicRuleModal EnterRuleDescription(string description)
        {
            if (string.IsNullOrEmpty(description) is false)
                RuleDescription_txt.SetText(description);
            return this;
        }

        public AddBOMLogicRuleModal EnterSortOrder(string value)
        {
            if (string.IsNullOrEmpty(value) is false)
                SortOrder_txt.SetText(value);
            return this;
        }

        public void SelectExecutionType(string value)
        {
            Button OpenBOMExecution_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbBomRuleExecution_Arrow']");
            OpenBOMExecution_btn.Click();
            IWebElement Execution_Modal = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbBomRuleExecution_DropDown']");
            if(Execution_Modal.Displayed)
            {
                CheckBox ExecutionType_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbBomRuleExecution_DropDown']//label[contains(text(),'{value}')]/input");
                CheckBox UncheckExecution_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbBomRuleExecution_DropDown']//label[contains(text(),'During Product Assembly')]/input");
                switch (value)
                {
                    case "Check All":
                        ExecutionType_chk.Check(true);
                        break;

                    case "Pre Product Assembly":
                        UncheckExecution_chk.UnCheck();
                        ExecutionType_chk.Check(true);
                        break;
                    case "During Product Assembly":
                        ExecutionType_chk.Check(true);
                        break;
                    case "Post Product Assembly":
                        UncheckExecution_chk.UnCheck();
                        ExecutionType_chk.Check(true);
                        break;
                    default:
                        ExtentReportsHelper.LogFail("<font color='red'>The Execution Name is not found</font>");
                        return;
                }
            }

        }

        public void Save()
        {
            BOMLogicRuleSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddRule']/div[1]");
        }


        public void CloseModal()
        {
            CloseModal_btn.Click();
            ModalTitle_lbl.WaitForElementIsInVisible(5);
        }
    }
}
