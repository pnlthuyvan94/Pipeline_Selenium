using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.Units.AddUnit;

namespace Pipeline.Testing.Pages.Estimating.Units
{
    public partial class UnitPage : DashboardContentPage<UnitPage>
    {
        public AddUnitPage AddUnitModal { get; private set; }
        protected IGrid Unit_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProductUnitTypes_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductUnitTypes']/div[1]");
    }
}
