using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.OptionSelectionGroup.AddOptionSelectionGroup;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup
{
    public partial class OptionSelectionGroupPage : DashboardContentPage<OptionSelectionGroupPage>
    {
        public AddOptionSelectionGroupModal AddOptionSelectionGroup { get; private set; }

        protected IGrid OptionSelectionGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSelectionGroups_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgSelectionGroups']/div[1]");
    }
}
