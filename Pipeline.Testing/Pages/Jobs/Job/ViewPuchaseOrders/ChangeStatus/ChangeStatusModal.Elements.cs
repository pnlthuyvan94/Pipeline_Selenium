using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders.ChangeStatus
{
    public partial class ChangeStatusModal : ViewPurchaseOrdersPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id=\"changestatus-modal\"]/section/header/h1");
        protected DropdownList Status_ddl => new DropdownList(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_ddlStatusPO\"]");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_lbUpdateStatusPO\"]");
    }

}
