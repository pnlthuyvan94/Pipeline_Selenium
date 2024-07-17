using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;

namespace Pipeline.Testing.Pages.Jobs.Job
{
    public partial class JobPage : DashboardContentPage<JobPage>
    {
        public JobDetailPage JobDetailPage { get; private set; }
        protected Grid JobPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgJobs_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]");
    }
}
