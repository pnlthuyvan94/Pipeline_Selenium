using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Purchasing.PurchaseOrders.ManageAllPurchaseOrders.ChangeStatus;

namespace Pipeline.Testing.Pages.Purchasing.PurchaseOrders.ManageAllPurchaseOrders
{
    public partial class ManageAllPurchaseOrdersPage : DashboardContentPage<ManageAllPurchaseOrdersPage>
    {
        public ChangeStatusModal ChangeStatusModal { get; private set; }
        protected Grid ManageAllPurchaseOrdersPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgJobPurchaseOrders_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]");

        private string _gridLoading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobPurchaseOrders]/div[1]";
        protected IGrid PO_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgJobPurchaseOrders_ctl00']", _gridLoading);


    }
}
