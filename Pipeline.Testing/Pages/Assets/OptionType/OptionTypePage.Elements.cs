using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.OptionType.AddOptionType;


namespace Pipeline.Testing.Pages.Assets.OptionType
{
    public partial class OptionTypePage : DashboardContentPage<OptionTypePage>
    {
        public AddOptionTypeModal AddOptionTypeModal { get; private set; }
        protected IGrid OptionType_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptionTypes_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionTypes']/div[1]");
    }
}
