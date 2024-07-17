using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionGroup.AddOptionGroup
{
    public partial class AddOptionGroupModal : OptionGroupPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='optiongroups-modal']/section/header/h1");
        protected Textbox OptionGroupName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox SortOrder_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSort']");

        protected Button OptionGroupSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertOptionGroup']");

        protected Button OptionGroupClose_btn
            => new Button(FindType.XPath, "//*[@id='optiongroups-modal']/section/header/a");
    }

}
