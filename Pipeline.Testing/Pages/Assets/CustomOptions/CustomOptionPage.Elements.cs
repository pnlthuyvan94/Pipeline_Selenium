using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Assets.CustomOptions
{
    public partial class CustomOptionPage : DashboardContentPage<CustomOptionPage>
    {
        protected IGrid CustomOptionPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCustomOptions_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomOptions']/div[1]");
    }
}
