using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule.AddBuildingPhaseRule;


namespace Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule
{
    public partial class BOMBuildingPhaseRulePage : DashboardContentPage<BOMBuildingPhaseRulePage>
    {

        public AddBuildingPhaseRuleModal AddBuildingPhaseRuleModal { get; private set; }
        protected IGrid BuildingPhaseRule_Grid => new Grid(FindType.XPath,"//*[@id='ctl00_CPH_Content_rgBOMPhaseRules_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMPhaseRules']/div[1]");
        protected Button Add_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewBOMPhaseRule']");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypUtils']");
        protected DropdownList UpdateNewSubBuildingPhase_ddl => new DropdownList(FindType.XPath, "//*[contains(@id,'ddlNewChildPhase')]");
        protected Button UpdateNewSubBuildingPhase_btn => new Button(FindType.XPath, "//*[contains(@id,'UpdateButton')]");
        protected Button BulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions']");
    }
}
