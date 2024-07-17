using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.StyleImportRules.AddRule;


namespace Pipeline.Testing.Pages.Estimating.StyleImportRules
{
    public partial class StyleImportRulesPage : DetailsContentPage<StyleImportRulesPage>
    {
        public string SelectedStyle = "";
        public AddRuleModal AddRuleModal { get; private set; }

        protected IGrid BuildingPhaseRule_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgRules_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgRules']/div[1]");

        protected Button StyleSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNew']");

    }
}
