using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.SpecSet.AddSpecSetGroup
{
    public partial class AddSpecSetGroupModal : SpecSetPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='options-modal']/section/header/h1");

        protected Textbox GroupName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewGroupName']");

        protected CheckBox UseDefault_chb
            => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ckbUseDefault']");

        protected Button SpecSetSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertGroup']");

        protected Button SpecSetClose_btn
           => new Button(FindType.XPath, "//*[@id='options-modal']/section/header/a");

    }

}
