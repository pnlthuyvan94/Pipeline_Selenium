using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Communities
{
    public partial class CommunityPage : DashboardContentPage<CommunityPage>
    {
        public CommunityPage() : base()
        {
        }

        protected IGrid CommunityPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCommunities_ctl00']", "//*[@id='ctl00_CPH_Content_RadAjaxLoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");

        protected Button BuildProSync_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbStartSync");

        protected Button SyncToBuildPro_Btn => new Button(FindType.Id, "ctl00_CPH_Content_BuildProSyncModal_lbBuildProIntegrationSync");
    }
}
