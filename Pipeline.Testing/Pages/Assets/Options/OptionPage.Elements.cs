using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.Options.AddOption;


namespace Pipeline.Testing.Pages.Assets.Options
{
    public partial class OptionPage : DashboardContentPage<OptionPage>
    {
        public AddOptionModal AddOptionModal { get; private set; }
        protected IGrid OptionPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptions_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
      

    }
}
