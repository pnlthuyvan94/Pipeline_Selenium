using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail.AddOptionToOptionType;

namespace Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail
{
    public partial class OptionTypeDetailPage : DetailsContentPage<OptionTypeDetailPage>
    {
        public AddOptionToOptionTypeModal AddOptionToOptionTypeModal { get; private set; }

        // Option Type detail page
        protected Label OptionTypeTitle_lbl => new Label(FindType.XPath, "//h1[text()='Option Type']");

        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox SortOrder_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSortOrder']");

        protected Textbox DisplayName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDisplayNam']");

        protected CheckBox IsPathwayVisible_ckb => new CheckBox(FindType.XPath, "//label[text()='Is Pathway Visible']");

        protected CheckBox IsFlexPlan_ckb => new CheckBox(FindType.XPath, "//label[text()='Flex Plan']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        string _loadingXpath => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbLoadingAnimation']/div[1]";

        // Option
        protected Label OptionTitle_lbl => new Label(FindType.XPath, "//h1[text()='Options']");

        protected Button AddOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddOption']");

        protected IGrid Option_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelectionInfo_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionInfo']/div[1]");

        protected CheckBox SelectAllItems_btn => new CheckBox(FindType.XPath, "//*[contains(@id, 'ClientSelectColumnSelectCheckBox')]");

        protected Button BulkAction_btn => new Button(FindType.XPath, "//*[@id='bulk-actions']");

        protected Button DeleteSelectedItem_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbDeleteSelectedOptions']");
    }
}
