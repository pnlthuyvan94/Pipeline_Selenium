using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.AddOptionSelectionGroup
{
    public partial class AddOptionSelectionGroupModal : OptionSelectionGroupPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");

        protected Textbox OptionSelectionGroupName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddSGName']");

        protected Textbox SortOrder_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSort']");

        protected Button OptionSelectionGroupSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSG']");

        protected Button OptionSelectionGroupClose_btn
            => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");
    }

}
