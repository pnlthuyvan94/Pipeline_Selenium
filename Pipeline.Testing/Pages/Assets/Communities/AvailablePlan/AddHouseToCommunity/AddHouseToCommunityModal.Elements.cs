using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.AvailablePlan.AddHouseToCommunity
{
    public partial class AddHouseToCommunityModal : AvailablePlanPage
    {
        protected Label AddHouseToCommunity_lbl
            => new Label(FindType.XPath, "//*[@id='plans - modal']/section/header/h1");

        protected DropdownList House_ddl =>
            new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlHousesNotAssigned']");

        protected Textbox Price_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtPrice']");

        protected Textbox Note_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNotes']");
        
        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertHouse']");

        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='plans - modal'']/section/header/a");

        private string _loadingGiftSelectHouse => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_txtHousePrice']/div[1]";

    }

}
