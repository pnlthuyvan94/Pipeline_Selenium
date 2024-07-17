using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Jobs.Job.JobSpecSets
{
    public partial class JobSpecSetsPage : DetailsContentPage<JobSpecSetsPage>
    {
        protected Label SpecSetsHeaderTitle_Lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSetsPanel']");
        protected IGrid JobSpecSetsPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00']", "//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rgSets']/div[1]");
        protected Button PageSize_btn => new Button(FindType.XPath, "//*[@class='rcbArrowCell rcbArrowCellRight']//a");
    }
}
