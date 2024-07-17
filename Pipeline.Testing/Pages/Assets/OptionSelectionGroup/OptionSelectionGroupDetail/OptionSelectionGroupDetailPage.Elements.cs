using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail.AddOptionSelectionToGroup;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail
{
    public partial class OptionSelectionGroupDetailPage : DetailsContentPage<OptionSelectionGroupDetailPage>
    {
        public AddOptionSelectionToGroupModal AddOptionSelectionModal { get; private set; }

        // Option Selection detail page
        protected Label OptionSelectionGroupTitle_lbl => new Label(FindType.XPath, "//h1[contains(text(),'Option Selection Group')]");

        protected Textbox OptionSelectionGroupName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox SortOrder_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSortOrder']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");


        // Option Selection
        protected Label OptionSelectionTitle_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblSelectionGroupTitle']");

        protected Button AddOptionSelection_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAdd']");
        protected IGrid OptionSelection_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelectionGroup_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionGroup']/div[1]");

    }
}
