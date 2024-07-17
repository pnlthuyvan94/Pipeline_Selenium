using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.OptionGroup.AddOptionGroup;


namespace Pipeline.Testing.Pages.Assets.OptionGroup
{
    public partial class OptionGroupPage : DashboardContentPage<OptionGroupPage>
    {
        public AddOptionGroupModal AddOptionGroup { get; private set; }
        protected IGrid OptionGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptionGroups_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionGroups']/div[1]");
    }
}
