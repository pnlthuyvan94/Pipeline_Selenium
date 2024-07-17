using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.Bid_Costs
{
    public partial class BidCostsToHousePage : DashboardContentPage<BidCostsToHousePage>
    {
        protected readonly string optionBuildingPhaseLoadingIcon = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]";

        protected Grid OptionBuildingPhaseGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBidCosts_ctl00']", optionBuildingPhaseLoadingIcon);

        protected Textbox HouseOptionBidCostHouseOverried_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBidCosts_ctl00']//td/input[contains(@id,'txtHouseBidCost') and @type='text']");

        protected Button HouseOptionBidCostUpdate_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBidCosts_ctl00']//td/input[contains(@id,'UpdateButton') and @title='Update']");
    }

}
