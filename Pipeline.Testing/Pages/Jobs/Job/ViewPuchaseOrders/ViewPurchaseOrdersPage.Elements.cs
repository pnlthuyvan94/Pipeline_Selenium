using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders.ChangeStatus;


namespace Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders
{
    public partial class ViewPurchaseOrdersPage : DashboardContentPage<ViewPurchaseOrdersPage>
    {
        protected Grid ViewPuchaseOrder_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView']/div[1]");
        protected Textbox NoRecords_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']//tr[@class='rgNoRecords']");
        protected Label PurchaseOrder_lbl => new Label(FindType.XPath, "//*[@class='TextBox37 s8-']/div");
        protected  Textbox Product1_txt => new Textbox(FindType.XPath, "(//*[@class='txtProductName1 s15-']//div)[1]");
        protected Textbox Product2_txt => new Textbox(FindType.XPath, "(//*[@class='txtProductName1 s15-']//div)[2]");
        protected DropdownList ViewBy_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlViewByTypes");
        protected Button ChangeStatus_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbChangePOStatus");

        public ChangeStatusModal ChangeStatusModal { get; private set; }        
    }
}
