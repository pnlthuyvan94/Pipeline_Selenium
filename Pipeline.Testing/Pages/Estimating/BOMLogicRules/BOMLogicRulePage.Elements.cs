using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BOMLogicRules.AddBOMLogicRule;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules
{
    public partial class BOMLogicRulePage : DashboardContentPage<BOMLogicRulePage>
    {
        public AddBOMLogicRuleModal AddBOMLogicRuleModal { get; private set; }

        protected IGrid BOMLogicRule_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBOMLogicRules_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMLogicRules']/div[1]");
    }
}
