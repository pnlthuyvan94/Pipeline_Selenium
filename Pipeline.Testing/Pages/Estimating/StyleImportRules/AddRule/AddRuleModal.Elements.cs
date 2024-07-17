using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
namespace Pipeline.Testing.Pages.Estimating.StyleImportRules.AddRule
{
    public partial class AddRuleModal : StyleImportRulesPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='options-modal']/section/article/header/h1");

        protected CheckBox Active_chk
            => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ckbNewActive']");

        protected DropdownList DefaultStyle_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewDefaultStyle']");

        protected DropdownList Style_chklst
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_lboxNewStyles']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");

    }

}
