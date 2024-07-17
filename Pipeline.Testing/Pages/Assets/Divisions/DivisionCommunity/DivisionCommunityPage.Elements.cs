using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity.AddCommunity;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity
{
    public partial class DivisionCommunityPage : DetailsContentPage<DivisionCommunityPage>
    {
        public DivisionCommunityModal DivisionCommunityModal { get; private set; }

        protected Label DivisionCommunity_lbl
            => new Label(FindType.XPath, "//*[@id='aspnetForm']/div[3]/section[2]/article/section[2]/article/header/div/h1");

        protected IGrid DivisionCommunity_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCommunities_ctl00']"
            , "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");

    }
}
