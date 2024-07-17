using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Assets.OptionSelection.SelectionDetail
{
    public partial class SelectionDetailPage : DetailsContentPage<SelectionDetailPage>
    {

        protected Textbox SelectionName_Txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected DropdownList SelectionGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSelectionGroups']");

        protected CheckBox Customizable_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ckbIsCustomizable']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        protected DropdownList ResourcesType_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlResourceTypes']");

        protected IGrid Resources_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelectionResources_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionResources']/div[1]");

        protected Button AddOption_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAdd']");

        protected Button InsertOptionToSelection_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSO']");

        protected Label AddOptionToSelectionTitleModal_lbl => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");

        protected Button CloseAddOptionToSelectionModal_btn => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");

        protected IGrid Option_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelectionInfo_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionInfo']/div[1]");

    }

}
