using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Assets.OptionGroupDetail
{
    public partial class OptionGroupDetailPage : DetailsContentPage<OptionGroupDetailPage>
    {
        protected IGrid OptionGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptionGroups_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionGroups']/div[1]");
        protected Label optionGroupDetail_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblTitle']");
        protected Button addGroupOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddOption']");
        protected Button saveGroupOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSO']");
        protected Button closeGroupOption_btn => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");
        protected Textbox nameGroupOption_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");
        protected DropdownList cutoffGroupOption_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelectionInfo_ctl00']");

        protected IGrid OptionGroupDetail_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelectionInfo_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionInfo']/div[1]");

        protected Label addOptionToOption_lbl => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");
        protected Textbox sortOderOption_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSortOrder']");
        protected Button saveOptionGroup_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");
    }
}
