using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Assets.OptionType.AddOptionType
{
    public partial class AddOptionTypeModal : OptionTypePage
    {

        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='optionTypes-modal']/section/header/h1");

        protected Textbox OptionTypeName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox SortOrder_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSortOrder']");

        protected Textbox DisplayName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDisplayName']");

        protected CheckBox IsPathwayVisible_ckb => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlPathway']/section[2]/span[1]/label");

        protected CheckBox IsFlexPlan_ckb => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlPathway']/section[2]/span[2]/label");

        protected Button OptionTypeSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertOptionType']");

        protected Button OptionTypeClose_btn => new Button(FindType.XPath, "//*[@id='optionTypes-modal']/section/header/a");
    }

}
