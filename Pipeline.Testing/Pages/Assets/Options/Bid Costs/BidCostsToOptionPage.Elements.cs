using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Options.Bid_Costs
{
    public partial class BidCostsToOptionPage : DashboardContentPage<BidCostsToOptionPage>
    {
        protected readonly string optionBuildingPhaseLoadingIcon = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]";
        protected readonly string houseOptionBuildingPhaseLoadingIcon = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouseBidCosts']/div[1]";

        protected Grid OptionBuildingPhaseGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBidCosts_ctl00']", optionBuildingPhaseLoadingIcon);
        protected Grid HouseOptionBuildingPhaseGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgHouseBidCosts_ctl00']", houseOptionBuildingPhaseLoadingIcon);

    }

}
