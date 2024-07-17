using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BuildingPhaseType.AddType;


namespace Pipeline.Testing.Pages.Estimating.BuildingPhaseType
{
    public partial class BuildingPhaseTypePage : DashboardContentPage<BuildingPhaseTypePage>
    {
        public AddTypeModal AddTypeModal { get; private set; }
        protected IGrid BuildingPhaseType_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBuildingPhaseTypes_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhaseTypes']/div[1]");
        protected Textbox BuildingPhaseTypeUpdate_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBuildingPhaseTypes_ctl00']//td/input[contains(@id,'txtBuildingPhaseTypes_Name') and @type='text']");
        protected Button BuildingPhaseTypeUpdate_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBuildingPhaseTypes_ctl00']//td/input[contains(@id,'UpdateButton') and @title='Update']");

    }
}
