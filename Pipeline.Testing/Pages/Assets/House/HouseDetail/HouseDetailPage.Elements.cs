using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.House.HouseDetail
{
    public partial class HouseDetailPage : DetailsContentPage<HouseDetailPage>
    {
        #region "House Detail Page"
        
        protected Textbox HouseName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtHouseName']");
        protected Textbox SaleHouseName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtHouseSalesName']");

        protected DropdownList Series_ddl
            => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlSeries");
       
        protected Textbox PlanNumber_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtPlanNo']");

        protected Textbox BasePrice_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBasePrice']");

        protected Textbox SQFTBasement_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBasementSQF']");

        protected Textbox SQFTFloor1_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtFirstSQF']");

        protected Textbox SQFTFloor2_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSecondSQF']");
       
        protected Textbox SQFTHeated_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtHeatedSQF']");

        protected Textbox SQFTTotal_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtTotalSQF']");

        protected DropdownList Style_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStyle']");

        protected DropdownList Stories_ddl
            => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlStories");

        protected DropdownList BedRoom_ddl
            => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlBedrooms");

        protected DropdownList BathRoom_ddl
            => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlBathrooms");

        protected DropdownList Garage_ddl
            => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlGarage");

        protected Textbox Description_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtDescription");
      
        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");
        #endregion
        #region "House Community Phase"
        protected Button CloseCommunityPhaseModal_btn => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");
        protected Button InsertCommunityPhaseToHouse_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddCommunityPhaseToHouse");
        //protected Button AddCommunityPhase_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddCommunity");
        protected Button AddCommunityPhase_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddCommunityPhase");
        protected IGrid CommunityPhase_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgCommunityPhaseForHouse_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunityPhaseForHouse']/div[1]");
        //protected Label AddCommunityPhase_Title => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblHeader']");
        protected Label AddCommunityPhase_Title => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");
        protected Label GridCommunityPhase_CommunityNameItem => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCommunityPhaseForHouse_ctl00']/tbody/tr/td/a");
        protected Textbox CommunityPhasePrice_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtPrice");
        #endregion

    }

}
