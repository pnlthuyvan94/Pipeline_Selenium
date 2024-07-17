using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule.AddBuildingPhaseRule;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule
{
    public partial class QuantityBuildingPhaseRulePage : DashboardContentPage<QuantityBuildingPhaseRulePage>
    {

        public AddBuildingPhaseRuleModal AddBuildingPhaseRuleModal { get; private set; }

        protected IGrid BuildingPhaseRule_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_radgridPhaseRules_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rapPhGrid']/div[1]");

    }
}
