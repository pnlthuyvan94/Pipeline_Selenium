using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.Trades.EditTrade
{
    public partial class EditTradeModal : TradesPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id=\"trades-modal-edit\"]/section/header/h1");

        protected Textbox TradeName_txt => new Textbox(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_txtTradeNameEdit\"]");

        protected Textbox Code_txt => new Textbox(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_txtTradeCodeEdit\"]");

        protected Textbox TradeDescription_txt => new Textbox(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_txtTradeDescriptionEdit\"]");

        protected DropdownList CompanyVendor_ddl => new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_ddlVendorsEdit\"]");

        protected DropdownList BuildingPhases_ddl => new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_lstBuildingPhaseEdit\"]");

        protected DropdownList SchedulingTasks_ddl => new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_lsSchedulingTaskEdit\"]");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_lbUpdateContent\"]");

        protected Button Close_btn => new Button(FindType.XPath, "//*[@id=\"trades-modal-edit\"]/section/header/a");

    }

}
