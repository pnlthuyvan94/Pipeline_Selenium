using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.PurchaseOrders.ManageAllPurchaseOrders;

namespace Pipeline.Testing.Pages.Purchasing.PurchaseOrders.ManageAllPurchaseOrders.ChangeStatus
{
    public partial class ChangeStatusModal : ManageAllPurchaseOrdersPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id=\"changestatus-modal\"]/section/header/h1");
        protected DropdownList Status_ddl => new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_ddlStatusPO\"]");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_lbUpdateStatusPO\"]");
    }

}
