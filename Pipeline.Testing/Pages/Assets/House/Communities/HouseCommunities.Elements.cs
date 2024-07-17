using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.Communities
{
    public partial class HouseCommunities : DetailsContentPage<HouseCommunities>
    {
        private const string closeDialog_Xpath = "//h1[contains(text(),'Add Communities')]//following-sibling::a";
        private const string insertCommunities_Id = "ctl00_CPH_Content_lbInsertCommunity";
        private const string addCommunities_Id = "ctl00_CPH_Content_lbAddCommunity";
        const string _houseCommunitiesGrid = "//table[contains(@id,'Communities')]";
        const string _houseCommunitiesGridLoading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]";
        const string addCommunityModalTitle_Xpath = "//*[contains(@id, 'Content_lblHeader') and text()='Add Community']";
        protected Label AddCommunityModalTitle_lbl => new Label(FindType.XPath, addCommunityModalTitle_Xpath);
        protected Button Add_Btn => new Button(FindType.Id, addCommunities_Id);
        protected Button Insert_Btn => new Button(FindType.Id, insertCommunities_Id);
        protected Button CloseModal_Btn => new Button(FindType.XPath, closeDialog_Xpath);
        protected IGrid HouseCommunities_Grid => new Grid(FindType.XPath, _houseCommunitiesGrid, _houseCommunitiesGridLoading);
        protected Textbox CommunityFieldOfAddCommunityModal_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_cbProperty_Input");
        protected Button CommunityFieldOfAddCommunityModal_btn => new Button(FindType.Id, "ctl00_CPH_Content_cbProperty_Arrow");

    }

}
