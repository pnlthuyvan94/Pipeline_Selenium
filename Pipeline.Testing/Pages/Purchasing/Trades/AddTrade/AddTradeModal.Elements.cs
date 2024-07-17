using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.Trades.AddTrade
{
    public partial class AddTradeModal : TradesPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id=\"trades-modal\"]/section/header/h1");

        protected Textbox TradeName_txt => new Textbox(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_txtTradeName\"]");

        protected Textbox Code_txt => new Textbox(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_txtTradeCode\"]");

        protected Textbox TradeDescription_txt => new Textbox(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_txtTradeDescription\"]");
        protected RadioButton QualifyVendor_rbtn => new RadioButton(FindType.Id, "ctl00_CPH_Content_rbVendorQualifyFilter_0");
        protected RadioButton QualifyBuilderVendor_rbtn => new RadioButton(FindType.Id, "ctl00_CPH_Content_rbVendorQualifyFilter_1");
        protected DropdownList CompanyVendor_ddl => new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_ddlVendors\"]");
        protected DropdownList Vendor_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_lsVendors");
        protected DropdownList BuilderVendor_dll => new DropdownList(FindType.Id, "ctl00_CPH_Content_lsBuilderVendors");
        protected DropdownList BuildingPhases_ddl => new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_lstBuildingPhase\"]");

        protected DropdownList SchedulingTasks_ddl => new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_lsSchedulingTask\"]");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_lbSaveContent\"]");

        protected Button Close_btn => new Button(FindType.XPath, "//*[@id=\"trades-modal\"]/section/header/a");

    }

}
