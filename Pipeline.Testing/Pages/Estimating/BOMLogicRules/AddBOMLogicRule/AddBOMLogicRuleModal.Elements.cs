

using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules.AddBOMLogicRule
{
    public partial class AddBOMLogicRuleModal : BOMLogicRulePage
    {
        protected Label AddBOMLogicRule_lbl => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//h1[text() = 'Create Rule']");

        protected Textbox RuleName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtRuleName']");

        protected Textbox RuleDescription_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBomRuleDescription']");
        protected Textbox SortOrder_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBomRuleSortOrder']");

        protected Button BOMLogicRuleSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_btnSave']");

        protected Button CloseModal_btn => new Button(FindType.XPath, "//*[@class='close']");
    }
}
