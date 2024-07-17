using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor.AddVendorToTrade
{
    public partial class AddVendorToTradeModal : DashboardContentPage<Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor.TradeVendorPage>
    {
        public AddVendorToTradeModal() : base()
        {
        }


        protected Textbox Search_text => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSearchVendors");
        protected ListItem Vendor_list => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rlbVendors']/div[1]/ul/li"));
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertVendors");
        protected Label ModalTitle_lbl
           => new Label(FindType.XPath, "//*[@id='sg-bd-modal']/section/header/h1");

    }
}
