using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther;
using Pipeline.Testing.Pages.Assets.Communities.AvailablePlan.AddHouseToCommunity;

namespace Pipeline.Testing.Pages.Assets.Communities.AvailablePlan
{
    public partial class AvailablePlanPage : DetailsContentPage<AvailablePlanPage>
    {
        public AddHouseToCommunityModal AddHouseToCommunityModal { get; private set; }

        public AssignLotOrPhaseOrHouseToEachOtherModal AssignedModal { get; private set; }

        protected IGrid AvailablePlan_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgHouses_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]");

        protected Button Add_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddHouse']");

    }
}
