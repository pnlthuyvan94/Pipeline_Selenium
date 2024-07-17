using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionSelection.AddOptionSelection
{
    public partial class AddOptionSelectionModal : OptionSelectionPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");

        protected Textbox OptionSelectionName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddSName']");

        protected DropdownList OptionSelectionGroup_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSelectionGroups']");

        protected CheckBox Customizable_cb
            => new CheckBox(FindType.XPath, "//label[contains(.,'Customizable')]");

        protected Button OptionSelectionSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertS']");

        protected Button OptionSelectionClose_btn
            => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");
    }

}
