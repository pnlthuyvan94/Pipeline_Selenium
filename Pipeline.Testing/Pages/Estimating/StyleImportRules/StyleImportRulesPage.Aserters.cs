using OpenQA.Selenium;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.StyleImportRules
{
    public partial class StyleImportRulesPage
    {
        public bool DisplayCorrectStyleImportRuke(string expectedStyleRuleName, bool isNewItem = false)
        {
            if (isNewItem)
                // Open new Style Import Rule
                return SubHeadTitle() != null && "New Style" == SubHeadTitle() && CurrentURL.EndsWith("mports/StyleImportRules.aspx");

            // Open an existing Style import Rule
            return SubHeadTitle() != null && expectedStyleRuleName == SubHeadTitle() && !CurrentURL.EndsWith("sid=0");
        }

        public bool IsEmptyGridView()
        {
            // Verify that There are no style rule on the grid view 
            var noRuleMessageXpath = "//*[@id='ctl00_CPH_Content_rgRules_ctl00']/tbody/tr/td[text()='There are currently no Import Style Rules defined.']";
            IWebElement noRuleMessage = FindElementHelper.FindElement(FindType.XPath, noRuleMessageXpath);
            if (noRuleMessage != null)
                return true;
            return false;
        }
    }
}
