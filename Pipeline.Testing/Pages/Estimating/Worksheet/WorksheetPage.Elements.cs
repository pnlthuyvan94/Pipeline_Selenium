using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Estimating.Worksheet
{
    public partial class WorksheetPage : DashboardContentPage<WorksheetPage>
    {
        protected IGrid WorksheetPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgWorksheets_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheets']/div[1]");
    }
}
