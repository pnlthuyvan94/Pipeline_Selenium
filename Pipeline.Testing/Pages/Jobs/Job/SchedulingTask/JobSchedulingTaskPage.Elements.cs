using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Jobs.Job.SchedulingTask
{
    public partial class JobSchedulingTaskPage : DashboardContentPage<JobSchedulingTaskPage>
    {

        protected Grid Milestones_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgJobScheduling_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobScheduling']/div[1]");
        protected Grid Detail_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgJobScheduling_ctl00_ctl05_Detail10", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobScheduling']/div[1]");

    }
}
