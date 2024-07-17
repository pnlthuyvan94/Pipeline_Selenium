using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Costing.Vendor
{
    public partial class VendorPage : DashboardContentPage<VendorPage>
    {

        protected Button SyncToBuildPro => new Button(FindType.Id, "ctl00_CPH_Content_lbStartSync");

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendors']/div[1]";

        protected IGrid VendorPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgVendors_ctl00']", _gridLoading);

        protected Button StartSyncToBuildPro_Btn => new Button(FindType.Id, "ctl00_CPH_Content_BuildProSyncModal_lbBuildProIntegrationSync");

        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@data-original-title='Utilities']");
    }
}
