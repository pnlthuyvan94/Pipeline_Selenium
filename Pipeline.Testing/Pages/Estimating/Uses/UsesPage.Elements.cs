using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;

namespace Pipeline.Testing.Pages.Estimating.Uses
{
    public partial class UsesPage : DashboardContentPage<UsesPage>
    {
        public AddUsesModal AddUsesModal { get; private set; }

        protected IGrid Uses_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgUses_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUses']/div[1]");
    }
}
