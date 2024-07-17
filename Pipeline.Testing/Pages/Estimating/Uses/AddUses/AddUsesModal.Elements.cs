using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Estimating.Uses.AddUses
{
    public partial class AddUsesModal : UsesPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='add-modal']/section/header/h1");

        protected Textbox UsesName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUseName']");

        protected Textbox UsesDescription_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUseDescription']");

        protected Textbox UsesSortOrder_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUseSortOrder']");

        protected Button UsesSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveNewUse']");

        protected Button UsesClose_btn
           => new Button(FindType.XPath, "//*[@id='add-modal']/section/header/a");

    }

}
