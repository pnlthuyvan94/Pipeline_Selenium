using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Purchasing.BuildingPhases
{
    public partial class BuildingPhasesPage : DashboardContentPage<BuildingPhasesPage>
    {
        public BuildingPhasesPage() : base()
        {
        }

        protected IGrid BuildingPhase_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBuildingPhases_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']/div[1]");
    }
}
