using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities.Lot.AddLot
{
    public partial class AddLotModal : LotPage
    {
        public AddLotModal() : base()
        {
        }
        protected Label AddLotTitle_lbl
            => new Label(FindType.XPath, "//*[@id='lotinfomodal']/section/header/h1");

        protected Textbox LotNumber_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtLotNum']");

        protected DropdownList Status_ddl =>
            new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_drpStatusNew']");

        protected DropdownList CommunityPhase_ddl =>
            new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_drpCommunityPhase']");

        protected Textbox Acre_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAcre']");

        protected Textbox Cost_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtValue']");

        protected Textbox Markup_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtMarkupValue']");

        protected Textbox premium_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtPremiumValue']");

        protected Textbox Width_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtWidth']");

        protected Textbox Depth_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDepth']");

        protected Textbox Address_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddress']");

        protected Textbox City_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtCity']");

        protected Textbox State_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtState']");

        protected Textbox Zip_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtZip']");

        protected Textbox Latitude_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtLat']");

        protected Textbox Longitude_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtLong']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveLot']");

        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='lotinfomodal']/section/header/a");

    }

}
