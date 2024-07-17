using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BuildingGroup.AddBuildingGroup;


namespace Pipeline.Testing.Pages.Estimating.BuildingGroup
{
    public partial class BuildingGroupPage : DashboardContentPage<BuildingGroupPage>
    {
        public AddBuildingGroupModal AddBuildingGroupModal { get; private set; }

        protected IGrid BuildingGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBuildingGroups_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingGroups']/div[1]");
        protected Button BulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions']");
        protected Button Add_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNew']");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@id='hypUtils']");
    }
}
