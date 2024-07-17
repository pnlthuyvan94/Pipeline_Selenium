using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.OptionSelection.AddOptionSelection;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.OptionSelection
{
    public partial class OptionSelectionPage : DashboardContentPage<OptionSelectionPage>
    {
        public AddOptionSelectionModal AddOptionSelectionModal { get; private set; }
        public IQueryable<Row> TestData_RT01078 { get; private set; }

        protected IGrid OptionSelection_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelections_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelections']/div[1]");
    }
}
