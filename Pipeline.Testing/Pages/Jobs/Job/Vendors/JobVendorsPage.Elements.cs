using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM.OneTimeItem;

namespace Pipeline.Testing.Pages.Jobs.Job.Vendors
{
    public partial class JobVendorsPage : DashboardContentPage<JobVendorsPage>
    {

        string grid_Xpath = "//*[@id='ctl00_CPH_Content_RgJobVendors_ctl00']";
        string loadingGrid_xpath = "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_pnlRpt']/div[1]";
        protected Grid JobVendorPage_Grid => new Grid(FindType.XPath, grid_Xpath, loadingGrid_xpath);
    }
}
