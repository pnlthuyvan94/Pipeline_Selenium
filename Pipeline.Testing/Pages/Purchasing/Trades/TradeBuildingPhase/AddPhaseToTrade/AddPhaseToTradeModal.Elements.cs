using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase.AddPhaseToTrade
{
    public partial class AddPhaseToTradeModal : DetailsContentPage<Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase.TradeBuildingPhasePage>
    {
        public AddPhaseToTradeModal() : base()
        {
        }


        //Elements below are for Purchasing Module
        protected Textbox Search_text => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSearchBuildingPhases");
        protected ListItem BuildingPhase_list => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbBuildingPhase']/div[1]/ul/li"));
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertBuildingPhase");
        protected Label ModalTitle_lbl
           => new Label(FindType.XPath, "//*[@id='sg-bd-modal']/section/header/h1");

    }
}
