

using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
namespace Pipeline.Testing.Pages.Assets.Communities.Spec_Sets
{
    public partial class CommunitySpecSetsPage : DashboardContentPage<CommunitySpecSetsPage>
    {
        protected IGrid SpecSetGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
    }
}
