using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Options.Assigments
{
    public partial class AssignmentDetailPage : DetailsContentPage<AssignmentDetailPage>
    {
        protected DropdownList AssigmentViewBy_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlViewType']");

        protected Button AddHouse_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddHouse']");

        protected Label AddHouseModalTitle_lbl => new Label(FindType.XPath, "//*[@id='houses-modal']/section/header/h1");

        protected Button InsertHouseToOption_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertHouse']");

        protected Button CloseAddHouseModal_btn => new Button(FindType.XPath, "//*[@id='houses-modal']/section/header/a");

        protected Button AddCommunity_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddCommunity']");

        protected Button InsertCommunitToOption_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertCommunity']");

        protected Button CloseAddCommunity_Btn => new Button(FindType.XPath, "//*[@id='communities-modal']/section/header/a");

        protected Label AddCommunityTitleModal_lbl => new Label(FindType.XPath, "//*[@id='communities-modal']/section/header/h1");

        protected Button AddProduct_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddPQCOModal']");

        protected Button InsertProductToOption_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertNewChild']");

        protected Button CloseProductModal_Btn => new Button(FindType.XPath, "//*[@id='addPQCO-modal']/section/header/a");

        protected Label AddProductTitleModal_lbl => new Label(FindType.XPath, "//*[@id='addPQCO-modal']/section/header/h1");

        protected IGrid AssignmentHouse_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgHouses_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]");
        protected IGrid AssignmentCommunity_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgCommunities_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");
        protected IGrid AssignmentProduct_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgChildOptions_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgChildOptions']/div[1]");

    }
}
