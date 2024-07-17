using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes.MoveInReadyResource;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes
{
    public partial class MoveInReadyHomesPage : DetailsContentPage<MoveInReadyHomesPage>
    {
        public MoveInReadyResourceGrid MoveInReadyResource { get; private set; }

        protected Button AddMoveInReadyHomes_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypAddSpec']");


        private string _gridLoadingMoveInReadyHomes = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecHomes']/div[1]";

        protected IGrid MoveInReadyHomePage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSpecHomes_ctl00']", _gridLoadingMoveInReadyHomes);

        protected DropdownList HouseBeforeSaving_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlPlanList']");

        protected Textbox HouseAfterSaving_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtHouseName']");

        protected DropdownList Lot_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlLotList']");

        protected DropdownList Status_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSpecStatus']");

        protected Textbox Price_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSpecPrice']");

        protected Textbox Address_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSpecAddress']");

        protected Textbox Basement_txt  => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBasementSQF']");

        protected Textbox FistFloor_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtFirstSQF']");

        protected Textbox SecondFloor_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSecondSQF']");

        protected Textbox Heated_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtHeatedSQF']");

        protected Textbox Total_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtTotalSQF']");

        protected DropdownList Style_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStyle']");

        protected DropdownList Story_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStories']");

        protected DropdownList Bedroom_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBedrooms']");

        protected DropdownList Bathroom_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBathrooms']");

        protected DropdownList Garage_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlGarage']");

        protected Textbox Note_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSpecNotes']");

        protected Button IsModalHome_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlDetails']/section/section[17]/span/label");

        protected Button SaveMoveInReadyHomes_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

    }
}
